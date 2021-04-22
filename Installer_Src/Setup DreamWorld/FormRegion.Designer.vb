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
        Me.Advanced = New System.Windows.Forms.GroupBox()
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
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.ScriptDefaultButton = New System.Windows.Forms.RadioButton()
        Me.XEngineButton = New System.Windows.Forms.RadioButton()
        Me.YEngineButton = New System.Windows.Forms.RadioButton()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.ConciergeCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Hyperica = New System.Windows.Forms.LinkLabel()
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Basics = New System.Windows.Forms.TabPage()
        Me.Options = New System.Windows.Forms.TabPage()
        Me.Maps = New System.Windows.Forms.TabPage()
        Me.Physics = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Physics_ODE = New System.Windows.Forms.RadioButton()
        Me.Physics_Hybrid = New System.Windows.Forms.RadioButton()
        Me.Physics_Bullet = New System.Windows.Forms.RadioButton()
        Me.Physics_Default = New System.Windows.Forms.RadioButton()
        Me.Physics_Separate = New System.Windows.Forms.RadioButton()
        Me.Physics_ubODE = New System.Windows.Forms.RadioButton()
        Me.Scripts = New System.Windows.Forms.TabPage()
        Me.Permissions = New System.Windows.Forms.TabPage()
        Me.Publicity = New System.Windows.Forms.TabPage()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Opensimworld = New System.Windows.Forms.LinkLabel()
        Me.Modules = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox3 = New System.Windows.Forms.RichTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.RichTextBox4 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox5 = New System.Windows.Forms.RichTextBox()
        Me.Advanced.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.MapBox.SuspendLayout()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.Basics.SuspendLayout()
        Me.Options.SuspendLayout()
        Me.Maps.SuspendLayout()
        Me.Physics.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Scripts.SuspendLayout()
        Me.Permissions.SuspendLayout()
        Me.Publicity.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.Modules.SuspendLayout()
        Me.SuspendLayout()
        '
        'CoordY
        '
        Me.CoordY.Location = New System.Drawing.Point(199, 39)
        Me.CoordY.Margin = New System.Windows.Forms.Padding(4)
        Me.CoordY.Name = "CoordY"
        Me.CoordY.Size = New System.Drawing.Size(63, 26)
        Me.CoordY.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CoordY, Global.Outworldz.My.Resources.Resources.CoordY)
        '
        'CoordX
        '
        Me.CoordX.Location = New System.Drawing.Point(101, 39)
        Me.CoordX.Margin = New System.Windows.Forms.Padding(4)
        Me.CoordX.Name = "CoordX"
        Me.CoordX.Size = New System.Drawing.Size(58, 26)
        Me.CoordX.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CoordX, Global.Outworldz.My.Resources.Resources.Coordx)
        '
        'RegionName
        '
        Me.RegionName.Location = New System.Drawing.Point(142, 24)
        Me.RegionName.Margin = New System.Windows.Forms.Padding(4)
        Me.RegionName.Name = "RegionName"
        Me.RegionName.Size = New System.Drawing.Size(272, 26)
        Me.RegionName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.RegionName, Global.Outworldz.My.Resources.Resources.Region_Name)
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(30, 128)
        Me.RadioButton4.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton4.TabIndex = 6
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "4 X 4"
        Me.ToolTip1.SetToolTip(Me.RadioButton4, "1024 X 1024")
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(30, 94)
        Me.RadioButton3.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton3.TabIndex = 5
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "3 X 3"
        Me.ToolTip1.SetToolTip(Me.RadioButton3, "768 X 768")
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(30, 58)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton2.TabIndex = 4
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "2 X 2"
        Me.ToolTip1.SetToolTip(Me.RadioButton2, "512 X 512")
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(30, 24)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton1.TabIndex = 3
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "1 X 1"
        Me.ToolTip1.SetToolTip(Me.RadioButton1, "256 X 256")
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'MaxAgents
        '
        Me.MaxAgents.Location = New System.Drawing.Point(18, 181)
        Me.MaxAgents.Margin = New System.Windows.Forms.Padding(4)
        Me.MaxAgents.Name = "MaxAgents"
        Me.MaxAgents.Size = New System.Drawing.Size(58, 26)
        Me.MaxAgents.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.MaxAgents, Global.Outworldz.My.Resources.Resources.Max_Agents)
        '
        'NonPhysPrimLabel
        '
        Me.NonPhysPrimLabel.AutoSize = True
        Me.NonPhysPrimLabel.Location = New System.Drawing.Point(98, 85)
        Me.NonPhysPrimLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.NonPhysPrimLabel.Name = "NonPhysPrimLabel"
        Me.NonPhysPrimLabel.Size = New System.Drawing.Size(164, 20)
        Me.NonPhysPrimLabel.TabIndex = 9
        Me.NonPhysPrimLabel.Text = "Nonphysical Prim Size"
        Me.ToolTip1.SetToolTip(Me.NonPhysPrimLabel, Global.Outworldz.My.Resources.Resources.Max_NonPhys)
        '
        'PhysPrimLabel
        '
        Me.PhysPrimLabel.AutoSize = True
        Me.PhysPrimLabel.Location = New System.Drawing.Point(98, 121)
        Me.PhysPrimLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PhysPrimLabel.Name = "PhysPrimLabel"
        Me.PhysPrimLabel.Size = New System.Drawing.Size(169, 20)
        Me.PhysPrimLabel.TabIndex = 11
        Me.PhysPrimLabel.Text = "Physical Prim Max Size"
        Me.ToolTip1.SetToolTip(Me.PhysPrimLabel, Global.Outworldz.My.Resources.Resources.Max_Phys)
        '
        'PhysicalPrimMax
        '
        Me.PhysicalPrimMax.Location = New System.Drawing.Point(18, 115)
        Me.PhysicalPrimMax.Margin = New System.Windows.Forms.Padding(4)
        Me.PhysicalPrimMax.Name = "PhysicalPrimMax"
        Me.PhysicalPrimMax.Size = New System.Drawing.Size(58, 26)
        Me.PhysicalPrimMax.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.PhysicalPrimMax, Global.Outworldz.My.Resources.Resources.Max_Phys)
        '
        'ClampPrimLabel
        '
        Me.ClampPrimLabel.AutoSize = True
        Me.ClampPrimLabel.Location = New System.Drawing.Point(444, 123)
        Me.ClampPrimLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ClampPrimLabel.Name = "ClampPrimLabel"
        Me.ClampPrimLabel.Size = New System.Drawing.Size(124, 20)
        Me.ClampPrimLabel.TabIndex = 13
        Me.ClampPrimLabel.Text = "Clamp Prim Size"
        Me.ToolTip1.SetToolTip(Me.ClampPrimLabel, Global.Outworldz.My.Resources.Resources.ClampSize)
        '
        'MaxNPrimsLabel
        '
        Me.MaxNPrimsLabel.AutoSize = True
        Me.MaxNPrimsLabel.Location = New System.Drawing.Point(98, 153)
        Me.MaxNPrimsLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MaxNPrimsLabel.Name = "MaxNPrimsLabel"
        Me.MaxNPrimsLabel.Size = New System.Drawing.Size(236, 20)
        Me.MaxNPrimsLabel.TabIndex = 15
        Me.MaxNPrimsLabel.Text = "Max Number of Prims in a Parcel"
        Me.ToolTip1.SetToolTip(Me.MaxNPrimsLabel, Global.Outworldz.My.Resources.Resources.Viewer_Stops_Counting)
        '
        'MaxPrims
        '
        Me.MaxPrims.Location = New System.Drawing.Point(18, 147)
        Me.MaxPrims.Margin = New System.Windows.Forms.Padding(4)
        Me.MaxPrims.Name = "MaxPrims"
        Me.MaxPrims.Size = New System.Drawing.Size(58, 26)
        Me.MaxPrims.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.MaxPrims, Global.Outworldz.My.Resources.Resources.Not_Normal)
        '
        'NonphysicalPrimMax
        '
        Me.NonphysicalPrimMax.Location = New System.Drawing.Point(18, 79)
        Me.NonphysicalPrimMax.Margin = New System.Windows.Forms.Padding(4)
        Me.NonphysicalPrimMax.Name = "NonphysicalPrimMax"
        Me.NonphysicalPrimMax.Size = New System.Drawing.Size(58, 26)
        Me.NonphysicalPrimMax.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.NonphysicalPrimMax, Global.Outworldz.My.Resources.Resources.Normal_Prim)
        '
        'MaxMAvatarsLabel
        '
        Me.MaxMAvatarsLabel.AutoSize = True
        Me.MaxMAvatarsLabel.Location = New System.Drawing.Point(98, 187)
        Me.MaxMAvatarsLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MaxMAvatarsLabel.Name = "MaxMAvatarsLabel"
        Me.MaxMAvatarsLabel.Size = New System.Drawing.Size(229, 20)
        Me.MaxMAvatarsLabel.TabIndex = 17
        Me.MaxMAvatarsLabel.Text = "Max number of Avatars + NPCs"
        Me.ToolTip1.SetToolTip(Me.MaxMAvatarsLabel, Global.Outworldz.My.Resources.Resources.Max_Agents)
        '
        'ClampPrimSize
        '
        Me.ClampPrimSize.AutoSize = True
        Me.ClampPrimSize.Location = New System.Drawing.Point(358, 121)
        Me.ClampPrimSize.Margin = New System.Windows.Forms.Padding(4)
        Me.ClampPrimSize.Name = "ClampPrimSize"
        Me.ClampPrimSize.Size = New System.Drawing.Size(22, 21)
        Me.ClampPrimSize.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.ClampPrimSize, Global.Outworldz.My.Resources.Resources.ClampSize)
        Me.ClampPrimSize.UseVisualStyleBackColor = True
        '
        'BirdsCheckBox
        '
        Me.BirdsCheckBox.AutoSize = True
        Me.BirdsCheckBox.Location = New System.Drawing.Point(22, 28)
        Me.BirdsCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.BirdsCheckBox.Name = "BirdsCheckBox"
        Me.BirdsCheckBox.Size = New System.Drawing.Size(119, 24)
        Me.BirdsCheckBox.TabIndex = 0
        Me.BirdsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Bird_Module_word
        Me.ToolTip1.SetToolTip(Me.BirdsCheckBox, Global.Outworldz.My.Resources.Resources.GBoids)
        Me.BirdsCheckBox.UseVisualStyleBackColor = True
        '
        'TidesCheckbox
        '
        Me.TidesCheckbox.AutoSize = True
        Me.TidesCheckbox.Location = New System.Drawing.Point(22, 62)
        Me.TidesCheckbox.Margin = New System.Windows.Forms.Padding(4)
        Me.TidesCheckbox.Name = "TidesCheckbox"
        Me.TidesCheckbox.Size = New System.Drawing.Size(286, 24)
        Me.TidesCheckbox.TabIndex = 1
        Me.TidesCheckbox.Text = Global.Outworldz.My.Resources.Resources.Tide_Enable
        Me.ToolTip1.SetToolTip(Me.TidesCheckbox, Global.Outworldz.My.Resources.Resources.GTide)
        Me.TidesCheckbox.UseVisualStyleBackColor = True
        '
        'TPCheckBox1
        '
        Me.TPCheckBox1.AutoSize = True
        Me.TPCheckBox1.Location = New System.Drawing.Point(22, 98)
        Me.TPCheckBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TPCheckBox1.Name = "TPCheckBox1"
        Me.TPCheckBox1.Size = New System.Drawing.Size(183, 24)
        Me.TPCheckBox1.TabIndex = 2
        Me.TPCheckBox1.Text = Global.Outworldz.My.Resources.Resources.Teleporter_Enable_word
        Me.ToolTip1.SetToolTip(Me.TPCheckBox1, Global.Outworldz.My.Resources.Resources.Teleport_Tooltip)
        Me.TPCheckBox1.UseVisualStyleBackColor = True
        '
        'AllowGods
        '
        Me.AllowGods.AutoSize = True
        Me.AllowGods.Location = New System.Drawing.Point(22, 78)
        Me.AllowGods.Margin = New System.Windows.Forms.Padding(4)
        Me.AllowGods.Name = "AllowGods"
        Me.AllowGods.Size = New System.Drawing.Size(201, 24)
        Me.AllowGods.TabIndex = 1
        Me.AllowGods.Text = Global.Outworldz.My.Resources.Resources.Allow_Or_Disallow_Gods_word
        Me.ToolTip1.SetToolTip(Me.AllowGods, Global.Outworldz.My.Resources.Resources.AllowGodsTooltip)
        Me.AllowGods.UseVisualStyleBackColor = True
        '
        'ManagerGod
        '
        Me.ManagerGod.AutoSize = True
        Me.ManagerGod.Location = New System.Drawing.Point(22, 148)
        Me.ManagerGod.Margin = New System.Windows.Forms.Padding(4)
        Me.ManagerGod.Name = "ManagerGod"
        Me.ManagerGod.Size = New System.Drawing.Size(195, 24)
        Me.ManagerGod.TabIndex = 3
        Me.ManagerGod.Text = Global.Outworldz.My.Resources.Resources.EstateManagerIsGod_word
        Me.ToolTip1.SetToolTip(Me.ManagerGod, Global.Outworldz.My.Resources.Resources.EMGod)
        Me.ManagerGod.UseVisualStyleBackColor = True
        '
        'RegionGod
        '
        Me.RegionGod.AutoSize = True
        Me.RegionGod.Location = New System.Drawing.Point(22, 112)
        Me.RegionGod.Margin = New System.Windows.Forms.Padding(4)
        Me.RegionGod.Name = "RegionGod"
        Me.RegionGod.Size = New System.Drawing.Size(186, 24)
        Me.RegionGod.TabIndex = 2
        Me.RegionGod.Text = Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word
        Me.ToolTip1.SetToolTip(Me.RegionGod, Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word)
        Me.RegionGod.UseVisualStyleBackColor = True
        '
        'SmartStartCheckBox
        '
        Me.SmartStartCheckBox.AutoSize = True
        Me.SmartStartCheckBox.Location = New System.Drawing.Point(563, 30)
        Me.SmartStartCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.SmartStartCheckBox.Name = "SmartStartCheckBox"
        Me.SmartStartCheckBox.Size = New System.Drawing.Size(117, 24)
        Me.SmartStartCheckBox.TabIndex = 2
        Me.SmartStartCheckBox.Text = Global.Outworldz.My.Resources.Resources.Smart_Start_word
        Me.ToolTip1.SetToolTip(Me.SmartStartCheckBox, Global.Outworldz.My.Resources.Resources.GTide)
        Me.SmartStartCheckBox.UseVisualStyleBackColor = True
        '
        'ScriptRateLabel
        '
        Me.ScriptRateLabel.AutoSize = True
        Me.ScriptRateLabel.Location = New System.Drawing.Point(444, 39)
        Me.ScriptRateLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ScriptRateLabel.Name = "ScriptRateLabel"
        Me.ScriptRateLabel.Size = New System.Drawing.Size(168, 20)
        Me.ScriptRateLabel.TabIndex = 19
        Me.ScriptRateLabel.Text = "Script Timer Rate (0.2)"
        Me.ToolTip1.SetToolTip(Me.ScriptRateLabel, Global.Outworldz.My.Resources.Resources.Script_Timer_Text)
        '
        'ScriptTimerTextBox
        '
        Me.ScriptTimerTextBox.Location = New System.Drawing.Point(358, 39)
        Me.ScriptTimerTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ScriptTimerTextBox.Name = "ScriptTimerTextBox"
        Me.ScriptTimerTextBox.Size = New System.Drawing.Size(58, 26)
        Me.ScriptTimerTextBox.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.ScriptTimerTextBox, Global.Outworldz.My.Resources.Resources.STComment)
        '
        'DisableGBCheckBox
        '
        Me.DisableGBCheckBox.AutoSize = True
        Me.DisableGBCheckBox.Location = New System.Drawing.Point(22, 128)
        Me.DisableGBCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.DisableGBCheckBox.Name = "DisableGBCheckBox"
        Me.DisableGBCheckBox.Size = New System.Drawing.Size(172, 24)
        Me.DisableGBCheckBox.TabIndex = 3
        Me.DisableGBCheckBox.Text = Global.Outworldz.My.Resources.Resources.Disable_Gloebits_word
        Me.ToolTip1.SetToolTip(Me.DisableGBCheckBox, Global.Outworldz.My.Resources.Resources.Disable_Gloebits_text)
        Me.DisableGBCheckBox.UseVisualStyleBackColor = True
        '
        'DisallowForeigners
        '
        Me.DisallowForeigners.AutoSize = True
        Me.DisallowForeigners.Location = New System.Drawing.Point(22, 160)
        Me.DisallowForeigners.Margin = New System.Windows.Forms.Padding(4)
        Me.DisallowForeigners.Name = "DisallowForeigners"
        Me.DisallowForeigners.Size = New System.Drawing.Size(202, 24)
        Me.DisallowForeigners.TabIndex = 4
        Me.DisallowForeigners.Text = Global.Outworldz.My.Resources.Resources.Disable_Foreigners_word
        Me.ToolTip1.SetToolTip(Me.DisallowForeigners, Global.Outworldz.My.Resources.Resources.No_HG)
        Me.DisallowForeigners.UseVisualStyleBackColor = True
        '
        'DisallowResidents
        '
        Me.DisallowResidents.AutoSize = True
        Me.DisallowResidents.Location = New System.Drawing.Point(22, 192)
        Me.DisallowResidents.Margin = New System.Windows.Forms.Padding(4)
        Me.DisallowResidents.Name = "DisallowResidents"
        Me.DisallowResidents.Size = New System.Drawing.Size(189, 24)
        Me.DisallowResidents.TabIndex = 5
        Me.DisallowResidents.Text = Global.Outworldz.My.Resources.Resources.Disable_Residents
        Me.ToolTip1.SetToolTip(Me.DisallowResidents, Global.Outworldz.My.Resources.Resources.Only_Owners)
        Me.DisallowResidents.UseVisualStyleBackColor = True
        '
        'FrametimeBox
        '
        Me.FrametimeBox.Location = New System.Drawing.Point(358, 81)
        Me.FrametimeBox.Margin = New System.Windows.Forms.Padding(4)
        Me.FrametimeBox.Name = "FrametimeBox"
        Me.FrametimeBox.Size = New System.Drawing.Size(58, 26)
        Me.FrametimeBox.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.FrametimeBox, Global.Outworldz.My.Resources.Resources.FrameTime)
        '
        'SkipAutoCheckBox
        '
        Me.SkipAutoCheckBox.AutoSize = True
        Me.SkipAutoCheckBox.Location = New System.Drawing.Point(22, 226)
        Me.SkipAutoCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.SkipAutoCheckBox.Name = "SkipAutoCheckBox"
        Me.SkipAutoCheckBox.Size = New System.Drawing.Size(237, 24)
        Me.SkipAutoCheckBox.TabIndex = 6
        Me.SkipAutoCheckBox.Text = Global.Outworldz.My.Resources.Resources.Skip_Autobackup_word
        Me.ToolTip1.SetToolTip(Me.SkipAutoCheckBox, Global.Outworldz.My.Resources.Resources.WillNotSave)
        Me.SkipAutoCheckBox.UseVisualStyleBackColor = True
        '
        'FrameRateLabel
        '
        Me.FrameRateLabel.AutoSize = True
        Me.FrameRateLabel.Location = New System.Drawing.Point(444, 81)
        Me.FrameRateLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.FrameRateLabel.Name = "FrameRateLabel"
        Me.FrameRateLabel.Size = New System.Drawing.Size(157, 20)
        Me.FrameRateLabel.TabIndex = 21
        Me.FrameRateLabel.Text = "Frame Rate (0.0909)"
        Me.ToolTip1.SetToolTip(Me.FrameRateLabel, Global.Outworldz.My.Resources.Resources.FRText)
        '
        'RadioButton8
        '
        Me.RadioButton8.AutoSize = True
        Me.RadioButton8.Location = New System.Drawing.Point(134, 124)
        Me.RadioButton8.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton8.Name = "RadioButton8"
        Me.RadioButton8.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton8.TabIndex = 10
        Me.RadioButton8.TabStop = True
        Me.RadioButton8.Text = "8 X 8"
        Me.ToolTip1.SetToolTip(Me.RadioButton8, "2,048 X 2,048")
        Me.RadioButton8.UseVisualStyleBackColor = True
        '
        'RadioButton7
        '
        Me.RadioButton7.AutoSize = True
        Me.RadioButton7.Location = New System.Drawing.Point(134, 90)
        Me.RadioButton7.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton7.Name = "RadioButton7"
        Me.RadioButton7.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton7.TabIndex = 9
        Me.RadioButton7.TabStop = True
        Me.RadioButton7.Text = "7 X 7"
        Me.ToolTip1.SetToolTip(Me.RadioButton7, "1792 X 1792")
        Me.RadioButton7.UseVisualStyleBackColor = True
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Location = New System.Drawing.Point(134, 56)
        Me.RadioButton6.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton6.TabIndex = 8
        Me.RadioButton6.TabStop = True
        Me.RadioButton6.Text = "6 X 6"
        Me.ToolTip1.SetToolTip(Me.RadioButton6, "1536 X 1536")
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(134, 22)
        Me.RadioButton5.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton5.TabIndex = 7
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "5 X 5"
        Me.ToolTip1.SetToolTip(Me.RadioButton5, "1280 X 1280")
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton10
        '
        Me.RadioButton10.AutoSize = True
        Me.RadioButton10.Location = New System.Drawing.Point(230, 56)
        Me.RadioButton10.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton10.Name = "RadioButton10"
        Me.RadioButton10.Size = New System.Drawing.Size(89, 24)
        Me.RadioButton10.TabIndex = 12
        Me.RadioButton10.TabStop = True
        Me.RadioButton10.Text = "10 X 10"
        Me.ToolTip1.SetToolTip(Me.RadioButton10, "2560 X 2560")
        Me.RadioButton10.UseVisualStyleBackColor = True
        '
        'RadioButton9
        '
        Me.RadioButton9.AutoSize = True
        Me.RadioButton9.Location = New System.Drawing.Point(230, 22)
        Me.RadioButton9.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton9.Name = "RadioButton9"
        Me.RadioButton9.Size = New System.Drawing.Size(71, 24)
        Me.RadioButton9.TabIndex = 11
        Me.RadioButton9.TabStop = True
        Me.RadioButton9.Text = "9 X 9"
        Me.ToolTip1.SetToolTip(Me.RadioButton9, "2304 X 2304")
        Me.RadioButton9.UseVisualStyleBackColor = True
        '
        'RadioButton12
        '
        Me.RadioButton12.AutoSize = True
        Me.RadioButton12.Location = New System.Drawing.Point(232, 124)
        Me.RadioButton12.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton12.Name = "RadioButton12"
        Me.RadioButton12.Size = New System.Drawing.Size(89, 24)
        Me.RadioButton12.TabIndex = 14
        Me.RadioButton12.TabStop = True
        Me.RadioButton12.Text = "12 X 12"
        Me.ToolTip1.SetToolTip(Me.RadioButton12, "3072 X 3072")
        Me.RadioButton12.UseVisualStyleBackColor = True
        '
        'RadioButton11
        '
        Me.RadioButton11.AutoSize = True
        Me.RadioButton11.Location = New System.Drawing.Point(232, 90)
        Me.RadioButton11.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton11.Name = "RadioButton11"
        Me.RadioButton11.Size = New System.Drawing.Size(89, 24)
        Me.RadioButton11.TabIndex = 13
        Me.RadioButton11.TabStop = True
        Me.RadioButton11.Text = "11 X 11"
        Me.ToolTip1.SetToolTip(Me.RadioButton11, "2816 X 2816")
        Me.RadioButton11.UseVisualStyleBackColor = True
        '
        'RadioButton16
        '
        Me.RadioButton16.AutoSize = True
        Me.RadioButton16.Location = New System.Drawing.Point(336, 124)
        Me.RadioButton16.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton16.Name = "RadioButton16"
        Me.RadioButton16.Size = New System.Drawing.Size(89, 24)
        Me.RadioButton16.TabIndex = 18
        Me.RadioButton16.TabStop = True
        Me.RadioButton16.Text = "16 X 16"
        Me.ToolTip1.SetToolTip(Me.RadioButton16, "4096 X 4096")
        Me.RadioButton16.UseVisualStyleBackColor = True
        '
        'RadioButton15
        '
        Me.RadioButton15.AutoSize = True
        Me.RadioButton15.Location = New System.Drawing.Point(336, 90)
        Me.RadioButton15.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton15.Name = "RadioButton15"
        Me.RadioButton15.Size = New System.Drawing.Size(89, 24)
        Me.RadioButton15.TabIndex = 17
        Me.RadioButton15.TabStop = True
        Me.RadioButton15.Text = "15 X 15"
        Me.ToolTip1.SetToolTip(Me.RadioButton15, "3840 X 3840")
        Me.RadioButton15.UseVisualStyleBackColor = True
        '
        'RadioButton14
        '
        Me.RadioButton14.AutoSize = True
        Me.RadioButton14.Location = New System.Drawing.Point(334, 56)
        Me.RadioButton14.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton14.Name = "RadioButton14"
        Me.RadioButton14.Size = New System.Drawing.Size(89, 24)
        Me.RadioButton14.TabIndex = 16
        Me.RadioButton14.TabStop = True
        Me.RadioButton14.Text = "14 X 14"
        Me.ToolTip1.SetToolTip(Me.RadioButton14, "3584 X 3584")
        Me.RadioButton14.UseVisualStyleBackColor = True
        '
        'RadioButton13
        '
        Me.RadioButton13.AutoSize = True
        Me.RadioButton13.Location = New System.Drawing.Point(334, 22)
        Me.RadioButton13.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton13.Name = "RadioButton13"
        Me.RadioButton13.Size = New System.Drawing.Size(89, 24)
        Me.RadioButton13.TabIndex = 15
        Me.RadioButton13.TabStop = True
        Me.RadioButton13.Text = "13 X 13"
        Me.ToolTip1.SetToolTip(Me.RadioButton13, "3328 X 3328")
        Me.RadioButton13.UseVisualStyleBackColor = True
        '
        'Advanced
        '
        Me.Advanced.BackColor = System.Drawing.SystemColors.Window
        Me.Advanced.Controls.Add(Me.Label7)
        Me.Advanced.Controls.Add(Me.FrameRateLabel)
        Me.Advanced.Controls.Add(Me.FrametimeBox)
        Me.Advanced.Controls.Add(Me.ScriptRateLabel)
        Me.Advanced.Controls.Add(Me.ScriptTimerTextBox)
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
        Me.Advanced.Location = New System.Drawing.Point(26, 17)
        Me.Advanced.Margin = New System.Windows.Forms.Padding(4)
        Me.Advanced.Name = "Advanced"
        Me.Advanced.Padding = New System.Windows.Forms.Padding(4)
        Me.Advanced.Size = New System.Drawing.Size(684, 296)
        Me.Advanced.TabIndex = 26
        Me.Advanced.TabStop = False
        Me.Advanced.Text = "Regions"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(291, 221)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 20)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "UUID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 42)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Map  X:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(229, 45)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 20)
        Me.Label1.TabIndex = 3
        '
        'UUID
        '
        Me.UUID.Enabled = False
        Me.UUID.Location = New System.Drawing.Point(18, 215)
        Me.UUID.Margin = New System.Windows.Forms.Padding(4)
        Me.UUID.Name = "UUID"
        Me.UUID.Size = New System.Drawing.Size(265, 26)
        Me.UUID.TabIndex = 7
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
        Me.GroupBox2.Location = New System.Drawing.Point(200, 87)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(446, 170)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sim Size"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(222, 70)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 20)
        Me.Label3.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(222, 38)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 20)
        Me.Label2.TabIndex = 2
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(48, 115)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(4)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(112, 34)
        Me.SaveButton.TabIndex = 5
        Me.SaveButton.Text = Global.Outworldz.My.Resources.Resources.Save_word
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        Me.DeleteButton.Location = New System.Drawing.Point(50, 201)
        Me.DeleteButton.Margin = New System.Windows.Forms.Padding(4)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(110, 34)
        Me.DeleteButton.TabIndex = 7
        Me.DeleteButton.Text = Global.Outworldz.My.Resources.Resources.Delete_word
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'EnabledCheckBox
        '
        Me.EnabledCheckBox.AutoSize = True
        Me.EnabledCheckBox.Location = New System.Drawing.Point(443, 30)
        Me.EnabledCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.EnabledCheckBox.Name = "EnabledCheckBox"
        Me.EnabledCheckBox.Size = New System.Drawing.Size(94, 24)
        Me.EnabledCheckBox.TabIndex = 1
        Me.EnabledCheckBox.Text = Global.Outworldz.My.Resources.Resources.Enabled_word
        Me.EnabledCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.ScriptDefaultButton)
        Me.GroupBox8.Controls.Add(Me.XEngineButton)
        Me.GroupBox8.Controls.Add(Me.YEngineButton)
        Me.GroupBox8.Location = New System.Drawing.Point(15, 28)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox8.Size = New System.Drawing.Size(211, 176)
        Me.GroupBox8.TabIndex = 4
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Script Engine"
        '
        'ScriptDefaultButton
        '
        Me.ScriptDefaultButton.AutoSize = True
        Me.ScriptDefaultButton.Location = New System.Drawing.Point(22, 38)
        Me.ScriptDefaultButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ScriptDefaultButton.Name = "ScriptDefaultButton"
        Me.ScriptDefaultButton.Size = New System.Drawing.Size(119, 24)
        Me.ScriptDefaultButton.TabIndex = 0
        Me.ScriptDefaultButton.TabStop = True
        Me.ScriptDefaultButton.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.ScriptDefaultButton.UseVisualStyleBackColor = True
        '
        'XEngineButton
        '
        Me.XEngineButton.AutoSize = True
        Me.XEngineButton.Location = New System.Drawing.Point(20, 70)
        Me.XEngineButton.Margin = New System.Windows.Forms.Padding(4)
        Me.XEngineButton.Name = "XEngineButton"
        Me.XEngineButton.Size = New System.Drawing.Size(99, 24)
        Me.XEngineButton.TabIndex = 1
        Me.XEngineButton.TabStop = True
        Me.XEngineButton.Text = Global.Outworldz.My.Resources.Resources.XEngine_word
        Me.XEngineButton.UseVisualStyleBackColor = True
        '
        'YEngineButton
        '
        Me.YEngineButton.AutoSize = True
        Me.YEngineButton.Location = New System.Drawing.Point(18, 100)
        Me.YEngineButton.Margin = New System.Windows.Forms.Padding(4)
        Me.YEngineButton.Name = "YEngineButton"
        Me.YEngineButton.Size = New System.Drawing.Size(99, 24)
        Me.YEngineButton.TabIndex = 2
        Me.YEngineButton.TabStop = True
        Me.YEngineButton.Text = Global.Outworldz.My.Resources.Resources.YEngine_word
        Me.YEngineButton.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.RichTextBox3)
        Me.GroupBox7.Controls.Add(Me.ConciergeCheckBox)
        Me.GroupBox7.Controls.Add(Me.SkipAutoCheckBox)
        Me.GroupBox7.Controls.Add(Me.DisallowResidents)
        Me.GroupBox7.Controls.Add(Me.DisallowForeigners)
        Me.GroupBox7.Controls.Add(Me.DisableGBCheckBox)
        Me.GroupBox7.Controls.Add(Me.TPCheckBox1)
        Me.GroupBox7.Controls.Add(Me.TidesCheckbox)
        Me.GroupBox7.Controls.Add(Me.BirdsCheckBox)
        Me.GroupBox7.Location = New System.Drawing.Point(31, 16)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Size = New System.Drawing.Size(646, 296)
        Me.GroupBox7.TabIndex = 0
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Modules"
        '
        'ConciergeCheckBox
        '
        Me.ConciergeCheckBox.AutoSize = True
        Me.ConciergeCheckBox.Location = New System.Drawing.Point(22, 258)
        Me.ConciergeCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ConciergeCheckBox.Name = "ConciergeCheckBox"
        Me.ConciergeCheckBox.Size = New System.Drawing.Size(164, 24)
        Me.ConciergeCheckBox.TabIndex = 7
        Me.ConciergeCheckBox.Text = "Announce Visitors"
        Me.ConciergeCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Hyperica)
        Me.GroupBox3.Controls.Add(Me.Publish)
        Me.GroupBox3.Controls.Add(Me.NoPublish)
        Me.GroupBox3.Controls.Add(Me.PublishDefault)
        Me.GroupBox3.Location = New System.Drawing.Point(17, 37)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(346, 193)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Publicity"
        '
        'Hyperica
        '
        Me.Hyperica.AutoSize = True
        Me.Hyperica.Location = New System.Drawing.Point(70, 151)
        Me.Hyperica.Name = "Hyperica"
        Me.Hyperica.Size = New System.Drawing.Size(150, 20)
        Me.Hyperica.TabIndex = 1
        Me.Hyperica.TabStop = True
        Me.Hyperica.Text = "https://hyperica.com"
        '
        'Publish
        '
        Me.Publish.AutoSize = True
        Me.Publish.Location = New System.Drawing.Point(16, 110)
        Me.Publish.Margin = New System.Windows.Forms.Padding(4)
        Me.Publish.Name = "Publish"
        Me.Publish.Size = New System.Drawing.Size(261, 24)
        Me.Publish.TabIndex = 2
        Me.Publish.TabStop = True
        Me.Publish.Text = Global.Outworldz.My.Resources.Resources.Publish_Items
        Me.Publish.UseVisualStyleBackColor = True
        '
        'NoPublish
        '
        Me.NoPublish.AutoSize = True
        Me.NoPublish.Location = New System.Drawing.Point(16, 76)
        Me.NoPublish.Margin = New System.Windows.Forms.Padding(4)
        Me.NoPublish.Name = "NoPublish"
        Me.NoPublish.Size = New System.Drawing.Size(213, 24)
        Me.NoPublish.TabIndex = 1
        Me.NoPublish.TabStop = True
        Me.NoPublish.Text = Global.Outworldz.My.Resources.Resources.No_Publish_Items
        Me.NoPublish.UseVisualStyleBackColor = True
        '
        'PublishDefault
        '
        Me.PublishDefault.AutoSize = True
        Me.PublishDefault.Location = New System.Drawing.Point(16, 40)
        Me.PublishDefault.Margin = New System.Windows.Forms.Padding(4)
        Me.PublishDefault.Name = "PublishDefault"
        Me.PublishDefault.Size = New System.Drawing.Size(119, 24)
        Me.PublishDefault.TabIndex = 0
        Me.PublishDefault.TabStop = True
        Me.PublishDefault.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.PublishDefault.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.RichTextBox2)
        Me.GroupBox4.Controls.Add(Me.Gods_Use_Default)
        Me.GroupBox4.Controls.Add(Me.AllowGods)
        Me.GroupBox4.Controls.Add(Me.ManagerGod)
        Me.GroupBox4.Controls.Add(Me.RegionGod)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 13)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(702, 291)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Permissions"
        '
        'Gods_Use_Default
        '
        Me.Gods_Use_Default.AutoSize = True
        Me.Gods_Use_Default.Location = New System.Drawing.Point(22, 40)
        Me.Gods_Use_Default.Margin = New System.Windows.Forms.Padding(4)
        Me.Gods_Use_Default.Name = "Gods_Use_Default"
        Me.Gods_Use_Default.Size = New System.Drawing.Size(120, 24)
        Me.Gods_Use_Default.TabIndex = 0
        Me.Gods_Use_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.Gods_Use_Default.UseVisualStyleBackColor = True
        '
        'MapBox
        '
        Me.MapBox.Controls.Add(Me.RichTextBox5)
        Me.MapBox.Controls.Add(Me.Maps_Use_Default)
        Me.MapBox.Controls.Add(Me.MapPicture)
        Me.MapBox.Controls.Add(Me.MapNone)
        Me.MapBox.Controls.Add(Me.MapSimple)
        Me.MapBox.Controls.Add(Me.MapBetter)
        Me.MapBox.Controls.Add(Me.MapBest)
        Me.MapBox.Controls.Add(Me.MapGood)
        Me.MapBox.Location = New System.Drawing.Point(34, 11)
        Me.MapBox.Margin = New System.Windows.Forms.Padding(4)
        Me.MapBox.Name = "MapBox"
        Me.MapBox.Padding = New System.Windows.Forms.Padding(4)
        Me.MapBox.Size = New System.Drawing.Size(665, 351)
        Me.MapBox.TabIndex = 5
        Me.MapBox.TabStop = False
        Me.MapBox.Text = "Maps"
        '
        'Maps_Use_Default
        '
        Me.Maps_Use_Default.AutoSize = True
        Me.Maps_Use_Default.Checked = True
        Me.Maps_Use_Default.Location = New System.Drawing.Point(22, 34)
        Me.Maps_Use_Default.Margin = New System.Windows.Forms.Padding(4)
        Me.Maps_Use_Default.Name = "Maps_Use_Default"
        Me.Maps_Use_Default.Size = New System.Drawing.Size(119, 24)
        Me.Maps_Use_Default.TabIndex = 0
        Me.Maps_Use_Default.TabStop = True
        Me.Maps_Use_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.Maps_Use_Default.UseVisualStyleBackColor = True
        '
        'MapPicture
        '
        Me.MapPicture.InitialImage = CType(resources.GetObject("MapPicture.InitialImage"), System.Drawing.Image)
        Me.MapPicture.Location = New System.Drawing.Point(232, 40)
        Me.MapPicture.Margin = New System.Windows.Forms.Padding(4)
        Me.MapPicture.Name = "MapPicture"
        Me.MapPicture.Size = New System.Drawing.Size(105, 104)
        Me.MapPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.MapPicture.TabIndex = 138
        Me.MapPicture.TabStop = False
        '
        'MapNone
        '
        Me.MapNone.AutoSize = True
        Me.MapNone.Location = New System.Drawing.Point(20, 62)
        Me.MapNone.Margin = New System.Windows.Forms.Padding(4)
        Me.MapNone.Name = "MapNone"
        Me.MapNone.Size = New System.Drawing.Size(72, 24)
        Me.MapNone.TabIndex = 1
        Me.MapNone.Text = Global.Outworldz.My.Resources.Resources.None
        Me.MapNone.UseVisualStyleBackColor = True
        '
        'MapSimple
        '
        Me.MapSimple.AutoSize = True
        Me.MapSimple.Location = New System.Drawing.Point(20, 92)
        Me.MapSimple.Margin = New System.Windows.Forms.Padding(4)
        Me.MapSimple.Name = "MapSimple"
        Me.MapSimple.Size = New System.Drawing.Size(140, 24)
        Me.MapSimple.TabIndex = 2
        Me.MapSimple.Text = Global.Outworldz.My.Resources.Resources.Simple_but_Fast_word
        Me.MapSimple.UseVisualStyleBackColor = True
        '
        'MapBetter
        '
        Me.MapBetter.AutoSize = True
        Me.MapBetter.Location = New System.Drawing.Point(22, 150)
        Me.MapBetter.Margin = New System.Windows.Forms.Padding(4)
        Me.MapBetter.Name = "MapBetter"
        Me.MapBetter.Size = New System.Drawing.Size(173, 24)
        Me.MapBetter.TabIndex = 4
        Me.MapBetter.Text = Global.Outworldz.My.Resources.Resources.Better_Prims
        Me.MapBetter.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.MapBetter.UseVisualStyleBackColor = True
        '
        'MapBest
        '
        Me.MapBest.AutoSize = True
        Me.MapBest.Location = New System.Drawing.Point(20, 182)
        Me.MapBest.Margin = New System.Windows.Forms.Padding(4)
        Me.MapBest.Name = "MapBest"
        Me.MapBest.Size = New System.Drawing.Size(254, 24)
        Me.MapBest.TabIndex = 5
        Me.MapBest.Text = Global.Outworldz.My.Resources.Resources.Best_Prims
        Me.MapBest.UseVisualStyleBackColor = True
        '
        'MapGood
        '
        Me.MapGood.AutoSize = True
        Me.MapGood.Location = New System.Drawing.Point(22, 120)
        Me.MapGood.Margin = New System.Windows.Forms.Padding(4)
        Me.MapGood.Name = "MapGood"
        Me.MapGood.Size = New System.Drawing.Size(147, 24)
        Me.MapGood.TabIndex = 3
        Me.MapGood.Text = Global.Outworldz.My.Resources.Resources.Good_Warp3D_word
        Me.MapGood.UseVisualStyleBackColor = True
        '
        'DeregisterButton
        '
        Me.DeregisterButton.Location = New System.Drawing.Point(48, 159)
        Me.DeregisterButton.Margin = New System.Windows.Forms.Padding(4)
        Me.DeregisterButton.Name = "DeregisterButton"
        Me.DeregisterButton.Size = New System.Drawing.Size(112, 34)
        Me.DeregisterButton.TabIndex = 6
        Me.DeregisterButton.Text = Global.Outworldz.My.Resources.Resources.Deregister_word
        Me.DeregisterButton.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(780, 33)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(89, 29)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Basics)
        Me.TabControl1.Controls.Add(Me.Options)
        Me.TabControl1.Controls.Add(Me.Maps)
        Me.TabControl1.Controls.Add(Me.Physics)
        Me.TabControl1.Controls.Add(Me.Scripts)
        Me.TabControl1.Controls.Add(Me.Permissions)
        Me.TabControl1.Controls.Add(Me.Publicity)
        Me.TabControl1.Controls.Add(Me.Modules)
        Me.TabControl1.Location = New System.Drawing.Point(12, 36)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(753, 362)
        Me.TabControl1.TabIndex = 27
        '
        'Basics
        '
        Me.Basics.Controls.Add(Me.Label8)
        Me.Basics.Controls.Add(Me.EnabledCheckBox)
        Me.Basics.Controls.Add(Me.DeregisterButton)
        Me.Basics.Controls.Add(Me.GroupBox2)
        Me.Basics.Controls.Add(Me.DeleteButton)
        Me.Basics.Controls.Add(Me.RegionName)
        Me.Basics.Controls.Add(Me.SmartStartCheckBox)
        Me.Basics.Controls.Add(Me.SaveButton)
        Me.Basics.Location = New System.Drawing.Point(4, 29)
        Me.Basics.Name = "Basics"
        Me.Basics.Padding = New System.Windows.Forms.Padding(3)
        Me.Basics.Size = New System.Drawing.Size(745, 329)
        Me.Basics.TabIndex = 0
        Me.Basics.Text = "Region Basics"
        Me.Basics.UseVisualStyleBackColor = True
        '
        'Options
        '
        Me.Options.Controls.Add(Me.Advanced)
        Me.Options.Location = New System.Drawing.Point(4, 29)
        Me.Options.Name = "Options"
        Me.Options.Padding = New System.Windows.Forms.Padding(3)
        Me.Options.Size = New System.Drawing.Size(745, 329)
        Me.Options.TabIndex = 1
        Me.Options.Text = "Optional"
        Me.Options.UseVisualStyleBackColor = True
        '
        'Maps
        '
        Me.Maps.Controls.Add(Me.MapBox)
        Me.Maps.Location = New System.Drawing.Point(4, 29)
        Me.Maps.Name = "Maps"
        Me.Maps.Size = New System.Drawing.Size(745, 329)
        Me.Maps.TabIndex = 2
        Me.Maps.Text = "Maps"
        Me.Maps.UseVisualStyleBackColor = True
        '
        'Physics
        '
        Me.Physics.Controls.Add(Me.RichTextBox4)
        Me.Physics.Controls.Add(Me.GroupBox1)
        Me.Physics.Location = New System.Drawing.Point(4, 29)
        Me.Physics.Name = "Physics"
        Me.Physics.Size = New System.Drawing.Size(745, 329)
        Me.Physics.TabIndex = 8
        Me.Physics.Text = "Physics"
        Me.Physics.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Physics_ODE)
        Me.GroupBox1.Controls.Add(Me.Physics_Hybrid)
        Me.GroupBox1.Controls.Add(Me.Physics_Bullet)
        Me.GroupBox1.Controls.Add(Me.Physics_Default)
        Me.GroupBox1.Controls.Add(Me.Physics_Separate)
        Me.GroupBox1.Controls.Add(Me.Physics_ubODE)
        Me.GroupBox1.Location = New System.Drawing.Point(28, 30)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(342, 246)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Physics"
        '
        'Physics_ODE
        '
        Me.Physics_ODE.AutoSize = True
        Me.Physics_ODE.Location = New System.Drawing.Point(22, 64)
        Me.Physics_ODE.Margin = New System.Windows.Forms.Padding(4)
        Me.Physics_ODE.Name = "Physics_ODE"
        Me.Physics_ODE.Size = New System.Drawing.Size(192, 24)
        Me.Physics_ODE.TabIndex = 1
        Me.Physics_ODE.TabStop = True
        Me.Physics_ODE.Text = "Open Dynamic Engine"
        Me.Physics_ODE.UseVisualStyleBackColor = True
        '
        'Physics_Hybrid
        '
        Me.Physics_Hybrid.AutoSize = True
        Me.Physics_Hybrid.Location = New System.Drawing.Point(22, 202)
        Me.Physics_Hybrid.Margin = New System.Windows.Forms.Padding(4)
        Me.Physics_Hybrid.Name = "Physics_Hybrid"
        Me.Physics_Hybrid.Size = New System.Drawing.Size(79, 24)
        Me.Physics_Hybrid.TabIndex = 5
        Me.Physics_Hybrid.TabStop = True
        Me.Physics_Hybrid.Text = "Hybrid"
        Me.Physics_Hybrid.UseVisualStyleBackColor = True
        '
        'Physics_Bullet
        '
        Me.Physics_Bullet.AutoSize = True
        Me.Physics_Bullet.Location = New System.Drawing.Point(22, 136)
        Me.Physics_Bullet.Margin = New System.Windows.Forms.Padding(4)
        Me.Physics_Bullet.Name = "Physics_Bullet"
        Me.Physics_Bullet.Size = New System.Drawing.Size(134, 24)
        Me.Physics_Bullet.TabIndex = 3
        Me.Physics_Bullet.TabStop = True
        Me.Physics_Bullet.Text = "Bullet physics "
        Me.Physics_Bullet.UseVisualStyleBackColor = True
        '
        'Physics_Default
        '
        Me.Physics_Default.AutoSize = True
        Me.Physics_Default.Location = New System.Drawing.Point(22, 30)
        Me.Physics_Default.Margin = New System.Windows.Forms.Padding(4)
        Me.Physics_Default.Name = "Physics_Default"
        Me.Physics_Default.Size = New System.Drawing.Size(119, 24)
        Me.Physics_Default.TabIndex = 0
        Me.Physics_Default.TabStop = True
        Me.Physics_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.Physics_Default.UseVisualStyleBackColor = True
        '
        'Physics_Separate
        '
        Me.Physics_Separate.AutoSize = True
        Me.Physics_Separate.Location = New System.Drawing.Point(22, 170)
        Me.Physics_Separate.Margin = New System.Windows.Forms.Padding(4)
        Me.Physics_Separate.Name = "Physics_Separate"
        Me.Physics_Separate.Size = New System.Drawing.Size(263, 24)
        Me.Physics_Separate.TabIndex = 4
        Me.Physics_Separate.TabStop = True
        Me.Physics_Separate.Text = Global.Outworldz.My.Resources.Resources.BP
        Me.Physics_Separate.UseVisualStyleBackColor = True
        '
        'Physics_ubODE
        '
        Me.Physics_ubODE.AutoSize = True
        Me.Physics_ubODE.Location = New System.Drawing.Point(22, 100)
        Me.Physics_ubODE.Margin = New System.Windows.Forms.Padding(4)
        Me.Physics_ubODE.Name = "Physics_ubODE"
        Me.Physics_ubODE.Size = New System.Drawing.Size(225, 24)
        Me.Physics_ubODE.TabIndex = 2
        Me.Physics_ubODE.TabStop = True
        Me.Physics_ubODE.Text = Global.Outworldz.My.Resources.Resources.UBODE_words
        Me.Physics_ubODE.UseVisualStyleBackColor = True
        '
        'Scripts
        '
        Me.Scripts.Controls.Add(Me.RichTextBox1)
        Me.Scripts.Controls.Add(Me.GroupBox8)
        Me.Scripts.Location = New System.Drawing.Point(4, 29)
        Me.Scripts.Name = "Scripts"
        Me.Scripts.Size = New System.Drawing.Size(745, 329)
        Me.Scripts.TabIndex = 3
        Me.Scripts.Text = "Scripts"
        Me.Scripts.UseVisualStyleBackColor = True
        '
        'Permissions
        '
        Me.Permissions.Controls.Add(Me.GroupBox4)
        Me.Permissions.Location = New System.Drawing.Point(4, 29)
        Me.Permissions.Name = "Permissions"
        Me.Permissions.Size = New System.Drawing.Size(745, 329)
        Me.Permissions.TabIndex = 5
        Me.Permissions.Text = "Permissions"
        Me.Permissions.UseVisualStyleBackColor = True
        '
        'Publicity
        '
        Me.Publicity.Controls.Add(Me.GroupBox6)
        Me.Publicity.Controls.Add(Me.GroupBox3)
        Me.Publicity.Location = New System.Drawing.Point(4, 29)
        Me.Publicity.Name = "Publicity"
        Me.Publicity.Size = New System.Drawing.Size(745, 329)
        Me.Publicity.TabIndex = 7
        Me.Publicity.Text = "Publicity"
        Me.Publicity.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Controls.Add(Me.TextBox1)
        Me.GroupBox6.Controls.Add(Me.Opensimworld)
        Me.GroupBox6.Location = New System.Drawing.Point(395, 37)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(303, 193)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Opensimworld API Key"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 50)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 20)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Key"
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(27, 86)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(248, 26)
        Me.TextBox1.TabIndex = 9
        '
        'Opensimworld
        '
        Me.Opensimworld.AutoSize = True
        Me.Opensimworld.Location = New System.Drawing.Point(53, 151)
        Me.Opensimworld.Name = "Opensimworld"
        Me.Opensimworld.Size = New System.Drawing.Size(143, 20)
        Me.Opensimworld.TabIndex = 0
        Me.Opensimworld.TabStop = True
        Me.Opensimworld.Text = "Opensimworld.com"
        '
        'Modules
        '
        Me.Modules.Controls.Add(Me.GroupBox7)
        Me.Modules.Location = New System.Drawing.Point(4, 29)
        Me.Modules.Name = "Modules"
        Me.Modules.Size = New System.Drawing.Size(745, 329)
        Me.Modules.TabIndex = 6
        Me.Modules.Text = "Modules"
        Me.Modules.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(169, 42)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 20)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Y:"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(404, 28)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(273, 267)
        Me.RichTextBox1.TabIndex = 5
        Me.RichTextBox1.Text = ""
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Location = New System.Drawing.Point(390, 40)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(273, 218)
        Me.RichTextBox2.TabIndex = 6
        Me.RichTextBox2.Text = ""
        '
        'RichTextBox3
        '
        Me.RichTextBox3.Location = New System.Drawing.Point(336, 28)
        Me.RichTextBox3.Name = "RichTextBox3"
        Me.RichTextBox3.Size = New System.Drawing.Size(286, 254)
        Me.RichTextBox3.TabIndex = 8
        Me.RichTextBox3.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(57, 30)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 20)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Name:"
        '
        'RichTextBox4
        '
        Me.RichTextBox4.Location = New System.Drawing.Point(430, 30)
        Me.RichTextBox4.Name = "RichTextBox4"
        Me.RichTextBox4.Size = New System.Drawing.Size(273, 267)
        Me.RichTextBox4.TabIndex = 6
        Me.RichTextBox4.Text = ""
        '
        'RichTextBox5
        '
        Me.RichTextBox5.Location = New System.Drawing.Point(369, 33)
        Me.RichTextBox5.Name = "RichTextBox5"
        Me.RichTextBox5.Size = New System.Drawing.Size(273, 267)
        Me.RichTextBox5.TabIndex = 139
        Me.RichTextBox5.Text = ""
        '
        'FormRegion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(780, 492)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "FormRegion"
        Me.Text = "Regions"
        Me.Advanced.ResumeLayout(False)
        Me.Advanced.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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
        Me.TabControl1.ResumeLayout(False)
        Me.Basics.ResumeLayout(False)
        Me.Basics.PerformLayout()
        Me.Options.ResumeLayout(False)
        Me.Maps.ResumeLayout(False)
        Me.Physics.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Scripts.ResumeLayout(False)
        Me.Permissions.ResumeLayout(False)
        Me.Publicity.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.Modules.ResumeLayout(False)
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
    Friend WithEvents ConciergeCheckBox As CheckBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Basics As TabPage
    Friend WithEvents Options As TabPage
    Friend WithEvents Maps As TabPage
    Friend WithEvents Scripts As TabPage
    Friend WithEvents Permissions As TabPage
    Friend WithEvents Modules As TabPage
    Friend WithEvents Publicity As TabPage
    Friend WithEvents Hyperica As LinkLabel
    Friend WithEvents Physics As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Physics_ODE As RadioButton
    Friend WithEvents Physics_Hybrid As RadioButton
    Friend WithEvents Physics_Bullet As RadioButton
    Friend WithEvents Physics_Default As RadioButton
    Friend WithEvents Physics_Separate As RadioButton
    Friend WithEvents Physics_ubODE As RadioButton
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Opensimworld As LinkLabel
    Friend WithEvents Label7 As Label
    Friend WithEvents RichTextBox3 As RichTextBox
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents RichTextBox5 As RichTextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents RichTextBox4 As RichTextBox
    Friend WithEvents RichTextBox1 As RichTextBox
End Class
