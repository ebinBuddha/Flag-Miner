using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FlagMiner
{
    public partial class DumperForm : Form
    {
        private class DumperOptions
        {
            public bool completeDump = true;
            public bool useHeader = true;
            public bool useSeparator = true;
            public bool useFooter = true;
        }

        private class PathComparer : EqualityComparer<string>
        {
            public override bool Equals(string path1, string path2)
            {
                return String.Equals(path1.ToNormalizedPath(), path2.ToNormalizedPath(),
                    StringComparison.InvariantCultureIgnoreCase);
            }

            public override int GetHashCode(string path)
            { return path.ToNormalizedPath().ToUpperInvariant().GetHashCode(); }
        }

        private class DumperLists
        {
            public HashSet<string> groupA = new HashSet<string>(new PathComparer());
            public HashSet<string> groupB = new HashSet<string>(new PathComparer());
        }

        private DumperOptions dumperOptions;
        private DumperLists dumperLists;

        private bool CompleteDump { get => dumperOptions.completeDump; set { dumperOptions.completeDump = value; SetUpCompleteDump(); } }
        private bool UseHeader { get => dumperOptions.useHeader; set { dumperOptions.useHeader = value; SetUpHeader(); } }
        private bool UseSeparator { get => dumperOptions.useSeparator; set { dumperOptions.useSeparator = value; SetUpSeparator(); } }
        private bool UseFooter { get => dumperOptions.useFooter; set { dumperOptions.useFooter = value; SetUpFooter(); } }

        private void AddToList(ref HashSet<string> list, IEnumerable<string> items)
        {
            foreach (var path in items)
            {
                string fullPath = Path.GetFullPath(new Uri(path).LocalPath)
                   .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                list.Add(fullPath);
            }

            SetUpLists();
        }

        private void RemoveFromList(ref HashSet<string> list, IEnumerable<string> items)
        {
            foreach (var path in items)
            {
                string fullPath = Path.GetFullPath(new Uri(path).LocalPath)
                   .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                list.Remove(fullPath);
            }

            SetUpLists();
        }


        public FlagMiner flagMiner;
        public DumperForm(FlagMiner frm)
        {
            flagMiner = frm;
            dumperOptions = new DumperOptions();
            dumperLists = new DumperLists();
            InitializeComponent();
        }

        public void DumperForm_Load(object sender, EventArgs e)
        {
            SetUpCompleteDump();
            SetUpHeader();
            SetUpSeparator();
            SetUpFooter();
            SetUpLists();
        }

        private void SetUpCompleteDump()
        {
            var enabled = CompleteDump;
            groupBBox.Enabled = enabled;
            splitContainer2.Panel1.Enabled = enabled;
        }

        private void SetUpHeader() => headerTextBox.Enabled = UseHeader;
        private void SetUpSeparator() => separationTextBox.Enabled = UseSeparator;
        private void SetUpFooter() => footerTextBox.Enabled = UseFooter;

        private void SetUpLists()
        {
            groupAListView.Items.Clear();
            foreach (var l in dumperLists.groupA)
            { groupAListView.Items.Add(l); }

            groupBListView.Items.Clear();
            foreach (var l in dumperLists.groupB)
            { groupBListView.Items.Add(l); }
        }

        private void GroupAminusBRadioButton_CheckedChanged(object sender, EventArgs e) => CompleteDump = groupAminusBRadioButton.Checked;
        private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e) => UseHeader = headerCheckBox.Checked;
        private void SeparationCheckBox_CheckedChanged(object sender, EventArgs e) => UseSeparator = separationCheckBox.Checked;
        private void FooterCheckBox_CheckedChanged(object sender, EventArgs e) => UseFooter = footerCheckBox.Checked;

        private void AddABtn_Click(object sender, EventArgs e)
        {
            if (openFlagFileDialog.ShowDialog() == DialogResult.Cancel)
            { return; }

            AddToList(ref dumperLists.groupA, openFlagFileDialog.FileNames);
        }

        private void removeABtn_Click(object sender, EventArgs e)
        {
            var selected = new List<string>();
            foreach (ListViewItem item in groupAListView.SelectedItems) {
                selected.Add(item.Text);
            }
            RemoveFromList(ref dumperLists.groupA, selected);
        }

        private void addBBtn_Click(object sender, EventArgs e)
        {
            if (openFlagFileDialog.ShowDialog() == DialogResult.Cancel)
            { return; }

            AddToList(ref dumperLists.groupB, openFlagFileDialog.FileNames);
        }

        private void removeBBtn_Click(object sender, EventArgs e)
        {
            var selected = new List<string>();
            foreach (ListViewItem item in groupBListView.SelectedItems)
            {
                selected.Add(item.Text);
            }
            RemoveFromList(ref dumperLists.groupB, selected);
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Pasta generation started";

            var ATree = new SerializableDictionary<string, RegionalFleg>();
            foreach (string fileName in dumperLists.groupA)
            {
                string currentFile = fileName;
                try
                {
                    using (FileStream fs = new FileStream(currentFile, FileMode.Open))
                    {
                        XmlSerializer treeSerializer = new XmlSerializer(typeof(SerializableDictionary<string, RegionalFleg>));
                        SerializableDictionary<string, RegionalFleg> temptree = (SerializableDictionary<string, RegionalFleg>)treeSerializer.Deserialize(fs);
                        FlegOperations.MergeFlegs(temptree.Values.ToList(), ref ATree);
                    }
                }
                catch (Exception ex)
                {
                    statusLabel.Text = "Error processing file " + fileName;
                    flagMiner.AppendText(DateTime.Now + " : " + "Error processing file " + fileName + " " + ex.ToString() + Environment.NewLine);
                }
            }

            var BTree = new SerializableDictionary<string, RegionalFleg>();
            if (CompleteDump)
            {
                foreach (string fileName in dumperLists.groupB)
                {
                    string currentFile = fileName;
                    try
                    {
                        using (FileStream fs = new FileStream(currentFile, FileMode.Open))
                        {
                            XmlSerializer treeSerializer = new XmlSerializer(typeof(SerializableDictionary<string, RegionalFleg>));
                            SerializableDictionary<string, RegionalFleg> temptree = (SerializableDictionary<string, RegionalFleg>)treeSerializer.Deserialize(fs);
                            FlegOperations.MergeFlegs(temptree.Values.ToList(), ref BTree);
                        }
                    }
                    catch (Exception ex)
                    {
                        statusLabel.Text = "Error processing file " + fileName;
                        flagMiner.AppendText(DateTime.Now + " : " + "Error processing file " + fileName + " " + ex.ToString() + Environment.NewLine);
                    }
                }

                var TempTree = new SerializableDictionary<string, RegionalFleg>();
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlSerializer treeSerializer = new XmlSerializer(typeof(SerializableDictionary<string, RegionalFleg>));
                    treeSerializer.Serialize(ms, ATree);
                    ms.Position = 0;
                    TempTree = (SerializableDictionary<string, RegionalFleg>)treeSerializer.Deserialize(ms);
                }

                FlegOperations.SubtractFlegs(BTree, ref ATree); // A-B
                FlegOperations.SubtractFlegs(ATree, ref TempTree); // A intersected B
                BTree = TempTree;
            }

            var pasta = new StringBuilder();

            if (UseHeader)
            { pasta.AppendFormat("{0}\n\n", headerTextBox.Text); }

            if (ATree.Count > 0)
            {
                FlegOperations.AppendPasta(ATree,"",ref pasta);
                pasta.AppendLine();
            } else
            {
                pasta.AppendFormat("{0}\n\n", "No flags in group A, or all flags in group A are also in group B");
            }

            if (CompleteDump)
            {
                if (UseSeparator)
                { pasta.AppendFormat("{0}\n\n", separationTextBox.Text); }

                if (BTree.Count > 0)
                {
                    FlegOperations.AppendPasta(BTree, "", ref pasta);
                    pasta.AppendLine();
                }
                else
                {
                    pasta.AppendFormat("{0}\n\n", "No flags are both in group A and group B");
                }
            }

            if (UseFooter)
            { pasta.AppendFormat("{0}\n\n", footerTextBox.Text); }

            if (pasta.Length > 0) {
                Clipboard.SetText(pasta.ToString());
                statusLabel.Text = "Copied to clipboard!";
            } else
            {
                statusLabel.Text = "Nothing to copy to clipboard!";
            }
        }

    }
}
