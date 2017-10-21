Imports System.ComponentModel
Imports System.IO

Public Class OptionsForm

    Private Sub OptionsForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (DialogResult = Windows.Forms.DialogResult.None) Then e.Cancel = True
    End Sub

    Private Sub OptionsForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.enableCheck.Checked = Form1.options.enableCheck
        Me.enablePurge.Checked = Form1.options.enablePurge
        Me.localRepoFolder.Text = Form1.options.localRepoFolder
        Me.localSaveFolder.Text = Form1.options.localSaveFolder
        Me.markTroll.Checked = Form1.options.markTroll
        Me.RadioButton1.Checked = Form1.options.useLocal
        Me.userAgent.Text = Form1.options.userAgent
        Me.saveAndLoadFolder.Text = Form1.options.saveAndLoadFolder
        Me.deleteChildFree.Checked = Form1.options.deleteChildFree
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not Me.ValidateChildren() Then
            Me.DialogResult = DialogResult.None
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub localSaveFolder_Validated(sender As Object, e As EventArgs) Handles localSaveFolder.Validated
        Form1.options.localSaveFolder = Me.localSaveFolder.Text
        Form1.options.localRepoFolder = Me.localRepoFolder.Text
        Form1.options.enableCheck = Me.enableCheck.Checked
        Form1.options.enablePurge = Me.enablePurge.Checked
        Form1.options.useLocal = Me.RadioButton1.Checked
        Form1.options.markTroll = Me.markTroll.Checked
        Form1.options.userAgent = Me.userAgent.Text
        Form1.options.saveAndLoadFolder = Me.saveAndLoadFolder.Text
        Form1.options.deleteChildFree = Me.deleteChildFree.Checked
    End Sub

    Private Sub TextBox1_Validating(sender As Object, e As CancelEventArgs) Handles localSaveFolder.Validating
        If enableCheck.Checked Then
            If (localSaveFolder.Text.Length = 0) AndAlso Not Directory.Exists(localSaveFolder.Text) Then
                MsgBox("Invalid folder", MsgBoxStyle.Critical, "Flag Miner")
                e.Cancel = True
                Return
            End If
        End If
    End Sub

    Private Sub TextBox2_Validating(sender As Object, e As CancelEventArgs) Handles localRepoFolder.Validating
        If enablePurge.Checked AndAlso RadioButton1.Checked Then
            If (localRepoFolder.Text.Length = 0) AndAlso Not Directory.Exists(localRepoFolder.Text) Then
                MsgBox("Invalid folder", MsgBoxStyle.Critical, "Flag Miner")
                e.Cancel = True
                Return
            End If
        End If
    End Sub

    Private Sub userAgent_Validating(sender As Object, e As CancelEventArgs) Handles userAgent.Validating
        If (String.IsNullOrEmpty(userAgent.Text) OrElse String.IsNullOrWhiteSpace(userAgent.Text)) Then
            MsgBox("Insert a valid User Agent or press Default", MsgBoxStyle.Critical, "Flag Miner")
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            localSaveFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles enablePurge.CheckedChanged
        Panel1.Enabled = enablePurge.Checked
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            localRepoFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        userAgent.Text = Form1.DefaultUserAgent
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            saveAndLoadFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

End Class