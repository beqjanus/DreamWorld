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
        Me.SizeY = New System.Windows.Forms.TextBox()
        Me.SizeX = New System.Windows.Forms.TextBox()
        Me.MaxAgents = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PhysicalPrimMax = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.MaxPrims = New System.Windows.Forms.TextBox()
        Me.NonphysicalPrimMax = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ClampPrimSize = New System.Windows.Forms.CheckBox()
        Me.BirdsCheckBox = New System.Windows.Forms.CheckBox()
        Me.TidesCheckbox = New System.Windows.Forms.CheckBox()
        Me.TPCheckBox1 = New System.Windows.Forms.CheckBox()
        Me.MapHelp = New System.Windows.Forms.PictureBox()
        Me.GodHelp = New System.Windows.Forms.PictureBox()
        Me.AllowGods = New System.Windows.Forms.CheckBox()
        Me.ManagerGod = New System.Windows.Forms.CheckBox()
        Me.RegionGod = New System.Windows.Forms.CheckBox()
        Me.SmartStartCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ScriptTimerTextBox = New System.Windows.Forms.TextBox()
        Me.DisableGBCheckBox = New System.Windows.Forms.CheckBox()
        Me.DisallowForeigners = New System.Windows.Forms.CheckBox()
        Me.DisallowResidents = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Physics_Default = New System.Windows.Forms.RadioButton()
        Me.PhysicsSeparate = New System.Windows.Forms.RadioButton()
        Me.PhysicsubODE = New System.Windows.Forms.RadioButton()
        Me.FrametimeBox = New System.Windows.Forms.TextBox()
        Me.SkipAutoCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.RegionPort = New System.Windows.Forms.TextBox()
        Me.Advanced = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UUID = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.EnabledCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
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
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.DeregisterButton = New System.Windows.Forms.Button()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.ScriptDefaultButton = New System.Windows.Forms.RadioButton()
        Me.XEngineButton = New System.Windows.Forms.RadioButton()
        Me.YEngineButton = New System.Windows.Forms.RadioButton()
        CType(Me.MapHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GodHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Advanced.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.MapBox.SuspendLayout()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'CoordY
        '
        Me.CoordY.Location = New System.Drawing.Point(244, 20)
        Me.CoordY.Margin = New System.Windows.Forms.Padding(4)
        Me.CoordY.Name = "CoordY"
        Me.CoordY.Size = New System.Drawing.Size(55, 26)
        Me.CoordY.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.CoordY, Global.Outworldz.My.Resources.Resources.CoordY)
        '
        'CoordX
        '
        Me.CoordX.Location = New System.Drawing.Point(146, 20)
        Me.CoordX.Margin = New System.Windows.Forms.Padding(4)
        Me.CoordX.Name = "CoordX"
        Me.CoordX.Size = New System.Drawing.Size(58, 26)
        Me.CoordX.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.CoordX, Global.Outworldz.My.Resources.Resources.Coordx)
        '
        'RegionName
        '
        Me.RegionName.Location = New System.Drawing.Point(22, 75)
        Me.RegionName.Margin = New System.Windows.Forms.Padding(4)
        Me.RegionName.Name = "RegionName"
        Me.RegionName.Size = New System.Drawing.Size(214, 26)
        Me.RegionName.TabIndex = 1
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
        Me.RadioButton3.Location = New System.Drawing.Point(30, 93)
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
        'SizeY
        '
        Me.SizeY.Location = New System.Drawing.Point(252, 66)
        Me.SizeY.Margin = New System.Windows.Forms.Padding(4)
        Me.SizeY.Name = "SizeY"
        Me.SizeY.Size = New System.Drawing.Size(62, 26)
        Me.SizeY.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.SizeY, Global.Outworldz.My.Resources.Resources.MustX)
        '
        'SizeX
        '
        Me.SizeX.Location = New System.Drawing.Point(252, 28)
        Me.SizeX.Margin = New System.Windows.Forms.Padding(4)
        Me.SizeX.Name = "SizeX"
        Me.SizeX.Size = New System.Drawing.Size(62, 26)
        Me.SizeX.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.SizeX, Global.Outworldz.My.Resources.Resources.MustY)
        '
        'MaxAgents
        '
        Me.MaxAgents.Location = New System.Drawing.Point(18, 302)
        Me.MaxAgents.Margin = New System.Windows.Forms.Padding(4)
        Me.MaxAgents.Name = "MaxAgents"
        Me.MaxAgents.Size = New System.Drawing.Size(58, 26)
        Me.MaxAgents.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.MaxAgents, Global.Outworldz.My.Resources.Resources.Max_Agents)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(98, 156)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(164, 20)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "Nonphysical Prim Size"
        Me.ToolTip1.SetToolTip(Me.Label5, Global.Outworldz.My.Resources.Resources.Max_NonPhys)
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(104, 192)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(169, 20)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "Physical Prim Max Size"
        Me.ToolTip1.SetToolTip(Me.Label9, Global.Outworldz.My.Resources.Resources.Max_Phys)
        '
        'PhysicalPrimMax
        '
        Me.PhysicalPrimMax.Location = New System.Drawing.Point(18, 188)
        Me.PhysicalPrimMax.Margin = New System.Windows.Forms.Padding(4)
        Me.PhysicalPrimMax.Name = "PhysicalPrimMax"
        Me.PhysicalPrimMax.Size = New System.Drawing.Size(58, 26)
        Me.PhysicalPrimMax.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.PhysicalPrimMax, Global.Outworldz.My.Resources.Resources.Max_Phys)
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(104, 230)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(124, 20)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Clamp Prim Size"
        Me.ToolTip1.SetToolTip(Me.Label10, Global.Outworldz.My.Resources.Resources.ClampSize)
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(104, 264)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(236, 20)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Max Number of Prims in a Parcel"
        Me.ToolTip1.SetToolTip(Me.Label11, Global.Outworldz.My.Resources.Resources.Viewer_Stops_Counting)
        '
        'MaxPrims
        '
        Me.MaxPrims.Location = New System.Drawing.Point(18, 258)
        Me.MaxPrims.Margin = New System.Windows.Forms.Padding(4)
        Me.MaxPrims.Name = "MaxPrims"
        Me.MaxPrims.Size = New System.Drawing.Size(58, 26)
        Me.MaxPrims.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.MaxPrims, Global.Outworldz.My.Resources.Resources.Not_Normal)
        '
        'NonphysicalPrimMax
        '
        Me.NonphysicalPrimMax.Location = New System.Drawing.Point(18, 150)
        Me.NonphysicalPrimMax.Margin = New System.Windows.Forms.Padding(4)
        Me.NonphysicalPrimMax.Name = "NonphysicalPrimMax"
        Me.NonphysicalPrimMax.Size = New System.Drawing.Size(58, 26)
        Me.NonphysicalPrimMax.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.NonphysicalPrimMax, Global.Outworldz.My.Resources.Resources.Normal_Prim)
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(104, 302)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(959, 20)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "How many Avatars + NPC's can be in a region before the region is shown as Full. T" &
    "he actual value is set in Estate Settings in the viewer."
        Me.ToolTip1.SetToolTip(Me.Label12, Global.Outworldz.My.Resources.Resources.Max_Agents)
        '
        'ClampPrimSize
        '
        Me.ClampPrimSize.AutoSize = True
        Me.ClampPrimSize.Location = New System.Drawing.Point(18, 228)
        Me.ClampPrimSize.Margin = New System.Windows.Forms.Padding(4)
        Me.ClampPrimSize.Name = "ClampPrimSize"
        Me.ClampPrimSize.Size = New System.Drawing.Size(22, 21)
        Me.ClampPrimSize.TabIndex = 18
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
        Me.BirdsCheckBox.TabIndex = 21
        Me.BirdsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Bird_Module_word
        Me.ToolTip1.SetToolTip(Me.BirdsCheckBox, Global.Outworldz.My.Resources.Resources.GBoids)
        Me.BirdsCheckBox.UseVisualStyleBackColor = True
        '
        'TidesCheckbox
        '
        Me.TidesCheckbox.AutoSize = True
        Me.TidesCheckbox.Location = New System.Drawing.Point(22, 63)
        Me.TidesCheckbox.Margin = New System.Windows.Forms.Padding(4)
        Me.TidesCheckbox.Name = "TidesCheckbox"
        Me.TidesCheckbox.Size = New System.Drawing.Size(286, 24)
        Me.TidesCheckbox.TabIndex = 21
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
        Me.TPCheckBox1.Size = New System.Drawing.Size(161, 24)
        Me.TPCheckBox1.TabIndex = 22
        Me.TPCheckBox1.Text = Global.Outworldz.My.Resources.Resources.Teleporter_Enable_word
        Me.ToolTip1.SetToolTip(Me.TPCheckBox1, Global.Outworldz.My.Resources.Resources.GTide)
        Me.TPCheckBox1.UseVisualStyleBackColor = True
        '
        'MapHelp
        '
        Me.MapHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.MapHelp.Location = New System.Drawing.Point(248, 75)
        Me.MapHelp.Margin = New System.Windows.Forms.Padding(4)
        Me.MapHelp.Name = "MapHelp"
        Me.MapHelp.Size = New System.Drawing.Size(34, 38)
        Me.MapHelp.TabIndex = 1857
        Me.MapHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.MapHelp, Global.Outworldz.My.Resources.Resources.OverridesMap)
        '
        'GodHelp
        '
        Me.GodHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.GodHelp.Location = New System.Drawing.Point(218, 15)
        Me.GodHelp.Margin = New System.Windows.Forms.Padding(4)
        Me.GodHelp.Name = "GodHelp"
        Me.GodHelp.Size = New System.Drawing.Size(45, 40)
        Me.GodHelp.TabIndex = 1857
        Me.GodHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GodHelp, Global.Outworldz.My.Resources.Resources.Allow_Or_Disallow_Gods_word)
        '
        'AllowGods
        '
        Me.AllowGods.AutoSize = True
        Me.AllowGods.Location = New System.Drawing.Point(21, 78)
        Me.AllowGods.Margin = New System.Windows.Forms.Padding(4)
        Me.AllowGods.Name = "AllowGods"
        Me.AllowGods.Size = New System.Drawing.Size(192, 24)
        Me.AllowGods.TabIndex = 1858
        Me.AllowGods.Text = Global.Outworldz.My.Resources.Resources.Allow_Or_Disallow_Gods_word
        Me.ToolTip1.SetToolTip(Me.AllowGods, resources.GetString("AllowGods.ToolTip"))
        Me.AllowGods.UseVisualStyleBackColor = True
        '
        'ManagerGod
        '
        Me.ManagerGod.AutoSize = True
        Me.ManagerGod.Location = New System.Drawing.Point(21, 148)
        Me.ManagerGod.Margin = New System.Windows.Forms.Padding(4)
        Me.ManagerGod.Name = "ManagerGod"
        Me.ManagerGod.Size = New System.Drawing.Size(195, 24)
        Me.ManagerGod.TabIndex = 6
        Me.ManagerGod.Text = Global.Outworldz.My.Resources.Resources.EstateManagerIsGod_word
        Me.ToolTip1.SetToolTip(Me.ManagerGod, Global.Outworldz.My.Resources.Resources.EMGod)
        Me.ManagerGod.UseVisualStyleBackColor = True
        '
        'RegionGod
        '
        Me.RegionGod.AutoSize = True
        Me.RegionGod.Location = New System.Drawing.Point(21, 112)
        Me.RegionGod.Margin = New System.Windows.Forms.Padding(4)
        Me.RegionGod.Name = "RegionGod"
        Me.RegionGod.Size = New System.Drawing.Size(186, 24)
        Me.RegionGod.TabIndex = 1855
        Me.RegionGod.Text = Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word
        Me.ToolTip1.SetToolTip(Me.RegionGod, Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word)
        Me.RegionGod.UseVisualStyleBackColor = True
        '
        'SmartStartCheckBox
        '
        Me.SmartStartCheckBox.AutoSize = True
        Me.SmartStartCheckBox.Location = New System.Drawing.Point(24, 256)
        Me.SmartStartCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.SmartStartCheckBox.Name = "SmartStartCheckBox"
        Me.SmartStartCheckBox.Size = New System.Drawing.Size(117, 24)
        Me.SmartStartCheckBox.TabIndex = 23
        Me.SmartStartCheckBox.Text = Global.Outworldz.My.Resources.Resources.Smart_Start_word
        Me.ToolTip1.SetToolTip(Me.SmartStartCheckBox, Global.Outworldz.My.Resources.Resources.GTide)
        Me.SmartStartCheckBox.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(104, 344)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(195, 20)
        Me.Label14.TabIndex = 41
        Me.Label14.Text = "Script Timer Rate (0.0909)"
        Me.ToolTip1.SetToolTip(Me.Label14, Global.Outworldz.My.Resources.Resources.Script_Timer_Text)
        '
        'ScriptTimerTextBox
        '
        Me.ScriptTimerTextBox.Location = New System.Drawing.Point(18, 344)
        Me.ScriptTimerTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ScriptTimerTextBox.Name = "ScriptTimerTextBox"
        Me.ScriptTimerTextBox.Size = New System.Drawing.Size(58, 26)
        Me.ScriptTimerTextBox.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.ScriptTimerTextBox, Global.Outworldz.My.Resources.Resources.STComment)
        '
        'DisableGBCheckBox
        '
        Me.DisableGBCheckBox.AutoSize = True
        Me.DisableGBCheckBox.Location = New System.Drawing.Point(22, 128)
        Me.DisableGBCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.DisableGBCheckBox.Name = "DisableGBCheckBox"
        Me.DisableGBCheckBox.Size = New System.Drawing.Size(151, 24)
        Me.DisableGBCheckBox.TabIndex = 24
        Me.DisableGBCheckBox.Text = Global.Outworldz.My.Resources.Resources.Disable_Gloebits_word
        Me.ToolTip1.SetToolTip(Me.DisableGBCheckBox, Global.Outworldz.My.Resources.Resources.Disable_Gloebits_text)
        Me.DisableGBCheckBox.UseVisualStyleBackColor = True
        '
        'DisallowForeigners
        '
        Me.DisallowForeigners.AutoSize = True
        Me.DisallowForeigners.Location = New System.Drawing.Point(22, 158)
        Me.DisallowForeigners.Margin = New System.Windows.Forms.Padding(4)
        Me.DisallowForeigners.Name = "DisallowForeigners"
        Me.DisallowForeigners.Size = New System.Drawing.Size(202, 24)
        Me.DisallowForeigners.TabIndex = 25
        Me.DisallowForeigners.Text = Global.Outworldz.My.Resources.Resources.Disable_Foreigners_word
        Me.ToolTip1.SetToolTip(Me.DisallowForeigners, Global.Outworldz.My.Resources.Resources.No_HG)
        Me.DisallowForeigners.UseVisualStyleBackColor = True
        '
        'DisallowResidents
        '
        Me.DisallowResidents.AutoSize = True
        Me.DisallowResidents.Location = New System.Drawing.Point(22, 189)
        Me.DisallowResidents.Margin = New System.Windows.Forms.Padding(4)
        Me.DisallowResidents.Name = "DisallowResidents"
        Me.DisallowResidents.Size = New System.Drawing.Size(164, 24)
        Me.DisallowResidents.TabIndex = 26
        Me.DisallowResidents.Text = Global.Outworldz.My.Resources.Resources.Disable_Residents
        Me.ToolTip1.SetToolTip(Me.DisallowResidents, Global.Outworldz.My.Resources.Resources.Only_Owners)
        Me.DisallowResidents.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Physics_Default)
        Me.GroupBox1.Controls.Add(Me.PhysicsSeparate)
        Me.GroupBox1.Controls.Add(Me.PhysicsubODE)
        Me.GroupBox1.Location = New System.Drawing.Point(30, 444)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(342, 145)
        Me.GroupBox1.TabIndex = 1879
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Physics"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, Global.Outworldz.My.Resources.Resources.Sim_Rate)
        '
        'Physics_Default
        '
        Me.Physics_Default.AutoSize = True
        Me.Physics_Default.Location = New System.Drawing.Point(8, 27)
        Me.Physics_Default.Margin = New System.Windows.Forms.Padding(4)
        Me.Physics_Default.Name = "Physics_Default"
        Me.Physics_Default.Size = New System.Drawing.Size(119, 24)
        Me.Physics_Default.TabIndex = 137
        Me.Physics_Default.TabStop = True
        Me.Physics_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.Physics_Default.UseVisualStyleBackColor = True
        '
        'PhysicsSeparate
        '
        Me.PhysicsSeparate.AutoSize = True
        Me.PhysicsSeparate.Location = New System.Drawing.Point(7, 91)
        Me.PhysicsSeparate.Margin = New System.Windows.Forms.Padding(4)
        Me.PhysicsSeparate.Name = "PhysicsSeparate"
        Me.PhysicsSeparate.Size = New System.Drawing.Size(263, 24)
        Me.PhysicsSeparate.TabIndex = 37
        Me.PhysicsSeparate.TabStop = True
        Me.PhysicsSeparate.Text = Global.Outworldz.My.Resources.Resources.BP
        Me.PhysicsSeparate.UseVisualStyleBackColor = True
        '
        'PhysicsubODE
        '
        Me.PhysicsubODE.AutoSize = True
        Me.PhysicsubODE.Location = New System.Drawing.Point(8, 59)
        Me.PhysicsubODE.Margin = New System.Windows.Forms.Padding(4)
        Me.PhysicsubODE.Name = "PhysicsubODE"
        Me.PhysicsubODE.Size = New System.Drawing.Size(225, 24)
        Me.PhysicsubODE.TabIndex = 35
        Me.PhysicsubODE.TabStop = True
        Me.PhysicsubODE.Text = "Ubit Open Dynamic Engine"
        Me.PhysicsubODE.UseVisualStyleBackColor = True
        '
        'FrametimeBox
        '
        Me.FrametimeBox.Location = New System.Drawing.Point(18, 386)
        Me.FrametimeBox.Margin = New System.Windows.Forms.Padding(4)
        Me.FrametimeBox.Name = "FrametimeBox"
        Me.FrametimeBox.Size = New System.Drawing.Size(58, 26)
        Me.FrametimeBox.TabIndex = 42
        Me.ToolTip1.SetToolTip(Me.FrametimeBox, Global.Outworldz.My.Resources.Resources.FrameTime)
        '
        'SkipAutoCheckBox
        '
        Me.SkipAutoCheckBox.AutoSize = True
        Me.SkipAutoCheckBox.Location = New System.Drawing.Point(22, 224)
        Me.SkipAutoCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.SkipAutoCheckBox.Name = "SkipAutoCheckBox"
        Me.SkipAutoCheckBox.Size = New System.Drawing.Size(156, 24)
        Me.SkipAutoCheckBox.TabIndex = 27
        Me.SkipAutoCheckBox.Text = Global.Outworldz.My.Resources.Resources.Skip_Autobackup_word
        Me.ToolTip1.SetToolTip(Me.SkipAutoCheckBox, Global.Outworldz.My.Resources.Resources.WillNotSave)
        Me.SkipAutoCheckBox.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(104, 386)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(157, 20)
        Me.Label15.TabIndex = 43
        Me.Label15.Text = "Frame Rate (0.0909)"
        Me.ToolTip1.SetToolTip(Me.Label15, Global.Outworldz.My.Resources.Resources.FRText)
        '
        'RegionPort
        '
        Me.RegionPort.Enabled = False
        Me.RegionPort.Location = New System.Drawing.Point(146, 58)
        Me.RegionPort.Margin = New System.Windows.Forms.Padding(4)
        Me.RegionPort.Name = "RegionPort"
        Me.RegionPort.Size = New System.Drawing.Size(58, 26)
        Me.RegionPort.TabIndex = 39
        '
        'Advanced
        '
        Me.Advanced.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Advanced.Controls.Add(Me.Label16)
        Me.Advanced.Controls.Add(Me.Label15)
        Me.Advanced.Controls.Add(Me.FrametimeBox)
        Me.Advanced.Controls.Add(Me.Label14)
        Me.Advanced.Controls.Add(Me.ScriptTimerTextBox)
        Me.Advanced.Controls.Add(Me.RegionPort)
        Me.Advanced.Controls.Add(Me.ClampPrimSize)
        Me.Advanced.Controls.Add(Me.Label12)
        Me.Advanced.Controls.Add(Me.Label10)
        Me.Advanced.Controls.Add(Me.NonphysicalPrimMax)
        Me.Advanced.Controls.Add(Me.Label11)
        Me.Advanced.Controls.Add(Me.PhysicalPrimMax)
        Me.Advanced.Controls.Add(Me.Label6)
        Me.Advanced.Controls.Add(Me.Label9)
        Me.Advanced.Controls.Add(Me.MaxPrims)
        Me.Advanced.Controls.Add(Me.Label5)
        Me.Advanced.Controls.Add(Me.MaxAgents)
        Me.Advanced.Controls.Add(Me.Label4)
        Me.Advanced.Controls.Add(Me.Label1)
        Me.Advanced.Controls.Add(Me.UUID)
        Me.Advanced.Controls.Add(Me.CoordY)
        Me.Advanced.Controls.Add(Me.CoordX)
        Me.Advanced.Location = New System.Drawing.Point(22, 384)
        Me.Advanced.Margin = New System.Windows.Forms.Padding(4)
        Me.Advanced.Name = "Advanced"
        Me.Advanced.Padding = New System.Windows.Forms.Padding(4)
        Me.Advanced.Size = New System.Drawing.Size(468, 426)
        Me.Advanced.TabIndex = 26
        Me.Advanced.TabStop = False
        Me.Advanced.Text = "Region"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(214, 64)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(101, 20)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "Region Ports"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 82)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 20)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "UUID"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 24)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 20)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Maps: X"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(214, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 20)
        Me.Label1.TabIndex = 17
        '
        'UUID
        '
        Me.UUID.Location = New System.Drawing.Point(14, 106)
        Me.UUID.Margin = New System.Windows.Forms.Padding(4)
        Me.UUID.Name = "UUID"
        Me.UUID.Size = New System.Drawing.Size(320, 26)
        Me.UUID.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 51)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(426, 20)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Alpha-Numeric plus minus sign (no spaces or special chars)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.RadioButton4)
        Me.GroupBox2.Controls.Add(Me.RadioButton3)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.SizeY)
        Me.GroupBox2.Controls.Add(Me.SizeX)
        Me.GroupBox2.Location = New System.Drawing.Point(22, 152)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(340, 171)
        Me.GroupBox2.TabIndex = 23
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sim Size"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(156, 39)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 20)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Or:"
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
        Me.Label2.Location = New System.Drawing.Point(222, 39)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 20)
        Me.Label2.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(36, 330)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 34)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Save_word
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        Me.DeleteButton.Location = New System.Drawing.Point(255, 332)
        Me.DeleteButton.Margin = New System.Windows.Forms.Padding(4)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(87, 34)
        Me.DeleteButton.TabIndex = 11
        Me.DeleteButton.Text = Global.Outworldz.My.Resources.Resources.Delete_word
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'EnabledCheckBox
        '
        Me.EnabledCheckBox.AutoSize = True
        Me.EnabledCheckBox.Location = New System.Drawing.Point(36, 117)
        Me.EnabledCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.EnabledCheckBox.Name = "EnabledCheckBox"
        Me.EnabledCheckBox.Size = New System.Drawing.Size(94, 24)
        Me.EnabledCheckBox.TabIndex = 2
        Me.EnabledCheckBox.Text = Global.Outworldz.My.Resources.Resources.Enabled_word
        Me.EnabledCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBox6.Controls.Add(Me.GroupBox8)
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.Controls.Add(Me.GroupBox3)
        Me.GroupBox6.Controls.Add(Me.GroupBox4)
        Me.GroupBox6.Controls.Add(Me.MapBox)
        Me.GroupBox6.Controls.Add(Me.GroupBox5)
        Me.GroupBox6.Controls.Add(Me.GroupBox1)
        Me.GroupBox6.Location = New System.Drawing.Point(476, 51)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Size = New System.Drawing.Size(724, 759)
        Me.GroupBox6.TabIndex = 1879
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Region Specific Settings"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.SkipAutoCheckBox)
        Me.GroupBox7.Controls.Add(Me.DisallowResidents)
        Me.GroupBox7.Controls.Add(Me.DisallowForeigners)
        Me.GroupBox7.Controls.Add(Me.DisableGBCheckBox)
        Me.GroupBox7.Controls.Add(Me.SmartStartCheckBox)
        Me.GroupBox7.Controls.Add(Me.TPCheckBox1)
        Me.GroupBox7.Controls.Add(Me.TidesCheckbox)
        Me.GroupBox7.Controls.Add(Me.BirdsCheckBox)
        Me.GroupBox7.Location = New System.Drawing.Point(383, 464)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox7.Size = New System.Drawing.Size(333, 295)
        Me.GroupBox7.TabIndex = 1881
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Modules"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(46, 30)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(183, 20)
        Me.Label13.TabIndex = 1884
        Me.Label13.Text = "Region Specific Settings"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Publish)
        Me.GroupBox3.Controls.Add(Me.NoPublish)
        Me.GroupBox3.Controls.Add(Me.PublishDefault)
        Me.GroupBox3.Location = New System.Drawing.Point(30, 66)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(346, 152)
        Me.GroupBox3.TabIndex = 1883
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Search"
        '
        'Publish
        '
        Me.Publish.AutoSize = True
        Me.Publish.Location = New System.Drawing.Point(16, 111)
        Me.Publish.Margin = New System.Windows.Forms.Padding(4)
        Me.Publish.Name = "Publish"
        Me.Publish.Size = New System.Drawing.Size(261, 24)
        Me.Publish.TabIndex = 1881
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
        Me.NoPublish.TabIndex = 1880
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
        Me.PublishDefault.TabIndex = 1879
        Me.PublishDefault.TabStop = True
        Me.PublishDefault.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.PublishDefault.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Gods_Use_Default)
        Me.GroupBox4.Controls.Add(Me.AllowGods)
        Me.GroupBox4.Controls.Add(Me.GodHelp)
        Me.GroupBox4.Controls.Add(Me.ManagerGod)
        Me.GroupBox4.Controls.Add(Me.RegionGod)
        Me.GroupBox4.Location = New System.Drawing.Point(30, 245)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(342, 183)
        Me.GroupBox4.TabIndex = 1882
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Permissions"
        '
        'Gods_Use_Default
        '
        Me.Gods_Use_Default.AutoSize = True
        Me.Gods_Use_Default.Location = New System.Drawing.Point(21, 40)
        Me.Gods_Use_Default.Margin = New System.Windows.Forms.Padding(4)
        Me.Gods_Use_Default.Name = "Gods_Use_Default"
        Me.Gods_Use_Default.Size = New System.Drawing.Size(120, 24)
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
        Me.MapBox.Location = New System.Drawing.Point(384, 66)
        Me.MapBox.Margin = New System.Windows.Forms.Padding(4)
        Me.MapBox.Name = "MapBox"
        Me.MapBox.Padding = New System.Windows.Forms.Padding(4)
        Me.MapBox.Size = New System.Drawing.Size(333, 370)
        Me.MapBox.TabIndex = 1881
        Me.MapBox.TabStop = False
        Me.MapBox.Text = "Maps"
        '
        'Maps_Use_Default
        '
        Me.Maps_Use_Default.AutoSize = True
        Me.Maps_Use_Default.Location = New System.Drawing.Point(21, 34)
        Me.Maps_Use_Default.Margin = New System.Windows.Forms.Padding(4)
        Me.Maps_Use_Default.Name = "Maps_Use_Default"
        Me.Maps_Use_Default.Size = New System.Drawing.Size(119, 24)
        Me.Maps_Use_Default.TabIndex = 1858
        Me.Maps_Use_Default.TabStop = True
        Me.Maps_Use_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.Maps_Use_Default.UseVisualStyleBackColor = True
        '
        'MapPicture
        '
        Me.MapPicture.InitialImage = CType(resources.GetObject("MapPicture.InitialImage"), System.Drawing.Image)
        Me.MapPicture.Location = New System.Drawing.Point(45, 222)
        Me.MapPicture.Margin = New System.Windows.Forms.Padding(4)
        Me.MapPicture.Name = "MapPicture"
        Me.MapPicture.Size = New System.Drawing.Size(150, 140)
        Me.MapPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.MapPicture.TabIndex = 138
        Me.MapPicture.TabStop = False
        '
        'MapNone
        '
        Me.MapNone.AutoSize = True
        Me.MapNone.Location = New System.Drawing.Point(20, 63)
        Me.MapNone.Margin = New System.Windows.Forms.Padding(4)
        Me.MapNone.Name = "MapNone"
        Me.MapNone.Size = New System.Drawing.Size(72, 24)
        Me.MapNone.TabIndex = 7
        Me.MapNone.TabStop = True
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
        Me.MapSimple.TabIndex = 8
        Me.MapSimple.TabStop = True
        Me.MapSimple.Text = Global.Outworldz.My.Resources.Resources.Simple_but_Fast_word
        Me.MapSimple.UseVisualStyleBackColor = True
        '
        'MapBetter
        '
        Me.MapBetter.AutoSize = True
        Me.MapBetter.Location = New System.Drawing.Point(21, 150)
        Me.MapBetter.Margin = New System.Windows.Forms.Padding(4)
        Me.MapBetter.Name = "MapBetter"
        Me.MapBetter.Size = New System.Drawing.Size(173, 24)
        Me.MapBetter.TabIndex = 10
        Me.MapBetter.TabStop = True
        Me.MapBetter.Text = Global.Outworldz.My.Resources.Resources.Better_Prims
        Me.MapBetter.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.MapBetter.UseVisualStyleBackColor = True
        '
        'MapBest
        '
        Me.MapBest.AutoSize = True
        Me.MapBest.Location = New System.Drawing.Point(20, 183)
        Me.MapBest.Margin = New System.Windows.Forms.Padding(4)
        Me.MapBest.Name = "MapBest"
        Me.MapBest.Size = New System.Drawing.Size(254, 24)
        Me.MapBest.TabIndex = 11
        Me.MapBest.TabStop = True
        Me.MapBest.Text = Global.Outworldz.My.Resources.Resources.Best_Prims
        Me.MapBest.UseVisualStyleBackColor = True
        '
        'MapGood
        '
        Me.MapGood.AutoSize = True
        Me.MapGood.Location = New System.Drawing.Point(21, 120)
        Me.MapGood.Margin = New System.Windows.Forms.Padding(4)
        Me.MapGood.Name = "MapGood"
        Me.MapGood.Size = New System.Drawing.Size(147, 24)
        Me.MapGood.TabIndex = 9
        Me.MapGood.TabStop = True
        Me.MapGood.Text = Global.Outworldz.My.Resources.Resources.Good_Warp3D_word
        Me.MapGood.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Size = New System.Drawing.Size(300, 150)
        Me.GroupBox5.TabIndex = 1885
        Me.GroupBox5.TabStop = False
        '
        'DeregisterButton
        '
        Me.DeregisterButton.Location = New System.Drawing.Point(134, 332)
        Me.DeregisterButton.Margin = New System.Windows.Forms.Padding(4)
        Me.DeregisterButton.Name = "DeregisterButton"
        Me.DeregisterButton.Size = New System.Drawing.Size(112, 34)
        Me.DeregisterButton.TabIndex = 1880
        Me.DeregisterButton.Text = Global.Outworldz.My.Resources.Resources.Deregister_word
        Me.DeregisterButton.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(1214, 33)
        Me.MenuStrip2.TabIndex = 1888
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(85, 29)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.ScriptDefaultButton)
        Me.GroupBox8.Controls.Add(Me.XEngineButton)
        Me.GroupBox8.Controls.Add(Me.YEngineButton)
        Me.GroupBox8.Location = New System.Drawing.Point(26, 611)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox8.Size = New System.Drawing.Size(354, 148)
        Me.GroupBox8.TabIndex = 1886
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Script Engine"
        '
        'ScriptDefaultButton
        '
        Me.ScriptDefaultButton.AutoSize = True
        Me.ScriptDefaultButton.Location = New System.Drawing.Point(21, 37)
        Me.ScriptDefaultButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ScriptDefaultButton.Name = "ScriptDefaultButton"
        Me.ScriptDefaultButton.Size = New System.Drawing.Size(119, 24)
        Me.ScriptDefaultButton.TabIndex = 1858
        Me.ScriptDefaultButton.TabStop = True
        Me.ScriptDefaultButton.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
        Me.ScriptDefaultButton.UseVisualStyleBackColor = True
        '
        'XEngineButton
        '
        Me.XEngineButton.AutoSize = True
        Me.XEngineButton.Location = New System.Drawing.Point(20, 71)
        Me.XEngineButton.Margin = New System.Windows.Forms.Padding(4)
        Me.XEngineButton.Name = "XEngineButton"
        Me.XEngineButton.Size = New System.Drawing.Size(95, 24)
        Me.XEngineButton.TabIndex = 7
        Me.XEngineButton.TabStop = True
        Me.XEngineButton.Text = "XEngine"
        Me.XEngineButton.UseVisualStyleBackColor = True
        '
        'YEngineButton
        '
        Me.YEngineButton.AutoSize = True
        Me.YEngineButton.Location = New System.Drawing.Point(21, 103)
        Me.YEngineButton.Margin = New System.Windows.Forms.Padding(4)
        Me.YEngineButton.Name = "YEngineButton"
        Me.YEngineButton.Size = New System.Drawing.Size(95, 24)
        Me.YEngineButton.TabIndex = 9
        Me.YEngineButton.TabStop = True
        Me.YEngineButton.Text = "YEngine"
        Me.YEngineButton.UseVisualStyleBackColor = True
        '
        'FormRegion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1214, 823)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.DeregisterButton)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.MapHelp)
        Me.Controls.Add(Me.EnabledCheckBox)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Advanced)
        Me.Controls.Add(Me.RegionName)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormRegion"
        Me.Text = "Region"
        CType(Me.MapHelp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GodHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Advanced.ResumeLayout(False)
        Me.Advanced.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
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
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
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
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents RadioButton4 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents SizeY As TextBox
    Friend WithEvents SizeX As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents DeleteButton As Button
    Friend WithEvents EnabledCheckBox As CheckBox
    Friend WithEvents MaxAgents As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents PhysicalPrimMax As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents NonphysicalPrimMax As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents MaxPrims As TextBox
    Friend WithEvents ClampPrimSize As CheckBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents NoPublish As RadioButton
    Friend WithEvents PublishDefault As RadioButton
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Gods_Use_Default As CheckBox
    Friend WithEvents AllowGods As CheckBox
    Friend WithEvents GodHelp As PictureBox
    Friend WithEvents ManagerGod As CheckBox
    Friend WithEvents RegionGod As CheckBox
    Friend WithEvents MapBox As GroupBox
    Friend WithEvents Maps_Use_Default As RadioButton
    Friend WithEvents MapHelp As PictureBox
    Friend WithEvents MapPicture As PictureBox
    Friend WithEvents MapNone As RadioButton
    Friend WithEvents MapSimple As RadioButton
    Friend WithEvents MapBetter As RadioButton
    Friend WithEvents MapBest As RadioButton
    Friend WithEvents MapGood As RadioButton
    Friend WithEvents GroupBox5 As GroupBox

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Physics_Default As RadioButton
    Friend WithEvents PhysicsSeparate As RadioButton
    Friend WithEvents PhysicsubODE As RadioButton
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
    Friend WithEvents Label14 As Label
    Friend WithEvents ScriptTimerTextBox As TextBox
    Friend WithEvents DisableGBCheckBox As CheckBox
    Friend WithEvents DisallowForeigners As CheckBox
    Friend WithEvents DisallowResidents As CheckBox
    Friend WithEvents Label15 As Label
    Friend WithEvents FrametimeBox As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents SkipAutoCheckBox As CheckBox
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents ScriptDefaultButton As RadioButton
    Friend WithEvents XEngineButton As RadioButton
    Friend WithEvents YEngineButton As RadioButton
End Class
