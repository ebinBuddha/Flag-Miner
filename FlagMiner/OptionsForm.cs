using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.ComponentModel;
using System.IO;
namespace FlagMiner
{

    partial class OptionsForm : Form
	{
        private Form1 myForm1;
        public OptionsForm(Form1 frm1)
        {
            myForm1 = frm1;
            Load += OptionsForm_Load;
            FormClosing += OptionsForm_FormClosing;
            InitializeComponent();
        }
		private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if ((DialogResult == System.Windows.Forms.DialogResult.None))
				e.Cancel = true;
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
            this.enableCheck.Checked = myForm1.options.enableCheck;
            this.enableCheck_CheckedChanged(null, System.EventArgs.Empty);
            this.enablePurge.Checked = myForm1.options.enablePurge;
            this.CheckBox1_CheckedChanged(null, System.EventArgs.Empty);
            this.localRepoFolder.Text = myForm1.options.localRepoFolder;
            this.localSaveFolder.Text = myForm1.options.localSaveFolder;
            this.markTroll.Checked = myForm1.options.markTroll;
            this.RadioButton1.Checked = myForm1.options.useLocal;
            this.userAgent.Text = myForm1.options.userAgent;
            this.saveAndLoadFolder.Text = myForm1.options.saveAndLoadFolder;
            this.deleteChildFree.Checked = myForm1.options.deleteChildFree;
            this.repoUrl.Text = myForm1.options.repoUrl;
            this.backendServers.Clear();
            if (myForm1.options.backendServers != null)
            {
                foreach (string st in myForm1.options.backendServers)
                {
                    if (st != "")
                        this.backendServers.Text += st + "\n";
                }
            }
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			if (!this.ValidateChildren()) {
				this.DialogResult = DialogResult.None;
			} else {
                OptionsForm_UpdateOpts();
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
		}

		private void OptionsForm_UpdateOpts()
		{
            myForm1.options.localSaveFolder = this.localSaveFolder.Text;
            myForm1.options.localRepoFolder = this.localRepoFolder.Text;
            myForm1.options.enableCheck = this.enableCheck.Checked;
            myForm1.options.enablePurge = this.enablePurge.Checked;
            myForm1.options.useLocal = this.RadioButton1.Checked;
            myForm1.options.markTroll = this.markTroll.Checked;
            myForm1.options.userAgent = this.userAgent.Text;
            myForm1.options.saveAndLoadFolder = this.saveAndLoadFolder.Text;
            myForm1.options.deleteChildFree = this.deleteChildFree.Checked;
            myForm1.options.repoUrl = this.repoUrl.Text;
            myForm1.options.backendServers.Clear();
            foreach (string st in this.backendServers.Lines)
            {
                if (st != "")
                    myForm1.options.backendServers.Add(st);
            }
		}

		private void TextBox1_Validating(object sender, CancelEventArgs e)
		{
			if (enableCheck.Checked) {
				if ((localSaveFolder.Text.Length == 0) && !Directory.Exists(localSaveFolder.Text)) {
                    MessageBox.Show("Invalid folder", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
					return;
				}
			}
		}

		private void TextBox2_Validating(object sender, CancelEventArgs e)
		{
			if (enablePurge.Checked && RadioButton1.Checked) {
				if ((localRepoFolder.Text.Length == 0) || !Directory.Exists(localRepoFolder.Text)) {
                    MessageBox.Show("Invalid folder", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
					return;
				}
			}
		}

		private void userAgent_Validating(object sender, CancelEventArgs e)
		{
			if ((string.IsNullOrEmpty(userAgent.Text) || string.IsNullOrWhiteSpace(userAgent.Text))) {
                MessageBox.Show("Insert a valid User Agent or press Default", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}
		}

        private void repoUrl_Validating(object sender, CancelEventArgs e)
        {
            if ((string.IsNullOrEmpty(repoUrl.Text) || string.IsNullOrWhiteSpace(repoUrl.Text)))
            {
                MessageBox.Show("Insert a valid Url for the Repository or press Default", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void backendServers_Validating(object sender, CancelEventArgs e)
        {
            if (this.backendServers.Lines.All(s => string.IsNullOrWhiteSpace(s)))
            {
                MessageBox.Show("Insert a valid Url for the backend server or press Default", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
		{
			if (FolderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				localSaveFolder.Text = FolderBrowserDialog1.SelectedPath;
			}
		}

		private void CheckBox1_CheckedChanged(object sender, EventArgs e)
		{
			Panel1.Enabled = enablePurge.Checked;
		}

		private void Button4_Click(object sender, EventArgs e)
		{
			if (FolderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				localRepoFolder.Text = FolderBrowserDialog1.SelectedPath;
			}
		}

		private void defaultUserAgentButton_Click(object sender, EventArgs e)
		{
			userAgent.Text = myForm1.DefaultUserAgent;
		}

		private void Button6_Click(object sender, EventArgs e)
		{
			if (FolderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				saveAndLoadFolder.Text = FolderBrowserDialog1.SelectedPath;
			}
		}

        private void enableCheck_CheckedChanged(object sender, EventArgs e)
        {
            selectLocalDestFolderButton.Enabled = enableCheck.Checked;
        }

        private void defaultBackendButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restore default server address?", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.backendServers.Clear();
                // DEFAULT SERVERS
                //this.backendServers.Text += "https://whatisthisimnotgoodwithcomputers.com/\n";
                this.backendServers.Text += "https://countryball.ca/\n";
            }
        }

        private void defaultRepoUrlButton_Click(object sender, EventArgs e)
        {
            this.repoUrl.Text = "https://gitlab.com/flagtism/Extra-Flags-for-4chan/raw/master/flags/";
        }

        public override bool ValidateChildren()
        {
            foreach (Control control in new List<Control> { this.backendServers, this.localRepoFolder, this.repoUrl, this.userAgent, this.localSaveFolder })
            {
                control.Focus();
                if (!Validate())
                {
                    return false;
                }
            }
            return true;
        }

	}
}
