<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormTrees
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
        Me.Flat = New System.Windows.Forms.RadioButton()
        Me.Rand = New System.Windows.Forms.RadioButton()
        Me.AI = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.FreezeButton = New System.Windows.Forms.Button()
        Me.RevertButton = New System.Windows.Forms.Button()
        Me.None = New System.Windows.Forms.CheckBox()
        Me.All = New System.Windows.Forms.CheckBox()
        Me.ApplyButtton = New System.Windows.Forms.Button()
        Me.Undergrowth = New System.Windows.Forms.CheckBox()
        Me.Grass4 = New System.Windows.Forms.CheckBox()
        Me.Grass3 = New System.Windows.Forms.CheckBox()
        Me.Grass2 = New System.Windows.Forms.CheckBox()
        Me.Grass1 = New System.Windows.Forms.CheckBox()
        Me.Grass0 = New System.Windows.Forms.CheckBox()
        Me.BeachGrass = New System.Windows.Forms.CheckBox()
        Me.Kelp2 = New System.Windows.Forms.CheckBox()
        Me.Kelp1 = New System.Windows.Forms.CheckBox()
        Me.SeaSword = New System.Windows.Forms.CheckBox()
        Me.Eelgrass = New System.Windows.Forms.CheckBox()
        Me.Fern = New System.Windows.Forms.CheckBox()
        Me.Eucalyptus = New System.Windows.Forms.CheckBox()
        Me.WinterAspen = New System.Windows.Forms.CheckBox()
        Me.WinterPine2 = New System.Windows.Forms.CheckBox()
        Me.WinterPine1 = New System.Windows.Forms.CheckBox()
        Me.Plumeria = New System.Windows.Forms.CheckBox()
        Me.Cypress2 = New System.Windows.Forms.CheckBox()
        Me.Cypress1 = New System.Windows.Forms.CheckBox()
        Me.Palm2 = New System.Windows.Forms.CheckBox()
        Me.Palm1 = New System.Windows.Forms.CheckBox()
        Me.TropicalBush2 = New System.Windows.Forms.CheckBox()
        Me.TropicalBush1 = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Oak = New System.Windows.Forms.CheckBox()
        Me.Pine2 = New System.Windows.Forms.CheckBox()
        Me.Pine1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Water = New System.Windows.Forms.RadioButton()
        Me.TerrainPic = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TerrainToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadTerrainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveTerrainToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAllTerrainsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegeenerateTerrainsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TerrainsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAllRunningRegioonTerrainsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TerrainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadTTerrainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveTerrainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RebuildAllTerrainsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAllTerrainsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.TerrainPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Flat
        '
        Me.Flat.AutoSize = True
        Me.Flat.Location = New System.Drawing.Point(23, 20)
        Me.Flat.Name = "Flat"
        Me.Flat.Size = New System.Drawing.Size(42, 17)
        Me.Flat.TabIndex = 0
        Me.Flat.TabStop = True
        Me.Flat.Text = "Flat"
        Me.Flat.UseVisualStyleBackColor = True
        '
        'Rand
        '
        Me.Rand.AutoSize = True
        Me.Rand.Location = New System.Drawing.Point(23, 43)
        Me.Rand.Name = "Rand"
        Me.Rand.Size = New System.Drawing.Size(101, 17)
        Me.Rand.TabIndex = 1
        Me.Rand.TabStop = True
        Me.Rand.Text = "Random Terrain"
        Me.Rand.UseVisualStyleBackColor = True
        '
        'AI
        '
        Me.AI.AutoSize = True
        Me.AI.Location = New System.Drawing.Point(138, 18)
        Me.AI.Name = "AI"
        Me.AI.Size = New System.Drawing.Size(75, 17)
        Me.AI.TabIndex = 2
        Me.AI.TabStop = True
        Me.AI.Text = "Generated"
        Me.AI.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.FreezeButton)
        Me.GroupBox1.Controls.Add(Me.RevertButton)
        Me.GroupBox1.Controls.Add(Me.None)
        Me.GroupBox1.Controls.Add(Me.All)
        Me.GroupBox1.Controls.Add(Me.ApplyButtton)
        Me.GroupBox1.Controls.Add(Me.Undergrowth)
        Me.GroupBox1.Controls.Add(Me.Grass4)
        Me.GroupBox1.Controls.Add(Me.Grass3)
        Me.GroupBox1.Controls.Add(Me.Grass2)
        Me.GroupBox1.Controls.Add(Me.Grass1)
        Me.GroupBox1.Controls.Add(Me.Grass0)
        Me.GroupBox1.Controls.Add(Me.BeachGrass)
        Me.GroupBox1.Controls.Add(Me.Kelp2)
        Me.GroupBox1.Controls.Add(Me.Kelp1)
        Me.GroupBox1.Controls.Add(Me.SeaSword)
        Me.GroupBox1.Controls.Add(Me.Eelgrass)
        Me.GroupBox1.Controls.Add(Me.Fern)
        Me.GroupBox1.Controls.Add(Me.Eucalyptus)
        Me.GroupBox1.Controls.Add(Me.WinterAspen)
        Me.GroupBox1.Controls.Add(Me.WinterPine2)
        Me.GroupBox1.Controls.Add(Me.WinterPine1)
        Me.GroupBox1.Controls.Add(Me.Plumeria)
        Me.GroupBox1.Controls.Add(Me.Cypress2)
        Me.GroupBox1.Controls.Add(Me.Cypress1)
        Me.GroupBox1.Controls.Add(Me.Palm2)
        Me.GroupBox1.Controls.Add(Me.Palm1)
        Me.GroupBox1.Controls.Add(Me.TropicalBush2)
        Me.GroupBox1.Controls.Add(Me.TropicalBush1)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Oak)
        Me.GroupBox1.Controls.Add(Me.Pine2)
        Me.GroupBox1.Controls.Add(Me.Pine1)
        Me.GroupBox1.Location = New System.Drawing.Point(297, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(648, 301)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Plants"
        '
        'FreezeButton
        '
        Me.FreezeButton.Image = Global.Outworldz.My.Resources.Resources.redo
        Me.FreezeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.FreezeButton.Location = New System.Drawing.Point(222, 250)
        Me.FreezeButton.Name = "FreezeButton"
        Me.FreezeButton.Size = New System.Drawing.Size(122, 34)
        Me.FreezeButton.TabIndex = 35
        Me.FreezeButton.Text = "Freeze"
        Me.FreezeButton.UseVisualStyleBackColor = True
        '
        'RevertButton
        '
        Me.RevertButton.Image = Global.Outworldz.My.Resources.Resources.redo
        Me.RevertButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RevertButton.Location = New System.Drawing.Point(222, 210)
        Me.RevertButton.Name = "RevertButton"
        Me.RevertButton.Size = New System.Drawing.Size(122, 34)
        Me.RevertButton.TabIndex = 34
        Me.RevertButton.Text = "Revert"
        Me.RevertButton.UseVisualStyleBackColor = True
        '
        'None
        '
        Me.None.AutoSize = True
        Me.None.Location = New System.Drawing.Point(19, 41)
        Me.None.Name = "None"
        Me.None.Size = New System.Drawing.Size(52, 17)
        Me.None.TabIndex = 32
        Me.None.Text = "None"
        Me.None.UseVisualStyleBackColor = True
        '
        'All
        '
        Me.All.AutoSize = True
        Me.All.Location = New System.Drawing.Point(19, 18)
        Me.All.Name = "All"
        Me.All.Size = New System.Drawing.Size(37, 17)
        Me.All.TabIndex = 31
        Me.All.Text = "All"
        Me.All.UseVisualStyleBackColor = True
        '
        'ApplyButtton
        '
        Me.ApplyButtton.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.ApplyButtton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ApplyButtton.Location = New System.Drawing.Point(222, 167)
        Me.ApplyButtton.Name = "ApplyButtton"
        Me.ApplyButtton.Size = New System.Drawing.Size(122, 37)
        Me.ApplyButtton.TabIndex = 30
        Me.ApplyButtton.Text = "Apply"
        Me.ApplyButtton.UseVisualStyleBackColor = True
        '
        'Undergrowth
        '
        Me.Undergrowth.AutoSize = True
        Me.Undergrowth.Location = New System.Drawing.Point(235, 21)
        Me.Undergrowth.Name = "Undergrowth"
        Me.Undergrowth.Size = New System.Drawing.Size(87, 17)
        Me.Undergrowth.TabIndex = 29
        Me.Undergrowth.Text = "Undergrowth"
        Me.Undergrowth.UseVisualStyleBackColor = True
        '
        'Grass4
        '
        Me.Grass4.AutoSize = True
        Me.Grass4.Location = New System.Drawing.Point(128, 114)
        Me.Grass4.Name = "Grass4"
        Me.Grass4.Size = New System.Drawing.Size(62, 17)
        Me.Grass4.TabIndex = 28
        Me.Grass4.Text = "Grass 4"
        Me.Grass4.UseVisualStyleBackColor = True
        '
        'Grass3
        '
        Me.Grass3.AutoSize = True
        Me.Grass3.Location = New System.Drawing.Point(128, 91)
        Me.Grass3.Name = "Grass3"
        Me.Grass3.Size = New System.Drawing.Size(62, 17)
        Me.Grass3.TabIndex = 27
        Me.Grass3.Text = "Grass 3"
        Me.Grass3.UseVisualStyleBackColor = True
        '
        'Grass2
        '
        Me.Grass2.AutoSize = True
        Me.Grass2.Location = New System.Drawing.Point(128, 68)
        Me.Grass2.Name = "Grass2"
        Me.Grass2.Size = New System.Drawing.Size(62, 17)
        Me.Grass2.TabIndex = 26
        Me.Grass2.Text = "Grass 2"
        Me.Grass2.UseVisualStyleBackColor = True
        '
        'Grass1
        '
        Me.Grass1.AutoSize = True
        Me.Grass1.Location = New System.Drawing.Point(128, 45)
        Me.Grass1.Name = "Grass1"
        Me.Grass1.Size = New System.Drawing.Size(62, 17)
        Me.Grass1.TabIndex = 25
        Me.Grass1.Text = "Grass 1"
        Me.Grass1.UseVisualStyleBackColor = True
        '
        'Grass0
        '
        Me.Grass0.AutoSize = True
        Me.Grass0.Location = New System.Drawing.Point(128, 21)
        Me.Grass0.Name = "Grass0"
        Me.Grass0.Size = New System.Drawing.Size(62, 17)
        Me.Grass0.TabIndex = 24
        Me.Grass0.Text = "Grass 0"
        Me.Grass0.UseVisualStyleBackColor = True
        '
        'BeachGrass
        '
        Me.BeachGrass.AutoSize = True
        Me.BeachGrass.Location = New System.Drawing.Point(235, 137)
        Me.BeachGrass.Name = "BeachGrass"
        Me.BeachGrass.Size = New System.Drawing.Size(87, 17)
        Me.BeachGrass.TabIndex = 23
        Me.BeachGrass.Text = "Beach Grass"
        Me.BeachGrass.UseVisualStyleBackColor = True
        '
        'Kelp2
        '
        Me.Kelp2.AutoSize = True
        Me.Kelp2.Location = New System.Drawing.Point(128, 253)
        Me.Kelp2.Name = "Kelp2"
        Me.Kelp2.Size = New System.Drawing.Size(56, 17)
        Me.Kelp2.TabIndex = 22
        Me.Kelp2.Text = "Kelp 2"
        Me.Kelp2.UseVisualStyleBackColor = True
        '
        'Kelp1
        '
        Me.Kelp1.AutoSize = True
        Me.Kelp1.Location = New System.Drawing.Point(128, 230)
        Me.Kelp1.Name = "Kelp1"
        Me.Kelp1.Size = New System.Drawing.Size(56, 17)
        Me.Kelp1.TabIndex = 21
        Me.Kelp1.Text = "Kelp 1"
        Me.Kelp1.UseVisualStyleBackColor = True
        '
        'SeaSword
        '
        Me.SeaSword.AutoSize = True
        Me.SeaSword.Location = New System.Drawing.Point(128, 207)
        Me.SeaSword.Name = "SeaSword"
        Me.SeaSword.Size = New System.Drawing.Size(78, 17)
        Me.SeaSword.TabIndex = 20
        Me.SeaSword.Text = "Sea Sword"
        Me.SeaSword.UseVisualStyleBackColor = True
        '
        'Eelgrass
        '
        Me.Eelgrass.AutoSize = True
        Me.Eelgrass.Location = New System.Drawing.Point(128, 184)
        Me.Eelgrass.Name = "Eelgrass"
        Me.Eelgrass.Size = New System.Drawing.Size(66, 17)
        Me.Eelgrass.TabIndex = 19
        Me.Eelgrass.Text = "Eelgrass"
        Me.Eelgrass.UseVisualStyleBackColor = True
        '
        'Fern
        '
        Me.Fern.AutoSize = True
        Me.Fern.Location = New System.Drawing.Point(19, 90)
        Me.Fern.Name = "Fern"
        Me.Fern.Size = New System.Drawing.Size(47, 17)
        Me.Fern.TabIndex = 18
        Me.Fern.Text = "Fern"
        Me.Fern.UseVisualStyleBackColor = True
        '
        'Eucalyptus
        '
        Me.Eucalyptus.AutoSize = True
        Me.Eucalyptus.Location = New System.Drawing.Point(19, 67)
        Me.Eucalyptus.Name = "Eucalyptus"
        Me.Eucalyptus.Size = New System.Drawing.Size(78, 17)
        Me.Eucalyptus.TabIndex = 17
        Me.Eucalyptus.Text = "Eucalyptus"
        Me.Eucalyptus.UseVisualStyleBackColor = True
        '
        'WinterAspen
        '
        Me.WinterAspen.AutoSize = True
        Me.WinterAspen.Location = New System.Drawing.Point(19, 206)
        Me.WinterAspen.Name = "WinterAspen"
        Me.WinterAspen.Size = New System.Drawing.Size(90, 17)
        Me.WinterAspen.TabIndex = 16
        Me.WinterAspen.Text = "Winter Aspen"
        Me.WinterAspen.UseVisualStyleBackColor = True
        '
        'WinterPine2
        '
        Me.WinterPine2.AutoSize = True
        Me.WinterPine2.Location = New System.Drawing.Point(19, 253)
        Me.WinterPine2.Name = "WinterPine2"
        Me.WinterPine2.Size = New System.Drawing.Size(90, 17)
        Me.WinterPine2.TabIndex = 15
        Me.WinterPine2.Text = "Winter Pine 2"
        Me.WinterPine2.UseVisualStyleBackColor = True
        '
        'WinterPine1
        '
        Me.WinterPine1.AutoSize = True
        Me.WinterPine1.Location = New System.Drawing.Point(19, 230)
        Me.WinterPine1.Name = "WinterPine1"
        Me.WinterPine1.Size = New System.Drawing.Size(90, 17)
        Me.WinterPine1.TabIndex = 14
        Me.WinterPine1.Text = "Winter Pine 1"
        Me.WinterPine1.UseVisualStyleBackColor = True
        '
        'Plumeria
        '
        Me.Plumeria.AutoSize = True
        Me.Plumeria.Location = New System.Drawing.Point(19, 183)
        Me.Plumeria.Name = "Plumeria"
        Me.Plumeria.Size = New System.Drawing.Size(66, 17)
        Me.Plumeria.TabIndex = 13
        Me.Plumeria.Text = "Plumeria"
        Me.Plumeria.UseVisualStyleBackColor = True
        '
        'Cypress2
        '
        Me.Cypress2.AutoSize = True
        Me.Cypress2.Location = New System.Drawing.Point(128, 160)
        Me.Cypress2.Name = "Cypress2"
        Me.Cypress2.Size = New System.Drawing.Size(72, 17)
        Me.Cypress2.TabIndex = 12
        Me.Cypress2.Text = "Cypress 2"
        Me.Cypress2.UseVisualStyleBackColor = True
        '
        'Cypress1
        '
        Me.Cypress1.AutoSize = True
        Me.Cypress1.Location = New System.Drawing.Point(128, 137)
        Me.Cypress1.Name = "Cypress1"
        Me.Cypress1.Size = New System.Drawing.Size(72, 17)
        Me.Cypress1.TabIndex = 11
        Me.Cypress1.Text = "Cypress 1"
        Me.Cypress1.UseVisualStyleBackColor = True
        '
        'Palm2
        '
        Me.Palm2.AutoSize = True
        Me.Palm2.Location = New System.Drawing.Point(235, 67)
        Me.Palm2.Name = "Palm2"
        Me.Palm2.Size = New System.Drawing.Size(58, 17)
        Me.Palm2.TabIndex = 8
        Me.Palm2.Text = "Palm 2"
        Me.Palm2.UseVisualStyleBackColor = True
        '
        'Palm1
        '
        Me.Palm1.AutoSize = True
        Me.Palm1.Location = New System.Drawing.Point(235, 44)
        Me.Palm1.Name = "Palm1"
        Me.Palm1.Size = New System.Drawing.Size(58, 17)
        Me.Palm1.TabIndex = 7
        Me.Palm1.Text = "Palm 1"
        Me.Palm1.UseVisualStyleBackColor = True
        '
        'TropicalBush2
        '
        Me.TropicalBush2.AutoSize = True
        Me.TropicalBush2.Location = New System.Drawing.Point(235, 114)
        Me.TropicalBush2.Name = "TropicalBush2"
        Me.TropicalBush2.Size = New System.Drawing.Size(100, 17)
        Me.TropicalBush2.TabIndex = 6
        Me.TropicalBush2.Text = "Tropical Bush 2"
        Me.TropicalBush2.UseVisualStyleBackColor = True
        '
        'TropicalBush1
        '
        Me.TropicalBush1.AutoSize = True
        Me.TropicalBush1.Location = New System.Drawing.Point(235, 91)
        Me.TropicalBush1.Name = "TropicalBush1"
        Me.TropicalBush1.Size = New System.Drawing.Size(100, 17)
        Me.TropicalBush1.TabIndex = 3
        Me.TropicalBush1.Text = "Tropical Bush 1"
        Me.TropicalBush1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.NoImage
        Me.PictureBox1.Location = New System.Drawing.Point(362, 20)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(280, 264)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'Oak
        '
        Me.Oak.AutoSize = True
        Me.Oak.Location = New System.Drawing.Point(19, 160)
        Me.Oak.Name = "Oak"
        Me.Oak.Size = New System.Drawing.Size(46, 17)
        Me.Oak.TabIndex = 2
        Me.Oak.Text = "Oak"
        Me.Oak.UseVisualStyleBackColor = True
        '
        'Pine2
        '
        Me.Pine2.AutoSize = True
        Me.Pine2.Location = New System.Drawing.Point(19, 137)
        Me.Pine2.Name = "Pine2"
        Me.Pine2.Size = New System.Drawing.Size(56, 17)
        Me.Pine2.TabIndex = 1
        Me.Pine2.Text = "Pine 2"
        Me.Pine2.UseVisualStyleBackColor = True
        '
        'Pine1
        '
        Me.Pine1.AutoSize = True
        Me.Pine1.Location = New System.Drawing.Point(19, 113)
        Me.Pine1.Name = "Pine1"
        Me.Pine1.Size = New System.Drawing.Size(56, 17)
        Me.Pine1.TabIndex = 0
        Me.Pine1.Text = "Pine 1"
        Me.Pine1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Water)
        Me.GroupBox2.Controls.Add(Me.TerrainPic)
        Me.GroupBox2.Controls.Add(Me.AI)
        Me.GroupBox2.Controls.Add(Me.Flat)
        Me.GroupBox2.Controls.Add(Me.Rand)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 36)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(279, 301)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Terrain"
        '
        'Water
        '
        Me.Water.AutoSize = True
        Me.Water.Location = New System.Drawing.Point(138, 44)
        Me.Water.Name = "Water"
        Me.Water.Size = New System.Drawing.Size(54, 17)
        Me.Water.TabIndex = 7
        Me.Water.TabStop = True
        Me.Water.Text = "Water"
        Me.Water.UseVisualStyleBackColor = True
        '
        'TerrainPic
        '
        Me.TerrainPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TerrainPic.Image = Global.Outworldz.My.Resources.Resources.AI
        Me.TerrainPic.Location = New System.Drawing.Point(23, 68)
        Me.TerrainPic.Name = "TerrainPic"
        Me.TerrainPic.Size = New System.Drawing.Size(229, 204)
        Me.TerrainPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.TerrainPic.TabIndex = 6
        Me.TerrainPic.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem2, Me.TerrainToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(977, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem2
        '
        Me.HelpToolStripMenuItem2.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem2.Name = "HelpToolStripMenuItem2"
        Me.HelpToolStripMenuItem2.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem2.Text = "Help"
        '
        'TerrainToolStripMenuItem1
        '
        Me.TerrainToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadTerrainToolStripMenuItem, Me.SaveTerrainToolStripMenuItem1, Me.SaveAllTerrainsToolStripMenuItem1, Me.RegeenerateTerrainsToolStripMenuItem})
        Me.TerrainToolStripMenuItem1.Name = "TerrainToolStripMenuItem1"
        Me.TerrainToolStripMenuItem1.Size = New System.Drawing.Size(54, 20)
        Me.TerrainToolStripMenuItem1.Text = "Terrain"
        '
        'LoadTerrainToolStripMenuItem
        '
        Me.LoadTerrainToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.earth_view
        Me.LoadTerrainToolStripMenuItem.Name = "LoadTerrainToolStripMenuItem"
        Me.LoadTerrainToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.LoadTerrainToolStripMenuItem.Text = "Load Terrain"
        '
        'SaveTerrainToolStripMenuItem1
        '
        Me.SaveTerrainToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.disk_yellow
        Me.SaveTerrainToolStripMenuItem1.Name = "SaveTerrainToolStripMenuItem1"
        Me.SaveTerrainToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.SaveTerrainToolStripMenuItem1.Text = "Save Terrain"
        '
        'SaveAllTerrainsToolStripMenuItem1
        '
        Me.SaveAllTerrainsToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.SaveAllTerrainsToolStripMenuItem1.Name = "SaveAllTerrainsToolStripMenuItem1"
        Me.SaveAllTerrainsToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.SaveAllTerrainsToolStripMenuItem1.Text = "Save All Terrains"
        '
        'RegeenerateTerrainsToolStripMenuItem
        '
        Me.RegeenerateTerrainsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.pictures
        Me.RegeenerateTerrainsToolStripMenuItem.Name = "RegeenerateTerrainsToolStripMenuItem"
        Me.RegeenerateTerrainsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RegeenerateTerrainsToolStripMenuItem.Text = "Regenerate Terrains"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'TerrainsToolStripMenuItem
        '
        Me.TerrainsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.map
        Me.TerrainsToolStripMenuItem.Name = "TerrainsToolStripMenuItem"
        Me.TerrainsToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.TerrainsToolStripMenuItem.Text = "Terrains"
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_tall
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        Me.LoadToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.LoadToolStripMenuItem.Text = "Load a Region with a Terrain"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_yellow
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.SaveToolStripMenuItem.Text = "Save a Region's Terrain"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.pictures
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh All Terrain Files"
        '
        'SaveAllRunningRegioonTerrainsToolStripMenuItem
        '
        Me.SaveAllRunningRegioonTerrainsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.SaveAllRunningRegioonTerrainsToolStripMenuItem.Name = "SaveAllRunningRegioonTerrainsToolStripMenuItem"
        Me.SaveAllRunningRegioonTerrainsToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.SaveAllRunningRegioonTerrainsToolStripMenuItem.Text = "Save Running Region Terrains"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'TerrainToolStripMenuItem
        '
        Me.TerrainToolStripMenuItem.Name = "TerrainToolStripMenuItem"
        Me.TerrainToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.TerrainToolStripMenuItem.Text = "Terrain"
        '
        'LoadTTerrainToolStripMenuItem
        '
        Me.LoadTTerrainToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.picture_empty
        Me.LoadTTerrainToolStripMenuItem.Name = "LoadTTerrainToolStripMenuItem"
        Me.LoadTTerrainToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.LoadTTerrainToolStripMenuItem.Text = "Load Terrain"
        '
        'SaveTerrainToolStripMenuItem
        '
        Me.SaveTerrainToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.SaveTerrainToolStripMenuItem.Name = "SaveTerrainToolStripMenuItem"
        Me.SaveTerrainToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SaveTerrainToolStripMenuItem.Text = "Save Terrain"
        '
        'RebuildAllTerrainsToolStripMenuItem
        '
        Me.RebuildAllTerrainsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
        Me.RebuildAllTerrainsToolStripMenuItem.Name = "RebuildAllTerrainsToolStripMenuItem"
        Me.RebuildAllTerrainsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RebuildAllTerrainsToolStripMenuItem.Text = "Rebuild All Terrains"
        '
        'SaveAllTerrainsToolStripMenuItem
        '
        Me.SaveAllTerrainsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.SaveAllTerrainsToolStripMenuItem.Name = "SaveAllTerrainsToolStripMenuItem"
        Me.SaveAllTerrainsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SaveAllTerrainsToolStripMenuItem.Text = "Save All Terrains"
        '
        'FormTrees
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(977, 349)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "FormTrees"
        Me.Text = "Landcaping"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.TerrainPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Flat As RadioButton
    Friend WithEvents Rand As RadioButton
    Friend WithEvents AI As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TropicalBush1 As CheckBox
    Friend WithEvents Oak As CheckBox
    Friend WithEvents Pine2 As CheckBox
    Friend WithEvents Pine1 As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TerrainPic As PictureBox
    Friend WithEvents Eucalyptus As CheckBox
    Friend WithEvents WinterAspen As CheckBox
    Friend WithEvents WinterPine2 As CheckBox
    Friend WithEvents WinterPine1 As CheckBox
    Friend WithEvents Plumeria As CheckBox
    Friend WithEvents Cypress2 As CheckBox
    Friend WithEvents Cypress1 As CheckBox
    Friend WithEvents Palm2 As CheckBox
    Friend WithEvents Palm1 As CheckBox
    Friend WithEvents TropicalBush2 As CheckBox
    Friend WithEvents Undergrowth As CheckBox
    Friend WithEvents Grass4 As CheckBox
    Friend WithEvents Grass3 As CheckBox
    Friend WithEvents Grass2 As CheckBox
    Friend WithEvents Grass1 As CheckBox
    Friend WithEvents Grass0 As CheckBox
    Friend WithEvents BeachGrass As CheckBox
    Friend WithEvents Kelp2 As CheckBox
    Friend WithEvents Kelp1 As CheckBox
    Friend WithEvents SeaSword As CheckBox
    Friend WithEvents Eelgrass As CheckBox
    Friend WithEvents Fern As CheckBox
    Friend WithEvents Water As RadioButton
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ApplyButtton As Button
    Friend WithEvents None As CheckBox
    Friend WithEvents All As CheckBox
    Friend WithEvents RevertButton As Button
    Friend WithEvents TerrainsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FreezeButton As Button
    Friend WithEvents LoadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAllRunningRegioonTerrainsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents TerrainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadTTerrainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAllTerrainsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveTerrainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RebuildAllTerrainsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents TerrainToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents LoadTerrainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveTerrainToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SaveAllTerrainsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents RegeenerateTerrainsToolStripMenuItem As ToolStripMenuItem
End Class
