Imports BrightIdeasSoftware
Imports System.Net
Imports System.IO
Imports System.Collections.Concurrent
Imports System.Xml.Serialization
Imports System.Runtime.CompilerServices
Imports System.Threading

Public Class ChanCatalog
    Public ThreadArray As String()
End Class

Public Class ChanThread
    Public posts As List(Of Post)
End Class


Public Class Post
    '' deserializer looks for proper field names to deserialize json fields
    Public no As Int64
    Public resto As Int64
    Public sticky As Int64
    Public closed As Int64
    Public archived As Int64
    Public archived_on As Int64
    Public now As String
    Public time As Int64
    Public name As String
    Public trip As String
    Public id As String
    Public capcode As String
    Public country As String
    Public country_name As String
    Public [sub] As String    ' escape the keyword
    Public com As String
    Public tim As Int64
    Public filename As String
    Public ext As String
    Public fsize As Int64
    Public md5 As String
    Public w As Int64
    Public h As Int64
    Public tn_w As Int64
    Public tn_h As Int64
    Public filedeleted As Int64
    Public spoiler As Int64
    Public custom_spoiler As Int64
    Public omitted_posts As Int64
    Public omitted_images As Int64
    Public replies As Int64
    Public images As Int64
    Public bumplimit As Int64
    Public imagelimit As Int64
    'Public capcode_replies As Array   '' fcking deserializer
    Public last_modified As Int64
    Public tag As String
    Public semantic_url As String
    Public since4pass As Int64
    Public troll_country As String

End Class

Public Class Fleg
    Public post_nr As Int64
    Public region As String
End Class

Public Class CompleteFleg
    Public flegs As List(Of RegionalFleg)
    Public isTrollFlag As Boolean
End Class

<Serializable()> _
Public Class RegionalFleg
    Public isTrollFlag As Boolean
    Public imgurl As String
    Public title As String
    Public thread As String
    Public board As String
    Public pNo As String
    Public time As Long
    Public children As New SerializableDictionary(Of String, RegionalFleg)

    <XmlIgnore> _
    Public fetching As Boolean = False
    <XmlIgnore> _
    Public markedfordeletion As Boolean = False
    <XmlIgnore> _
    Public exists As Boolean = False
    <XmlIgnore> _
    Public status As Integer = 0   ' existing, invalid etc...
End Class

Public Enum PurgeEnum As Integer
    genericError = -2
    notFound = -1
    undefined = 0
    ok = 1
End Enum

Public Class PostComparer
    Implements IComparer(Of Post)

    Public Function Compare(ByVal x As Post, _
        ByVal y As Post) As Integer _
        Implements IComparer(Of Post).Compare

        If x Is Nothing Then
            If y Is Nothing Then
                Return 0
            Else
                Return -1
            End If
        Else
            If y Is Nothing Then
                Return 1
            Else
                Dim retval As Integer = _
                    x.no.CompareTo(y.no)
                Return retval
            End If
        End If
    End Function
End Class

Public Class FlegComparer
    Implements IComparer(Of Fleg)

    Public Function Compare(ByVal x As Fleg, _
        ByVal y As Fleg) As Integer _
        Implements IComparer(Of Fleg).Compare

        If x Is Nothing Then
            If y Is Nothing Then
                Return 0
            Else
                Return -1
            End If
        Else
            If y Is Nothing Then
                Return 1
            Else
                Dim retval As Integer = _
                    x.post_nr.CompareTo(y.post_nr)
                Return retval
            End If
        End If
    End Function
End Class

<Serializable> _
Public Structure Options
    Public intCheck As Boolean
    Public polCheck As Boolean
    Public spCheck As Boolean

    Public exclusionByList As Boolean
    Public exclusionByDate As Boolean
    Public exclusionDate As DateTime

    Public localSaveFolder As String
    Public localRepoFolder As String

    Public enableCheck As Boolean
    Public enablePurge As Boolean
    Public useLocal As Boolean
    Public markTroll As Boolean
    Public deleteChildFree As Boolean

    Public userAgent As String
    Public saveAndLoadFolder As String

End Structure


Public Class ImageListHelper
    Shared frm As Form1

    Private stack As BlockingCollection(Of String)
    Private consumer As Task

    Protected listView As ObjectListView

    Private Sub New(form As Form1)
        frm = form
    End Sub

    ''' <summary>
    ''' Create a SysImageListHelper that will fetch images for the given tree control
    ''' </summary>
    ''' <param name="listView">The tree view that will use the images</param>
    Public Sub New(listView As ObjectListView, source As ConcurrentQueue(Of String))
        If listView.SmallImageList Is Nothing Then
            listView.SmallImageList = New ImageList()
            'listView.ImageList.ImageSize = New Size(16, 16)
        End If
        Me.listView = listView
        ImageListHelper.frm = listView.Parent

        stack = New BlockingCollection(Of String)(source)
        consumer = Task.Run(Sub()
                                For Each path As String In stack.GetConsumingEnumerable()
                                    If Not (Me.SmallImageList.Images.ContainsKey(path)) Then
                                        Try
                                            Me.AddImageToCollection(path, Me.SmallImageList, ScrapeImage(path))
                                        Catch generatedExceptionName As ArgumentNullException
                                        End Try
                                    End If
                                    If stack.Count = 0 Then
                                        Thread.Sleep(200) ' fist run ok, but now wait a little to build the queue for the next run
                                        frm.RefreshTree()
                                    End If

                                Next
                            End Sub)
    End Sub

    Protected ReadOnly Property SmallImageCollection() As ImageList.ImageCollection
        Get
            If Me.listView IsNot Nothing Then
                Return Me.listView.SmallImageList.Images
            End If
            Return Nothing
        End Get
    End Property

    Protected ReadOnly Property SmallImageList() As ImageList
        Get
            If Me.listView IsNot Nothing Then
                Return Me.listView.SmallImageList
            End If
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Return the index of the image that has the Shell Icon for the given file/directory.
    ''' </summary>
    ''' <param name="path">The full path to the file/directory</param>
    ''' <returns>The index of the image or -1 if something goes wrong.</returns>
    Public Function GetImageIndex(path As String) As Integer
        Try
            If Me.SmallImageCollection.ContainsKey(path) Then
                'SyncLock messagesLock
                Return Me.SmallImageCollection.IndexOfKey(path)
                'End SyncLock
            End If
        Catch ex As Exception
        End Try

        'Try
        '    Me.AddImageToCollection(path, Me.SmallImageList, ScrapeImage(path))
        'Catch generatedExceptionName As ArgumentNullException
        '    Return -1
        'End Try

        'Return Me.SmallImageCollection.IndexOfKey(path)
    End Function


    Public Function HasImage(path As String) As Boolean
        Try
            'SyncLock messagesLock
            Return Me.SmallImageCollection.ContainsKey(path)
            'End SyncLock
        Catch ex As Exception
        End Try
        Return False
    End Function

    Public Sub Clear()
        SyncLock messagesLock
            listView.BaseSmallImageList.Images.Clear()
            listView.SmallImageList.Images.Clear()
        End SyncLock
    End Sub

    Public Sub AddToStack(Path As String)
        stack.Add(Path)
    End Sub

    Public Sub AddToStack(Paths As List(Of String))
        For Each st In Paths
            stack.Add(st)
        Next
        'stack.Union(Paths)
    End Sub


    Shared Function ScrapeImage(url As String) As Image
        Dim img As System.Drawing.Bitmap

        'Dim finalimg As Image
        Dim wc As New WebClient()
        Try
            Dim bytes As Byte() = wc.DownloadData(url)
            Dim ms As New MemoryStream(bytes)
            img = System.Drawing.Image.FromStream(ms)

        Catch ex As WebException
            Dim resp = DirectCast(ex.Response, HttpWebResponse)
            If resp.StatusCode = HttpStatusCode.NotFound Then
                img = frm.blankImg
            End If
        End Try

        Return img
    End Function

    Private messagesLock As New Object

    Public Sub AddImageToCollection(key As String, imageList As ImageList, img As Image)
        If imageList Is Nothing Or img Is Nothing Then
            Return
        End If
        SyncLock messagesLock
            Try
                Dim finalimg As Image
                If img.Size = Me.SmallImageList.ImageSize Then
                    finalimg = img
                Else

                    Dim maxSize As Size
                    maxSize.Width = Math.Max(img.Width, Me.SmallImageList.ImageSize.Width)
                    maxSize.Height = Math.Max(img.Height, Me.SmallImageList.ImageSize.Height)

                    Dim tempList As New ImageList
                    tempList.ImageSize = New Size(maxSize.Width, maxSize.Height)
                    Dim g As Graphics

                    For Each ke As String In Me.SmallImageList.Images.Keys
                        Dim savedImg As Image = Me.SmallImageList.Images(ke)
                        Dim tempbmp As New Bitmap(maxSize.Width, maxSize.Height)

                        g = Graphics.FromImage(tempbmp)
                        'g.Clear(Color.White)
                        g.DrawImage(savedImg, New Rectangle(0, 0, savedImg.Width, savedImg.Height), _
                                    New Rectangle(0, 0, savedImg.Width, savedImg.Height), GraphicsUnit.Pixel)
                        ' sostituisci
                        tempList.Images.Add(ke, tempbmp)
                    Next

                    ' copia
                    Me.SmallImageList.Images.Clear()
                    Dim myform As Form1 = DirectCast(Me.listView.Parent, Form1)
                    myform.SetImgSizeInvoker(New Size(maxSize.Width, maxSize.Height))
                    'Me.SmallImageList.ImageSize = New Size(maxSize.Width, maxSize.Height)
                    For Each ke As String In tempList.Images.Keys
                        Me.SmallImageList.Images.Add(ke, tempList.Images(ke))
                    Next

                    ' sistema nuova
                    Dim newBitmap As Bitmap = New Bitmap(maxSize.Width, maxSize.Height)
                    Dim gr As Graphics = Graphics.FromImage(newBitmap)
                    gr.DrawImage(img, 0, 0, img.Width, img.Height)
                    finalimg = newBitmap

                End If
                imageList.Images.Add(key, finalimg)
            Catch ex As Exception
            End Try
        End SyncLock

    End Sub

End Class


Public Class MergeManager
    Private stack As BlockingCollection(Of SerializableDictionary(Of String, RegionalFleg))
    Private consumer As Task

    Private treeView As TreeListView

    Sub New(source As ConcurrentQueue(Of SerializableDictionary(Of String, RegionalFleg)), ByRef dest As SerializableDictionary(Of String, RegionalFleg), myTreeView As TreeListView)
        stack = New BlockingCollection(Of SerializableDictionary(Of String, RegionalFleg))(source)
        treeView = myTreeView
        Dim frm As Form1 = treeView.Parent
        Dim dict As SerializableDictionary(Of String, RegionalFleg) = dest
        consumer = Task.Run(Sub()
                                For Each myObj As SerializableDictionary(Of String, RegionalFleg) In stack.GetConsumingEnumerable()
                                    For Each Fleg As RegionalFleg In myObj.Values
                                        Dim curDict As SerializableDictionary(Of String, RegionalFleg) = dict
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
                                                Form1.merger(curSrcDict, curDestDict)
                                            End If
                                        End If
                                    Next

                                    Thread.Sleep(200)
                                    If stack.Count = 0 Then
                                        frm.UpdateRootsInvoker()
                                        'frm.updateManager.AddToStack(Tuple.Create(Of String, Object)("ut", myTreeView))
                                    End If

                                Next
                            End Sub)
    End Sub


    Public Sub AddToStack(obj As SerializableDictionary(Of String, RegionalFleg))
        stack.Add(obj)
    End Sub

End Class


Public Class UpdateManager
    Private stack As BlockingCollection(Of Object)
    Private consumer As Task

    Private treeView As TreeListView

    Sub New(source As ConcurrentQueue(Of Object), myTreeView As TreeListView)
        stack = New BlockingCollection(Of Object)(source)
        treeView = myTreeView
        Dim frm As Form1 = treeView.Parent
        consumer = Task.Run(Sub()
                                Dim accumulator As New List(Of Object)
                                For Each myObj As Object In stack.GetConsumingEnumerable()
                                    accumulator.Add(myObj)
                                    If stack.Count = 0 Then
                                        'accumulator.Distinct()
                                        'frm.UpdateTreeViewInvoker(accumulator)
                                        treeView.Invalidate()
                                        accumulator.Clear()
                                        Thread.Sleep(200) ' fist run ok, but now wait a little to build the queue for the next run
                                    End If

                                Next
                            End Sub)
    End Sub


    Public Sub AddToStack(obj As Object)
        stack.Add(obj)
    End Sub

End Class

'https://weblogs.asp.net/pwelter34/444961
<XmlRoot("dictionary")> _
Public Class SerializableDictionary(Of TKey, TValue)

    Inherits SortedDictionary(Of TKey, TValue)
    Implements IXmlSerializable

#Region "IXmlSerializable Members"

    Public Function GetSchema() As System.Xml.Schema.XmlSchema Implements IXmlSerializable.GetSchema
        Return Nothing
    End Function


    Public Sub ReadXml(reader As System.Xml.XmlReader) Implements IXmlSerializable.ReadXml

        Dim keySerializer As XmlSerializer = New XmlSerializer(GetType(TKey))
        Dim valueSerializer As XmlSerializer = New XmlSerializer(GetType(TValue))

        Dim wasEmpty As Boolean = reader.IsEmptyElement

        reader.Read()

        If (wasEmpty) Then Return

        While (reader.NodeType <> System.Xml.XmlNodeType.EndElement)

            reader.ReadStartElement("item")
            reader.ReadStartElement("key")

            Dim key As TKey = DirectCast(keySerializer.Deserialize(reader), TKey)

            reader.ReadEndElement()
            reader.ReadStartElement("value")

            Dim value As TValue = DirectCast(valueSerializer.Deserialize(reader), TValue)

            reader.ReadEndElement()

            Me.Add(key, value)
            reader.ReadEndElement()
            reader.MoveToContent()
        End While

        reader.ReadEndElement()

    End Sub

    Public Sub WriteXml(writer As System.Xml.XmlWriter) Implements IXmlSerializable.WriteXml
        Dim keySerializer As XmlSerializer = New XmlSerializer(GetType(TKey))
        Dim valueSerializer As XmlSerializer = New XmlSerializer(GetType(TValue))

        For Each key As TKey In Me.Keys
            writer.WriteStartElement("item")
            writer.WriteStartElement("key")

            keySerializer.Serialize(writer, key)
            writer.WriteEndElement()
            writer.WriteStartElement("value")

            Dim value As TValue = Me(key)

            valueSerializer.Serialize(writer, value)

            writer.WriteEndElement()
            writer.WriteEndElement()

        Next

    End Sub

#End Region

End Class


' array mangling
Module enumExtensions
    <Extension()> _
    Public Function Apply(Of T)(ByVal source As IEnumerable(Of T), ByVal action As Action(Of T)) As IEnumerable(Of T)
        'Argument.NotNull(source, Function() Function() source)
        'Argument.NotNull(action, Function() Function() action)
        Return ApplyInternal(source, action)
    End Function

    Friend Function ApplyInternal(Of T)(ByVal source As IEnumerable(Of T), ByVal action As Action(Of T)) As IEnumerable(Of T)
        Dim res As New List(Of T)
        For Each e As Object In source
            action(e)
            res.Add(e)
        Next
        Return res
    End Function

    <Extension()> _
    Public Sub Apply(Of T)(ByVal source As IEnumerable(Of T))
        For Each e As Object In source
            '' do nothing, just make sure the elements are enumerated.
        Next
    End Sub
End Module