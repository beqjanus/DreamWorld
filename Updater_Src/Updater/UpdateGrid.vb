Imports System.Net
Imports System.IO

Imports System.Threading
Imports System.IO.Compression

' Copyright 2019 Fred Beckhusen
' AGPL 3.0 Licensed
' Redistribution and use in binary and source form is permitted provided
' that ALL the licenses in the text files are followed and included in all copies

Public Class UpdateGrid
    Private WithEvents ApacheProcess As New Process()

#Region "Private Fields"

    ReadOnly Filename As String = "DreamGrid.zip"
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

        Label1.Text = "DreamGrid Updater/Installer"
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
                MsgBox("File not found. Aborting.")
                End
            Else

                Dim result = MsgBox("Ready to update your system. Proceed?", vbYesNo)
                If result = vbNo Then End

                Application.DoEvents()

                Label1.Text = "Stopping MySQL"
                StopMYSQL()
                Label1.Text = "Stopping Apache"
                StopApache()
                Label1.Text = "Stopping Apache"
                For Each p As Process In Process.GetProcessesByName("Robust")
                    p.Kill()
                Next
                Label1.Text = "Stopping Opensim"
                For Each p As Process In Process.GetProcessesByName("Opensim")
                    p.Kill()
                Next
                Label1.Text = "Stopping Icecast"
                For Each p As Process In Process.GetProcessesByName("Icecast")
                    p.Kill()
                Next

                Try
                    My.Computer.FileSystem.DeleteDirectory(MyFolder & "\Outworldzfiles\opensim\bin\addin-db-002", FileIO.DeleteDirectoryOption.DeleteAllContents)
                Catch ex As Exception
                End Try
                Dim err As Integer = 0
                Dim fname As String = ""

                Dim extractPath = Path.GetFullPath(MyFolder)
                If (Not extractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal)) Then
                    extractPath += Path.DirectorySeparatorChar
                End If

                Dim ZipContains As Integer = 0
                Dim counter As Integer = 0
                Try
                    Using zip As ZipArchive = ZipFile.Open(MyFolder & "\" & Filename, ZipArchiveMode.Read)
                        ZipContains = zip.Entries.Count
                        For Each ZipEntry In zip.Entries
                            counter += 1
                            fname = ZipEntry.Name
                            If fname.Length = 0 Then
                                Continue For
                            End If
                            If ZipEntry.Name <> "DreamGridUpdater.exe" Then
                                TextPrint(CStr(counter) & " of " & CStr(ZipContains) & ":" & Path.GetFileName(ZipEntry.Name))
                                Application.DoEvents()
                                Dim destinationPath As String = Path.GetFullPath(Path.Combine(extractPath, ZipEntry.FullName))
                                If File.Exists(destinationPath) Then
                                    File.Delete(destinationPath)
                                End If
                                Dim folder = Path.GetDirectoryName(destinationPath)
                                Directory.CreateDirectory(folder)
                                ZipEntry.ExtractToFile(folder & "\" & ZipEntry.Name)
                            End If
                        Next
                    End Using


                Catch ex As Exception
                    TextPrint("Unable to extract file: " & fname & ":" & ex.Message)
                    Thread.Sleep(5000)
                    err += 1
                End Try

                If counter <> ZipContains Then
                    err += 1
                    TextPrint("Aborting, did not extract all files.")
                    Thread.Sleep(5000)
                    End
                End If


                If Not err Then TextPrint("Completed!")

                Application.DoEvents()
                Thread.Sleep(3000)

                ApacheProcess.StartInfo.FileName = "Start.exe"
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal

                Try
                    ApacheProcess.Start()
                Catch ex As Exception
                    TextPrint("Could not start DreamGrid!")
                    Thread.Sleep(5000)
                End Try

                End
            End If
        Else
            TextPrint("Canceled")
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
        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "-u root shutdown",
            .FileName = MyFolder + "\OutworldzFiles\mysql\bin\mysqladmin.exe",
            .WindowStyle = ProcessWindowStyle.Minimized
        }
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
