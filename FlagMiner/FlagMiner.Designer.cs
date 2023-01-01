
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlagMiner
{
	partial class FlagMiner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlagMiner));
            this.ThreadColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ThreadParserBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.FlegImageList = new System.Windows.Forms.ImageList(this.components);
            this.ExclusionListCheckBox = new System.Windows.Forms.CheckBox();
            this.ExclusionDateCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveFlagDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenXmlDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveXmlDialog = new System.Windows.Forms.SaveFileDialog();
            this.Label2 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ExclusionDatePicker = new System.Windows.Forms.DateTimePicker();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.TitleColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FlegTreeListView = new BrightIdeasSoftware.TreeListView();
            this.FlagsColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.intCheck = new System.Windows.Forms.CheckBox();
            this.spCheck = new System.Windows.Forms.CheckBox();
            this.polCheck = new System.Windows.Forms.CheckBox();
            this.ParsingProgressBar = new System.Windows.Forms.ProgressBar();
            this.PopUpMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExpandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyFlagToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusText = new System.Windows.Forms.TextBox();
            this.MinerBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
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
            this.deleteCheckedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
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
            ((System.ComponentModel.ISupportInitialize)(this.FlegTreeListView)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.PopUpMenu.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.TopMenuStrip.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
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
            // ThreadParserBackgroundWorker
            // 
            this.ThreadParserBackgroundWorker.WorkerReportsProgress = true;
            this.ThreadParserBackgroundWorker.WorkerSupportsCancellation = true;
            this.ThreadParserBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ThreadParserBackgroundWorker_DoWork);
            this.ThreadParserBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MinerBackgroundWorker_ProgressChanged);
            this.ThreadParserBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MinerBackgroundWorker_RunWorkerCompleted);
            // 
            // FlegImageList
            // 
            this.FlegImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.FlegImageList.ImageSize = new System.Drawing.Size(11, 11);
            this.FlegImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ExclusionListCheckBox
            // 
            this.ExclusionListCheckBox.AutoSize = true;
            this.ExclusionListCheckBox.Checked = true;
            this.ExclusionListCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExclusionListCheckBox.Location = new System.Drawing.Point(6, 19);
            this.ExclusionListCheckBox.Name = "ExclusionListCheckBox";
            this.ExclusionListCheckBox.Size = new System.Drawing.Size(139, 17);
            this.ExclusionListCheckBox.TabIndex = 38;
            this.ExclusionListCheckBox.Text = "exclude previously seen";
            this.ExclusionListCheckBox.UseVisualStyleBackColor = true;
            this.ExclusionListCheckBox.CheckedChanged += new System.EventHandler(this.ExclusionListCheckBox_CheckedChanged);
            // 
            // ExclusionDateCheckBox
            // 
            this.ExclusionDateCheckBox.AutoSize = true;
            this.ExclusionDateCheckBox.Location = new System.Drawing.Point(6, 42);
            this.ExclusionDateCheckBox.Name = "ExclusionDateCheckBox";
            this.ExclusionDateCheckBox.Size = new System.Drawing.Size(118, 17);
            this.ExclusionDateCheckBox.TabIndex = 37;
            this.ExclusionDateCheckBox.Text = "archived after date:";
            this.ExclusionDateCheckBox.UseVisualStyleBackColor = true;
            this.ExclusionDateCheckBox.CheckedChanged += new System.EventHandler(this.ExclusionDateCheckBox_CheckedChanged);
            // 
            // SaveFlagDialog
            // 
            this.SaveFlagDialog.Title = "Save flag...";
            // 
            // OpenXmlDialog
            // 
            this.OpenXmlDialog.FileName = "SavedTree.xml";
            this.OpenXmlDialog.Filter = "Xml files|*.xml|All files|*.*";
            this.OpenXmlDialog.Multiselect = true;
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
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(6, 105);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(41, 13);
            this.StatusLabel.TabIndex = 45;
            this.StatusLabel.Text = "status: ";
            // 
            // ExclusionDatePicker
            // 
            this.ExclusionDatePicker.CustomFormat = "yyyy/MM/dd HH:mm";
            this.ExclusionDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ExclusionDatePicker.Location = new System.Drawing.Point(6, 62);
            this.ExclusionDatePicker.Name = "ExclusionDatePicker";
            this.ExclusionDatePicker.Size = new System.Drawing.Size(159, 20);
            this.ExclusionDatePicker.TabIndex = 34;
            this.ExclusionDatePicker.ValueChanged += new System.EventHandler(this.ExclusionDatePicker_ValueChanged);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.ExclusionListCheckBox);
            this.GroupBox2.Controls.Add(this.ExclusionDateCheckBox);
            this.GroupBox2.Controls.Add(this.ExclusionDatePicker);
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
            // FlegTreeListView
            // 
            this.FlegTreeListView.AllColumns.Add(this.FlagsColumn);
            this.FlegTreeListView.AllColumns.Add(this.TitleColumn);
            this.FlegTreeListView.AllColumns.Add(this.ThreadColumn);
            this.FlegTreeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlegTreeListView.CellEditUseWholeCell = false;
            this.FlegTreeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FlagsColumn,
            this.TitleColumn,
            this.ThreadColumn});
            this.FlegTreeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.FlegTreeListView.FullRowSelect = true;
            this.FlegTreeListView.HideSelection = false;
            this.FlegTreeListView.Location = new System.Drawing.Point(375, 8);
            this.FlegTreeListView.Name = "FlegTreeListView";
            this.FlegTreeListView.ShowGroups = false;
            this.FlegTreeListView.Size = new System.Drawing.Size(484, 395);
            this.FlegTreeListView.SmallImageList = this.FlegImageList;
            this.FlegTreeListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.FlegTreeListView.TabIndex = 44;
            this.FlegTreeListView.TintSortColumn = true;
            this.FlegTreeListView.UseCompatibleStateImageBehavior = false;
            this.FlegTreeListView.UseHotControls = false;
            this.FlegTreeListView.UseHyperlinks = true;
            this.FlegTreeListView.View = System.Windows.Forms.View.Details;
            this.FlegTreeListView.VirtualMode = true;
            this.FlegTreeListView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.Olv_CellRightClick);
            this.FlegTreeListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.FormatRow_EventHandler);
            this.FlegTreeListView.SelectionChanged += new System.EventHandler(this.FlegTreeListView_SelectionChanged);
            // 
            // FlagsColumn
            // 
            this.FlagsColumn.AspectName = "";
            this.FlagsColumn.ImageAspectName = "imgurl";
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
            this.intCheck.CheckedChanged += new System.EventHandler(this.IntCheck_CheckedChanged);
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
            this.spCheck.CheckedChanged += new System.EventHandler(this.SpCheck_CheckedChanged);
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
            this.polCheck.CheckedChanged += new System.EventHandler(this.PolCheck_CheckedChanged);
            // 
            // ParsingProgressBar
            // 
            this.ParsingProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ParsingProgressBar.Location = new System.Drawing.Point(6, 409);
            this.ParsingProgressBar.Name = "ParsingProgressBar";
            this.ParsingProgressBar.Size = new System.Drawing.Size(853, 23);
            this.ParsingProgressBar.TabIndex = 38;
            // 
            // PopUpMenu
            // 
            this.PopUpMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpandAllToolStripMenuItem,
            this.CollapseAllToolStripMenuItem,
            this.ToolStripSeparator1,
            this.CopyFlagToClipboardToolStripMenuItem,
            this.SaveFlagToolStripMenuItem,
            this.ToolStripSeparator2,
            this.CopyLinkToolStripMenuItem});
            this.PopUpMenu.Name = "PopUpMenu";
            this.PopUpMenu.ShowImageMargin = false;
            this.PopUpMenu.Size = new System.Drawing.Size(168, 148);
            // 
            // ExpandAllToolStripMenuItem
            // 
            this.ExpandAllToolStripMenuItem.Name = "ExpandAllToolStripMenuItem";
            this.ExpandAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.ExpandAllToolStripMenuItem.Text = "Expand All";
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
            // MinerBackgroundWorker
            // 
            this.MinerBackgroundWorker.WorkerReportsProgress = true;
            this.MinerBackgroundWorker.WorkerSupportsCancellation = true;
            this.MinerBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MinerBackgroundWorker_DoWork);
            this.MinerBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MinerBackgroundWorker_ProgressChanged);
            this.MinerBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MinerBackgroundWorker_RunWorkerCompleted);
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
            this.toolStripContainer1.ContentPanel.Controls.Add(this.ParsingProgressBar);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.FlegTreeListView);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.StatusLabel);
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
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.TopMenuStrip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MainToolStrip);
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.actionMenu,
            this.aboutMenu,
            this.flagsMenu});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Size = new System.Drawing.Size(865, 24);
            this.TopMenuStrip.TabIndex = 56;
            this.TopMenuStrip.Text = "menuStrip1";
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
            this.loadMenuItem.Click += new System.EventHandler(this.Loadbutt_Click);
            // 
            // subtractMenuItem
            // 
            this.subtractMenuItem.Image = global::FlagMiner.Properties.Resources.subtract;
            this.subtractMenuItem.Name = "subtractMenuItem";
            this.subtractMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.subtractMenuItem.Size = new System.Drawing.Size(161, 22);
            this.subtractMenuItem.Text = "S&ubtract...";
            this.subtractMenuItem.Click += new System.EventHandler(this.SubtractButt_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Image = global::FlagMiner.Properties.Resources.save;
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveMenuItem.Text = "&Save as...";
            this.saveMenuItem.Click += new System.EventHandler(this.Savebutt_Click);
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
            this.mineMenuItem.Click += new System.EventHandler(this.ParseBtn_Click);
            // 
            // parseMenuItem
            // 
            this.parseMenuItem.Image = global::FlagMiner.Properties.Resources.parse;
            this.parseMenuItem.Name = "parseMenuItem";
            this.parseMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.parseMenuItem.Size = new System.Drawing.Size(269, 22);
            this.parseMenuItem.Text = "&Parse flag dump...";
            this.parseMenuItem.Click += new System.EventHandler(this.Importbutt_Click);
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
            this.settingsMenuItem.Click += new System.EventHandler(this.OptButt_Click);
            // 
            // aboutMenu
            // 
            this.aboutMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutMenu.Size = new System.Drawing.Size(24, 20);
            this.aboutMenu.Text = "&?";
            this.aboutMenu.Click += new System.EventHandler(this.AboutButt_Click);
            // 
            // flagsMenu
            // 
            this.flagsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkMenuItem,
            this.purgeMenuItem,
            this.toolStripSeparator13,
            this.clearmenuItem,
            this.deleteCheckedToolStripMenuItem});
            this.flagsMenu.Name = "flagsMenu";
            this.flagsMenu.Size = new System.Drawing.Size(46, 20);
            this.flagsMenu.Text = "F&lags";
            // 
            // checkMenuItem
            // 
            this.checkMenuItem.Image = global::FlagMiner.Properties.Resources.check;
            this.checkMenuItem.Name = "checkMenuItem";
            this.checkMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.checkMenuItem.Size = new System.Drawing.Size(156, 22);
            this.checkMenuItem.Text = "&Check";
            this.checkMenuItem.Click += new System.EventHandler(this.Checkbutt_Click);
            // 
            // purgeMenuItem
            // 
            this.purgeMenuItem.Image = global::FlagMiner.Properties.Resources.purge;
            this.purgeMenuItem.Name = "purgeMenuItem";
            this.purgeMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.purgeMenuItem.Size = new System.Drawing.Size(156, 22);
            this.purgeMenuItem.Text = "&Purge";
            this.purgeMenuItem.Click += new System.EventHandler(this.Purgebutt_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(153, 6);
            // 
            // clearmenuItem
            // 
            this.clearmenuItem.Image = global::FlagMiner.Properties.Resources.clear;
            this.clearmenuItem.Name = "clearmenuItem";
            this.clearmenuItem.Size = new System.Drawing.Size(156, 22);
            this.clearmenuItem.Text = "Clear &all";
            this.clearmenuItem.Click += new System.EventHandler(this.Clearbutt_Click);
            // 
            // deleteCheckedToolStripMenuItem
            // 
            this.deleteCheckedToolStripMenuItem.Name = "deleteCheckedToolStripMenuItem";
            this.deleteCheckedToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.deleteCheckedToolStripMenuItem.Text = "&Delete Checked";
            this.deleteCheckedToolStripMenuItem.Click += new System.EventHandler(this.DeleteCheckedToolStripMenuItem_Click);
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.MainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainToolStrip.Size = new System.Drawing.Size(865, 25);
            this.MainToolStrip.Stretch = true;
            this.MainToolStrip.TabIndex = 55;
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
            this.loadToolStripButton.Click += new System.EventHandler(this.Loadbutt_Click);
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
            this.subtractToolStripButton.Click += new System.EventHandler(this.SubtractButt_Click);
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
            this.SaveToolStripButton.Click += new System.EventHandler(this.Savebutt_Click);
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
            this.mineToolStripMenuItem.Click += new System.EventHandler(this.ParseBtn_Click);
            // 
            // parseToolStripMenuItem
            // 
            this.parseToolStripMenuItem.Image = global::FlagMiner.Properties.Resources.parse;
            this.parseToolStripMenuItem.Name = "parseToolStripMenuItem";
            this.parseToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.parseToolStripMenuItem.Text = "Parse flag dump...";
            this.parseToolStripMenuItem.Click += new System.EventHandler(this.Importbutt_Click);
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
            this.settingsToolStripButton.Click += new System.EventHandler(this.OptButt_Click);
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
            this.checkToolStripButton.Click += new System.EventHandler(this.Checkbutt_Click);
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
            this.purgeToolStripButton.Click += new System.EventHandler(this.Purgebutt_Click);
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
            this.clearToolStripButton.Click += new System.EventHandler(this.Clearbutt_Click);
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
            // FlagMiner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 487);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.TopMenuStrip;
            this.MinimumSize = new System.Drawing.Size(875, 500);
            this.Name = "FlagMiner";
            this.Text = "Flag Miner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlagMiner_FormClosing);
            this.Load += new System.EventHandler(this.FlagMiner_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlegTreeListView)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.PopUpMenu.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.ResumeLayout(false);

		}

        private BrightIdeasSoftware.OLVColumn ThreadColumn;
        private ImageList FlegImageList;
        private SaveFileDialog SaveFlagDialog;
        private OpenFileDialog OpenXmlDialog;
        private SaveFileDialog SaveXmlDialog;
        private Label Label2;
        private Label StatusLabel;
        private GroupBox GroupBox2;
        private BrightIdeasSoftware.OLVColumn TitleColumn;
        private BrightIdeasSoftware.OLVColumn FlagsColumn;
        private GroupBox GroupBox1;
        private ProgressBar ParsingProgressBar;
        private ContextMenuStrip PopUpMenu;
        private ToolStripSeparator ToolStripSeparator1;
        private ToolStripMenuItem CopyFlagToClipboardToolStripMenuItem;
        private ToolStripMenuItem SaveFlagToolStripMenuItem;
        private ToolStripSeparator ToolStripSeparator2;
        private ToolStripMenuItem CopyLinkToolStripMenuItem;
        private TextBox StatusText;
        private System.ComponentModel.BackgroundWorker MinerBackgroundWorker;
        private ToolStripMenuItem CollapseAllToolStripMenuItem;
        private ToolStripMenuItem ExpandAllToolStripMenuItem;
        private CheckBox polCheck;
        private CheckBox spCheck;
        private CheckBox intCheck;
        private BrightIdeasSoftware.TreeListView FlegTreeListView;
        private DateTimePicker ExclusionDatePicker;
        private CheckBox ExclusionDateCheckBox;
        private CheckBox ExclusionListCheckBox;
        private System.ComponentModel.BackgroundWorker ThreadParserBackgroundWorker;

        #endregion

        private ToolStripContainer toolStripContainer1;
        private ToolStrip MainToolStrip;
        private MenuStrip TopMenuStrip;
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
        private ToolStripMenuItem deleteCheckedToolStripMenuItem;
    }
}
