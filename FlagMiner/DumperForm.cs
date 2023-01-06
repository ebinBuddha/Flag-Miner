using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private DumperOptions dumperOptions;

        private bool CompleteDump { get => dumperOptions.completeDump; set { dumperOptions.completeDump = value; SetUpCompleteDump(); } }
        private bool UseHeader { get => dumperOptions.useHeader; set { dumperOptions.useHeader = value; SetUpHeader(); } }
        private bool UseSeparator { get => dumperOptions.useSeparator; set { dumperOptions.useSeparator = value; SetUpSeparator(); } }
        private bool UseFooter { get => dumperOptions.useFooter; set { dumperOptions.useFooter = value; SetUpFooter(); } }

        public FlagMiner flagMiner;
        public DumperForm(FlagMiner frm)
        {
            flagMiner = frm;
            dumperOptions = new DumperOptions();
            InitializeComponent();
        }

        public void DumperForm_Load(object sender, EventArgs e)
        {
            SetUpCompleteDump();
            SetUpHeader();
            SetUpSeparator();
            SetUpFooter();
        }

        private void SetUpCompleteDump()
        {
            var enabled = CompleteDump;
            groupBBox.Enabled = enabled;
            splitContainer2.Panel1.Enabled = enabled;
        }

        private void SetUpHeader() => headerTextBox.Enabled = UseHeader;
        private void SetUpSeparator() => separationTextBox.Enabled =UseSeparator;
        private void SetUpFooter() => footerTextBox.Enabled = UseFooter;
        private void GroupAminusBRadioButton_CheckedChanged(object sender, EventArgs e) => CompleteDump = groupAminusBRadioButton.Checked;
        private void HeaderCheckBox_CheckedChanged(object sender, EventArgs e) => UseHeader = headerCheckBox.Checked;
        private void SeparationCheckBox_CheckedChanged(object sender, EventArgs e) => UseSeparator = separationCheckBox.Checked;
        private void FooterCheckBox_CheckedChanged(object sender, EventArgs e) => UseFooter = footerCheckBox.Checked;

        private void AddABtn_Click(object sender, EventArgs e)
        {

        }

        private void removeABtn_Click(object sender, EventArgs e)
        {

        }

        private void addBBtn_Click(object sender, EventArgs e)
        {

        }

        private void removeBBtn_Click(object sender, EventArgs e)
        {

        }

        private void copyBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
