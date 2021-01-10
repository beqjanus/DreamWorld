Imports System.IO

Module GlobalSettings

    Private _mySetting As New MySettings
    Private _regionClass As RegionMaker
    Private _OpensimBackupRunning As Integer
    Private _MaxPortUsed As Integer

    Public Function SafeFolderName() As String

        Dim destinationpath As String = IO.Path.Combine(Settings.CurrentDirectory(), "tmp/" & CStr(RandomNumber.Random))
        If Not System.IO.Directory.Exists(destinationpath) Then
            System.IO.Directory.CreateDirectory(destinationpath)
        End If

        Return destinationpath

    End Function

    Public Function BackupPath() As String

        'Autobackup must exist. if not create it
        ' if they set the folder somewhere else, it may have been deleted, so reset it to default
        If Settings.BackupFolder.ToUpper(Globalization.CultureInfo.InvariantCulture) = "AUTOBACKUP" Then
            BackupPath = IO.Path.Combine(FormSetup.PropCurSlashDir, "OutworldzFiles/AutoBackup")
            Settings.BackupFolder = BackupPath
            If Not Directory.Exists(BackupPath) Then
                MkDir(BackupPath)
            End If
        Else
            BackupPath = Settings.BackupFolder
            BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why
            Settings.BackupFolder = BackupPath
            If Not Directory.Exists(BackupPath) Then
                BackupPath = IO.Path.Combine(FormSetup.PropCurSlashDir, "OutworldzFiles/Autobackup")
                If Not Directory.Exists(BackupPath) Then
                    MkDir(BackupPath)
                End If
                MsgBox(My.Resources.Autobackup_cannot_be_located & BackupPath)
            End If
        End If
        Return BackupPath

    End Function

    Public Property PropRegionClass As RegionMaker
        Get
            Return _regionClass
        End Get
        Set(value As RegionMaker)
            _regionClass = value
        End Set
    End Property

    Public Property Settings As MySettings
        Get
            Return _mySetting
        End Get
        Set(value As MySettings)
            _mySetting = value
        End Set
    End Property

    Public Property OpensimBackupRunning As Integer
        Get
            Return _OpensimBackupRunning
        End Get
        Set(value As Integer)
            _OpensimBackupRunning = value
        End Set
    End Property

    Public Function RobustName() As String

        Return "Robust " & Settings.PublicIP

    End Function

End Module
