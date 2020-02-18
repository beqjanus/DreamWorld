Imports System.Net
Imports System.IO
Imports System.Security
Imports System.Threading

' Copyright 2014 Fred Beckhusen   AGPL Licensed
' Redistribution and use in binary and source form is permitted provided
' that ALL the licenses in the text files are followed and included in all copies

' Command line args:
'     '-debug' forces this to use the a different folder for testing

Public Class Downloader

#Region "Private Fields"

    Dim Cancelled As Boolean = False
    Dim debugfolder = "C:\tmp\Testing" ''-debug' forces this to use the a different folder for testing
    Dim gCurDir = Nothing

    ' Holds the current folder that we are running in
    Dim gFileName As String = "http://www.outworldz.com/Outworldz_installer/Grid/DreamGrid.zip"

    Dim Type As String = "Downloader"
    Dim whereToSave As String = Nothing

#End Region

    'Where the program saves the file

#Region "Public Properties"

    Public Property MyFolder() As String
        Get
            Return gCurDir
        End Get
        Set(ByVal Value As String)
            gCurDir = Value
        End Set
    End Property

#End Region

#Region "Public Methods"

    Public Sub Log(message As String)
        Try
            Using outputFile As New StreamWriter(MyFolder & "\" + Type + ".log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message)
            End Using
        Catch
        End Try

    End Sub

#End Region

#Region "Private Methods"

    Private Sub Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.FormClosing

        Log("Cancel requested")

        Cancelled = True
        Try
            My.Computer.FileSystem.DeleteFile(whereToSave)
        Catch
        End Try
        Label1.Text = "Cancelled"
        Thread.Sleep(2000)
        Environment.Exit(1)

    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MyFolder = My.Application.Info.DirectoryPath
        ' I would like to buy an argument
        Dim arguments As String() = Environment.GetCommandLineArgs()
        If Debugger.IsAttached = True Then
            MyFolder = debugfolder ' for testing, as the compiler buries itself in ../../../debug
            ChDir(MyFolder)
        End If

        Dim Filename As String = ""
        Dim args() As String = System.Environment.GetCommandLineArgs()
        If args.Length = 2 Then
            Filename = args(1)
        End If
        If Filename.StartsWith("DreamGrid-V") Then
            whereToSave = MyFolder & "\" & Filename
            IO.File.Delete(MyFolder & "\" & Type & ".log")
            Log("Downloading Version " + Filename)

            Dim client As New WebClient()

            Dim date1 = Date.Now
            'Creating the request and getting the response
            Label1.Text = "Downloading " + Filename
            Dim urlContents As Byte() = Await GetURLContentsAsync(gFileName)

            Dim date2 = Date.Now
            Dim Seconds As Long = DateDiff(DateInterval.Second, date1, date2)
            Dim Bytespersec As Single
            If Seconds > 0 Then
                Bytespersec = urlContents.Length / Seconds / 1024
            End If

            Label1.Text = "Downloaded " & Math.Ceiling(urlContents.Length / 1024 / 1024).ToString & " MB at " & Math.Ceiling(Bytespersec).ToString & " KB/Sec"
            Log(Label1.Text)
            Using DestinationStream As New IO.FileStream(whereToSave, IO.FileMode.Create)
                DestinationStream.Write(urlContents, 0, urlContents.Length)
            End Using
            Log("Created " & whereToSave)
            Thread.Sleep(5000)
            Environment.Exit(0)
        Else
            MsgBox("Syntax: Downloader DreamGrid-Vn.n.zip")
            Log("Syntax: Downloader DreamGrid-Vn.n.zip")
            Environment.Exit(1)
        End If

    End Sub

    Private Async Function GetURLContentsAsync(url As String) As Task(Of Byte())

        Dim content = New MemoryStream()
        ' Initialize an HttpWebRequest for the current URL.
        Dim webReq = CType(WebRequest.Create(url), HttpWebRequest)
        webReq.AllowAutoRedirect = True

        ' Send the request to the Internet resource and wait for
        ' the response.
        Using response As WebResponse = Await webReq.GetResponseAsync()
            ' Get the data stream that is associated with the specified URL.
            Using responseStream As Stream = response.GetResponseStream()
                ' Read the bytes in responseStream and copy them to content.
                Await responseStream.CopyToAsync(content)
            End Using
        End Using
        Return content.ToArray()

    End Function

#End Region

End Class
