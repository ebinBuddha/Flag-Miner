<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.parseBtn = New System.Windows.Forms.Button()
        Me.PopUpMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyFlagToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFlagToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbortButt = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.StatusText = New System.Windows.Forms.TextBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.clearbutt = New System.Windows.Forms.Button()
        Me.intCheck = New System.Windows.Forms.CheckBox()
        Me.polCheck = New System.Windows.Forms.CheckBox()
        Me.spCheck = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CopyBtn = New System.Windows.Forms.Button()
        Me.TreeListView1 = New BrightIdeasSoftware.TreeListView()
        Me.FlagsColumn = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.TitleColumn = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.ThreadColumn = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.loadbutt = New System.Windows.Forms.Button()
        Me.savebutt = New System.Windows.Forms.Button()
        Me.purgebutt = New System.Windows.Forms.Button()
        Me.checkbutt = New System.Windows.Forms.Button()
        Me.importbutt = New System.Windows.Forms.Button()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optButt = New System.Windows.Forms.Button()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.aboutButt = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenXmlDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveXmlDialog = New System.Windows.Forms.SaveFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.subtractButt = New System.Windows.Forms.Button()
        Me.PopUpMenu.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.TreeListView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'parseBtn
        '
        Me.parseBtn.Location = New System.Drawing.Point(108, 77)
        Me.parseBtn.Name = "parseBtn"
        Me.parseBtn.Size = New System.Drawing.Size(77, 24)
        Me.parseBtn.TabIndex = 1
        Me.parseBtn.Text = "Mine it!"
        Me.parseBtn.UseVisualStyleBackColor = True
        '
        'PopUpMenu
        '
        Me.PopUpMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.CollapseAllToolStripMenuItem, Me.ToolStripSeparator1, Me.CopyFlagToClipboardToolStripMenuItem, Me.SaveFlagToolStripMenuItem, Me.ToolStripSeparator2, Me.CopyLinkToolStripMenuItem})
        Me.PopUpMenu.Name = "PopUpMenu"
        Me.PopUpMenu.ShowImageMargin = False
        Me.PopUpMenu.Size = New System.Drawing.Size(168, 126)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(167, 22)
        Me.ToolStripMenuItem1.Text = "Expand All"
        '
        'CollapseAllToolStripMenuItem
        '
        Me.CollapseAllToolStripMenuItem.Name = "CollapseAllToolStripMenuItem"
        Me.CollapseAllToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.CollapseAllToolStripMenuItem.Text = "Collapse All"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(164, 6)
        '
        'CopyFlagToClipboardToolStripMenuItem
        '
        Me.CopyFlagToClipboardToolStripMenuItem.Name = "CopyFlagToClipboardToolStripMenuItem"
        Me.CopyFlagToClipboardToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.CopyFlagToClipboardToolStripMenuItem.Text = "Copy flag to clipboard"
        '
        'SaveFlagToolStripMenuItem
        '
        Me.SaveFlagToolStripMenuItem.Name = "SaveFlagToolStripMenuItem"
        Me.SaveFlagToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.SaveFlagToolStripMenuItem.Text = "Save flag..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(164, 6)
        '
        'CopyLinkToolStripMenuItem
        '
        Me.CopyLinkToolStripMenuItem.Name = "CopyLinkToolStripMenuItem"
        Me.CopyLinkToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.CopyLinkToolStripMenuItem.Text = "Copy link"
        '
        'AbortButt
        '
        Me.AbortButt.Enabled = False
        Me.AbortButt.Location = New System.Drawing.Point(191, 77)
        Me.AbortButt.Name = "AbortButt"
        Me.AbortButt.Size = New System.Drawing.Size(74, 24)
        Me.AbortButt.TabIndex = 11
        Me.AbortButt.Text = "Abort"
        Me.AbortButt.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 446)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(363, 23)
        Me.ProgressBar1.TabIndex = 12
        '
        'StatusText
        '
        Me.StatusText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.StatusText.Location = New System.Drawing.Point(12, 125)
        Me.StatusText.Multiline = True
        Me.StatusText.Name = "StatusText"
        Me.StatusText.ReadOnly = True
        Me.StatusText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.StatusText.Size = New System.Drawing.Size(363, 315)
        Me.StatusText.TabIndex = 13
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'clearbutt
        '
        Me.clearbutt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clearbutt.Enabled = False
        Me.clearbutt.Location = New System.Drawing.Point(598, 446)
        Me.clearbutt.Name = "clearbutt"
        Me.clearbutt.Size = New System.Drawing.Size(50, 23)
        Me.clearbutt.TabIndex = 18
        Me.clearbutt.Text = "Clear"
        Me.clearbutt.UseVisualStyleBackColor = True
        '
        'intCheck
        '
        Me.intCheck.AutoSize = True
        Me.intCheck.Location = New System.Drawing.Point(6, 19)
        Me.intCheck.Name = "intCheck"
        Me.intCheck.Size = New System.Drawing.Size(47, 17)
        Me.intCheck.TabIndex = 20
        Me.intCheck.Text = "/int/"
        Me.intCheck.UseVisualStyleBackColor = True
        '
        'polCheck
        '
        Me.polCheck.AutoSize = True
        Me.polCheck.Location = New System.Drawing.Point(6, 42)
        Me.polCheck.Name = "polCheck"
        Me.polCheck.Size = New System.Drawing.Size(50, 17)
        Me.polCheck.TabIndex = 21
        Me.polCheck.Text = "/pol/"
        Me.polCheck.UseVisualStyleBackColor = True
        '
        'spCheck
        '
        Me.spCheck.AutoSize = True
        Me.spCheck.Location = New System.Drawing.Point(6, 65)
        Me.spCheck.Name = "spCheck"
        Me.spCheck.Size = New System.Drawing.Size(47, 17)
        Me.spCheck.TabIndex = 22
        Me.spCheck.Text = "/sp/"
        Me.spCheck.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.intCheck)
        Me.GroupBox1.Controls.Add(Me.spCheck)
        Me.GroupBox1.Controls.Add(Me.polCheck)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(90, 89)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select chans"
        '
        'CopyBtn
        '
        Me.CopyBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CopyBtn.Location = New System.Drawing.Point(381, 446)
        Me.CopyBtn.Name = "CopyBtn"
        Me.CopyBtn.Size = New System.Drawing.Size(99, 23)
        Me.CopyBtn.TabIndex = 24
        Me.CopyBtn.Text = "Copy to clipboard"
        Me.CopyBtn.UseVisualStyleBackColor = True
        '
        'TreeListView1
        '
        Me.TreeListView1.AllColumns.Add(Me.FlagsColumn)
        Me.TreeListView1.AllColumns.Add(Me.TitleColumn)
        Me.TreeListView1.AllColumns.Add(Me.ThreadColumn)
        Me.TreeListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeListView1.CellEditUseWholeCell = False
        Me.TreeListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FlagsColumn, Me.TitleColumn, Me.ThreadColumn})
        Me.TreeListView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.TreeListView1.Location = New System.Drawing.Point(381, 12)
        Me.TreeListView1.Name = "TreeListView1"
        Me.TreeListView1.ShowGroups = False
        Me.TreeListView1.Size = New System.Drawing.Size(477, 428)
        Me.TreeListView1.SmallImageList = Me.ImageList1
        Me.TreeListView1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.TreeListView1.TabIndex = 26
        Me.TreeListView1.TintSortColumn = True
        Me.TreeListView1.UseCompatibleStateImageBehavior = False
        Me.TreeListView1.UseHotControls = False
        Me.TreeListView1.UseHyperlinks = True
        Me.TreeListView1.View = System.Windows.Forms.View.Details
        Me.TreeListView1.VirtualMode = True
        '
        'FlagsColumn
        '
        Me.FlagsColumn.AspectName = "imgurl"
        Me.FlagsColumn.Text = "Flags"
        '
        'TitleColumn
        '
        Me.TitleColumn.AspectName = "title"
        Me.TitleColumn.MinimumWidth = 150
        Me.TitleColumn.Text = "Title"
        Me.TitleColumn.Width = 150
        '
        'ThreadColumn
        '
        Me.ThreadColumn.AspectName = "thread"
        Me.ThreadColumn.Hyperlink = True
        Me.ThreadColumn.MinimumWidth = 150
        Me.ThreadColumn.Text = "Thread"
        Me.ThreadColumn.Width = 150
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(11, 11)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "status: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(59, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 28
        '
        'loadbutt
        '
        Me.loadbutt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.loadbutt.Enabled = False
        Me.loadbutt.Location = New System.Drawing.Point(792, 446)
        Me.loadbutt.Name = "loadbutt"
        Me.loadbutt.Size = New System.Drawing.Size(66, 23)
        Me.loadbutt.TabIndex = 29
        Me.loadbutt.Text = "Load tree"
        Me.loadbutt.UseVisualStyleBackColor = True
        '
        'savebutt
        '
        Me.savebutt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.savebutt.Enabled = False
        Me.savebutt.Location = New System.Drawing.Point(720, 446)
        Me.savebutt.Name = "savebutt"
        Me.savebutt.Size = New System.Drawing.Size(66, 23)
        Me.savebutt.TabIndex = 30
        Me.savebutt.Text = "Save tree"
        Me.savebutt.UseVisualStyleBackColor = True
        '
        'purgebutt
        '
        Me.purgebutt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.purgebutt.Enabled = False
        Me.purgebutt.Location = New System.Drawing.Point(542, 446)
        Me.purgebutt.Name = "purgebutt"
        Me.purgebutt.Size = New System.Drawing.Size(50, 23)
        Me.purgebutt.TabIndex = 31
        Me.purgebutt.Text = "Purge"
        Me.ToolTip2.SetToolTip(Me.purgebutt, "Button is disabled. To enable it, open the Options dialog to properly configure t" & _
        "he folders.")
        Me.purgebutt.UseVisualStyleBackColor = True
        '
        'checkbutt
        '
        Me.checkbutt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.checkbutt.Enabled = False
        Me.checkbutt.Location = New System.Drawing.Point(486, 446)
        Me.checkbutt.Name = "checkbutt"
        Me.checkbutt.Size = New System.Drawing.Size(50, 23)
        Me.checkbutt.TabIndex = 32
        Me.checkbutt.Text = "Check"
        Me.ToolTip1.SetToolTip(Me.checkbutt, "Button is disabled. To enable it, open the Options dialog to properly configure t" & _
        "he folders.")
        Me.checkbutt.UseVisualStyleBackColor = True
        '
        'importbutt
        '
        Me.importbutt.Enabled = False
        Me.importbutt.Location = New System.Drawing.Point(271, 77)
        Me.importbutt.Name = "importbutt"
        Me.importbutt.Size = New System.Drawing.Size(74, 24)
        Me.importbutt.TabIndex = 33
        Me.importbutt.Text = "Parse posts"
        Me.importbutt.UseVisualStyleBackColor = True
        '
        'BackgroundWorker2
        '
        Me.BackgroundWorker2.WorkerReportsProgress = True
        Me.BackgroundWorker2.WorkerSupportsCancellation = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optButt)
        Me.GroupBox2.Controls.Add(Me.CheckBox2)
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox2.Location = New System.Drawing.Point(108, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(267, 59)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "options"
        '
        'optButt
        '
        Me.optButt.Image = Global.FlagMiner.My.Resources.Resources.opt
        Me.optButt.Location = New System.Drawing.Point(243, 35)
        Me.optButt.Name = "optButt"
        Me.optButt.Size = New System.Drawing.Size(24, 24)
        Me.optButt.TabIndex = 35
        Me.optButt.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Location = New System.Drawing.Point(122, 18)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(139, 17)
        Me.CheckBox2.TabIndex = 38
        Me.CheckBox2.Text = "exclude previously seen"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(6, 18)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(118, 17)
        Me.CheckBox1.TabIndex = 37
        Me.CheckBox1.Text = "archived after date:"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(6, 37)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(159, 20)
        Me.DateTimePicker1.TabIndex = 34
        '
        'aboutButt
        '
        Me.aboutButt.Location = New System.Drawing.Point(351, 77)
        Me.aboutButt.Name = "aboutButt"
        Me.aboutButt.Size = New System.Drawing.Size(24, 24)
        Me.aboutButt.TabIndex = 34
        Me.aboutButt.Text = "?"
        Me.aboutButt.UseVisualStyleBackColor = True
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Title = "Save flag..."
        '
        'OpenXmlDialog
        '
        Me.OpenXmlDialog.FileName = "SavedTree.xml"
        Me.OpenXmlDialog.Filter = "Xml files|*.xml|All files|*.*"
        '
        'SaveXmlDialog
        '
        Me.SaveXmlDialog.Filter = "Xml files|*.xml|All files|*.*"
        '
        'subtractButt
        '
        Me.subtractButt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.subtractButt.Enabled = False
        Me.subtractButt.Location = New System.Drawing.Point(654, 446)
        Me.subtractButt.Name = "subtractButt"
        Me.subtractButt.Size = New System.Drawing.Size(60, 23)
        Me.subtractButt.TabIndex = 35
        Me.subtractButt.Text = "Subtract"
        Me.subtractButt.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(870, 481)
        Me.Controls.Add(Me.subtractButt)
        Me.Controls.Add(Me.aboutButt)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.importbutt)
        Me.Controls.Add(Me.checkbutt)
        Me.Controls.Add(Me.purgebutt)
        Me.Controls.Add(Me.savebutt)
        Me.Controls.Add(Me.loadbutt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TreeListView1)
        Me.Controls.Add(Me.CopyBtn)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.clearbutt)
        Me.Controls.Add(Me.StatusText)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.AbortButt)
        Me.Controls.Add(Me.parseBtn)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(880, 514)
        Me.Name = "Form1"
        Me.Text = "Flag Miner"
        Me.PopUpMenu.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.TreeListView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents parseBtn As System.Windows.Forms.Button
    Friend WithEvents AbortButt As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusText As System.Windows.Forms.TextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents clearbutt As System.Windows.Forms.Button
    Friend WithEvents PopUpMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyFlagToClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents intCheck As System.Windows.Forms.CheckBox
    Friend WithEvents polCheck As System.Windows.Forms.CheckBox
    Friend WithEvents spCheck As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CopyBtn As System.Windows.Forms.Button
    Friend WithEvents TreeListView1 As BrightIdeasSoftware.TreeListView
    Friend WithEvents FlagsColumn As BrightIdeasSoftware.OLVColumn
    Friend WithEvents TitleColumn As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ThreadColumn As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents loadbutt As System.Windows.Forms.Button
    Friend WithEvents savebutt As System.Windows.Forms.Button
    Friend WithEvents purgebutt As System.Windows.Forms.Button
    Friend WithEvents checkbutt As System.Windows.Forms.Button
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents importbutt As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents aboutButt As System.Windows.Forms.Button
    Friend WithEvents SaveFlagToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyLinkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents optButt As System.Windows.Forms.Button
    Friend WithEvents OpenXmlDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveXmlDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents subtractButt As System.Windows.Forms.Button

End Class
