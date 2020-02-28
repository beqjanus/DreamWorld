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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Clouds = New System.Windows.Forms.CheckBox()
        Me.OutBoundPermissionsCheckbox = New System.Windows.Forms.CheckBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.DomainUpDown1 = New System.Windows.Forms.DomainUpDown()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4.SuspendLayout()
        CType(Me.GodHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LimitsBox.SuspendLayout()
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
        Me.GroupBox4.Location = New System.Drawing.Point(12, 31)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(231, 118)
        Me.GroupBox4.TabIndex = 49
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = Global.Outworldz.My.Resources.Resources.Permissions_word
        '
        'AllowGods
        '
        Me.AllowGods.AutoSize = True
        Me.AllowGods.Location = New System.Drawing.Point(15, 35)
        Me.AllowGods.Name = "AllowGods"
        Me.AllowGods.Size = New System.Drawing.Size(133, 17)
        Me.AllowGods.TabIndex = 14
        Me.AllowGods.Text = Global.Outworldz.My.Resources.Resources.Allow_Or_Disallow_Gods_word
        Me.ToolTip1.SetToolTip(Me.AllowGods, Global.Outworldz.My.Resources.Resources.AllowGodsTooltip)
        Me.AllowGods.UseVisualStyleBackColor = True
        '
        'GodHelp
        '
        Me.GodHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.GodHelp.Location = New System.Drawing.Point(77, 6)
        Me.GodHelp.Name = "GodHelp"
        Me.GodHelp.Size = New System.Drawing.Size(27, 23)
        Me.GodHelp.TabIndex = 1857
        Me.GodHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GodHelp, Global.Outworldz.My.Resources.Resources.Help_Godmodes)
        '
        'ManagerGod
        '
        Me.ManagerGod.AutoSize = True
        Me.ManagerGod.Location = New System.Drawing.Point(15, 82)
        Me.ManagerGod.Name = "ManagerGod"
        Me.ManagerGod.Size = New System.Drawing.Size(141, 17)
        Me.ManagerGod.TabIndex = 16
        Me.ManagerGod.Text = Global.Outworldz.My.Resources.Resources.Region_manager_god
        Me.ToolTip1.SetToolTip(Me.ManagerGod, Global.Outworldz.My.Resources.Resources.Region_Manager_is_God)
        Me.ManagerGod.UseVisualStyleBackColor = True
        '
        'RegionGod
        '
        Me.RegionGod.AutoSize = True
        Me.RegionGod.Location = New System.Drawing.Point(15, 58)
        Me.RegionGod.Name = "RegionGod"
        Me.RegionGod.Size = New System.Drawing.Size(146, 17)
        Me.RegionGod.TabIndex = 15
        Me.RegionGod.Text = Global.Outworldz.My.Resources.Resources.Allow_Region_Owner_Gods_word
        Me.ToolTip1.SetToolTip(Me.RegionGod, Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word)
        Me.RegionGod.UseVisualStyleBackColor = True
        '
        'LimitsBox
        '
        Me.LimitsBox.Controls.Add(Me.EnableMaxPrims)
        Me.LimitsBox.Location = New System.Drawing.Point(9, 155)
        Me.LimitsBox.Name = "LimitsBox"
        Me.LimitsBox.Size = New System.Drawing.Size(233, 49)
        Me.LimitsBox.TabIndex = 1861
        Me.LimitsBox.TabStop = False
        Me.LimitsBox.Text = Global.Outworldz.My.Resources.Resources.Prim_Limits
        '
        'EnableMaxPrims
        '
        Me.EnableMaxPrims.AutoSize = True
        Me.EnableMaxPrims.Location = New System.Drawing.Point(12, 19)
        Me.EnableMaxPrims.Name = "EnableMaxPrims"
        Me.EnableMaxPrims.Size = New System.Drawing.Size(144, 17)
        Me.EnableMaxPrims.TabIndex = 19
        Me.EnableMaxPrims.Text = Global.Outworldz.My.Resources.Resources.Max_Prims
        Me.ToolTip1.SetToolTip(Me.EnableMaxPrims, Global.Outworldz.My.Resources.Resources.Max_PrimLimit)
        Me.EnableMaxPrims.UseVisualStyleBackColor = True
        '
        'Clouds
        '
        Me.Clouds.AutoSize = True
        Me.Clouds.Location = New System.Drawing.Point(12, 19)
        Me.Clouds.Name = "Clouds"
        Me.Clouds.Size = New System.Drawing.Size(59, 17)
        Me.Clouds.TabIndex = 17
        Me.Clouds.Text = "Enable"
        Me.ToolTip1.SetToolTip(Me.Clouds, Global.Outworldz.My.Resources.Resources.Allow_cloud)
        Me.Clouds.UseVisualStyleBackColor = True
        '
        'OutBoundPermissionsCheckbox
        '
        Me.OutBoundPermissionsCheckbox.AutoSize = True
        Me.OutBoundPermissionsCheckbox.Location = New System.Drawing.Point(15, 19)
        Me.OutBoundPermissionsCheckbox.Name = "OutBoundPermissionsCheckbox"
        Me.OutBoundPermissionsCheckbox.Size = New System.Drawing.Size(122, 17)
        Me.OutBoundPermissionsCheckbox.TabIndex = 18
        Me.OutBoundPermissionsCheckbox.Text = Global.Outworldz.My.Resources.Resources.Allow_Items_to_leave_word
        Me.ToolTip1.SetToolTip(Me.OutBoundPermissionsCheckbox, Global.Outworldz.My.Resources.Resources.Allow_objects)
        Me.OutBoundPermissionsCheckbox.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.DomainUpDown1)
        Me.GroupBox7.Controls.Add(Me.Clouds)
        Me.GroupBox7.Location = New System.Drawing.Point(12, 210)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(226, 56)
        Me.GroupBox7.TabIndex = 1863
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = Global.Outworldz.My.Resources.Resources.Clouds_word
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
        Me.DomainUpDown1.Location = New System.Drawing.Point(77, 18)
        Me.DomainUpDown1.Name = "DomainUpDown1"
        Me.DomainUpDown1.Size = New System.Drawing.Size(66, 20)
        Me.DomainUpDown1.TabIndex = 18
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(6, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(261, 26)
        Me.MenuStrip2.TabIndex = 1889
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(64, 24)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.OutBoundPermissionsCheckbox)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 272)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(231, 45)
        Me.GroupBox1.TabIndex = 1864
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Export_Permission_word
        '
        'FormPermissions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(261, 327)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.LimitsBox)
        Me.Controls.Add(Me.GroupBox4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormPermissions"
        Me.Text = Global.Outworldz.My.Resources.Resources.Permissions_word
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.GodHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LimitsBox.ResumeLayout(False)
        Me.LimitsBox.PerformLayout()
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
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents DomainUpDown1 As DomainUpDown
    Friend WithEvents Clouds As CheckBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents OutBoundPermissionsCheckbox As CheckBox
End Class
