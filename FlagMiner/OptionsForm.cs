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
        private FlagMiner myForm1;
        public OptionsForm(FlagMiner frm1)
        {
            myForm1 = frm1;
            Load += OptionsForm_Load;
            FormClosing += OptionsForm_FormClosing;
            InitializeComponent();
        }
		private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == System.Windows.Forms.DialogResult.None)
				e.Cancel = true;
		}

		private void OptionsForm_Load(object sender, EventArgs e)
		{
            var options = OptionsManager.OptionsInstance;

            this.BackendServersTextBox.Clear();
            if (options.backendServers != null)
            {
                foreach (string st in options.backendServers)
                {
                    if (st != "")
                        this.BackendServersTextBox.Text += st + "\n";
                }
            }

            this.LocalFlagSaveFolderTextBox.Text = options.localSaveFolder;
            this.LocalRepoFolderTextBox.Text = options.localRepoFolder;

            this.EnableCheckCheckBox.Checked = options.enableCheck;
            this.enableCheck_CheckedChanged(null, System.EventArgs.Empty);
            this.EnablePurgeCheckBox.Checked = options.enablePurge;
            this.enablePurge_CheckedChanged(null, System.EventArgs.Empty);
            this.UseLocalRepoRadioButton.Checked = options.useLocal;
            this.MarkTrollCheckBox.Checked = options.markTroll;
            this.DeleteChildFreeCheckBox.Checked = options.deleteChildFree;

            this.UserAgentTextBox.Text = options.userAgent;

            this.TreeSaveAndLoadFolderTextBox.Text = options.saveAndLoadFolder;

            this.RepoUrlTextBox.Text = options.repoUrl;
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
            var options = OptionsManager.OptionsInstance;

            options.backendServers = new List<string>();
            foreach (string st in this.BackendServersTextBox.Lines)
            {
                if (st != "")
                { options.backendServers.Add(st); }
            }

            options.localSaveFolder = this.LocalFlagSaveFolderTextBox.Text;
            options.localRepoFolder = this.LocalRepoFolderTextBox.Text;

            options.enableCheck = this.EnableCheckCheckBox.Checked;
            options.enablePurge = this.EnablePurgeCheckBox.Checked;
            options.useLocal = this.UseLocalRepoRadioButton.Checked;
            options.markTroll = this.MarkTrollCheckBox.Checked;
            options.deleteChildFree = this.DeleteChildFreeCheckBox.Checked;

            options.userAgent = this.UserAgentTextBox.Text;

            options.saveAndLoadFolder = this.TreeSaveAndLoadFolderTextBox.Text;

            options.repoUrl = this.RepoUrlTextBox.Text;

            OptionsManager.OptionsInstance = options;
            OptionsManager.SaveOptions();
		}

		private void TextBox1_Validating(object sender, CancelEventArgs e)
		{
			if (EnableCheckCheckBox.Checked && (LocalFlagSaveFolderTextBox.Text.Length == 0 || !Directory.Exists(LocalFlagSaveFolderTextBox.Text))) {
                MessageBox.Show("Invalid local save folder.", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}
		}

		private void TextBox2_Validating(object sender, CancelEventArgs e)
		{
			if (EnablePurgeCheckBox.Checked && UseLocalRepoRadioButton.Checked && (LocalRepoFolderTextBox.Text.Length == 0) || !Directory.Exists(LocalRepoFolderTextBox.Text)) {
                MessageBox.Show("Invalid local repo folder.", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}
		}

		private void userAgent_Validating(object sender, CancelEventArgs e)
		{
			if ((string.IsNullOrEmpty(UserAgentTextBox.Text) || string.IsNullOrWhiteSpace(UserAgentTextBox.Text))) {
                MessageBox.Show("Insert a valid User Agent or press Default", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}
		}

        private void repoUrl_Validating(object sender, CancelEventArgs e)
        {
            if ((string.IsNullOrEmpty(RepoUrlTextBox.Text) || string.IsNullOrWhiteSpace(RepoUrlTextBox.Text)))
            {
                MessageBox.Show("Insert a valid Url for the Repository or press Default", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void backendServers_Validating(object sender, CancelEventArgs e)
        {
            if (this.BackendServersTextBox.Lines.All(s => string.IsNullOrWhiteSpace(s)))
            {
                MessageBox.Show("Insert a valid Url for the backend server or press Default", "Flag Miner", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
		{
			if (FolderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				LocalFlagSaveFolderTextBox.Text = FolderBrowserDialog1.SelectedPath;
			}
		}

		private void enablePurge_CheckedChanged(object sender, EventArgs e)
		{
			Panel1.Enabled = EnablePurgeCheckBox.Checked;
		}

		private void Button4_Click(object sender, EventArgs e)
		{
			if (FolderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				LocalRepoFolderTextBox.Text = FolderBrowserDialog1.SelectedPath;
			}
		}

		private void defaultUserAgentButton_Click(object sender, EventArgs e)
		{
			UserAgentTextBox.Text = Properties.Resources.DefaultUserAgent;
        }

		private void Button6_Click(object sender, EventArgs e)
		{
			if (FolderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				TreeSaveAndLoadFolderTextBox.Text = FolderBrowserDialog1.SelectedPath;
			}
		}

        private void enableCheck_CheckedChanged(object sender, EventArgs e)
        {
            selectLocalDestFolderButton.Enabled = EnableCheckCheckBox.Checked;
        }

        private void defaultBackendButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restore default server address?", "Flag Miner", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.BackendServersTextBox.Clear();
                // DEFAULT SERVERS
                this.BackendServersTextBox.Text += Properties.Resources.backendBaseUrl + "\n";
            }
        }

        private void defaultRepoUrlButton_Click(object sender, EventArgs e)
        {
            this.RepoUrlTextBox.Text = Properties.Resources.DefaultflegsBaseUrl;
        }

        public override bool ValidateChildren()
        {
            foreach (Control control in new List<Control> { this.BackendServersTextBox, this.LocalRepoFolderTextBox, this.RepoUrlTextBox, this.UserAgentTextBox, this.LocalFlagSaveFolderTextBox })
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
