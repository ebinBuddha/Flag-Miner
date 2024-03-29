﻿
namespace FlagMiner
{
    partial class DumperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DumperForm));
            this.copyBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.iconImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupABox = new System.Windows.Forms.GroupBox();
            this.groupAListView = new System.Windows.Forms.ListView();
            this.removeABtn = new System.Windows.Forms.Button();
            this.addABtn = new System.Windows.Forms.Button();
            this.groupBBox = new System.Windows.Forms.GroupBox();
            this.groupBListView = new System.Windows.Forms.ListView();
            this.removeBBtn = new System.Windows.Forms.Button();
            this.addBBtn = new System.Windows.Forms.Button();
            this.optionsBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.headerCheckBox = new System.Windows.Forms.CheckBox();
            this.headerTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.separationCheckBox = new System.Windows.Forms.CheckBox();
            this.separationTextBox = new System.Windows.Forms.TextBox();
            this.footerCheckBox = new System.Windows.Forms.CheckBox();
            this.footerTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupARadioButton = new System.Windows.Forms.RadioButton();
            this.groupAminusBRadioButton = new System.Windows.Forms.RadioButton();
            this.statusLabel = new System.Windows.Forms.Label();
            this.openFlagFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.boardComboBox = new System.Windows.Forms.ComboBox();
            this.boardLabel = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupABox.SuspendLayout();
            this.groupBBox.SuspendLayout();
            this.optionsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // copyBtn
            // 
            this.copyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.copyBtn.Location = new System.Drawing.Point(532, 535);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(125, 23);
            this.copyBtn.TabIndex = 0;
            this.copyBtn.Text = "Copy to clipboard";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(663, 535);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(125, 23);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // iconImageList
            // 
            this.iconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.iconImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.iconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.splitContainer3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 534);
            this.panel2.TabIndex = 6;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.optionsBox);
            this.splitContainer3.Size = new System.Drawing.Size(800, 534);
            this.splitContainer3.SplitterDistance = 385;
            this.splitContainer3.TabIndex = 6;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupABox);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.groupBBox);
            this.splitContainer4.Size = new System.Drawing.Size(385, 534);
            this.splitContainer4.SplitterDistance = 258;
            this.splitContainer4.TabIndex = 0;
            // 
            // groupABox
            // 
            this.groupABox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupABox.Controls.Add(this.groupAListView);
            this.groupABox.Controls.Add(this.removeABtn);
            this.groupABox.Controls.Add(this.addABtn);
            this.groupABox.Location = new System.Drawing.Point(3, 12);
            this.groupABox.MinimumSize = new System.Drawing.Size(200, 150);
            this.groupABox.Name = "groupABox";
            this.groupABox.Size = new System.Drawing.Size(378, 243);
            this.groupABox.TabIndex = 2;
            this.groupABox.TabStop = false;
            this.groupABox.Text = "Group A";
            // 
            // groupAListView
            // 
            this.groupAListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAListView.HideSelection = false;
            this.groupAListView.Location = new System.Drawing.Point(7, 20);
            this.groupAListView.Name = "groupAListView";
            this.groupAListView.Size = new System.Drawing.Size(284, 208);
            this.groupAListView.TabIndex = 6;
            this.groupAListView.UseCompatibleStateImageBehavior = false;
            this.groupAListView.View = System.Windows.Forms.View.List;
            // 
            // removeABtn
            // 
            this.removeABtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeABtn.Location = new System.Drawing.Point(297, 48);
            this.removeABtn.Name = "removeABtn";
            this.removeABtn.Size = new System.Drawing.Size(75, 23);
            this.removeABtn.TabIndex = 5;
            this.removeABtn.Text = "Remove";
            this.removeABtn.UseVisualStyleBackColor = true;
            this.removeABtn.Click += new System.EventHandler(this.removeABtn_Click);
            // 
            // addABtn
            // 
            this.addABtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addABtn.Location = new System.Drawing.Point(297, 19);
            this.addABtn.Name = "addABtn";
            this.addABtn.Size = new System.Drawing.Size(75, 23);
            this.addABtn.TabIndex = 4;
            this.addABtn.Text = "Add...";
            this.addABtn.UseVisualStyleBackColor = true;
            this.addABtn.Click += new System.EventHandler(this.AddABtn_Click);
            // 
            // groupBBox
            // 
            this.groupBBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBBox.Controls.Add(this.groupBListView);
            this.groupBBox.Controls.Add(this.removeBBtn);
            this.groupBBox.Controls.Add(this.addBBtn);
            this.groupBBox.Location = new System.Drawing.Point(3, 3);
            this.groupBBox.MinimumSize = new System.Drawing.Size(200, 150);
            this.groupBBox.Name = "groupBBox";
            this.groupBBox.Size = new System.Drawing.Size(378, 266);
            this.groupBBox.TabIndex = 3;
            this.groupBBox.TabStop = false;
            this.groupBBox.Text = "Group B";
            // 
            // groupBListView
            // 
            this.groupBListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBListView.HideSelection = false;
            this.groupBListView.Location = new System.Drawing.Point(7, 19);
            this.groupBListView.Name = "groupBListView";
            this.groupBListView.Size = new System.Drawing.Size(284, 231);
            this.groupBListView.TabIndex = 9;
            this.groupBListView.UseCompatibleStateImageBehavior = false;
            this.groupBListView.View = System.Windows.Forms.View.List;
            // 
            // removeBBtn
            // 
            this.removeBBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeBBtn.Location = new System.Drawing.Point(297, 47);
            this.removeBBtn.Name = "removeBBtn";
            this.removeBBtn.Size = new System.Drawing.Size(75, 23);
            this.removeBBtn.TabIndex = 8;
            this.removeBBtn.Text = "Remove";
            this.removeBBtn.UseVisualStyleBackColor = true;
            this.removeBBtn.Click += new System.EventHandler(this.removeBBtn_Click);
            // 
            // addBBtn
            // 
            this.addBBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addBBtn.Location = new System.Drawing.Point(297, 18);
            this.addBBtn.Name = "addBBtn";
            this.addBBtn.Size = new System.Drawing.Size(75, 23);
            this.addBBtn.TabIndex = 7;
            this.addBBtn.Text = "Add...";
            this.addBBtn.UseVisualStyleBackColor = true;
            this.addBBtn.Click += new System.EventHandler(this.addBBtn_Click);
            // 
            // optionsBox
            // 
            this.optionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsBox.Controls.Add(this.splitContainer1);
            this.optionsBox.Controls.Add(this.panel1);
            this.optionsBox.Location = new System.Drawing.Point(3, 12);
            this.optionsBox.Name = "optionsBox";
            this.optionsBox.Size = new System.Drawing.Size(405, 519);
            this.optionsBox.TabIndex = 4;
            this.optionsBox.TabStop = false;
            this.optionsBox.Text = "Options";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 76);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.headerCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.headerTextBox);
            this.splitContainer1.Panel1MinSize = 70;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 140;
            this.splitContainer1.Size = new System.Drawing.Size(393, 437);
            this.splitContainer1.SplitterDistance = 134;
            this.splitContainer1.TabIndex = 1;
            // 
            // headerCheckBox
            // 
            this.headerCheckBox.AutoSize = true;
            this.headerCheckBox.Checked = true;
            this.headerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.headerCheckBox.Location = new System.Drawing.Point(3, 3);
            this.headerCheckBox.Name = "headerCheckBox";
            this.headerCheckBox.Size = new System.Drawing.Size(61, 17);
            this.headerCheckBox.TabIndex = 1;
            this.headerCheckBox.Text = "Header";
            this.headerCheckBox.UseVisualStyleBackColor = true;
            this.headerCheckBox.CheckedChanged += new System.EventHandler(this.HeaderCheckBox_CheckedChanged);
            // 
            // headerTextBox
            // 
            this.headerTextBox.AcceptsReturn = true;
            this.headerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerTextBox.Location = new System.Drawing.Point(3, 26);
            this.headerTextBox.MinimumSize = new System.Drawing.Size(200, 70);
            this.headerTextBox.Multiline = true;
            this.headerTextBox.Name = "headerTextBox";
            this.headerTextBox.Size = new System.Drawing.Size(387, 104);
            this.headerTextBox.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.separationCheckBox);
            this.splitContainer2.Panel1.Controls.Add(this.separationTextBox);
            this.splitContainer2.Panel1MinSize = 70;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.footerCheckBox);
            this.splitContainer2.Panel2.Controls.Add(this.footerTextBox);
            this.splitContainer2.Panel2MinSize = 70;
            this.splitContainer2.Size = new System.Drawing.Size(393, 299);
            this.splitContainer2.SplitterDistance = 141;
            this.splitContainer2.TabIndex = 0;
            // 
            // separationCheckBox
            // 
            this.separationCheckBox.AutoSize = true;
            this.separationCheckBox.Checked = true;
            this.separationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.separationCheckBox.Location = new System.Drawing.Point(3, 4);
            this.separationCheckBox.Name = "separationCheckBox";
            this.separationCheckBox.Size = new System.Drawing.Size(97, 17);
            this.separationCheckBox.TabIndex = 2;
            this.separationCheckBox.Text = "Separation text";
            this.separationCheckBox.UseVisualStyleBackColor = true;
            this.separationCheckBox.CheckedChanged += new System.EventHandler(this.SeparationCheckBox_CheckedChanged);
            // 
            // separationTextBox
            // 
            this.separationTextBox.AcceptsReturn = true;
            this.separationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separationTextBox.Location = new System.Drawing.Point(3, 27);
            this.separationTextBox.MinimumSize = new System.Drawing.Size(200, 70);
            this.separationTextBox.Multiline = true;
            this.separationTextBox.Name = "separationTextBox";
            this.separationTextBox.Size = new System.Drawing.Size(387, 111);
            this.separationTextBox.TabIndex = 5;
            // 
            // footerCheckBox
            // 
            this.footerCheckBox.AutoSize = true;
            this.footerCheckBox.Checked = true;
            this.footerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.footerCheckBox.Location = new System.Drawing.Point(3, 3);
            this.footerCheckBox.Name = "footerCheckBox";
            this.footerCheckBox.Size = new System.Drawing.Size(56, 17);
            this.footerCheckBox.TabIndex = 3;
            this.footerCheckBox.Text = "Footer";
            this.footerCheckBox.UseVisualStyleBackColor = true;
            this.footerCheckBox.CheckedChanged += new System.EventHandler(this.FooterCheckBox_CheckedChanged);
            // 
            // footerTextBox
            // 
            this.footerTextBox.AcceptsReturn = true;
            this.footerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.footerTextBox.Location = new System.Drawing.Point(3, 26);
            this.footerTextBox.MinimumSize = new System.Drawing.Size(200, 70);
            this.footerTextBox.Multiline = true;
            this.footerTextBox.Name = "footerTextBox";
            this.footerTextBox.Size = new System.Drawing.Size(387, 125);
            this.footerTextBox.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupARadioButton);
            this.panel1.Controls.Add(this.groupAminusBRadioButton);
            this.panel1.Location = new System.Drawing.Point(6, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 54);
            this.panel1.TabIndex = 0;
            // 
            // groupARadioButton
            // 
            this.groupARadioButton.AutoSize = true;
            this.groupARadioButton.Location = new System.Drawing.Point(3, 8);
            this.groupARadioButton.Name = "groupARadioButton";
            this.groupARadioButton.Size = new System.Drawing.Size(86, 17);
            this.groupARadioButton.TabIndex = 5;
            this.groupARadioButton.Text = "Group A only";
            this.groupARadioButton.UseVisualStyleBackColor = true;
            // 
            // groupAminusBRadioButton
            // 
            this.groupAminusBRadioButton.AutoSize = true;
            this.groupAminusBRadioButton.Checked = true;
            this.groupAminusBRadioButton.Location = new System.Drawing.Point(3, 31);
            this.groupAminusBRadioButton.Name = "groupAminusBRadioButton";
            this.groupAminusBRadioButton.Size = new System.Drawing.Size(263, 17);
            this.groupAminusBRadioButton.TabIndex = 6;
            this.groupAminusBRadioButton.TabStop = true;
            this.groupAminusBRadioButton.Text = "Group A minus Group B, followed by what\'s in both";
            this.groupAminusBRadioButton.UseVisualStyleBackColor = true;
            this.groupAminusBRadioButton.CheckedChanged += new System.EventHandler(this.GroupAminusBRadioButton_CheckedChanged);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.Location = new System.Drawing.Point(208, 536);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(318, 21);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.Text = "Ready";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFlagFileDialog
            // 
            this.openFlagFileDialog.Filter = "Xml files|*.xml";
            this.openFlagFileDialog.Multiselect = true;
            // 
            // boardComboBox
            // 
            this.boardComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.boardComboBox.FormattingEnabled = true;
            this.boardComboBox.Location = new System.Drawing.Point(119, 537);
            this.boardComboBox.Name = "boardComboBox";
            this.boardComboBox.Size = new System.Drawing.Size(83, 21);
            this.boardComboBox.TabIndex = 7;
            // 
            // boardLabel
            // 
            this.boardLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.boardLabel.Location = new System.Drawing.Point(-3, 536);
            this.boardLabel.Name = "boardLabel";
            this.boardLabel.Size = new System.Drawing.Size(116, 21);
            this.boardLabel.TabIndex = 8;
            this.boardLabel.Text = "Make links relative to:";
            this.boardLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DumperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 570);
            this.Controls.Add(this.boardLabel);
            this.Controls.Add(this.boardComboBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.copyBtn);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "DumperForm";
            this.Text = "Create the flag dump...";
            this.Load += new System.EventHandler(this.DumperForm_Load);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupABox.ResumeLayout(false);
            this.groupBBox.ResumeLayout(false);
            this.optionsBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button copyBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.ImageList iconImageList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupABox;
        private System.Windows.Forms.ListView groupAListView;
        private System.Windows.Forms.Button removeABtn;
        private System.Windows.Forms.Button addABtn;
        private System.Windows.Forms.GroupBox groupBBox;
        private System.Windows.Forms.ListView groupBListView;
        private System.Windows.Forms.Button removeBBtn;
        private System.Windows.Forms.Button addBBtn;
        private System.Windows.Forms.GroupBox optionsBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox headerCheckBox;
        private System.Windows.Forms.TextBox headerTextBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox separationCheckBox;
        private System.Windows.Forms.TextBox separationTextBox;
        private System.Windows.Forms.CheckBox footerCheckBox;
        private System.Windows.Forms.TextBox footerTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton groupARadioButton;
        private System.Windows.Forms.RadioButton groupAminusBRadioButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.OpenFileDialog openFlagFileDialog;
        private System.Windows.Forms.ComboBox boardComboBox;
        private System.Windows.Forms.Label boardLabel;
    }
}