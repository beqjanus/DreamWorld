<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormIcecast
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormIcecast))
        Me.ShoutcastEnable = New System.Windows.Forms.CheckBox()
        Me.ShoutcastPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ShoutcastPort1 = New System.Windows.Forms.TextBox()
        Me.LoadURL = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AdminPassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ShoutcastPort = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ShoutcastEnable
        '
        Me.ShoutcastEnable.AutoSize = True
        Me.ShoutcastEnable.Location = New System.Drawing.Point(26, 43)
        Me.ShoutcastEnable.Margin = New System.Windows.Forms.Padding(4)
        Me.ShoutcastEnable.Name = "ShoutcastEnable"
        Me.ShoutcastEnable.Size = New System.Drawing.Size(77, 21)
        Me.ShoutcastEnable.TabIndex = 0
        Me.ShoutcastEnable.Text = Global.Outworldz.My.Resources.Resources.Enable_word
        Me.ShoutcastEnable.UseVisualStyleBackColor = True
        '
        'ShoutcastPassword
        '
        Me.ShoutcastPassword.Location = New System.Drawing.Point(26, 179)
        Me.ShoutcastPassword.Margin = New System.Windows.Forms.Padding(4)
        Me.ShoutcastPassword.Name = "ShoutcastPassword"
        Me.ShoutcastPassword.Size = New System.Drawing.Size(172, 22)
        Me.ShoutcastPassword.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(219, 179)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Password"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.ShoutcastPort1)
        Me.GroupBox1.Controls.Add(Me.LoadURL)
        Me.GroupBox1.Controls.Add(Me.ShoutcastEnable)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.AdminPassword)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ShoutcastPort)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ShoutcastPassword)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 61)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(424, 302)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "IceCast Server"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(26, 233)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(152, 44)
        Me.Button1.TabIndex = 1862
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(110, 107)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 16)
        Me.Label4.TabIndex = 1861
        Me.Label4.Text = "Port 2"
        '
        'ShoutcastPort1
        '
        Me.ShoutcastPort1.Location = New System.Drawing.Point(26, 107)
        Me.ShoutcastPort1.Margin = New System.Windows.Forms.Padding(4)
        Me.ShoutcastPort1.Name = "ShoutcastPort1"
        Me.ShoutcastPort1.Size = New System.Drawing.Size(76, 22)
        Me.ShoutcastPort1.TabIndex = 3
        '
        'LoadURL
        '
        Me.LoadURL.Location = New System.Drawing.Point(205, 233)
        Me.LoadURL.Margin = New System.Windows.Forms.Padding(2)
        Me.LoadURL.Name = "LoadURL"
        Me.LoadURL.Size = New System.Drawing.Size(189, 44)
        Me.LoadURL.TabIndex = 6
        Me.LoadURL.Text = Global.Outworldz.My.Resources.Resources.Admin_Web_Page_word
        Me.LoadURL.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(219, 144)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Admin Password"
        '
        'AdminPassword
        '
        Me.AdminPassword.Location = New System.Drawing.Point(26, 141)
        Me.AdminPassword.Margin = New System.Windows.Forms.Padding(4)
        Me.AdminPassword.Name = "AdminPassword"
        Me.AdminPassword.Size = New System.Drawing.Size(172, 22)
        Me.AdminPassword.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(110, 75)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Port 1"
        '
        'ShoutcastPort
        '
        Me.ShoutcastPort.Location = New System.Drawing.Point(26, 75)
        Me.ShoutcastPort.Margin = New System.Windows.Forms.Padding(4)
        Me.ShoutcastPort.Name = "ShoutcastPort"
        Me.ShoutcastPort.Size = New System.Drawing.Size(76, 22)
        Me.ShoutcastPort.TabIndex = 2
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem4})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(455, 38)
        Me.MenuStrip2.TabIndex = 1891
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(93, 34)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(93, 32)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(93, 32)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(34, 28)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'HelpToolStripMenuItem2
        '
        Me.HelpToolStripMenuItem2.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem2.Name = "HelpToolStripMenuItem2"
        Me.HelpToolStripMenuItem2.Size = New System.Drawing.Size(93, 34)
        Me.HelpToolStripMenuItem2.Text = "Help"
        '
        'HelpToolStripMenuItem3
        '
        Me.HelpToolStripMenuItem3.Name = "HelpToolStripMenuItem3"
        Me.HelpToolStripMenuItem3.Size = New System.Drawing.Size(65, 29)
        Me.HelpToolStripMenuItem3.Text = "Help"
        '
        'HelpToolStripMenuItem4
        '
        Me.HelpToolStripMenuItem4.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem4.Name = "HelpToolStripMenuItem4"
        Me.HelpToolStripMenuItem4.Size = New System.Drawing.Size(93, 34)
        Me.HelpToolStripMenuItem4.Text = "Help"
        '
        'FormIcecast
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(455, 370)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormIcecast"
        Me.Text = "Icecast"
        Me.ToolTip1.SetToolTip(Me, Global.Outworldz.My.Resources.Resources.icecast_help)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ShoutcastEnable As CheckBox
    Friend WithEvents ShoutcastPassword As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ShoutcastPort As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents AdminPassword As TextBox
    Friend WithEvents LoadURL As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label4 As Label
    Friend WithEvents ShoutcastPort1 As TextBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents Button1 As Button
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents HelpToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem4 As ToolStripMenuItem
End Class
