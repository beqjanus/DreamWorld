<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRegions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRegions))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.NormalizeButton1 = New System.Windows.Forms.Button()
        Me.Z = New System.Windows.Forms.TextBox()
        Me.Y = New System.Windows.Forms.TextBox()
        Me.X = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RegionButton = New System.Windows.Forms.Button()
        Me.RegionBox = New System.Windows.Forms.ComboBox()
        Me.RegionHelp = New System.Windows.Forms.PictureBox()
        Me.WelcomeRegion = New System.Windows.Forms.Label()
        Me.WelcomeBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AddRegion = New System.Windows.Forms.Button()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SmartStartEnabled = New System.Windows.Forms.CheckBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.RegionHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.SmartStartEnabled)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.NormalizeButton1)
        Me.GroupBox2.Controls.Add(Me.Z)
        Me.GroupBox2.Controls.Add(Me.Y)
        Me.GroupBox2.Controls.Add(Me.X)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.RegionButton)
        Me.GroupBox2.Controls.Add(Me.RegionBox)
        Me.GroupBox2.Controls.Add(Me.RegionHelp)
        Me.GroupBox2.Controls.Add(Me.WelcomeRegion)
        Me.GroupBox2.Controls.Add(Me.WelcomeBox1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.AddRegion)
        Me.GroupBox2.Location = New System.Drawing.Point(17, 49)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(176, 340)
        Me.GroupBox2.TabIndex = 1862
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Regions"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(8, 300)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(148, 23)
        Me.Button1.TabIndex = 1866
        Me.Button1.Text = "Clear All Registrations"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'NormalizeButton1
        '
        Me.NormalizeButton1.Location = New System.Drawing.Point(3, 181)
        Me.NormalizeButton1.Name = "NormalizeButton1"
        Me.NormalizeButton1.Size = New System.Drawing.Size(148, 23)
        Me.NormalizeButton1.TabIndex = 1865
        Me.NormalizeButton1.Text = "Normalize Regions"
        Me.NormalizeButton1.UseVisualStyleBackColor = True
        '
        'Z
        '
        Me.Z.Location = New System.Drawing.Point(80, 115)
        Me.Z.Name = "Z"
        Me.Z.Size = New System.Drawing.Size(30, 20)
        Me.Z.TabIndex = 1864
        '
        'Y
        '
        Me.Y.Location = New System.Drawing.Point(44, 115)
        Me.Y.Name = "Y"
        Me.Y.Size = New System.Drawing.Size(30, 20)
        Me.Y.TabIndex = 1863
        '
        'X
        '
        Me.X.Location = New System.Drawing.Point(8, 115)
        Me.X.Name = "X"
        Me.X.Size = New System.Drawing.Size(30, 20)
        Me.X.TabIndex = 1862
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 1861
        Me.Label2.Text = "New User Home X,Y,Z"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 219)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 1860
        Me.Label1.Text = "Edit Region:"
        '
        'RegionButton
        '
        Me.RegionButton.Location = New System.Drawing.Point(5, 271)
        Me.RegionButton.Name = "RegionButton"
        Me.RegionButton.Size = New System.Drawing.Size(148, 23)
        Me.RegionButton.TabIndex = 4
        Me.RegionButton.Text = "Configure All Regions"
        Me.RegionButton.UseVisualStyleBackColor = True
        '
        'RegionBox
        '
        Me.RegionBox.AutoCompleteCustomSource.AddRange(New String() {"1 Hour", "4 Hour", "12 Hour", "Daily", "Weekly"})
        Me.RegionBox.FormattingEnabled = True
        Me.RegionBox.Items.AddRange(New Object() {"Choose a region"})
        Me.RegionBox.Location = New System.Drawing.Point(3, 244)
        Me.RegionBox.MaxDropDownItems = 15
        Me.RegionBox.Name = "RegionBox"
        Me.RegionBox.Size = New System.Drawing.Size(148, 21)
        Me.RegionBox.Sorted = True
        Me.RegionBox.TabIndex = 3
        '
        'RegionHelp
        '
        Me.RegionHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.RegionHelp.Location = New System.Drawing.Point(135, 8)
        Me.RegionHelp.Name = "RegionHelp"
        Me.RegionHelp.Size = New System.Drawing.Size(28, 27)
        Me.RegionHelp.TabIndex = 1858
        Me.RegionHelp.TabStop = False
        '
        'WelcomeRegion
        '
        Me.WelcomeRegion.AutoSize = True
        Me.WelcomeRegion.Location = New System.Drawing.Point(6, 22)
        Me.WelcomeRegion.Name = "WelcomeRegion"
        Me.WelcomeRegion.Size = New System.Drawing.Size(123, 13)
        Me.WelcomeRegion.TabIndex = 32
        Me.WelcomeRegion.Text = "Default region for visitors"
        '
        'WelcomeBox1
        '
        Me.WelcomeBox1.AutoCompleteCustomSource.AddRange(New String() {"1 Hour", "4 Hour", "12 Hour", "Daily", "Weekly"})
        Me.WelcomeBox1.FormattingEnabled = True
        Me.WelcomeBox1.Items.AddRange(New Object() {"Hourly", "6 hour", "12 Hour", "Daily", "2 days", "3 days", "4 days", "5 days", "6 days", "Weekly"})
        Me.WelcomeBox1.Location = New System.Drawing.Point(4, 38)
        Me.WelcomeBox1.Name = "WelcomeBox1"
        Me.WelcomeBox1.Size = New System.Drawing.Size(148, 21)
        Me.WelcomeBox1.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 28
        '
        'AddRegion
        '
        Me.AddRegion.Location = New System.Drawing.Point(3, 152)
        Me.AddRegion.Name = "AddRegion"
        Me.AddRegion.Size = New System.Drawing.Size(148, 23)
        Me.AddRegion.TabIndex = 2
        Me.AddRegion.Text = "Add Region"
        Me.AddRegion.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(205, 28)
        Me.MenuStrip2.TabIndex = 1887
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(64, 24)
        Me.ToolStripMenuItem30.Text = My.Resources.Help
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.DatabaseSetupToolStripMenuItem.Text = My.Resources.Help
        '
        'SmartStartEnabled
        '
        Me.SmartStartEnabled.AutoSize = True
        Me.SmartStartEnabled.Location = New System.Drawing.Point(6, 76)
        Me.SmartStartEnabled.Name = "SmartStartEnabled"
        Me.SmartStartEnabled.Size = New System.Drawing.Size(120, 17)
        Me.SmartStartEnabled.TabIndex = 1867
        Me.SmartStartEnabled.Text = "Smart Start Enabled"
        Me.SmartStartEnabled.UseVisualStyleBackColor = True
        '
        'FormRegions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(205, 390)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormRegions"
        Me.Text = "Region"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.RegionHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RegionHelp As PictureBox
    Friend WithEvents WelcomeRegion As Label
    Friend WithEvents WelcomeBox1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents AddRegion As Button
    Friend WithEvents RegionButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents RegionBox As ComboBox
    Friend WithEvents Z As TextBox
    Friend WithEvents Y As TextBox
    Friend WithEvents X As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NormalizeButton1 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SmartStartEnabled As CheckBox
End Class
