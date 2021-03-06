Imports System.IO
Imports System.Threading
Imports IniParser.Model

Module GlobalSettings

#Region "Const"

    Public Const _Domain As String = "http://outworldz.com"
    Public Const _MyVersion As String = "3.899"
    Public Const _SimVersion As String = "#be49d426d9=>1610c3f741 (mantis 8862: do cancel negative cache on valid store"
    Public Const jOpensimRev As String = "Joomla_3.9.23-Stable-Full_Package"
    Public Const jRev As String = "3.9.23"
    Public Const MySqlRev = "5.6.5"

    Public Const SSTime As Integer = 30

#End Region

#Region "Global Strings"

    Public Const Hyperica As String = "Hyperica"
    Public Const JOpensim As String = "JOpensim"
    Public Const MetroServer As String = "Metro"
    Public Const OsgridServer As String = "OsGrid"
    Public Const RegionServerName As String = "Region"
    Public Const RobustServerName As String = "Robust"

#End Region

#Region "Private"

    Dim _Data As IniParser.Model.IniData
    Private _IsRunning As Boolean
    Private _lastbackup As Integer
    Private _mySetting As New MySettings
    Private _PropAborting As Boolean
    Private _regionClass As RegionMaker
    Private _SelectedBox As String = ""
    Private _UpdateView As Boolean = True
    Private _XYINI As String ' global XY INI

#End Region

    Public Function BackupsRunning(dt As String) As String

        Dim folder = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\tmp")
        Dim Running() As String
        Dim count As Integer = 0
        Try
            Running = IO.Directory.GetDirectories(folder)
            count = Running.Length
        Catch
        End Try

        Dim text As String = ""

        If _lastbackup <> count Then
            If _lastbackup = 0 And count = 1 Then
                text = dt & " " & CStr(count) & " " & "backup running"
            ElseIf _lastbackup > 0 And count > 1 Then
                text = dt & " " & CStr(count) & " " & "backups running"
            ElseIf _lastbackup > 0 And count = 0 Then
                text = dt & " backup completed"
            End If
        End If
        _lastbackup = count
        Return text

    End Function

#Region "Properties"

    Public ReadOnly Property GitVersion As String
        ' output of  git rev-parse --short HEAD   from Perl
        Get
            Dim line As String = "None"
            Dim fname = IO.Path.Combine(Settings.CurrentDirectory, "GitVersion")
            If System.IO.File.Exists(fname) Then
                Using reader As StreamReader = System.IO.File.OpenText(fname)
                    'now loop through each line
                    While reader.Peek <> -1
                        line = reader.ReadLine()
                    End While
                End Using
            End If
            Return line
        End Get

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

    Public ReadOnly Property PropMyVersion As String
        Get
            Return _MyVersion
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

    Public Property PropRegionClass As RegionMaker
        Get
            Return _regionClass
        End Get
        Set(value As RegionMaker)
            _regionClass = value
        End Set
    End Property

    Public Property PropSelectedBox As String
        Get
            Return _SelectedBox
        End Get
        Set(value As String)
            _SelectedBox = value
        End Set
    End Property

    Public ReadOnly Property PropSimVersion As String
        Get
            Return _SimVersion
        End Get
    End Property

    Public Property PropUpdateView() As Boolean
        Get
            Return _UpdateView
        End Get
        Set(ByVal Value As Boolean)
            If Debugger.IsAttached Then Diagnostics.Debug.Print("View refresh")
            _UpdateView = Value
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

    Public Property XYData As IniData
        Get
            Return _Data
        End Get
        Set(value As IniData)
            _Data = value
        End Set
    End Property

    Public Property XYINI As String
        Get
            Return _XYINI
        End Get
        Set(value As String)
            _XYINI = value
        End Set
    End Property

#End Region

#Region "Subs"

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

    Public Sub TextPrint(Value As String)

        Log(My.Resources.Info_word, Value)
        FormSetup.TextBox1.Text += Value & vbCrLf
        If FormSetup.TextBox1.Text.Length > FormSetup.TextBox1.MaxLength - 1000 Then
            FormSetup.TextBox1.Text = Mid(FormSetup.TextBox1.Text, 1000)
        End If

    End Sub

#End Region

#Region "Teleport"

    Public Function RegionListHTML(Settings As MySettings, PropRegionClass As RegionMaker) As String

        ' http://localhost:8001/teleports.htm
        ' http://YourURL:8001/teleports.htm
        'Outworldz|Welcome||outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||outworldz.com9000Welcome|128,128,96|
        Dim HTML As String = ""

        Dim ToSort As New List(Of String)

        For Each RegionUUID As String In PropRegionClass.RegionUuids
            Dim status = PropRegionClass.Status(RegionUUID)
            If PropRegionClass.Teleport(RegionUUID) = "True" Or PropRegionClass.SmartStart(RegionUUID) = "True" Then
                ToSort.Add(PropRegionClass.RegionName(RegionUUID))
            End If
        Next

        ToSort.Sort()

        'TODO   "||"  is coordinates for destinations

        For Each S As String In ToSort
            HTML += "*|" & S & "||" & Settings.PublicIP & ":" & Settings.HttpPort & ":" & S & "||" & S & "|" & vbCrLf
        Next

        Dim HTMLFILE = Settings.OpensimBinPath & "data\teleports.htm"
        DeleteFile(HTMLFILE)

        Try
            Using outputFile As New StreamWriter(HTMLFILE, True)
                outputFile.WriteLine(HTML)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Return HTML

    End Function

#End Region

#Region "Functions"

    Public Function BackupPath() As String

        'Autobackup must exist. if not create it
        ' if they set the folder somewhere else, it may have been deleted, so reset it to default
        BackupPath = Settings.BackupFolder
        BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses Unix-like slashes, that's why

        If Not Directory.Exists(BackupPath) Then
            BackupPath = IO.Path.Combine(FormSetup.PropCurSlashDir, "OutworldzFiles/Autobackup")
            If Not Directory.Exists(BackupPath) Then
                MkDir(BackupPath)
            End If
            Settings.BackupFolder = BackupPath
        End If

        Return BackupPath

    End Function

    Public Function RobustName() As String

        Return "Robust " & Settings.PublicIP

    End Function

    Public Function SafeFolderName() As String

        Dim destinationpath As String = IO.Path.Combine(Settings.CurrentDirectory(), "tmp/" & CStr(DateTime.Now.Ticks))
        If Not System.IO.Directory.Exists(destinationpath) Then
            System.IO.Directory.CreateDirectory(destinationpath)
        End If

        Return destinationpath

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

#End Region

End Module
