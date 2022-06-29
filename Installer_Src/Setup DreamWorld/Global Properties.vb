Imports System.IO
Imports System.Threading
Imports IniParser.Model

Module Global_Properties

#Region "Globals"

    Public _Data As IniParser.Model.IniData
    Public Bench As New Benchmark()
    Public DebugLandMaker As Boolean
    Public gEstateName As String = ""
    Public gEstateOwner As String = ""
    Public MapX As Integer = 100
    Public MapY As Integer = 100
    Public RunningBackupName As String = ""

#End Region

#Region "Private"

    Private _IsRunning As Boolean
    Private _mySetting As MySettings
    Private _PropAborting As Boolean
    Private _RegionFilesChanged As Boolean
    Private _SelectedBox As String = ""
    Private _SkipSetup As Boolean = True
    Private _ThreadsArerunning As Boolean
    Private _UpdateView As Boolean = True
    Private _XYINI As String ' global XY INI

#End Region

#Region "Subs"

    Public Sub PokeGroupTimer(GroupName As String)

        For Each RegionUUID In RegionUuidListByName(GroupName)
            Timer(RegionUUID) = Date.Now()
        Next

    End Sub

    Public Sub PokeRegionTimer(RegionUUID As String)

        PokeGroupTimer(Group_Name(RegionUUID))

    End Sub

    Public Sub Sleep(value As Integer)
        ''' <summary>Sleep(ms)</summary>
        ''' <param name="value">millseconds</param>
        ' value is in milliseconds, but we do it in 10 passes a second so we can doevents() to free up console
        Dim slices As Integer = 10

        While value > 0
            Application.DoEvents()
            Dim t = CInt(1000 / slices)
            Thread.Sleep(t)
            value -= t
        End While

    End Sub

    Public Sub TextPrint(Value As String)

        Log(My.Resources.Info_word, Value)
        Dim dt = Date.Now.ToString(Globalization.CultureInfo.CurrentCulture)
        If Settings.ShowDateandTimeinLogs Then
            FormSetup.TextBox1.Text += $"{dt} {Value}{vbCrLf}"
            Log(My.Resources.Info_word, $"{dt} {Value}{vbCrLf}")
        Else
            FormSetup.TextBox1.Text += $"{Value}{vbCrLf}"
            Log(My.Resources.Info_word, $"{dt} {Value}{vbCrLf}")
        End If

        If FormSetup.TextBox1.Text.Length > FormSetup.TextBox1.MaxLength - 1000 Then
            FormSetup.TextBox1.Text = Mid(FormSetup.TextBox1.Text, 1000)
        End If

    End Sub

#End Region

#Region "Functions"

    Public Function GetStateString(state As Integer) As String

        Dim statestring As String
        Select Case state
            Case -1
                statestring = "ERROR"
            Case 0
                statestring = "Stopped"
            Case 1
                statestring = "Booting"
            Case 2
                statestring = "Booted"
            Case 3
                statestring = "RecyclingUp"
            Case 4
                statestring = "RecyclingDown"
            Case 5
                statestring = "RestartPending"
            Case 6
                statestring = "RetartingNow"
            Case 7
                statestring = "Resume"
            Case 8
                statestring = "Suspended"
            Case 9
                statestring = "RestartStage2"
            Case 10
                statestring = "ShuttingDownForGood"
            Case 11
                statestring = "NoLogin"
            Case 12
                statestring = "No Error"
            Case Else
                statestring = "**** Unknown state ****"
                BreakPoint.Print($"**** Unknown state **** {CStr(state)}")
        End Select

        Return statestring

    End Function

    Public Function RobustName() As String

        Return "Robust " & Settings.PublicIP

    End Function

    Public Function VarChooser(RegionName As String, Optional modal As Boolean = True, Optional Map As Boolean = True) As String

        Dim RegionUUID As String = FindRegionByName(RegionName)
        Dim size = SizeX(RegionUUID)

#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim VarForm As New FormDisplacement ' form for choosing a region in  a var
#Enable Warning CA2000 ' Dispose objects before losing scope
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

#Region "Classes and Enums"

    Public Class PRIEnumClass

        Public AboveNormal As ProcessPriorityClass = ProcessPriorityClass.AboveNormal
        Public BelowNormal As ProcessPriorityClass = ProcessPriorityClass.BelowNormal
        Public High As ProcessPriorityClass = ProcessPriorityClass.High
        Public Normal As ProcessPriorityClass = ProcessPriorityClass.Normal
        Public RealTime As ProcessPriorityClass = ProcessPriorityClass.RealTime
    End Class

#End Region

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

    ''' <summary>
    ''' Set when the RegionList should be refreshed
    ''' </summary>
    ''' <returns>true if it need refresh</returns>
    Public Property PropChangedRegionSettings As Boolean
        Get
            Return _RegionFilesChanged
        End Get
        Set(value As Boolean)
            _RegionFilesChanged = value
        End Set
    End Property

    Public ReadOnly Property PropDomain As String
        Get
            Return _Domain
        End Get

    End Property

    Public ReadOnly Property PropHttpsDomain As String
        Get
            Return _httpsDomain
        End Get

    End Property

    Public ReadOnly Property PropMyVersion As String
        Get
            Return _MyVersion
        End Get
    End Property

    ''' <summary>
    ''' Property set if Opensim when supposed to be running
    ''' </summary>
    ''' <returns>True if running</returns>
    Public Property PropOpensimIsRunning() As Boolean
        Get
            Return _IsRunning
        End Get
        Set(ByVal Value As Boolean)
            _IsRunning = Value
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
            'If Debugger.IsAttached Then Breakpoint.Print("View refresh")
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

    Public Property SkipSetup() As Boolean
        Get
            Return _SkipSetup
        End Get
        Set(ByVal Value As Boolean)
            _SkipSetup = Value
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

End Module
