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
            this.enableCheck = new System.Windows.Forms.CheckBox();
            this.Button3 = new System.Windows.Forms.Button();
            this.localSaveFolder = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.deleteChildFree = new System.Windows.Forms.CheckBox();
            this.markTroll = new System.Windows.Forms.CheckBox();
            this.enablePurge = new System.Windows.Forms.CheckBox();
            this.localRepoFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.repoUrl = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button4 = new System.Windows.Forms.Button();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Button6 = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.saveAndLoadFolder = new System.Windows.Forms.TextBox();
            this.userAgent = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.backendServers = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
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
            this.Button1.Location = new System.Drawing.Point(316, 533);
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
            this.Button2.Location = new System.Drawing.Point(397, 533);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 1;
            this.Button2.Text = "Cancel";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.enableCheck);
            this.GroupBox1.Controls.Add(this.Button3);
            this.GroupBox1.Controls.Add(this.localSaveFolder);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 89);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(461, 83);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Flag storage";
            // 
            // enableCheck
            // 
            this.enableCheck.AutoSize = true;
            this.enableCheck.Location = new System.Drawing.Point(6, 19);
            this.enableCheck.Name = "enableCheck";
            this.enableCheck.Size = new System.Drawing.Size(444, 17);
            this.enableCheck.TabIndex = 6;
            this.enableCheck.Text = "Mark saved flags using the local folder (use this only if you keep the repo folde" +
    "r structure)";
            this.enableCheck.UseVisualStyleBackColor = true;
            this.enableCheck.CheckedChanged += new System.EventHandler(this.enableCheck_CheckedChanged);
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(368, 53);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(50, 23);
            this.Button3.TabIndex = 3;
            this.Button3.Text = "...";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // localSaveFolder
            // 
            this.localSaveFolder.Location = new System.Drawing.Point(9, 55);
            this.localSaveFolder.Name = "localSaveFolder";
            this.localSaveFolder.ReadOnly = true;
            this.localSaveFolder.Size = new System.Drawing.Size(353, 20);
            this.localSaveFolder.TabIndex = 2;
            this.localSaveFolder.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox1_Validating);
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
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.Controls.Add(this.Panel1);
            this.GroupBox2.Controls.Add(this.enablePurge);
            this.GroupBox2.Location = new System.Drawing.Point(12, 178);
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
            this.Panel1.Controls.Add(this.deleteChildFree);
            this.Panel1.Controls.Add(this.markTroll);
            this.Panel1.Location = new System.Drawing.Point(6, 42);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(447, 46);
            this.Panel1.TabIndex = 5;
            // 
            // deleteChildFree
            // 
            this.deleteChildFree.AutoSize = true;
            this.deleteChildFree.Location = new System.Drawing.Point(2, 26);
            this.deleteChildFree.Name = "deleteChildFree";
            this.deleteChildFree.Size = new System.Drawing.Size(270, 17);
            this.deleteChildFree.TabIndex = 9;
            this.deleteChildFree.Text = "When purging purge national flags with no regionals";
            this.deleteChildFree.UseVisualStyleBackColor = true;
            // 
            // markTroll
            // 
            this.markTroll.AutoSize = true;
            this.markTroll.Location = new System.Drawing.Point(2, 3);
            this.markTroll.Name = "markTroll";
            this.markTroll.Size = new System.Drawing.Size(189, 17);
            this.markTroll.TabIndex = 7;
            this.markTroll.Text = "When purging purge also troll flags";
            this.markTroll.UseVisualStyleBackColor = true;
            // 
            // enablePurge
            // 
            this.enablePurge.AutoSize = true;
            this.enablePurge.Location = new System.Drawing.Point(6, 19);
            this.enablePurge.Name = "enablePurge";
            this.enablePurge.Size = new System.Drawing.Size(427, 17);
            this.enablePurge.TabIndex = 0;
            this.enablePurge.Text = "Enable validation and purging (use this only if you keep the repository folder st" +
    "ructure)";
            this.enablePurge.UseVisualStyleBackColor = true;
            this.enablePurge.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // localRepoFolder
            // 
            this.localRepoFolder.Location = new System.Drawing.Point(9, 55);
            this.localRepoFolder.Name = "localRepoFolder";
            this.localRepoFolder.ReadOnly = true;
            this.localRepoFolder.Size = new System.Drawing.Size(353, 20);
            this.localRepoFolder.TabIndex = 5;
            this.localRepoFolder.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox2_Validating);
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
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(368, 115);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(50, 23);
            this.button8.TabIndex = 11;
            this.button8.Text = "Default";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // repoUrl
            // 
            this.repoUrl.Location = new System.Drawing.Point(9, 117);
            this.repoUrl.Name = "repoUrl";
            this.repoUrl.Size = new System.Drawing.Size(353, 20);
            this.repoUrl.TabIndex = 10;
            this.repoUrl.Validating += new System.ComponentModel.CancelEventHandler(this.repoUrl_Validating);
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
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Location = new System.Drawing.Point(6, 19);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(155, 17);
            this.RadioButton1.TabIndex = 3;
            this.RadioButton1.Text = "Use local copy of repository";
            this.RadioButton1.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Checked = true;
            this.RadioButton2.Location = new System.Drawing.Point(5, 81);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(177, 17);
            this.RadioButton2.TabIndex = 4;
            this.RadioButton2.TabStop = true;
            this.RadioButton2.Text = "Use official repository image Urls";
            this.RadioButton2.UseVisualStyleBackColor = true;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox3.Controls.Add(this.Label4);
            this.GroupBox3.Controls.Add(this.Button6);
            this.GroupBox3.Controls.Add(this.Button5);
            this.GroupBox3.Controls.Add(this.saveAndLoadFolder);
            this.GroupBox3.Controls.Add(this.userAgent);
            this.GroupBox3.Controls.Add(this.Label3);
            this.GroupBox3.Location = new System.Drawing.Point(12, 429);
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
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(368, 30);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(50, 23);
            this.Button5.TabIndex = 9;
            this.Button5.Text = "Default";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // saveAndLoadFolder
            // 
            this.saveAndLoadFolder.Location = new System.Drawing.Point(9, 71);
            this.saveAndLoadFolder.Name = "saveAndLoadFolder";
            this.saveAndLoadFolder.ReadOnly = true;
            this.saveAndLoadFolder.Size = new System.Drawing.Size(353, 20);
            this.saveAndLoadFolder.TabIndex = 9;
            // 
            // userAgent
            // 
            this.userAgent.CausesValidation = false;
            this.userAgent.Location = new System.Drawing.Point(9, 32);
            this.userAgent.Name = "userAgent";
            this.userAgent.Size = new System.Drawing.Size(353, 20);
            this.userAgent.TabIndex = 8;
            this.userAgent.Validating += new System.ComponentModel.CancelEventHandler(this.userAgent_Validating);
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
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Backend servers (one per line):";
            // 
            // backendServers
            // 
            this.backendServers.Location = new System.Drawing.Point(12, 25);
            this.backendServers.Multiline = true;
            this.backendServers.Name = "backendServers";
            this.backendServers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.backendServers.Size = new System.Drawing.Size(405, 58);
            this.backendServers.TabIndex = 9;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(423, 25);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(50, 23);
            this.button7.TabIndex = 12;
            this.button7.Text = "Default";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // GroupBox4
            // 
            this.GroupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox4.Controls.Add(this.label6);
            this.GroupBox4.Controls.Add(this.button8);
            this.GroupBox4.Controls.Add(this.repoUrl);
            this.GroupBox4.Controls.Add(this.Label2);
            this.GroupBox4.Controls.Add(this.Button4);
            this.GroupBox4.Controls.Add(this.localRepoFolder);
            this.GroupBox4.Controls.Add(this.RadioButton1);
            this.GroupBox4.Controls.Add(this.RadioButton2);
            this.GroupBox4.Location = new System.Drawing.Point(12, 278);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(461, 145);
            this.GroupBox4.TabIndex = 13;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Flag sources";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 568);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.backendServers);
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
		internal System.Windows.Forms.CheckBox enableCheck;
		private System.Windows.Forms.Button Button3;
		private System.Windows.Forms.TextBox localSaveFolder;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Panel Panel1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.CheckBox markTroll;
		private System.Windows.Forms.Button Button4;
		private System.Windows.Forms.TextBox localRepoFolder;
		internal System.Windows.Forms.RadioButton RadioButton1;
		internal System.Windows.Forms.RadioButton RadioButton2;
		private System.Windows.Forms.CheckBox enablePurge;
		internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
		internal System.Windows.Forms.GroupBox GroupBox3;
		private System.Windows.Forms.TextBox userAgent;
		internal System.Windows.Forms.Label Label3;
		private System.Windows.Forms.Button Button5;
		internal System.Windows.Forms.Label Label4;
		private System.Windows.Forms.Button Button6;
		internal System.Windows.Forms.TextBox saveAndLoadFolder;
		internal System.Windows.Forms.CheckBox deleteChildFree;
        private Label label5;
        private TextBox backendServers;
        private Button button7;
        internal Label label6;
        private Button button8;
        private TextBox repoUrl;
        private GroupBox GroupBox4;
    }
}
