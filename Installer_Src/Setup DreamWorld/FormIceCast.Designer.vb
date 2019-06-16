<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Icecast
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Icecast))
        Me.ShoutcastEnable = New System.Windows.Forms.CheckBox()
        Me.ShoutcastPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ShoutcastPort1 = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LoadURL = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AdminPassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ShoutcastPort = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ShoutcastEnable
        '
        Me.ShoutcastEnable.AutoSize = True
        Me.ShoutcastEnable.Location = New System.Drawing.Point(31, 29)
        Me.ShoutcastEnable.Name = "ShoutcastEnable"
        Me.ShoutcastEnable.Size = New System.Drawing.Size(59, 17)
        Me.ShoutcastEnable.TabIndex = 0
        Me.ShoutcastEnable.Text = "Enable"
        Me.ShoutcastEnable.UseVisualStyleBackColor = True
        '
        'ShoutcastPassword
        '
        Me.ShoutcastPassword.Location = New System.Drawing.Point(86, 132)
        Me.ShoutcastPassword.Name = "ShoutcastPassword"
        Me.ShoutcastPassword.Size = New System.Drawing.Size(100, 20)
        Me.ShoutcastPassword.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Password"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.ShoutcastPort1)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.LoadURL)
        Me.GroupBox1.Controls.Add(Me.ShoutcastEnable)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.AdminPassword)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ShoutcastPort)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ShoutcastPassword)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(214, 201)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Icecast Server"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 1861
        Me.Label4.Text = "Port2"
        '
        'ShoutcastPort1
        '
        Me.ShoutcastPort1.Location = New System.Drawing.Point(86, 86)
        Me.ShoutcastPort1.Name = "ShoutcastPort1"
        Me.ShoutcastPort1.Size = New System.Drawing.Size(52, 20)
        Me.ShoutcastPort1.TabIndex = 3
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox1.Location = New System.Drawing.Point(98, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(40, 27)
        Me.PictureBox1.TabIndex = 1859
        Me.PictureBox1.TabStop = False
        '
        'LoadURL
        '
        Me.LoadURL.Location = New System.Drawing.Point(35, 167)
        Me.LoadURL.Margin = New System.Windows.Forms.Padding(2)
        Me.LoadURL.Name = "LoadURL"
        Me.LoadURL.Size = New System.Drawing.Size(150, 29)
        Me.LoadURL.TabIndex = 6
        Me.LoadURL.Text = "Admin Web Page"
        Me.LoadURL.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(-4, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Admin Password"
        '
        'AdminPassword
        '
        Me.AdminPassword.Location = New System.Drawing.Point(86, 107)
        Me.AdminPassword.Name = "AdminPassword"
        Me.AdminPassword.Size = New System.Drawing.Size(100, 20)
        Me.AdminPassword.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Port1"
        '
        'ShoutcastPort
        '
        Me.ShoutcastPort.Location = New System.Drawing.Point(86, 64)
        Me.ShoutcastPort.Name = "ShoutcastPort"
        Me.ShoutcastPort.Size = New System.Drawing.Size(52, 20)
        Me.ShoutcastPort.TabIndex = 2
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'Icecast
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(238, 222)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Icecast"
        Me.Text = "IceCast"
        Me.ToolTip1.SetToolTip(Me, "Click for help on setting up Shoutcast")
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ShoutcastPort1 As TextBox
End Class
