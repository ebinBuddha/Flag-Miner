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
namespace FlagMiner
{
	partial class OptionsForm
	{

		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try {
				if (disposing && components != null) {
					components.Dispose();
				}
			} finally {
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer

		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.EnableCheckCheckBox = new System.Windows.Forms.CheckBox();
            this.selectLocalDestFolderButton = new System.Windows.Forms.Button();
            this.LocalFlagSaveFolderTextBox = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.DeleteChildFreeCheckBox = new System.Windows.Forms.CheckBox();
            this.MarkTrollCheckBox = new System.Windows.Forms.CheckBox();
            this.EnablePurgeCheckBox = new System.Windows.Forms.CheckBox();
            this.LocalRepoFolderTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.defaultRepoUrlButton = new System.Windows.Forms.Button();
            this.RepoUrlTextBox = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button4 = new System.Windows.Forms.Button();
            this.UseLocalRepoRadioButton = new System.Windows.Forms.RadioButton();
            this.UseRemoteRepoRadioButton = new System.Windows.Forms.RadioButton();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Button6 = new System.Windows.Forms.Button();
            this.defaultUserAgentButton = new System.Windows.Forms.Button();
            this.TreeSaveAndLoadFolderTextBox = new System.Windows.Forms.TextBox();
            this.UserAgentTextBox = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BackendServersTextBox = new System.Windows.Forms.TextBox();
            this.defaultBackendButton = new System.Windows.Forms.Button();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.defaultArchiveButton = new System.Windows.Forms.Button();
            this.ArchiveUrlTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.Location = new System.Drawing.Point(316, 583);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "OK";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button2.Location = new System.Drawing.Point(397, 583);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 1;
            this.Button2.Text = "Cancel";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.EnableCheckCheckBox);
            this.GroupBox1.Controls.Add(this.selectLocalDestFolderButton);
            this.GroupBox1.Controls.Add(this.LocalFlagSaveFolderTextBox);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 139);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(461, 83);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Flag storage";
            // 
            // EnableCheckCheckBox
            // 
            this.EnableCheckCheckBox.AutoSize = true;
            this.EnableCheckCheckBox.Location = new System.Drawing.Point(6, 19);
            this.EnableCheckCheckBox.Name = "EnableCheckCheckBox";
            this.EnableCheckCheckBox.Size = new System.Drawing.Size(444, 17);
            this.EnableCheckCheckBox.TabIndex = 6;
            this.EnableCheckCheckBox.Text = "Mark saved flags using the local folder (use this only if you keep the repo folde" +
    "r structure)";
            this.EnableCheckCheckBox.UseVisualStyleBackColor = true;
            this.EnableCheckCheckBox.CheckedChanged += new System.EventHandler(this.enableCheck_CheckedChanged);
            // 
            // selectLocalDestFolderButton
            // 
            this.selectLocalDestFolderButton.Location = new System.Drawing.Point(368, 53);
            this.selectLocalDestFolderButton.Name = "selectLocalDestFolderButton";
            this.selectLocalDestFolderButton.Size = new System.Drawing.Size(50, 23);
            this.selectLocalDestFolderButton.TabIndex = 3;
            this.selectLocalDestFolderButton.Text = "...";
            this.selectLocalDestFolderButton.UseVisualStyleBackColor = true;
            this.selectLocalDestFolderButton.Click += new System.EventHandler(this.Button3_Click);
            // 
            // LocalFlagSaveFolderTextBox
            // 
            this.LocalFlagSaveFolderTextBox.Location = new System.Drawing.Point(9, 55);
            this.LocalFlagSaveFolderTextBox.Name = "LocalFlagSaveFolderTextBox";
            this.LocalFlagSaveFolderTextBox.ReadOnly = true;
            this.LocalFlagSaveFolderTextBox.Size = new System.Drawing.Size(353, 20);
            this.LocalFlagSaveFolderTextBox.TabIndex = 2;
            this.LocalFlagSaveFolderTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox1_Validating);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 39);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(185, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Local folder (e.g. C:\\Muh flags\\flags\\)";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.Controls.Add(this.Panel1);
            this.GroupBox2.Controls.Add(this.EnablePurgeCheckBox);
            this.GroupBox2.Location = new System.Drawing.Point(12, 228);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(461, 94);
            this.GroupBox2.TabIndex = 0;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Flag validation";
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.DeleteChildFreeCheckBox);
            this.Panel1.Controls.Add(this.MarkTrollCheckBox);
            this.Panel1.Location = new System.Drawing.Point(6, 42);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(447, 46);
            this.Panel1.TabIndex = 5;
            // 
            // DeleteChildFreeCheckBox
            // 
            this.DeleteChildFreeCheckBox.AutoSize = true;
            this.DeleteChildFreeCheckBox.Location = new System.Drawing.Point(2, 26);
            this.DeleteChildFreeCheckBox.Name = "DeleteChildFreeCheckBox";
            this.DeleteChildFreeCheckBox.Size = new System.Drawing.Size(270, 17);
            this.DeleteChildFreeCheckBox.TabIndex = 9;
            this.DeleteChildFreeCheckBox.Text = "When purging purge national flags with no regionals";
            this.DeleteChildFreeCheckBox.UseVisualStyleBackColor = true;
            // 
            // MarkTrollCheckBox
            // 
            this.MarkTrollCheckBox.AutoSize = true;
            this.MarkTrollCheckBox.Location = new System.Drawing.Point(2, 3);
            this.MarkTrollCheckBox.Name = "MarkTrollCheckBox";
            this.MarkTrollCheckBox.Size = new System.Drawing.Size(189, 17);
            this.MarkTrollCheckBox.TabIndex = 7;
            this.MarkTrollCheckBox.Text = "When purging purge also troll flags";
            this.MarkTrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // EnablePurgeCheckBox
            // 
            this.EnablePurgeCheckBox.AutoSize = true;
            this.EnablePurgeCheckBox.Location = new System.Drawing.Point(6, 19);
            this.EnablePurgeCheckBox.Name = "EnablePurgeCheckBox";
            this.EnablePurgeCheckBox.Size = new System.Drawing.Size(427, 17);
            this.EnablePurgeCheckBox.TabIndex = 0;
            this.EnablePurgeCheckBox.Text = "Enable validation and purging (use this only if you keep the repository folder st" +
    "ructure)";
            this.EnablePurgeCheckBox.UseVisualStyleBackColor = true;
            this.EnablePurgeCheckBox.CheckedChanged += new System.EventHandler(this.enablePurge_CheckedChanged);
            // 
            // LocalRepoFolderTextBox
            // 
            this.LocalRepoFolderTextBox.Location = new System.Drawing.Point(9, 55);
            this.LocalRepoFolderTextBox.Name = "LocalRepoFolderTextBox";
            this.LocalRepoFolderTextBox.ReadOnly = true;
            this.LocalRepoFolderTextBox.Size = new System.Drawing.Size(353, 20);
            this.LocalRepoFolderTextBox.TabIndex = 5;
            this.LocalRepoFolderTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox2_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(398, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Repo Url (e.g. https://gitlab.com/flagtism/Extra-Flags-for-4chan/raw/master/flags" +
    "/)";
            // 
            // defaultRepoUrlButton
            // 
            this.defaultRepoUrlButton.Location = new System.Drawing.Point(368, 115);
            this.defaultRepoUrlButton.Name = "defaultRepoUrlButton";
            this.defaultRepoUrlButton.Size = new System.Drawing.Size(50, 23);
            this.defaultRepoUrlButton.TabIndex = 11;
            this.defaultRepoUrlButton.Text = "Default";
            this.defaultRepoUrlButton.UseVisualStyleBackColor = true;
            this.defaultRepoUrlButton.Click += new System.EventHandler(this.defaultRepoUrlButton_Click);
            // 
            // RepoUrlTextBox
            // 
            this.RepoUrlTextBox.Location = new System.Drawing.Point(9, 117);
            this.RepoUrlTextBox.Name = "RepoUrlTextBox";
            this.RepoUrlTextBox.Size = new System.Drawing.Size(353, 20);
            this.RepoUrlTextBox.TabIndex = 10;
            this.RepoUrlTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.repoUrl_Validating);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 39);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(236, 13);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "Local folder (e.g. C:\\Extra-flags-for-4chan\\flags\\)";
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(368, 53);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(50, 23);
            this.Button4.TabIndex = 6;
            this.Button4.Text = "...";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // UseLocalRepoRadioButton
            // 
            this.UseLocalRepoRadioButton.AutoSize = true;
            this.UseLocalRepoRadioButton.Location = new System.Drawing.Point(6, 19);
            this.UseLocalRepoRadioButton.Name = "UseLocalRepoRadioButton";
            this.UseLocalRepoRadioButton.Size = new System.Drawing.Size(155, 17);
            this.UseLocalRepoRadioButton.TabIndex = 3;
            this.UseLocalRepoRadioButton.Text = "Use local copy of repository";
            this.UseLocalRepoRadioButton.UseVisualStyleBackColor = true;
            // 
            // UseRemoteRepoRadioButton
            // 
            this.UseRemoteRepoRadioButton.AutoSize = true;
            this.UseRemoteRepoRadioButton.Checked = true;
            this.UseRemoteRepoRadioButton.Location = new System.Drawing.Point(5, 81);
            this.UseRemoteRepoRadioButton.Name = "UseRemoteRepoRadioButton";
            this.UseRemoteRepoRadioButton.Size = new System.Drawing.Size(177, 17);
            this.UseRemoteRepoRadioButton.TabIndex = 4;
            this.UseRemoteRepoRadioButton.TabStop = true;
            this.UseRemoteRepoRadioButton.Text = "Use official repository image Urls";
            this.UseRemoteRepoRadioButton.UseVisualStyleBackColor = true;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.Controls.Add(this.Label4);
            this.GroupBox3.Controls.Add(this.Button6);
            this.GroupBox3.Controls.Add(this.defaultUserAgentButton);
            this.GroupBox3.Controls.Add(this.TreeSaveAndLoadFolderTextBox);
            this.GroupBox3.Controls.Add(this.UserAgentTextBox);
            this.GroupBox3.Controls.Add(this.Label3);
            this.GroupBox3.Location = new System.Drawing.Point(12, 479);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(461, 98);
            this.GroupBox3.TabIndex = 7;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Misc.";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(10, 55);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(130, 13);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "Save and Load tree folder";
            // 
            // Button6
            // 
            this.Button6.Location = new System.Drawing.Point(368, 69);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(50, 23);
            this.Button6.TabIndex = 10;
            this.Button6.Text = "...";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // defaultUserAgentButton
            // 
            this.defaultUserAgentButton.Location = new System.Drawing.Point(368, 30);
            this.defaultUserAgentButton.Name = "defaultUserAgentButton";
            this.defaultUserAgentButton.Size = new System.Drawing.Size(50, 23);
            this.defaultUserAgentButton.TabIndex = 9;
            this.defaultUserAgentButton.Text = "Default";
            this.defaultUserAgentButton.UseVisualStyleBackColor = true;
            this.defaultUserAgentButton.Click += new System.EventHandler(this.defaultUserAgentButton_Click);
            // 
            // TreeSaveAndLoadFolderTextBox
            // 
            this.TreeSaveAndLoadFolderTextBox.Location = new System.Drawing.Point(9, 71);
            this.TreeSaveAndLoadFolderTextBox.Name = "TreeSaveAndLoadFolderTextBox";
            this.TreeSaveAndLoadFolderTextBox.ReadOnly = true;
            this.TreeSaveAndLoadFolderTextBox.Size = new System.Drawing.Size(353, 20);
            this.TreeSaveAndLoadFolderTextBox.TabIndex = 9;
            // 
            // UserAgentTextBox
            // 
            this.UserAgentTextBox.Location = new System.Drawing.Point(9, 32);
            this.UserAgentTextBox.Name = "UserAgentTextBox";
            this.UserAgentTextBox.Size = new System.Drawing.Size(353, 20);
            this.UserAgentTextBox.TabIndex = 8;
            this.UserAgentTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.userAgent_Validating);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(10, 16);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(60, 13);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "User Agent";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Backend servers (one per line):";
            // 
            // BackendServersTextBox
            // 
            this.BackendServersTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BackendServersTextBox.Location = new System.Drawing.Point(12, 75);
            this.BackendServersTextBox.Multiline = true;
            this.BackendServersTextBox.Name = "BackendServersTextBox";
            this.BackendServersTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.BackendServersTextBox.Size = new System.Drawing.Size(405, 58);
            this.BackendServersTextBox.TabIndex = 9;
            this.BackendServersTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.backendServers_Validating);
            // 
            // defaultBackendButton
            // 
            this.defaultBackendButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultBackendButton.Location = new System.Drawing.Point(423, 75);
            this.defaultBackendButton.Name = "defaultBackendButton";
            this.defaultBackendButton.Size = new System.Drawing.Size(50, 23);
            this.defaultBackendButton.TabIndex = 12;
            this.defaultBackendButton.Text = "Default";
            this.defaultBackendButton.UseVisualStyleBackColor = true;
            this.defaultBackendButton.Click += new System.EventHandler(this.defaultBackendButton_Click);
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.Controls.Add(this.label6);
            this.GroupBox4.Controls.Add(this.defaultRepoUrlButton);
            this.GroupBox4.Controls.Add(this.RepoUrlTextBox);
            this.GroupBox4.Controls.Add(this.Label2);
            this.GroupBox4.Controls.Add(this.Button4);
            this.GroupBox4.Controls.Add(this.LocalRepoFolderTextBox);
            this.GroupBox4.Controls.Add(this.UseLocalRepoRadioButton);
            this.GroupBox4.Controls.Add(this.UseRemoteRepoRadioButton);
            this.GroupBox4.Location = new System.Drawing.Point(12, 328);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(461, 145);
            this.GroupBox4.TabIndex = 13;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Flag sources";
            // 
            // defaultArchiveButton
            // 
            this.defaultArchiveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultArchiveButton.Location = new System.Drawing.Point(423, 28);
            this.defaultArchiveButton.Name = "defaultArchiveButton";
            this.defaultArchiveButton.Size = new System.Drawing.Size(50, 23);
            this.defaultArchiveButton.TabIndex = 16;
            this.defaultArchiveButton.Text = "Default";
            this.defaultArchiveButton.UseVisualStyleBackColor = true;
            this.defaultArchiveButton.Click += new System.EventHandler(this.defaultArchiveButton_Click);
            // 
            // ArchiveUrlTextBox
            // 
            this.ArchiveUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArchiveUrlTextBox.Location = new System.Drawing.Point(12, 30);
            this.ArchiveUrlTextBox.Name = "ArchiveUrlTextBox";
            this.ArchiveUrlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ArchiveUrlTextBox.Size = new System.Drawing.Size(405, 20);
            this.ArchiveUrlTextBox.TabIndex = 15;
            this.ArchiveUrlTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.archiveUrl_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Archive base url:";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(484, 618);
            this.Controls.Add(this.defaultArchiveButton);
            this.Controls.Add(this.ArchiveUrlTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.defaultBackendButton);
            this.Controls.Add(this.BackendServersTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button Button1;
		private System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.CheckBox EnableCheckCheckBox;
		private System.Windows.Forms.Button selectLocalDestFolderButton;
		private System.Windows.Forms.TextBox LocalFlagSaveFolderTextBox;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.CheckBox MarkTrollCheckBox;
		private System.Windows.Forms.Button Button4;
		private System.Windows.Forms.TextBox LocalRepoFolderTextBox;
		internal System.Windows.Forms.RadioButton UseLocalRepoRadioButton;
		internal System.Windows.Forms.RadioButton UseRemoteRepoRadioButton;
		private System.Windows.Forms.CheckBox EnablePurgeCheckBox;
		internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
		internal System.Windows.Forms.GroupBox GroupBox3;
		private System.Windows.Forms.TextBox UserAgentTextBox;
		internal System.Windows.Forms.Label Label3;
		private System.Windows.Forms.Button defaultUserAgentButton;
		internal System.Windows.Forms.Label Label4;
		private System.Windows.Forms.Button Button6;
		internal System.Windows.Forms.TextBox TreeSaveAndLoadFolderTextBox;
		internal System.Windows.Forms.CheckBox DeleteChildFreeCheckBox;
        private Label label5;
        private TextBox BackendServersTextBox;
        private Button defaultBackendButton;
        internal Label label6;
        private Button defaultRepoUrlButton;
        private TextBox RepoUrlTextBox;
        private GroupBox GroupBox4;
        private Button defaultArchiveButton;
        private TextBox ArchiveUrlTextBox;
        private Label label7;
    }
}
