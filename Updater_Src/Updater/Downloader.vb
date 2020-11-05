Imports System.Net
Imports System.IO
Imports System.Security
Imports System.Threading

' Copyright 2014 Fred Beckhusen   AGPL Licensed
' Redistribution and use in binary and source form is permitted provided
' that ALL the licenses in the text files are followed and included in all copies

Public Class Downloader

#Region "Private Fields"

    Dim Cancelled As Boolean = False
    Dim debugfolder = "C:\Opensim\TestDreamgridInstaller"
    Dim gCurDir = Nothing

    ' Holds the current folder that we are running in
    Dim gFileName As String = "https://www.outworldz.com/Outworldz_installer/Grid/DreamGrid.zip"
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
            Using outputFile As New StreamWriter(MyFolder & "\Updater.log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message)
            End Using
        Catch
        End Try

    End Sub

#End Region

#Region "Private Methods"

    Private Sub Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.FormClosing

        DialogResult = DialogResult.OK

    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MyFolder = My.Application.Info.DirectoryPath
        Label1.Text = ""
        If Debugger.IsAttached = True Then
            MyFolder = debugfolder ' for testing, as the compiler buries itself in ../../../debug
            If Not IO.Directory.Exists(MyFolder) Then
                IO.Directory.CreateDirectory(MyFolder)
            End If
            ChDir(MyFolder)
        End If

        Dim Filename As String = ""
        whereToSave = MyFolder & "\" & "DreamGrid.zip"
        IO.File.Delete(MyFolder & "\Updater.log")
        Log("Downloading " + Filename)

        'Creating the request and getting the response
        Label1.Text = "Downloading " + Filename

        Dim client As New WebClient()
        Dim urlContents As Byte() = Await GetURLContentsAsync(gFileName)

        Using DestinationStream As New IO.FileStream(whereToSave, IO.FileMode.Create)
            DestinationStream.Write(urlContents, 0, urlContents.Length)
        End Using
        Log("Created " & whereToSave)
        DialogResult = DialogResult.OK

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
