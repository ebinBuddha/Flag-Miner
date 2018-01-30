using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Concurrent;
using System.Text;
using System.Web;
using System.Net.Http;
using System.Collections.Specialized;
using BrightIdeasSoftware;
using FlagMiner;

namespace FlagMiner
{


	public partial class Form1 : Form
	{


		public string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10136";
		public string baseUrl = "http://boards.4chan.org/";
		public string archiveBaseUrl = "http://a.4cdn.org/";
		public string imageBaseUrl = "http://s.4cdn.org/image/country/";
		public List<string> boardDict = new List<string> {
			"int/",
			"pol/",
			"sp/"
		};

		public string catalogStr = "/archive.json";

		XmlSerializer xs = new XmlSerializer(typeof(SerializableDictionary<string, long>));
		string flegsBaseUrl = "https://raw.githubusercontent.com/flaghunters/Extra-Flags-for-int-/master/flags/";
			// // not https bcs installing the certificate on wine is a nightmare
		string backendBaseUrl = "http://whatisthisimnotgoodwithcomputers.com/";


		string getUrl = "int/get_flags_api2.php";

		WebHeaderCollection headerCollection = new WebHeaderCollection();

		System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();

		string regionDivider = "||";
		FlegComparer flegComparer = new FlegComparer();

		PostComparer postComparer = new PostComparer();
		ImageListHelper helper;
		MergeManager rootManager;

		public UpdateManager updateManager;

		ConcurrentQueue<string> helperStack = new ConcurrentQueue<string>();
		public ConcurrentQueue<SerializableDictionary<string, RegionalFleg>> MainMergeStack = new ConcurrentQueue<SerializableDictionary<string, RegionalFleg>>();
		public ConcurrentQueue<object> MainUpdateStack = new ConcurrentQueue<object>();

		public SerializableDictionary<string, RegionalFleg> MainTree = new SerializableDictionary<string, RegionalFleg>();
		const string savedTreeFile = "savedTree.xml";

		const string badFlagString = "Region empty, no flag yet or you did not set.";
		//Public exclusionByList As Boolean
		//Public exclusionByDate As Boolean
		//Public exclusionDate As DateTime

		public long exclusionDateLong;

		public Image blankImg;

		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		public Options options;

		public string optionsFile = "options.xml";
		private void parseBtn_Click(object sender, EventArgs e)
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

			ValidateOptions();

			StatusText.AppendText(DateTime.Now + " : Mining started." + System.Environment.NewLine);

			BackgroundWorker1.RunWorkerAsync(boardList);

		}


		private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			int statusFlag = 0;
			List<string> boardList = (List<string>) e.Argument;
			BackgroundWorker worker = (BackgroundWorker) sender;

			bool markedForAbortion = false;

			foreach (string board in boardList) {
				if (markedForAbortion)
					break; // TODO: might not be correct. Was : Exit For
				try {
					int errorCode = 0;
					string response = "";
					List<string> threads = null;
					ConcurrentDictionary<string, long> excludedThreads = null;

					errorCode = loadArchive(board, ref response);
					raiseError(errorCode, ref statusFlag);

					errorCode = parseArchive(response, ref threads);
					raiseError(errorCode, ref statusFlag);

					errorCode = loadExclusionList(board, ref excludedThreads);
					raiseError(errorCode, ref statusFlag);

					errorCode = purgeExclusionList(ref excludedThreads, ref threads);
					raiseError(errorCode, ref statusFlag);

					long finalTime = (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;

					for (int i = 0; i <= threads.Count - 1; i += 1) {
						if ((worker.CancellationPending == true)) {
							e.Cancel = true;
							markedForAbortion = true;
							break; // TODO: might not be correct. Was : Exit For
						}

						worker.ReportProgress(i + 1, new object [] {
							board,
							threads.Count
						});
                        Thread.Sleep(850); // do not flood the server and get banned
						try {
							string rawResponse = null;
							errorCode = loadThread(board, threads[i], out rawResponse);
							raiseError(errorCode, ref statusFlag);

							List<Post> posts = null;
							errorCode = parseThread(rawResponse, ref posts);
							raiseError(errorCode, ref statusFlag);

							Post firstPost = posts[0];
							finalTime = firstPost.archived_on;

							if ((options.exclusionByDate && finalTime > exclusionDateLong) | (!options.exclusionByDate)) {
								List<Fleg> flegs = new List<Fleg>();
								errorCode = queryExtraFlags(board, ref posts, ref flegs);
								raiseError(errorCode, ref statusFlag);

								List<RegionalFleg> parsedFlegs = null;
								errorCode = parseFlags(board, posts, ref flegs, ref parsedFlegs);

								SerializableDictionary<string, RegionalFleg> flagTree = new SerializableDictionary<string, RegionalFleg>();
								errorCode = mergeFlegs(parsedFlegs, ref flagTree);

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
								AppendText(DateTime.Now + " : " + board + "/" + threads[i] + " " + ex.ToString() + System.Environment.NewLine);
								markedForAbortion = true;
							}
						} catch (Exception ex) {
							AppendText(DateTime.Now + " : " + board + "/" + threads[i] + " " + ex.ToString() + System.Environment.NewLine);
							markedForAbortion = true;
						}

						if (markedForAbortion)
							break; // TODO: might not be correct. Was : Exit For

					}

					errorCode = saveExclusionList(board, ref excludedThreads);
					raiseError(errorCode, ref statusFlag);

					if (markedForAbortion)
						break; // TODO: might not be correct. Was : Exit For

				} catch (WebException ex) {
					AppendText(DateTime.Now + " : " + board + " " + ex.ToString() + System.Environment.NewLine);
					markedForAbortion = true;
				} catch (Exception ex) {
					AppendText(DateTime.Now + " : " + board + " " + ex.ToString() + System.Environment.NewLine);
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
			StatusText.AppendText(DateTime.Now + " : Abort signal sent." + System.Environment.NewLine);
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
		private void AppendText(string str)
		{
			AppendTextCallBack d = new AppendTextCallBack(appendTextCallBackFunction);
			this.Invoke(d, str);
		}

		public void appendTextCallBackFunction(string str)
		{
			StatusText.AppendText(str);
		}


		// @TODO :  proper error handling
		private void raiseError(int errorcode, ref int statusFlag)
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
			this.TreeListView1.Roots = MainTree;
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
			//If (TreeListView1.InvokeRequired) Then
			setImgSizeCallback d = new setImgSizeCallback(setImgSize);
			this.Invoke(d, new object[] { imgSize });
			//Else
			//Me.ImageList1.ImageSize = imgSize

			//End If

		}

		public void setImgSize(Size imgSize)
		{
			this.ImageList1.ImageSize = imgSize;
			this.TreeListView1.BaseSmallImageList = this.ImageList1;
		}

		//public delegate void setRootsCallback(flagTree);

		private int loadArchive(string board, ref string rawResponse)
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

		private int parseArchive(string response, ref List<string> threads)
		{
			string[] tempArray = null;

			tempArray = ser.Deserialize<string[]>(response);
			// all archived thread numbers listed
			threads = tempArray.ToList();

			return 0;

		}

		private int loadExclusionList(string board, ref ConcurrentDictionary<string, long> exclusionList)
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
				//exclusionList = New ConcurrentDictionary(Of String, Long)
				//For Each item As String In tempList
				//    exclusionList.TryAdd(item, 1)
				//Next
				fs.Close();
			} else {
				exclusionList = new ConcurrentDictionary<string, long>();
			}

			return 0;

		}

		private int saveExclusionList(string board, ref ConcurrentDictionary<string, long> exclusionList)
		{
			SerializableDictionary<string, long> tempList = new SerializableDictionary<string, long>();
			var threadDbName = board + ".db";
			foreach (KeyValuePair<string, long> ke in exclusionList) {
				tempList.Add(ke.Key, ke.Value);
			}
			FileStream fs = new FileStream(threadDbName, FileMode.Create);
			xs.Serialize(fs, tempList);
			fs.Close();
			return 0;
		}

		private int purgeExclusionList(ref ConcurrentDictionary<string, long> exclusionList, ref List<string> threadList)
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

			return 0;
		}

		public int queryExtraFlags(string board, ref List<Post> posts, ref List<Fleg> flags)
		{
			StringBuilder strB = new StringBuilder();
			StringBuilder tempstr = new StringBuilder();

			if (posts.Count > 0) {
				strB.Append("post_nrs=");
				for (int i = 0; i <= posts.Count - 1; i++) {
					string postNo = posts[i].no.ToString();
					if (i > 0) {
						tempstr.Append("," + postNo);
					} else {
						tempstr.Append(postNo);
					}
				}
				strB.Append(HttpUtility.UrlEncode(tempstr.ToString()));
				strB.Append("&" + "board=" + HttpUtility.UrlEncode(board));

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
                        //var responses = client.UploadValues(backendBaseUrl + getUrl, values);

                        var response = Encoding.Default.GetString(responses);

                        flags.AddRange(ser.Deserialize<Fleg[]>(response));

                        //return 0;
                    }
                }

                //flags.Distinct();

				return 0;
				
			} else {
                if (flags.Count > 0)
                    flags.Clear();
				return 0;
			}
		}

		public int loadThread(string board, string thread, out string rawResponse, string fullpath = null)
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

		public int parseThread(string response, ref List<Post> posts)
		{
			ChanThread tempArray = null;
			tempArray = ser.Deserialize<ChanThread>(response);
			// all archived thread numbers listed

			posts = tempArray.posts;

			return 0;
		}

        private void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (BackgroundWorker1.IsBusy | BackgroundWorker2.IsBusy) {
				MessageBox.Show("Task is ongoing. Please hit \"Abort\" before closing", "Flag Miner", MessageBoxButtons.OK);
				e.Cancel = true;
				return;
			}
			try {
				SaveOptions();
			} catch (Exception ex) {
                MessageBox.Show(e.ToString(), "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			} catch (Exception ex) {
			} finally {
				if (fs != null)
					fs.Close();
			}

			if (options.exclusionDate > DateTimePicker1.MaxDate | options.exclusionDate < DateTimePicker1.MinDate) {
				options.exclusionDate = DateTime.Now;
			}
			DateTimePicker1.Value = options.exclusionDate;
			intCheck.Checked = options.intCheck;
			polCheck.Checked = options.polCheck;
			spCheck.Checked = options.spCheck;
			CheckBox1.Checked = options.exclusionByDate;
			CheckBox2.Checked = options.exclusionByList;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			LoadOptions();

            ser.MaxJsonLength = 10 * 1024 * 1024;

            // otherwise drunkensailor complains 
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

			headerCollection.Add("Content-Type", "application/x-www-form-urlencoded");

			Bitmap tempbmp = new Bitmap(1, 1);
			blankImg = Image.FromHbitmap(tempbmp.GetHbitmap());
			//Dim g As Graphics = Graphics.FromImage(tempbmp)
			//g.DrawImage(blankImg, 1, 1, 1, 1)

			helper = new ImageListHelper(this.TreeListView1, helperStack);
			rootManager = new MergeManager(MainMergeStack, ref MainTree, TreeListView1);
			updateManager = new UpdateManager(MainUpdateStack, TreeListView1);

			TreeListView1.CanExpandGetter = ExpandGetter;
            TreeListView1.ChildrenGetter = delegate(object x) { return (IEnumerable)(ChildrenGetter(x)); };

			TreeListView1.HoverSelection = false;
			TreeListView1.HotTracking = false;

			this.ThreadColumn.AspectGetter = ThreadAspect;
			this.TitleColumn.AspectGetter = TitleAspect;
			//Me.FlagsColumn.AspectGetter = AddressOf ImageAspect
			this.FlagsColumn.ImageGetter = ImageGetter;

			SetupForIdle();

		}

		public void FormatRow_EventHandler(object sender, FormatRowEventArgs e)
		{
			KeyValuePair<string, RegionalFleg> fleg = (KeyValuePair<string, RegionalFleg>)e.Model;
			if ((fleg.Value.isTrollFlag))
				e.Item.BackColor = Color.LightPink;
			if ((fleg.Value.exists))
				e.Item.BackColor = Color.LightGreen;
		}

		public object ThreadAspect(object x)
		{
			KeyValuePair<string, RegionalFleg> fleg = (KeyValuePair<string, RegionalFleg>)x;
			return fleg.Value.thread;
		}

		public object TitleAspect(object x)
		{
			KeyValuePair<string, RegionalFleg> fleg = (KeyValuePair<string, RegionalFleg>)x;
			return fleg.Value.title;
		}

		public object ImageAspect(object x)
		{
			KeyValuePair<string, RegionalFleg> fleg = (KeyValuePair<string, RegionalFleg>)x;
			return fleg.Value.imgurl;
		}

		public object ImageGetter(object x)
		{
			KeyValuePair<string, RegionalFleg> fleg = (KeyValuePair<string, RegionalFleg>)x;

			//Return helper.GetImageIndex(fleg.Value.imgurl)
			if ((helper.HasImage(fleg.Value.imgurl))) {
				return helper.GetImageIndex(fleg.Value.imgurl);
			}

			if (!fleg.Value.fetching) {
				fleg.Value.fetching = true;
				Task.Factory.StartNew(() =>
				{
					Form1 frm = (Form1)TreeListView1.Parent.Parent.Parent;
					try {
						helper.AddToStack(fleg.Value.imgurl);
						//helper.GetImageIndex(fleg.Value.imgurl)
						//frm.updateManager.AddToStack(fleg)
						//Me.TreeListView1.RefreshObject(fleg)
					} catch (Exception ex) {
					} finally {
						//fleg.Value.fetching = False
					}
				});
				//helper.GetImageIndex(fleg.Value.imgurl)
				//Me.TreeListView1.RefreshObject(fleg)
			}
			return blankImg;
		}

		public bool ExpandGetter(object x)
		{
			KeyValuePair<string, RegionalFleg> fleg = (KeyValuePair<string, RegionalFleg>)x;
			return (fleg.Value.children.Count > 0);
		}

		public object ChildrenGetter(object x)
		{
			KeyValuePair<string, RegionalFleg> fleg = (KeyValuePair<string, RegionalFleg>)x;
			return fleg.Value.children;
		}

		public int parseFlags(string board, List<Post> posts, ref List<Fleg> extraflags, ref List<RegionalFleg> parsedFlegs)
		{
			List<Post> tempPosts = new List<Post>();

			List<string> listOfNo = new List<string>();
			foreach (Fleg flag in extraflags) {
                if (listOfNo.Contains(flag.post_nr.ToString())) continue;   
				listOfNo.Add(flag.post_nr.ToString());
			}


			// some of these variables may come not sorted... sort everything by post # !!!
			listOfNo.Sort();
            //listOfNo = (List<string>)listOfNo.Distinct();
			tempPosts = posts.Where((Post e) => listOfNo.Contains(e.no.ToString())).ToList();
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
				if (post.troll_country != null && post.troll_country != string.Empty) {
					mf = post.country_name;
					trollflag = true;
					imgUrl = imageBaseUrl + "troll/" + post.troll_country.ToLower() + ".gif";
				} else {
					mf = post.country_name;
					trollflag = false;
					imgUrl = imageBaseUrl + post.country.ToLower() + ".gif";
				}

				var postUrl = baseUrl + board + "/thread/" + post.resto + "#p" + post.no;

				RegionalFleg regFlag = new RegionalFleg();
				regFlag.isTrollFlag = trollflag;
				regFlag.title = mf;
				regFlag.board = board;
				regFlag.pNo = post.no.ToString();
				regFlag.imgurl = imgUrl;
				regFlag.thread = postUrl;
				regFlag.time = post.time;

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

			return 0;

		}

		public static int mergeFlegs(List<RegionalFleg> collectedFlegs, ref SerializableDictionary<string, RegionalFleg> flegTree)
		{
			//flegTree = New SerializableDictionary(Of String, RegionalFleg)

			foreach (RegionalFleg fleg in collectedFlegs) {
				SerializableDictionary<string, RegionalFleg> curDict = flegTree;
				RegionalFleg curFleg = fleg;
				RegionalFleg prevFleg = null;

				if (!curDict.ContainsKey(curFleg.title)) {
					curDict.Add(curFleg.title, curFleg);
					// does it copy it all?  TODO CREATE DEEP COPY   48861
				} else {
					RegionalFleg presentFleg = curDict[curFleg.title];
					if (presentFleg.time < curFleg.time) {
						presentFleg.time = curFleg.time;
						presentFleg.pNo = curFleg.pNo;
						presentFleg.thread = curFleg.thread;
						presentFleg.board = curFleg.board;
					}
					if (curFleg.children.Count > 0) {
						SerializableDictionary<string, RegionalFleg> curSrcDict = curFleg.children;
						SerializableDictionary<string, RegionalFleg> curDestDict = curDict[curFleg.title].children;
						merger(ref curSrcDict, ref curDestDict);
					}
				}
			}

			return 0;
		}


		public static void merger(ref SerializableDictionary<string, RegionalFleg> source, ref SerializableDictionary<string, RegionalFleg> dest)
		{
			foreach (KeyValuePair<string, RegionalFleg> el in source) {
				if (!dest.ContainsKey(el.Key)) {
					dest.Add(el.Key, el.Value);
				} else {
					var curdestfleg = dest[el.Key];
					var cursourcefleg = el.Value;
					if (curdestfleg.time < cursourcefleg.time) {
						curdestfleg.time = cursourcefleg.time;
						curdestfleg.pNo = cursourcefleg.pNo;
						curdestfleg.thread = cursourcefleg.thread;
						curdestfleg.board = cursourcefleg.board;
					}
					if (cursourcefleg.children.Count > 0) {
						merger(ref cursourcefleg.children, ref curdestfleg.children);
					}
				}
			}
		}


		private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
            object[] arr = (object[])e.UserState;
			Label2.Text = string.Format("Current board: {0}. Parsing thread {1} of {2}", arr[0], e.ProgressPercentage, arr[1]);
			ProgressBar1.Maximum = (int)arr[1];
			ProgressBar1.Value = e.ProgressPercentage;
		}

		private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled == true) {
				SetupForIdle();
				StatusText.AppendText(DateTime.Now + " : Aborted." + System.Environment.NewLine);
			} else if (e.Error != null) {
				StatusText.AppendText(DateTime.Now + " : An error occurred." + System.Environment.NewLine);
				SetupForIdle();
			} else {
				StatusText.AppendText(DateTime.Now + " : Parsing completed." + System.Environment.NewLine);
				SetupForIdle();
			}
            var res = WindowsApi.FlashWindow(Process.GetCurrentProcess().MainWindowHandle, true, true, 5);
		}

		public void returnPasta(SerializableDictionary<string, RegionalFleg> dict, string str, ref StringBuilder pasta)
		{

			foreach (KeyValuePair<string, RegionalFleg> ch in dict) {
				RegionalFleg curFleg = ch.Value;
				SerializableDictionary<string, RegionalFleg> curDict = curFleg.children;
				string curString = curFleg.title + ", " + str;
				if (curDict.Count == 0) {
					pasta.AppendLine(">>>/" + curFleg.board + "/" + curFleg.pNo + " " + curString);
					continue;
				} else {
					returnPasta(curDict, curString, ref pasta);
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
					continue;
				} else {
					returnPasta(curDict, curString, ref pasta);
				}
			}

            if (pasta.Length>0) Clipboard.SetText(pasta.ToString());
		}

		private void clearbutt_Click(object sender, EventArgs e)
		{
            if (MessageBox.Show("Clear flags, sure?", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
				MainTree.Clear();
				helper.Clear();
				UpdateRoots();
			}
		}

		private void savebutt_Click(object sender, EventArgs e)
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

		private void loadbutt_Click(object sender, EventArgs e)
		{
            if (MessageBox.Show("Load tree from file? It will be merged with the current tree", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
				OpenXmlDialog.InitialDirectory = options.saveAndLoadFolder;
				if (OpenXmlDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					string currentFile = OpenXmlDialog.FileName;
					SerializableDictionary<string, RegionalFleg> temptree = null;
					FileStream fs = null;
					try {
						fs = new FileStream(currentFile, FileMode.Open);
						XmlSerializer treeSerializer = new XmlSerializer(typeof(SerializableDictionary<string, RegionalFleg>));
                        temptree = (SerializableDictionary<string, RegionalFleg>)treeSerializer.Deserialize(fs);
						mergeFlegs(temptree.Values.ToList(), ref MainTree);
					} catch (Exception ex) {
					} finally {
						if (fs != null)
							fs.Close();
					}

					Task.Run(new Action(CacheFlegs));

					UpdateRoots();
					TreeListView1.Invalidate();
				}
			}

		}

		void CacheFlegs()
		{
			List<string> lstr = new List<string>();
			foreach (KeyValuePair<string, RegionalFleg> ke in MainTree) {
				lstr.Add(ke.Value.imgurl);
				foreach (KeyValuePair<string, RegionalFleg> ke2 in ke.Value.children) {
					cacheEm(ke2.Value, ref lstr);
				}
			}
			lstr.Distinct();
			ConcurrentDictionary<string, Image> queue = new ConcurrentDictionary<string, Image>();
			ParallelOptions pOpt = new ParallelOptions();
			pOpt.MaxDegreeOfParallelism = 10;
			Parallel.ForEach<string>(lstr, pOpt, str =>
			{
				Image img = ImageListHelper.ScrapeImage(str);
				queue.TryAdd(str, img);
			});
			this.TreeListView1.SuspendLayout();
			foreach (KeyValuePair<string, Image> ke in queue) {
				try {
					if (!helper.HasImage(ke.Key))
						helper.AddImageToCollection(ke.Key, this.TreeListView1.SmallImageList, ke.Value);
				} catch (ArgumentNullException generatedExceptionName) {
				}
			}
			this.TreeListView1.ResumeLayout();
			this.RefreshTree();
			//return null;
		}

		public object cacheEm(RegionalFleg fleg, ref List<string> lstr)
		{
			lstr.Add(fleg.imgurl);
			foreach (KeyValuePair<string, RegionalFleg> ke2 in fleg.children) {
				cacheEm(ke2.Value, ref lstr);
			}
			return null;
		}

		public PurgeEnum queryFlag(string imgurl)
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
			} catch (Exception ex) {
				return PurgeEnum.genericError;
				// network error
			}
			return PurgeEnum.undefined;
			// undecided bcs of godknowswhy
		}

		private PurgeEnum purgeInvalid(RegionalFleg fleg, SerializableDictionary<string, RegionalFleg> parentDict, string path, int level)
		{
			var basestr = path + "\\" + fleg.title;
			var baseFile0 = basestr + ".gif";
			var baseFile1 = basestr + ".png";

			PurgeEnum checkedFlag = default(PurgeEnum);
			if (options.useLocal & level > 0) {
				string initString = "";
				if (fleg.imgurl.Contains(flegsBaseUrl))
					initString = options.localRepoFolder + "\\" + fleg.imgurl.Replace(flegsBaseUrl, "");
				// for regionals
				if (fleg.imgurl.Contains(imageBaseUrl))
					initString = options.localRepoFolder + "\\" + fleg.imgurl.Replace(imageBaseUrl, "");
				// for nationals
				//Dim pth As String = System.IO.Path.GetDirectoryName(initString)
				//Dim fileName As String = System.IO.Path.GetFileName(initString)
				if (File.Exists(initString)) {
					checkedFlag = PurgeEnum.ok;
				} else {
					checkedFlag = PurgeEnum.notFound;
				}
			} else {
				checkedFlag = queryFlag(fleg.imgurl);
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
					if (purgeInvalid(ke.Value, fleg.children, basestr, level) == PurgeEnum.genericError) {
						return PurgeEnum.genericError;
					}
				}
				SerializableDictionary<string, RegionalFleg> mirror = fleg.children;
				fleg.children.Where((KeyValuePair<string, RegionalFleg> pair) => pair.Value.markedfordeletion == true).ToArray().Apply((KeyValuePair<string, RegionalFleg> pair) => mirror.Remove(pair.Key)).Apply();
			}
            return PurgeEnum.ok;
		}

		private void purgebutt_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Purge inexistent flags? This cannot be undone", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
				var level = 0;
				foreach (KeyValuePair<string, RegionalFleg> ke in MainTree) {
					if (purgeInvalid(ke.Value, MainTree, options.localRepoFolder, level) == PurgeEnum.genericError) {
                        MessageBox.Show("An error occurred while checking the flags. Make sure your connection is up and/or local folders are valid.", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				MainTree.Where((KeyValuePair<string, RegionalFleg> pair) => pair.Value.markedfordeletion == true | (options.deleteChildFree && pair.Value.children.Count == 0)).ToArray().Apply((KeyValuePair<string, RegionalFleg> pair) => MainTree.Remove(pair.Key)).Apply();

				UpdateRoots();
				TreeListView1.Invalidate();
			}
		}

		private void checkExistent( RegionalFleg fleg,  SerializableDictionary<string, RegionalFleg> parentDict, int level)
		{
			//Dim basestr = path & "\" & fleg.title
			//Dim baseFile0 = basestr & ".gif"
			//Dim baseFile1 = basestr & ".png"
			string initString = "";
			if (fleg.imgurl.Contains(flegsBaseUrl))
				initString = options.localSaveFolder + "\\" + fleg.imgurl.Replace(flegsBaseUrl, "");
			// for regionals
			if (fleg.imgurl.Contains(imageBaseUrl))
				initString = options.localSaveFolder + "\\" + fleg.imgurl.Replace(imageBaseUrl, "");
			// for nationals
			//Dim pth As String = System.IO.Path.GetDirectoryName(initString)
			//Dim fileName As String = System.IO.Path.GetFileName(initString)
			//If File.Exists(initString) Then
			//If ((level = 0 AndAlso Not File.Exists(baseFile0)) OrElse (level > 0 AndAlso Not File.Exists(baseFile1))) Then
			fleg.exists = File.Exists(initString);
			//Else
			//fleg.exists = True
			//End If
			level += 1;
			foreach (KeyValuePair<string, RegionalFleg> ke in fleg.children) {
				checkExistent(ke.Value, fleg.children, level);
			}
			//Dim mirror As SerializableDictionary(Of String, RegionalFleg) = fleg.children
		}

		private void checkbutt_Click(object sender, EventArgs e)
		{
            if (MessageBox.Show("Mark existent flags?", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
				var level = 0;
				foreach (KeyValuePair<string, RegionalFleg> ke in MainTree) {
					checkExistent(ke.Value, MainTree, level);
				}
				UpdateRoots();
				TreeListView1.Invalidate();
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
		private void importbutt_Click(object sender, EventArgs e)
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

			bool markedForAbortion = false;
			//Dim i As Integer = 0

			int errorCode = 0;
			string response = "";
            List<Tuple<string, string, string>> threads = (List<Tuple<string, string, string>>)e.Argument;

			threads.Sort();

			for (int i = 0; i <= threads.Count - 1; i += 1) {
				if ((worker.CancellationPending == true)) {
					e.Cancel = true;
					markedForAbortion = true;
					break; // TODO: might not be correct. Was : Exit For
				}

				string board = threads[i].Item1;

				worker.ReportProgress(i + 1, new object [] {
					"N/A",
					threads.Count
				});
				Thread.Sleep(950);
				// do not flood the server and get banned
				try {
					string rawResponse = null;
					errorCode = loadThread(board, threads[i].Item2, out rawResponse);
					raiseError(errorCode, ref statusFlag);

					List<Post> posts = null;
					errorCode = parseThread(rawResponse, ref posts);
					raiseError(errorCode, ref statusFlag);

					List<Fleg> flegs = new List<Fleg>();
					errorCode = queryExtraFlags(board, ref posts, ref flegs);
					raiseError(errorCode, ref statusFlag);

					List<RegionalFleg> parsedFlegs = null;
					errorCode = parseFlags(board, posts, ref flegs, ref parsedFlegs);
					raiseError(errorCode, ref statusFlag);

					SerializableDictionary<string, RegionalFleg> flagTree = new SerializableDictionary<string, RegionalFleg>();
					errorCode = mergeFlegs( parsedFlegs, ref flagTree);
					raiseError(errorCode, ref statusFlag);

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

		private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			options.exclusionDate = DateTimePicker1.Value;
		}

		private void CheckBox1_CheckedChanged(object sender, EventArgs e)
		{
			options.exclusionByDate = CheckBox1.Checked;
		}

		private void CheckBox2_CheckedChanged(object sender, EventArgs e)
		{
			options.exclusionByList = CheckBox2.Checked;
		}

        AboutBox1 aboutBox1 = null;

		private void aboutButt_Click(object sender, EventArgs e)
		{
            if (aboutBox1 == null) aboutBox1 = new AboutBox1();
            aboutBox1.ShowDialog();
			SetupForIdle();
		}

		private void olv_CellRightClick(object sender, CellRightClickEventArgs e)
		{
			try {
				ContextMenuStrip m = new ContextMenuStrip();
				//.(e.Model, e.Column)
                m.Tag = ((KeyValuePair<string,RegionalFleg>)e.Model).Value;
				m.Items.Add("Expand All", null, ExpandHandler);
				m.Items.Add("Collapse All", null, CollapseHandler);
				m.Items.Add(new ToolStripSeparator());
				m.Items.Add("Copy flag to clipboard", null, CopyImageHandler);
				m.Items.Add("Save as...", null, SaveImageHandler);
				m.Items.Add(new ToolStripSeparator());
				m.Items.Add("Copy link", null, CopyLinkHandler);
				e.MenuStrip = m;
			} catch (Exception ex) {
			}
		}

		private void ExpandHandler(object sender, EventArgs e)
		{
			TreeListView1.ExpandAll();
		}

		private void CollapseHandler(object sender, EventArgs e)
		{
			TreeListView1.CollapseAll();
		}

		private void CopyImageHandler(object sender, EventArgs e)
		{
			//RegionalFleg regFlag = (RegionalFleg)((ToolStripMenuItem)sender).Owner.Tag;
            RegionalFleg regFlag = ((KeyValuePair<string,RegionalFleg>)TreeListView1.SelectedItem.RowObject).Value;
			try {
				Image image = ImageListHelper.ScrapeImage(regFlag.imgurl);
				Clipboard.SetImage(image);
			} catch (Exception ex) {
				MessageBox.Show("Error while downloading the flag to copy", "Flag Miner");
			}
		}

		private void SaveImageHandler(object sender, EventArgs e)
		{
            //RegionalFleg regFlag = (RegionalFleg)((ToolStripMenuItem)sender).Owner.Tag;
            RegionalFleg regFlag = ((KeyValuePair<string, RegionalFleg>)TreeListView1.SelectedItem.RowObject).Value;
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
				//Dim fileName As String

				//If Path.Equals(destPath, pth) Then fileName = regFlag.title & ".gif" ' it's a level 0
				//If regFlag.imgurl.Contains(flegsBaseUrl) Then fileName = regFlag.title & ".png" ' it's not a level 0

				SaveFileDialog1.FileName = fileName;
				if (SaveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					image.Save(SaveFileDialog1.FileName);
			} catch (Exception ex) {
				MessageBox.Show("Error while downloading the flag to save", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CopyLinkHandler(object sender, EventArgs e)
		{
            //RegionalFleg regFlag = (RegionalFleg)((ToolStripMenuItem)sender).Owner.Tag;
            RegionalFleg regFlag = ((KeyValuePair<string, RegionalFleg>)TreeListView1.SelectedItem.RowObject).Value;
			Clipboard.SetText(regFlag.thread);
		}

        OptionsForm optionsForm = null;
		private void optButt_Click(object sender, EventArgs e)
		{
            if (optionsForm == null) optionsForm = new OptionsForm(this);
			optionsForm.ShowDialog();
			SetupForIdle();
		}

		private void intCheck_CheckedChanged(object sender, EventArgs e)
		{
			options.intCheck = intCheck.Checked;
		}

		private void polCheck_CheckedChanged(object sender, EventArgs e)
		{
			options.polCheck = polCheck.Checked;
		}

		private void spCheck_CheckedChanged(object sender, EventArgs e)
		{
			options.spCheck = spCheck.Checked;
		}

		private void ValidateOptions()
		{
			if (string.IsNullOrEmpty(options.userAgent)) {
				options.userAgent = DefaultUserAgent;
			}
		}

		/*private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			Control parent = (Control)sender;
			if (parent == null)
				return;

			Control ctrl = parent.GetChildAtPoint(e.Location);
			if ((ctrl != null && !ctrl.Enabled)) {
				if ((ctrl.Visible && ctrl.Equals(checkbutt))) {
					if ((ToolTip1.Tag == null && ToolTip1.Active)) {
						string tipstring = ToolTip1.GetToolTip(ctrl);
						ToolTip1.Show(tipstring, ctrl, ctrl.Width / 2, ctrl.Height / 2);
						ToolTip1.Tag = ctrl;
					}
				}
				if ((ctrl.Visible && ctrl.Equals(purgebutt))) {
					if ((ToolTip2.Tag == null && ToolTip2.Active)) {
						string tipstring = ToolTip2.GetToolTip(ctrl);
						ToolTip2.Show(tipstring, ctrl, ctrl.Width / 2, ctrl.Height / 2);
						ToolTip2.Tag = ctrl;
					}
				}
			} else {
                ctrl = (Control)ToolTip1.Tag;
				if ((ctrl != null)) {
					ToolTip1.Hide(ctrl);
					ToolTip1.Tag = null;
				}
                ctrl = (Control)ToolTip2.Tag;
				if ((ctrl != null)) {
					ToolTip2.Hide(ctrl);
					ToolTip2.Tag = null;
				}
			}
		}*/
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
        public void subtractFlegs(SerializableDictionary<String, RegionalFleg> src, ref SerializableDictionary<String, RegionalFleg> dest)
        {
            SerializableDictionary<String, RegionalFleg> curDestDict = dest;
            SerializableDictionary<String, RegionalFleg> curSrcDict = src;

            foreach (KeyValuePair<String, RegionalFleg> ke in curDestDict)
            {
                RegionalFleg Fleg = ke.Value;
                if (!curSrcDict.ContainsKey(ke.Key))
                {
                    continue;  // nothing to subtract
                }
                else
                {
                    subtractFlegs(curSrcDict[ke.Key].children, ref curDestDict[ke.Key].children);
                    if (Fleg.children.Count == 0)
                    {
                        curDestDict[ke.Key].markedfordeletion = true;
                        continue;
                    }
                }
            }
            curDestDict.Where(pair => pair.Value.markedfordeletion == true).ToArray().
            Apply(pair => curDestDict.Remove(pair.Key)).Apply();
        }

        private void subtractButt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Import the flags to subtract from the current tree? The action cannot be undone.", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                OpenXmlDialog.InitialDirectory = options.saveAndLoadFolder;
                if (OpenXmlDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    String currentFile = OpenXmlDialog.FileName;
                    SerializableDictionary<String, RegionalFleg> temptree;
                    FileStream fs = null;
                    try
                    {
                        fs = new FileStream(currentFile, FileMode.Open);
                        XmlSerializer treeSerializer =
                            new XmlSerializer(typeof(SerializableDictionary<String, RegionalFleg>));
                        temptree = (SerializableDictionary<String, RegionalFleg>)treeSerializer.Deserialize(fs);
                        subtractFlegs(temptree, ref MainTree);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        if (fs != null) fs.Close();
                    }

                    Task.Run(new Action(CacheFlegs));

                    UpdateRoots();
                    TreeListView1.Invalidate();
                }
            }
        }
	}
}
