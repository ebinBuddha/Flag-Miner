
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlagMiner
{
	partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ThreadColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BackgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.OpenXmlDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveXmlDialog = new System.Windows.Forms.SaveFileDialog();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.DateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.TitleColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.TreeListView1 = new BrightIdeasSoftware.TreeListView();
            this.FlagsColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.intCheck = new System.Windows.Forms.CheckBox();
            this.spCheck = new System.Windows.Forms.CheckBox();
            this.polCheck = new System.Windows.Forms.CheckBox();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.PopUpMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyFlagToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusText = new System.Windows.Forms.TextBox();
            this.ToolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtractMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.abortMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.flagsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.checkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.clearmenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.loadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.subtractToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.mineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.checkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.purgeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.copyFlagToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveFlagToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyLinkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.collapseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.expandToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreeListView1)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.PopUpMenu.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ThreadColumn
            // 
            this.ThreadColumn.AspectName = "thread";
            this.ThreadColumn.Hyperlink = true;
            this.ThreadColumn.MinimumWidth = 150;
            this.ThreadColumn.Text = "Thread";
            this.ThreadColumn.Width = 150;
            // 
            // BackgroundWorker2
            // 
            this.BackgroundWorker2.WorkerReportsProgress = true;
            this.BackgroundWorker2.WorkerSupportsCancellation = true;
            this.BackgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker2_DoWork);
            this.BackgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            this.BackgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // ImageList1
            // 
            this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList1.ImageSize = new System.Drawing.Size(11, 11);
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Checked = true;
            this.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox2.Location = new System.Drawing.Point(6, 19);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(139, 17);
            this.CheckBox2.TabIndex = 38;
            this.CheckBox2.Text = "exclude previously seen";
            this.CheckBox2.UseVisualStyleBackColor = true;
            this.CheckBox2.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(6, 42);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(118, 17);
            this.CheckBox1.TabIndex = 37;
            this.CheckBox1.Text = "archived after date:";
            this.CheckBox1.UseVisualStyleBackColor = true;
            this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.Title = "Save flag...";
            // 
            // OpenXmlDialog
            // 
            this.OpenXmlDialog.FileName = "SavedTree.xml";
            this.OpenXmlDialog.Filter = "Xml files|*.xml|All files|*.*";
            // 
            // SaveXmlDialog
            // 
            this.SaveXmlDialog.Filter = "Xml files|*.xml|All files|*.*";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(47, 105);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(0, 13);
            this.Label2.TabIndex = 46;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 105);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 13);
            this.Label1.TabIndex = 45;
            this.Label1.Text = "status: ";
            // 
            // DateTimePicker1
            // 
            this.DateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm";
            this.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePicker1.Location = new System.Drawing.Point(6, 62);
            this.DateTimePicker1.Name = "DateTimePicker1";
            this.DateTimePicker1.Size = new System.Drawing.Size(159, 20);
            this.DateTimePicker1.TabIndex = 34;
            this.DateTimePicker1.ValueChanged += new System.EventHandler(this.DateTimePicker1_ValueChanged);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.CheckBox2);
            this.GroupBox2.Controls.Add(this.CheckBox1);
            this.GroupBox2.Controls.Add(this.DateTimePicker1);
            this.GroupBox2.Location = new System.Drawing.Point(102, 6);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(267, 89);
            this.GroupBox2.TabIndex = 42;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Options";
            // 
            // TitleColumn
            // 
            this.TitleColumn.AspectName = "title";
            this.TitleColumn.MinimumWidth = 150;
            this.TitleColumn.Text = "Title";
            this.TitleColumn.Width = 150;
            // 
            // TreeListView1
            // 
            this.TreeListView1.AllColumns.Add(this.FlagsColumn);
            this.TreeListView1.AllColumns.Add(this.TitleColumn);
            this.TreeListView1.AllColumns.Add(this.ThreadColumn);
            this.TreeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeListView1.CellEditUseWholeCell = false;
            this.TreeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FlagsColumn,
            this.TitleColumn,
            this.ThreadColumn});
            this.TreeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.TreeListView1.FullRowSelect = true;
            this.TreeListView1.Location = new System.Drawing.Point(375, 8);
            this.TreeListView1.Name = "TreeListView1";
            this.TreeListView1.ShowGroups = false;
            this.TreeListView1.Size = new System.Drawing.Size(484, 395);
            this.TreeListView1.SmallImageList = this.ImageList1;
            this.TreeListView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.TreeListView1.TabIndex = 44;
            this.TreeListView1.TintSortColumn = true;
            this.TreeListView1.UseCompatibleStateImageBehavior = false;
            this.TreeListView1.UseHotControls = false;
            this.TreeListView1.UseHyperlinks = true;
            this.TreeListView1.View = System.Windows.Forms.View.Details;
            this.TreeListView1.VirtualMode = true;
            this.TreeListView1.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.olv_CellRightClick);
            this.TreeListView1.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.FormatRow_EventHandler);
            this.TreeListView1.SelectionChanged += new System.EventHandler(TreeListView1_SelectionChanged);
            // 
            // FlagsColumn
            // 
            this.FlagsColumn.AspectName = "imgurl";
            this.FlagsColumn.Text = "Flags";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.intCheck);
            this.GroupBox1.Controls.Add(this.spCheck);
            this.GroupBox1.Controls.Add(this.polCheck);
            this.GroupBox1.Location = new System.Drawing.Point(6, 6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(90, 89);
            this.GroupBox1.TabIndex = 41;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Select chans";
            // 
            // intCheck
            // 
            this.intCheck.AutoSize = true;
            this.intCheck.Location = new System.Drawing.Point(6, 19);
            this.intCheck.Name = "intCheck";
            this.intCheck.Size = new System.Drawing.Size(47, 17);
            this.intCheck.TabIndex = 20;
            this.intCheck.Text = "/int/";
            this.intCheck.UseVisualStyleBackColor = true;
            this.intCheck.CheckedChanged += new System.EventHandler(this.intCheck_CheckedChanged);
            // 
            // spCheck
            // 
            this.spCheck.AutoSize = true;
            this.spCheck.Location = new System.Drawing.Point(6, 65);
            this.spCheck.Name = "spCheck";
            this.spCheck.Size = new System.Drawing.Size(47, 17);
            this.spCheck.TabIndex = 22;
            this.spCheck.Text = "/sp/";
            this.spCheck.UseVisualStyleBackColor = true;
            this.spCheck.CheckedChanged += new System.EventHandler(this.spCheck_CheckedChanged);
            // 
            // polCheck
            // 
            this.polCheck.AutoSize = true;
            this.polCheck.Location = new System.Drawing.Point(6, 42);
            this.polCheck.Name = "polCheck";
            this.polCheck.Size = new System.Drawing.Size(50, 17);
            this.polCheck.TabIndex = 21;
            this.polCheck.Text = "/pol/";
            this.polCheck.UseVisualStyleBackColor = true;
            this.polCheck.CheckedChanged += new System.EventHandler(this.polCheck_CheckedChanged);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar1.Location = new System.Drawing.Point(6, 409);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(853, 23);
            this.ProgressBar1.TabIndex = 38;
            // 
            // PopUpMenu
            // 
            this.PopUpMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.CollapseAllToolStripMenuItem,
            this.ToolStripSeparator1,
            this.CopyFlagToClipboardToolStripMenuItem,
            this.SaveFlagToolStripMenuItem,
            this.ToolStripSeparator2,
            this.CopyLinkToolStripMenuItem});
            this.PopUpMenu.Name = "PopUpMenu";
            this.PopUpMenu.ShowImageMargin = false;
            this.PopUpMenu.Size = new System.Drawing.Size(168, 126);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.ToolStripMenuItem1.Text = "Expand All";
            // 
            // CollapseAllToolStripMenuItem
            // 
            this.CollapseAllToolStripMenuItem.Name = "CollapseAllToolStripMenuItem";
            this.CollapseAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.CollapseAllToolStripMenuItem.Text = "Collapse All";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // CopyFlagToClipboardToolStripMenuItem
            // 
            this.CopyFlagToClipboardToolStripMenuItem.Name = "CopyFlagToClipboardToolStripMenuItem";
            this.CopyFlagToClipboardToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.CopyFlagToClipboardToolStripMenuItem.Text = "Copy flag to clipboard";
            // 
            // SaveFlagToolStripMenuItem
            // 
            this.SaveFlagToolStripMenuItem.Name = "SaveFlagToolStripMenuItem";
            this.SaveFlagToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.SaveFlagToolStripMenuItem.Text = "Save flag...";
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // CopyLinkToolStripMenuItem
            // 
            this.CopyLinkToolStripMenuItem.Name = "CopyLinkToolStripMenuItem";
            this.CopyLinkToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.CopyLinkToolStripMenuItem.Text = "Copy link";
            // 
            // StatusText
            // 
            this.StatusText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusText.Location = new System.Drawing.Point(6, 121);
            this.StatusText.Multiline = true;
            this.StatusText.Name = "StatusText";
            this.StatusText.ReadOnly = true;
            this.StatusText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.StatusText.Size = new System.Drawing.Size(363, 282);
            this.StatusText.TabIndex = 39;
            // 
            // BackgroundWorker1
            // 
            this.BackgroundWorker1.WorkerReportsProgress = true;
            this.BackgroundWorker1.WorkerSupportsCancellation = true;
            this.BackgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.BackgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            this.BackgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.Label2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.GroupBox1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.StatusText);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.ProgressBar1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.TreeListView1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.Label1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.GroupBox2);
            this.toolStripContainer1.ContentPanel.Padding = new System.Windows.Forms.Padding(3);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(865, 438);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(865, 487);
            this.toolStripContainer1.TabIndex = 56;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.actionMenu,
            this.aboutMenu,
            this.flagsMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(865, 24);
            this.menuStrip1.TabIndex = 56;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMenuItem,
            this.subtractMenuItem,
            this.saveMenuItem,
            this.toolStripSeparator3,
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // loadMenuItem
            // 
            this.loadMenuItem.Image = global::FlagMiner.Properties.Resources.open;
            this.loadMenuItem.Name = "loadMenuItem";
            this.loadMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.loadMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loadMenuItem.Text = "&Load...";
            this.loadMenuItem.Click += new System.EventHandler(this.loadbutt_Click);
            // 
            // subtractMenuItem
            // 
            this.subtractMenuItem.Image = global::FlagMiner.Properties.Resources.subtract;
            this.subtractMenuItem.Name = "subtractMenuItem";
            this.subtractMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.subtractMenuItem.Size = new System.Drawing.Size(161, 22);
            this.subtractMenuItem.Text = "S&ubtract...";
            this.subtractMenuItem.Click += new System.EventHandler(this.subtractButt_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Image = global::FlagMiner.Properties.Resources.save;
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveMenuItem.Text = "&Save as...";
            this.saveMenuItem.Click += new System.EventHandler(this.savebutt_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(158, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.CloseForm);
            // 
            // actionMenu
            // 
            this.actionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mineMenuItem,
            this.parseMenuItem,
            this.toolStripSeparator6,
            this.abortMenuItem,
            this.toolStripSeparator4,
            this.copyMenuItem,
            this.toolStripSeparator5,
            this.settingsMenuItem});
            this.actionMenu.Name = "actionMenu";
            this.actionMenu.Size = new System.Drawing.Size(54, 20);
            this.actionMenu.Text = "&Action";
            // 
            // mineMenuItem
            // 
            this.mineMenuItem.Image = global::FlagMiner.Properties.Resources.parse;
            this.mineMenuItem.Name = "mineMenuItem";
            this.mineMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mineMenuItem.Size = new System.Drawing.Size(269, 22);
            this.mineMenuItem.Text = "&Mine the archives";
            this.mineMenuItem.Click += new System.EventHandler(this.parseBtn_Click);
            // 
            // parseMenuItem
            // 
            this.parseMenuItem.Image = global::FlagMiner.Properties.Resources.parse;
            this.parseMenuItem.Name = "parseMenuItem";
            this.parseMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.parseMenuItem.Size = new System.Drawing.Size(269, 22);
            this.parseMenuItem.Text = "&Parse flag dump...";
            this.parseMenuItem.Click += new System.EventHandler(this.importbutt_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(266, 6);
            // 
            // abortMenuItem
            // 
            this.abortMenuItem.Enabled = false;
            this.abortMenuItem.Image = global::FlagMiner.Properties.Resources.abort;
            this.abortMenuItem.Name = "abortMenuItem";
            this.abortMenuItem.Size = new System.Drawing.Size(269, 22);
            this.abortMenuItem.Text = "&Abort current action";
            this.abortMenuItem.Click += new System.EventHandler(this.AbortButt_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(266, 6);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Image = global::FlagMiner.Properties.Resources.copy;
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyMenuItem.Size = new System.Drawing.Size(269, 22);
            this.copyMenuItem.Text = "&Copy flag dump to clipboard";
            this.copyMenuItem.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(266, 6);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Image = global::FlagMiner.Properties.Resources.opt;
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.settingsMenuItem.Size = new System.Drawing.Size(269, 22);
            this.settingsMenuItem.Text = "Ad&vaced settings...";
            this.settingsMenuItem.Click += new System.EventHandler(this.optButt_Click);
            // 
            // aboutMenu
            // 
            this.aboutMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutMenu.Size = new System.Drawing.Size(24, 20);
            this.aboutMenu.Text = "&?";
            this.aboutMenu.Click += new System.EventHandler(this.aboutButt_Click);
            // 
            // flagsMenu
            // 
            this.flagsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkMenuItem,
            this.purgeMenuItem,
            this.toolStripSeparator13,
            this.clearmenuItem});
            this.flagsMenu.Name = "flagsMenu";
            this.flagsMenu.Size = new System.Drawing.Size(46, 20);
            this.flagsMenu.Text = "F&lags";
            // 
            // checkMenuItem
            // 
            this.checkMenuItem.Image = global::FlagMiner.Properties.Resources.check;
            this.checkMenuItem.Name = "checkMenuItem";
            this.checkMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.checkMenuItem.Size = new System.Drawing.Size(126, 22);
            this.checkMenuItem.Text = "&Check";
            this.checkMenuItem.Click += new System.EventHandler(this.checkbutt_Click);
            // 
            // purgeMenuItem
            // 
            this.purgeMenuItem.Image = global::FlagMiner.Properties.Resources.purge;
            this.purgeMenuItem.Name = "purgeMenuItem";
            this.purgeMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.purgeMenuItem.Size = new System.Drawing.Size(126, 22);
            this.purgeMenuItem.Text = "&Purge";
            this.purgeMenuItem.Click += new System.EventHandler(this.purgebutt_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(123, 6);
            // 
            // clearmenuItem
            // 
            this.clearmenuItem.Image = global::FlagMiner.Properties.Resources.clear;
            this.clearmenuItem.Name = "clearmenuItem";
            this.clearmenuItem.Size = new System.Drawing.Size(126, 22);
            this.clearmenuItem.Text = "&Clear all";
            this.clearmenuItem.Click += new System.EventHandler(this.clearbutt_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripButton,
            this.subtractToolStripButton,
            this.SaveToolStripButton,
            this.toolStripSeparator7,
            this.toolStripSplitButton1,
            this.abortToolStripButton,
            this.toolStripSeparator8,
            this.settingsToolStripButton,
            this.toolStripSeparator9,
            this.copyToolStripButton,
            this.toolStripSeparator11,
            this.checkToolStripButton,
            this.purgeToolStripButton,
            this.toolStripSeparator12,
            this.clearToolStripButton,
            this.toolStripSeparator10,
            this.copyFlagToolStripButton,
            this.saveFlagToolStripButton,
            this.copyLinkToolStripButton,
            this.collapseToolStripButton,
            this.expandToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 24);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(865, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 55;
            // 
            // loadToolStripButton
            // 
            this.loadToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadToolStripButton.Image = global::FlagMiner.Properties.Resources.open;
            this.loadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadToolStripButton.Name = "loadToolStripButton";
            this.loadToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.loadToolStripButton.Text = "toolStripButton1";
            this.loadToolStripButton.ToolTipText = "Load file...";
            this.loadToolStripButton.Click += new System.EventHandler(this.loadbutt_Click);
            // 
            // subtractToolStripButton
            // 
            this.subtractToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.subtractToolStripButton.Image = global::FlagMiner.Properties.Resources.subtract;
            this.subtractToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.subtractToolStripButton.Name = "subtractToolStripButton";
            this.subtractToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.subtractToolStripButton.Text = "toolStripButton2";
            this.subtractToolStripButton.ToolTipText = "Subtract file";
            this.subtractToolStripButton.Click += new System.EventHandler(this.subtractButt_Click);
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToolStripButton.Image = global::FlagMiner.Properties.Resources.save;
            this.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SaveToolStripButton.Text = "toolStripButton3";
            this.SaveToolStripButton.ToolTipText = "Save to file...";
            this.SaveToolStripButton.Click += new System.EventHandler(this.savebutt_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mineToolStripMenuItem,
            this.parseToolStripMenuItem});
            this.toolStripSplitButton1.Image = global::FlagMiner.Properties.Resources.parse;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            // 
            // mineToolStripMenuItem
            // 
            this.mineToolStripMenuItem.Image = global::FlagMiner.Properties.Resources.parse;
            this.mineToolStripMenuItem.Name = "mineToolStripMenuItem";
            this.mineToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.mineToolStripMenuItem.Text = "Mine the archives";
            this.mineToolStripMenuItem.Click += new System.EventHandler(this.parseBtn_Click);
            // 
            // parseToolStripMenuItem
            // 
            this.parseToolStripMenuItem.Image = global::FlagMiner.Properties.Resources.parse;
            this.parseToolStripMenuItem.Name = "parseToolStripMenuItem";
            this.parseToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.parseToolStripMenuItem.Text = "Parse flag dump...";
            this.parseToolStripMenuItem.Click += new System.EventHandler(this.importbutt_Click);
            // 
            // abortToolStripButton
            // 
            this.abortToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.abortToolStripButton.Enabled = false;
            this.abortToolStripButton.Image = global::FlagMiner.Properties.Resources.abort;
            this.abortToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abortToolStripButton.Name = "abortToolStripButton";
            this.abortToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.abortToolStripButton.Text = "toolStripButton4";
            this.abortToolStripButton.ToolTipText = "Abort";
            this.abortToolStripButton.Click += new System.EventHandler(this.AbortButt_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsToolStripButton.Image = global::FlagMiner.Properties.Resources.opt;
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.settingsToolStripButton.Text = "toolStripButton5";
            this.settingsToolStripButton.ToolTipText = "Advanced settings";
            this.settingsToolStripButton.Click += new System.EventHandler(this.optButt_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripButton.Image = global::FlagMiner.Properties.Resources.copy;
            this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyToolStripButton.Text = "toolStripButton6";
            this.copyToolStripButton.ToolTipText = "Copy dump to clipboard";
            this.copyToolStripButton.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // checkToolStripButton
            // 
            this.checkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.checkToolStripButton.Enabled = false;
            this.checkToolStripButton.Image = global::FlagMiner.Properties.Resources.check;
            this.checkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkToolStripButton.Name = "checkToolStripButton";
            this.checkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.checkToolStripButton.Text = "toolStripButton8";
            this.checkToolStripButton.ToolTipText = "Check flags";
            this.checkToolStripButton.Click += new System.EventHandler(this.checkbutt_Click);
            // 
            // purgeToolStripButton
            // 
            this.purgeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.purgeToolStripButton.Enabled = false;
            this.purgeToolStripButton.Image = global::FlagMiner.Properties.Resources.purge;
            this.purgeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.purgeToolStripButton.Name = "purgeToolStripButton";
            this.purgeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.purgeToolStripButton.Text = "toolStripButton9";
            this.purgeToolStripButton.ToolTipText = "Purge flags";
            this.purgeToolStripButton.Click += new System.EventHandler(this.purgebutt_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // clearToolStripButton
            // 
            this.clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearToolStripButton.Image = global::FlagMiner.Properties.Resources.clear;
            this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolStripButton.Name = "clearToolStripButton";
            this.clearToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.clearToolStripButton.Text = "toolStripButton10";
            this.clearToolStripButton.ToolTipText = "Clear all";
            this.clearToolStripButton.Click += new System.EventHandler(this.clearbutt_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // copyFlagToolStripButton
            // 
            this.copyFlagToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyFlagToolStripButton.Enabled = false;
            this.copyFlagToolStripButton.Image = global::FlagMiner.Properties.Resources.copyflag;
            this.copyFlagToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyFlagToolStripButton.Name = "copyFlagToolStripButton";
            this.copyFlagToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyFlagToolStripButton.Text = "toolStripButton7";
            this.copyFlagToolStripButton.ToolTipText = "Copy flag to clipboard";
            this.copyFlagToolStripButton.Click += new System.EventHandler(this.CopyImageHandler);
            // 
            // saveFlagToolStripButton
            // 
            this.saveFlagToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveFlagToolStripButton.Enabled = false;
            this.saveFlagToolStripButton.Image = global::FlagMiner.Properties.Resources.download;
            this.saveFlagToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFlagToolStripButton.Name = "saveFlagToolStripButton";
            this.saveFlagToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveFlagToolStripButton.Text = "toolStripButton11";
            this.saveFlagToolStripButton.ToolTipText = "Save flag...";
            this.saveFlagToolStripButton.Click += new System.EventHandler(this.SaveImageHandler);
            // 
            // copyLinkToolStripButton
            // 
            this.copyLinkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyLinkToolStripButton.Enabled = false;
            this.copyLinkToolStripButton.Image = global::FlagMiner.Properties.Resources.link;
            this.copyLinkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyLinkToolStripButton.Name = "copyLinkToolStripButton";
            this.copyLinkToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyLinkToolStripButton.Text = "toolStripButton12";
            this.copyLinkToolStripButton.ToolTipText = "Copy link to clipboard";
            this.copyLinkToolStripButton.Click += new System.EventHandler(this.CopyLinkHandler);
            // 
            // collapseToolStripButton
            // 
            this.collapseToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.collapseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.collapseToolStripButton.Image = global::FlagMiner.Properties.Resources.collapse;
            this.collapseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.collapseToolStripButton.Name = "collapseToolStripButton";
            this.collapseToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.collapseToolStripButton.Text = "toolStripButton13";
            this.collapseToolStripButton.ToolTipText = "Collapse all";
            this.collapseToolStripButton.Click += new System.EventHandler(this.CollapseHandler);
            // 
            // expandToolStripButton
            // 
            this.expandToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.expandToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.expandToolStripButton.Image = global::FlagMiner.Properties.Resources.expand;
            this.expandToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.expandToolStripButton.Name = "expandToolStripButton";
            this.expandToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.expandToolStripButton.Text = "toolStripButton14";
            this.expandToolStripButton.ToolTipText = "Expand all";
            this.expandToolStripButton.Click += new System.EventHandler(this.ExpandHandler);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 487);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(875, 500);
            this.Name = "Form1";
            this.Text = "Flag Miner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreeListView1)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.PopUpMenu.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

		}

        private BrightIdeasSoftware.OLVColumn ThreadColumn;
        private ImageList ImageList1;
        private SaveFileDialog SaveFileDialog1;
        private OpenFileDialog OpenXmlDialog;
        private SaveFileDialog SaveXmlDialog;
        private ToolTip ToolTip1;
        private ToolTip ToolTip2;
        private Label Label2;
        private Label Label1;
        private GroupBox GroupBox2;
        private BrightIdeasSoftware.OLVColumn TitleColumn;
        private BrightIdeasSoftware.OLVColumn FlagsColumn;
        private GroupBox GroupBox1;
        private ProgressBar ProgressBar1;
        private ContextMenuStrip PopUpMenu;
        private ToolStripSeparator ToolStripSeparator1;
        private ToolStripMenuItem CopyFlagToClipboardToolStripMenuItem;
        private ToolStripMenuItem SaveFlagToolStripMenuItem;
        private ToolStripSeparator ToolStripSeparator2;
        private ToolStripMenuItem CopyLinkToolStripMenuItem;
        private TextBox StatusText;
        private System.ComponentModel.BackgroundWorker BackgroundWorker1;
        private ToolStripMenuItem CollapseAllToolStripMenuItem;
        private ToolStripMenuItem ToolStripMenuItem1;
        private CheckBox polCheck;
        private CheckBox spCheck;
        private CheckBox intCheck;
        private BrightIdeasSoftware.TreeListView TreeListView1;
        private DateTimePicker DateTimePicker1;
        private CheckBox CheckBox1;
        private CheckBox CheckBox2;
        private System.ComponentModel.BackgroundWorker BackgroundWorker2;

        #endregion

        private ToolStripContainer toolStripContainer1;
        private ToolStrip toolStrip2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem loadMenuItem;
        private ToolStripMenuItem subtractMenuItem;
        private ToolStripMenuItem saveMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem actionMenu;
        private ToolStripMenuItem mineMenuItem;
        private ToolStripMenuItem parseMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem abortMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem copyMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem settingsMenuItem;
        private ToolStripMenuItem aboutMenu;
        private ToolStripButton loadToolStripButton;
        private ToolStripButton subtractToolStripButton;
        private ToolStripButton SaveToolStripButton;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripButton abortToolStripButton;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton settingsToolStripButton;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripButton copyToolStripButton;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripButton checkToolStripButton;
        private ToolStripButton purgeToolStripButton;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripButton clearToolStripButton;
        private ToolStripMenuItem flagsMenu;
        private ToolStripMenuItem checkMenuItem;
        private ToolStripMenuItem purgeMenuItem;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripButton copyFlagToolStripButton;
        private ToolStripButton saveFlagToolStripButton;
        private ToolStripButton copyLinkToolStripButton;
        private ToolStripButton collapseToolStripButton;
        private ToolStripButton expandToolStripButton;
        private ToolStripMenuItem mineToolStripMenuItem;
        private ToolStripMenuItem parseToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripMenuItem clearmenuItem;
	}
}
