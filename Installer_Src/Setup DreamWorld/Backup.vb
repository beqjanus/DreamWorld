Imports System.Threading
Imports Ionic.Zip

Module Backup

#Region "Backups"

    ''' <summary>
    ''' Zip up Regions
    ''' </summary>
    Public Sub BackupINI()

        Dim Name = "Region INI"
        RunningBackupName.TryAdd($"{Name} {My.Resources.Starting_word}", "")
        Sleep(2000)
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
        Sleep(2000)

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

#Disable Warning BC42016 ' Implicit conversion
        Dim start As ParameterizedThreadStart = AddressOf BackupAllRegionsTask
#Enable Warning BC42016 ' Implicit conversion
        Dim SaveIARThread = New Thread(start)
        SaveIARThread.SetApartmentState(ApartmentState.STA)
        SaveIARThread.Priority = ThreadPriority.Lowest ' UI gets priority
        SaveIARThread.Start()

    End Sub

    ''' <summary>
    ''' Background Thread to save all OARS
    ''' </summary>

    Public Sub BackupAllRegionsTask()

        Dim L = RegionUuids()
        L.Sort()

        For Each RegionUUID As String In L
            If BackupAbort Then Return
            If Not RegionEnabled(RegionUUID) Then Continue For
            If SkipAutobackup(RegionUUID) = "True" Then Continue For

            RunningBackupName.TryAdd($"{Region_Name(RegionUUID)} {My.Resources.Starting_word}", "")
            Sleep(2000)
            Dim file = BackupPath() & "/" & Region_Name(RegionUUID) & "_" &
         DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar"

            Dim Obj = New TaskObject With {
                .TaskName = TaskName.LaunchBackupper,
                .Command = file
            }
            RebootAndRunTask(RegionUUID, Obj)

            Application.DoEvents()
            WaitforComplete(RegionUUID, file)

            RunningBackupName.TryAdd($"{Region_Name(RegionUUID)} {My.Resources.Ok}", "")
            Sleep(2000)
        Next

    End Sub

    '' must use console as otherwise Smart Start will shutdown
    Public Sub Backupper(RegionUUID As String, file As String)

        ConsoleCommand(RegionUUID, $"change region ""{Region_Name(RegionUUID)}""{vbCrLf}save oar ""{file}""")

    End Sub

#End Region

End Module
