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

    public partial class FlagMiner : Form
    {
        private readonly string baseUrl = Properties.Resources.baseUrl;
        private readonly string archiveBaseUrl = Properties.Resources.archiveBaseUrl;
        private readonly string imageBaseUrl = Properties.Resources.imageBaseUrl;

        private readonly string catalogStr = Properties.Resources.catalogStr;
        private readonly string getUrl = Properties.Resources.getUrl;

        // the divider used in the back-end response
        private readonly string regionDivider = "||";

        // local repository
        string flegsBaseUrl = "";

        // headers used to perform the http requests
        WebHeaderCollection headerCollection = new WebHeaderCollection();

        // serializer to save/read fleg files
        XmlSerializer xs = new XmlSerializer(typeof(SerializableDictionary<string, long>));
        // serializer to parse 4chan data
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();

        // comparers to enable sorting
        FlegComparer flegComparer = new FlegComparer();
        PostComparer postComparer = new PostComparer();

        ImageListHelper helper;
        FlagMergeManager rootManager;

        FlagUpdateManager updateManager;

        ConcurrentQueue<string> helperStack = new ConcurrentQueue<string>();
        ConcurrentQueue<SerializableDictionary<string, RegionalFleg>> MainMergeStack = new ConcurrentQueue<SerializableDictionary<string, RegionalFleg>>();
        ConcurrentQueue<object> MainUpdateStack = new ConcurrentQueue<object>();

        SerializableDictionary<string, RegionalFleg> MainTree = new SerializableDictionary<string, RegionalFleg>();

        long exclusionDateLong;

        public Image blankImg;

        MinerAboutBox minerAboutBox = null;
        ImportForm importForm = null;
        DumperForm dumperForm = null;

        private void ParseBtn_Click(object sender, EventArgs e)
        {
            List<string> boardList = new List<string>();

            if (intCheck.Checked)
            { boardList.Add("int"); }
            if (polCheck.Checked)
            { boardList.Add("pol"); }
            if (spCheck.Checked)
            { boardList.Add("sp"); }

            exclusionDateLong = OptionsManager.OptionsInstance.exclusionDate.To4ChanTime();

            SetupForParsing();
            try
            { ValidateOptions(); }
            catch (Exception ex)
            { AppendText(DateTime.Now + " : " + ex.ToString() + Environment.NewLine); }

            AppendText(DateTime.Now + " : Mining started." + Environment.NewLine);

            MinerBackgroundWorker.RunWorkerAsync(boardList);
        }


        private void MinerBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
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

            foreach (string board in boardList)
            {
                if (markedForAbortion)
                { break; }
                try
                {
                    int errorCode = 0;
                    string response = "";
                    List<long> threads = null;
                    ConcurrentDictionary<long, long> excludedThreads = null;

                    worker.ReportProgress(0, new WorkerUserState { board = board, status = WorkerStatus.starting });

                    errorCode = LoadArchive(board, ref response);
                    RaiseError(errorCode, ref statusFlag);

                    ParseArchive(response, ref threads);
                    LoadExclusionList(board, ref excludedThreads);
                    PurgeExclusionList(ref excludedThreads, ref threads);

                    worker.ReportProgress(0, new WorkerUserState { board = board, status = WorkerStatus.initializing, progress = 0, total = threads.Count });

                    long finalTime = DateTime.UtcNow.To4ChanTime();

                    ParallelOptions parallelOptions = new ParallelOptions();
                    parallelOptions.MaxDegreeOfParallelism = 12;
                    CancellationTokenSource cts = new CancellationTokenSource();
                    parallelOptions.CancellationToken = cts.Token;
                    try
                    {
                        int currentCount = 0;
                        Parallel.ForEach(Enumerable.Range(0, threads.Count), parallelOptions, (i) =>
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                                markedForAbortion = true;
                                cts.Cancel();
                                return;
                            }

                            Interlocked.Increment(ref currentCount);
                            worker.ReportProgress(currentCount, new WorkerUserState
                            {
                                board = board,
                                status = WorkerStatus.running,
                                progress = currentCount,
                                total = threads.Count
                            });
                            Thread.Sleep(10); // do not flood the server and get banned
                            try
                            {
                                errorCode = LoadThread(board, threads[i], out string rawResponse);
                                RaiseError(errorCode, ref statusFlag);

                                List<Post> posts = new List<Post>();
                                try
                                { ParseThread(rawResponse, ref posts); }
                                catch (Exception ex)
                                {
                                    worker.ReportProgress(i + 1,
                                        new WorkerUserState
                                        {
                                            board = board,
                                            current = threads[i],
                                            status = WorkerStatus.curruptJson,
                                            progress = i + 1,
                                            total = threads.Count,
                                            additionalString = ex.ToString()
                                        });
                                }

                                if (posts.Count > 0)
                                {
                                    Post firstPost = posts[0];
                                    finalTime = firstPost.archived_on;

                                    if ((OptionsManager.OptionsInstance.exclusionByDate && finalTime > exclusionDateLong) || (!OptionsManager.OptionsInstance.exclusionByDate))
                                    {
                                        List<Fleg> flegs = new List<Fleg>();
                                        QueryExtraFlags(board, ref posts, ref flegs);

                                        List<RegionalFleg> parsedFlegs = null;
                                        ParseFlags(board, posts, ref flegs, ref parsedFlegs);

                                        SerializableDictionary<string, RegionalFleg> flagTree = new SerializableDictionary<string, RegionalFleg>();
                                        FlegOperations.MergeFlegs(parsedFlegs, ref flagTree);

                                        rootManager.AddToStack(flagTree);
                                    }
                                }
                                excludedThreads.TryAdd(threads[i], finalTime);

                                // for inner loop catch it here bls
                            }
                            catch (Exception ex)
                            {
                                // check if this is due to a thread not being found.
                                if (ex is WebException webEx && (((HttpWebResponse)webEx.Response)?.StatusCode == HttpStatusCode.NotFound))
                                {
                                    // if so skip this and save as exclusion for next time.
                                    excludedThreads.TryAdd(threads[i], finalTime);
                                }
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
                            {
                                cts.Cancel(); // TODO: might not be correct. Was : Exit For
                                return;
                            }


                        });
                    }
                    catch (OperationCanceledException)
                    {
                        // do nothing. this exception is normal when the task is cancelled
                    }

                    SaveExclusionList(board, ref excludedThreads);

                    if (markedForAbortion)
                    { break; }

                }
                catch (WebException ex)
                {
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
                catch (Exception ex)
                {
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
            if (MinerBackgroundWorker.IsBusy)
            { MinerBackgroundWorker.CancelAsync(); }
            if (ThreadParserBackgroundWorker.IsBusy)
            { ThreadParserBackgroundWorker.CancelAsync(); }
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

            checkMenuItem.Enabled = false;
            checkToolStripButton.Enabled = false;

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

            purgeMenuItem.Enabled = OptionsManager.OptionsInstance.enablePurge;
            purgeToolStripButton.Enabled = OptionsManager.OptionsInstance.enablePurge;

            checkMenuItem.Enabled = OptionsManager.OptionsInstance.enableCheck;
            checkToolStripButton.Enabled = OptionsManager.OptionsInstance.enableCheck;

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
            if (errorcode != 0)
            {
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
            this.FlegTreeListView.SuspendLayout();
            this.FlegTreeListView.Roots = MainTree.Values;
            this.FlegTreeListView.ResumeLayout();
        }

        public delegate void RefreshTreeCallBack();

        public void RefreshTree()
        {
            RefreshTreeCallBack d = new RefreshTreeCallBack(RefreshTreeFunction);
            this.Invoke(d);
        }

        public void RefreshTreeFunction()
        {
            this.FlegTreeListView.Refresh();
        }

        public delegate void UpdateTreeViewCallback(List<object> acc);

        public void UpdateTreeViewInvoker(List<object> accumulator)
        {
            UpdateTreeViewCallback d = new UpdateTreeViewCallback(UpdateObjects);
            this.Invoke(d, accumulator);
        }

        public void UpdateObjects(List<object> acc)
        {
            this.FlegTreeListView.SuspendLayout();
            this.FlegTreeListView.RefreshObjects(acc);
            this.FlegTreeListView.ResumeLayout();
        }

        public delegate void setImgSizeCallback(Size imgSize);

        public void SetImgSizeInvoker(Size imgSize)
        {
            setImgSizeCallback d = new setImgSizeCallback(SetImgSize);
            this.Invoke(d, new object[] { imgSize });
        }

        public void SetImgSize(Size imgSize)
        {
            this.FlegImageList.ImageSize = imgSize;
            this.FlegTreeListView.BaseSmallImageList = this.FlegImageList;
        }

        private int LoadArchive(string board, ref string rawResponse)
        {
            string boardUrl = baseUrl + board + catalogStr;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(boardUrl);
            request.UserAgent = OptionsManager.OptionsInstance.userAgent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            if (response.StatusCode != HttpStatusCode.OK)
            { return 1; }

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            { rawResponse = reader.ReadToEnd(); }

            return 0;
        }

        private void ParseArchive(string response, ref List<long> threads)
        {
            long[] tempArray = ser.Deserialize<long[]>(response);
            // all archived thread numbers listed
            threads = tempArray.ToList();
        }

        private void LoadExclusionList(string board, ref ConcurrentDictionary<long, long> exclusionList)
        {
            var threadDbName = board + ".db";

            // database with already seen threads
            if (File.Exists(threadDbName))
            {
                SerializableDictionary<string, long> tempList = null;
                using (FileStream fs = new FileStream(threadDbName, FileMode.Open))
                {
                    exclusionList = new ConcurrentDictionary<long, long>();
                    var exclusionListStr = new ConcurrentDictionary<string, long>();

                    tempList = (SerializableDictionary<string, long>)xs.Deserialize(fs);
                    foreach (KeyValuePair<string, long> ke in tempList)
                    { exclusionListStr.TryAdd(ke.Key, ke.Value); }

                    foreach (var ke in exclusionListStr)
                    { exclusionList.TryAdd(long.Parse(ke.Key), ke.Value); }
                }
            }
            else
            { exclusionList = new ConcurrentDictionary<long, long>(); }
        }

        private void SaveExclusionList(string board, ref ConcurrentDictionary<long, long> exclusionList)
        {
            SerializableDictionary<string, long> tempList = new SerializableDictionary<string, long>();
            var threadDbName = board + ".db";
            foreach (KeyValuePair<long, long> ke in exclusionList)
            { tempList.Add(ke.Key.ToString(), ke.Value); }
            using (FileStream fs = new FileStream(threadDbName, FileMode.Create))
            {
                xs.Serialize(fs, tempList);
            }
        }

        private void PurgeExclusionList(ref ConcurrentDictionary<long, long> exclusionList, ref List<long> threadList)
        {
            Dictionary<long, int> threads = new Dictionary<long, int>();
            foreach (long thr in threadList)
            { threads.Add(thr, 0); }
            if (OptionsManager.OptionsInstance.exclusionByList)
            {
                List<long> TBDeleted = new List<long>();
                for (int i = 0; i <= exclusionList.Count - 1; i++)
                {
                    long str = exclusionList.Keys.ElementAt(i);
                    if (!threads.ContainsKey(str))
                    { TBDeleted.Add(str); }
                }

                long bogusshitwedontneednow = 0;
                foreach (long st in TBDeleted)
                { exclusionList.TryRemove(st, out bogusshitwedontneednow); }

                foreach (long st in exclusionList.Keys)
                { threads.Remove(st); }
            }

            if (OptionsManager.OptionsInstance.exclusionByDate)
            {
                Dictionary<long, long> tempDict = null;
                tempDict = exclusionList.Where(e => e.Value < exclusionDateLong).ToDictionary(e => e.Key, e => e.Value);

                foreach (long st in tempDict.Keys)
                { threads.Remove(st); }

            }
            threadList.Clear();
            threadList.AddRange(threads.Keys);
        }

        public void QueryExtraFlags(string board, ref List<Post> posts, ref List<Fleg> flags)
        {
            List<string> reqStrings = new List<string>();

            if (posts.Count > 0)
            {
                List<List<long>> chunks = posts.Select(p => p.no).ChunkBy(400);
                ConcurrentQueue<Fleg[]> concurrentQueue = new ConcurrentQueue<Fleg[]>();
                Parallel.ForEach(chunks, (subList) =>
                {
                    StringBuilder tempstr = subList.Aggregate(new StringBuilder(), (acc, p) => acc.Append("," + p.ToString()), (acc) => acc.Remove(0, 1));
                    
                    // better if parallelized?
                    foreach (string st in OptionsManager.OptionsInstance.backendServers)
                    {
                        if (st == "") { continue; }

                        using (WebClient client = new WebClient())
                        {
                            client.Headers["User-Agent"] = OptionsManager.OptionsInstance.userAgent;
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

                            concurrentQueue.Enqueue(ser.Deserialize<Fleg[]>(response));
                        }
                    }
                });
                foreach (Fleg[] flagArray in concurrentQueue)
                { flags.AddRange(flagArray); }

            }
            else if (flags.Count > 0)
            { flags.Clear(); }
        }

        public int LoadThread(string board, long thread, out string rawResponse, string fullpath = null)
        {
            string boardUrl;
            if (fullpath == null)
            { boardUrl = archiveBaseUrl + board + "/thread/" + thread.ToString() + ".json"; }
            else
            { boardUrl = fullpath + ".json"; }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(boardUrl);
            request.UserAgent = OptionsManager.OptionsInstance.userAgent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            HttpStatusCode status = response.StatusCode;

            rawResponse = null;

            if (status != HttpStatusCode.OK)
            { return 1; }

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            { rawResponse = reader.ReadToEnd(); }

            return 0;
        }

        public void ParseThread(string response, ref List<Post> posts)
        {
            ChanThread tempArray = null;
            tempArray = ser.Deserialize<ChanThread>(response);
            // all archived thread numbers listed

            if (tempArray != null)
            { posts = tempArray.posts; }
        }

        private void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FlagMiner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MinerBackgroundWorker.IsBusy || ThreadParserBackgroundWorker.IsBusy)
            {
                MessageBox.Show("Task is ongoing. Please hit \"Abort\" before closing", "Flag Miner", MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            try
            { OptionsManager.SaveOptions(); }
            catch (Exception ex)
            {
                MessageBox.Show(e.ToString() + "\n" + ex.ToString(), "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        public void ApplyLoadedOptions() {
            ExclusionDatePicker.Value = OptionsManager.OptionsInstance.exclusionDate;
            intCheck.Checked = OptionsManager.OptionsInstance.intCheck;
            polCheck.Checked = OptionsManager.OptionsInstance.polCheck;
            spCheck.Checked = OptionsManager.OptionsInstance.spCheck;
            ExclusionDateCheckBox.Checked = OptionsManager.OptionsInstance.exclusionByDate;
            ExclusionListCheckBox.Checked = OptionsManager.OptionsInstance.exclusionByList;
            flegsBaseUrl = OptionsManager.OptionsInstance.repoUrl;
        }

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void FlagMiner_Load(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            ExclusionDatePicker.MinDate = OptionsManager.MinDate;
            ExclusionDatePicker.MaxDate = OptionsManager.MaxDate;

            OptionsManager.LoadOptions();
            ApplyLoadedOptions();

            ser.MaxJsonLength = 10 * 1024 * 1024;

            // otherwise drunkensailor complains 
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            headerCollection.Add("Content-Type", "application/x-www-form-urlencoded");

            Bitmap tempbmp = new Bitmap(1, 1);
            blankImg = Image.FromHbitmap(tempbmp.GetHbitmap());

            helper = new ImageListHelper(this.FlegTreeListView, helperStack);
            rootManager = new FlagMergeManager(MainMergeStack, ref MainTree, FlegTreeListView);
            updateManager = new FlagUpdateManager(MainUpdateStack, FlegTreeListView);

            FlegTreeListView.CanExpandGetter = ExpandGetter;
            FlegTreeListView.ChildrenGetter = delegate (object x) { return (IEnumerable)(ChildrenGetter(x)); };

            FlegTreeListView.HoverSelection = false;
            FlegTreeListView.HotTracking = false;

            ThreadColumn.AspectGetter = ThreadAspect;
            TitleColumn.AspectGetter = TitleAspect;
            FlagsColumn.ImageGetter = ImageGetter;

            SetupForIdle();
            tempbmp.Dispose();

        }

        public void FormatRow_EventHandler(object sender, FormatRowEventArgs e)
        {
            RegionalFleg fleg = (RegionalFleg)e.Model;
            if (fleg.isTrollFlag)
            { e.Item.BackColor = Color.LightPink; }
            if (fleg.exists)
            { e.Item.BackColor = Color.LightGreen; }
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
            { return helper.GetImageIndex(fleg.imgurl); }
            if (!fleg.fetching)
            {
                fleg.fetching = true;
                Task.Factory.StartNew(() =>
                {
                    try
                    { helper.AddToStack(fleg.imgurl); }
                    catch (Exception)
                    {
                        // ignore, for the moment being
                    }
                    finally
                    {
                        // @TODO
                    }
                });
            }

            return blankImg;
        }

        public bool ExpandGetter(object x)
        {
            RegionalFleg fleg = (RegionalFleg)x;
            return fleg.children.Count > 0;
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
            foreach (Fleg flag in extraflags)
            {
                if (listOfNo.Contains(flag.post_nr)) { continue; }
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

            for (int i = 0; i <= listOfNo.Count - 1; i++)
            {
                Post post = tempPosts[i];
                string fleg = flegList[i].region;

                string[] arr = fleg.Split((new List<string> { regionDivider }).ToArray(), StringSplitOptions.RemoveEmptyEntries);

                // main flag
                string mf = null;
                bool trollflag = false;
                string imgUrl = null;
                if (String.IsNullOrEmpty(post.country_name))
                { continue; }// no flags here, skip!  
                if (String.IsNullOrEmpty(post.troll_country))
                {
                    if (post.country == "TL") { post.country_name = "Timor-Leste"; }
                    mf = post.country_name;
                    trollflag = false;
                    imgUrl = imageBaseUrl + post.country.ToLower() + ".gif";
                }
                else
                {
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

                for (int child = 0; child <= arr.Length - 1; child++)
                {
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


        private void parseWorkerObject(object sender, WorkerUserState userState)
        {
            switch (userState.status)
            {
                case WorkerStatus.starting:
                    StatusText.AppendText(DateTime.Now + " : Parsing " + userState.board + " board" + Environment.NewLine);
                    ParsingProgressBar.Maximum = 1;
                    ParsingProgressBar.Value = 0;
                    break;
                case WorkerStatus.initializing:
                    StatusText.AppendText(DateTime.Now + " : " + userState.board + " board has " + userState.total + " threads to be parsed" + Environment.NewLine);
                    ParsingProgressBar.Maximum = 1;
                    ParsingProgressBar.Value = 0;
                    break;
                case WorkerStatus.running:
                    Label2.Text = string.Format("Current board: {0}. Parsing thread {1} of {2}", userState.board, userState.progress, userState.total);
                    ParsingProgressBar.Maximum = userState.total;
                    ParsingProgressBar.Value = userState.progress;
                    break;
                case WorkerStatus.cancelling:
                    StatusText.AppendText(DateTime.Now + " : Abort signal sent." + Environment.NewLine);
                    break;
                case WorkerStatus.cancelled:
                    StatusText.AppendText(DateTime.Now + " : Aborted." + Environment.NewLine);
                    break;
                case WorkerStatus.error:
                    StatusText.AppendText(DateTime.Now + " : An unexpected error occurred: " + userState.additionalString + Environment.NewLine);
                    break;
                case WorkerStatus.completed:
                    StatusText.AppendText(DateTime.Now + " : Parsing completed." + Environment.NewLine);
                    break;
                case WorkerStatus.curruptJson:
                    StatusText.AppendText(DateTime.Now + " : Error parsing 4chan json. " + userState.board + "/" + userState.current + ". " + userState.additionalString + Environment.NewLine);
                    break;
            }
        }


        private void MinerBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WorkerUserState userState = (WorkerUserState)e.UserState;
            if (userState != null)
            { parseWorkerObject(sender, userState); }
        }

        private void MinerBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetupForIdle();
            WorkerUserState userState = new WorkerUserState();
            if (e.Cancelled)
            { userState.status = WorkerStatus.cancelled; }
            else if (e.Error != null)
            {
                userState.status = WorkerStatus.error;
                userState.additionalString = e.Error.ToString();
            }
            else
            { userState.status = WorkerStatus.completed; }
            parseWorkerObject(sender, userState);
            var res = WindowsApi.FlashWindow(Process.GetCurrentProcess().MainWindowHandle, true, true, 5);
        }


        private void CopyBtn_Click(object sender, EventArgs e)
        {
            // copy links to clipboard
            StringBuilder pasta = new StringBuilder();
            FlegOperations.ReturnPasta(MainTree, "", ref pasta);

            if (pasta.Length > 0) { Clipboard.SetText(pasta.ToString()); }
        }

        private void DumpWizardToolStripButton_Click(object sender, EventArgs e)
        {
            dumperForm ??= new DumperForm(this);

            DialogResult res = dumperForm.ShowDialog();
            //if (res == DialogResult.OK && dumperForm.links.Count > 0)
            //{ ThreadParserBackgroundWorker.RunWorkerAsync(importForm.links); }
            //else
            //{
            //    StatusText.AppendText(DateTime.Now + " : Action cancelled by user or no valid thread/posts given." + Environment.NewLine);
            //    SetupForIdle();
            //}
        }

        private void Clearbutt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear flags, sure?", "Flag Miner", MessageBoxButtons.YesNo) != DialogResult.Yes)
            { return; }

            MainTree.Clear();
            helper.Clear();
            UpdateRoots();
        }

        private void Savebutt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save current tree to file?", "Flag Miner", MessageBoxButtons.YesNo) != DialogResult.Yes)
            { return; }
            SaveXmlDialog.InitialDirectory = OptionsManager.OptionsInstance.saveAndLoadFolder;
            if (SaveXmlDialog.ShowDialog() != DialogResult.OK)
            { return; }
            string currentFile = SaveXmlDialog.FileName;
            using (FileStream fs = new FileStream(currentFile, FileMode.Create))
            {
                SerializableDictionary<string, RegionalFleg> CurTree = MainTree;

                XmlSerializer treeSerializer = new XmlSerializer(typeof(SerializableDictionary<string, RegionalFleg>));
                treeSerializer.Serialize(fs, MainTree);
            }
        }

        private void Loadbutt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Load tree from file? It will be merged with the current tree", "Flag Miner", MessageBoxButtons.YesNo) != DialogResult.Yes)
            { return; }
            try
            {
                OpenXmlDialog.InitialDirectory = OptionsManager.OptionsInstance.saveAndLoadFolder;
                if (OpenXmlDialog.ShowDialog() != DialogResult.OK)
                { return; }

                foreach (string fileName in OpenXmlDialog.FileNames)
                {
                    string currentFile = fileName;
                    SerializableDictionary<string, RegionalFleg> temptree = null;
                    try
                    {
                        using (FileStream fs = new FileStream(currentFile, FileMode.Open)) { 
                            XmlSerializer treeSerializer = new XmlSerializer(typeof(SerializableDictionary<string, RegionalFleg>));
                            temptree = (SerializableDictionary<string, RegionalFleg>)treeSerializer.Deserialize(fs);
                            FlegOperations.MergeFlegs(temptree.Values.ToList(), ref MainTree);
                        }
                    }
                    catch (Exception ex)
                    {
                        AppendText(DateTime.Now + " : " + "Error processing file " + fileName + " " + ex.ToString() + Environment.NewLine);
                    }
                }

                Task.Run(new Action(CacheFlegs));

                UpdateRoots();
                FlegTreeListView.Invalidate();
            }
            catch (Exception ex)
            {
                AppendText(DateTime.Now + " : " + ex.ToString() + Environment.NewLine);
                MessageBox.Show("Error during the process", "Flag Miner", MessageBoxButtons.OK);
            }

        }

        void CacheFlegs()
        {
            List<string> lstr = new List<string>();
            CacheEm(MainTree, ref lstr);

            lstr = lstr.Distinct().ToList();
            ConcurrentDictionary<string, Image> queue = new ConcurrentDictionary<string, Image>();
            ParallelOptions pOpt = new ParallelOptions { MaxDegreeOfParallelism = 10 };
            Parallel.ForEach(lstr, pOpt, str =>
            {
                Image image = ImageListHelper.ScrapeImage(str);
                queue.TryAdd(str, image);
            });
            this.FlegTreeListView.SuspendLayout();
            foreach (KeyValuePair<string, Image> ke in queue)
            {
                try
                {
                    if (!helper.HasImage(ke.Key))
                    { helper.AddImageToCollection(ke.Key, this.FlegTreeListView.SmallImageList, ke.Value); }
                }
                catch (ArgumentNullException)
                {
                    // ignored
                }
            }
            this.FlegTreeListView.ResumeLayout();
            this.RefreshTree();
        }

        public void CacheEm(SerializableDictionary<string, RegionalFleg> flegs, ref List<string> lstr)
        {
            foreach (RegionalFleg fleg in flegs.Values)
            {
                lstr.Add(fleg.imgurl);
                CacheEm(fleg.children, ref lstr);
            }
        }


        private void Purgebutt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Purge inexistent flags? This cannot be undone", "Flag Miner", MessageBoxButtons.YesNo) != DialogResult.Yes)
            { return; }
            var level = 0;
            if (FlegOperations.PurgeInvalid(MainTree, OptionsManager.OptionsInstance.localRepoFolder, level) == PurgeEnum.genericError)
            {
                MessageBox.Show("An error occurred while checking the flags. Make sure your connection is up and/or local folders are valid.", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MainTree.Where((KeyValuePair<string, RegionalFleg> pair) => pair.Value.markedfordeletion == true || (OptionsManager.OptionsInstance.deleteChildFree && pair.Value.children.Count == 0)).ToArray().Apply((KeyValuePair<string, RegionalFleg> pair) => MainTree.Remove(pair.Key)).Apply();

            UpdateRoots();
            FlegTreeListView.Invalidate();
        }

        private void Checkbutt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Mark existent flags?", "Flag Miner", MessageBoxButtons.YesNo) != DialogResult.Yes)
            { return; }
            try
            {
                var level = 0;
                FlegOperations.CheckExistent(MainTree, level);

                UpdateRoots();
                FlegTreeListView.Invalidate();
            }
            catch (Exception ex)
            { AppendText(DateTime.Now + " : " + ex.ToString() + Environment.NewLine); }
        }

        private void ExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FlegTreeListView.ExpandAll();
        }

        private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FlegTreeListView.CollapseAll();
        }

        private void Importbutt_Click(object sender, EventArgs e)
        {
            SetupForParsing();
            StatusText.AppendText(DateTime.Now + " : Parsing started." + Environment.NewLine);
            if (importForm == null) { importForm = new ImportForm(this); }

            DialogResult res = importForm.ShowDialog();
            if (res == DialogResult.OK && importForm.links.Count > 0)
            { ThreadParserBackgroundWorker.RunWorkerAsync(importForm.links); }
            else
            {
                StatusText.AppendText(DateTime.Now + " : Action cancelled by user or no valid thread/posts given." + Environment.NewLine);
                SetupForIdle();
            }
        }

        private void ThreadParserBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int statusFlag = 0;
            //Dim boardList As List(Of String) = e.Argument
            BackgroundWorker worker = (BackgroundWorker)sender;

            int errorCode = 0;
            List<Tuple<string, string, string>> threads = (List<Tuple<string, string, string>>)e.Argument;

            threads.Sort();

            for (int i = 0; i <= threads.Count - 1; i += 1)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                string board = threads[i].Item1;

                worker.ReportProgress(i + 1, new object[] {
                    "N/A",
                    threads.Count
                });
                Thread.Sleep(450);
                // do not flood the server and get banned
                try
                {
                    errorCode = LoadThread(board, Int64.Parse(threads[i].Item2), out string rawResponse);
                    RaiseError(errorCode, ref statusFlag);

                    List<Post> posts = null;
                    ParseThread(rawResponse, ref posts);

                    List<Fleg> flegs = new List<Fleg>();
                    QueryExtraFlags(board, ref posts, ref flegs);

                    List<RegionalFleg> parsedFlegs = null;
                    ParseFlags(board, posts, ref flegs, ref parsedFlegs);

                    SerializableDictionary<string, RegionalFleg> flagTree = new SerializableDictionary<string, RegionalFleg>();
                    FlegOperations.MergeFlegs(parsedFlegs, ref flagTree);

                    //TreeListView1.Roots = flagTree   ' TODO inviare a concurrent stack e inizializzare rootmanager
                    rootManager.AddToStack(flagTree);

                    // for inner loop catch it here bls
                }
                catch (WebException ex)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        // anything to do?
                    }
                    else
                    {
                        // halt everything.. internet down?
                        AppendText(DateTime.Now + " : " + board + "/" + threads[i].Item2 + " " + ex.ToString() + Environment.NewLine);
                    }
                }
                catch (Exception ex)
                {
                    AppendText(DateTime.Now + " : " + board + "/" + threads[i].Item2 + " " + ex.ToString() + Environment.NewLine);
                }
            }
        }

        private void ExclusionDatePicker_ValueChanged(object sender, EventArgs e) => OptionsManager.ExclusionDate = ExclusionDatePicker.Value;

        private void ExclusionDateCheckBox_CheckedChanged(object sender, EventArgs e) => OptionsManager.ExclusionByDate = ExclusionDateCheckBox.Checked;

        private void ExclusionListCheckBox_CheckedChanged(object sender, EventArgs e) => OptionsManager.ExclusionByList = ExclusionListCheckBox.Checked;

        private void AboutButt_Click(object sender, EventArgs e)
        {
            if (minerAboutBox == null) { minerAboutBox = new MinerAboutBox(); }
            minerAboutBox.ShowDialog();
            SetupForIdle();
        }

        private void Olv_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            try
            {
                ContextMenuStrip m = new ContextMenuStrip();
                m.Tag = (RegionalFleg)e.Model;
                m.Items.Add("Expand All", null, ExpandHandler);
                m.Items.Add("Collapse All", null, CollapseHandler);
                m.Items.Add(new ToolStripSeparator());
                m.Items.Add("Copy flag to clipboard", null, CopyImageHandler);
                m.Items.Add("Save as...", null, SaveImageHandler);
                m.Items.Add(new ToolStripSeparator());
                m.Items.Add("Auto save selected...", null, AutoSaveImagesHandler);
                m.Items.Add(new ToolStripSeparator());
                m.Items.Add("Copy link", null, CopyLinkHandler);
                e.MenuStrip = m;
            }
            catch (Exception)
            {
                // ingore it for now
            }
        }

        private void ExpandHandler(object sender, EventArgs e) => FlegTreeListView.ExpandAll();

        private void CollapseHandler(object sender, EventArgs e) => FlegTreeListView.CollapseAll();

        private void CopyImageHandler(object sender, EventArgs e)
        {
            RegionalFleg regFlag = (RegionalFleg)FlegTreeListView.SelectedItem.RowObject;
            try
            {
                Image image = ImageListHelper.ScrapeImage(regFlag.imgurl);
                Clipboard.SetImage(image);
            }
            catch (Exception)
            {
                MessageBox.Show("Error while downloading the flag to copy", "Flag Miner");
            }
        }

        private void SaveImageHandler(object sender, EventArgs e)
        {
            RegionalFleg regFlag = (RegionalFleg)FlegTreeListView.SelectedItem.RowObject;
            try
            {
                Image image = ImageListHelper.ScrapeImage(regFlag.imgurl);
                string pth, fileName, flagPath;
                createSaveInformation(regFlag, out pth, out fileName, out flagPath);
                SaveFlagDialog.InitialDirectory = pth;

                SaveFlagDialog.FileName = fileName;
                if (SaveFlagDialog.ShowDialog() == DialogResult.OK)
                { image.Save(SaveFlagDialog.FileName); }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while downloading the flag to save", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createSaveInformation(RegionalFleg regFlag, out string path, out string fileName, out string flagPath)
        {

            string initString = "";
            flagPath = "";
            if (regFlag.imgurl.Contains(flegsBaseUrl))
            {
                flagPath = regFlag.imgurl.Replace(flegsBaseUrl, "");
                initString = OptionsManager.OptionsInstance.localSaveFolder + "\\" + flagPath;
            }
            // for regionals
            if (regFlag.imgurl.Contains(imageBaseUrl))
            {
                flagPath = regFlag.imgurl.Replace(imageBaseUrl, "");
                initString = OptionsManager.OptionsInstance.localSaveFolder + "\\" + regFlag.imgurl.Replace(imageBaseUrl, "");
            }
            // for nationals
            path = Path.GetDirectoryName(initString);
            fileName = Path.GetFileName(initString);
        }

        private void AutoSaveImagesHandler(object sender, EventArgs e)
        {
            foreach (object si in FlegTreeListView.SelectedObjects)
            {
                RegionalFleg regFlag = (RegionalFleg)si;
                string flagPath = "";
                try
                {
                    string pth, fileName;
                    createSaveInformation(regFlag, out pth, out fileName, out flagPath);
                    using (Image image = ImageListHelper.ScrapeImage(regFlag.imgurl))
                    {
                        if (!Directory.Exists(pth))
                        {
                            Directory.CreateDirectory(pth);
                        }
                        image.Save(Path.Combine(pth, fileName));
                    }
                }
                catch (Exception ex)
                {
                    AppendText(DateTime.Now + " : Error while downloading the flag (" + flagPath + ") to save. " + ex.Message.ToString() + Environment.NewLine);
                }
            }
        }

        private void CopyLinkHandler(object sender, EventArgs e)
        {
            RegionalFleg regFlag = (RegionalFleg)FlegTreeListView.SelectedItem.RowObject;
            Clipboard.SetText(regFlag.thread);
        }

        OptionsForm optionsForm = null;
        private void OptButt_Click(object sender, EventArgs e)
        {
            if (optionsForm == null) { optionsForm = new OptionsForm(this); }
            optionsForm.ShowDialog();
            SetupForIdle();
        }

        private void IntCheck_CheckedChanged(object sender, EventArgs e) => OptionsManager.IntCheck = intCheck.Checked;

        private void PolCheck_CheckedChanged(object sender, EventArgs e) => OptionsManager.PolCheck = polCheck.Checked;

        private void SpCheck_CheckedChanged(object sender, EventArgs e) => OptionsManager.SpCheck = spCheck.Checked;

        private void ValidateOptions()
        {
            if (!(intCheck.Checked || polCheck.Checked || spCheck.Checked))
            {
                throw new Exception("No board selected for parsing. Select at least one among (/int/, /pol/, and /sp/ with the checkboxes.");
            }
            if (OptionsManager.OptionsInstance.backendServers == null)
            {
                throw new Exception("No backend server defined. A backend server is necessary for extraflags queries.");
            }
            if (string.IsNullOrEmpty(OptionsManager.OptionsInstance.userAgent))
            {
                throw new Exception("User Agent not defined. Make sure to set a valid one in the OptionsManager.GetOptions.");
                //OptionsManager.GetOptions.userAgent = DefaultUserAgent;
            }
            if (string.IsNullOrEmpty(OptionsManager.OptionsInstance.repoUrl))
            {
                throw new Exception("Repository Url not defined. Make sure to set a valid one in the OptionsManager.GetOptions.");
            }
            if (OptionsManager.OptionsInstance.exclusionDate > OptionsManager.MaxDate || OptionsManager.OptionsInstance.exclusionDate < OptionsManager.MinDate)
            {
                throw new Exception("Correct time and date are not set.");
            }
        }

        public FlagMiner()
        {
            InitializeComponent();
        }

        void FlegTreeListView_SelectionChanged(object sender, EventArgs e)
        {
            bool enable = ((TreeListView)sender).SelectedObjects.Count != 0;
            copyFlagToolStripButton.Enabled = enable;
            saveFlagToolStripButton.Enabled = enable;
            copyLinkToolStripButton.Enabled = enable;
        }


        private void SubtractButt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Import the flags to subtract from the current tree? The action cannot be undone.", "Flag Miner", MessageBoxButtons.YesNo) != DialogResult.Yes)
            { return; }
            try
            {
                OpenXmlDialog.InitialDirectory = OptionsManager.OptionsInstance.saveAndLoadFolder;
                if (OpenXmlDialog.ShowDialog() != DialogResult.OK)
                { return; }
                foreach (string fileName in OpenXmlDialog.FileNames)
                {
                    String currentFile = fileName;
                    SerializableDictionary<String, RegionalFleg> temptree;
                    try
                    {
                        using (FileStream fs = new FileStream(currentFile, FileMode.Open))
                        {
                            XmlSerializer treeSerializer =
                                new XmlSerializer(typeof(SerializableDictionary<String, RegionalFleg>));
                            temptree = (SerializableDictionary<String, RegionalFleg>)treeSerializer.Deserialize(fs);
                        }
                        FlegOperations.SubtractFlegs(temptree, ref MainTree);
                    }
                    catch (Exception ex)
                    {
                        AppendText(DateTime.Now + " : " + "Error processing file " + fileName + " " + ex.ToString() + Environment.NewLine);
                    }
                }

                Task.Run(new Action(CacheFlegs));
                UpdateRoots();
                FlegTreeListView.Invalidate();
            }
            catch (Exception ex)
            {
                AppendText(DateTime.Now + " : " + ex.ToString() + Environment.NewLine);
                MessageBox.Show("Error during the process", "Flag Miner", MessageBoxButtons.OK);
            }
        }


        private void DeleteCheckedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove Checked Flags?", "Flag Miner", MessageBoxButtons.YesNo) != DialogResult.Yes)
            { return; }

            try
            {
                FlegOperations.DeleteCheckedFlegs(ref MainTree);
                UpdateRoots();
                FlegTreeListView.Invalidate();
            }
            catch (Exception ex)
            {
                AppendText(DateTime.Now + " : " + ex.ToString() + Environment.NewLine);
            }
        }
    }
}
