<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormVoice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVoice))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RunOnBoot = New System.Windows.Forms.PictureBox()
        Me.RequestPassword = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.VivoxEnable = New System.Windows.Forms.CheckBox()
        Me.VivoxPassword = New System.Windows.Forms.TextBox()
        Me.VivoxUserName = New System.Windows.Forms.TextBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.RunOnBoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RunOnBoot)
        Me.GroupBox1.Controls.Add(Me.RequestPassword)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.VivoxEnable)
        Me.GroupBox1.Controls.Add(Me.VivoxPassword)
        Me.GroupBox1.Controls.Add(Me.VivoxUserName)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(245, 135)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = My.Resources.Setup_Voice_Service
        '
        'RunOnBoot
        '
        Me.RunOnBoot.Image = Global.Outworldz.My.Resources.Resources.about
        Me.RunOnBoot.Location = New System.Drawing.Point(180, 7)
        Me.RunOnBoot.Name = "RunOnBoot"
        Me.RunOnBoot.Size = New System.Drawing.Size(30, 34)
        Me.RunOnBoot.TabIndex = 1861
        Me.RunOnBoot.TabStop = False
        '
        'RequestPassword
        '
        Me.RequestPassword.Location = New System.Drawing.Point(6, 18)
        Me.RequestPassword.Name = "RequestPassword"
        Me.RequestPassword.Size = New System.Drawing.Size(168, 23)
        Me.RequestPassword.TabIndex = 0
        Me.RequestPassword.Text = My.Resources.Click_to_Request_Voice_Service
        Me.RequestPassword.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = My.Resources.Password
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = My.Resources.UserID
        '
        'VivoxEnable
        '
        Me.VivoxEnable.AutoSize = True
        Me.VivoxEnable.Location = New System.Drawing.Point(9, 51)
        Me.VivoxEnable.Name = "VivoxEnable"
        Me.VivoxEnable.Size = New System.Drawing.Size(59, 17)
        Me.VivoxEnable.TabIndex = 1
        Me.VivoxEnable.Text = My.Resources.Enable
        Me.VivoxEnable.UseVisualStyleBackColor = True
        '
        'VivoxPassword
        '
        Me.VivoxPassword.Location = New System.Drawing.Point(109, 101)
        Me.VivoxPassword.Name = "VivoxPassword"
        Me.VivoxPassword.Size = New System.Drawing.Size(130, 20)
        Me.VivoxPassword.TabIndex = 3
        Me.VivoxPassword.UseSystemPasswordChar = True
        '
        'VivoxUserName
        '
        Me.VivoxUserName.Location = New System.Drawing.Point(109, 76)
        Me.VivoxUserName.Name = "VivoxUserName"
        Me.VivoxUserName.Size = New System.Drawing.Size(130, 20)
        Me.VivoxUserName.TabIndex = 2
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(272, 28)
        Me.MenuStrip2.TabIndex = 1888
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
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.DatabaseSetupToolStripMenuItem.Text = My.Resources.Help
        '
        'FormVoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(272, 191)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormVoice"
        Me.Text = My.Resources.Voice
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.RunOnBoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents VivoxEnable As CheckBox
    Friend WithEvents VivoxPassword As TextBox
    Friend WithEvents VivoxUserName As TextBox
    Friend WithEvents RequestPassword As Button
    Friend WithEvents RunOnBoot As PictureBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
