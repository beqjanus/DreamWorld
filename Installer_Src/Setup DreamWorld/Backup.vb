Imports System.Threading

Module Backup

#Region "Backups"

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

    Public Sub BackupAllRegions()

#Disable Warning BC42016 ' Implicit conversion
        Dim start As ParameterizedThreadStart = AddressOf BackupAllRegionsTask
#Enable Warning BC42016 ' Implicit conversion
        Dim SaveIARThread = New Thread(start)
        SaveIARThread.SetApartmentState(ApartmentState.STA)
        SaveIARThread.Priority = ThreadPriority.Lowest ' UI gets priority
        SaveIARThread.Start()

    End Sub

    Public Sub BackupAllRegionsTask()

        Dim L = RegionUuids()
        L.Sort()

        For Each RegionUUID As String In L
            If Not RegionEnabled(RegionUUID) Then Continue For
            If SkipAutobackup(RegionUUID) = "True" Then Continue For

            Dim file = BackupPath() & "/" & Region_Name(RegionUUID) & "_" &
         DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss", Globalization.CultureInfo.InvariantCulture) & ".oar"

            Dim Obj = New TaskObject With {
                .TaskName = FormSetup.TaskName.RPCBackupper,
                .Command = file
            }
            FormSetup.RebootAndRunTask(RegionUUID, Obj)

            Application.DoEvents()
            WaitforComplete(RegionUUID, file)

        Next
    End Sub

    Public Sub Backupper(RegionUUID As String, file As String)

        ConsoleCommand(RegionUUID, $"change region ""{Region_Name(RegionUUID)}""{vbCrLf}save oar ""{file}""")

    End Sub

#End Region

End Module
