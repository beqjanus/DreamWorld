Imports System.Net
Imports System.IO
Imports System.Security
Imports System.Threading

' Copyright 2014 Fred Beckhusen   AGPL Licensed
' Redistribution and use in binary and source form is permitted provided
' that ALL the licenses in the text files are followed and included in all copies

Public Class Downloader


#Region "Private Fields"

    ReadOnly debugfolder = "C:\Users\Fred\Downloads\dg"
    Dim gCurDir = Nothing
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
    Private Function Random() As String

        Dim value As Integer = CInt(Int((600000000 * Rnd(System.DateTime.Now.Millisecond)) + 1))
        Random = System.Convert.ToString(value, Globalization.CultureInfo.InvariantCulture)

    End Function
    Private Sub Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.FormClosing

        DialogResult = DialogResult.OK

    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = 200
        Me.Top = 300
        MyFolder = My.Application.Info.DirectoryPath
        Label1.Text = ""
        If Debugger.IsAttached = True Then
            MyFolder = debugfolder ' for testing, as the compiler buries itself in ../../../debug
            If Not IO.Directory.Exists(MyFolder) Then
                IO.Directory.CreateDirectory(MyFolder)
            End If
            ChDir(MyFolder)
        End If

        ' Fix for Windows 7
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim Filename As String = ""
        whereToSave = MyFolder & "\" & "DreamGrid.zip"
        IO.File.Delete(MyFolder & "\Updater.log")
        Log($"Downloading {whereToSave}")

        'Creating the request and getting the response
        Label1.Text = "Downloading DreamGrid.zip"

        Dim client As New WebClient()
        ' Holds the current folder that we are running in
        Dim gFileName As String = "http://www.outworldz.com/Outworldz_Installer/Grid/DreamGrid.zip?r=" & Random()


        Dim urlContents As Byte() = Await GetURLContentsAsync(gFileName)

        Using DestinationStream As New IO.FileStream(whereToSave, IO.FileMode.Create)
            DestinationStream.Write(urlContents, 0, urlContents.Length)
            Log("Saved " & urlContents.Length.ToString)
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
