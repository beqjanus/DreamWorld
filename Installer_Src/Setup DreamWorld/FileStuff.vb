Imports System.IO
Imports System.Threading

Module FileStuff

    Public Sub Cleanup() ' old files

        ' cleanup old code and files
        Dim ToDrop = New List(Of String) From {
            "Downloader.exe",
            "DreamGridSetup.exe",
            "Downloader.exe.config",
            "DreamGridSetup.exe.config",
            "Outworldzfiles\Opensim\bin\OpenSim.Additional.AutoRestart.dll",
            "Outworldzfiles\Opensim\bin\OpenSim.Additional.AutoRestart.pdb",
            "Outworldzfiles\Opensim\bin\config-include\Birds.ini",
            "SET_externalIP-Log.txt"
        }

        For Each N As String In ToDrop
            DeleteFile(IO.Path.Combine(Settings.CurrentDirectory, N))
        Next

        Dim FoldersToDrop = New List(Of String) From {
            IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\WifiPages"),
            IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\WifiPages")
        }

        For Each N As String In ToDrop
            DeleteFolder(N)
        Next

        Dim files As New List(Of String) From {
        "\Shoutcast", ' deprecated
        "\Icecast",   ' moved to Outworldzfiles
        "\Outworldzfiles\Opensim\bin\addins",' moved to Outworldzfiles
        "\Outworldzfiles\Opensim\bin\addin-db-002", ' must be cleared or opensim updates can break.
        "\Outworldzfiles\Opensim\bin\addin-db-001", ' must be cleared or opensim updates can break.
        "\Outworldzfiles\Opensim\bin\addin-db" ' must be cleared or opensim updates can break.
        }

        If FormSetup.PropKillSource Then
            files.Add("Outworldzfiles\Opensim\.nant")
            files.Add("Outworldzfiles\Opensim\doc")
            files.Add("Outworldzfiles\Opensim\Opensim")
            files.Add("Outworldzfiles\Opensim\Prebuild")
            files.Add("Outworldzfiles\Opensim\share")
            files.Add("Outworldzfiles\Opensim\Thirdparty")
        End If

        For Each N In files
            DeleteFolder(N)   ' wipe these folders out
        Next

        ' crap load of old DLLS have to be eliminated
        CleanDLLs() ' drop old opensim Dll's

    End Sub

    ''' <summary>Deletes old log files</summary>
    Public Sub ClearLogFiles()

        Dim Logfiles = New List(Of String) From {
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
            DeleteFile(thing)
            Application.DoEvents()
        Next

        For Each UUID As String In PropRegionClass.RegionUuids
            Dim GroupName = PropRegionClass.GroupName(UUID)
            DeleteFile(Settings.OpensimBinPath() & "Regions\" & GroupName & "\Opensim.log")
            DeleteFile(Settings.OpensimBinPath() & "Regions\" & GroupName & "\PID.pid")
            DeleteFile(Settings.OpensimBinPath() & "regions\" & GroupName & "\OpensimConsole.log")
            DeleteFile(Settings.OpensimBinPath() & "regions\" & GroupName & "\OpenSimStats.log")
        Next

    End Sub

    Public Sub CopyFileFast(From As String, Dest As String)

        Try
            'Create the file stream for the source file
            Dim streamRead As New System.IO.FileStream(From, System.IO.FileMode.Open)
            'Create the file stream for the destination file
            Dim streamWrite As New System.IO.FileStream(Dest, System.IO.FileMode.Create)
            'Determine the size in bytes of the source file (-1 as our position starts at 0)
            Dim lngLen As Long = streamRead.Length - 1
            Dim byteBuffer(1048576) As Byte   'our stream buffer
            Dim intBytesRead As Integer    'number of bytes read

            While streamRead.Position < lngLen    'keep streaming until EOF
                'Read from the Source
                intBytesRead = (streamRead.Read(byteBuffer, 0, 1048576))
                'Write to the Target
                streamWrite.Write(byteBuffer, 0, intBytesRead)
                Application.DoEvents()    'do it
            End While

            'Clean up
            streamWrite.Flush()
            streamWrite.Close()
            streamRead.Close()
        Catch
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
            MsgBox("Cannot locate folder " & sourcePath, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
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
                    TextPrint(CStr(ctr) & " copied")
                End If

                Try
                    CopyFileFast(fileSystemInfo.FullName, destinationFileName)
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

    Public Sub CopyWifi()

        'DeleteFolder(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\WifiPages"))
        'DeleteFolder(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\WifiPages"))

        CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\WifiPages-" & Settings.Theme), IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\WifiPages"))
        CopyFolder(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\WifiPages-" & Settings.Theme), IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\WifiPages"))
        CopyFileFast(IO.Path.Combine(Settings.CurrentDirectory, Settings.Theme() & ".png"), IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\WifiPages\images\Photo.png"))

    End Sub

    Sub DeleteDirectory(folder As String, param As FileIO.DeleteDirectoryOption)

        Try
            My.Computer.FileSystem.DeleteDirectory(folder, param)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub DeleteDirectoryTmp()

        Try
            Dim WebThread = New Thread(AddressOf Deltmp)
            WebThread.SetApartmentState(ApartmentState.STA)
            WebThread.Priority = ThreadPriority.BelowNormal ' UI gets priority
            WebThread.Start()
        Catch
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

    Public Sub DeleteFolder(n As String)

        If System.IO.Directory.Exists(n) Then
            Try
                System.IO.Directory.Delete(n, True)
            Catch ex As IOException
                BreakPoint.Show(ex.Message)
            Catch ex As UnauthorizedAccessException
                BreakPoint.Show(ex.Message)
            Catch ex As ArgumentException
                BreakPoint.Show(ex.Message)
            End Try

        End If

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

    Public Function DelLibrary() As Boolean

        TextPrint("->Set Library")
        DeleteFile(Settings.OpensimBinPath & "Library\Clothing Library (small).iar")
        DeleteFile(Settings.OpensimBinPath & "Library\Objects Library (small).iar")
        Return False

    End Function

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
                DeleteFile(File1.FullName)
            End If
        Next

    End Sub

    Sub FixUpdater()

        CopyFileFast(IO.Path.Combine(Settings.CurrentDirectory, "DreamGridUpdater.New"), IO.Path.Combine(Settings.CurrentDirectory, "DreamGridUpdater.exe"))

    End Sub

    Public Sub MoveFile(Src As String, Dest As String)

        Try
            File.Move(Src, Dest)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub CleanDLLs()

        If Not Debugger.IsAttached Then
            Dim dlls As List(Of String) = GetDlls(IO.Path.Combine(Settings.CurrentDirectory, "dlls.txt"))
            Dim localdlls As List(Of String) = GetFilesRecursive(Settings.OpensimBinPath)
            For Each localdllname In localdlls
                Application.DoEvents()
                Dim x = localdllname.IndexOf("OutworldzFiles", StringComparison.InvariantCulture)
                Dim newlocaldllname = Mid(localdllname, x)
                If Not CompareDLLignoreCase(newlocaldllname, dlls) Then
                    DeleteFile(localdllname)
                End If
            Next
        End If

    End Sub

    Private Function CompareDLLignoreCase(tofind As String, dll As List(Of String)) As Boolean
        If dll Is Nothing Then Return False
        If tofind Is Nothing Then Return False
        For Each filename In dll
            If tofind.ToUpper(Globalization.CultureInfo.InvariantCulture) = filename.ToUpper(Globalization.CultureInfo.InvariantCulture) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub Deltmp() ' thread
        DeleteDirectory(IO.Path.Combine(Settings.CurrentDirectory, "tmp"), FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub

    Private Function GetDlls(fname As String) As List(Of String)

        Dim DllList As New List(Of String)

        If System.IO.File.Exists(fname) Then
            Dim line As String
            Using reader As StreamReader = System.IO.File.OpenText(fname)
                'now loop through each line
                While reader.Peek <> -1
                    line = reader.ReadLine()
                    DllList.Add(line)
                End While
            End Using
        End If
        Return DllList

    End Function

    Private Function GetFilesRecursive(ByVal initial As String) As List(Of String)
        ''' <summary>This method starts at the specified directory. It traverses all subdirectories. It returns a List of those directories.</summary>
        ''' ' This list stores the results.
        Dim result As New List(Of String)

        ' This stack stores the directories to process.
        Dim stack As New Stack(Of String)

        ' Add the initial directory
        stack.Push(initial)

        ' Continue processing for each stacked directory
        Do While (stack.Count > 0)
            ' Get top directory string
            Dim dir As String = stack.Pop

            ' Add all immediate file paths
            Try
                result.AddRange(Directory.GetFiles(dir, "*.dll"))
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

            ' Loop through all subdirectories and add them to the stack.
            Dim directoryName As String

            'Save, but skip script engines
            For Each directoryName In Directory.GetDirectories(dir)
                If Not directoryName.Contains("ScriptEngines") And
                    Not directoryName.Contains("fsassets") And
                    Not directoryName.Contains("assetcache") And
                    Not directoryName.Contains("j2kDecodeCache") Then
                    stack.Push(directoryName)
                End If
                Application.DoEvents()
            Next
            Application.DoEvents()
        Loop

        ' Return the list
        Return result
    End Function

End Module
