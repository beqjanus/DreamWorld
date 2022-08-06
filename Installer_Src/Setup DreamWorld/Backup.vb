Imports System.Threading
Imports Ionic.Zip

Module Backup

    ''' <summary>
    ''' Waits until a file stops changing length so we can type again. Quits if DreamGrid  is stopped.
    ''' Waits for 5 minutes or the file stops growing for 30 seconds
    ''' </summary>
    ''' <param name="BackupName">Name of region to watch</param>
    Public Function WaitforComplete(RegionUUID As String, FolderAndFileName As String) As Boolean

        If SkipAutobackup(RegionUUID) = "True" Then Return True

        If Not Settings.Smart_Start Or Not Smart_Start(RegionUUID) Then
            SequentialPause()
            Return True
        End If


        Const Seconds As Integer = 30

        Dim Filectr As Integer = 60
        Dim ctr As Integer = 600
        Dim s As Long
        Dim oldsize As Long = 0
        Dim same As Integer = 0

        While same < Seconds And PropOpensimIsRunning

            Debug.Print($"Waiting on {Region_Name(RegionUUID)} {CStr(same)}")
            PokeRegionTimer(RegionUUID)
            Try
                Dim fi = New System.IO.FileInfo(FolderAndFileName)
                s = fi.Length
            Catch ex As Exception
                Filectr -= 1
            End Try

            If Filectr = 0 Then
                Log("Error", $"{Region_Name(RegionUUID)} failed to start saving")
                Return False
            End If

            If s = oldsize And s > 0 Then
                same += 1
            Else
                same = 0
            End If
            ctr -= 1

            If ctr = 0 Then
                Log("Error", $"{Region_Name(RegionUUID)}  timeout, took too long to save")
                Return False
            End If


            Sleep(1000)
            oldsize = s

        End While

        Return True

    End Function

#Region "Backups"

    ''' <summary>
    ''' Zip up Regions
    ''' </summary>
    Public Sub BackupINI()

        Dim Name = "Region INI"
        RunningBackupName.TryAdd($"{Name} {My.Resources.Starting_word}", "")

        Dim zipused As Boolean
        'used to zip it, zip it good
        Dim _folder = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\tmp\Region_" & DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture))
        FileIO.FileSystem.CreateDirectory(_folder)

        Dim Foldername = "Region_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture)   ' Set default folder
        Dim Bak = IO.Path.Combine(_folder, Foldername & ".zip")

        Using Z = New ZipFile(Bak) With {
                .UseZip64WhenSaving = Zip64Option.AsNecessary,
                .CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression
            }

            Try
                Dim sourcePath = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Opensim\bin\Regions")
                Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)
                For Each fileSystemInfo In sourceDirectoryInfo.GetDirectories
                    Try
                        Dim folder = fileSystemInfo.FullName
                        Dim Regionpath = IO.Path.Combine(Settings.CurrentDirectory, folder & "\Region")
                        Dim RegionDirectoryInfo As New System.IO.DirectoryInfo(Regionpath)
                        For Each RegionName In RegionDirectoryInfo.GetFileSystemInfos
                            If RegionName.Name.EndsWith(".ini", StringComparison.OrdinalIgnoreCase) Then
                                Dim shortname = RegionName.Name.Replace(".ini", "")
                                Z.AddFile(IO.Path.Combine(Regionpath, RegionName.Name), $"\Regions\{shortname}\Region\")
                                zipused = True
                            End If
                        Next
                    Catch ex As Exception
                        BreakPoint.Print(ex.Message)
                    End Try
                Next
            Catch ex As Exception
                BreakPoint.Print(ex.Message)
            End Try

            Try
                If zipused = True Then
                    Z.Save()
                    Sleep(500)
                    MoveFile(Bak, IO.Path.Combine(BackupPath, Foldername & ".zip"))
                End If

                DeleteFolder(_folder)
            Catch ex As Exception
                BreakPoint.Print(ex.Message)
            End Try

        End Using

        RunningBackupName.TryAdd($"{Name} {My.Resources.Ok}", "")

    End Sub

    ''' <summary>
    ''' Get the path to the Autobackup folder. Make it if necessary
    ''' </summary>
    ''' <returns>file path</returns>
    Public Function BackupPath() As String

        'Autobackup must exist. if not create it
        ' if they set the folder somewhere else, it may have been deleted, so reset it to default
        BackupPath = Settings.BackupFolder
        BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why

        If Not IO.Directory.Exists(BackupPath) Then
            BackupPath = IO.Path.Combine(Settings.CurrentSlashDir, "OutworldzFiles/Autobackup")
            FileIO.FileSystem.CreateDirectory(BackupPath)
            Settings.BackupFolder = BackupPath
        End If

        Return BackupPath

    End Function

#End Region

#Region "Tasks"

    ''' <summary>
    ''' Background backup all OARS as a thread
    ''' </summary>
    Public Sub BackupAllRegions()

        If Not Settings.BackupOARs Then Return
        For Each RegionUUID In RegionUuids()
            If BackupAbort Then Return
            If Not RegionEnabled(RegionUUID) Then Continue For
            If SkipAutobackup(RegionUUID) = "True" Then Continue For

            RunningBackupName.TryAdd($"{Region_Name(RegionUUID)} {My.Resources.backup_running}", "")

            Dim file = BackupPath() & "/" & Region_Name(RegionUUID) & "_" &
         DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar"

            Dim Obj = New TaskObject With {
                .TaskName = TaskName.LaunchBackupper,
                .Command = file
            }
            RebootAndRunTask(RegionUUID, Obj)
            Sleep(5000)
            If WaitforComplete(RegionUUID, file) Then
                ShowDOSWindow(GetHwnd(Group_Name(RegionUUID)), MaybeHideWindow())
                RunningBackupName.TryAdd($"{My.Resources.Backup_word} {Region_Name(RegionUUID)} {My.Resources.Finished_with_backup_word}", "")
            Else
                Log("Error", $"Timeout waiting for region {Region_Name(RegionUUID)}")
            End If


        Next

        RunningBackupName.TryAdd(My.Resources.Finished_with_backup_word, "")

    End Sub

    '' must use console as otherwise Smart Start will shutdown
    Public Sub Backupper(RegionUUID As String, file As String)

        PokeRegionTimer(RegionUUID)
        ConsoleCommand(RegionUUID, $"change region ""{Region_Name(RegionUUID)}""{vbCrLf}save oar ""{file}""")

    End Sub

#End Region

End Module
