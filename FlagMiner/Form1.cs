using BrightIdeasSoftware;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FlagMiner
{

    public partial class Form1 : Form
    {

        public string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0";
        string baseUrl = "https://boards.4chan.org/";
        string archiveBaseUrl = "https://a.4cdn.org/";
        string imageBaseUrl = "https://s.4cdn.org/image/country/";
        List<string> boardDict = new List<string> {
            "int/",
            "pol/",
            "sp/"
        };

        string catalogStr = "/archive.json";

        XmlSerializer xs = new XmlSerializer(typeof(SerializableDictionary<string, long>));
        string DefaultflegsBaseUrl = "https://gitlab.com/flagtism/Extra-Flags-for-4chan/raw/master/flags/";
        string flegsBaseUrl = "";
        // // not https bcs installing the certificate on wine is a nightmare
        string backendBaseUrl = "https://countryball.ca/";


        string getUrl = "int/get_flags_api2.php";

        WebHeaderCollection headerCollection = new WebHeaderCollection();

        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();

        string regionDivider = "||";
        FlegComparer flegComparer = new FlegComparer();

        PostComparer postComparer = new PostComparer();
        ImageListHelper helper;
        MergeManager rootManager;

        UpdateManager updateManager;

        ConcurrentQueue<string> helperStack = new ConcurrentQueue<string>();
        ConcurrentQueue<SerializableDictionary<string, RegionalFleg>> MainMergeStack = new ConcurrentQueue<SerializableDictionary<string, RegionalFleg>>();
        ConcurrentQueue<object> MainUpdateStack = new ConcurrentQueue<object>();

        SerializableDictionary<string, RegionalFleg> MainTree = new SerializableDictionary<string, RegionalFleg>();
        const string savedTreeFile = "savedTree.xml";

        const string badFlagString = "Region empty, no flag yet or you did not set.";

        long exclusionDateLong;

        public Image blankImg;

        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public Options options;

        AboutBox1 aboutBox1 = null;

        public string optionsFile = "options.xml";
        private void ParseBtn_Click(object sender, EventArgs e)
        {
            List<string> boardList = new List<string>();

            if (intCheck.Checked)
                boardList.Add("int");
            if (polCheck.Checked)
                boardList.Add("pol");
            if (spCheck.Checked)
                boardList.Add("sp");

            exclusionDateLong = (long)(options.exclusionDate.ToUniversalTime() - UnixEpoch).TotalSeconds;

            SetupForParsing();
            try
            {
                ValidateOptions();
            }
            catch (Exception ex)
            {
                AppendText(DateTime.Now + " : " + ex.ToString() + System.Environment.NewLine);
            }

            StatusText.AppendText(DateTime.Now + " : Mining started." + System.Environment.NewLine);

            BackgroundWorker1.RunWorkerAsync(boardList);
        }


        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int statusFlag = 0;
            List<string> boardList = (List<string>)e.Argument;
            BackgroundWorker worker = (BackgroundWorker)sender;

            bool markedForAbortion = false;

            if (boardList.Count == 0)
            {
                worker.ReportProgress(0,
                    new WorkerUserState
                    {
                        status = WorkerStatus.error,
                        additionalString = "No board selected for parsing. Please select at least one."
                    });
            }

            foreach (string board in boardList) {
                if (markedForAbortion)
                    break;
                try {
                    int errorCode = 0;
                    string response = "";
                    List<string> threads = null;
                    ConcurrentDictionary<string, long> excludedThreads = null;

                    worker.ReportProgress(0, new WorkerUserState { board=board, status=WorkerStatus.starting });

                    errorCode = LoadArchive(board, ref response);
                    RaiseError(errorCode, ref statusFlag);

                    ParseArchive(response, ref threads);
                    LoadExclusionList(board, ref excludedThreads);
                    PurgeExclusionList(ref excludedThreads, ref threads);

                    worker.ReportProgress(0, new WorkerUserState { board = board, status = WorkerStatus.initializing, progress=0, total = threads.Count });

                    long finalTime = (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;

                    for (int i = 0; i <= threads.Count - 1; i += 1) {
                        if ((worker.CancellationPending)) {
                            e.Cancel = true;
                            markedForAbortion = true;
                            break;
                        }

                        worker.ReportProgress(i + 1, new WorkerUserState {
                            board = board,
                            status = WorkerStatus.running,
                            progress = i+1,
                            total = threads.Count
                        });
                        Thread.Sleep(50); // do not flood the server and get banned
                        try {
                            string rawResponse = null;
                            errorCode = LoadThread(board, threads[i], out rawResponse);
                            RaiseError(errorCode, ref statusFlag);

                            List<Post> posts = null;
                            ParseThread(rawResponse, ref posts);

                            Post firstPost = posts[0];
                            finalTime = firstPost.archived_on;

                            if ((options.exclusionByDate && finalTime > exclusionDateLong) || (!options.exclusionByDate)) {
                                List<Fleg> flegs = new List<Fleg>();
                                QueryExtraFlags(board, ref posts, ref flegs);

                                List<RegionalFleg> parsedFlegs = null;
                                ParseFlags(board, posts, ref flegs, ref parsedFlegs);

                                SerializableDictionary<string, RegionalFleg> flagTree = new SerializableDictionary<string, RegionalFleg>();
                                MergeFlegs(parsedFlegs, ref flagTree);

                                //TreeListView1.Roots = flagTree   ' TODO send to concurrent stack and init the rootmanager
                                rootManager.AddToStack(flagTree);
                            }
                            excludedThreads.TryAdd(threads[i], finalTime);

                            // for inner loop catch it here bls
                        } catch (WebException ex) {
                            var resp = (HttpWebResponse)ex.Response;
                            if (resp != null && resp.StatusCode == HttpStatusCode.NotFound) {
                                // skip this and save as exclusion
                                excludedThreads.TryAdd(threads[i], finalTime);
                            } else {
                                // halt everything.. internet down?
                                worker.ReportProgress(i + 1,
                                    new WorkerUserState
                                    {
                                        board = board,
                                        current = threads[i],
                                        status = WorkerStatus.cancelling,
                                        progress = i + 1,
                                        total = threads.Count,
                                        additionalString = ex.ToString()
                                    });
                                markedForAbortion = true;
                            }
                        } catch (Exception ex) {
                            worker.ReportProgress(i + 1,
                                new WorkerUserState
                                {
                                    board = board,
                                    current = threads[i],
                                    status = WorkerStatus.cancelling,
                                    progress = i + 1,
                                    total = threads.Count,
                                    additionalString = ex.ToString()
                                });
                            markedForAbortion = true;
                        }

                        if (markedForAbortion)
                            break; // TODO: might not be correct. Was : Exit For

                    }

                    SaveExclusionList(board, ref excludedThreads);

                    if (markedForAbortion)
                        break;

                } catch (WebException ex) {
                    worker.ReportProgress(0,
                        new WorkerUserState
                        {
                            board = board,
                            status = WorkerStatus.cancelling,
                            additionalString = ex.ToString()
                        });
                    //AppendText(DateTime.Now + " : " + board + " " + ex.ToString() + System.Environment.NewLine);
                    markedForAbortion = true;
                } catch (Exception ex) {
                    worker.ReportProgress(0,
                        new WorkerUserState
                        {
                            board = board,
                            status = WorkerStatus.cancelling,
                            additionalString = ex.ToString()
                        });
                    //AppendText(DateTime.Now + " : " + board + " " + ex.ToString() + System.Environment.NewLine);
                    markedForAbortion = true;
                }
            }
        }


        private void AbortButt_Click(object sender, EventArgs e)
        {
            if (BackgroundWorker1.IsBusy)
                BackgroundWorker1.CancelAsync();
            if (BackgroundWorker2.IsBusy)
                BackgroundWorker2.CancelAsync();
            //StatusText.AppendText(DateTime.Now + " : Abort signal sent." + System.Environment.NewLine);
            parseWorkerObject(sender, new WorkerUserState() { status = WorkerStatus.cancelling });
            SetupForIdle();
        }

        public void SetupForParsing()
        {
            abortMenuItem.Enabled = true;
            abortToolStripButton.Enabled = true;

            mineMenuItem.Enabled = false;
            mineToolStripMenuItem.Enabled = false;

            copyMenuItem.Enabled = true;
            copyToolStripButton.Enabled = true;

            clearmenuItem.Enabled = false;
            clearToolStripButton.Enabled = false;

            saveMenuItem.Enabled = false;
            SaveToolStripButton.Enabled = false;

            loadMenuItem.Enabled = false;
            loadToolStripButton.Enabled = false;

            purgeMenuItem.Enabled = false;
            purgeToolStripButton.Enabled = false;
            ToolTip2.Active = false;

            checkMenuItem.Enabled = false;
            checkToolStripButton.Enabled = false;
            ToolTip1.Active = false;

            subtractMenuItem.Enabled = false;
            subtractToolStripButton.Enabled = false;

            parseMenuItem.Enabled = false;
            parseToolStripMenuItem.Enabled = false;

            GroupBox1.Enabled = false;
            GroupBox2.Enabled = false;
        }

        public void SetupForIdle()
        {
            abortMenuItem.Enabled = false;
            abortToolStripButton.Enabled = false;

            mineMenuItem.Enabled = true;
            mineToolStripMenuItem.Enabled = true;

            copyMenuItem.Enabled = true;
            copyToolStripButton.Enabled = true;

            clearmenuItem.Enabled = true;
            clearToolStripButton.Enabled = true;

            saveMenuItem.Enabled = true;
            SaveToolStripButton.Enabled = true;

            loadMenuItem.Enabled = true;
            loadToolStripButton.Enabled = true;

            if (options.enablePurge) {
                purgeMenuItem.Enabled = true;
                purgeToolStripButton.Enabled = true;
                ToolTip2.Active = false;
            } else {
                purgeMenuItem.Enabled = false;
                purgeToolStripButton.Enabled = false;
                ToolTip2.Active = true;
            }
            if (options.enableCheck) {
                checkMenuItem.Enabled = true;
                checkToolStripButton.Enabled = true;
                ToolTip1.Active = false;
            } else {
                checkMenuItem.Enabled = false;
                checkToolStripButton.Enabled = false;
                ToolTip1.Active = true;
            }
            subtractMenuItem.Enabled = true;
            subtractToolStripButton.Enabled = true;

            parseMenuItem.Enabled = true;
            parseToolStripMenuItem.Enabled = true;

            GroupBox1.Enabled = true;
            GroupBox2.Enabled = true;
        }

        public delegate void AppendTextCallBack(string str);
        public void AppendText(string str)
        {
            AppendTextCallBack d = new AppendTextCallBack(AppendTextCallBackFunction);
            this.Invoke(d, str);
        }

        public void AppendTextCallBackFunction(string str)
        {
            StatusText.AppendText(str);
        }


        // @TODO :  proper error handling
        public static void RaiseError(int errorcode, ref int statusFlag)
        {
            if (errorcode != 0) {
                statusFlag = 1;
                throw new Exception();
            }
        }

        public delegate void UpdateRootsCallBack();

        public void UpdateRootsInvoker()
        {
            UpdateRootsCallBack d = new UpdateRootsCallBack(UpdateRoots);
            this.Invoke(d);
        }

        public void UpdateRoots()
        {
            this.TreeListView1.SuspendLayout();
            this.TreeListView1.Roots = MainTree.Values;
            this.TreeListView1.ResumeLayout();
        }

        public delegate void RefreshTreeCallBack();

        public void RefreshTree()
        {
            RefreshTreeCallBack d = new RefreshTreeCallBack(RefreshTreeFunction);
            this.Invoke(d);
        }

        public void RefreshTreeFunction()
        {
            this.TreeListView1.Refresh();
        }

        public delegate void UpdateTreeViewCallback(List<object> acc);

        public void UpdateTreeViewInvoker(List<object> accumulator)
        {
            UpdateTreeViewCallback d = new UpdateTreeViewCallback(UpdateObjects);
            this.Invoke(d, accumulator);
        }

        public void UpdateObjects(List<object> acc)
        {
            this.TreeListView1.SuspendLayout();
            this.TreeListView1.RefreshObjects(acc);
            this.TreeListView1.ResumeLayout();
        }

        public delegate void setImgSizeCallback(Size imgSize);

        public void SetImgSizeInvoker(Size imgSize)
        {
            setImgSizeCallback d = new setImgSizeCallback(SetImgSize);
            this.Invoke(d, new object[] { imgSize });
        }

        public void SetImgSize(Size imgSize)
        {
            this.ImageList1.ImageSize = imgSize;
            this.TreeListView1.BaseSmallImageList = this.ImageList1;
        }

        private int LoadArchive(string board, ref string rawResponse)
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            StreamReader reader = null;
            string boardUrl = null;

            boardUrl = baseUrl + board + catalogStr;

            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(boardUrl);
            request.UserAgent = options.userAgent;
            response = (System.Net.HttpWebResponse)request.GetResponse();

            HttpStatusCode status = response.StatusCode;

            if (status == HttpStatusCode.OK) {
                reader = new StreamReader(response.GetResponseStream());
                rawResponse = reader.ReadToEnd();

                return 0;
            }

            return 1;
        }

        private void ParseArchive(string response, ref List<string> threads)
        {
            string[] tempArray = null;

            tempArray = ser.Deserialize<string[]>(response);
            // all archived thread numbers listed
            threads = tempArray.ToList();
        }

        private void LoadExclusionList(string board, ref ConcurrentDictionary<string, long> exclusionList)
        {
            var threadDbName = board + ".db";

            // database with already seen threads
            if (System.IO.File.Exists(threadDbName)) {
                SerializableDictionary<string, long> tempList = null;
                FileStream fs = new FileStream(threadDbName, FileMode.Open);
                exclusionList = new ConcurrentDictionary<string, long>();

                tempList = (SerializableDictionary<string, long>)xs.Deserialize(fs);
                foreach (KeyValuePair<string, long> ke in tempList) {
                    exclusionList.TryAdd(ke.Key, ke.Value);
                }
                fs.Close();
            } else {
                exclusionList = new ConcurrentDictionary<string, long>();
            }
        }

        private void SaveExclusionList(string board, ref ConcurrentDictionary<string, long> exclusionList)
        {
            SerializableDictionary<string, long> tempList = new SerializableDictionary<string, long>();
            var threadDbName = board + ".db";
            foreach (KeyValuePair<string, long> ke in exclusionList) {
                tempList.Add(ke.Key, ke.Value);
            }
            FileStream fs = new FileStream(threadDbName, FileMode.Create);
            xs.Serialize(fs, tempList);
            fs.Close();
        }

        private void PurgeExclusionList(ref ConcurrentDictionary<string, long> exclusionList, ref List<string> threadList)
        {

            if (options.exclusionByList) {
                List<string> TBDeleted = new List<string>();
                for (int i = 0; i <= exclusionList.Count - 1; i++) {
                    string str = exclusionList.Keys.ElementAt(i);
                    if (!threadList.Contains(str)) {
                        TBDeleted.Add(str);
                    }
                }

                long bogusshitwedontneednow = 0;
                foreach (string st in TBDeleted) {
                    exclusionList.TryRemove(st, out bogusshitwedontneednow);
                }

                foreach (string st in exclusionList.Keys) {
                    threadList.Remove(st);
                }
            }

            if (options.exclusionByDate) {
                Dictionary<string, long> tempDict = null;
                tempDict = exclusionList.Where(e => e.Value < exclusionDateLong).ToDictionary(e => e.Key, e => e.Value);
                //.ToDictionary(Of String, Long)(Function(e) e.Key, Function(e) e.Value)

                foreach (string st in tempDict.Keys) {
                    threadList.Remove(st);
                }

            }
        }

        public void QueryExtraFlags(string board, ref List<Post> posts, ref List<Fleg> flags)
        {
            List<string> reqStrings = new List<string>();

            if (posts.Count > 0) {
                List<List<long>> chunks = posts.Select(p => p.no).ChunkBy(400);
                foreach (List<long> subList in chunks)
                {
                    StringBuilder tempstr = subList.Aggregate(new StringBuilder(), (acc, p) => acc.Append("," + p.ToString()),(acc)=>acc.Remove(0,1));

                    // better if parallelized?
                    foreach (string st in options.backendServers)
                    {
                        if (st == "") continue;

                        using (WebClient client = new WebClient())
                        {
                            client.Headers["User-Agent"] = options.userAgent;
                            NameValueCollection values = new NameValueCollection {
                                {
                                    "post_nrs",
                                    tempstr.ToString()
                                },
                                {
                                    "board",
                                    board
                                }
                            };
                            var responses = client.UploadValues(st + getUrl, values);
                            var response = Encoding.Default.GetString(responses);

                            flags.AddRange(ser.Deserialize<Fleg[]>(response));

                        }
                    }

                }

            } else {
                if (flags.Count > 0)
                    flags.Clear();
            }
        }

        public int LoadThread(string board, string thread, out string rawResponse, string fullpath = null)
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            StreamReader reader = null;
            string boardUrl = null;

            if (fullpath == null) {
                boardUrl = archiveBaseUrl + board + "/thread/" + thread + ".json";
            } else {
                boardUrl = fullpath + ".json";
            }

            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(boardUrl);
            request.UserAgent = options.userAgent;
            response = (System.Net.HttpWebResponse)request.GetResponse();

            HttpStatusCode status = response.StatusCode;

            if (status == HttpStatusCode.OK) {
                reader = new StreamReader(response.GetResponseStream());
                rawResponse = reader.ReadToEnd();

                return 0;
            }

            rawResponse = null;
            return 1;
        }

        public void ParseThread(string response, ref List<Post> posts)
        {
            ChanThread tempArray = null;
            tempArray = ser.Deserialize<ChanThread>(response);
            // all archived thread numbers listed

            posts = tempArray.posts;
        }

        private void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BackgroundWorker1.IsBusy || BackgroundWorker2.IsBusy) {
                MessageBox.Show("Task is ongoing. Please hit \"Abort\" before closing", "Flag Miner", MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            try {
                SaveOptions();
            } catch (Exception ex) {
                MessageBox.Show(e.ToString() + "\n" + ex.ToString(), "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void SaveOptions()
        {
            FileStream fs = new FileStream(optionsFile, FileMode.Create);

            XmlSerializer optionsSerializer = new XmlSerializer(typeof(Options));
            optionsSerializer.Serialize(fs, options);
            fs.Close();
        }

        private void LoadOptions()
        {
            FileStream fs = null;
            try {
                fs = new FileStream(optionsFile, FileMode.OpenOrCreate);

                XmlSerializer optionsSerializer = new XmlSerializer(typeof(Options));
                options = (Options)optionsSerializer.Deserialize(fs);
            } catch (Exception) {
                // ignore it for now.
            } finally {
                if (fs != null)
                    fs.Close();
            }
            if (options.backendServers == null)
            {
                options.backendServers = new List<string>();
            }
            if (options.exclusionDate > DateTimePicker1.MaxDate || options.exclusionDate < DateTimePicker1.MinDate)
            {
                options.exclusionDate = DateTime.Now;
            }
            DateTimePicker1.Value = options.exclusionDate;
            intCheck.Checked = options.intCheck;
            polCheck.Checked = options.polCheck;
            spCheck.Checked = options.spCheck;
            CheckBox1.Checked = options.exclusionByDate;
            CheckBox2.Checked = options.exclusionByList;
            flegsBaseUrl = options.repoUrl;
        }

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            LoadOptions();

            ser.MaxJsonLength = 10 * 1024 * 1024;

            // otherwise drunkensailor complains 
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            headerCollection.Add("Content-Type", "application/x-www-form-urlencoded");

            Bitmap tempbmp = new Bitmap(1, 1);
            blankImg = Image.FromHbitmap(tempbmp.GetHbitmap());

            helper = new ImageListHelper(this.TreeListView1, helperStack);
            rootManager = new MergeManager(MainMergeStack, ref MainTree, TreeListView1);
            updateManager = new UpdateManager(MainUpdateStack, TreeListView1);

            TreeListView1.CanExpandGetter = ExpandGetter;
            TreeListView1.ChildrenGetter = delegate (object x) { return (IEnumerable)(ChildrenGetter(x)); };

            TreeListView1.HoverSelection = false;
            TreeListView1.HotTracking = false;

            this.ThreadColumn.AspectGetter = ThreadAspect;
            this.TitleColumn.AspectGetter = TitleAspect;
            this.FlagsColumn.ImageGetter = ImageGetter;

            SetupForIdle();
            tempbmp.Dispose();

        }

        public void FormatRow_EventHandler(object sender, FormatRowEventArgs e)
        {
            RegionalFleg fleg = (RegionalFleg)e.Model;
            if ((fleg.isTrollFlag))
                e.Item.BackColor = Color.LightPink;
            if ((fleg.exists))
                e.Item.BackColor = Color.LightGreen;
        }

        public object ThreadAspect(object x)
        {
            RegionalFleg fleg = (RegionalFleg)x;
            return fleg.thread;
        }

        public object TitleAspect(object x)
        {
            RegionalFleg fleg = (RegionalFleg)x;
            return fleg.title;
        }

        public object ImageAspect(object x)
        {
            RegionalFleg fleg = (RegionalFleg)x;
            return fleg.imgurl;
        }

        public object ImageGetter(object x)
        {
            RegionalFleg fleg = (RegionalFleg)x;

            if (helper.HasImage(fleg.imgurl))
            {
                return helper.GetImageIndex(fleg.imgurl);
            }
            if (!fleg.fetching) {
				fleg.fetching = true;
				Task.Factory.StartNew(() =>
				{
					try {
						helper.AddToStack(fleg.imgurl);
					} catch (Exception)
                    {
                        // ignore, for the moment being
					} finally {
					}
				});
			}
			return blankImg;
		}

		public bool ExpandGetter(object x)
		{
            RegionalFleg fleg = (RegionalFleg)x;
            return (fleg.children.Count > 0);
		}

		public object ChildrenGetter(object x)
		{
            RegionalFleg fleg = (RegionalFleg)x;
            return fleg.children.Values;
		}

		public void ParseFlags(string board, List<Post> posts, ref List<Fleg> extraflags, ref List<RegionalFleg> parsedFlegs)
		{
			List<Post> tempPosts;

			List<long> listOfNo = new List<long>();
			foreach (Fleg flag in extraflags) {
                if (listOfNo.Contains(flag.post_nr)) continue;   
				listOfNo.Add(flag.post_nr);
			}

			// some of these variables may come not sorted... sort everything by post # !!!
			listOfNo.Sort();
			tempPosts = posts.Where((Post e) => listOfNo.Contains(e.no)).ToList();
			//purge posts without extraflags
			tempPosts.Sort(postComparer);

			List<Fleg> flegList = null;
			flegList = extraflags.ToList();
			flegList.Sort(flegComparer);

			// all sorted, go on
			parsedFlegs = new List<RegionalFleg>();

			for (int i = 0; i <= listOfNo.Count - 1; i++) {
				Post post = tempPosts[i];
				string fleg = flegList[i].region;

				string[] arr = fleg.Split((new List<string> { regionDivider }).ToArray(), StringSplitOptions.RemoveEmptyEntries);

				// main flag
				string mf = null;
				bool trollflag = false;
				string imgUrl = null;
				if (String.IsNullOrEmpty(post.troll_country)) {
					mf = post.country_name;
					trollflag = false;
					imgUrl = imageBaseUrl + post.country.ToLower() + ".gif";
				} else {
					mf = post.country_name;
					trollflag = true;
					imgUrl = imageBaseUrl + "troll/" + post.troll_country.ToLower() + ".gif";
				}

				var postUrl = baseUrl + board + "/thread/" + post.resto + "#p" + post.no;

                RegionalFleg regFlag = new RegionalFleg()
                {
                    isTrollFlag = trollflag,
                    title = mf,
                    board = board,
                    pNo = post.no.ToString(),
                    imgurl = imgUrl,
                    thread = postUrl,
                    time = post.time
                };

                // loop on children
                RegionalFleg prev = regFlag;
				string completeUrl = flegsBaseUrl + post.country_name + "/";

				for (int child = 0; child <= arr.Length - 1; child++) {
					RegionalFleg curChild = new RegionalFleg();

					curChild.isTrollFlag = false;
					curChild.thread = postUrl;
					curChild.title = arr[child];
					curChild.board = board;
					curChild.pNo = post.no.ToString();
					curChild.time = post.time;
					curChild.imgurl = completeUrl + arr[child] + ".png";

					completeUrl += arr[child] + "/";

					prev.children.Add(curChild.title, curChild);

					prev = curChild;
				}

				parsedFlegs.Add(regFlag);
			}
		}

		public static void MergeFlegs(List<RegionalFleg> collectedFlegs, ref SerializableDictionary<string, RegionalFleg> flegTree)
		{
			//flegTree = New SerializableDictionary(Of String, RegionalFleg)

			foreach (RegionalFleg fleg in collectedFlegs) {
				SerializableDictionary<string, RegionalFleg> curDict = flegTree;
				RegionalFleg curFleg = fleg;

				if (!curDict.ContainsKey(curFleg.title)) {
					curDict.Add(curFleg.title, curFleg);
					// does it copy it all?  TODO CREATE DEEP COPY   48861
				} else {
					RegionalFleg presentFleg = curDict[curFleg.title];
					if (presentFleg.time < curFleg.time) {
                        presentFleg.copySerializableItems(curFleg);
					}
					if (curFleg.children.Count > 0) {
						SerializableDictionary<string, RegionalFleg> curSrcDict = curFleg.children;
						SerializableDictionary<string, RegionalFleg> curDestDict = curDict[curFleg.title].children;
						Merger(ref curSrcDict, ref curDestDict);
					}
				}
			}
		}


		public static void Merger(ref SerializableDictionary<string, RegionalFleg> source, ref SerializableDictionary<string, RegionalFleg> dest)
		{
			foreach (KeyValuePair<string, RegionalFleg> el in source) {
				if (!dest.ContainsKey(el.Key)) {
					dest.Add(el.Key, el.Value);
				} else {
					var curdestfleg = dest[el.Key];
					var cursourcefleg = el.Value;
					if (curdestfleg.time < cursourcefleg.time) {
                        curdestfleg.copySerializableItems(cursourcefleg);
                    }
					if (cursourcefleg.children.Count > 0) {
						Merger(ref cursourcefleg.children, ref curdestfleg.children);
					}
				}
			}
		}

        private void parseWorkerObject(object sender, WorkerUserState userState)
        {
            switch (userState.status)
            {
                case WorkerStatus.starting:
                    StatusText.AppendText(DateTime.Now + " : Parsing " + userState.board + " board" + System.Environment.NewLine);
                    ProgressBar1.Maximum = 1;
                    ProgressBar1.Value = 0;
                    break;
                case WorkerStatus.initializing:
                    StatusText.AppendText(DateTime.Now + " : " + userState.board + " board has " + userState.total + " threads to be parsed" + System.Environment.NewLine);
                    ProgressBar1.Maximum = 1;
                    ProgressBar1.Value = 0;
                    break;
                case WorkerStatus.running:
                    Label2.Text = string.Format("Current board: {0}. Parsing thread {1} of {2}", userState.board, userState.progress, userState.total);
                    ProgressBar1.Maximum = userState.total;
                    ProgressBar1.Value = userState.progress;
                    break;
                case WorkerStatus.cancelling:
                    StatusText.AppendText(DateTime.Now + " : Abort signal sent." + System.Environment.NewLine);
                    break;
                case WorkerStatus.cancelled:
                    StatusText.AppendText(DateTime.Now + " : Aborted." + System.Environment.NewLine);
                    break;
                case WorkerStatus.error:
                    StatusText.AppendText(DateTime.Now + " : An unexpected error occurred: " + userState.additionalString + System.Environment.NewLine);
                    break;
                case WorkerStatus.completed:
                    StatusText.AppendText(DateTime.Now + " : Parsing completed." + System.Environment.NewLine);
                    break;
            }
        }


		private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
            WorkerUserState userState = (WorkerUserState)e.UserState;
            if (userState != null)
            {
                parseWorkerObject(sender, userState);
            }
		}

		private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			SetupForIdle();
            WorkerUserState userState = new WorkerUserState();
			if (e.Cancelled) {
                userState.status = WorkerStatus.cancelled;
			} else if (e.Error != null) {
                userState.status = WorkerStatus.error;
                userState.additionalString = e.Error.ToString();
			} else {
                userState.status = WorkerStatus.completed;
			}
            parseWorkerObject(sender, userState);
            var res = WindowsApi.FlashWindow(Process.GetCurrentProcess().MainWindowHandle, true, true, 5);
		}

		public void ReturnPasta(SerializableDictionary<string, RegionalFleg> dict, string str, ref StringBuilder pasta)
		{
			foreach (KeyValuePair<string, RegionalFleg> ch in dict) {
				RegionalFleg curFleg = ch.Value;
				SerializableDictionary<string, RegionalFleg> curDict = curFleg.children;
				string curString = curFleg.title + ", " + str;
				if (curDict.Count == 0) {
					pasta.AppendLine(">>>/" + curFleg.board + "/" + curFleg.pNo + " " + curString);
				} else {
					ReturnPasta(curDict, curString, ref pasta);
				}
			}
		}


		private void CopyBtn_Click(object sender, EventArgs e)
		{
			// copy links to clipboard
			StringBuilder pasta = new StringBuilder();

			foreach (KeyValuePair<string, RegionalFleg> fleg in MainTree) {
				var curString = fleg.Value.title;
				//Dim curDict As Dictionary(Of String, RegionalFleg) = flegTree
				RegionalFleg curFleg = fleg.Value;
				SerializableDictionary<string, RegionalFleg> curDict = fleg.Value.children;

				if (curDict.Count == 0) {
					pasta.AppendLine(">>>/" + curFleg.board + "/" + curFleg.pNo + " " + curString);
				} else {
					ReturnPasta(curDict, curString, ref pasta);
				}
			}

            if (pasta.Length>0) Clipboard.SetText(pasta.ToString());
		}

		private void Clearbutt_Click(object sender, EventArgs e)
		{
            if (MessageBox.Show("Clear flags, sure?", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
				MainTree.Clear();
				helper.Clear();
				UpdateRoots();
			}
		}

		private void Savebutt_Click(object sender, EventArgs e)
		{
            if (MessageBox.Show("Save current tree to file?", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
				SaveXmlDialog.InitialDirectory = options.saveAndLoadFolder;
				if (SaveXmlDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					string currentFile = SaveXmlDialog.FileName;
					FileStream fs = new FileStream(currentFile, FileMode.Create);
					SerializableDictionary<string, RegionalFleg> CurTree = MainTree;

					XmlSerializer treeSerializer = new XmlSerializer(typeof(SerializableDictionary<string, RegionalFleg>));
					treeSerializer.Serialize(fs, MainTree);
					fs.Close();
				}
			}
		}

		private void Loadbutt_Click(object sender, EventArgs e)
		{
            if (MessageBox.Show("Load tree from file? It will be merged with the current tree", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    OpenXmlDialog.InitialDirectory = options.saveAndLoadFolder;
                    if (OpenXmlDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        foreach (string fileName in OpenXmlDialog.FileNames)
                        {
                            string currentFile = fileName;
                            SerializableDictionary<string, RegionalFleg> temptree = null;
                            FileStream fs = null;
                            try
                            {
                                fs = new FileStream(currentFile, FileMode.Open);
                                XmlSerializer treeSerializer = new XmlSerializer(typeof(SerializableDictionary<string, RegionalFleg>));
                                temptree = (SerializableDictionary<string, RegionalFleg>)treeSerializer.Deserialize(fs);
                                MergeFlegs(temptree.Values.ToList(), ref MainTree);
                            }
                            catch (Exception ex)
                            {
                                AppendText(DateTime.Now + " : " + "Error processing file " + fileName + " " + ex.ToString() + System.Environment.NewLine);
                            }
                            finally
                            {
                                if (fs != null)
                                    fs.Close();
                            }
                        }

                        Task.Run(new Action(CacheFlegs));

                        UpdateRoots();
                        TreeListView1.Invalidate();
                    }
                }
                catch (Exception ex)
                {
                    AppendText(DateTime.Now + " : " + ex.ToString() + System.Environment.NewLine);
                    MessageBox.Show("Error during the process", "Flag Miner", MessageBoxButtons.OK);
                }
			}

		}

		void CacheFlegs()
		{
			List<string> lstr = new List<string>();
			foreach (KeyValuePair<string, RegionalFleg> ke in MainTree) {
				lstr.Add(ke.Value.imgurl);
				foreach (KeyValuePair<string, RegionalFleg> ke2 in ke.Value.children) {
					CacheEm(ke2.Value, ref lstr);
				}
			}
			lstr = lstr.Distinct().ToList();
			ConcurrentDictionary<string, Image> queue = new ConcurrentDictionary<string, Image>();
            ParallelOptions pOpt = new ParallelOptions { MaxDegreeOfParallelism = 10 };
            Parallel.ForEach<string>(lstr, pOpt, str =>
			{
				Image image = ImageListHelper.ScrapeImage(str);
				queue.TryAdd(str, image);
			});
			this.TreeListView1.SuspendLayout();
			foreach (KeyValuePair<string, Image> ke in queue) {
				try {
					if (!helper.HasImage(ke.Key))
						helper.AddImageToCollection(ke.Key, this.TreeListView1.SmallImageList, ke.Value);
				} catch (ArgumentNullException) {
                    // ignored
				}
			}
			this.TreeListView1.ResumeLayout();
			this.RefreshTree();
		}

		public object CacheEm(RegionalFleg fleg, ref List<string> lstr)
		{
			lstr.Add(fleg.imgurl);
			foreach (KeyValuePair<string, RegionalFleg> ke2 in fleg.children) {
				CacheEm(ke2.Value, ref lstr);
			}
			return null;
		}

		public PurgeEnum QueryFlag(string imgurl)
		{
			HttpWebRequest request = null;
			HttpWebResponse response = null;
			try {
				request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(imgurl);
				request.UserAgent = options.userAgent;
				request.Method = "HEAD";
				response = (System.Net.HttpWebResponse)request.GetResponse();

				HttpStatusCode status = response.StatusCode;
				response.Dispose();

				if (status == HttpStatusCode.NotFound) {
					return PurgeEnum.notFound;
				}
				return PurgeEnum.ok;
			} catch (WebException ex) {
				var resp = (HttpWebResponse)ex.Response;
				if (resp != null && resp.StatusCode == HttpStatusCode.NotFound) {
					return PurgeEnum.notFound;
				} else {
					return PurgeEnum.genericError;
				}
			} catch (Exception) {
				return PurgeEnum.genericError;
				// network error
			}
        }

		private PurgeEnum PurgeInvalid(RegionalFleg fleg, string path, int level)
		{
			var basestr = path + "\\" + fleg.title;

			PurgeEnum checkedFlag = default(PurgeEnum);
			if (options.useLocal & level > 0) {
				string initString = "";
				if (fleg.imgurl.Contains(flegsBaseUrl))
					initString = options.localRepoFolder + "\\" + fleg.imgurl.Replace(flegsBaseUrl, "");
				// for regionals
				if (fleg.imgurl.Contains(imageBaseUrl))
					initString = options.localRepoFolder + "\\" + fleg.imgurl.Replace(imageBaseUrl, "");
				// for nationals
				if (File.Exists(initString)) {
					checkedFlag = PurgeEnum.ok;
				} else {
					checkedFlag = PurgeEnum.notFound;
				}
			} else {
				checkedFlag = QueryFlag(fleg.imgurl);
			}
			if (checkedFlag == PurgeEnum.genericError)
				return checkedFlag;
			if ((fleg.isTrollFlag)) {
				if ((options.markTroll)) {
					fleg.markedfordeletion = true;
				} else {
					fleg.markedfordeletion = false;
				}
			}
			if ((checkedFlag == PurgeEnum.notFound)) {
				fleg.markedfordeletion = true;
			} else {
				level += 1;
				foreach (KeyValuePair<string, RegionalFleg> ke in fleg.children) {
					if (PurgeInvalid(ke.Value, basestr, level) == PurgeEnum.genericError) {
						return PurgeEnum.genericError;
					}
				}
				SerializableDictionary<string, RegionalFleg> mirror = fleg.children;
				fleg.children.Where((KeyValuePair<string, RegionalFleg> pair) => pair.Value.markedfordeletion).ToArray().Apply((KeyValuePair<string, RegionalFleg> pair) => mirror.Remove(pair.Key)).Apply();
			}
            return PurgeEnum.ok;
		}

		private void Purgebutt_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Purge inexistent flags? This cannot be undone", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
				var level = 0;
				foreach (KeyValuePair<string, RegionalFleg> ke in MainTree) {
					if (PurgeInvalid(ke.Value, options.localRepoFolder, level) == PurgeEnum.genericError) {
                        MessageBox.Show("An error occurred while checking the flags. Make sure your connection is up and/or local folders are valid.", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				MainTree.Where((KeyValuePair<string, RegionalFleg> pair) => pair.Value.markedfordeletion == true || (options.deleteChildFree && pair.Value.children.Count == 0)).ToArray().Apply((KeyValuePair<string, RegionalFleg> pair) => MainTree.Remove(pair.Key)).Apply();

				UpdateRoots();
				TreeListView1.Invalidate();
			}
		}

		private void CheckExistent( RegionalFleg fleg, int level)
		{
			string initString = "";
            if (string.IsNullOrEmpty(flegsBaseUrl))
                throw new Exception("Repository url is not set. Make sure to set a valid one in the options.");
			if (fleg.imgurl.Contains(flegsBaseUrl))
				initString = options.localSaveFolder + "\\" + fleg.imgurl.Replace(flegsBaseUrl, "");
			// for regionals
			if (fleg.imgurl.Contains(imageBaseUrl))
				initString = options.localSaveFolder + "\\" + fleg.imgurl.Replace(imageBaseUrl, "");
			// for nationals
			fleg.exists = File.Exists(initString);
			level += 1;
			foreach (KeyValuePair<string, RegionalFleg> ke in fleg.children) {
				CheckExistent(ke.Value, level);
			}
		}

		private void Checkbutt_Click(object sender, EventArgs e)
		{
            if (MessageBox.Show("Mark existent flags?", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    var level = 0;
                    foreach (KeyValuePair<string, RegionalFleg> ke in MainTree)
                    {
                        CheckExistent(ke.Value, level);
                    }
                    UpdateRoots();
                    TreeListView1.Invalidate();
                }
                catch (Exception ex)
                {
                    AppendText(DateTime.Now + " : " + ex.ToString() + System.Environment.NewLine);
                }
			}
		}

		private void ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.TreeListView1.ExpandAll();
		}

		private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TreeListView1.CollapseAll();
		}

        ImportForm importForm = null;
		private void Importbutt_Click(object sender, EventArgs e)
		{
			SetupForParsing();
			StatusText.AppendText(DateTime.Now + " : Parsing started." + System.Environment.NewLine);
            if (importForm == null) importForm = new ImportForm(this);
			DialogResult res = importForm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && importForm.links.Count > 0)
            {
				BackgroundWorker2.RunWorkerAsync(importForm.links);

			} else {
				StatusText.AppendText(DateTime.Now + " : Action cancelled by user or no valid thread/posts given." + System.Environment.NewLine);
				SetupForIdle();
			}
		}

		private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
		{
			int statusFlag = 0;
			//Dim boardList As List(Of String) = e.Argument
            BackgroundWorker worker = (BackgroundWorker)sender;

			int errorCode = 0;
            List<Tuple<string, string, string>> threads = (List<Tuple<string, string, string>>)e.Argument;

			threads.Sort();

			for (int i = 0; i <= threads.Count - 1; i += 1) {
				if ((worker.CancellationPending)) {
					e.Cancel = true;
					break;
				}

				string board = threads[i].Item1;

				worker.ReportProgress(i + 1, new object [] {
					"N/A",
					threads.Count
				});
				Thread.Sleep(450);
				// do not flood the server and get banned
				try {
					string rawResponse = null;
					errorCode = LoadThread(board, threads[i].Item2, out rawResponse);
					RaiseError(errorCode, ref statusFlag);

					List<Post> posts = null;
					ParseThread(rawResponse, ref posts);

					List<Fleg> flegs = new List<Fleg>();
					QueryExtraFlags(board, ref posts, ref flegs);

					List<RegionalFleg> parsedFlegs = null;
					ParseFlags(board, posts, ref flegs, ref parsedFlegs);

					SerializableDictionary<string, RegionalFleg> flagTree = new SerializableDictionary<string, RegionalFleg>();
					MergeFlegs( parsedFlegs, ref flagTree);

					//TreeListView1.Roots = flagTree   ' TODO inviare a concurrent stack e inizializzare rootmanager
					rootManager.AddToStack(flagTree);


				// for inner loop catch it here bls
				} catch (WebException ex) {
					var resp = (HttpWebResponse)ex.Response;
					if (resp.StatusCode == HttpStatusCode.NotFound) {
						// anything to do?
					} else {
						// halt everything.. internet down?
						AppendText(DateTime.Now + " : " + board + "/" + threads[i].Item2 + " " + ex.ToString() + System.Environment.NewLine);
					}
				} catch (Exception ex) {
					AppendText(DateTime.Now + " : " + board + "/" + threads[i].Item2 + " " + ex.ToString() + System.Environment.NewLine);

				}

			}

		}

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e) => options.exclusionDate = DateTimePicker1.Value;

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) => options.exclusionByDate = CheckBox1.Checked;

        private void CheckBox2_CheckedChanged(object sender, EventArgs e) => options.exclusionByList = CheckBox2.Checked;

		private void AboutButt_Click(object sender, EventArgs e)
		{
            if (aboutBox1 == null) aboutBox1 = new AboutBox1();
            aboutBox1.ShowDialog();
			SetupForIdle();
		}

		private void Olv_CellRightClick(object sender, CellRightClickEventArgs e)
		{
			try {
				ContextMenuStrip m = new ContextMenuStrip();
                m.Tag = (RegionalFleg)e.Model;
				m.Items.Add("Expand All", null, ExpandHandler);
				m.Items.Add("Collapse All", null, CollapseHandler);
				m.Items.Add(new ToolStripSeparator());
				m.Items.Add("Copy flag to clipboard", null, CopyImageHandler);
				m.Items.Add("Save as...", null, SaveImageHandler);
				m.Items.Add(new ToolStripSeparator());
				m.Items.Add("Copy link", null, CopyLinkHandler);
				e.MenuStrip = m;
			} catch (Exception ex) {
                // ingore it for now
			}
		}

        private void ExpandHandler(object sender, EventArgs e) => TreeListView1.ExpandAll();

        private void CollapseHandler(object sender, EventArgs e) => TreeListView1.CollapseAll();

        private void CopyImageHandler(object sender, EventArgs e)
		{
            RegionalFleg regFlag = (RegionalFleg)TreeListView1.SelectedItem.RowObject;
            try {
				Image image = ImageListHelper.ScrapeImage(regFlag.imgurl);
				Clipboard.SetImage(image);
			} catch (Exception) {
				MessageBox.Show("Error while downloading the flag to copy", "Flag Miner");
			}
		}

		private void SaveImageHandler(object sender, EventArgs e)
		{
            RegionalFleg regFlag = (RegionalFleg)TreeListView1.SelectedItem.RowObject;
            try {
				Image image = ImageListHelper.ScrapeImage(regFlag.imgurl);
				string initString = null;
				if (regFlag.imgurl.Contains(flegsBaseUrl))
					initString = options.localSaveFolder + "\\" + regFlag.imgurl.Replace(flegsBaseUrl, "");
				// for regionals
				if (regFlag.imgurl.Contains(imageBaseUrl))
					initString = options.localSaveFolder + "\\" + regFlag.imgurl.Replace(imageBaseUrl, "");
				// for nationals
				string pth = Path.GetDirectoryName(initString);
				string fileName = Path.GetFileName(initString);
				SaveFileDialog1.InitialDirectory = pth;

				SaveFileDialog1.FileName = fileName;
				if (SaveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					image.Save(SaveFileDialog1.FileName);
			} catch (Exception) {
				MessageBox.Show("Error while downloading the flag to save", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CopyLinkHandler(object sender, EventArgs e)
		{
            RegionalFleg regFlag = (RegionalFleg)TreeListView1.SelectedItem.RowObject;
			Clipboard.SetText(regFlag.thread);
		}

        OptionsForm optionsForm = null;
		private void OptButt_Click(object sender, EventArgs e)
		{
            if (optionsForm == null) optionsForm = new OptionsForm(this);
			optionsForm.ShowDialog();
            SaveOptions();
            LoadOptions();
			SetupForIdle();
		}

        private void IntCheck_CheckedChanged(object sender, EventArgs e) => options.intCheck = intCheck.Checked;

        private void PolCheck_CheckedChanged(object sender, EventArgs e) => options.polCheck = polCheck.Checked;

        private void SpCheck_CheckedChanged(object sender, EventArgs e) => options.spCheck = spCheck.Checked;

        private void ValidateOptions()
		{
            if (options.backendServers == null)
            {
                options.backendServers = new List<string>();
            }
			if (string.IsNullOrEmpty(options.userAgent)) {
                throw new Exception("User Agent not defined. Make sure to set a valid one in the options.");
				//options.userAgent = DefaultUserAgent;
			}
            if (string.IsNullOrEmpty(options.repoUrl))
            {
                throw new Exception("Repository Url not defined. Make sure to set a valid one in the options.");
                //options.repoUrl = DefaultflegsBaseUrl;
            }
            else
            {
                flegsBaseUrl = options.repoUrl;
            }
            if (options.exclusionDate > DateTimePicker1.MaxDate || options.exclusionDate < DateTimePicker1.MinDate)
            {
                options.exclusionDate = DateTime.Now;
            }
        }

		public Form1()
		{
			//MouseMove += Form1_MouseMove;
			Load += Form1_Load;
			FormClosing += Form1_FormClosing;
			InitializeComponent();
		}

        void TreeListView1_SelectionChanged(object sender, EventArgs e)
        {
            if (((TreeListView)sender).SelectedObjects.Count !=0) {
                copyFlagToolStripButton.Enabled = true;
                saveFlagToolStripButton.Enabled = true;
                copyLinkToolStripButton.Enabled = true;
            } else {
                copyFlagToolStripButton.Enabled = false;
                saveFlagToolStripButton.Enabled = false;
                copyLinkToolStripButton.Enabled = false;
            }
        }

        /// <summary>
        /// subtracts src from dest: dest = dest-src
        /// </summary>
        /// <param name="src">subtrahend</param>
        /// <param name="dest">minuend</param>
        /// <remarks></remarks>
        public void SubtractFlegs(SerializableDictionary<String, RegionalFleg> src, ref SerializableDictionary<String, RegionalFleg> dest)
        {
            SerializableDictionary<String, RegionalFleg> curDestDict = dest;
            SerializableDictionary<String, RegionalFleg> curSrcDict = src;

            foreach (KeyValuePair<String, RegionalFleg> ke in curDestDict)
            {
                RegionalFleg Fleg = ke.Value;
                if (curSrcDict.ContainsKey(ke.Key))
                {
                    SubtractFlegs(curSrcDict[ke.Key].children, ref curDestDict[ke.Key].children);
                    if (Fleg.children.Count == 0)
                    {
                        curDestDict[ke.Key].markedfordeletion = true;
                    }
                }
            }
            curDestDict.Where(pair => pair.Value.markedfordeletion).ToList().
                Apply(pair => curDestDict.Remove(pair.Key)).Apply();  // ToList isn't useless. It allows to avoid an InvalidOperationException by editing an object that is being looped. duplicate first maybe?
        }

        private void SubtractButt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Import the flags to subtract from the current tree? The action cannot be undone.", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    OpenXmlDialog.InitialDirectory = options.saveAndLoadFolder;
                    if (OpenXmlDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        foreach (string fileName in OpenXmlDialog.FileNames)
                        {
                            String currentFile = fileName;
                            SerializableDictionary<String, RegionalFleg> temptree;
                            FileStream fs = null;
                            try
                            {
                                fs = new FileStream(currentFile, FileMode.Open);
                                XmlSerializer treeSerializer =
                                    new XmlSerializer(typeof(SerializableDictionary<String, RegionalFleg>));
                                temptree = (SerializableDictionary<String, RegionalFleg>)treeSerializer.Deserialize(fs);
                                SubtractFlegs(temptree, ref MainTree);
                            }
                            catch (Exception ex)
                            {
                                AppendText(DateTime.Now + " : " + "Error processing file " + fileName + " " + ex.ToString() + System.Environment.NewLine);
                            }
                            finally
                            {
                                if (fs != null) fs.Close();
                            }
                        }

                        Task.Run(new Action(CacheFlegs));
                        UpdateRoots();
                        TreeListView1.Invalidate();
                    }
                }
                catch (Exception ex) {
                    AppendText(DateTime.Now + " : " + ex.ToString() + System.Environment.NewLine);
                    MessageBox.Show("Error during the process", "Flag Miner", MessageBoxButtons.OK);
                }
            }
        }
	}
}
