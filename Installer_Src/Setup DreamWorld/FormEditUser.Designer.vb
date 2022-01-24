<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEditUser
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
        Me.FnameTextBox = New System.Windows.Forms.TextBox()
        Me.LastNameTextBox = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.UUIDTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AllowButton = New System.Windows.Forms.Button()
        Me.BanButton = New System.Windows.Forms.Button()
        Me.RadioGid = New System.Windows.Forms.RadioButton()
        Me.RadioDiva = New System.Windows.Forms.RadioButton()
        Me.RadioLogin = New System.Windows.Forms.RadioButton()
        Me.RadioNologin = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HyperGridCheckBox = New System.Windows.Forms.CheckBox()
        Me.LevelGroupBox = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.LevelGroupBox.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'FnameTextBox
        '
        Me.FnameTextBox.Location = New System.Drawing.Point(21, 37)
        Me.FnameTextBox.Name = "FnameTextBox"
        Me.FnameTextBox.Size = New System.Drawing.Size(137, 20)
        Me.FnameTextBox.TabIndex = 0
        '
        'LastNameTextBox
        '
        Me.LastNameTextBox.Location = New System.Drawing.Point(190, 37)
        Me.LastNameTextBox.Name = "LastNameTextBox"
        Me.LastNameTextBox.Size = New System.Drawing.Size(204, 20)
        Me.LastNameTextBox.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TitleTextBox)
        Me.GroupBox1.Controls.Add(Me.UUIDTextBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.FnameTextBox)
        Me.GroupBox1.Controls.Add(Me.LastNameTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 44)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(421, 114)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "User"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Title"
        '
        'TitleTextBox
        '
        Me.TitleTextBox.Location = New System.Drawing.Point(21, 79)
        Me.TitleTextBox.Name = "TitleTextBox"
        Me.TitleTextBox.Size = New System.Drawing.Size(152, 20)
        Me.TitleTextBox.TabIndex = 13
        '
        'UUIDTextBox
        '
        Me.UUIDTextBox.Location = New System.Drawing.Point(190, 79)
        Me.UUIDTextBox.Name = "UUIDTextBox"
        Me.UUIDTextBox.Size = New System.Drawing.Size(204, 20)
        Me.UUIDTextBox.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(187, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "UUID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "First Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(187, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Last Name"
        '
        'AllowButton
        '
        Me.AllowButton.Location = New System.Drawing.Point(20, 83)
        Me.AllowButton.Name = "AllowButton"
        Me.AllowButton.Size = New System.Drawing.Size(109, 35)
        Me.AllowButton.TabIndex = 7
        Me.AllowButton.Text = "Allow"
        Me.AllowButton.UseVisualStyleBackColor = True
        '
        'BanButton
        '
        Me.BanButton.Location = New System.Drawing.Point(20, 43)
        Me.BanButton.Name = "BanButton"
        Me.BanButton.Size = New System.Drawing.Size(109, 34)
        Me.BanButton.TabIndex = 6
        Me.BanButton.Text = "Ban"
        Me.BanButton.UseVisualStyleBackColor = True
        '
        'RadioGid
        '
        Me.RadioGid.AutoSize = True
        Me.RadioGid.Location = New System.Drawing.Point(21, 88)
        Me.RadioGid.Name = "RadioGid"
        Me.RadioGid.Size = New System.Drawing.Size(116, 17)
        Me.RadioGid.TabIndex = 5
        Me.RadioGid.TabStop = True
        Me.RadioGid.Text = "Level God Enabled"
        Me.RadioGid.UseVisualStyleBackColor = True
        '
        'RadioDiva
        '
        Me.RadioDiva.AutoSize = True
        Me.RadioDiva.Location = New System.Drawing.Point(21, 65)
        Me.RadioDiva.Name = "RadioDiva"
        Me.RadioDiva.Size = New System.Drawing.Size(100, 17)
        Me.RadioDiva.TabIndex = 4
        Me.RadioDiva.TabStop = True
        Me.RadioDiva.Text = "Diva Wifi Admin"
        Me.RadioDiva.UseVisualStyleBackColor = True
        '
        'RadioLogin
        '
        Me.RadioLogin.AutoSize = True
        Me.RadioLogin.Location = New System.Drawing.Point(21, 42)
        Me.RadioLogin.Name = "RadioLogin"
        Me.RadioLogin.Size = New System.Drawing.Size(90, 17)
        Me.RadioLogin.TabIndex = 3
        Me.RadioLogin.TabStop = True
        Me.RadioLogin.Text = "Login allowed"
        Me.RadioLogin.UseVisualStyleBackColor = True
        '
        'RadioNologin
        '
        Me.RadioNologin.AutoSize = True
        Me.RadioNologin.Location = New System.Drawing.Point(21, 19)
        Me.RadioNologin.Name = "RadioNologin"
        Me.RadioNologin.Size = New System.Drawing.Size(68, 17)
        Me.RadioNologin.TabIndex = 2
        Me.RadioNologin.TabStop = True
        Me.RadioNologin.Text = "No Login"
        Me.RadioNologin.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(445, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'HyperGridCheckBox
        '
        Me.HyperGridCheckBox.AutoSize = True
        Me.HyperGridCheckBox.Enabled = False
        Me.HyperGridCheckBox.Location = New System.Drawing.Point(20, 20)
        Me.HyperGridCheckBox.Name = "HyperGridCheckBox"
        Me.HyperGridCheckBox.Size = New System.Drawing.Size(71, 17)
        Me.HyperGridCheckBox.TabIndex = 10
        Me.HyperGridCheckBox.Text = "Hypergrid"
        Me.HyperGridCheckBox.UseVisualStyleBackColor = True
        '
        'LevelGroupBox
        '
        Me.LevelGroupBox.Controls.Add(Me.RadioNologin)
        Me.LevelGroupBox.Controls.Add(Me.RadioLogin)
        Me.LevelGroupBox.Controls.Add(Me.RadioDiva)
        Me.LevelGroupBox.Controls.Add(Me.RadioGid)
        Me.LevelGroupBox.Location = New System.Drawing.Point(12, 164)
        Me.LevelGroupBox.Name = "LevelGroupBox"
        Me.LevelGroupBox.Size = New System.Drawing.Size(200, 134)
        Me.LevelGroupBox.TabIndex = 11
        Me.LevelGroupBox.TabStop = False
        Me.LevelGroupBox.Text = "Level"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.HyperGridCheckBox)
        Me.GroupBox3.Controls.Add(Me.BanButton)
        Me.GroupBox3.Controls.Add(Me.AllowButton)
        Me.GroupBox3.Location = New System.Drawing.Point(233, 164)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(200, 134)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Level"
        '
        'FormEditUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 315)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.LevelGroupBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormEditUser"
        Me.Text = "FormEditUser"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.LevelGroupBox.ResumeLayout(False)
        Me.LevelGroupBox.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FnameTextBox As TextBox
    Friend WithEvents LastNameTextBox As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents AllowButton As Button
    Friend WithEvents BanButton As Button
    Friend WithEvents RadioGid As RadioButton
    Friend WithEvents RadioDiva As RadioButton
    Friend WithEvents RadioLogin As RadioButton
    Friend WithEvents RadioNologin As RadioButton
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents HyperGridCheckBox As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents LevelGroupBox As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TitleTextBox As TextBox
    Friend WithEvents UUIDTextBox As TextBox
End Class
