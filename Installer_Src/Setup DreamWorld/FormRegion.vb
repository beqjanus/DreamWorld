#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.ComponentModel
Imports System.IO
Imports System.Text.RegularExpressions

Public Class FormRegion

#Region "Declarations"

    Private _NotSmarttStart As Boolean
    Dim _RegionUUID As String = ""
    Dim BoxSize As Integer = 256
    Dim changed As Boolean
    Dim initted As Boolean

    ' needed a flag to see if we are initialized as the dialogs change on start. true if we need to save a form
    Dim isNew As Boolean

    Dim oldname As String = ""
    Dim RName As String

#End Region

#Region "Properties"

    Public Property Changed1 As Boolean
        Get
            Return changed
        End Get
        Set(value As Boolean)
            changed = value
        End Set
    End Property

    Public Property Initted1 As Boolean
        Get
            Return initted
        End Get
        Set(value As Boolean)
            initted = value
        End Set
    End Property

    Public Property IsNew1 As Boolean
        Get
            Return isNew
        End Get
        Set(value As Boolean)
            isNew = value
        End Set
    End Property

    Public Property Oldname1 As String
        Get
            Return oldname
        End Get
        Set(value As String)
            oldname = value
        End Set
    End Property

    Public Property RegionUUID As String
        Get
            Return _RegionUUID
        End Get
        Set(value As String)
            _RegionUUID = value
        End Set
    End Property

    Public Property RName1 As String
        Get
            Return RName
        End Get
        Set(value As String)
            RName = value
        End Set
    End Property

#End Region

#Region "RestartRobustIfNeeded"

    Private Sub RestartRobustIfNeeded(RegionUUID As String)

        Me.Hide()

        If Not _NotSmarttStart And SmartStartCheckBox.Checked And IsRobustRunning() Then

            PropAborting = True
            Dim loopctr = 60 ' wait 1 minute
            PropRegionClass.StopRegion(RegionUUID)
            While CheckPort(Settings.PublicIP(), PropRegionClass.GroupPort(RegionUUID)) And loopctr > 0
                loopctr -= 1
                Sleep(1000)
            End While

            StopRobust()
            PropAborting = False
            StartRobust()

            Boot(PropRegionClass.RegionName(RegionUUID))

        End If

    End Sub


#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Start/Stop"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        SetScreen()
        Text = Global.Outworldz.My.Resources.Regions_word

        RichTextBoxOptions.Text = Global.Outworldz.My.Resources.OptionsHelp
        RichTextBoxMap.Text = Global.Outworldz.My.Resources.MapsHelp
        RichTextBoxPhysics.Text = Global.Outworldz.My.Resources.PhysicsHelp
        RichTextBoxScripts.Text = Global.Outworldz.My.Resources.ScriptsHelp
        RichTextBoxPermissions.Text = Global.Outworldz.My.Resources.PermissionsHelp
        RichTextBoxPublicity.Text = Global.Outworldz.My.Resources.PublicityHelp
        RichTextBoxModules.Text = Global.Outworldz.My.Resources.ModulesHelp

        RegionsGroupbox.Text = Global.Outworldz.My.Resources.Regions_word
        MapGroupBox.Text = Global.Outworldz.My.Resources.Maps_word
        PhysicsGroupbox.Text = Global.Outworldz.My.Resources.Physics_word
        PermissionsGroupbox.Text = Global.Outworldz.My.Resources.Permissions_word
        ScriptsGroupbox.Text = Global.Outworldz.My.Resources.Scripts_word
        PublicityGroupBox.Text = Global.Outworldz.My.Resources.Publicity_Word
        ModulesGroupBox.Text = Global.Outworldz.My.Resources.Modules_word

        GodLevel.Text = Global.Outworldz.My.Resources.Allow_Or_Disallow_Gods_word
        BirdsCheckBox.Text = Global.Outworldz.My.Resources.Bird_Module_word
        ClampPrimLabel.Text = Global.Outworldz.My.Resources.Clamp_Prim_Size_word
        ConciergeCheckBox.Text = Global.Outworldz.My.Resources.Announce_visitors
        DeleteButton.Text = Global.Outworldz.My.Resources.Delete_word
        DeregisterButton.Text = Global.Outworldz.My.Resources.Deregister_word
        DisableGBCheckBox.Text = Global.Outworldz.My.Resources.Disable_Gloebits_word
        DisallowForeigners.Text = Global.Outworldz.My.Resources.Disable_Foreigners_word
        DisallowResidents.Text = Global.Outworldz.My.Resources.Disable_Residents
        EnabledCheckBox.Text = Global.Outworldz.My.Resources.Enabled_word
        FrameRateLabel.Text = Global.Outworldz.My.Resources.FrameRate

        'boxes
        PhysicsGroupbox.Text = Global.Outworldz.My.Resources.Physics_word
        GroupBox2.Text = Global.Outworldz.My.Resources.Sim_Size_word
        PublicityGroupBox.Text = My.Resources.Publicity_Word
        PermissionsGroupbox.Text = Global.Outworldz.My.Resources.Permissions_word
        ModulesGroupBox.Text = Global.Outworldz.My.Resources.Modules_word
        ScriptsGroupbox.Text = Global.Outworldz.My.Resources.Script_Engine_word  '

        XLabel.Text = Global.Outworldz.My.Resources.X
        YLabel.Text = Global.Outworldz.My.Resources.Y

        Gods_Use_Default.Text = Global.Outworldz.My.Resources.Use_Default_word
        GodEstate.Text = Global.Outworldz.My.Resources.Region_Owner_Is_God_word
        GodManager.Text = Global.Outworldz.My.Resources.EstateManagerIsGod_word

        MapBest.Text = Global.Outworldz.My.Resources.Best_Prims
        MapBetter.Text = Global.Outworldz.My.Resources.Better_Prims
        MapGroupBox.Text = Global.Outworldz.My.Resources.Maps_word
        MapGood.Text = Global.Outworldz.My.Resources.Good_Warp3D_word
        MapNone.Text = Global.Outworldz.My.Resources.None
        Maps_Use_Default.Text = Global.Outworldz.My.Resources.Use_Default_word
        MapSimple.Text = Global.Outworldz.My.Resources.Simple_but_Fast_word

        MaxMAvatarsLabel.Text = Global.Outworldz.My.Resources.Max_Avatars
        MaxNPrimsLabel.Text = Global.Outworldz.My.Resources.Max_NumPrims

        MenuStrip2.Text = Global.Outworldz.My.Resources._0
        NonPhysPrimLabel.Text = Global.Outworldz.My.Resources.Nonphysical_Prim
        NoPublish.Text = Global.Outworldz.My.Resources.No_Publish_Items

        Physics_Default.Text = Global.Outworldz.My.Resources.Use_Default_word
        Physics_Separate.Text = Global.Outworldz.My.Resources.BP
        Physics_ubODE.Text = Global.Outworldz.My.Resources.UBODE_words
        PhysPrimLabel.Text = Global.Outworldz.My.Resources.Physical_Prim

        Publish.Text = Global.Outworldz.My.Resources.Publish_Items
        PublishDefault.Text = Global.Outworldz.My.Resources.Use_Default_word

        SaveButton.Text = Global.Outworldz.My.Resources.Save_word
        ScriptDefaultButton.Text = Global.Outworldz.My.Resources.Use_Default_word
        ScriptRateLabel.Text = Global.Outworldz.My.Resources.Script_Timer_Rate
        SkipAutoCheckBox.Text = Global.Outworldz.My.Resources.Skip_Autobackup_word
        SmartStartCheckBox.Text = Global.Outworldz.My.Resources.Smart_Start_word
        TidesCheckbox.Text = Global.Outworldz.My.Resources.Tide_Enable

        ToolTip1.SetToolTip(GodLevel, Global.Outworldz.My.Resources.AllowGodsTooltip)
        ToolTip1.SetToolTip(BirdsCheckBox, Global.Outworldz.My.Resources.GBoids)
        ToolTip1.SetToolTip(ClampPrimLabel, Global.Outworldz.My.Resources.ClampSize)
        ToolTip1.SetToolTip(ClampPrimSize, Global.Outworldz.My.Resources.ClampSize)
        ToolTip1.SetToolTip(CoordX, Global.Outworldz.My.Resources.Coordx)
        ToolTip1.SetToolTip(CoordY, Global.Outworldz.My.Resources.CoordY)
        ToolTip1.SetToolTip(DisableGBCheckBox, Global.Outworldz.My.Resources.Disable_Gloebits_text)
        ToolTip1.SetToolTip(DisallowForeigners, Global.Outworldz.My.Resources.No_HG)
        ToolTip1.SetToolTip(DisallowResidents, Global.Outworldz.My.Resources.Only_Owners)
        ToolTip1.SetToolTip(FrameRateLabel, Global.Outworldz.My.Resources.FRText)
        ToolTip1.SetToolTip(FrametimeBox, Global.Outworldz.My.Resources.FrameTime)
        ToolTip1.SetToolTip(PhysicsGroupbox, Global.Outworldz.My.Resources.Sim_Rate)
        ToolTip1.SetToolTip(GodManager, Global.Outworldz.My.Resources.EMGod)
        ToolTip1.SetToolTip(MaxAgents, Global.Outworldz.My.Resources.Max_Agents)
        ToolTip1.SetToolTip(MaxMAvatarsLabel, Global.Outworldz.My.Resources.Max_Agents)
        ToolTip1.SetToolTip(MaxNPrimsLabel, Global.Outworldz.My.Resources.Viewer_Stops_Counting)
        ToolTip1.SetToolTip(MaxPrims, Global.Outworldz.My.Resources.Not_Normal)
        ToolTip1.SetToolTip(NonphysicalPrimMax, Global.Outworldz.My.Resources.Normal_Prim)
        ToolTip1.SetToolTip(NonPhysPrimLabel, Global.Outworldz.My.Resources.Max_NonPhys)
        ToolTip1.SetToolTip(PhysicalPrimMax, Global.Outworldz.My.Resources.Max_Phys)
        ToolTip1.SetToolTip(PhysPrimLabel, Global.Outworldz.My.Resources.Max_Phys)
        ToolTip1.SetToolTip(GodEstate, Global.Outworldz.My.Resources.Region_Owner_Is_God_word)
        ToolTip1.SetToolTip(RegionName, Global.Outworldz.My.Resources.Region_Name)
        ToolTip1.SetToolTip(ScriptRateLabel, Global.Outworldz.My.Resources.Script_Timer_Text)
        ToolTip1.SetToolTip(ScriptRateLabel, Global.Outworldz.My.Resources.STComment)
        ToolTip1.SetToolTip(ScriptTimerTextBox, Global.Outworldz.My.Resources.STComment)
        ToolTip1.SetToolTip(SkipAutoCheckBox, Global.Outworldz.My.Resources.WillNotSave)
        ToolTip1.SetToolTip(SmartStartCheckBox, Global.Outworldz.My.Resources.GTide)
        ToolTip1.SetToolTip(TidesCheckbox, Global.Outworldz.My.Resources.GTide)
        ToolTip1.SetToolTip(TPCheckBox1, Global.Outworldz.My.Resources.Teleport_Tooltip)

        TPCheckBox1.Text = Global.Outworldz.My.Resources.Teleporter_Enable_word
        UUID.Name = Global.Outworldz.My.Resources.UUID

        'Scripting
        XEngineButton.Text = Global.Outworldz.My.Resources.XEngine_word
        YEngineButton.Text = Global.Outworldz.My.Resources.YEngine_word

    End Sub

    Public Sub Init(Name As String)

        Me.Focus()

        If Name Is Nothing Then Return
        Name = Name.Trim() ' remove spaces

        PropRegionClass = RegionMaker.Instance()

        ' NEW REGION
        If Name.Length = 0 Then
            IsNew1 = True
            Gods_Use_Default.Checked = True
            RegionName.Text = Global.Outworldz.My.Resources.Name_of_Region_Word
            UUID.Text = Guid.NewGuid().ToString
            CoordX.Text = (PropRegionClass.LargestX() + 8).ToString(Globalization.CultureInfo.InvariantCulture)
            CoordY.Text = (PropRegionClass.LargestY() + 0).ToString(Globalization.CultureInfo.InvariantCulture)
            EnabledCheckBox.Checked = True
            RadioButton1.Checked = True
            SmartStartCheckBox.Checked = False
            NonphysicalPrimMax.Text = 1024.ToString(Globalization.CultureInfo.InvariantCulture)
            PhysicalPrimMax.Text = 64.ToString(Globalization.CultureInfo.InvariantCulture)
            ClampPrimSize.Checked = False
            ConciergeCheckBox.Checked = False
            MaxPrims.Text = 45000.ToString(Globalization.CultureInfo.InvariantCulture)
            MaxAgents.Text = 100.ToString(Globalization.CultureInfo.InvariantCulture)
            RegionUUID = PropRegionClass.CreateRegion("New Region")
            Gods_Use_Default.Checked = True

            Core1Button.Checked = True
            Core2Button.Checked = True
            Core3Button.Checked = True
            Core4Button.Checked = True
            Core5Button.Checked = True
            Core6Button.Checked = True
            Core7Button.Checked = True
            Core8Button.Checked = True
            Core9Button.Checked = True
            Core10Button.Checked = True
            Core11Button.Checked = True
            Core12Button.Checked = True
            Core13Button.Checked = True
            Core14Button.Checked = True
            Core15Button.Checked = True
            Core16Button.Checked = True

            Normal.Checked = True
        Else
            ' OLD REGION EDITED all this is required to be filled in!
            IsNew1 = False

            RegionUUID = PropRegionClass.FindRegionByName(Name)
            APIKey.Text = PropRegionClass.OpensimWorldAPIKey(RegionUUID)
            Oldname1 = PropRegionClass.RegionName(RegionUUID) ' backup in case of rename
            EnabledCheckBox.Checked = PropRegionClass.RegionEnabled(RegionUUID)
            Me.Text = Name & " " & Global.Outworldz.My.Resources.Region_word ' on screen
            RegionName.Text = Name
            UUID.Text = RegionUUID
            NonphysicalPrimMax.Text = CStr(PropRegionClass.NonPhysicalPrimMax(RegionUUID))
            PhysicalPrimMax.Text = CStr(PropRegionClass.PhysicalPrimMax(RegionUUID))
            ClampPrimSize.Checked = PropRegionClass.ClampPrimSize(RegionUUID)
            MaxPrims.Text = PropRegionClass.MaxPrims(RegionUUID)
            MaxAgents.Text = PropRegionClass.MaxAgents(RegionUUID)
            RegionPort.Text = CStr(PropRegionClass.RegionPort(RegionUUID))

            If PropRegionClass.Priority(RegionUUID).Length = 0 Then
                Normal.Checked = True
            Else
                RealTime.Checked = PropRegionClass.Priority(RegionUUID) = "RealTime"
                High.Checked = PropRegionClass.Priority(RegionUUID) = "High"
                AboveNormal.Checked = PropRegionClass.Priority(RegionUUID) = "AboveNormal"
                Normal.Checked = PropRegionClass.Priority(RegionUUID) = "Normal"
                BelowNormal.Checked = PropRegionClass.Priority(RegionUUID) = "BelowNormal"
            End If

            Core1Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H1)
            Core2Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H2)
            Core3Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H4)
            Core4Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H8)
            Core5Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H10)
            Core6Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H20)
            Core7Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H40)
            Core8Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H80)
            Core9Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H100)
            Core10Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H200)
            Core11Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H400)
            Core12Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H800)
            Core13Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H1000)
            Core14Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H2000)
            Core15Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H4000)
            Core16Button.Checked = CBool(PropRegionClass.Cores(RegionUUID) And &H8000)

            Dim C = Environment.ProcessorCount
            If C >= 1 Then Core1Button.Visible = True Else Core1Button.Visible = False
            If C >= 2 Then Core2Button.Visible = True Else Core2Button.Visible = False
            If C >= 3 Then Core3Button.Visible = True Else Core3Button.Visible = False
            If C >= 4 Then Core4Button.Visible = True Else Core4Button.Visible = False
            If C >= 5 Then Core5Button.Visible = True Else Core5Button.Visible = False
            If C >= 6 Then Core6Button.Visible = True Else Core6Button.Visible = False
            If C >= 7 Then Core7Button.Visible = True Else Core7Button.Visible = False
            If C >= 8 Then Core8Button.Visible = True Else Core8Button.Visible = False
            If C >= 9 Then Core9Button.Visible = True Else Core9Button.Visible = False
            If C >= 10 Then Core10Button.Visible = True Else Core10Button.Visible = False
            If C >= 11 Then Core11Button.Visible = True Else Core11Button.Visible = False
            If C >= 12 Then Core12Button.Visible = True Else Core12Button.Visible = False
            If C >= 13 Then Core13Button.Visible = True Else Core13Button.Visible = False
            If C >= 14 Then Core14Button.Visible = True Else Core14Button.Visible = False
            If C >= 15 Then Core15Button.Visible = True Else Core15Button.Visible = False
            If C >= 16 Then Core16Button.Visible = True Else Core16Button.Visible = False

            If PropRegionClass.Cores(RegionUUID) = 0 Then
                Core1Button.Checked = True
                Core2Button.Checked = True
                Core3Button.Checked = True
                Core4Button.Checked = True
                Core5Button.Checked = True
                Core6Button.Checked = True
                Core7Button.Checked = True
                Core8Button.Checked = True
                Core9Button.Checked = True
                Core10Button.Checked = True
                Core11Button.Checked = True
                Core12Button.Checked = True
                Core13Button.Checked = True
                Core14Button.Checked = True
                Core15Button.Checked = True
                Core16Button.Checked = True
            End If

            ' Size buttons can be zero
            If PropRegionClass.SizeY(RegionUUID) = 0 Or PropRegionClass.SizeX(RegionUUID) = 0 Then
                RadioButton1.Checked = True
                BoxSize = 256
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 Then
                RadioButton1.Checked = True
                BoxSize = 256 * 1
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 2 Then
                RadioButton2.Checked = True
                BoxSize = 256 * 2
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 3 Then
                RadioButton3.Checked = True
                BoxSize = 256 * 3
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 4 Then
                RadioButton4.Checked = True
                BoxSize = 256 * 4
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 5 Then
                RadioButton5.Checked = True
                BoxSize = 256 * 5
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 6 Then
                RadioButton6.Checked = True
                BoxSize = 256 * 6
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 7 Then
                RadioButton7.Checked = True
                BoxSize = 256 * 7
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 8 Then
                RadioButton8.Checked = True
                BoxSize = 256 * 8
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 9 Then
                RadioButton9.Checked = True
                BoxSize = 256 * 9
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 10 Then
                RadioButton10.Checked = True
                BoxSize = 256 * 10
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 11 Then
                RadioButton11.Checked = True
                BoxSize = 256 * 11
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 12 Then
                RadioButton12.Checked = True
                BoxSize = 256 * 12
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 13 Then
                RadioButton13.Checked = True
                BoxSize = 256 * 14
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 14 Then
                RadioButton14.Checked = True
                BoxSize = 256 * 14
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 15 Then
                RadioButton15.Checked = True
                BoxSize = 256 * 15
            ElseIf PropRegionClass.SizeY(RegionUUID) = 256 * 16 Then
                RadioButton16.Checked = True
                BoxSize = 256 * 16
            Else
                RadioButton1.Checked = True
            End If

            ' global coordinates
            If PropRegionClass.CoordX(RegionUUID) <> 0 Then
                CoordX.Text = PropRegionClass.CoordX(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture)
            End If

            If PropRegionClass.CoordY(RegionUUID) <> 0 Then
                CoordY.Text = PropRegionClass.CoordY(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture)
            End If

        End If

        If PropOpensimIsRunning Then
            UUID.ReadOnly = True
        Else
            UUID.ReadOnly = False
        End If
        ' The following are all options.
        If PropRegionClass.DisallowResidents(RegionUUID) = "True" Then
            DisallowResidents.Checked = True
        End If

        If PropRegionClass.DisallowForeigners(RegionUUID) = "True" Then
            DisallowForeigners.Checked = True
        End If

        ScriptTimerTextBox.Text = PropRegionClass.MinTimerInterval(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture)
        FrametimeBox.Text = PropRegionClass.FrameTime(RegionUUID).ToString(Globalization.CultureInfo.InvariantCulture)

        If PropRegionClass.SkipAutobackup(RegionUUID) = "True" Then
            SkipAutoCheckBox.Checked = True
        End If
        If PropRegionClass.SmartStart(RegionUUID) = "True" Then
            SmartStartCheckBox.Checked = True
        End If

        Select Case PropRegionClass.DisableGloebits(RegionUUID)
            Case ""
                DisableGBCheckBox.Checked = False
            Case "False"
                DisableGBCheckBox.Checked = False
            Case "True"
                DisableGBCheckBox.Checked = True
        End Select

        RichTextBoxMap.Text = My.Resources.MapsHelp
        RichTextBoxOptions.Text = My.Resources.OptionsHelp
        RichTextBoxPhysics.Text = My.Resources.PhysicsHelp
        RichTextBoxPublicity.Text = My.Resources.PublicityHelp
        RichTextBoxPermissions.Text = My.Resources.PermissionsHelp
        RichTextBoxScripts.Text = My.Resources.ScriptsHelp
        RichTextBoxModules.Text = My.Resources.ModulesHelp

        RName1 = Name

        ''''''''''''''''''''''''''''' DREAMGRID REGION LOAD '''''''''''''''''

        If PropRegionClass.MapType(RegionUUID).Length = 0 Then
            Maps_Use_Default.Checked = True
            MapPicture.Image = Global.Outworldz.My.Resources.blankbox
        ElseIf PropRegionClass.MapType(RegionUUID) = "Simple" Then
            MapSimple.Checked = True
            MapPicture.Image = Global.Outworldz.My.Resources.Simple
        ElseIf PropRegionClass.MapType(RegionUUID) = "Good" Then
            MapGood.Checked = True
            MapPicture.Image = Global.Outworldz.My.Resources.Good
        ElseIf PropRegionClass.MapType(RegionUUID) = "Better" Then
            MapBetter.Checked = True
            MapPicture.Image = Global.Outworldz.My.Resources.Better
        ElseIf PropRegionClass.MapType(RegionUUID) = "Best" Then
            MapBest.Checked = True
            MapPicture.Image = Global.Outworldz.My.Resources.Best
        End If

        Select Case PropRegionClass.Physics(RegionUUID)
            Case "" : Physics_Default.Checked = True
            Case "-1" : Physics_Default.Checked = True
            Case "0" : Physics_Default.Checked = True
            Case "1" : Physics_ODE.Checked = True
            Case "2" : Physics_Bullet.Checked = True
            Case "3" : Physics_Separate.Checked = True
            Case "4" : Physics_ubODE.Checked = True
            Case "5" : Physics_Hybrid.Checked = True
            Case Else : Physics_Default.Checked = True
        End Select

        If PropRegionClass.GodDefault(RegionUUID) = "True" Then
            GodLevel.Checked = False
            GodEstate.Checked = False
            GodManager.Checked = False
            Gods_Use_Default.Checked = True
        Else
            Select Case PropRegionClass.AllowGods(RegionUUID)
                Case ""
                    GodLevel.Checked = False
                Case "False"
                    GodLevel.Checked = False
                    Gods_Use_Default.Checked = False
                Case "True"
                    GodLevel.Checked = True
                    Gods_Use_Default.Checked = False
            End Select

            Select Case PropRegionClass.RegionGod(RegionUUID)
                Case ""
                    GodEstate.Checked = False
                Case "False"
                    GodEstate.Checked = False
                    Gods_Use_Default.Checked = False
                Case "True"
                    GodEstate.Checked = True
                    Gods_Use_Default.Checked = False
            End Select

            Select Case PropRegionClass.ManagerGod(RegionUUID)
                Case ""
                    GodManager.Checked = False
                Case "False"
                    GodManager.Checked = False
                    Gods_Use_Default.Checked = False
                Case "True"
                    GodManager.Checked = True
                    Gods_Use_Default.Checked = False
            End Select

            ' if none selected, turn default on. This updates old code to new GodDefault global

            If PropRegionClass.AllowGods(RegionUUID).Length = 0 And
                 PropRegionClass.RegionGod(RegionUUID).Length = 0 And
                PropRegionClass.ManagerGod(RegionUUID).Length = 0 Then
                Gods_Use_Default.Checked = True
            End If
        End If

        Select Case PropRegionClass.RegionSnapShot(RegionUUID)
            Case ""
                PublishDefault.Checked = True
                NoPublish.Checked = False
                Publish.Checked = False
            Case "False"
                PublishDefault.Checked = False
                NoPublish.Checked = True
                Publish.Checked = False
            Case "True"
                PublishDefault.Checked = False
                NoPublish.Checked = False
                Publish.Checked = True
        End Select

        Select Case PropRegionClass.Birds(RegionUUID)
            Case ""
                BirdsCheckBox.Checked = False
            Case "False"
                BirdsCheckBox.Checked = False
            Case "True"
                BirdsCheckBox.Checked = True
        End Select

        Select Case PropRegionClass.Tides(RegionUUID)
            Case ""
                TidesCheckbox.Checked = False
            Case "False"
                TidesCheckbox.Checked = False
            Case "True"
                TidesCheckbox.Checked = True
        End Select

        Select Case PropRegionClass.Teleport(RegionUUID)
            Case ""
                TPCheckBox1.Checked = False
            Case "False"
                TPCheckBox1.Checked = False
            Case "True"
                TPCheckBox1.Checked = True
        End Select

        Select Case PropRegionClass.DisallowForeigners(RegionUUID)
            Case ""
                DisallowForeigners.Checked = False
            Case "False"
                DisallowForeigners.Checked = False
            Case "True"
                DisallowForeigners.Checked = True
        End Select

        Select Case PropRegionClass.DisallowResidents(RegionUUID)
            Case ""
                DisallowResidents.Checked = False
            Case "False"
                DisallowResidents.Checked = False
            Case "True"
                DisallowResidents.Checked = True
        End Select

        Select Case PropRegionClass.ScriptEngine(RegionUUID)
            Case ""
                ScriptDefaultButton.Checked = True
            Case "XEngine"
                XEngineButton.Checked = True
            Case "YEngine"
                YEngineButton.Checked = True
        End Select

        Select Case PropRegionClass.Concierge(RegionUUID)
            Case ""
                ConciergeCheckBox.Checked = False
            Case "True"
                ConciergeCheckBox.Checked = True
            Case "False"
                ConciergeCheckBox.Checked = False
        End Select

        _NotSmarttStart = SmartStartCheckBox.Checked

        Try
            Me.Show() ' time to show the results
            Me.Activate()

            Initted1 = True
            HelpOnce("Region")
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub FormRegion_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        If Changed1 Then
            FormSetup.PropChangedRegionSettings = True
            Dim v = MsgBox(My.Resources.Save_changes_word, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Save_changes_word)
            If v = vbYes Then
                Dim message = RegionValidate()
                If Len(message) > 0 Then
                    v = MsgBox(message + vbCrLf + Global.Outworldz.My.Resources.Discard_Exit, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Info_word)
                    If v = vbYes Then
                        Me.Close()
                    End If
                Else
                    WriteRegion(RegionUUID)
                    Firewall.SetFirewall()
                    RestartRobustIfNeeded(RegionUUID)
                    PropUpdateView() = True
                    Changed1 = False

                End If
            End If
        End If

    End Sub

#End Region

#Region "Events"

    Private Sub AllowGods_CheckedChanged(sender As Object, e As EventArgs) Handles GodLevel.CheckedChanged

        If GodLevel.Checked Then
            Gods_Use_Default.Checked = False
            Log(My.Resources.Info_word, "Region " + Name + " Is allowing Gods")
        Else
            Log(My.Resources.Info_word, "Region " + Name + " is not allowing Region Gods")
        End If

        If Initted1 Then Changed1 = True

    End Sub

    Private Sub BirdsCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles BirdsCheckBox.CheckedChanged

        If BirdsCheckBox.Checked Then
            Log(My.Resources.Info_word, "Region " + Name + " has birds enabled")
        End If
        If Initted1 Then Changed1 = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SaveButton.Click


        FormSetup.PropChangedRegionSettings = True
        Dim message = RegionValidate()
        If Len(message) > 0 Then
            Dim v = MsgBox(message + vbCrLf + Global.Outworldz.My.Resources.Discard_Exit, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Info_word)
            If v = vbYes Then
                Changed1 = False
            End If
        Else
            DeregisterRegionUUID(RegionUUID)
            WriteRegion(RegionUUID)
            Firewall.SetFirewall()
            RestartRobustIfNeeded(RegionUUID)
            PropUpdateView() = True
            Changed1 = False
        End If
        Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles DeregisterButton.Click

        Dim response As MsgBoxResult = MsgBox(My.Resources.Another_Region, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground)
        If response = vbYes Then

            StartMySQL()
            StartRobust()

            Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName.Text)
            If RegionUUID.Length > 0 Then
                If CheckPort(Settings.LANIP(), PropRegionClass.GroupPort(RegionUUID)) Then
                    ShutDown(RegionUUID)
                End If
                Dim loopctr = 120 ' wait 2 minutes
                While CheckPort(Settings.LANIP(), PropRegionClass.GroupPort(RegionUUID)) And loopctr > 0
                    Sleep(1000)
                    loopctr -= 1
                End While

                If loopctr > 0 Then
                    DeregisterRegionUUID(RegionUUID)
                    TextPrint(My.Resources.Region_Removed)
                End If
            End If

        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles DisallowForeigners.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles DisallowResidents.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub ClampPrimSize_CheckedChanged(sender As Object, e As EventArgs) Handles ClampPrimSize.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub ConciergeCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ConciergeCheckBox.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub CoordX_TextChanged(sender As Object, e As EventArgs) Handles CoordX.TextChanged

        If Not initted Then Return
        Dim digitsOnly As Regex = New Regex("[^\d]")
        CoordX.Text = digitsOnly.Replace(CoordX.Text, "")
        Changed1 = True

    End Sub

    Private Sub Coordy_TextChanged(sender As Object, e As EventArgs) Handles CoordY.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        CoordY.Text = digitsOnly.Replace(CoordY.Text, "")
        If Initted1 And CoordY.Text.Length >= 0 Then
            Changed1 = True
        End If

    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click

        If PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Stopped Then
            MsgBox(My.Resources.Regions_Are_Running, vbInformation Or vbMsgBoxSetForeground)
            Return
        End If

        Dim msg = MsgBox(My.Resources.Are_you_Sure_Delete_Region, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Info_word)
        If msg = vbYes Then


            Dim GroupName = PropRegionClass.GroupName(RegionUUID)
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)

            PropRegionClass.Delete_Region_Map(RegionUUID)

            DeleteMaps(RegionUUID)

            CopyFileFast(IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{GroupName}\Region\{RegionName}.ini"), IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{GroupName}\Region\{RegionName}.bak"))
            DeleteFile(IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{GroupName}\Region\{RegionName}.ini"))

            DeregisterRegionUUID(RegionUUID)

            PropRegionClass.DeleteRegion(RegionUUID)
            PropRegionClass.GetAllRegions()
            Changed1 = False
            PropUpdateView = True
        End If

        Me.Close()

    End Sub

    Private Sub EnabledCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles EnabledCheckBox.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub EnableMaxPrims_text(sender As Object, e As EventArgs) Handles MaxPrims.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        MaxPrims.Text = digitsOnly.Replace(MaxPrims.Text, "")
        If Initted1 Then Changed1 = True

    End Sub

    Private Sub RLost(sender As Object, e As EventArgs) Handles RegionName.LostFocus
        RegionName.Text = RegionName.Text.Trim() ' remove spaces
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub ScriptDefaultButton_CheckedChanged(sender As Object, e As EventArgs) Handles ScriptDefaultButton.CheckedChanged

        If ScriptDefaultButton.Checked Then
            XEngineButton.Checked = False
            YEngineButton.Checked = False
        End If

    End Sub

    Private Sub ScriptTimerTextBox_focusChanged(sender As Object, e As EventArgs) Handles ScriptTimerTextBox.LostFocus

        Try
            Dim value = Convert.ToDouble(ScriptTimerTextBox.Text, Globalization.CultureInfo.InvariantCulture)
            If value < 0.01 Then ScriptTimerTextBox.Text = ""
        Catch ex As Exception
            ScriptTimerTextBox.Text = ""
        End Try

        If Initted1 Then Changed1 = True
    End Sub

    Private Sub ScriptTimerTextBox_TextChanged(sender As Object, e As EventArgs) Handles ScriptTimerTextBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        ScriptTimerTextBox.Text = digitsOnly.Replace(ScriptTimerTextBox.Text, "")

        If Initted1 Then Changed1 = True

    End Sub

    Private Sub SkipAutoCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles SkipAutoCheckBox.CheckedChanged

        If Initted1 Then Changed1 = True

    End Sub

    Private Sub SmartStartCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles SmartStartCheckBox.CheckedChanged

        If Initted1 Then Changed1 = True

    End Sub

    Private Sub StopHGCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles DisableGBCheckBox.CheckedChanged

        If Initted1 Then Changed1 = True

    End Sub

    Private Sub TextBox1_FocusChanged(sender As Object, e As EventArgs) Handles FrametimeBox.LostFocus

        Try
            Dim value = Convert.ToDouble(FrametimeBox.Text, Globalization.CultureInfo.InvariantCulture)
            If value < 0.01 Then FrametimeBox.Text = ""
        Catch ex As Exception
            FrametimeBox.Text = ""
        End Try

        If Initted1 Then Changed1 = True

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles FrametimeBox.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d\.]")
        FrametimeBox.Text = digitsOnly.Replace(FrametimeBox.Text, "")
        If Initted1 Then Changed1 = True

    End Sub

    Private Sub TidesCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles TidesCheckbox.CheckedChanged

        If TidesCheckbox.Checked Then
            Log(My.Resources.Info_word, "Region " + Name + " has tides enabled")
        End If
        If Initted1 Then Changed1 = True

    End Sub

    Private Sub TPCheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles TPCheckBox1.CheckedChanged

        If TPCheckBox1.Checked Then
            Log(My.Resources.Info_word, $"Region {Name} has Teleport Board enabled")
        End If
        If Initted1 Then Changed1 = True

    End Sub

    Private Sub UUID_LostFocus(sender As Object, e As EventArgs) Handles UUID.LostFocus

        If UUID.Text <> UUID.Text And Initted1 Then
            Dim resp = MsgBox(My.Resources.Change_UUID, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Info_word)
            If resp = vbYes Then
                Changed1 = True
                Dim result As Guid
                If Guid.TryParse(UUID.Text, result) Then
                Else
                    Dim ok = MsgBox(My.Resources.NotValidUUID, MsgBoxStyle.OkCancel Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Info_word)
                    If ok = vbOK Then
                        UUID.Text = System.Guid.NewGuid.ToString
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub UUID_TextChanged(sender As Object, e As EventArgs) Handles UUID.TextChanged

        If Initted1 Then Changed1 = True

    End Sub

#End Region

#Region "Subs"

    Public Shared Function FilenameIsOK(ByVal fileName As String) As Boolean
        ' check for invalid chars in file name for INI file
        If fileName Is Nothing Then Return False
        Dim value As Boolean = False
        Try
            value = Not fileName.Intersect(Path.GetInvalidFileNameChars()).Any()
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Return value

    End Function

    Shared Function RegionChosen(regionName As String) As String

        Dim result As DialogResult
        Dim chosen As String

        Using Chooseform As New FormChooser ' form for choosing a set of regions
            ' Show testDialog as a modal dialog and determine if DialogResult = OK.
            Chooseform.FillGrid("Group")  ' populate the grid with either Group or RegionName
            result = Chooseform.ShowDialog()
            If result = DialogResult.Cancel Then
                Return ""
            End If

            Try
                ' Read the chosen sim name
                chosen = Chooseform.DataGridView.CurrentCell.Value.ToString()
                If chosen = "! Add New Name" Then
                    chosen = InputBox(My.Resources.Enter_Dos_Name, "", regionName)
                End If
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                chosen = ""
            End Try

        End Using
        Return chosen

    End Function

    Private Function RegionValidate() As String

        Dim Message As String

        If Len(RegionName.Text) = 0 Then
            Message = Global.Outworldz.My.Resources.Region_name_must_not_be_blank_word
            Return Message
        End If

        ' UUID
        Dim result As Guid
        If Not Guid.TryParse(UUID.Text, result) Then
            Message = Global.Outworldz.My.Resources.Region_UUID_Is_invalid_word & " " & UUID.Text
            Return Message
        End If

        ' global coordinates
        If Convert.ToInt32("0" & CoordX.Text, Globalization.CultureInfo.InvariantCulture) < 0 Then
            Message = Global.Outworldz.My.Resources.Region_Coordinate_X_cannot_be_less_than_0_word
            Return Message
        ElseIf Convert.ToInt32("0" & CoordX.Text, Globalization.CultureInfo.InvariantCulture) > 65536 Then
            Message = Global.Outworldz.My.Resources.Region_Coordinate_X_is_too_large
            Return Message
        End If

        If Convert.ToInt32("0" & CoordY.Text, Globalization.CultureInfo.InvariantCulture) < 32 Then
            Message = Global.Outworldz.My.Resources.Region_Coordinate_Y_cannot_be_less_than_32
            Return Message
        ElseIf Convert.ToInt32("0" & CoordY.Text, Globalization.CultureInfo.InvariantCulture) > 65536 Then
            Message = Global.Outworldz.My.Resources.Region_Coordinate_Y_Is_too_large
            Return Message
        End If

        Dim aresult As Guid
        If Not Guid.TryParse(UUID.Text, aresult) Then
            Message = Global.Outworldz.My.Resources.UUID0
            Return Message
        End If

        Try
            If (NonphysicalPrimMax.Text.Length = 0) Or (CType(NonphysicalPrimMax.Text, Integer) <= 0) Then
                Message = Global.Outworldz.My.Resources.NVNonPhysPrim
                Return Message
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Message = Global.Outworldz.My.Resources.NVNonPhysPrim
            Return Message
        End Try

        Try
            If (PhysicalPrimMax.Text.Length = 0) Or (CType(PhysicalPrimMax.Text, Integer) <= 0) Then
                Message = Global.Outworldz.My.Resources.NVPhysPrim
                Return Message
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Message = Global.Outworldz.My.Resources.NVPhysPrim
            Return Message
        End Try

        Try
            If (MaxPrims.Text.Length = 0) Or (CType(MaxPrims.Text, Integer) <= 0) Then
                Message = Global.Outworldz.My.Resources.NVMaxPrim
                Return Message
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Message = Global.Outworldz.My.Resources.NVMaxPrim
            Return Message
        End Try
        Try
            If (MaxAgents.Text.Length = 0) Or (CType(MaxAgents.Text, Integer) <= 0) Then
                Message = Global.Outworldz.My.Resources.NVMaxAgents
                Return Message
            End If
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Message = Global.Outworldz.My.Resources.NVMaxAgents
            Return Message
        End Try

        Return ""

    End Function

    ''' <returns>false if it fails</returns>
    Private Function WriteRegion(RegionUUID As String) As Boolean

        ' save the Region File, choose an existing DOS box to put it in, or make a new one
        ' rename is possible
        If Oldname1 <> RegionName.Text And Not IsNew1 Then
            Try
                StartMySQL()
                MysqlInterface.DeregisterRegionUUID(RegionUUID)

                My.Computer.FileSystem.RenameFile(PropRegionClass.RegionIniFilePath(RegionUUID), RegionName.Text + ".ini")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                TextPrint(My.Resources.Aborted_word)
                Return False
            End Try

            ' rename it
            Dim RegionIniFolderPath = PropRegionClass.RegionIniFolderPath(RegionUUID)
            PropRegionClass.RegionIniFilePath(RegionUUID) = RegionIniFolderPath + "/" + RegionName.Text + ".ini"
        End If

        ' might be a new region, so give them a choice

        If IsNew1 Then
            Dim NewGroup As String

            NewGroup = RegionChosen(RegionName.Text)
            If NewGroup.Length = 0 Then
                TextPrint(My.Resources.Aborted_word)
                Return False
            End If

            If Not Directory.Exists(PropRegionClass.RegionIniFilePath(RegionUUID)) Or PropRegionClass.RegionIniFilePath(RegionUUID).Length = 0 Then
                Try
                    Directory.CreateDirectory(Settings.OpensimBinPath & "Regions\" + NewGroup + "\Region")
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                    TextPrint(My.Resources.Aborted_word)
                    Return False
                End Try
            End If

            PropRegionClass.RegionIniFilePath(RegionUUID) = Settings.OpensimBinPath & "Regions\" + NewGroup + "\Region\" + RegionName.Text + ".ini"
            PropRegionClass.RegionIniFolderPath(RegionUUID) = System.IO.Path.GetDirectoryName(PropRegionClass.RegionIniFilePath(RegionUUID))
            PropRegionClass.GroupName(RegionUUID) = NewGroup

            Dim theEnd As Integer = PropRegionClass.RegionIniFolderPath(RegionUUID).LastIndexOf("\", StringComparison.InvariantCulture)
            PropRegionClass.OpensimIniPath(RegionUUID) = PropRegionClass.RegionIniFolderPath(RegionUUID).Substring(0, theEnd + 1)

        End If

        ' save the changes to the memory structure, then to disk

        'CPU Affinity
        Dim cores = 0
        If Core1Button.Checked Then cores += &H1
        If Core2Button.Checked Then cores += &H2
        If Core3Button.Checked Then cores += &H4
        If Core4Button.Checked Then cores += &H8
        If Core5Button.Checked Then cores += &H10
        If Core6Button.Checked Then cores += &H20
        If Core7Button.Checked Then cores += &H40
        If Core8Button.Checked Then cores += &H80
        If Core9Button.Checked Then cores += &H100
        If Core10Button.Checked Then cores += &H200
        If Core10Button.Checked Then cores += &H400
        If Core12Button.Checked Then cores += &H800
        If Core13Button.Checked Then cores += &H1000
        If Core14Button.Checked Then cores += &H2000
        If Core15Button.Checked Then cores += &H4000
        If Core16Button.Checked Then cores += &H8000

        If cores = 0 Then cores = &HFFFF
        PropRegionClass.Cores(RegionUUID) = cores

        If RealTime.Checked Then
            PropRegionClass.Priority(RegionUUID) = "RealTime"
        ElseIf High.Checked Then
            PropRegionClass.Priority(RegionUUID) = "High"
        ElseIf AboveNormal.Checked Then
            PropRegionClass.Priority(RegionUUID) = "AboveNormal"
        ElseIf Normal.Checked Then
            PropRegionClass.Priority(RegionUUID) = "Normal"
        ElseIf BelowNormal.Checked Then
            PropRegionClass.Priority(RegionUUID) = "BelowNormal"
        Else
            PropRegionClass.Priority(RegionUUID) = "Normal"
        End If

        PropRegionClass.CoordX(RegionUUID) = CInt("0" & CoordX.Text)
        PropRegionClass.CoordY(RegionUUID) = CInt("0" & CoordY.Text)
        PropRegionClass.RegionName(RegionUUID) = RegionName.Text

        If PropRegionClass.RegionPort(RegionUUID) = 0 Then
            Dim port = PropRegionClass.LargestPort + 1
            PropRegionClass.RegionPort(RegionUUID) = port
            PropRegionClass.GroupPort(RegionUUID) = port
        End If

        PropRegionClass.SizeX(RegionUUID) = BoxSize
        PropRegionClass.SizeY(RegionUUID) = BoxSize
        PropRegionClass.RegionEnabled(RegionUUID) = EnabledCheckBox.Checked
        PropRegionClass.NonPhysicalPrimMax(RegionUUID) = NonphysicalPrimMax.Text
        PropRegionClass.PhysicalPrimMax(RegionUUID) = PhysicalPrimMax.Text
        PropRegionClass.ClampPrimSize(RegionUUID) = ClampPrimSize.Checked
        PropRegionClass.Concierge(RegionUUID) = CStr(ConciergeCheckBox.Checked)
        PropRegionClass.MaxAgents(RegionUUID) = MaxAgents.Text
        PropRegionClass.MaxPrims(RegionUUID) = MaxPrims.Text
        PropRegionClass.MinTimerInterval(RegionUUID) = ScriptTimerTextBox.Text
        PropRegionClass.FrameTime(RegionUUID) = FrametimeBox.Text

        Dim Snapshot As String = ""
        If PublishDefault.Checked Then
            Snapshot = ""
        ElseIf NoPublish.Checked Then
            Snapshot = "False"
        ElseIf Publish.Checked Then
            Snapshot = "True"
        End If

        PropRegionClass.RegionSnapShot(RegionUUID) = Snapshot

        Dim Map As String = ""
        If MapNone.Checked Then
            Map = ""
        ElseIf MapSimple.Checked Then
            Map = "Simple"
        ElseIf MapGood.Checked Then
            Map = "Good"
        ElseIf MapBetter.Checked Then
            Map = "Better"
        ElseIf MapBest.Checked Then
            Map = "Best"
        End If


        PropRegionClass.MapType(RegionUUID) = Map

        'Case "" : Physics_Default.Checked = True
        'Case "-1" : Physics_Default.Checked = True
        'Case "0" : Physics_Default.Checked = True
        'Case "1" : Physics_ODE.Checked = True
        'Case "2" : Physics_Bullet.Checked = True
        'Case "3" : Physics_Separate.Checked = True
        'Case "4" : Physics_ubODE.Checked = True
        'Case "5" : Physics_Hybrid.Checked = True
        'Case Else : Physics_Default.Checked = True

        Dim Phys As String
        If Physics_Default.Checked Then
            Phys = ""
        ElseIf Physics_ODE.Checked Then
            Phys = "1"
        ElseIf Physics_Bullet.Checked Then
            Phys = "2"
        ElseIf Physics_Separate.Checked Then
            Phys = "3"
        ElseIf Physics_ubODE.Checked Then
            Phys = "4"
        ElseIf Physics_Hybrid.Checked Then
            Phys = "5"
        Else
            Phys = "2"
        End If

        PropRegionClass.Physics(RegionUUID) = Phys

        If Gods_Use_Default.Checked Then
            PropRegionClass.GodDefault(RegionUUID) = "True"
            PropRegionClass.AllowGods(RegionUUID) = ""
            PropRegionClass.RegionGod(RegionUUID) = ""
            PropRegionClass.ManagerGod(RegionUUID) = ""
        Else
            PropRegionClass.GodDefault(RegionUUID) = "False"
            PropRegionClass.AllowGods(RegionUUID) = CStr(GodLevel.Checked)
            PropRegionClass.RegionGod(RegionUUID) = CStr(GodEstate.Checked)
            PropRegionClass.ManagerGod(RegionUUID) = CStr(GodManager.Checked)
        End If

        If DisallowForeigners.Checked Then
            PropRegionClass.DisallowForeigners(RegionUUID) = "True"
        Else
            PropRegionClass.DisallowForeigners(RegionUUID) = ""
        End If

        If DisallowResidents.Checked Then
            PropRegionClass.DisallowResidents(RegionUUID) = "True"
        Else
            PropRegionClass.DisallowResidents(RegionUUID) = ""
        End If

        If SkipAutoCheckBox.Checked Then
            PropRegionClass.SkipAutobackup(RegionUUID) = "True"
        Else
            PropRegionClass.SkipAutobackup(RegionUUID) = ""
        End If

        If BirdsCheckBox.Checked Then
            PropRegionClass.Birds(RegionUUID) = "True"
        Else
            PropRegionClass.Birds(RegionUUID) = ""
        End If

        If TidesCheckbox.Checked Then
            PropRegionClass.Tides(RegionUUID) = "True"
        Else
            PropRegionClass.Tides(RegionUUID) = ""
        End If

        If TPCheckBox1.Checked Then
            PropRegionClass.Teleport(RegionUUID) = "True"
        Else
            PropRegionClass.Teleport(RegionUUID) = ""
        End If

        If DisableGBCheckBox.Checked Then
            PropRegionClass.DisableGloebits(RegionUUID) = "True"
        Else
            PropRegionClass.DisableGloebits(RegionUUID) = ""
        End If

        If NoPublish.Checked Then
            PropRegionClass.GDPR(RegionUUID) = "False"
        Else
            PropRegionClass.GDPR(RegionUUID) = ""
        End If

        If Publish.Checked Then
            PropRegionClass.GDPR(RegionUUID) = "True"
        Else
            PropRegionClass.GDPR(RegionUUID) = ""
        End If

        If SmartStartCheckBox.Checked Then
            PropRegionClass.SmartStart(RegionUUID) = "True"
        Else
            PropRegionClass.SmartStart(RegionUUID) = ""
        End If

        Dim ScriptEngine As String = ""
        PropRegionClass.ScriptEngine(RegionUUID) = ""
        If XEngineButton.Checked = True Then
            ScriptEngine = "XEngine"
            PropRegionClass.ScriptEngine(RegionUUID) = "XEngine"
        End If
        If YEngineButton.Checked = True Then
            ScriptEngine = "YEngine"
            PropRegionClass.ScriptEngine(RegionUUID) = "YEngine"
        End If

        Dim Region = "; * Regions configuration file" &
                        "; * This Is Your World. See Common Settings->[Region Settings]." & vbCrLf &
                        "; Automatically changed by Dreamworld" & vbCrLf &
                        "[" & RegionName.Text & "]" & vbCrLf &
                        "RegionUUID = " & UUID.Text & vbCrLf &
                        "Location = " & CoordX.Text & "," & CoordY.Text & vbCrLf &
                        "InternalAddress = 0.0.0.0" & vbCrLf &
                        "InternalPort = " & PropRegionClass.RegionPort(RegionUUID) & vbCrLf &
                        "GroupPort = " & PropRegionClass.GroupPort(RegionUUID) & vbCrLf &
                        "AllowAlternatePorts = False" & vbCrLf &
                        "ExternalHostName = " & Settings.ExternalHostName & vbCrLf &
                        "SizeX = " & BoxSize & vbCrLf &
                        "SizeY = " & BoxSize & vbCrLf &
                        "Enabled = " & CStr(EnabledCheckBox.Checked) & vbCrLf &
                        "NonPhysicalPrimMax = " & NonphysicalPrimMax.Text & vbCrLf &
                        "PhysicalPrimMax = " & PhysicalPrimMax.Text & vbCrLf &
                        "ClampPrimSize = " & CStr(ClampPrimSize.Checked) & vbCrLf &
                        "Concierge =  " & CStr(ConciergeCheckBox.Checked) & vbCrLf &
                        "MaxAgents = " & MaxAgents.Text & vbCrLf &
                        "MaxPrims = " & MaxPrims.Text & vbCrLf &
                        "RegionType = Estate" & vbCrLf & vbCrLf &
                        ";# Extended region properties from Dreamgrid" & vbCrLf &
                        "MinTimerInterval = " & ScriptTimerTextBox.Text & vbCrLf &
                        "FrameTime = " & FrametimeBox.Text & vbCrLf &
                        "RegionSnapShot = " & Snapshot & vbCrLf &
                        "MapType = " & Map & vbCrLf &
                        "Physics = " & Phys & vbCrLf &
                        "GodDefault = " & PropRegionClass.GodDefault(RegionUUID) & vbCrLf &
                        "AllowGods = " & PropRegionClass.AllowGods(RegionUUID) & vbCrLf &
                        "RegionGod = " & PropRegionClass.RegionGod(RegionUUID) & vbCrLf &
                        "ManagerGod = " & PropRegionClass.ManagerGod(RegionUUID) & vbCrLf &
                        "Birds = " & PropRegionClass.Birds(RegionUUID) & vbCrLf &
                        "Tides = " & PropRegionClass.Tides(RegionUUID) & vbCrLf &
                        "Teleport = " & PropRegionClass.Teleport(RegionUUID) & vbCrLf &
                        "DisableGloebits = " & PropRegionClass.DisableGloebits(RegionUUID) & vbCrLf &
                        "DisallowForeigners = " & PropRegionClass.DisallowForeigners(RegionUUID) & vbCrLf &
                        "DisallowResidents = " & PropRegionClass.DisallowResidents(RegionUUID) & vbCrLf &
                        "SkipAutoBackup = " & PropRegionClass.SkipAutobackup(RegionUUID) & vbCrLf &
                        "ScriptEngine = " & PropRegionClass.ScriptEngine(RegionUUID) & vbCrLf &
                        "Publicity = " & PropRegionClass.GDPR(RegionUUID) & vbCrLf &
                        "OpensimWorldAPIKey = " & PropRegionClass.OpensimWorldAPIKey(RegionUUID) & vbCrLf &
                        "Priority = " & PropRegionClass.Priority(RegionUUID) & vbCrLf &
                        "Cores = " & CStr(PropRegionClass.Cores(RegionUUID)) & vbCrLf &
                        "SmartStart = " & PropRegionClass.SmartStart(RegionUUID) & vbCrLf

        'Debug.Print(Region)

        FileStuff.CopyFileFast(PropRegionClass.RegionIniFilePath(RegionUUID), PropRegionClass.RegionIniFilePath(RegionUUID) & ".bak")
        Try
            Using outputFile As New StreamWriter(PropRegionClass.RegionIniFilePath(RegionUUID), False)
                outputFile.Write(Region)
            End Using
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            MsgBox(My.Resources.Cannot_save_region_word + ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxSetForeground)
            Return False

        End Try

        If PropRegionClass.GetAllRegions() = -1 Then Return False

        PropUpdateView = True
        Oldname1 = RegionName.Text

        Return True

    End Function

#End Region

#Region "Scripting"

    Private Sub APIKey_TextChanged(sender As Object, e As EventArgs) Handles APIKey.TextChanged

        If Initted1 Then Changed1 = True
        PropRegionClass.OpensimWorldAPIKey(RegionUUID) = APIKey.Text

    End Sub

    Private Sub BasicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BasicsToolStripMenuItem.Click
        HelpManual("Region")
    End Sub

    Private Sub Hyperica_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles Hyperica.LinkClicked
        Dim webAddress As String = "https://hyperica.com"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub MapsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MapsToolStripMenuItem.Click
        HelpManual("Map Overrides")
    End Sub

    Private Sub ModulesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModulesToolStripMenuItem.Click
        HelpManual("Module Overrides")
    End Sub

    Private Sub Opensimworld_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles Opensimworld.LinkClicked
        Dim webAddress As String = "https://opensimworld.com"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        HelpManual("Options for Regions")
    End Sub

    Private Sub PermissionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PermissionsToolStripMenuItem.Click
        HelpManual("Permission Overrides")
    End Sub

    Private Sub PhysicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PhysicsToolStripMenuItem.Click
        HelpManual("Physics Overrides")
    End Sub

    Private Sub PublicityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PublicityToolStripMenuItem.Click
        HelpManual("Publicity Overrides")
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        BoxSize = 256
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton10_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton10.CheckedChanged
        BoxSize = 256 * 10
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton11_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton11.CheckedChanged
        BoxSize = 256 * 11
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        BoxSize = 256 * 12
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        BoxSize = 256 * 13
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton14_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton14.CheckedChanged
        BoxSize = 256 * 14
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton15_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton15.CheckedChanged
        BoxSize = 256 * 15
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton16_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton16.CheckedChanged
        BoxSize = 256 * 16
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        BoxSize = 256 * 2
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        BoxSize = 256 * 3
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        BoxSize = 256 * 4
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        BoxSize = 256 * 5
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        BoxSize = 256 * 6
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        BoxSize = 256 * 7
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        BoxSize = 256 * 8
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        BoxSize = 256 * 9
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub RegionPort_TextChanged(sender As Object, e As EventArgs) Handles RegionPort.TextChanged

    End Sub

    Private Sub ScriptsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScriptsToolStripMenuItem.Click
        HelpManual("Script Overrides")
    End Sub

    Private Sub XEngineButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles XEngineButton.CheckedChanged

        If XEngineButton.Checked Then
            ScriptDefaultButton.Checked = False
            YEngineButton.Checked = False
        End If
        If Initted1 Then Changed1 = True

    End Sub

    Private Sub YEngineButton_CheckedChanged_1(sender As Object, e As EventArgs) Handles YEngineButton.CheckedChanged

        If YEngineButton.Checked Then
            ScriptDefaultButton.Checked = False
            XEngineButton.Checked = False
        End If
        If Initted1 Then Changed1 = True

    End Sub

    Private Sub RealTime_CheckedChanged(sender As Object, e As EventArgs) Handles RealTime.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub High_CheckedChanged(sender As Object, e As EventArgs) Handles High.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub AboveNormal_CheckedChanged(sender As Object, e As EventArgs) Handles AboveNormal.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Normal_CheckedChanged(sender As Object, e As EventArgs) Handles Normal.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub BelowNormal_CheckedChanged(sender As Object, e As EventArgs) Handles BelowNormal.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core1Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core1Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core2Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core2Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core3Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core3Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core4Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core4Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core5Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core5Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core6Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core6Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core7Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core7Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core8Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core8Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core9Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core9Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core10Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core10Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core11Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core11Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core12Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core12Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core13Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core13Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core14Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core14Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core15Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core15Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub Core16Button_CheckedChanged(sender As Object, e As EventArgs) Handles Core16Button.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub MapNone_CheckedChanged(sender As Object, e As EventArgs) Handles MapNone.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub MapSimple_CheckedChanged(sender As Object, e As EventArgs) Handles MapSimple.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub MapGood_CheckedChanged(sender As Object, e As EventArgs) Handles MapGood.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub MapBetter_CheckedChanged(sender As Object, e As EventArgs) Handles MapBetter.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub

    Private Sub MapBest_CheckedChanged(sender As Object, e As EventArgs) Handles MapBest.CheckedChanged
        If Initted1 Then Changed1 = True
    End Sub




#End Region

End Class
