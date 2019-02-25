
Imports System.Net
    Imports System.IO
    Imports Ionic.Zip
    Imports System.Threading

' Copyright 2014 Fred Beckhusen   AGPL Licensed
' Redistribution and use in binary and source form is permitted provided 
' that ALL the licenses in the text files are followed and included in all copies

' Command line args:
'     '-debug' forces this to use the a different folder for testing 

Public Class UpdateGrid

    Dim debugfolder = "f:/tmp" ''-debug' forces this to use the a different folder for testing 

    Dim Cancelled As Boolean = False
    Dim Version As String = "4"
    'Dim Type As String = "DreamGrid-Update.zip"  ' possible server-side choices are "Update" and "Installer"
    Dim Type As String = "DreamGrid.zip"  ' possible server-side choices are "Update" and "Installer"

    Dim gCurDir = Nothing   ' Holds the current folder that we are running in
    Dim gFileName As String = Nothing
    Dim whereToSave As String = Nothing 'Where the program save the file

    Delegate Sub ChangeTextsSafe(ByVal length As Long, ByVal position As Integer, ByVal percent As Integer, ByVal speed As Double)
    Delegate Sub TextPrintSafe(ByVal str As String)

    Public Property MyFolder() As String
        Get
            Return gCurDir
        End Get
        Set(ByVal Value As String)
            gCurDir = Value
        End Set
    End Property

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "Outworldz " + Type + " V" + Version
        MyFolder = My.Application.Info.DirectoryPath
        ' I would like to buy an argument
        Dim arguments As String() = Environment.GetCommandLineArgs()
        If arguments.Length > 1 Then
            ' for debugging when compiling
            If arguments(1) = "-debug" Then
                MyFolder = debugfolder ' for testing, as the compiler buries itself in ../../../debug
                ChDir(MyFolder)
            End If
        End If

        IO.File.Delete(MyFolder & "\" + Type + ".log")
        Log("Version " + Version)
        Dim Name1 As String = "https://www.outworldz.com/Outworldz_Installer/Downloader/DotNetZip.dll"
        Dim Name2 As String = "https://www.outworldz.com/Outworldz_Installer/Downloader/DotNetZip.xml"

        Try
            Label1.Text = "Downloading Tools"
            Application.DoEvents()
            Dim client As WebClient
            client = New WebClient()
            client.Credentials = New NetworkCredential("", "")
            client.DownloadFile(Name1, "DotNetZip.dll")
            Log("DotNetZip.dll loaded")
            client.DownloadFile(Name2, "DotNetZip.xml")
            Log("DotNetZip.xml loaded")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        Application.DoEvents()

        Label1.Text = "Checking MySQL"
        Label2.Text = ""
        Label3.Text = ""

        StopMYSQL()

        gFileName = "http://www.outworldz.com/Outworldz_Installer/Grid/" + Type
        Log("Starting Background Downloader")
        btnCancel.Visible = True

        BackgroundWorker1.RunWorkerAsync() 'Start download

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Log("Cancel requested")
        Try
            BackgroundWorker1.CancelAsync() 'Send cancel request
            Cancelled = True
        Catch
        End Try

        End

    End Sub

    Public Sub TextPrint(ByVal str As String)
        Label1.Text = "Unzipping " + Type + ", please wait..."
        Label2.Text = str
        Label3.Text = ""
    End Sub

    Public Sub ChangeTexts(ByVal length As Long, ByVal position As Integer, ByVal percent As Integer, ByVal speed As Double)

        Application.DoEvents()

        Try
            Label1.Text = "Downloading " + Type + ", please wait..."
            'Label3.Text = "File Size: " & Math.Round((length / 1024), 2) & " KB"
            Label2.Text = "Downloaded " & Math.Round((position / 1024), 2) & " KB of " & Math.Round((length / 1024), 2) & "KB "

            Application.DoEvents()
            If speed = -1 Then
                Label3.Text = "Speed: calculating..."
            Else
                Label3.Text = "Speed: " & Math.Round((speed / 1024), 2) & " KB/s"
            End If

            Application.DoEvents()
        Catch ex As Exception
            MsgBox("Exception: " + ex.Message)
            Log("Exception: " + ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        'Creating the request and getting the response
        Log("Background running")
        Dim theResponse As HttpWebResponse
        Dim theRequest As HttpWebRequest
        Try 'Checks if the file exist
            theRequest = WebRequest.Create(gFileName)
            theRequest.Proxy = Nothing
            Debug.Print(gFileName)
            theResponse = theRequest.GetResponse

        Catch ex As Exception
            Log(ex.Message)
            MessageBox.Show("An error occurred while downloading file. Possible causes:" & ControlChars.CrLf &
                    "1) File doesn't exist" & ControlChars.CrLf &
                    "2) Remote server error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try
        Dim length As Long = theResponse.ContentLength 'Size of the response (in bytes)
        If length = 0 Then
            Log("File len = 0")
            MessageBox.Show("An error occurred while downloading file. Possible causes:" & ControlChars.CrLf &
                   "1) File doesn't exist" & ControlChars.CrLf &
                   "2) Remote server error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If

        Dim safedelegate As New ChangeTextsSafe(AddressOf ChangeTexts)
        Invoke(safedelegate, length, 0, 0, 0) 'Invoke the TreadsafeDelegate

        whereToSave = MyFolder + "\" + Type
        Log("Saving to " + whereToSave)

        Dim writeStream As New IO.FileStream(whereToSave, IO.FileMode.Create)

        'Replacement for Stream.Position (webResponse stream doesn't support seek)

        'To calculate the download speed
        Dim nRead As Integer
        Dim currentspeed As Double = -1
        Dim readStream As Stream = theResponse.GetResponseStream
        Dim lastTickCount As Integer = Environment.TickCount
        Dim bytesReadPerInterval As Integer
        Dim readBytes(32 * 1024) As Byte
        Dim bytesread As Integer
        Dim newTickCount As Integer
        Dim percent As Short

        Do
            Application.DoEvents()

            If BackgroundWorker1.CancellationPending Then 'If user abort download
                Cancelled = True
                Exit Do
            End If

            bytesread = readStream.Read(readBytes, 0, readBytes.Length)

            Application.DoEvents()

            nRead += bytesread
            bytesReadPerInterval += bytesread

            percent = nRead / length

            Invoke(safedelegate, length, nRead, percent, currentspeed)
            Application.DoEvents()
            If bytesread = 0 Then
                Debug.Print("Exit do")
                Exit Do
            End If

            writeStream.Write(readBytes, 0, bytesread)

            newTickCount = Environment.TickCount
            If newTickCount - lastTickCount > 2000 Then
                'BackgroundWorker1.ReportProgress(nRead, "Downloading...")
                Dim tickDiff As Double = newTickCount - lastTickCount
                lastTickCount = newTickCount
                currentspeed = bytesReadPerInterval / (tickDiff / 1000.0)
                bytesReadPerInterval = 0
            End If
        Loop

        Log("Close stream")
        readStream.Close()
        writeStream.Close()

        If Cancelled Then
            Log("Cancelled: " + whereToSave)
            IO.File.Delete(whereToSave)
            MessageBox.Show("Download aborted", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Log("Prepping for install")

        Dim safeprint As New TextPrintSafe(AddressOf TextPrint)
        Invoke(safeprint, "") 'Invoke the TreadsafeDelegate

        Try
            My.Computer.FileSystem.DeleteDirectory(MyFolder + "\Outworldzfiles\opensim\bin\addin-db-002", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch
        End Try

        Dim ctr As Integer
        Try
            Log("Opening zip " + whereToSave)
            Using zip As ZipFile = ZipFile.Read(whereToSave)
                Invoke(safeprint, "Received " + Str(zip.Entries.Count) + " files. ")
                Thread.Sleep(1000)
                Log("Received " + Str(zip.Entries.Count) + " files. Extracting to disk.")
                For Each ZipEntry In zip
                    Application.DoEvents()
                    ctr = ctr + 1
                    If ZipEntry.FileName <> "DotNetZip.dll" Then
                        Invoke(safeprint, "Extracting " + Path.GetFileName(ZipEntry.FileName)) 'Invoke the TreadsafeDelegate
                        ZipEntry.Extract(MyFolder, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                    End If

                Next
            End Using
        Catch ex As Exception
            Log("Aborted: " + ex.Message)
            Invoke(safeprint, "Aborted: " + ex.Message)
        End Try
        Log("Extract Complete")

        Invoke(safeprint, "Completed!")

        Thread.Sleep(5000)
        Log("Exit Done")
        End

    End Sub
    Private Function Random() As String
        Randomize()
        Dim value As Integer = CInt(Int((6000 * Rnd()) + 1))
        Random = Str(value)
    End Function

    Public Function Log(message As String)
        Try
            Using outputFile As New StreamWriter(MyFolder & "\" + Type + ".log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message)
            End Using
        Catch
        End Try
        Return True

    End Function

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

End Class


