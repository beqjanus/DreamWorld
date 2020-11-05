Imports System.Net
Imports System.IO

Imports Ionic.Zip
Imports System.Threading

' Copyright 2019 Fred Beckhusen
' AGPL 3.0 Licensed
' Redistribution and use in binary and source form is permitted provided
' that ALL the licenses in the text files are followed and included in all copies

Public Class UpdateGrid

#Region "Private Fields"

    Dim Filename As String = "DreamGrid.zip"
    Dim MyFolder As String = ""

#End Region

#Region "Public Methods"

    Public Sub TextPrint(ByVal str As String)
        Label1.Text = str
    End Sub

#End Region

#Region "Private Methods"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        EnsureInitialized()

        Label1.Text = "DreamGrid Updater"
        Me.Text = "Outworldz DreamGrid Setup"
        Me.Show()
        Application.DoEvents()
        MyFolder = My.Application.Info.DirectoryPath
        If Debugger.IsAttached = True Then
            MyFolder = "C:\Opensim\TestDreamgridInstaller"
            If Not IO.Directory.Exists(MyFolder) Then
                IO.Directory.CreateDirectory(MyFolder)
            End If
            ' for testing, as the compiler buries itself in ../../../debug
        End If

        ChDir(MyFolder)


        Dim f1 = New Downloader

        Dim ret = Downloader.ShowDialog()
        If ret = DialogResult.OK Then

            If Not File.Exists(MyFolder & "\" & Filename) Then
                MsgBox("File not found. Aborting." & vbCrLf & "Syntax: DreamGridSetup.exe  Dreamgrid-Vn.n.zip")
                End
            Else

                Dim result = MsgBox("Ready to update your system. Proceed?", vbYesNo)
                If result = vbNo Then End

                Application.DoEvents()

                Label1.Text = "Stopping MySQL"
                StopMYSQL()
                Label1.Text = "Stopping Apache"
                StopApache()

                Try
                    My.Computer.FileSystem.DeleteDirectory(MyFolder & "\Outworldzfiles\opensim\bin\addin-db-002", FileIO.DeleteDirectoryOption.DeleteAllContents)
                Catch ex As Exception
                End Try
                Dim err As Integer = 0
                Dim fname As String = ""
                Dim ctr As Integer
                Try
                    Using zip As ZipFile = ZipFile.Read(MyFolder & "\" & Filename)
                        For Each ZipEntry In zip
                            ctr = ctr + 1
                            fname = ZipEntry.FileName

                            File.Delete(Path.GetFileName(ZipEntry.FileName) & ".PendingOverwrite")
                            File.Delete(Path.GetFileName(ZipEntry.FileName) & ".tmp")

                            If ZipEntry.FileName <> "Ionic.Zip.dll" And ZipEntry.FileName <> "DreamGridUpdater.exe" Then
                                TextPrint("Extracting " + Path.GetFileName(ZipEntry.FileName))
                                Application.DoEvents()
                                ZipEntry.Extract(MyFolder, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                            End If
                        Next
                    End Using
                Catch ex As Exception
                    TextPrint("Unable to extract file: " & fname & ":" & ex.Message)
                    Thread.Sleep(3000)
                    err += 1
                End Try
                Application.DoEvents()
                If Not err Then TextPrint("Completed!")
                If Not err Then
                    Try
                        My.Computer.FileSystem.RenameFile(MyFolder & "\Ionic.Zip.proto", "Ionic.Zip.dll")
                    Catch ex As Exception
                    End Try
                End If
                Application.DoEvents()
                Thread.Sleep(3000)
                End
            End If
        Else
            TextPrint("Cancelled!")
        End If

        End

    End Sub

    Private Sub StopApache()

        Using ApacheProcess As New Process()
            ApacheProcess.StartInfo.FileName = "net"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            ApacheProcess.StartInfo.Arguments = "stop " & "ApacheHTTPServer"
            ApacheProcess.Start()
            Application.DoEvents()
            ApacheProcess.WaitForExit()
        End Using

        Zap("httpd")
        Zap("rotatelogs")

    End Sub

    Private Sub StopMYSQL()

        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = "-u root shutdown"
        pi.FileName = MyFolder + "\OutworldzFiles\mysql\bin\mysqladmin.exe"
        pi.WindowStyle = ProcessWindowStyle.Minimized
        p.StartInfo = pi
        Try
            p.Start()
        Catch
        End Try

        pi.Arguments = "-u root -port=3306 shutdown"
        p.StartInfo = pi
        Try
            p.Start()
        Catch
        End Try
        pi.Arguments = "-u root -port=3309 shutdown"
        p.StartInfo = pi
        Try
            p.Start()
        Catch
        End Try

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

#End Region

End Class
