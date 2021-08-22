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
using System.Xml;
using System.Net;

namespace FlagMiner
{

	partial class ImportForm :Form
	{
        public Form1 myForm1;
        public ImportForm(Form1 frm1)
        {
            myForm1 = frm1;
            InitializeComponent();
        }

		public List<Tuple<string, string, string>> links;
		private void Button1_Click(object sender, EventArgs e)
		{
            try
            {
                int statusFlag = 0;
                int errorCode = 0;
                string rawResponse = null;
                //Dim board, thread As String

                Uri uri = new Uri(TextBox1.Text);
                string parsedUrl = uri.GetLeftPart(UriPartial.Path);

                errorCode = myForm1.LoadThread(null, 0, out rawResponse, parsedUrl);
                Form1.RaiseError(errorCode, ref statusFlag);

                List<Post> posts = null;
                myForm1.ParseThread(rawResponse, ref posts);

                List<ulong> sourcePosts = new List<ulong>();
                errorCode = parseStrings(ref posts, ref sourcePosts);
                Form1.RaiseError(errorCode, ref statusFlag);

                List<Post> trimmedPosts = new List<Post>();
                filterPosts(ref posts, ref sourcePosts, trimmedPosts);

                links = new List<Tuple<string, string, string>>();
                gatherLinks(ref trimmedPosts, ref links);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (WebException ex)
            {
                myForm1.AppendText(DateTime.Now + " : " + ex.ToString() + System.Environment.NewLine);
            }
            catch (Exception ex)
            {
                myForm1.AppendText(DateTime.Now + " : " + ex.ToString() + System.Environment.NewLine);
            }
		}

        private void Button2_Click(object sender, EventArgs e) => this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

        public int parseStrings(ref List<Post> posts, ref List<ulong> sourcePosts)
		{
			foreach (string lin in TextBox2.Lines) {
				string temp = lin.Trim( new char[] {
					'>',
					' '
				});
				ulong res = 0;
				if (string.IsNullOrEmpty(temp)) {
					continue;
				}
				if (ulong.TryParse(temp, out res)) {
					sourcePosts.Add(res);
				} else {
                    MessageBox.Show("Wrong format for " + lin, "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return 1;
				}
			}
			sourcePosts = (List<ulong>)sourcePosts.Distinct();
			sourcePosts.Sort();

			return 0;
		}

		public void filterPosts(ref List<Post> posts, ref List<ulong> sourcePosts, List<Post> filtered)
		{
			foreach (Post Post in posts) {
				if (sourcePosts.Contains((ulong)Post.no)) {
					filtered.Add(Post);
				}
			}
		}


		public void gatherLinks(ref List<Post> filtered, ref List<Tuple<string, string, string>> links)
		{

			WebBrowser1.Navigate(string.Empty);
			HtmlDocument fakeDoc = WebBrowser1.Document;
			foreach (Post Post in filtered) {
				string text = Post.com;
				text = "<HTML><body>" + text + "</body></HTML>";

				fakeDoc.Write(text);
			}

			HtmlElementCollection HtmlElems = fakeDoc.Links;
			foreach (HtmlElement HtmlElem in HtmlElems) {
				string str = HtmlElem.GetAttribute("href");
				Uri uri = new Uri(str);
				string str2 = uri.Fragment;
				string[] parts = uri.AbsolutePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
				if (string.IsNullOrEmpty(str2)) {
					str2 = parts[2];
				} else {
					str2 = str2.Trim(new char[] {
						'#',
						'p'
					});
				}
				if (parts.Length == 3)
					links.Add(new Tuple<string, string, string>(parts[0], parts[2], str2));
			}
		}

        WatDo watDo=null;
		private void ImportForm_HelpRequested(object sender, EventArgs e)
		{
            if (watDo == null) watDo = new WatDo();
			watDo.ShowDialog();
		}
	}
}
