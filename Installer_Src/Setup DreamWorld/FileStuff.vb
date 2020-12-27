Imports System.IO

Module FileStuff

    ''' <summary>Deletes old log files</summary>
    '''
    Public Sub ClearLogFiles()

        Dim Logfiles = New List(Of String) From {
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Error.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Diagnostics.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Outworldz.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Restart.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\OpenSimConsoleHistory.txt"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Diagnostics.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\UPnp.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\Robust.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\http.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\PHPLog.log"),
            IO.Path.Combine(Settings.CurrentDirectory, "http.log")     ' an old mistake
        }

        For Each thing As String In Logfiles
            ' clear out the log files
            FileStuff.DeleteFile(thing)
            Application.DoEvents()
        Next

        For Each UUID As String In PropRegionClass.RegionUuids
            Dim GroupName = PropRegionClass.GroupName(UUID)
            FileStuff.DeleteFile(Settings.OpensimBinPath() & "Regions\" & GroupName & "\Opensim.log")
            FileStuff.DeleteFile(Settings.OpensimBinPath() & "Regions\" & GroupName & "\PID.pid")
            FileStuff.DeleteFile(Settings.OpensimBinPath() & "regions\" & GroupName & "\OpensimConsole.log")
            FileStuff.DeleteFile(Settings.OpensimBinPath() & "regions\" & GroupName & "\OpenSimStats.log")
        Next

    End Sub

    Public Sub ExpireApacheLogs()

        ' Delete old Apache logs
        Dim ApacheLogPath = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\logs")

        Dim currentdatetime As Date = Date.Now

        Dim directory As New System.IO.DirectoryInfo(ApacheLogPath)
        Dim File As System.IO.FileInfo() = directory.GetFiles()
        Dim File1 As System.IO.FileInfo

        ' get each file's last modified date
        For Each File1 In File
            Dim strLastModified As Date = System.IO.File.GetLastWriteTime(ApacheLogPath & "\" & File1.Name)
            strLastModified = strLastModified.AddDays(CDbl(Settings.KeepForDays))
            Dim y = DateTime.Compare(currentdatetime, strLastModified)
            If DateTime.Compare(currentdatetime, strLastModified) > 0 Then
                FileStuff.DeleteFile(File1.FullName)
            End If
        Next

    End Sub

    Sub FixUpdater()

        CopyFile(IO.Path.Combine(Settings.CurrentDirectory, "DreamGridUpdater.New"),
                 IO.Path.Combine(Settings.CurrentDirectory, "DreamGridUpdater.exe"),
                True)

    End Sub

    Sub DeleteOldHelpFiles()

        Dim folder As String = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Help")
        Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(folder)

        Dim fileSystemInfo As System.IO.FileSystemInfo
        For Each fileSystemInfo In sourceDirectoryInfo.GetFileSystemInfos
            If fileSystemInfo.FullName.EndsWith(".rtf", StringComparison.InvariantCulture) Then
                DeleteFile(fileSystemInfo.FullName)
            End If

        Next

    End Sub

    Sub CopyFile(source As String, dest As String, overwrite As Boolean)

        If source.EndsWith("\Opensim.ini", StringComparison.InvariantCulture) Then Return
        If source.EndsWith("/Opensim.ini", StringComparison.InvariantCulture) Then Return
        If source.EndsWith("OpenSim.log", StringComparison.InvariantCulture) Then Return
        If source.EndsWith("OpenSimStats.log", StringComparison.InvariantCulture) Then Return
        If source.EndsWith("PID.pid", StringComparison.InvariantCulture) Then Return
        If source.EndsWith("DataSnapshot", StringComparison.InvariantCulture) Then Return

        Try
            My.Computer.FileSystem.CopyFile(source, dest, overwrite)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Public Sub CopyFolder(ByVal sourcePath As String, ByVal destinationPath As String)

        Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)

        ' If the destination folder don't exist then create it
        If Not System.IO.Directory.Exists(destinationPath) Then
            Try
                System.IO.Directory.CreateDirectory(destinationPath)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End If

        If Not System.IO.Directory.Exists(sourcePath) Then
            MsgBox("Cannot locate folder " & sourcePath, vbInformation)
            Return
        End If
        Dim ctr = 0
        Dim fileSystemInfo As System.IO.FileSystemInfo
        For Each fileSystemInfo In sourceDirectoryInfo.GetFileSystemInfos
            Dim destinationFileName As String =
                System.IO.Path.Combine(destinationPath, fileSystemInfo.Name)

            ' Now check whether its a file or a folder and take action accordingly
            If File.Exists(fileSystemInfo.FullName) Then
                ctr += 1
                If ctr Mod 100 = 0 Then
                    FormSetup.Print(CStr(ctr) & " copied")
                End If

                Try
                    CopyFile(fileSystemInfo.FullName, destinationFileName, True)
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
            Else
                ' Recursively call the method to copy all the nested folders
                If Not System.IO.Directory.Exists(fileSystemInfo.FullName) Then
                    Try
                        System.IO.Directory.CreateDirectory(fileSystemInfo.FullName)
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    End Try
                End If
                CopyFolder(fileSystemInfo.FullName, destinationFileName)
                Application.DoEvents()
            End If

        Next
    End Sub

    Sub DeleteDirectory(folder As String, param As FileIO.DeleteDirectoryOption)

        Try
            My.Computer.FileSystem.DeleteDirectory(folder, param)
        Catch ex As Exception
        End Try
    End Sub

    Sub DeleteFile(file As String)

        Try
            If My.Computer.FileSystem.FileExists(file) Then
                My.Computer.FileSystem.DeleteFile(file)
            End If
        Catch ex As Exception
        End Try

    End Sub

End Module
