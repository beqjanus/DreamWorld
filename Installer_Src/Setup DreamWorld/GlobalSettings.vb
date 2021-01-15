Imports System.IO
Imports System.Threading

Module GlobalSettings

#Region "Const"

    Public Const _Domain As String = "http://outworldz.com"
    Public Const _MyVersion As String = "3.81"
    Public Const _SimVersion As String = "#70e00a00ec (fix creators user cache, 2021-01-07)"
    Public Const Hyperica As String = "Hyperica"
    Public Const JOpensim As String = "JOpensim"
    Public Const MySqlRev = "5.6.5"

    Private _IsRunning As Boolean
    Private _PropAborting As Boolean
    Private _mySetting As New MySettings
    Private _regionClass As RegionMaker
    Private _OpensimBackupRunning As Integer
    Private _SelectedBox As String = ""

#End Region

#Region "Properties"

    Public Property PropSelectedBox As String
        Get
            Return _SelectedBox
        End Get
        Set(value As String)
            _SelectedBox = value
        End Set
    End Property

    Public Property PropAborting() As Boolean
        Get
            Return _PropAborting
        End Get
        Set(ByVal Value As Boolean)
            _PropAborting = Value
        End Set
    End Property

    Public ReadOnly Property PropDomain As String
        Get
            Return _Domain
        End Get

    End Property

    Public Property PropOpensimIsRunning() As Boolean
        Get
            Return _IsRunning
        End Get
        Set(ByVal Value As Boolean)
            _IsRunning = Value
        End Set
    End Property

    Public ReadOnly Property PropMyVersion As String
        Get
            Return _MyVersion
        End Get
    End Property

    Public ReadOnly Property PropSimVersion As String
        Get
            Return _SimVersion
        End Get
    End Property

#End Region

    Public Sub TextPrint(Value As String)

        Log(My.Resources.Info_word, Value)
        FormSetup.TextBox1.Text += Value & vbCrLf
        If FormSetup.TextBox1.Text.Length > FormSetup.TextBox1.MaxLength - 1000 Then
            FormSetup.TextBox1.Text = Mid(FormSetup.TextBox1.Text, 1000)
        End If

    End Sub

    Public Sub Sleep(value As Integer)
        ''' <summary>Sleep(ms)</summary>
        ''' <param name="value">millseconds</param>
        ' value is in milliseconds, but we do it in 10 passes so we can doevents() to free up console
        Dim sleeptime = value / 100  ' now in tenths

        While sleeptime > 0
            Application.DoEvents()
            Thread.Sleep(100)
            sleeptime -= 1
        End While
    End Sub

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

    Public Function VarChooser(RegionName As String, Optional modal As Boolean = True, Optional Map As Boolean = True) As String

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
        Dim size = PropRegionClass.SizeX(RegionUUID)

#Disable Warning CA2000
        Dim VarForm As New FormDisplacement ' form for choosing a region in  a var
#Enable Warning CA2000
        Dim span As Integer = CInt(Math.Ceiling(size / 256))
        ' Show Dialog as a modal dialog
        VarForm.Init(span, RegionUUID, Map)

        If modal Then
            VarForm.ShowDialog()
            VarForm.Dispose()
        Else
            VarForm.Show()
        End If

        Return PropSelectedBox

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
