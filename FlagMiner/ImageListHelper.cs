using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using BrightIdeasSoftware;
using System.Net;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;


namespace FlagMiner
{

	// class responsible of scraping and caching the flags
    public class ImageListHelper
	{

		private static FlagMiner frm;
		private readonly BlockingCollection<string> stack;

        protected ObjectListView listView;
        private ImageListHelper(FlagMiner form) => ImageListHelper.frm = form;

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
            ImageListHelper.frm = (FlagMiner)listView.Parent.Parent.Parent; // this suck ass

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

			var options = OptionsManager.OptionsInstance;
			var useLocal = options.useLocal;
			var repoUrl = options.repoUrl;
			var localRepoFolder = options.localRepoFolder;

			if (useLocal && url.Contains(repoUrl))
            {
                string diskPath = url.Replace(repoUrl, localRepoFolder+"/");
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
			if (imageList == null || img == null) {
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
							// replace
							tempList.Images.Add(ke, tempbmp);
						}

						// copy
						this.SmallImageList.Images.Clear();
						FlagMiner myform = (FlagMiner)this.listView.Parent.Parent.Parent;
						myform.SetImgSizeInvoker(new Size(maxSize.Width, maxSize.Height));
						//Me.SmallImageList.ImageSize = New Size(maxSize.Width, maxSize.Height)
						foreach (string ke in tempList.Images.Keys) {
							this.SmallImageList.Images.Add(ke, tempList.Images[ke]);
						}

						// new one
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
