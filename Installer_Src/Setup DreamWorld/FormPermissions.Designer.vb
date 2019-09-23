<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPermissions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPermissions))
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.AllowGods = New System.Windows.Forms.CheckBox()
        Me.GodHelp = New System.Windows.Forms.PictureBox()
        Me.ManagerGod = New System.Windows.Forms.CheckBox()
        Me.RegionGod = New System.Windows.Forms.CheckBox()
        Me.LimitsBox = New System.Windows.Forms.GroupBox()
        Me.EnableMaxPrims = New System.Windows.Forms.CheckBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.LSLCheckbox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Clouds = New System.Windows.Forms.CheckBox()
        Me.OutBoundPermissionsCheckbox = New System.Windows.Forms.CheckBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.DomainUpDown1 = New System.Windows.Forms.DomainUpDown()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4.SuspendLayout()
        CType(Me.GodHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LimitsBox.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.AllowGods)
        Me.GroupBox4.Controls.Add(Me.GodHelp)
        Me.GroupBox4.Controls.Add(Me.ManagerGod)
        Me.GroupBox4.Controls.Add(Me.RegionGod)
        Me.GroupBox4.Location = New System.Drawing.Point(16, 38)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Size = New System.Drawing.Size(248, 146)
        Me.GroupBox4.TabIndex = 49
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Permissions"
        '
        'AllowGods
        '
        Me.AllowGods.AutoSize = True
        Me.AllowGods.Location = New System.Drawing.Point(20, 43)
        Me.AllowGods.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AllowGods.Name = "AllowGods"
        Me.AllowGods.Size = New System.Drawing.Size(179, 21)
        Me.AllowGods.TabIndex = 14
        Me.AllowGods.Text = "Allow 200+ Level gods?"
        Me.ToolTip1.SetToolTip(Me.AllowGods, resources.GetString("AllowGods.ToolTip"))
        Me.AllowGods.UseVisualStyleBackColor = True
        '
        'GodHelp
        '
        Me.GodHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.GodHelp.Location = New System.Drawing.Point(200, 22)
        Me.GodHelp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GodHelp.Name = "GodHelp"
        Me.GodHelp.Size = New System.Drawing.Size(40, 42)
        Me.GodHelp.TabIndex = 1857
        Me.GodHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GodHelp, "Click for Help on God Modes")
        '
        'ManagerGod
        '
        Me.ManagerGod.AutoSize = True
        Me.ManagerGod.Location = New System.Drawing.Point(20, 101)
        Me.ManagerGod.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ManagerGod.Name = "ManagerGod"
        Me.ManagerGod.Size = New System.Drawing.Size(185, 21)
        Me.ManagerGod.TabIndex = 16
        Me.ManagerGod.Text = "Region manager is god?"
        Me.ToolTip1.SetToolTip(Me.ManagerGod, "Region Manager is God - Estate managers can become gods, but just for this estate" &
        ".")
        Me.ManagerGod.UseVisualStyleBackColor = True
        '
        'RegionGod
        '
        Me.RegionGod.AutoSize = True
        Me.RegionGod.Location = New System.Drawing.Point(20, 71)
        Me.RegionGod.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RegionGod.Name = "RegionGod"
        Me.RegionGod.Size = New System.Drawing.Size(200, 21)
        Me.RegionGod.TabIndex = 15
        Me.RegionGod.Text = "Allow Region owner gods? "
        Me.ToolTip1.SetToolTip(Me.RegionGod, " Region Owner is God - When you first create a region, you are prompted for the o" &
        "wner name. If checked, this person has God mode rights to any regions they own.")
        Me.RegionGod.UseVisualStyleBackColor = True
        '
        'LimitsBox
        '
        Me.LimitsBox.Controls.Add(Me.EnableMaxPrims)
        Me.LimitsBox.Location = New System.Drawing.Point(12, 190)
        Me.LimitsBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LimitsBox.Name = "LimitsBox"
        Me.LimitsBox.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LimitsBox.Size = New System.Drawing.Size(252, 60)
        Me.LimitsBox.TabIndex = 1861
        Me.LimitsBox.TabStop = False
        Me.LimitsBox.Text = "Prim Limits"
        '
        'EnableMaxPrims
        '
        Me.EnableMaxPrims.AutoSize = True
        Me.EnableMaxPrims.Location = New System.Drawing.Point(16, 23)
        Me.EnableMaxPrims.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.EnableMaxPrims.Name = "EnableMaxPrims"
        Me.EnableMaxPrims.Size = New System.Drawing.Size(184, 21)
        Me.EnableMaxPrims.TabIndex = 19
        Me.EnableMaxPrims.Text = "Enable MAXPRIMS limits"
        Me.ToolTip1.SetToolTip(Me.EnableMaxPrims, "Maxprims limits the number of prims on a parcel.  if disabled, there are no limit" &
        "s.")
        Me.EnableMaxPrims.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.LSLCheckbox)
        Me.GroupBox8.Location = New System.Drawing.Point(16, 257)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox8.Size = New System.Drawing.Size(252, 59)
        Me.GroupBox8.TabIndex = 1862
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Allow LSL to contact the server"
        '
        'LSLCheckbox
        '
        Me.LSLCheckbox.AutoSize = True
        Me.LSLCheckbox.Location = New System.Drawing.Point(16, 23)
        Me.LSLCheckbox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LSLCheckbox.Name = "LSLCheckbox"
        Me.LSLCheckbox.Size = New System.Drawing.Size(74, 21)
        Me.LSLCheckbox.TabIndex = 210
        Me.LSLCheckbox.Text = "Enable"
        Me.ToolTip1.SetToolTip(Me.LSLCheckbox, "LSL scripts cannot reach your server by default.   If you turn this on, they can " &
        "read web pages on your PC. ")
        Me.LSLCheckbox.UseVisualStyleBackColor = True
        '
        'Clouds
        '
        Me.Clouds.AutoSize = True
        Me.Clouds.Location = New System.Drawing.Point(16, 23)
        Me.Clouds.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Clouds.Name = "Clouds"
        Me.Clouds.Size = New System.Drawing.Size(74, 21)
        Me.Clouds.TabIndex = 17
        Me.Clouds.Text = "Enable"
        Me.ToolTip1.SetToolTip(Me.Clouds, "The original Second Life clouds")
        Me.Clouds.UseVisualStyleBackColor = True
        '
        'OutBoundPermissionsCheckbox
        '
        Me.OutBoundPermissionsCheckbox.AutoSize = True
        Me.OutBoundPermissionsCheckbox.Location = New System.Drawing.Point(20, 23)
        Me.OutBoundPermissionsCheckbox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OutBoundPermissionsCheckbox.Name = "OutBoundPermissionsCheckbox"
        Me.OutBoundPermissionsCheckbox.Size = New System.Drawing.Size(208, 21)
        Me.OutBoundPermissionsCheckbox.TabIndex = 18
        Me.OutBoundPermissionsCheckbox.Text = "Allow items to leave via HG?"
        Me.ToolTip1.SetToolTip(Me.OutBoundPermissionsCheckbox, "Objects can be taken from your grid via Hypergrid.")
        Me.OutBoundPermissionsCheckbox.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.DomainUpDown1)
        Me.GroupBox7.Controls.Add(Me.Clouds)
        Me.GroupBox7.Location = New System.Drawing.Point(16, 324)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox7.Size = New System.Drawing.Size(252, 69)
        Me.GroupBox7.TabIndex = 1863
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Clouds"
        '
        'DomainUpDown1
        '
        Me.DomainUpDown1.Items.Add("0%")
        Me.DomainUpDown1.Items.Add("10%")
        Me.DomainUpDown1.Items.Add("20%")
        Me.DomainUpDown1.Items.Add("30%")
        Me.DomainUpDown1.Items.Add("60%")
        Me.DomainUpDown1.Items.Add("40%")
        Me.DomainUpDown1.Items.Add("50%")
        Me.DomainUpDown1.Items.Add("60%")
        Me.DomainUpDown1.Items.Add("70%")
        Me.DomainUpDown1.Items.Add("80%")
        Me.DomainUpDown1.Items.Add("90%")
        Me.DomainUpDown1.Items.Add("100%")
        Me.DomainUpDown1.Location = New System.Drawing.Point(103, 22)
        Me.DomainUpDown1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DomainUpDown1.Name = "DomainUpDown1"
        Me.DomainUpDown1.Size = New System.Drawing.Size(88, 22)
        Me.DomainUpDown1.TabIndex = 18
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuStrip2.Size = New System.Drawing.Size(311, 28)
        Me.MenuStrip2.TabIndex = 1889
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(73, 24)
        Me.ToolStripMenuItem30.Text = "Help"
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(116, 26)
        Me.DatabaseSetupToolStripMenuItem.Text = "Help"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.OutBoundPermissionsCheckbox)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 400)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(252, 73)
        Me.GroupBox1.TabIndex = 1864
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Export Permissions"
        '
        'FormPermissions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(311, 481)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.LimitsBox)
        Me.Controls.Add(Me.GroupBox4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "FormPermissions"
        Me.Text = "Permissions"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.GodHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LimitsBox.ResumeLayout(False)
        Me.LimitsBox.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents AllowGods As CheckBox
    Friend WithEvents GodHelp As PictureBox
    Friend WithEvents ManagerGod As CheckBox
    Friend WithEvents RegionGod As CheckBox
    Friend WithEvents LimitsBox As GroupBox
    Friend WithEvents EnableMaxPrims As CheckBox
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents LSLCheckbox As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents DomainUpDown1 As DomainUpDown
    Friend WithEvents Clouds As CheckBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents OutBoundPermissionsCheckbox As CheckBox
End Class
