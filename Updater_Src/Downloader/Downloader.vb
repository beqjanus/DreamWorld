
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

    Dim debugfolder = "C:\tmp\Testing" ''-debug' forces this to use the a different folder for testing 

    Dim Cancelled As Boolean = False
    Dim Type As String = "Downloader"
    Dim gCurDir = Nothing   ' Holds the current folder that we are running in
    Dim gFileName As String = "https://www.outworldz.com/Outworldz_installer/Grid/DreamGrid.zip"
    Dim whereToSave As String = Nothing 'Where the program saves the file


    Public Property MyFolder() As String
        Get
            Return gCurDir
        End Get
        Set(ByVal Value As String)
            gCurDir = Value
        End Set
    End Property

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        MyFolder = My.Application.Info.DirectoryPath
        ' I would like to buy an argument
        Dim arguments As String() = Environment.GetCommandLineArgs()
        If Debugger.IsAttached = True Then
            MyFolder = debugfolder ' for testing, as the compiler buries itself in ../../../debug
            ChDir(MyFolder)
            ' gFileName = "https://www.outworldz.com/Outworldz_Installer/Grid/revisions.txt"
        End If


        Dim Filename As String = ""
        Dim args() As String = System.Environment.GetCommandLineArgs()
        If args.Length = 2 Then
            Filename = args(1)
        End If
        If Filename.StartsWith("DreamGrid-V") Then
            whereToSave = Filename
            IO.File.Delete(MyFolder & "\" + Type + ".log")
            Log("Downloading Version " + Filename)
            Dim Name1 As String = "https://www.outworldz.com/Outworldz_Installer/Downloader/DotNetZip.dll"
            Dim Name2 As String = "https://www.outworldz.com/Outworldz_Installer/Downloader/DotNetZip.xml"
            Try
                My.Computer.FileSystem.DeleteDirectory(MyFolder + "\Outworldzfiles\opensim\bin\addin-db-002", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch
            End Try

            Dim client As New WebClient()

            Try
                Label1.Text = "Downloading Tools"
                Application.DoEvents()
                client.Credentials = New NetworkCredential("", "")
                client.DownloadFile(Name1, "DotNetZip.dll")
                Log("DotNetZip.dll loaded")
                client.DownloadFile(Name2, "DotNetZip.xml")
                Log("DotNetZip.xml loaded")

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

            Application.DoEvents()

            Label1.Text = "Stopping MySQL"
            StopMYSQL()

            Label1.Text = "Stopping Apache"
            StopApache()

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
            Using DestinationStream As New IO.FileStream(whereToSave, IO.FileMode.Create)
                DestinationStream.Write(urlContents, 0, urlContents.Length)
            End Using
            Thread.Sleep(5000)
            Environment.Exit(0)

        Else
            MsgBox("Syntax: Downloader DreamGrid-Vn.n.zip")
            Environment.Exit(1)
        End If

    End Sub

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


    Public Sub Log(message As String)
        Try
            Using outputFile As New StreamWriter(MyFolder & "\" + Type + ".log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message)
            End Using
        Catch
        End Try

    End Sub

    Private Sub StopMYSQL()

        Log("Info:using mysqladmin to close db")
        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = "-u root shutdown"
        pi.FileName = MyFolder + "\OutworldzFiles\mysql\bin\mysqladmin.exe"
        pi.WindowStyle = ProcessWindowStyle.Minimized
        p.StartInfo = pi
        Try
            p.Start()
        Catch
            Log("Warning:mysqladmin failed to stop mysql. This could be because it was not running")
        End Try

    End Sub


    Private Sub StopApache()

        Using ApacheProcess As New Process()
            ApacheProcess.StartInfo.FileName = "sc"
            ApacheProcess.StartInfo.Arguments = "stop " & "ApacheHTTPServer"
            ApacheProcess.Start()
            Application.DoEvents()
            ApacheProcess.WaitForExit()
        End Using

        Zap("httpd")
        Zap("rotatelogs")

    End Sub

    Private Sub Zap(processName As String)

        ' Kill process by name
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName(processName)
            Try
                P.Kill()
            Catch
            End Try
        Next

    End Sub


End Class


