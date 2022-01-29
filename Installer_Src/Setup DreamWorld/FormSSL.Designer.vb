<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSSL
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
        Me.CreateButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.ViewLogButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.EnableSSLCheckbox = New System.Windows.Forms.CheckBox()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.RestartButton = New System.Windows.Forms.Button()
        Me.StopButton = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmailBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Revokebutton = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CreateButton
        '
        Me.CreateButton.Location = New System.Drawing.Point(15, 155)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(118, 28)
        Me.CreateButton.TabIndex = 0
        Me.CreateButton.Text = "Create Certificate"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Revokebutton)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.EmailBox)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.ViewLogButton)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.EnableSSLCheckbox)
        Me.GroupBox1.Controls.Add(Me.StartButton)
        Me.GroupBox1.Controls.Add(Me.RestartButton)
        Me.GroupBox1.Controls.Add(Me.StopButton)
        Me.GroupBox1.Controls.Add(Me.CreateButton)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(307, 270)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SSL"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(20, 223)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(118, 28)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "View Status"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Outworldz.My.Resources.Resources.gear_time
        Me.PictureBox2.Location = New System.Drawing.Point(204, 123)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 8
        Me.PictureBox2.TabStop = False
        '
        'ViewLogButton
        '
        Me.ViewLogButton.Location = New System.Drawing.Point(15, 189)
        Me.ViewLogButton.Name = "ViewLogButton"
        Me.ViewLogButton.Size = New System.Drawing.Size(118, 28)
        Me.ViewLogButton.TabIndex = 7
        Me.ViewLogButton.Text = "View Log"
        Me.ViewLogButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(120, 134)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Status"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.lock_open
        Me.PictureBox1.Location = New System.Drawing.Point(62, 123)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'EnableSSLCheckbox
        '
        Me.EnableSSLCheckbox.AutoSize = True
        Me.EnableSSLCheckbox.Location = New System.Drawing.Point(20, 81)
        Me.EnableSSLCheckbox.Name = "EnableSSLCheckbox"
        Me.EnableSSLCheckbox.Size = New System.Drawing.Size(85, 17)
        Me.EnableSSLCheckbox.TabIndex = 4
        Me.EnableSSLCheckbox.Text = "Enable SSL "
        Me.EnableSSLCheckbox.UseVisualStyleBackColor = True
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(162, 223)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(118, 28)
        Me.StartButton.TabIndex = 3
        Me.StartButton.Text = "Start Apache"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'RestartButton
        '
        Me.RestartButton.Location = New System.Drawing.Point(162, 189)
        Me.RestartButton.Name = "RestartButton"
        Me.RestartButton.Size = New System.Drawing.Size(118, 28)
        Me.RestartButton.TabIndex = 2
        Me.RestartButton.Text = "Restart Apache"
        Me.RestartButton.UseVisualStyleBackColor = True
        '
        'StopButton
        '
        Me.StopButton.Location = New System.Drawing.Point(162, 155)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(118, 28)
        Me.StopButton.TabIndex = 1
        Me.StopButton.Text = "Stop Apache"
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(331, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'EmailBox
        '
        Me.EmailBox.Location = New System.Drawing.Point(20, 44)
        Me.EmailBox.Name = "EmailBox"
        Me.EmailBox.Size = New System.Drawing.Size(264, 20)
        Me.EmailBox.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Email for Notices"
        '
        'Revokebutton
        '
        Me.Revokebutton.Location = New System.Drawing.Point(162, 74)
        Me.Revokebutton.Name = "Revokebutton"
        Me.Revokebutton.Size = New System.Drawing.Size(118, 28)
        Me.Revokebutton.TabIndex = 12
        Me.Revokebutton.Text = "Revoke Certificate"
        Me.Revokebutton.UseVisualStyleBackColor = True
        '
        'FormSSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(331, 312)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormSSL"
        Me.Text = "SSL"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CreateButton As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents EnableSSLCheckbox As CheckBox
    Friend WithEvents StartButton As Button
    Friend WithEvents RestartButton As Button
    Friend WithEvents StopButton As Button
    Friend WithEvents ViewLogButton As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents EmailBox As TextBox
    Friend WithEvents Revokebutton As Button
End Class
