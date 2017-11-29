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
			int errorCode = 0;
			string rawResponse = null;
			//Dim board, thread As String

			Uri uri = new Uri(TextBox1.Text);
			string parsedUrl = uri.GetLeftPart(UriPartial.Path);

			errorCode = myForm1.loadThread(null, null, out rawResponse, parsedUrl);

			List<Post> posts = null;
			errorCode = myForm1.parseThread(rawResponse, ref posts);

			List<ulong> sourcePosts = new List<ulong>();
			errorCode = parseStrings(ref posts, ref sourcePosts);

			List<Post> trimmedPosts = new List<Post>();
			errorCode = filterPosts(ref posts, ref sourcePosts, trimmedPosts);

			links = new List<Tuple<string, string, string>>();
			errorCode = gatherLinks(ref trimmedPosts, ref links);

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

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
			sourcePosts.Distinct();
			sourcePosts.Sort();

			return 0;
		}

		public int filterPosts(ref List<Post> posts, ref List<ulong> sourcePosts, List<Post> filtered)
		{
			foreach (Post Post in posts) {
				if (sourcePosts.Contains((ulong)Post.no)) {
					filtered.Add(Post);
				}
			}
			return 0;
		}


		public int gatherLinks(ref List<Post> filtered, ref List<Tuple<string, string, string>> links)
		{
			List<string> temp = new List<string>();

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
			return 0;

		}

        WatDo watDo=null;
		private void ImportForm_HelpRequested(object sender, EventArgs e)
		{
            if (watDo == null) watDo = new WatDo();
			watDo.ShowDialog();
		}
	}
}
