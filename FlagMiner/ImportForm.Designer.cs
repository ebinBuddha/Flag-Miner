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
	partial class ImportForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
			this.Button1 = new System.Windows.Forms.Button();
			this.Button2 = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.TextBox1 = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.TextBox2 = new System.Windows.Forms.TextBox();
			this.WebBrowser1 = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			//
			//Button1
			//
			this.Button1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Button1.Location = new System.Drawing.Point(155, 232);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(75, 23);
			this.Button1.TabIndex = 0;
			this.Button1.Text = "Parse!";
			this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += Button1_Click;
			//
			//Button2
			//
			this.Button2.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Button2.Location = new System.Drawing.Point(236, 232);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(75, 23);
			this.Button2.TabIndex = 1;
			this.Button2.Text = "Cancel";
			this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += Button2_Click;
			//
			//Label1
			//
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(12, 9);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(60, 13);
			this.Label1.TabIndex = 2;
			this.Label1.Text = "Thread Url:";
			//
			//TextBox1
			//
			this.TextBox1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBox1.Location = new System.Drawing.Point(12, 25);
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new System.Drawing.Size(299, 20);
			this.TextBox1.TabIndex = 3;
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(12, 61);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(171, 13);
			this.Label2.TabIndex = 4;
			this.Label2.Text = "Script Dump Post #s (one per line):";
			//
			//TextBox2
			//
			this.TextBox2.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.TextBox2.Location = new System.Drawing.Point(12, 77);
			this.TextBox2.Multiline = true;
			this.TextBox2.Name = "TextBox2";
			this.TextBox2.Size = new System.Drawing.Size(299, 149);
			this.TextBox2.TabIndex = 5;
			//
			//WebBrowser1
			//
			this.WebBrowser1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.WebBrowser1.Location = new System.Drawing.Point(12, 77);
			this.WebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.WebBrowser1.Name = "WebBrowser1";
			this.WebBrowser1.Size = new System.Drawing.Size(299, 149);
			this.WebBrowser1.TabIndex = 6;
			//
			//ImportForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(323, 267);
			this.Controls.Add(this.TextBox2);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.TextBox1);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.Button2);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.WebBrowser1);
			this.HelpButton = true;
            this.HelpRequested += new HelpEventHandler(this.ImportForm_HelpRequested);
			this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(200, 180);
			this.Name = "ImportForm";
			this.Text = "Import from posts...";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        private System.Windows.Forms.Button Button1;
		private System.Windows.Forms.Button Button2;
		private System.Windows.Forms.Label Label1;
		private System.Windows.Forms.TextBox TextBox1;
		private System.Windows.Forms.Label Label2;
		private System.Windows.Forms.TextBox TextBox2;
		private System.Windows.Forms.WebBrowser WebBrowser1;

	}
}
