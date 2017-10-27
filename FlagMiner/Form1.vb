Imports System.ComponentModel
Imports System.Threading
Imports System.Net
Imports System.IO
Imports System.Xml.Serialization
Imports System.Collections.Concurrent
Imports System.Text
Imports System.Web
Imports System.Net.Http
Imports System.Collections.Specialized
Imports BrightIdeasSoftware


Public Class Form1

    Public DefaultUserAgent As String = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10136"

    Public baseUrl As String = "http://boards.4chan.org/"
    Public archiveBaseUrl As String = "http://a.4cdn.org/"
    Public imageBaseUrl As String = "http://s.4cdn.org/image/country/"
    Public boardDict As New List(Of String) From {"int/", "pol/", "sp/"}
    Public catalogStr As String = "/archive.json"

    Dim xs As New XmlSerializer(GetType(SerializableDictionary(Of String, Long)))

    Dim flegsBaseUrl As String = "https://raw.githubusercontent.com/flaghunters/Extra-Flags-for-int-/master/flags/"
    Dim backendBaseUrl As String = "http://whatisthisimnotgoodwithcomputers.com/"   ' // not https bcs installing the certificate on wine is a nightmare

    Dim getUrl As String = "int/get_flags_api2.php"

    Dim headerCollection As New WebHeaderCollection()

    Dim ser = New System.Web.Script.Serialization.JavaScriptSerializer()

    Dim regionDivider As String = "||"

    Dim flegComparer As New FlegComparer
    Dim postComparer As New PostComparer

    Dim helper As ImageListHelper
    Dim rootManager As MergeManager
    Public updateManager As UpdateManager

    Dim helperStack As New ConcurrentQueue(Of String)

    Public MainMergeStack As New ConcurrentQueue(Of SerializableDictionary(Of String, RegionalFleg))
    Public MainUpdateStack As New ConcurrentQueue(Of Object)
    Public MainTree As New SerializableDictionary(Of String, RegionalFleg)

    Const savedTreeFile As String = "savedTree.xml"
    Const badFlagString As String = "Region empty, no flag yet or you did not set."

    'Public exclusionByList As Boolean
    'Public exclusionByDate As Boolean
    'Public exclusionDate As DateTime
    Public exclusionDateLong As Long

    Public blankImg As Image

    Private Shared ReadOnly UnixEpoch As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)

    Public options As Options
    Public optionsFile As String = "options.xml"

    Private Sub parseBtn_Click(sender As Object, e As EventArgs) Handles parseBtn.Click
        Dim boardList As New List(Of String)

        If intCheck.Checked Then boardList.Add("int")
        If polCheck.Checked Then boardList.Add("pol")
        If spCheck.Checked Then boardList.Add("sp")

        exclusionDateLong = (options.exclusionDate.ToUniversalTime - UnixEpoch).TotalSeconds

        SetupForParsing()

        ValidateOptions()

        StatusText.AppendText(DateTime.Now & " : Mining started." & vbCrLf)

        BackgroundWorker1.RunWorkerAsync(boardList)

    End Sub


    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim statusFlag As Integer
        Dim boardList As List(Of String) = e.Argument
        Dim worker As BackgroundWorker = sender

        Dim markedForAbortion As Boolean = False

        For Each board In boardList
            If markedForAbortion Then Exit For
            Try
                Dim errorCode As Integer
                Dim response As String = ""
                Dim threads As List(Of String)
                Dim excludedThreads As ConcurrentDictionary(Of String, Long)

                errorCode = loadArchive(board, response)
                raiseError(errorCode, statusFlag)

                errorCode = parseArchive(response, threads)
                raiseError(errorCode, statusFlag)

                errorCode = loadExclusionList(board, excludedThreads)
                raiseError(errorCode, statusFlag)

                errorCode = purgeExclusionList(excludedThreads, threads)
                raiseError(errorCode, statusFlag)

                Dim finalTime As Long = (DateTime.UtcNow - UnixEpoch).TotalSeconds

                For i As Integer = 0 To threads.Count - 1 Step 1
                    If (worker.CancellationPending = True) Then
                        e.Cancel = True
                        markedForAbortion = True
                        Exit For
                    End If

                    worker.ReportProgress(i + 1, {board, threads.Count})
                    'Thread.Sleep(50) ' do not flood the server and get banned
                    Try
                        Dim rawResponse As String
                        errorCode = loadThread(board, threads(i), rawResponse)
                        raiseError(errorCode, statusFlag)

                        Dim posts As List(Of Post)
                        errorCode = parseThread(rawResponse, posts)
                        raiseError(errorCode, statusFlag)

                        Dim firstPost As Post = posts(0)
                        finalTime = firstPost.archived_on
                        If (options.exclusionByDate AndAlso finalTime > exclusionDateLong) Or (Not options.exclusionByDate) Then

                            Dim flegs As Fleg()
                            errorCode = queryExtraFlags(board, posts, flegs)
                            raiseError(errorCode, statusFlag)

                            Dim parsedFlegs As List(Of RegionalFleg)
                            errorCode = parseFlags(board, posts, flegs, parsedFlegs)

                            Dim flagTree As New SerializableDictionary(Of String, RegionalFleg)
                            errorCode = mergeFlegs(parsedFlegs, flagTree)

                            'TreeListView1.Roots = flagTree   ' TODO inviare a concurrent stack e inizializzare rootmanager
                            rootManager.AddToStack(flagTree)
                        End If
                        excludedThreads.TryAdd(threads(i), finalTime)

                    Catch ex As WebException   ' for inner loop catch it here bls
                        Dim resp = DirectCast(ex.Response, HttpWebResponse)
                        If resp IsNot Nothing AndAlso resp.StatusCode = HttpStatusCode.NotFound Then
                            ' skip this and save as exclusion
                            excludedThreads.TryAdd(threads(i), finalTime)
                        Else
                            ' halt everything.. internet down?
                            AppendText(DateTime.Now & " : " & board & "/" & threads(i) & " " & ex.ToString & vbCrLf)
                            markedForAbortion = True
                        End If
                    Catch ex As Exception
                        AppendText(DateTime.Now & " : " & board & "/" & threads(i) & " " & ex.ToString & vbCrLf)
                        markedForAbortion = True
                    End Try

                    If markedForAbortion Then Exit For

                Next

                errorCode = saveExclusionList(board, excludedThreads)
                raiseError(errorCode, statusFlag)

                If markedForAbortion Then Exit For

            Catch ex As WebException
                AppendText(DateTime.Now & " : " & board & " " & ex.ToString & vbCrLf)
                markedForAbortion = True
            Catch ex As Exception
                AppendText(DateTime.Now & " : " & board & " " & ex.ToString & vbCrLf)
                markedForAbortion = True
            End Try
        Next
    End Sub


    Private Sub AbortButt_Click(sender As Object, e As EventArgs) Handles AbortButt.Click
        If BackgroundWorker1.IsBusy Then BackgroundWorker1.CancelAsync()
        If BackgroundWorker2.IsBusy Then BackgroundWorker2.CancelAsync()
        StatusText.AppendText(DateTime.Now & " : Abort signal sent." & vbCrLf)
        SetupForIdle()
    End Sub

    Public Sub SetupForParsing()
        AbortButt.Enabled = True
        parseBtn.Enabled = False
        CopyBtn.Enabled = False

        clearbutt.Enabled = False
        savebutt.Enabled = False
        loadbutt.Enabled = False

        purgebutt.Enabled = False
        ToolTip2.Active = False
        checkbutt.Enabled = False
        ToolTip1.Active = False
        subtractButt.Enabled = False
        importbutt.Enabled = False

        GroupBox1.Enabled = False
        GroupBox2.Enabled = False

        'CommitAll.Enabled = False
        'CommitSel.Enabled = False
    End Sub

    Public Sub SetupForIdle()
        AbortButt.Enabled = False
        parseBtn.Enabled = True
        CopyBtn.Enabled = True

        clearbutt.Enabled = True
        savebutt.Enabled = True
        loadbutt.Enabled = True
        If options.enablePurge Then
            purgebutt.Enabled = True
            ToolTip2.Active = False
        Else
            purgebutt.Enabled = False
            ToolTip2.Active = True
        End If
        If options.enableCheck Then
            checkbutt.Enabled = True
            ToolTip1.Active = False
        Else
            checkbutt.Enabled = False
            ToolTip1.Active = True
        End If
        subtractButt.Enabled = True
        importbutt.Enabled = True

        GroupBox1.Enabled = True
        GroupBox2.Enabled = True
        'CommitAll.Enabled = True
        'CommitSel.Enabled = True
    End Sub

    Delegate Sub AppendTextCallBack(str As String)
    Private Sub AppendText(str As String)
        Dim d As New AppendTextCallBack(AddressOf AppendTextCallBackFunction)
        Me.Invoke(d, str)
    End Sub

    Sub appendTextCallBackFunction(str As String)
        StatusText.AppendText(str)
    End Sub


    Private Sub raiseError(errorcode As Integer, ByRef statusFlag As Integer)   ' @TODO :  proper error handling
        If errorcode <> 0 Then
            statusFlag = 1
            Throw New Exception()
        End If
    End Sub

    Delegate Sub UpdateRootsCallBack()

    Sub UpdateRootsInvoker()
        Dim d As New UpdateRootsCallBack(AddressOf UpdateRoots)
        Me.Invoke(d)
    End Sub

    Sub UpdateRoots()
        Me.TreeListView1.SuspendLayout()
        Me.TreeListView1.Roots = MainTree
        Me.TreeListView1.ResumeLayout()
    End Sub

    Delegate Sub RefreshTreeCallBack()

    Sub RefreshTree()
        Dim d As New RefreshTreeCallBack(AddressOf RefreshTreeFunction)
        Me.Invoke(d)
    End Sub

    Sub RefreshTreeFunction()
        Me.TreeListView1.Refresh()
    End Sub

    Delegate Sub UpdateTreeViewCallback(acc As List(Of Object))

    Sub UpdateTreeViewInvoker(accumulator As List(Of Object))
        Dim d As New UpdateTreeViewCallback(AddressOf UpdateObjects)
        Me.Invoke(d, accumulator)
    End Sub

    Sub UpdateObjects(acc As List(Of Object))
        Me.TreeListView1.SuspendLayout()
        Me.TreeListView1.RefreshObjects(acc)
        Me.TreeListView1.ResumeLayout()
    End Sub

    Delegate Sub setImgSizeCallback(imgSize As Size)

    Sub SetImgSizeInvoker(imgSize As Size)
        'If (TreeListView1.InvokeRequired) Then
        Dim d As New setImgSizeCallback(AddressOf setImgSize)
        Me.Invoke(d, New Object() {imgSize})
        'Else
        'Me.ImageList1.ImageSize = imgSize

        'End If

    End Sub

    Sub setImgSize(imgSize As Size)
        Me.ImageList1.ImageSize = imgSize
        Me.TreeListView1.BaseSmallImageList = Me.ImageList1
    End Sub

    'Delegate Sub setRootsCallback(flagTree)

    Private Function loadArchive(board As String, ByRef rawResponse As String) As Integer
        Dim request As System.Net.HttpWebRequest
        Dim response As System.Net.HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim boardUrl As String

        boardUrl = baseUrl & board & catalogStr

        request = DirectCast(System.Net.WebRequest.Create(boardUrl), System.Net.HttpWebRequest)
        request.UserAgent = options.userAgent
        response = DirectCast(request.GetResponse(), System.Net.HttpWebResponse)

        Dim status As HttpStatusCode = response.StatusCode()

        If status = HttpStatusCode.OK Then
            reader = New StreamReader(response.GetResponseStream())
            rawResponse = reader.ReadToEnd()

            Return 0
        End If

        Return 1

    End Function

    Private Function parseArchive(response As String, ByRef threads As List(Of String)) As Integer
        Dim tempArray As String()

        tempArray = ser.Deserialize(Of String())(response)    ' all archived thread numbers listed
        threads = tempArray.ToList()

        Return 0

    End Function

    Private Function loadExclusionList(board As String, ByRef exclusionList As ConcurrentDictionary(Of String, Long)) As Integer
        Dim threadDbName = board & ".db"

        ' database coi thread già visti
        If IO.File.Exists(threadDbName) Then
            Dim tempList As SerializableDictionary(Of String, Long)
            Dim fs As New FileStream(threadDbName, FileMode.Open)
            exclusionList = New ConcurrentDictionary(Of String, Long)

            tempList = xs.Deserialize(fs)
            For Each ke In tempList
                exclusionList.TryAdd(ke.Key, ke.Value)
            Next
            'exclusionList = New ConcurrentDictionary(Of String, Long)
            'For Each item As String In tempList
            '    exclusionList.TryAdd(item, 1)
            'Next
            fs.Close()
        Else
            exclusionList = New ConcurrentDictionary(Of String, Long)
        End If

        Return 0

    End Function

    Private Function saveExclusionList(board As String, ByRef exclusionList As ConcurrentDictionary(Of String, Long)) As Integer
        Dim tempList As New SerializableDictionary(Of String, Long)
        Dim threadDbName = board & ".db"
        For Each ke In exclusionList
            tempList.Add(ke.Key, ke.Value)
        Next
        Dim fs As New FileStream(threadDbName, FileMode.Create)
        xs.Serialize(fs, tempList)
        fs.Close()
        Return 0
    End Function

    Private Function purgeExclusionList(ByRef exclusionList As ConcurrentDictionary(Of String, Long), ByRef threadList As List(Of String)) As Integer

        If options.exclusionByList Then
            Dim TBDeleted As New List(Of String)
            For i = 0 To exclusionList.Count - 1
                Dim str As String = exclusionList.Keys(i)
                If Not threadList.Contains(str) Then
                    TBDeleted.Add(str)
                End If
            Next

            Dim bogusshitwedontneednow As Long
            For Each st As String In TBDeleted
                exclusionList.TryRemove(st, bogusshitwedontneednow)
            Next

            For Each st As String In exclusionList.Keys
                threadList.Remove(st)
            Next
        End If

        If options.exclusionByDate Then
            Dim tempDict As Dictionary(Of String, Long)
            tempDict = exclusionList.Where(Function(e) e.Value < exclusionDateLong).ToDictionary(Of String, Long)(Function(e) e.Key, Function(e) e.Value) '.ToDictionary(Of String, Long)(Function(e) e.Key, Function(e) e.Value)

            For Each st As String In tempDict.Keys
                threadList.Remove(st)
            Next

        End If

        Return 0
    End Function

    Public Function queryExtraFlags(ByVal board As String, ByRef posts As List(Of Post), ByRef flags As Fleg()) As Integer
        Dim strB As New StringBuilder()
        Dim tempstr As New StringBuilder()

        If posts.Count > 0 Then
            strB.Append("post_nrs=")
            For i As Integer = 0 To posts.Count - 1
                Dim postNo As String = posts(i).no
                If i > 0 Then
                    tempstr.Append("," & postNo)
                Else
                    tempstr.Append(postNo)
                End If
            Next
            strB.Append(HttpUtility.UrlEncode(tempstr.ToString))
            strB.Append("&" & "board=" & HttpUtility.UrlEncode(board))

            Using client As New WebClient()

                Dim values As New NameValueCollection() From {{"post_nrs", tempstr.ToString}, {"board", board}}
                Dim responses = client.UploadValues(backendBaseUrl + getUrl, values)

                Dim response = Encoding.Default.GetString(responses)

                flags = ser.Deserialize(Of Fleg())(response)

                Return 0
            End Using

            Return 1  ' fail
        Else
            If flags.Length > 0 Then Array.Clear(flags, 0, flags.Length)
            Return 0
        End If
    End Function

    Public Function loadThread(board As String, thread As String, ByRef rawResponse As String, Optional fullpath As String = Nothing) As Integer
        Dim request As System.Net.HttpWebRequest
        Dim response As System.Net.HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim boardUrl As String

        If fullpath Is Nothing Then
            boardUrl = archiveBaseUrl & board & "/thread/" & thread & ".json"
        Else : boardUrl = fullpath & ".json"
        End If

        request = DirectCast(System.Net.WebRequest.Create(boardUrl), System.Net.HttpWebRequest)
        request.UserAgent = options.userAgent
        response = DirectCast(request.GetResponse(), System.Net.HttpWebResponse)

        Dim status As HttpStatusCode = response.StatusCode()

        If status = HttpStatusCode.OK Then
            reader = New StreamReader(response.GetResponseStream())
            rawResponse = reader.ReadToEnd()

            Return 0
        End If

        Return 1
    End Function

    Public Function parseThread(response As String, ByRef posts As List(Of Post)) As Integer
        Dim tempArray As ChanThread
        tempArray = ser.Deserialize(Of ChanThread)(response)    ' all archived thread numbers listed

        posts = tempArray.posts

        Return 0
    End Function

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If BackgroundWorker1.IsBusy Or BackgroundWorker2.IsBusy Then
            MsgBox("Task is ongoing. Please hit ""Abort"" before closing", , "Flag Miner")
            e.Cancel = True
            Return
        End If
        Try
            SaveOptions()
        Catch ex As Exception
            MsgBox(e.ToString, MsgBoxStyle.Critical, "Flag Miner")
            e.Cancel = True
        End Try
    End Sub

    Private Sub SaveOptions()
        Dim fs As New FileStream(optionsFile, FileMode.Create)

        Dim optionsSerializer As New XmlSerializer(GetType(Options))
        optionsSerializer.Serialize(fs, Options)
        fs.Close()
    End Sub

    Private Sub LoadOptions()
        Dim fs As FileStream
        Try
            fs = New FileStream(optionsFile, FileMode.OpenOrCreate)

            Dim optionsSerializer As New XmlSerializer(GetType(Options))
            options = optionsSerializer.Deserialize(fs)
        Catch ex As Exception
        Finally
            If fs IsNot Nothing Then fs.Close()
        End Try

        If options.exclusionDate > DateTimePicker1.MaxDate Or options.exclusionDate < DateTimePicker1.MinDate Then
            options.exclusionDate = DateTime.Now
        End If
        DateTimePicker1.Value = options.exclusionDate
        intCheck.Checked = options.intCheck
        polCheck.Checked = options.polCheck
        spCheck.Checked = options.spCheck
        CheckBox1.Checked = options.exclusionByDate
        CheckBox2.Checked = options.exclusionByList
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadOptions()

        headerCollection.Add("Content-Type", "application/x-www-form-urlencoded")

        Dim tempbmp As New Bitmap(1, 1)
        blankImg = Image.FromHbitmap(tempbmp.GetHbitmap)
        'Dim g As Graphics = Graphics.FromImage(tempbmp)
        'g.DrawImage(blankImg, 1, 1, 1, 1)

        helper = New ImageListHelper(Me.TreeListView1, helperStack)
        rootManager = New MergeManager(MainMergeStack, MainTree, TreeListView1)
        updateManager = New UpdateManager(MainUpdateStack, TreeListView1)

        TreeListView1.CanExpandGetter = AddressOf ExpandGetter
        TreeListView1.ChildrenGetter = AddressOf ChildrenGetter

        TreeListView1.HoverSelection = False
        TreeListView1.HotTracking = False

        Me.ThreadColumn.AspectGetter = AddressOf ThreadAspect
        Me.TitleColumn.AspectGetter = AddressOf TitleAspect
        'Me.FlagsColumn.AspectGetter = AddressOf ImageAspect
        Me.FlagsColumn.ImageGetter = AddressOf ImageGetter

        SetupForIdle()

    End Sub

    Public Sub FormatRow_EventHandler(sender As Object, e As FormatRowEventArgs) Handles TreeListView1.FormatRow
        Dim fleg As KeyValuePair(Of String, RegionalFleg) = DirectCast(e.Model, KeyValuePair(Of String, RegionalFleg))
        If (fleg.Value.isTrollFlag) Then e.Item.BackColor = Color.LightPink
        If (fleg.Value.exists) Then e.Item.BackColor = Color.LightGreen
    End Sub

    Public Function ThreadAspect(x As Object) As Object
        Dim fleg As KeyValuePair(Of String, RegionalFleg) = DirectCast(x, KeyValuePair(Of String, RegionalFleg))
        Return fleg.Value.thread
    End Function

    Public Function TitleAspect(x As Object) As Object
        Dim fleg As KeyValuePair(Of String, RegionalFleg) = DirectCast(x, KeyValuePair(Of String, RegionalFleg))
        Return fleg.Value.title
    End Function

    Public Function ImageAspect(x As Object) As Object
        Dim fleg As KeyValuePair(Of String, RegionalFleg) = DirectCast(x, KeyValuePair(Of String, RegionalFleg))
        Return fleg.Value.imgurl
    End Function

    Public Function ImageGetter(x As Object) As Object
        Dim fleg As KeyValuePair(Of String, RegionalFleg) = DirectCast(x, KeyValuePair(Of String, RegionalFleg))

        'Return helper.GetImageIndex(fleg.Value.imgurl)
        If (helper.HasImage(fleg.Value.imgurl)) Then
            Return helper.GetImageIndex(fleg.Value.imgurl)
        End If

        If Not fleg.Value.fetching Then
            fleg.Value.fetching = True
            Task.Factory.StartNew(Sub()
                                      Dim frm As Form1 = TreeListView1.Parent
                                      Try
                                          helper.AddToStack(fleg.Value.imgurl)
                                          'helper.GetImageIndex(fleg.Value.imgurl)
                                          'frm.updateManager.AddToStack(fleg)
                                          'Me.TreeListView1.RefreshObject(fleg)
                                      Catch ex As Exception
                                      Finally
                                          'fleg.Value.fetching = False
                                      End Try
                                  End Sub)
            'helper.GetImageIndex(fleg.Value.imgurl)
            'Me.TreeListView1.RefreshObject(fleg)
        End If
        Return blankImg
    End Function

    Public Function ExpandGetter(x As Object) As Boolean
        Dim fleg As KeyValuePair(Of String, RegionalFleg) = DirectCast(x, KeyValuePair(Of String, RegionalFleg))
        Return (fleg.Value.children.Count > 0)
    End Function

    Public Function ChildrenGetter(x As Object) As Object
        Dim fleg As KeyValuePair(Of String, RegionalFleg) = DirectCast(x, KeyValuePair(Of String, RegionalFleg))
        Return fleg.Value.children
    End Function

    Public Function parseFlags(ByVal board As String, ByVal posts As List(Of Post), ByRef extraflags As Fleg(), ByRef parsedFlegs As List(Of RegionalFleg)) As Integer
        Dim tempPosts As New List(Of Post)

        Dim listOfNo As New List(Of String)
        For Each flag In extraflags
            listOfNo.Add(flag.post_nr)
        Next


        ' some of these variables may come not sorted... sort everything by post # !!!
        listOfNo.Sort()
        tempPosts = posts.Where(Function(e As Post) listOfNo.Contains(e.no)).ToList  'purge posts without extraflags
        tempPosts.Sort(postComparer)

        Dim flegList As List(Of Fleg)
        flegList = extraflags.ToList
        flegList.Sort(flegComparer)


        ' all sorted, go on
        parsedFlegs = New List(Of RegionalFleg)

        For i As Integer = 0 To listOfNo.Count - 1
            Dim post As Post = tempPosts(i)
            Dim fleg As String = flegList(i).region

            Dim arr() As String = fleg.Split((New List(Of String) From {regionDivider}).ToArray, StringSplitOptions.RemoveEmptyEntries)

            ' main flag
            Dim mf As String
            Dim trollflag As Boolean
            Dim imgUrl As String
            If post.troll_country IsNot Nothing AndAlso post.troll_country <> String.Empty Then
                mf = post.country_name
                trollflag = True
                imgUrl = imageBaseUrl & "troll/" & post.troll_country.ToLower & ".gif"
            Else
                mf = post.country_name
                trollflag = False
                imgUrl = imageBaseUrl & post.country.ToLower & ".gif"
            End If

            Dim postUrl = baseUrl & board & "/thread/" & post.resto & "#p" & post.no

            Dim regFlag As New RegionalFleg()
            regFlag.isTrollFlag = trollflag
            regFlag.title = mf
            regFlag.board = board
            regFlag.pNo = post.no
            regFlag.imgurl = imgUrl
            regFlag.thread = postUrl
            regFlag.time = post.time

            ' loop on children
            Dim prev As RegionalFleg = regFlag
            Dim completeUrl As String = flegsBaseUrl & post.country_name & "/"

            For child As Integer = 0 To arr.Length - 1
                Dim curChild As New RegionalFleg

                curChild.isTrollFlag = False
                curChild.thread = postUrl
                curChild.title = arr(child)
                curChild.board = board
                curChild.pNo = post.no
                curChild.time = post.time
                curChild.imgurl = completeUrl & arr(child) & ".png"

                completeUrl += arr(child) & "/"

                prev.children.Add(curChild.title, curChild)

                prev = curChild
            Next

            parsedFlegs.Add(regFlag)

        Next

        Return 0

    End Function

    Shared Function mergeFlegs(ByRef collectedFlegs As List(Of RegionalFleg), ByRef flegTree As SerializableDictionary(Of String, RegionalFleg)) As Integer
        'flegTree = New SerializableDictionary(Of String, RegionalFleg)

        For Each Fleg In collectedFlegs
            Dim curDict As SerializableDictionary(Of String, RegionalFleg) = flegTree
            Dim curFleg As RegionalFleg = Fleg
            Dim prevFleg As RegionalFleg = Nothing

            If Not curDict.ContainsKey(curFleg.title) Then
                curDict.Add(curFleg.title, curFleg)  ' does it copy it all?  TODO CREATE DEEP COPY   48861
            Else
                Dim presentFleg As RegionalFleg = curDict(curFleg.title)
                If presentFleg.time < curFleg.time Then
                    presentFleg.time = curFleg.time
                    presentFleg.pNo = curFleg.pNo
                    presentFleg.thread = curFleg.thread
                    presentFleg.board = curFleg.board
                End If
                If curFleg.children.Count > 0 Then
                    Dim curSrcDict As SerializableDictionary(Of String, RegionalFleg) = curFleg.children
                    Dim curDestDict As SerializableDictionary(Of String, RegionalFleg) = curDict(curFleg.title).children
                    merger(curSrcDict, curDestDict)
                End If
            End If
        Next

        Return 0
    End Function


    Shared Sub merger(ByRef source As SerializableDictionary(Of String, RegionalFleg), ByRef dest As SerializableDictionary(Of String, RegionalFleg))
        For Each el In source
            If Not dest.ContainsKey(el.Key) Then
                dest.Add(el.Key, el.Value)
            Else
                Dim curdestfleg = dest(el.Key)
                Dim cursourcefleg = el.Value
                If curdestfleg.time < cursourcefleg.time Then
                    curdestfleg.time = cursourcefleg.time
                    curdestfleg.pNo = cursourcefleg.pNo
                    curdestfleg.thread = cursourcefleg.thread
                    curdestfleg.board = cursourcefleg.board
                End If
                If cursourcefleg.children.Count > 0 Then
                    merger(cursourcefleg.children, curdestfleg.children)
                End If
            End If
        Next
    End Sub


    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged, BackgroundWorker2.ProgressChanged
        Dim arr As Object() = e.UserState
        Label2.Text = String.Format("Current board: {0}. Parsing thread {1} of {2}", arr(0), e.ProgressPercentage, arr(1))
        ProgressBar1.Maximum = arr(1)
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted, BackgroundWorker2.RunWorkerCompleted
        If e.Cancelled = True Then
            SetupForIdle()
            StatusText.AppendText(DateTime.Now & " : Aborted." & vbCrLf)
        ElseIf e.Error IsNot Nothing Then
            StatusText.AppendText(DateTime.Now & " : An error occurred." & vbCrLf)
            SetupForIdle()
        Else
            StatusText.AppendText(DateTime.Now & " : Parsing completed." & vbCrLf)
            SetupForIdle()
            Dim res = WindowsApi.FlashWindow(Process.GetCurrentProcess().MainWindowHandle, True, True, 5)
        End If
    End Sub

    Public Sub returnPasta(dict As SerializableDictionary(Of String, RegionalFleg), str As String, ByRef pasta As StringBuilder)
        For Each ch In dict

            Dim curFleg As RegionalFleg = ch.Value
            Dim curDict As SerializableDictionary(Of String, RegionalFleg) = curFleg.children
            Dim curString As String = curFleg.title & ", " & str
            If curDict.Count = 0 Then
                pasta.AppendLine(">>>/" & curFleg.board & "/" & curFleg.pNo & " " & curString)
                Continue For
            Else
                returnPasta(curDict, curString, pasta)
            End If
        Next
    End Sub


    Private Sub CopyBtn_Click(sender As Object, e As EventArgs) Handles CopyBtn.Click
        ' copy links to clipboard

        Dim pasta As New StringBuilder

        For Each Fleg In MainTree
            Dim curString = Fleg.Value.title
            'Dim curDict As Dictionary(Of String, RegionalFleg) = flegTree
            Dim curFleg As RegionalFleg = Fleg.Value
            Dim curDict As SerializableDictionary(Of String, RegionalFleg) = Fleg.Value.children

            If curDict.Count = 0 Then
                pasta.AppendLine(">>>/" & curFleg.board & "/" & curFleg.pNo & " " & curString)
                Continue For
            Else
                returnPasta(curDict, curString, pasta)
            End If
        Next

        My.Computer.Clipboard.SetText(pasta.ToString)
    End Sub

    Private Sub clearbutt_Click(sender As Object, e As EventArgs) Handles clearbutt.Click
        If MsgBox("Clear flags, sure?", MsgBoxStyle.YesNo, "Flag Miner") = MsgBoxResult.Yes Then
            MainTree.Clear()
            helper.Clear()
            UpdateRoots()
        End If
    End Sub

    Private Sub savebutt_Click(sender As Object, e As EventArgs) Handles savebutt.Click
        If MsgBox("Save current tree to file?", MsgBoxStyle.YesNo, "Flag Miner") = MsgBoxResult.Yes Then
            SaveXmlDialog.InitialDirectory = options.saveAndLoadFolder
            If SaveXmlDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim currentFile As String = SaveXmlDialog.FileName
                Dim fs As New FileStream(currentFile, FileMode.Create)
                Dim CurTree As SerializableDictionary(Of String, RegionalFleg) = MainTree

                Dim treeSerializer As New XmlSerializer(GetType(SerializableDictionary(Of String, RegionalFleg)))
                treeSerializer.Serialize(fs, MainTree)
                fs.Close()
            End If
        End If
    End Sub

    Private Sub loadbutt_Click(sender As Object, e As EventArgs) Handles loadbutt.Click
        If MsgBox("Load tree from file? It will be merged with the current tree", MsgBoxStyle.YesNo, "Flag Miner") = MsgBoxResult.Yes Then
            OpenXmlDialog.InitialDirectory = options.saveAndLoadFolder
            If OpenXmlDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim currentFile As String = OpenXmlDialog.FileName
                Dim temptree As SerializableDictionary(Of String, RegionalFleg)
                Dim fs As FileStream
                Try
                    fs = New FileStream(currentFile, FileMode.Open)
                    Dim treeSerializer As New XmlSerializer(GetType(SerializableDictionary(Of String, RegionalFleg)))
                    temptree = treeSerializer.Deserialize(fs)
                    mergeFlegs(temptree.Values.ToList, MainTree)
                Catch ex As Exception
                Finally
                    If fs IsNot Nothing Then fs.Close()
                End Try

                Task.Run(New Action(AddressOf CacheFlegs))

                UpdateRoots()
                TreeListView1.Invalidate()
            End If
        End If

    End Sub

    Public Function CacheFlegs()
        Dim lstr As New List(Of String)
        For Each ke In MainTree
            lstr.Add(ke.Value.imgurl)
            For Each ke2 In ke.Value.children
                cacheEm(ke2.Value, lstr)
            Next
        Next
        lstr.Distinct()
        Dim queue As New ConcurrentDictionary(Of String, Image)
        Dim pOpt As New ParallelOptions
        pOpt.MaxDegreeOfParallelism = 10
        Parallel.ForEach(Of String)(lstr, pOpt, Sub(str)
                                                    Dim img As Image = ImageListHelper.ScrapeImage(str)
                                                    queue.TryAdd(str, img)
                                                End Sub)
        Me.TreeListView1.SuspendLayout()
        For Each ke In queue
            Try
                If Not helper.HasImage(ke.Key) Then helper.AddImageToCollection(ke.Key, Me.TreeListView1.SmallImageList, ke.Value)
            Catch generatedExceptionName As ArgumentNullException
            End Try
        Next
        Me.TreeListView1.ResumeLayout()
        Me.RefreshTree()
        Return Nothing
    End Function

    Function cacheEm(fleg As RegionalFleg, ByRef lstr As List(Of String))
        lstr.Add(fleg.imgurl)
        For Each ke2 In fleg.children
            cacheEm(ke2.Value, lstr)
        Next
        Return Nothing
    End Function

    Public Function queryFlag(ByVal imgurl As String) As PurgeEnum
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse
        Try
            request = DirectCast(System.Net.WebRequest.Create(imgurl), System.Net.HttpWebRequest)
            request.UserAgent = options.userAgent
            request.Method = "HEAD"
            response = DirectCast(request.GetResponse(), System.Net.HttpWebResponse)

            Dim status As HttpStatusCode = response.StatusCode()
            response.Dispose()

            If status = HttpStatusCode.NotFound Then
                Return PurgeEnum.notFound
            End If
            Return PurgeEnum.ok
        Catch ex As WebException
            Dim resp = DirectCast(ex.Response, HttpWebResponse)
            If resp IsNot Nothing AndAlso resp.StatusCode = HttpStatusCode.NotFound Then
                Return PurgeEnum.notFound
            Else
                Return PurgeEnum.genericError
            End If
        Catch ex As Exception
            Return PurgeEnum.genericError   ' network error
        End Try
        Return PurgeEnum.undefined  ' undecided bcs of godknowswhy
    End Function

    Private Function purgeInvalid(ByRef fleg As RegionalFleg, ByRef parentDict As SerializableDictionary(Of String, RegionalFleg), path As String, level As Integer) As PurgeEnum
        Dim basestr = path & "\" & fleg.title
        Dim baseFile0 = basestr & ".gif"
        Dim baseFile1 = basestr & ".png"

        Dim checkedFlag As PurgeEnum
        If options.useLocal And level > 0 Then
            Dim initString As String = ""
            If fleg.imgurl.Contains(flegsBaseUrl) Then initString = options.localRepoFolder & "\" & fleg.imgurl.Replace(flegsBaseUrl, "") ' for regionals
            If fleg.imgurl.Contains(imageBaseUrl) Then initString = options.localRepoFolder & "\" & fleg.imgurl.Replace(imageBaseUrl, "") ' for nationals
            'Dim pth As String = System.IO.Path.GetDirectoryName(initString)
            'Dim fileName As String = System.IO.Path.GetFileName(initString)
            If File.Exists(initString) Then
                checkedFlag = PurgeEnum.ok
            Else
                checkedFlag = PurgeEnum.notFound
            End If
        Else
            checkedFlag = queryFlag(fleg.imgurl)
        End If
        If checkedFlag = PurgeEnum.genericError Then Return checkedFlag
        If (fleg.isTrollFlag) Then
            If (options.markTroll) Then
                fleg.markedfordeletion = True
            Else
                fleg.markedfordeletion = False
            End If
        End If
        If (checkedFlag = PurgeEnum.notFound) Then
            fleg.markedfordeletion = True
        Else
            level += 1
            For Each ke In fleg.children
                If purgeInvalid(ke.Value, fleg.children, basestr, level) = PurgeEnum.genericError Then
                    Return PurgeEnum.genericError
                End If
            Next
            Dim mirror As SerializableDictionary(Of String, RegionalFleg) = fleg.children
            fleg.children.Where(Function(pair As KeyValuePair(Of String, RegionalFleg)) pair.Value.markedfordeletion = True).ToArray(). _
                Apply(Function(pair As KeyValuePair(Of String, RegionalFleg)) mirror.Remove(pair.Key)).Apply()
        End If
    End Function

    Private Sub purgebutt_Click(sender As Object, e As EventArgs) Handles purgebutt.Click
        If MsgBox("Purge inexistent flags? This cannot be undone", MsgBoxStyle.YesNo, "Flag Miner") = MsgBoxResult.Yes Then
            Dim level = 0
            For Each ke As KeyValuePair(Of String, RegionalFleg) In MainTree
                If purgeInvalid(ke.Value, MainTree, options.localRepoFolder, level) = PurgeEnum.genericError Then
                    MsgBox("An error occurred while checking the flags. Make sure your connection is up and/or local folders are valid.", MsgBoxStyle.Critical)
                    Return
                End If
            Next
            MainTree.Where(Function(pair As KeyValuePair(Of String, RegionalFleg)) pair.Value.markedfordeletion = True Or (options.deleteChildFree AndAlso pair.Value.children.Count = 0)).ToArray(). _
                Apply(Function(pair As KeyValuePair(Of String, RegionalFleg)) MainTree.Remove(pair.Key)).Apply()

            UpdateRoots()
            TreeListView1.Invalidate()
        End If
    End Sub

    Private Sub checkExistent(ByRef fleg As RegionalFleg, ByRef parentDict As SerializableDictionary(Of String, RegionalFleg), level As Integer)
        'Dim basestr = path & "\" & fleg.title
        'Dim baseFile0 = basestr & ".gif"
        'Dim baseFile1 = basestr & ".png"
        Dim initString As String = ""
        If fleg.imgurl.Contains(flegsBaseUrl) Then initString = options.localSaveFolder & "\" & fleg.imgurl.Replace(flegsBaseUrl, "") ' for regionals
        If fleg.imgurl.Contains(imageBaseUrl) Then initString = options.localSaveFolder & "\" & fleg.imgurl.Replace(imageBaseUrl, "") ' for nationals
        'Dim pth As String = System.IO.Path.GetDirectoryName(initString)
        'Dim fileName As String = System.IO.Path.GetFileName(initString)
        'If File.Exists(initString) Then
        'If ((level = 0 AndAlso Not File.Exists(baseFile0)) OrElse (level > 0 AndAlso Not File.Exists(baseFile1))) Then
        fleg.exists = File.Exists(initString)
        'Else
        'fleg.exists = True
        'End If
        level += 1
        For Each ke In fleg.children
            checkExistent(ke.Value, fleg.children, level)
        Next
        'Dim mirror As SerializableDictionary(Of String, RegionalFleg) = fleg.children
    End Sub

    Private Sub checkbutt_Click(sender As Object, e As EventArgs) Handles checkbutt.Click
        If MsgBox("Mark existent flags?", MsgBoxStyle.YesNo, "Flag Miner") = MsgBoxResult.Yes Then
            Dim level = 0
            For Each ke As KeyValuePair(Of String, RegionalFleg) In MainTree
                checkExistent(ke.Value, MainTree, level)
            Next
            UpdateRoots()
            TreeListView1.Invalidate()
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Me.TreeListView1.ExpandAll()
    End Sub

    Private Sub CollapseAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CollapseAllToolStripMenuItem.Click
        Me.TreeListView1.CollapseAll()
    End Sub

    Private Sub importbutt_Click(sender As Object, e As EventArgs) Handles importbutt.Click
        SetupForParsing()
        StatusText.AppendText(DateTime.Now & " : Parsing started." & vbCrLf)
        Dim res As DialogResult = ImportForm.ShowDialog()
        If res = Windows.Forms.DialogResult.OK AndAlso ImportForm.links.Count > 0 Then
            BackgroundWorker2.RunWorkerAsync(ImportForm.links)

        Else
            StatusText.AppendText(DateTime.Now & " : Action cancelled by user or no valid thread/posts given." & vbCrLf)
            SetupForIdle()
        End If
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim statusFlag As Integer
        'Dim boardList As List(Of String) = e.Argument
        Dim worker As BackgroundWorker = sender

        Dim markedForAbortion As Boolean = False
        'Dim i As Integer = 0

        Dim errorCode As Integer
        Dim response As String = ""
        Dim threads As List(Of Tuple(Of String, String, String)) = e.Argument

        threads.Sort()

        For i As Integer = 0 To threads.Count - 1 Step 1
            If (worker.CancellationPending = True) Then
                e.Cancel = True
                markedForAbortion = True
                Exit For
            End If

            Dim board As String = threads(i).Item1

            worker.ReportProgress(i + 1, {"N/A", threads.Count})
            Thread.Sleep(50) ' do not flood the server and get banned
            Try
                Dim rawResponse As String
                errorCode = loadThread(board, threads(i).Item2, rawResponse)
                raiseError(errorCode, statusFlag)

                Dim posts As List(Of Post)
                errorCode = parseThread(rawResponse, posts)
                raiseError(errorCode, statusFlag)

                Dim flegs As Fleg()
                errorCode = queryExtraFlags(board, posts, flegs)
                raiseError(errorCode, statusFlag)

                Dim parsedFlegs As List(Of RegionalFleg)
                errorCode = parseFlags(board, posts, flegs, parsedFlegs)
                raiseError(errorCode, statusFlag)

                Dim flagTree As New SerializableDictionary(Of String, RegionalFleg)
                errorCode = mergeFlegs(parsedFlegs, flagTree)
                raiseError(errorCode, statusFlag)

                'TreeListView1.Roots = flagTree   ' TODO inviare a concurrent stack e inizializzare rootmanager
                rootManager.AddToStack(flagTree)


            Catch ex As WebException ' for inner loop catch it here bls
                Dim resp = DirectCast(ex.Response, HttpWebResponse)
                If resp.StatusCode = HttpStatusCode.NotFound Then
                    ' anything to do?
                Else
                    ' halt everything.. internet down?
                    AppendText(DateTime.Now & " : " & board & "/" & threads(i).Item2 & " " & ex.ToString & vbCrLf)
                End If
            Catch ex As Exception
                AppendText(DateTime.Now & " : " & board & "/" & threads(i).Item2 & " " & ex.ToString & vbCrLf)

            End Try

        Next

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        options.exclusionDate = DateTimePicker1.Value
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        options.exclusionByDate = CheckBox1.Checked
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        options.exclusionByList = CheckBox2.Checked
    End Sub

    Private Sub aboutButt_Click(sender As Object, e As EventArgs) Handles aboutButt.Click
        AboutBox1.ShowDialog()
        SetupForIdle()
    End Sub

    Private Sub olv_CellRightClick(sender As Object, e As CellRightClickEventArgs) Handles TreeListView1.CellRightClick
        Try
            Dim m As New ContextMenuStrip '.(e.Model, e.Column)
            m.Tag = e.Model.value
            m.Items.Add("Expand All", Nothing, AddressOf ExpandHandler)
            m.Items.Add("Collapse All", Nothing, AddressOf CollapseHandler)
            m.Items.Add(New ToolStripSeparator)
            m.Items.Add("Copy flag to clipboard", Nothing, AddressOf CopyImageHandler)
            m.Items.Add("Save as...", Nothing, AddressOf SaveImageHandler)
            m.Items.Add(New ToolStripSeparator)
            m.Items.Add("Copy link", Nothing, AddressOf CopyLinkHandler)
            e.MenuStrip = m
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ExpandHandler(sender As Object, e As EventArgs)
        TreeListView1.ExpandAll()
    End Sub

    Private Sub CollapseHandler(sender As Object, e As EventArgs)
        TreeListView1.CollapseAll()
    End Sub

    Private Sub CopyImageHandler(sender As Object, e As EventArgs)
        Dim regFlag As RegionalFleg = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner.Tag, RegionalFleg)
        Try
            Dim image As Image = ImageListHelper.ScrapeImage(regFlag.imgurl)
            My.Computer.Clipboard.SetImage(image)
        Catch ex As Exception
            MsgBox("Error while downloading the flag to copy", , "Flag Miner")
        End Try
    End Sub

    Private Sub SaveImageHandler(sender As Object, e As EventArgs)
        Dim regFlag As RegionalFleg = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner.Tag, RegionalFleg)
        Try
            Dim image As Image = ImageListHelper.ScrapeImage(regFlag.imgurl)
            Dim initString As String
            If regFlag.imgurl.Contains(flegsBaseUrl) Then initString = options.localSaveFolder & "\" & regFlag.imgurl.Replace(flegsBaseUrl, "") ' for regionals
            If regFlag.imgurl.Contains(imageBaseUrl) Then initString = options.localSaveFolder & "\" & regFlag.imgurl.Replace(imageBaseUrl, "") ' for nationals
            Dim pth As String = Path.GetDirectoryName(initString)
            Dim fileName As String = Path.GetFileName(initString)
            SaveFileDialog1.InitialDirectory = pth
            'Dim fileName As String

            'If Path.Equals(destPath, pth) Then fileName = regFlag.title & ".gif" ' it's a level 0
            'If regFlag.imgurl.Contains(flegsBaseUrl) Then fileName = regFlag.title & ".png" ' it's not a level 0

            SaveFileDialog1.FileName = fileName
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then image.Save(SaveFileDialog1.FileName)
        Catch ex As Exception
            MsgBox("Error while downloading the flag to save", , "Flag Miner")
        End Try
    End Sub

    Private Sub CopyLinkHandler(sender As Object, e As EventArgs)
        Dim regFlag As RegionalFleg = DirectCast(DirectCast(sender, ToolStripMenuItem).Owner.Tag, RegionalFleg)
        My.Computer.Clipboard.SetText(regFlag.thread)
    End Sub

    Private Sub optButt_Click(sender As Object, e As EventArgs) Handles optButt.Click
        OptionsForm.ShowDialog()
        SetupForIdle()
    End Sub

    Private Sub intCheck_CheckedChanged(sender As Object, e As EventArgs) Handles intCheck.CheckedChanged
        options.intCheck = intCheck.Checked
    End Sub

    Private Sub polCheck_CheckedChanged(sender As Object, e As EventArgs) Handles polCheck.CheckedChanged
        options.polCheck = polCheck.Checked
    End Sub

    Private Sub spCheck_CheckedChanged(sender As Object, e As EventArgs) Handles spCheck.CheckedChanged
        options.spCheck = spCheck.Checked
    End Sub

    Private Sub ValidateOptions()
        If String.IsNullOrEmpty(options.userAgent) Then
            options.userAgent = DefaultUserAgent
        End If
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Dim parent As Control = sender
        If parent Is Nothing Then Return

        Dim ctrl As Control = parent.GetChildAtPoint(e.Location)
        If (ctrl IsNot Nothing AndAlso Not ctrl.Enabled) Then
            If (ctrl.Visible AndAlso ctrl.Equals(checkbutt)) Then
                If (ToolTip1.Tag Is Nothing AndAlso ToolTip1.Active) Then
                    Dim tipstring As String = ToolTip1.GetToolTip(ctrl)
                    ToolTip1.Show(tipstring, ctrl, ctrl.Width / 2, ctrl.Height / 2)
                    ToolTip1.Tag = ctrl
                End If
            End If
            If (ctrl.Visible AndAlso ctrl.Equals(purgebutt)) Then
                If (ToolTip2.Tag Is Nothing AndAlso ToolTip2.Active) Then
                    Dim tipstring As String = ToolTip2.GetToolTip(ctrl)
                    ToolTip2.Show(tipstring, ctrl, ctrl.Width / 2, ctrl.Height / 2)
                    ToolTip2.Tag = ctrl
                End If
            End If
        Else
            ctrl = ToolTip1.Tag
            If (ctrl IsNot Nothing) Then
                ToolTip1.Hide(ctrl)
                ToolTip1.Tag = Nothing
            End If
            ctrl = ToolTip2.Tag
            If (ctrl IsNot Nothing) Then
                ToolTip2.Hide(ctrl)
                ToolTip2.Tag = Nothing
            End If
        End If
    End Sub


    ''' <summary>
    ''' subtracts src from dest: dest = dest-src
    ''' </summary>
    ''' <param name="src">subtrahend</param>
    ''' <param name="dest">minuend</param>
    ''' <remarks></remarks>
    Public Sub subtractFlegs(ByVal src As SerializableDictionary(Of String, RegionalFleg), ByVal dest As SerializableDictionary(Of String, RegionalFleg))
        Dim curDestDict As SerializableDictionary(Of String, RegionalFleg) = dest
        For Each ke In curDestDict
            Dim Fleg As RegionalFleg = ke.Value
            Dim curSrcDict As SerializableDictionary(Of String, RegionalFleg) = src

            If Not curSrcDict.ContainsKey(ke.Key) Then
                Continue For  ' nothing to subtract
            Else
                subtractFlegs(curSrcDict(ke.Key).children, curDestDict(ke.Key).children)
                If Fleg.children.Count = 0 Then
                    curDestDict(ke.Key).markedfordeletion = True
                    Continue For
                End If
            End If
        Next
        curDestDict.Where(Function(pair As KeyValuePair(Of String, RegionalFleg)) pair.Value.markedfordeletion = True).ToArray(). _
        Apply(Function(pair As KeyValuePair(Of String, RegionalFleg)) curDestDict.Remove(pair.Key)).Apply()
    End Sub

    Private Sub subtractButt_Click(sender As Object, e As EventArgs) Handles subtractButt.Click
        If MsgBox("Import the flags to subtract from the current tree? The action cannot be undone.", MsgBoxStyle.YesNo, "Flag Miner") = MsgBoxResult.Yes Then
            OpenXmlDialog.InitialDirectory = options.saveAndLoadFolder
            If OpenXmlDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim currentFile As String = OpenXmlDialog.FileName
                Dim temptree As SerializableDictionary(Of String, RegionalFleg)
                Dim fs As FileStream
                Try
                    fs = New FileStream(currentFile, FileMode.Open)
                    Dim treeSerializer As New XmlSerializer(GetType(SerializableDictionary(Of String, RegionalFleg)))
                    temptree = treeSerializer.Deserialize(fs)
                    subtractFlegs(temptree, MainTree)
                Catch ex As Exception
                Finally
                    If fs IsNot Nothing Then fs.Close()
                End Try

                Task.Run(New Action(AddressOf CacheFlegs))

                UpdateRoots()
                TreeListView1.Invalidate()
            End If
        End If
    End Sub

End Class
