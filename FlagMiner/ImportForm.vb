Imports System.Xml

Public Class ImportForm

    Public links As List(Of Tuple(Of String, String, String))

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim errorCode As Integer
        Dim rawResponse As String
        'Dim board, thread As String

        Dim uri As New Uri(TextBox1.Text)
        Dim parsedUrl As String = uri.GetLeftPart(UriPartial.Path)

        errorCode = Form1.loadThread(Nothing, Nothing, rawResponse, parsedUrl)

        Dim posts As List(Of Post)
        errorCode = Form1.parseThread(rawResponse, posts)

        Dim sourcePosts As New List(Of ULong)
        errorCode = parseStrings(posts, sourcePosts)

        Dim trimmedPosts As New List(Of Post)
        errorCode = filterPosts(posts, sourcePosts, trimmedPosts)

        links = New List(Of Tuple(Of String, String, String))
        errorCode = gatherLinks(trimmedPosts, links)

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Public Function parseStrings(ByRef posts As List(Of Post), ByRef sourcePosts As List(Of ULong)) As Integer
        For Each lin As String In TextBox2.Lines
            Dim temp As String = lin.Trim({">"c, " "c})
            Dim res As ULong
            If String.IsNullOrEmpty(temp) Then
                Continue For
            End If
            If ULong.TryParse(temp, res) Then
                sourcePosts.Add(res)
            Else
                MsgBox("Wrong format for " & lin, , "Flag Miner")
                Return 1
            End If
        Next
        sourcePosts.Distinct()
        sourcePosts.Sort()

        Return 0
    End Function

    Public Function filterPosts(ByRef posts As List(Of Post), ByRef sourcePosts As List(Of ULong), filtered As List(Of Post)) As Integer
        For Each Post As Post In posts
            If sourcePosts.Contains(Post.no) Then
                filtered.Add(Post)
            End If
        Next
        Return 0
    End Function


    Public Function gatherLinks(ByRef filtered As List(Of Post), ByRef links As List(Of Tuple(Of String, String, String))) As Integer
        Dim temp As New List(Of String)

        WebBrowser1.Navigate(String.Empty)
        Dim fakeDoc As HtmlDocument = WebBrowser1.Document
        For Each Post As Post In filtered
            Dim text As String = Post.com
            text = "<HTML><body>" & text & "</body></HTML>"

            fakeDoc.Write(text)
        Next

        Dim HtmlElems As HtmlElementCollection = fakeDoc.Links
        For Each HtmlElem As HtmlElement In HtmlElems
            Dim str As String = HtmlElem.GetAttribute("href")
            Dim uri As New Uri(str)
            Dim str2 As String = uri.Fragment
            Dim parts As String() = uri.AbsolutePath.Split({"/"c}, StringSplitOptions.RemoveEmptyEntries)
            If String.IsNullOrEmpty(str2) Then
                str2 = parts(2)
            Else
                str2 = str2.Trim({"#"c, "p"c})
            End If
            If parts.Length = 3 Then links.Add(New Tuple(Of String, String, String)(parts(0), parts(2), str2))
        Next
        Return 0

    End Function


    Private Sub ImportForm_Load(sender As Object, e As EventArgs) Handles MyBase.HelpRequested
        WatDo.ShowDialog()
    End Sub
End Class