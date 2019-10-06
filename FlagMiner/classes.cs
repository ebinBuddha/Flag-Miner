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
using BrightIdeasSoftware;
using System.Net;
using System.IO;
using System.Collections.Concurrent;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Threading;
namespace FlagMiner
{

	public class ChanCatalog
	{
		public string[] ThreadArray;
	}
}
namespace FlagMiner
{

	public class ChanThread
	{
		public List<Post> posts;
	}
}
namespace FlagMiner
{


	public class Post
	{
		//' deserializer looks for proper field names to deserialize json fields
		public Int64 no;
		public Int64 resto;
		public Int64 sticky;
		public Int64 closed;
		public Int64 archived;
		public Int64 archived_on;
		public string now;
		public Int64 time;
		public string name;
		public string trip;
		public string id;
		public string capcode;
		public string country;
		public string country_name;
			// escape the keyword
		public string sub;
		public string com;
		public Int64 tim;
		public string filename;
		public string ext;
		public Int64 fsize;
		public string md5;
		public Int64 w;
		public Int64 h;
		public Int64 tn_w;
		public Int64 tn_h;
		public Int64 filedeleted;
		public Int64 spoiler;
		public Int64 custom_spoiler;
		public Int64 omitted_posts;
		public Int64 omitted_images;
		public Int64 replies;
		public Int64 images;
		public Int64 bumplimit;
		public Int64 imagelimit;
		//Public capcode_replies As Array   '' fcking deserializer
		public Int64 last_modified;
		public string tag;
		public string semantic_url;
		public Int64 since4pass;

		public string troll_country;
	}
}
namespace FlagMiner
{

	public class Fleg
	{
		public Int64 post_nr;
		public string region;
	}
}
namespace FlagMiner
{

	[Serializable()]
	public class RegionalFleg
	{
		public bool isTrollFlag;
		public string imgurl;
		public string title;
		public string thread;
		public string board;
		public string pNo;
		public long time;

		public SerializableDictionary<string, RegionalFleg> children = new SerializableDictionary<string, RegionalFleg>();
		[XmlIgnore()]
		public bool fetching = false;
		[XmlIgnore()]
		public bool markedfordeletion = false;
		[XmlIgnore()]
		public bool exists = false;
		[XmlIgnore()]
			// existing, invalid etc...
		public int status = 0;
	}
}
namespace FlagMiner
{

	public enum PurgeEnum : int
	{
		genericError = -2,
		notFound = -1,
		undefined = 0,
		ok = 1
	}
}
namespace FlagMiner
{

	public class PostComparer : IComparer<Post>
	{

		public int Compare(Post x, Post y)
		{

			if (x == null) {
				if (y == null) {
					return 0;
				} else {
					return -1;
				}
			} else {
				if (y == null) {
					return 1;
				} else {
					int retval = x.no.CompareTo(y.no);
					return retval;
				}
			}
		}
	}
}
namespace FlagMiner
{

	public class FlegComparer : IComparer<Fleg>
	{

		public int Compare(Fleg x, Fleg y)
		{

			if (x == null) {
				if (y == null) {
					return 0;
				} else {
					return -1;
				}
			} else {
				if (y == null) {
					return 1;
				} else {
					int retval = x.post_nr.CompareTo(y.post_nr);
					return retval;
				}
			}
		}
	}
}
namespace FlagMiner
{

	[Serializable()]
	public struct Options
	{
        [XmlElement]
        public List<string> backendServers;
		public bool intCheck;
		public bool polCheck;

		public bool spCheck;
		public bool exclusionByList;
		public bool exclusionByDate;

		public DateTime exclusionDate;
		public string localSaveFolder;

		public string localRepoFolder;
		public bool enableCheck;
		public bool enablePurge;
		public bool useLocal;
		public bool markTroll;

		public bool deleteChildFree;
		public string userAgent;

		public string saveAndLoadFolder;

        public string repoUrl;
	}
}
namespace FlagMiner
{


	public class ImageListHelper
	{

		private static Form1 frm;
		private readonly BlockingCollection<string> stack;

        protected ObjectListView listView;
        private ImageListHelper(Form1 form) => ImageListHelper.frm = form;

        /// <summary>
        /// Create a SysImageListHelper that will fetch images for the given tree control
        /// </summary>
        /// <param name="listView">The tree view that will use the images</param>
        public ImageListHelper(ObjectListView listView, ConcurrentQueue<string> source)
		{
            Debug.AutoFlush = true;
            if (listView.SmallImageList == null) {
				listView.SmallImageList = new ImageList();
				//listView.ImageList.ImageSize = New Size(16, 16)
			}
			this.listView = listView;
            ImageListHelper.frm = (Form1)listView.Parent.Parent.Parent; // this suck ass

			stack = new BlockingCollection<string>(source);
            Task.Run(() =>
			{
				foreach (string path in stack.GetConsumingEnumerable()) {
					if (!(this.SmallImageList.Images.ContainsKey(path))) {
						try {
							this.AddImageToCollection(path, this.SmallImageList, ScrapeImage(path));
						} catch (ArgumentNullException) {
                            // ignore it
						}
					}
					if (stack.Count == 0) {
						Thread.Sleep(500);
						// fist run ok, but now wait a little to build the queue for the next run
						frm.RefreshTree();
					}

				}
			});
		}



		protected ImageList.ImageCollection SmallImageCollection {
			get {
				if (this.listView != null) {
					return this.listView.SmallImageList.Images;
				}
                ImageList il = new ImageList();
				return il.Images;
			}
		}

		protected ImageList SmallImageList {
			get {
				if (this.listView != null) {
					return this.listView.SmallImageList;
				}
				return null;
			}
		}

		/// <summary>
		/// Return the index of the image that has the Shell Icon for the given file/directory.
		/// </summary>
		/// <param name="path">The full path to the file/directory</param>
		/// <returns>The index of the image or -1 if something goes wrong.</returns>
		public int GetImageIndex(string path)
		{
			try {
				if (this.SmallImageCollection.ContainsKey(path)) {
					//SyncLock messagesLock
					return this.SmallImageCollection.IndexOfKey(path);
					//End SyncLock
				}
			} catch (Exception) {
                // ignore it
			}

			//Try
			//    Me.AddImageToCollection(path, Me.SmallImageList, ScrapeImage(path))
			//Catch generatedExceptionName As ArgumentNullException
			//    Return -1
			//End Try

			//Return Me.SmallImageCollection.IndexOfKey(path)
            return -1;
		}


		public bool HasImage(string path)
		{
			try {
				//SyncLock messagesLock
				return this.SmallImageCollection.ContainsKey(path);
				//End SyncLock
			} catch (Exception) {
                // ignore it
			}
			return false;
		}

		public void Clear()
		{
			lock (messagesLock) {
				listView.BaseSmallImageList.Images.Clear();
				listView.SmallImageList.Images.Clear();
			}
		}

		public void AddToStack(string Path)
		{
			stack.Add(Path);
		}

		public void AddToStack(List<string> Paths)
		{
			foreach (string st in Paths) {
				stack.Add(st);
			}
			//stack.Union(Paths)
		}


		public static Image ScrapeImage(string url)
		{
			System.Drawing.Bitmap img = null;

            //Dim finalimg As Image
            if (frm.options.useLocal && url.Contains(frm.options.repoUrl))
            {
                string diskPath = url.Replace(frm.options.repoUrl, frm.options.localRepoFolder+"/");
                try
                {
                    img = (Bitmap)System.Drawing.Image.FromFile(diskPath);
                    System.Diagnostics.Debug.Listeners[0].WriteLine(diskPath);
                }
                catch (Exception)
                {
                    img = (Bitmap)frm.blankImg;
                }
                return img;
            }
            else
            {
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        byte[] bytes = wc.DownloadData(url);
                        MemoryStream ms = new MemoryStream(bytes);
                        img = (Bitmap)System.Drawing.Image.FromStream(ms);

                    }
                    catch (WebException ex)
                    {
                        var resp = (HttpWebResponse)ex.Response;
                        if (resp.StatusCode == HttpStatusCode.NotFound)
                        {
                            img = (Bitmap)frm.blankImg;
                        }
                    }
                }
            }

			return img;
		}


		private readonly object messagesLock = new object();
		public void AddImageToCollection(string key, ImageList imageList, Image img)
		{
			if (imageList == null | img == null) {
				return;
			}
			lock (messagesLock) {
				try {
					Image finalimg = null;
					if (img.Size == this.SmallImageList.ImageSize) {
						finalimg = img;

					} else {
						Size maxSize = default(Size);
						maxSize.Width = Math.Max(img.Width, this.SmallImageList.ImageSize.Width);
						maxSize.Height = Math.Max(img.Height, this.SmallImageList.ImageSize.Height);

						ImageList tempList = new ImageList();
						tempList.ImageSize = new Size(maxSize.Width, maxSize.Height);
						Graphics g = null;

						foreach (string ke in this.SmallImageList.Images.Keys) {
							Image savedImg = this.SmallImageList.Images[ke];
							Bitmap tempbmp = new Bitmap(maxSize.Width, maxSize.Height);

							g = Graphics.FromImage(tempbmp);
							//g.Clear(Color.White)
							g.DrawImage(savedImg, new Rectangle(0, 0, savedImg.Width, savedImg.Height), new Rectangle(0, 0, savedImg.Width, savedImg.Height), GraphicsUnit.Pixel);
							// sostituisci
							tempList.Images.Add(ke, tempbmp);
						}

						// copia
						this.SmallImageList.Images.Clear();
						Form1 myform = (Form1)this.listView.Parent.Parent.Parent;
						myform.SetImgSizeInvoker(new Size(maxSize.Width, maxSize.Height));
						//Me.SmallImageList.ImageSize = New Size(maxSize.Width, maxSize.Height)
						foreach (string ke in tempList.Images.Keys) {
							this.SmallImageList.Images.Add(ke, tempList.Images[ke]);
						}

						// sistema nuova
						Bitmap newBitmap = new Bitmap(maxSize.Width, maxSize.Height);
						Graphics gr = Graphics.FromImage(newBitmap);
						gr.DrawImage(img, 0, 0, img.Width, img.Height);
						finalimg = newBitmap;

					}
					imageList.Images.Add(key, finalimg);
				} catch (Exception ex) {
                    // ignore this
				}
			}

		}

	}
}
namespace FlagMiner
{


	public class MergeManager
	{
		private BlockingCollection<SerializableDictionary<string, RegionalFleg>> stack;

		private Task consumer;

		private TreeListView treeView;
		public MergeManager(ConcurrentQueue<SerializableDictionary<string, RegionalFleg>> source, ref SerializableDictionary<string, RegionalFleg> dest, TreeListView myTreeView)
		{
			stack = new BlockingCollection<SerializableDictionary<string, RegionalFleg>>(source);
			treeView = myTreeView;
			Form1 frm = (Form1)treeView.Parent.Parent.Parent;
			SerializableDictionary<string, RegionalFleg> dict = dest;
			consumer = Task.Run(() =>
			{
				foreach (SerializableDictionary<string, RegionalFleg> myObj in stack.GetConsumingEnumerable()) {
					foreach (RegionalFleg Fleg in myObj.Values) {
						SerializableDictionary<string, RegionalFleg> curDict = dict;
						RegionalFleg curFleg = Fleg;
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
								Form1.Merger(ref curSrcDict, ref curDestDict);
							}
						}
					}

					Thread.Sleep(200);
					if (stack.Count == 0) {
						frm.UpdateRootsInvoker();
						//frm.updateManager.AddToStack(Tuple.Create(Of String, Object)("ut", myTreeView))
					}

				}
			});
		}


		public void AddToStack(SerializableDictionary<string, RegionalFleg> obj)
		{
			stack.Add(obj);
		}

	}
}
namespace FlagMiner
{


	public class UpdateManager
	{
		private BlockingCollection<object> stack;

		private Task consumer;

		private TreeListView treeView;
		public UpdateManager(ConcurrentQueue<object> source, TreeListView myTreeView)
		{
			stack = new BlockingCollection<object>(source);
			treeView = myTreeView;
			Form1 frm = (Form1)treeView.Parent.Parent.Parent;
			consumer = Task.Run(() =>
			{
				List<object> accumulator = new List<object>();
				foreach (object myObj in stack.GetConsumingEnumerable()) {
					accumulator.Add(myObj);
					if (stack.Count == 0) {
						//accumulator.Distinct()
						//frm.UpdateTreeViewInvoker(accumulator)
						treeView.Invalidate();
						accumulator.Clear();
						Thread.Sleep(200);
						// fist run ok, but now wait a little to build the queue for the next run
					}

				}
			});
		}


		public void AddToStack(object obj)
		{
			stack.Add(obj);
		}

	}
}
namespace FlagMiner
{

	//https://weblogs.asp.net/pwelter34/444961
	[XmlRoot("dictionary")]

	public class SerializableDictionary<TKey, TValue> : SortedDictionary<TKey, TValue>, IXmlSerializable
	{

		#region "IXmlSerializable Members"

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}



		public void ReadXml(System.Xml.XmlReader reader)
		{
			XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
			XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

			bool wasEmpty = reader.IsEmptyElement;

			reader.Read();

			if ((wasEmpty))
				return;


			while ((reader.NodeType != System.Xml.XmlNodeType.EndElement)) {
				reader.ReadStartElement("item");
				reader.ReadStartElement("key");

				TKey key = (TKey)keySerializer.Deserialize(reader);

				reader.ReadEndElement();
				reader.ReadStartElement("value");

				TValue value = (TValue)valueSerializer.Deserialize(reader);

				reader.ReadEndElement();

				this.Add(key, value);
				reader.ReadEndElement();
				reader.MoveToContent();
			}

			reader.ReadEndElement();

		}

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
			XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

			foreach (TKey key in this.Keys) {
				writer.WriteStartElement("item");
				writer.WriteStartElement("key");

				keySerializer.Serialize(writer, key);
				writer.WriteEndElement();
				writer.WriteStartElement("value");

				TValue value = this[key];

				valueSerializer.Serialize(writer, value);

				writer.WriteEndElement();
				writer.WriteEndElement();

			}

		}

		#endregion

	}
}
namespace FlagMiner
{


	// array mangling
	public static class enumExtensions
	{
		//[Extension()]
		public static IEnumerable<T> Apply<T>(this IEnumerable<T> source, Action<T> action)
		{
			//Argument.NotNull(source, Function() Function() source)
			//Argument.NotNull(action, Function() Function() action)
			return ApplyInternal(source, action);
		}

		static internal IEnumerable<T> ApplyInternal<T>(IEnumerable<T> source, Action<T> action)
		{
			List<T> res = new List<T>();
			foreach (T e in source) {
				action(e);
				res.Add(e);
			}
			return res;
		}

		//[Extension()]
		public static void Apply<T>(this IEnumerable<T> source)
		{
			foreach (object e in source) {
				//' do nothing, just make sure the elements are enumerated.
			}
		}
	}
}
