<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRegion
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ToolTip.SetToolTip(System.Windows.Forms.Control,System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ButtonBase.set_Text(System.String)")>
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.Label.set_Text(System.String)")>
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRegion))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CoordY = New System.Windows.Forms.TextBox()
        Me.CoordX = New System.Windows.Forms.TextBox()
        Me.RegionName = New System.Windows.Forms.TextBox()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.MaxAgents = New System.Windows.Forms.TextBox()
        Me.NonPhysPrimLabel = New System.Windows.Forms.Label()
        Me.PhysPrimLabel = New System.Windows.Forms.Label()
        Me.PhysicalPrimMax = New System.Windows.Forms.TextBox()
        Me.ClampPrimLabel = New System.Windows.Forms.Label()
        Me.MaxNPrimsLabel = New System.Windows.Forms.Label()
        Me.MaxPrims = New System.Windows.Forms.TextBox()
        Me.NonphysicalPrimMax = New System.Windows.Forms.TextBox()
        Me.MaxMAvatarsLabel = New System.Windows.Forms.Label()
        Me.ClampPrimSize = New System.Windows.Forms.CheckBox()
        Me.BirdsCheckBox = New System.Windows.Forms.CheckBox()
        Me.TidesCheckbox = New System.Windows.Forms.CheckBox()
        Me.TPCheckBox1 = New System.Windows.Forms.CheckBox()
        Me.AllowGods = New System.Windows.Forms.CheckBox()
        Me.ManagerGod = New System.Windows.Forms.CheckBox()
        Me.RegionGod = New System.Windows.Forms.CheckBox()
        Me.SmartStartCheckBox = New System.Windows.Forms.CheckBox()
        Me.ScriptRateLabel = New System.Windows.Forms.Label()
        Me.ScriptTimerTextBox = New System.Windows.Forms.TextBox()
        Me.DisableGBCheckBox = New System.Windows.Forms.CheckBox()
        Me.DisallowForeigners = New System.Windows.Forms.CheckBox()
        Me.DisallowResidents = New System.Windows.Forms.CheckBox()
        Me.FrametimeBox = New System.Windows.Forms.TextBox()
        Me.SkipAutoCheckBox = New System.Windows.Forms.CheckBox()
        Me.FrameRateLabel = New System.Windows.Forms.Label()
        Me.RadioButton8 = New System.Windows.Forms.RadioButton()
        Me.RadioButton7 = New System.Windows.Forms.RadioButton()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.RadioButton10 = New System.Windows.Forms.RadioButton()
        Me.RadioButton9 = New System.Windows.Forms.RadioButton()
        Me.RadioButton12 = New System.Windows.Forms.RadioButton()
        Me.RadioButton11 = New System.Windows.Forms.RadioButton()
        Me.RadioButton16 = New System.Windows.Forms.RadioButton()
        Me.RadioButton15 = New System.Windows.Forms.RadioButton()
        Me.RadioButton14 = New System.Windows.Forms.RadioButton()
        Me.RadioButton13 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Physics_Hybrid = New System.Windows.Forms.RadioButton()
        Me.Physics_Bullet = New System.Windows.Forms.RadioButton()
        Me.Physics_Default = New System.Windows.Forms.RadioButton()
        Me.Physics_Separate = New System.Windows.Forms.RadioButton()
        Me.Physics_ubODE = New System.Windows.Forms.RadioButton()
        Me.RegionPort = New System.Windows.Forms.TextBox()
        Me.Advanced = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UUID = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.EnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.ScriptDefaultButton = New System.Windows.Forms.RadioButton()
        Me.XEngineButton = New System.Windows.Forms.RadioButton()
        Me.YEngineButton = New System.Windows.Forms.RadioButton()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.ConciergeCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Publish = New System.Windows.Forms.RadioButton()
        Me.NoPublish = New System.Windows.Forms.RadioButton()
        Me.PublishDefault = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Gods_Use_Default = New System.Windows.Forms.CheckBox()
        Me.MapBox = New System.Windows.Forms.GroupBox()
        Me.Maps_Use_Default = New System.Windows.Forms.RadioButton()
        Me.MapPicture = New System.Windows.Forms.PictureBox()
        Me.MapNone = New System.Windows.Forms.RadioButton()
        Me.MapSimple = New System.Windows.Forms.RadioButton()
        Me.MapBetter = New System.Windows.Forms.RadioButton()
        Me.MapBest = New System.Windows.Forms.RadioButton()
        Me.MapGood = New System.Windows.Forms.RadioButton()
        Me.DeregisterButton = New System.Windows.Forms.Button()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Physics_ODE = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.Advanced.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.MapBox.SuspendLayout()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CoordY
        '
        Me.CoordY.Location = New System.Drawing.Point(163, 13)
        Me.CoordY.Name = "CoordY"
        Me.CoordY.Size = New System.Drawing.Size(37, 20)
        Me.CoordY.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.CoordY, Global.Outworldz.My.Resources.Resources.CoordY)
        '
        'CoordX
        '
        Me.CoordX.Location = New System.Drawing.Point(97, 13)
        Me.CoordX.Name = "CoordX"
        Me.CoordX.Size = New System.Drawing.Size(40, 20)
        Me.CoordX.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.CoordX, Global.Outworldz.My.Resources.Resources.Coordx)
        '
        'RegionName
        '
        Me.RegionName.Location = New System.Drawing.Point(27, 70)
        Me.RegionName.Name = "RegionName"
        Me.RegionName.Size = New System.Drawing.Size(183, 20)
        Me.RegionName.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.RegionName, Global.Outworldz.My.Resources.Resources.Region_Name)
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(20, 85)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton4.TabIndex = 6
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "4 X 4"
        Me.ToolTip1.SetToolTip(Me.RadioButton4, "1024 X 1024")
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(20, 63)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton3.TabIndex = 5
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "3 X 3"
        Me.ToolTip1.SetToolTip(Me.RadioButton3, "768 X 768")
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(20, 39)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton2.TabIndex = 4
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "2 X 2"
        Me.ToolTip1.SetToolTip(Me.RadioButton2, "512 X 512")
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(20, 16)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton1.TabIndex = 3
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "1 X 1"
        Me.ToolTip1.SetToolTip(Me.RadioButton1, "256 X 256")
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'MaxAgents
        '
        Me.MaxAgents.Location = New System.Drawing.Point(12, 201)
        Me.MaxAgents.Name = "MaxAgents"
        Me.MaxAgents.Size = New System.Drawing.Size(40, 20)
        Me.MaxAgents.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.MaxAgents, Global.Outworldz.My.Resources.Resources.Max_Agents)
        '
        'NonPhysPrimLabel
        '
        Me.NonPhysPrimLabel.AutoSize = True
        Me.NonPhysPrimLabel.Location = New System.Drawing.Point(65, 104)
        Me.NonPhysPrimLabel.Name = "NonPhysPrimLabel"
        Me.NonPhysPrimLabel.Size = New System.Drawing.Size(111, 13)
        Me.NonPhysPrimLabel.TabIndex = 30
        Me.NonPhysPrimLabel.Text = "Nonphysical Prim Size"
        Me.ToolTip1.SetToolTip(Me.NonPhysPrimLabel, Global.Outworldz.My.Resources.Resources.Max_NonPhys)
        '
        'PhysPrimLabel
        '
        Me.PhysPrimLabel.AutoSize = True
        Me.PhysPrimLabel.Location = New System.Drawing.Point(69, 128)
        Me.PhysPrimLabel.Name = "PhysPrimLabel"
        Me.PhysPrimLabel.Size = New System.Drawing.Size(115, 13)
        Me.PhysPrimLabel.TabIndex = 31
        Me.PhysPrimLabel.Text = "Physical Prim Max Size"
        Me.ToolTip1.SetToolTip(Me.PhysPrimLabel, Global.Outworldz.My.Resources.Resources.Max_Phys)
        '
        'PhysicalPrimMax
        '
        Me.PhysicalPrimMax.Location = New System.Drawing.Point(12, 125)
        Me.PhysicalPrimMax.Name = "PhysicalPrimMax"
        Me.PhysicalPrimMax.Size = New System.Drawing.Size(40, 20)
        Me.PhysicalPrimMax.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.PhysicalPrimMax, Global.Outworldz.My.Resources.Resources.Max_Phys)
        '
        'ClampPrimLabel
        '
        Me.ClampPrimLabel.AutoSize = True
        Me.ClampPrimLabel.Location = New System.Drawing.Point(69, 153)
        Me.ClampPrimLabel.Name = "ClampPrimLabel"
        Me.ClampPrimLabel.Size = New System.Drawing.Size(82, 13)
        Me.ClampPrimLabel.TabIndex = 34
        Me.ClampPrimLabel.Text = "Clamp Prim Size"
        Me.ToolTip1.SetToolTip(Me.ClampPrimLabel, Global.Outworldz.My.Resources.Resources.ClampSize)
        '
        'MaxNPrimsLabel
        '
        Me.MaxNPrimsLabel.AutoSize = True
        Me.MaxNPrimsLabel.Location = New System.Drawing.Point(69, 176)
        Me.MaxNPrimsLabel.Name = "MaxNPrimsLabel"
        Me.MaxNPrimsLabel.Size = New System.Drawing.Size(160, 13)
        Me.MaxNPrimsLabel.TabIndex = 36
        Me.MaxNPrimsLabel.Text = "Max Number of Prims in a Parcel"
        Me.ToolTip1.SetToolTip(Me.MaxNPrimsLabel, Global.Outworldz.My.Resources.Resources.Viewer_Stops_Counting)
        '
        'MaxPrims
        '
        Me.MaxPrims.Location = New System.Drawing.Point(12, 172)
        Me.MaxPrims.Name = "MaxPrims"
        Me.MaxPrims.Size = New System.Drawing.Size(40, 20)
        Me.MaxPrims.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.MaxPrims, Global.Outworldz.My.Resources.Resources.Not_Normal)
        '
        'NonphysicalPrimMax
        '
        Me.NonphysicalPrimMax.Location = New System.Drawing.Point(12, 100)
        Me.NonphysicalPrimMax.Name = "NonphysicalPrimMax"
        Me.NonphysicalPrimMax.Size = New System.Drawing.Size(40, 20)
        Me.NonphysicalPrimMax.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.NonphysicalPrimMax, Global.Outworldz.My.Resources.Resources.Normal_Prim)
        '
        'MaxMAvatarsLabel
        '
        Me.MaxMAvatarsLabel.AutoSize = True
        Me.MaxMAvatarsLabel.Location = New System.Drawing.Point(69, 201)
        Me.MaxMAvatarsLabel.Name = "MaxMAvatarsLabel"
        Me.MaxMAvatarsLabel.Size = New System.Drawing.Size(155, 13)
        Me.MaxMAvatarsLabel.TabIndex = 38
        Me.MaxMAvatarsLabel.Text = "Max number of Avatars + NPCs"
        Me.ToolTip1.SetToolTip(Me.MaxMAvatarsLabel, Global.Outworldz.My.Resources.Resources.Max_Agents)
        '
        'ClampPrimSize
        '
        Me.ClampPrimSize.AutoSize = True
        Me.ClampPrimSize.Location = New System.Drawing.Point(12, 152)
        Me.ClampPrimSize.Name = "ClampPrimSize"
        Me.ClampPrimSize.Size = New System.Drawing.Size(15, 14)
        Me.ClampPrimSize.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.ClampPrimSize, Global.Outworldz.My.Resources.Resources.ClampSize)
        Me.ClampPrimSize.UseVisualStyleBackColor = True
        '
        'BirdsCheckBox
        '
        Me.BirdsCheckBox.AutoSize = True
        Me.BirdsCheckBox.Location = New System.Drawing.Point(15, 19)
        Me.BirdsCheckBox.Name = "BirdsCheckBox"
        Me.BirdsCheckBox.Size = New System.Drawing.Size(82, 17)
        Me.BirdsCheckBox.TabIndex = 21
        Me.BirdsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Bird_Module_word
        Me.ToolTip1.SetToolTip(Me.BirdsCheckBox, Global.Outworldz.My.Resources.Resources.GBoids)
        Me.BirdsCheckBox.UseVisualStyleBackColor = True
        '
        'TidesCheckbox
        '
        Me.TidesCheckbox.AutoSize = True
        Me.TidesCheckbox.Location = New System.Drawing.Point(15, 41)
        Me.TidesCheckbox.Name = "TidesCheckbox"
        Me.TidesCheckbox.Size = New System.Drawing.Size(194, 17)
        Me.TidesCheckbox.TabIndex = 21
        Me.TidesCheckbox.Text = Global.Outworldz.My.Resources.Resources.Tide_Enable
        Me.ToolTip1.SetToolTip(Me.TidesCheckbox, Global.Outworldz.My.Resources.Resources.GTide)
        Me.TidesCheckbox.UseVisualStyleBackColor = True
        '
        'TPCheckBox1
        '
        Me.TPCheckBox1.AutoSize = True
        Me.TPCheckBox1.Location = New System.Drawing.Point(15, 65)
        Me.TPCheckBox1.Name = "TPCheckBox1"
        Me.TPCheckBox1.Size = New System.Drawing.Size(125, 17)
        Me.TPCheckBox1.TabIndex = 22
        Me.TPCheckBox1.Text = Global.Outworldz.My.Resources.Resources.Teleporter_Enable_word
        Me.ToolTip1.SetToolTip(Me.TPCheckBox1, Global.Outworldz.My.Resources.Resources.Teleport_Tooltip)
        Me.TPCheckBox1.UseVisualStyleBackColor = True
        '
        'AllowGods
        '
        Me.AllowGods.AutoSize = True
        Me.AllowGods.Location = New System.Drawing.Point(15, 52)
        Me.AllowGods.Name = "AllowGods"
        Me.AllowGods.Size = New System.Drawing.Size(138, 17)
        Me.AllowGods.TabIndex = 1858
        Me.AllowGods.Text = Global.Outworldz.My.Resources.Resources.Allow_Or_Disallow_Gods_word
        Me.ToolTip1.SetToolTip(Me.AllowGods, Global.Outworldz.My.Resources.Resources.AllowGodsTooltip)
        Me.AllowGods.UseVisualStyleBackColor = True
        '
        'ManagerGod
        '
        Me.ManagerGod.AutoSize = True
        Me.ManagerGod.Location = New System.Drawing.Point(15, 99)
        Me.ManagerGod.Name = "ManagerGod"
        Me.ManagerGod.Size = New System.Drawing.Size(132, 17)
        Me.ManagerGod.TabIndex = 6
        Me.ManagerGod.Text = Global.Outworldz.My.Resources.Resources.EstateManagerIsGod_word
        Me.ToolTip1.SetToolTip(Me.ManagerGod, Global.Outworldz.My.Resources.Resources.EMGod)
        Me.ManagerGod.UseVisualStyleBackColor = True
        '
        'RegionGod
        '
        Me.RegionGod.AutoSize = True
        Me.RegionGod.Location = New System.Drawing.Point(15, 75)
        Me.RegionGod.Name = "RegionGod"
        Me.RegionGod.Size = New System.Drawing.Size(127, 17)
        Me.RegionGod.TabIndex = 1855
        Me.RegionGod.Text = Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word
        Me.ToolTip1.SetToolTip(Me.RegionGod, Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word)
        Me.RegionGod.UseVisualStyleBackColor = True
        '
        'SmartStartCheckBox
        '
        Me.SmartStartCheckBox.AutoSize = True
        Me.SmartStartCheckBox.Location = New System.Drawing.Point(270, 475)
        Me.SmartStartCheckBox.Name = "SmartStartCheckBox"
        Me.SmartStartCheckBox.Size = New System.Drawing.Size(78, 17)
        Me.SmartStartCheckBox.TabIndex = 23
        Me.SmartStartCheckBox.Text = Global.Outworldz.My.Resources.Resources.Smart_Start_word
        Me.ToolTip1.SetToolTip(Me.SmartStartCheckBox, Global.Outworldz.My.Resources.Resources.GTide)
        Me.SmartStartCheckBox.UseVisualStyleBackColor = True
        Me.SmartStartCheckBox.Visible = False
        '
        'ScriptRateLabel
        '
        Me.ScriptRateLabel.AutoSize = True
        Me.ScriptRateLabel.Location = New System.Drawing.Point(69, 229)
        Me.ScriptRateLabel.Name = "ScriptRateLabel"
        Me.ScriptRateLabel.Size = New System.Drawing.Size(113, 13)
        Me.ScriptRateLabel.TabIndex = 41
        Me.ScriptRateLabel.Text = "Script Timer Rate (0.2)"
        Me.ToolTip1.SetToolTip(Me.ScriptRateLabel, Global.Outworldz.My.Resources.Resources.Script_Timer_Text)
        '
        'ScriptTimerTextBox
        '
        Me.ScriptTimerTextBox.Location = New System.Drawing.Point(12, 229)
        Me.ScriptTimerTextBox.Name = "ScriptTimerTextBox"
        Me.ScriptTimerTextBox.Size = New System.Drawing.Size(40, 20)
        Me.ScriptTimerTextBox.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.ScriptTimerTextBox, Global.Outworldz.My.Resources.Resources.STComment)
        '
        'DisableGBCheckBox
        '
        Me.DisableGBCheckBox.AutoSize = True
        Me.DisableGBCheckBox.Location = New System.Drawing.Point(15, 85)
        Me.DisableGBCheckBox.Name = "DisableGBCheckBox"
        Me.DisableGBCheckBox.Size = New System.Drawing.Size(116, 17)
        Me.DisableGBCheckBox.TabIndex = 24
        Me.DisableGBCheckBox.Text = Global.Outworldz.My.Resources.Resources.Disable_Gloebits_word
        Me.ToolTip1.SetToolTip(Me.DisableGBCheckBox, Global.Outworldz.My.Resources.Resources.Disable_Gloebits_text)
        Me.DisableGBCheckBox.UseVisualStyleBackColor = True
        '
        'DisallowForeigners
        '
        Me.DisallowForeigners.AutoSize = True
        Me.DisallowForeigners.Location = New System.Drawing.Point(15, 105)
        Me.DisallowForeigners.Name = "DisallowForeigners"
        Me.DisallowForeigners.Size = New System.Drawing.Size(135, 17)
        Me.DisallowForeigners.TabIndex = 25
        Me.DisallowForeigners.Text = Global.Outworldz.My.Resources.Resources.Disable_Foreigners_word
        Me.ToolTip1.SetToolTip(Me.DisallowForeigners, Global.Outworldz.My.Resources.Resources.No_HG)
        Me.DisallowForeigners.UseVisualStyleBackColor = True
        '
        'DisallowResidents
        '
        Me.DisallowResidents.AutoSize = True
        Me.DisallowResidents.Location = New System.Drawing.Point(15, 127)
        Me.DisallowResidents.Name = "DisallowResidents"
        Me.DisallowResidents.Size = New System.Drawing.Size(128, 17)
        Me.DisallowResidents.TabIndex = 26
        Me.DisallowResidents.Text = Global.Outworldz.My.Resources.Resources.Disable_Residents
        Me.ToolTip1.SetToolTip(Me.DisallowResidents, Global.Outworldz.My.Resources.Resources.Only_Owners)
        Me.DisallowResidents.UseVisualStyleBackColor = True
        '
        'FrametimeBox
        '
        Me.FrametimeBox.Location = New System.Drawing.Point(12, 257)
        Me.FrametimeBox.Name = "FrametimeBox"
        Me.FrametimeBox.Size = New System.Drawing.Size(40, 20)
        Me.FrametimeBox.TabIndex = 42
        Me.ToolTip1.SetToolTip(Me.FrametimeBox, Global.Outworldz.My.Resources.Resources.FrameTime)
        '
        'SkipAutoCheckBox
        '
        Me.SkipAutoCheckBox.AutoSize = True
        Me.SkipAutoCheckBox.Location = New System.Drawing.Point(15, 149)
        Me.SkipAutoCheckBox.Name = "SkipAutoCheckBox"
        Me.SkipAutoCheckBox.Size = New System.Drawing.Size(162, 17)
        Me.SkipAutoCheckBox.TabIndex = 27
        Me.SkipAutoCheckBox.Text = Global.Outworldz.My.Resources.Resources.Skip_Autobackup_word
        Me.ToolTip1.SetToolTip(Me.SkipAutoCheckBox, Global.Outworldz.My.Resources.Resources.WillNotSave)
        Me.SkipAutoCheckBox.UseVisualStyleBackColor = True
        '
        'FrameRateLabel
        '
        Me.FrameRateLabel.AutoSize = True
        Me.FrameRateLabel.Location = New System.Drawing.Point(69, 257)
        Me.FrameRateLabel.Name = "FrameRateLabel"
        Me.FrameRateLabel.Size = New System.Drawing.Size(104, 13)
        Me.FrameRateLabel.TabIndex = 43
        Me.FrameRateLabel.Text = "Frame Rate (0.0909)"
        Me.ToolTip1.SetToolTip(Me.FrameRateLabel, Global.Outworldz.My.Resources.Resources.FRText)
        '
        'RadioButton8
        '
        Me.RadioButton8.AutoSize = True
        Me.RadioButton8.Location = New System.Drawing.Point(89, 83)
        Me.RadioButton8.Name = "RadioButton8"
        Me.RadioButton8.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton8.TabIndex = 10
        Me.RadioButton8.TabStop = True
        Me.RadioButton8.Text = "8 X 8"
        Me.ToolTip1.SetToolTip(Me.RadioButton8, "2,048 X 2,048")
        Me.RadioButton8.UseVisualStyleBackColor = True
        '
        'RadioButton7
        '
        Me.RadioButton7.AutoSize = True
        Me.RadioButton7.Location = New System.Drawing.Point(89, 60)
        Me.RadioButton7.Name = "RadioButton7"
        Me.RadioButton7.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton7.TabIndex = 9
        Me.RadioButton7.TabStop = True
        Me.RadioButton7.Text = "7 X 7"
        Me.ToolTip1.SetToolTip(Me.RadioButton7, "1792 X 1792")
        Me.RadioButton7.UseVisualStyleBackColor = True
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Location = New System.Drawing.Point(89, 37)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton6.TabIndex = 8
        Me.RadioButton6.TabStop = True
        Me.RadioButton6.Text = "6 X 6"
        Me.ToolTip1.SetToolTip(Me.RadioButton6, "1536 X 1536")
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(89, 15)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton5.TabIndex = 7
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "5 X 5"
        Me.ToolTip1.SetToolTip(Me.RadioButton5, "1280 X 1280")
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton10
        '
        Me.RadioButton10.AutoSize = True
        Me.RadioButton10.Location = New System.Drawing.Point(153, 37)
        Me.RadioButton10.Name = "RadioButton10"
        Me.RadioButton10.Size = New System.Drawing.Size(62, 17)
        Me.RadioButton10.TabIndex = 12
        Me.RadioButton10.TabStop = True
        Me.RadioButton10.Text = "10 X 10"
        Me.ToolTip1.SetToolTip(Me.RadioButton10, "2560 X 2560")
        Me.RadioButton10.UseVisualStyleBackColor = True
        '
        'RadioButton9
        '
        Me.RadioButton9.AutoSize = True
        Me.RadioButton9.Location = New System.Drawing.Point(153, 15)
        Me.RadioButton9.Name = "RadioButton9"
        Me.RadioButton9.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton9.TabIndex = 11
        Me.RadioButton9.TabStop = True
        Me.RadioButton9.Text = "9 X 9"
        Me.ToolTip1.SetToolTip(Me.RadioButton9, "2304 X 2304")
        Me.RadioButton9.UseVisualStyleBackColor = True
        '
        'RadioButton12
        '
        Me.RadioButton12.AutoSize = True
        Me.RadioButton12.Location = New System.Drawing.Point(155, 83)
        Me.RadioButton12.Name = "RadioButton12"
        Me.RadioButton12.Size = New System.Drawing.Size(62, 17)
        Me.RadioButton12.TabIndex = 14
        Me.RadioButton12.TabStop = True
        Me.RadioButton12.Text = "12 X 12"
        Me.ToolTip1.SetToolTip(Me.RadioButton12, "3072 X 3072")
        Me.RadioButton12.UseVisualStyleBackColor = True
        '
        'RadioButton11
        '
        Me.RadioButton11.AutoSize = True
        Me.RadioButton11.Location = New System.Drawing.Point(155, 60)
        Me.RadioButton11.Name = "RadioButton11"
        Me.RadioButton11.Size = New System.Drawing.Size(62, 17)
        Me.RadioButton11.TabIndex = 13
        Me.RadioButton11.TabStop = True
        Me.RadioButton11.Text = "11 X 11"
        Me.ToolTip1.SetToolTip(Me.RadioButton11, "2816 X 2816")
        Me.RadioButton11.UseVisualStyleBackColor = True
        '
        'RadioButton16
        '
        Me.RadioButton16.AutoSize = True
        Me.RadioButton16.Location = New System.Drawing.Point(224, 83)
        Me.RadioButton16.Name = "RadioButton16"
        Me.RadioButton16.Size = New System.Drawing.Size(62, 17)
        Me.RadioButton16.TabIndex = 18
        Me.RadioButton16.TabStop = True
        Me.RadioButton16.Text = "16 X 16"
        Me.ToolTip1.SetToolTip(Me.RadioButton16, "4096 X 4096")
        Me.RadioButton16.UseVisualStyleBackColor = True
        '
        'RadioButton15
        '
        Me.RadioButton15.AutoSize = True
        Me.RadioButton15.Location = New System.Drawing.Point(224, 60)
        Me.RadioButton15.Name = "RadioButton15"
        Me.RadioButton15.Size = New System.Drawing.Size(62, 17)
        Me.RadioButton15.TabIndex = 17
        Me.RadioButton15.TabStop = True
        Me.RadioButton15.Text = "15 X 15"
        Me.ToolTip1.SetToolTip(Me.RadioButton15, "3840 X 3840")
        Me.RadioButton15.UseVisualStyleBackColor = True
        '
        'RadioButton14
        '
        Me.RadioButton14.AutoSize = True
        Me.RadioButton14.Location = New System.Drawing.Point(223, 37)
        Me.RadioButton14.Name = "RadioButton14"
        Me.RadioButton14.Size = New System.Drawing.Size(62, 17)
        Me.RadioButton14.TabIndex = 16
        Me.RadioButton14.TabStop = True
        Me.RadioButton14.Text = "14 X 14"
        Me.ToolTip1.SetToolTip(Me.RadioButton14, "3584 X 3584")
        Me.RadioButton14.UseVisualStyleBackColor = True
        '
        'RadioButton13
        '
        Me.RadioButton13.AutoSize = True
        Me.RadioButton13.Location = New System.Drawing.Point(223, 15)
        Me.RadioButton13.Name = "RadioButton13"
        Me.RadioButton13.Size = New System.Drawing.Size(62, 17)
        Me.RadioButton13.TabIndex = 15
        Me.RadioButton13.TabStop = True
        Me.RadioButton13.Text = "13 X 13"
        Me.ToolTip1.SetToolTip(Me.RadioButton13, "3328 X 3328")
        Me.RadioButton13.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Physics_ODE)
        Me.GroupBox1.Controls.Add(Me.Physics_Hybrid)
        Me.GroupBox1.Controls.Add(Me.Physics_Bullet)
        Me.GroupBox1.Controls.Add(Me.Physics_Default)
        Me.GroupBox1.Controls.Add(Me.Physics_Separate)
        Me.GroupBox1.Controls.Add(Me.Physics_ubODE)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 254)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(228, 164)
        Me.GroupBox1.TabIndex = 1879
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Physics"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, Global.Outworldz.My.Resources.Resources.Sim_Rate)
        '
        'Physics_Hybrid
        '
        Me.Physics_Hybrid.AutoSize = True
        Me.Physics_Hybrid.Location = New System.Drawing.Point(15, 135)
        Me.Physics_Hybrid.Name = "Physics_Hybrid"
        Me.Physics_Hybrid.Size = New System.Drawing.Size(55, 17)
        Me.Physics_Hybrid.TabIndex = 139
        Me.Physics_Hybrid.TabStop = True
        Me.Physics_Hybrid.Text = "Hybrid"
        Me.Physics_Hybrid.UseVisualStyleBackColor = True
        '
        'Physics_Bullet
        '
        Me.Physics_Bullet.AutoSize = True
        Me.Physics_Bullet.Location = New System.Drawing.Point(15, 91)
        Me.Physics_Bullet.Name = "Physics_Bullet"
        Me.Physics_Bullet.Size = New System.Drawing.Size(92, 17)
        Me.Physics_Bullet.TabIndex = 138
        Me.Physics_Bullet.TabStop = True
        Me.Physics_Bullet.Text = "Bullet physics "
        Me.Physics_Bullet.UseVisualStyleBackColor = True
        '
        'Physics_Default
        '
        Me.Physics_Default.AutoSize = True
        Me.Physics_Default.Location = New System.Drawing.Point(15, 20)
        Me.Physics_Default.Name = "Physics_Default"
        Me.Physics_Default.Size = New System.Drawing.Size(81, 17)
        Me.Physics_Default.TabIndex = 137
        Me.Physics_Default.TabStop = True
        Me.Physics_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.Physics_Default.UseVisualStyleBackColor = True
        '
        'Physics_Separate
        '
        Me.Physics_Separate.AutoSize = True
        Me.Physics_Separate.Location = New System.Drawing.Point(15, 113)
        Me.Physics_Separate.Name = "Physics_Separate"
        Me.Physics_Separate.Size = New System.Drawing.Size(177, 17)
        Me.Physics_Separate.TabIndex = 37
        Me.Physics_Separate.TabStop = True
        Me.Physics_Separate.Text = Global.Outworldz.My.Resources.Resources.BP
        Me.Physics_Separate.UseVisualStyleBackColor = True
        '
        'Physics_ubODE
        '
        Me.Physics_ubODE.AutoSize = True
        Me.Physics_ubODE.Location = New System.Drawing.Point(15, 67)
        Me.Physics_ubODE.Name = "Physics_ubODE"
        Me.Physics_ubODE.Size = New System.Drawing.Size(153, 17)
        Me.Physics_ubODE.TabIndex = 35
        Me.Physics_ubODE.TabStop = True
        Me.Physics_ubODE.Text = Global.Outworldz.My.Resources.Resources.UBODE_words
        Me.Physics_ubODE.UseVisualStyleBackColor = True
        '
        'RegionPort
        '
        Me.RegionPort.Enabled = False
        Me.RegionPort.Location = New System.Drawing.Point(97, 39)
        Me.RegionPort.Name = "RegionPort"
        Me.RegionPort.Size = New System.Drawing.Size(40, 20)
        Me.RegionPort.TabIndex = 39
        '
        'Advanced
        '
        Me.Advanced.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Advanced.Controls.Add(Me.Label16)
        Me.Advanced.Controls.Add(Me.FrameRateLabel)
        Me.Advanced.Controls.Add(Me.FrametimeBox)
        Me.Advanced.Controls.Add(Me.ScriptRateLabel)
        Me.Advanced.Controls.Add(Me.ScriptTimerTextBox)
        Me.Advanced.Controls.Add(Me.RegionPort)
        Me.Advanced.Controls.Add(Me.ClampPrimSize)
        Me.Advanced.Controls.Add(Me.MaxMAvatarsLabel)
        Me.Advanced.Controls.Add(Me.ClampPrimLabel)
        Me.Advanced.Controls.Add(Me.NonphysicalPrimMax)
        Me.Advanced.Controls.Add(Me.MaxNPrimsLabel)
        Me.Advanced.Controls.Add(Me.PhysicalPrimMax)
        Me.Advanced.Controls.Add(Me.Label6)
        Me.Advanced.Controls.Add(Me.PhysPrimLabel)
        Me.Advanced.Controls.Add(Me.MaxPrims)
        Me.Advanced.Controls.Add(Me.NonPhysPrimLabel)
        Me.Advanced.Controls.Add(Me.MaxAgents)
        Me.Advanced.Controls.Add(Me.Label4)
        Me.Advanced.Controls.Add(Me.Label1)
        Me.Advanced.Controls.Add(Me.UUID)
        Me.Advanced.Controls.Add(Me.CoordY)
        Me.Advanced.Controls.Add(Me.CoordX)
        Me.Advanced.Location = New System.Drawing.Point(15, 256)
        Me.Advanced.Name = "Advanced"
        Me.Advanced.Size = New System.Drawing.Size(305, 291)
        Me.Advanced.TabIndex = 26
        Me.Advanced.TabStop = False
        Me.Advanced.Text = "Regions"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(143, 43)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 13)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "Region Port"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "UUID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Map  X:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(143, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 17
        '
        'UUID
        '
        Me.UUID.Enabled = False
        Me.UUID.Location = New System.Drawing.Point(9, 71)
        Me.UUID.Name = "UUID"
        Me.UUID.Size = New System.Drawing.Size(215, 20)
        Me.UUID.TabIndex = 15
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButton16)
        Me.GroupBox2.Controls.Add(Me.RadioButton15)
        Me.GroupBox2.Controls.Add(Me.RadioButton14)
        Me.GroupBox2.Controls.Add(Me.RadioButton13)
        Me.GroupBox2.Controls.Add(Me.RadioButton12)
        Me.GroupBox2.Controls.Add(Me.RadioButton11)
        Me.GroupBox2.Controls.Add(Me.RadioButton10)
        Me.GroupBox2.Controls.Add(Me.RadioButton9)
        Me.GroupBox2.Controls.Add(Me.RadioButton8)
        Me.GroupBox2.Controls.Add(Me.RadioButton7)
        Me.GroupBox2.Controls.Add(Me.RadioButton6)
        Me.GroupBox2.Controls.Add(Me.RadioButton5)
        Me.GroupBox2.Controls.Add(Me.RadioButton4)
        Me.GroupBox2.Controls.Add(Me.RadioButton3)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 101)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(297, 113)
        Me.GroupBox2.TabIndex = 23
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sim Size"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(148, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(148, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 2
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(24, 220)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(59, 23)
        Me.SaveButton.TabIndex = 10
        Me.SaveButton.Text = Global.Outworldz.My.Resources.Resources.Save_word
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        Me.DeleteButton.Location = New System.Drawing.Point(237, 223)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(57, 23)
        Me.DeleteButton.TabIndex = 11
        Me.DeleteButton.Text = Global.Outworldz.My.Resources.Resources.Delete_word
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'EnabledCheckBox
        '
        Me.EnabledCheckBox.AutoSize = True
        Me.EnabledCheckBox.Location = New System.Drawing.Point(27, 45)
        Me.EnabledCheckBox.Name = "EnabledCheckBox"
        Me.EnabledCheckBox.Size = New System.Drawing.Size(65, 17)
        Me.EnabledCheckBox.TabIndex = 2
        Me.EnabledCheckBox.Text = Global.Outworldz.My.Resources.Resources.Enabled_word
        Me.EnabledCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox6.Controls.Add(Me.GroupBox8)
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        Me.GroupBox6.Controls.Add(Me.GroupBox3)
        Me.GroupBox6.Controls.Add(Me.SmartStartCheckBox)
        Me.GroupBox6.Controls.Add(Me.GroupBox4)
        Me.GroupBox6.Controls.Add(Me.MapBox)
        Me.GroupBox6.Controls.Add(Me.GroupBox1)
        Me.GroupBox6.Location = New System.Drawing.Point(317, 33)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(483, 513)
        Me.GroupBox6.TabIndex = 1879
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Region Specific Settings"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.ScriptDefaultButton)
        Me.GroupBox8.Controls.Add(Me.XEngineButton)
        Me.GroupBox8.Controls.Add(Me.YEngineButton)
        Me.GroupBox8.Location = New System.Drawing.Point(0, 424)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(236, 89)
        Me.GroupBox8.TabIndex = 1887
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Script Engine"
        '
        'ScriptDefaultButton
        '
        Me.ScriptDefaultButton.AutoSize = True
        Me.ScriptDefaultButton.Location = New System.Drawing.Point(15, 25)
        Me.ScriptDefaultButton.Name = "ScriptDefaultButton"
        Me.ScriptDefaultButton.Size = New System.Drawing.Size(81, 17)
        Me.ScriptDefaultButton.TabIndex = 1858
        Me.ScriptDefaultButton.TabStop = True
        Me.ScriptDefaultButton.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.ScriptDefaultButton.UseVisualStyleBackColor = True
        '
        'XEngineButton
        '
        Me.XEngineButton.AutoSize = True
        Me.XEngineButton.Location = New System.Drawing.Point(13, 47)
        Me.XEngineButton.Name = "XEngineButton"
        Me.XEngineButton.Size = New System.Drawing.Size(68, 17)
        Me.XEngineButton.TabIndex = 7
        Me.XEngineButton.TabStop = True
        Me.XEngineButton.Text = Global.Outworldz.My.Resources.Resources.XEngine_word
        Me.XEngineButton.UseVisualStyleBackColor = True
        '
        'YEngineButton
        '
        Me.YEngineButton.AutoSize = True
        Me.YEngineButton.Location = New System.Drawing.Point(12, 67)
        Me.YEngineButton.Name = "YEngineButton"
        Me.YEngineButton.Size = New System.Drawing.Size(68, 17)
        Me.YEngineButton.TabIndex = 9
        Me.YEngineButton.TabStop = True
        Me.YEngineButton.Text = Global.Outworldz.My.Resources.Resources.YEngine_word
        Me.YEngineButton.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.ConciergeCheckBox)
        Me.GroupBox7.Controls.Add(Me.SkipAutoCheckBox)
        Me.GroupBox7.Controls.Add(Me.DisallowResidents)
        Me.GroupBox7.Controls.Add(Me.DisallowForeigners)
        Me.GroupBox7.Controls.Add(Me.DisableGBCheckBox)
        Me.GroupBox7.Controls.Add(Me.TPCheckBox1)
        Me.GroupBox7.Controls.Add(Me.TidesCheckbox)
        Me.GroupBox7.Controls.Add(Me.BirdsCheckBox)
        Me.GroupBox7.Location = New System.Drawing.Point(255, 281)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(223, 197)
        Me.GroupBox7.TabIndex = 1881
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Modules"
        '
        'ConciergeCheckBox
        '
        Me.ConciergeCheckBox.AutoSize = True
        Me.ConciergeCheckBox.Location = New System.Drawing.Point(15, 171)
        Me.ConciergeCheckBox.Name = "ConciergeCheckBox"
        Me.ConciergeCheckBox.Size = New System.Drawing.Size(111, 17)
        Me.ConciergeCheckBox.TabIndex = 28
        Me.ConciergeCheckBox.Text = "Announce Visitors"
        Me.ConciergeCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Publish)
        Me.GroupBox3.Controls.Add(Me.NoPublish)
        Me.GroupBox3.Controls.Add(Me.PublishDefault)
        Me.GroupBox3.Location = New System.Drawing.Point(17, 19)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(231, 101)
        Me.GroupBox3.TabIndex = 1883
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Publicity"
        '
        'Publish
        '
        Me.Publish.AutoSize = True
        Me.Publish.Location = New System.Drawing.Point(11, 73)
        Me.Publish.Name = "Publish"
        Me.Publish.Size = New System.Drawing.Size(175, 17)
        Me.Publish.TabIndex = 1881
        Me.Publish.TabStop = True
        Me.Publish.Text = Global.Outworldz.My.Resources.Resources.Publish_Items
        Me.Publish.UseVisualStyleBackColor = True
        '
        'NoPublish
        '
        Me.NoPublish.AutoSize = True
        Me.NoPublish.Location = New System.Drawing.Point(11, 51)
        Me.NoPublish.Name = "NoPublish"
        Me.NoPublish.Size = New System.Drawing.Size(144, 17)
        Me.NoPublish.TabIndex = 1880
        Me.NoPublish.TabStop = True
        Me.NoPublish.Text = Global.Outworldz.My.Resources.Resources.No_Publish_Items
        Me.NoPublish.UseVisualStyleBackColor = True
        '
        'PublishDefault
        '
        Me.PublishDefault.AutoSize = True
        Me.PublishDefault.Location = New System.Drawing.Point(11, 27)
        Me.PublishDefault.Name = "PublishDefault"
        Me.PublishDefault.Size = New System.Drawing.Size(81, 17)
        Me.PublishDefault.TabIndex = 1879
        Me.PublishDefault.TabStop = True
        Me.PublishDefault.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.PublishDefault.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Gods_Use_Default)
        Me.GroupBox4.Controls.Add(Me.AllowGods)
        Me.GroupBox4.Controls.Add(Me.ManagerGod)
        Me.GroupBox4.Controls.Add(Me.RegionGod)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 126)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(239, 121)
        Me.GroupBox4.TabIndex = 1882
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Permissions"
        '
        'Gods_Use_Default
        '
        Me.Gods_Use_Default.AutoSize = True
        Me.Gods_Use_Default.Location = New System.Drawing.Point(15, 27)
        Me.Gods_Use_Default.Name = "Gods_Use_Default"
        Me.Gods_Use_Default.Size = New System.Drawing.Size(82, 17)
        Me.Gods_Use_Default.TabIndex = 1859
        Me.Gods_Use_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.Gods_Use_Default.UseVisualStyleBackColor = True
        '
        'MapBox
        '
        Me.MapBox.Controls.Add(Me.Maps_Use_Default)
        Me.MapBox.Controls.Add(Me.MapPicture)
        Me.MapBox.Controls.Add(Me.MapNone)
        Me.MapBox.Controls.Add(Me.MapSimple)
        Me.MapBox.Controls.Add(Me.MapBetter)
        Me.MapBox.Controls.Add(Me.MapBest)
        Me.MapBox.Controls.Add(Me.MapGood)
        Me.MapBox.Location = New System.Drawing.Point(255, 16)
        Me.MapBox.Name = "MapBox"
        Me.MapBox.Size = New System.Drawing.Size(223, 247)
        Me.MapBox.TabIndex = 1881
        Me.MapBox.TabStop = False
        Me.MapBox.Text = "Maps"
        '
        'Maps_Use_Default
        '
        Me.Maps_Use_Default.AutoSize = True
        Me.Maps_Use_Default.Location = New System.Drawing.Point(15, 23)
        Me.Maps_Use_Default.Name = "Maps_Use_Default"
        Me.Maps_Use_Default.Size = New System.Drawing.Size(81, 17)
        Me.Maps_Use_Default.TabIndex = 1858
        Me.Maps_Use_Default.TabStop = True
        Me.Maps_Use_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.Maps_Use_Default.UseVisualStyleBackColor = True
        '
        'MapPicture
        '
        Me.MapPicture.InitialImage = CType(resources.GetObject("MapPicture.InitialImage"), System.Drawing.Image)
        Me.MapPicture.Location = New System.Drawing.Point(31, 148)
        Me.MapPicture.Name = "MapPicture"
        Me.MapPicture.Size = New System.Drawing.Size(113, 93)
        Me.MapPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.MapPicture.TabIndex = 138
        Me.MapPicture.TabStop = False
        '
        'MapNone
        '
        Me.MapNone.AutoSize = True
        Me.MapNone.Location = New System.Drawing.Point(13, 41)
        Me.MapNone.Name = "MapNone"
        Me.MapNone.Size = New System.Drawing.Size(51, 17)
        Me.MapNone.TabIndex = 7
        Me.MapNone.TabStop = True
        Me.MapNone.Text = Global.Outworldz.My.Resources.Resources.None
        Me.MapNone.UseVisualStyleBackColor = True
        '
        'MapSimple
        '
        Me.MapSimple.AutoSize = True
        Me.MapSimple.Location = New System.Drawing.Point(13, 61)
        Me.MapSimple.Name = "MapSimple"
        Me.MapSimple.Size = New System.Drawing.Size(94, 17)
        Me.MapSimple.TabIndex = 8
        Me.MapSimple.TabStop = True
        Me.MapSimple.Text = Global.Outworldz.My.Resources.Resources.Simple_but_Fast_word
        Me.MapSimple.UseVisualStyleBackColor = True
        '
        'MapBetter
        '
        Me.MapBetter.AutoSize = True
        Me.MapBetter.Location = New System.Drawing.Point(15, 100)
        Me.MapBetter.Name = "MapBetter"
        Me.MapBetter.Size = New System.Drawing.Size(116, 17)
        Me.MapBetter.TabIndex = 10
        Me.MapBetter.TabStop = True
        Me.MapBetter.Text = Global.Outworldz.My.Resources.Resources.Better_Prims
        Me.MapBetter.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.MapBetter.UseVisualStyleBackColor = True
        '
        'MapBest
        '
        Me.MapBest.AutoSize = True
        Me.MapBest.Location = New System.Drawing.Point(13, 121)
        Me.MapBest.Name = "MapBest"
        Me.MapBest.Size = New System.Drawing.Size(171, 17)
        Me.MapBest.TabIndex = 11
        Me.MapBest.TabStop = True
        Me.MapBest.Text = Global.Outworldz.My.Resources.Resources.Best_Prims
        Me.MapBest.UseVisualStyleBackColor = True
        '
        'MapGood
        '
        Me.MapGood.AutoSize = True
        Me.MapGood.Location = New System.Drawing.Point(15, 80)
        Me.MapGood.Name = "MapGood"
        Me.MapGood.Size = New System.Drawing.Size(100, 17)
        Me.MapGood.TabIndex = 9
        Me.MapGood.TabStop = True
        Me.MapGood.Text = Global.Outworldz.My.Resources.Resources.Good_Warp3D_word
        Me.MapGood.UseVisualStyleBackColor = True
        '
        'DeregisterButton
        '
        Me.DeregisterButton.Location = New System.Drawing.Point(121, 220)
        Me.DeregisterButton.Name = "DeregisterButton"
        Me.DeregisterButton.Size = New System.Drawing.Size(75, 23)
        Me.DeregisterButton.TabIndex = 1880
        Me.DeregisterButton.Text = Global.Outworldz.My.Resources.Resources.Deregister_word
        Me.DeregisterButton.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(809, 30)
        Me.MenuStrip2.TabIndex = 1888
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(68, 28)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'Physics_ODE
        '
        Me.Physics_ODE.AutoSize = True
        Me.Physics_ODE.Location = New System.Drawing.Point(15, 43)
        Me.Physics_ODE.Name = "Physics_ODE"
        Me.Physics_ODE.Size = New System.Drawing.Size(131, 17)
        Me.Physics_ODE.TabIndex = 140
        Me.Physics_ODE.TabStop = True
        Me.Physics_ODE.Text = "Open Dynamic Engine"
        Me.Physics_ODE.UseVisualStyleBackColor = True
        '
        'FormRegion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(809, 553)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.DeregisterButton)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.EnabledCheckBox)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.Advanced)
        Me.Controls.Add(Me.RegionName)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormRegion"
        Me.Text = "Regions"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Advanced.ResumeLayout(False)
        Me.Advanced.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.MapBox.ResumeLayout(False)
        Me.MapBox.PerformLayout()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Advanced As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents UUID As TextBox
    Friend WithEvents CoordY As TextBox
    Friend WithEvents CoordX As TextBox
    Friend WithEvents RegionName As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents SaveButton As Button
    Friend WithEvents DeleteButton As Button
    Friend WithEvents EnabledCheckBox As CheckBox
    Friend WithEvents MaxAgents As TextBox
    Friend WithEvents NonPhysPrimLabel As Label
    Friend WithEvents PhysPrimLabel As Label
    Friend WithEvents PhysicalPrimMax As TextBox
    Friend WithEvents MaxMAvatarsLabel As Label
    Friend WithEvents ClampPrimLabel As Label
    Friend WithEvents NonphysicalPrimMax As TextBox
    Friend WithEvents MaxNPrimsLabel As Label
    Friend WithEvents MaxPrims As TextBox
    Friend WithEvents ClampPrimSize As CheckBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents NoPublish As RadioButton
    Friend WithEvents PublishDefault As RadioButton
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Gods_Use_Default As CheckBox
    Friend WithEvents AllowGods As CheckBox
    Friend WithEvents ManagerGod As CheckBox
    Friend WithEvents RegionGod As CheckBox
    Friend WithEvents MapBox As GroupBox
    Friend WithEvents Maps_Use_Default As RadioButton
    Friend WithEvents MapPicture As PictureBox
    Friend WithEvents MapNone As RadioButton
    Friend WithEvents MapSimple As RadioButton
    Friend WithEvents MapBetter As RadioButton
    Friend WithEvents MapBest As RadioButton
    Friend WithEvents MapGood As RadioButton
    Friend WithEvents Publish As RadioButton
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents BirdsCheckBox As CheckBox
    Friend WithEvents TidesCheckbox As CheckBox
    Friend WithEvents TPCheckBox1 As CheckBox
    Friend WithEvents DeregisterButton As Button
    Friend WithEvents RegionPort As TextBox
    Friend WithEvents SmartStartCheckBox As CheckBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents ScriptRateLabel As Label
    Friend WithEvents ScriptTimerTextBox As TextBox
    Friend WithEvents DisableGBCheckBox As CheckBox
    Friend WithEvents DisallowForeigners As CheckBox
    Friend WithEvents DisallowResidents As CheckBox
    Friend WithEvents FrameRateLabel As Label
    Friend WithEvents FrametimeBox As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents SkipAutoCheckBox As CheckBox
    Friend WithEvents RadioButton8 As RadioButton
    Friend WithEvents RadioButton7 As RadioButton
    Friend WithEvents RadioButton6 As RadioButton
    Friend WithEvents RadioButton5 As RadioButton
    Friend WithEvents RadioButton16 As RadioButton
    Friend WithEvents RadioButton15 As RadioButton
    Friend WithEvents RadioButton14 As RadioButton
    Friend WithEvents RadioButton13 As RadioButton
    Friend WithEvents RadioButton12 As RadioButton
    Friend WithEvents RadioButton11 As RadioButton
    Friend WithEvents RadioButton10 As RadioButton
    Friend WithEvents RadioButton9 As RadioButton
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents ScriptDefaultButton As RadioButton
    Friend WithEvents XEngineButton As RadioButton
    Friend WithEvents YEngineButton As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Physics_Default As RadioButton
    Friend WithEvents Physics_Separate As RadioButton
    Friend WithEvents Physics_ubODE As RadioButton
    Friend WithEvents Physics_Hybrid As RadioButton
    Friend WithEvents Physics_Bullet As RadioButton
    Friend WithEvents ConciergeCheckBox As CheckBox
    Friend WithEvents Physics_ODE As RadioButton
End Class
