Imports System.Net
Imports System.IO
Imports Ionic.Zip
Imports System.Threading

' Copyright 2019 Fred Beckhusen
' AGPL 3.0 Licensed
' Redistribution and use in binary and source form is permitted provided
' that ALL the licenses in the text files are followed and included in all copies

Public Class UpdateGrid

    Dim Filename As String = "DreamGrid.zip"
    Dim MyFolder As String = ""

    Private Function ResolveAssemblies(sender As Object, e As System.ResolveEventArgs) As Reflection.Assembly
        Dim desiredAssembly = New Reflection.AssemblyName(e.Name)

        If desiredAssembly.Name = "the name of your assembly" Then
            Return Reflection.Assembly.Load(My.Resources.DotNetZip) 'replace with your assembly's resource name
        Else
            Return Nothing
        End If
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' dotnetzip is part of the resources so we can overwrite it.
        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ResolveAssemblies

        Label1.Text = "Dreamgrid Updater"
        Me.Text = "Outworldz Dreamgrid Setup"
        Me.Show()
        Application.DoEvents()
        MyFolder = My.Application.Info.DirectoryPath
        If Debugger.IsAttached = True Then
            MyFolder = MyFolder.Replace("\Updater_Src\Updater-II\bin\Debug", "")
            MyFolder = MyFolder.Replace("\Updater_Src\Updater-II\bin\Release", "")
            ' for testing, as the compiler buries itself in ../../../debug
        End If
        ChDir(MyFolder)

        If Not File.Exists(MyFolder & "\" & Filename) Then
            TextPrint("DreamGrid.zip file was not found. Aborting.")
            Application.DoEvents()
            Thread.Sleep(5000)
            End
        Else

            Dim result = MsgBox("Unzipping and updating your system. Proceed?", vbYesNo)
            If result = vbNo Then End

            Application.DoEvents()

            Label1.Text = "Checking MySQL"
            StopMYSQL()
            StopApache()

            Try
                My.Computer.FileSystem.DeleteDirectory(MyFolder & "\Outworldzfiles\opensim\bin\addin-db-002", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception
            End Try

            Dim ctr As Integer
            Try
                Using zip As ZipFile = ZipFile.Read(MyFolder & "\" & Filename)
                    For Each ZipEntry In zip
                        Application.DoEvents()
                        ctr = ctr + 1
                        If ZipEntry.FileName <> "DotNetZip.dll" And ZipEntry.FileName <> "DreamGridSetup.exe" Then
                            TextPrint("Extracting " + Path.GetFileName(ZipEntry.FileName))
                            Application.DoEvents()
                            ZipEntry.Extract(MyFolder, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                        End If
                    Next
                End Using
            Catch ex As Exception
                TextPrint("Unable to extract file: " + ex.Message)
            End Try
            TextPrint("Completed!")
            Thread.Sleep(3000)
            End

        End If
    End Sub

    Public Sub TextPrint(ByVal str As String)
        Label1.Text = str
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