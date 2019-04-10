<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRestart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRestart))
        Me.AutoStart = New System.Windows.Forms.GroupBox()
        Me.SequentialCheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ARTimerBox = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.AutoRestartBox = New System.Windows.Forms.TextBox()
        Me.RunOnBoot = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.AutoStartCheckbox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RestartOnCrash = New System.Windows.Forms.CheckBox()
        Me.RestartOnPhysicsCrash = New System.Windows.Forms.CheckBox()
        Me.AutoStart.SuspendLayout()
        CType(Me.RunOnBoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AutoStart
        '
        Me.AutoStart.Controls.Add(Me.RestartOnPhysicsCrash)
        Me.AutoStart.Controls.Add(Me.RestartOnCrash)
        Me.AutoStart.Controls.Add(Me.SequentialCheckBox1)
        Me.AutoStart.Controls.Add(Me.ARTimerBox)
        Me.AutoStart.Controls.Add(Me.Label25)
        Me.AutoStart.Controls.Add(Me.AutoRestartBox)
        Me.AutoStart.Controls.Add(Me.RunOnBoot)
        Me.AutoStart.Controls.Add(Me.Label13)
        Me.AutoStart.Controls.Add(Me.AutoStartCheckbox)
        Me.AutoStart.Location = New System.Drawing.Point(12, 12)
        Me.AutoStart.Name = "AutoStart"
        Me.AutoStart.Size = New System.Drawing.Size(223, 244)
        Me.AutoStart.TabIndex = 45
        Me.AutoStart.TabStop = False
        Me.AutoStart.Text = "Auto Restart"
        '
        'SequentialCheckBox1
        '
        Me.SequentialCheckBox1.AutoSize = True
        Me.SequentialCheckBox1.Location = New System.Drawing.Point(25, 107)
        Me.SequentialCheckBox1.Name = "SequentialCheckBox1"
        Me.SequentialCheckBox1.Size = New System.Drawing.Size(150, 17)
        Me.SequentialCheckBox1.TabIndex = 1864
        Me.SequentialCheckBox1.Text = "Start Regions Sequentially"
        Me.ToolTip1.SetToolTip(Me.SequentialCheckBox1, "if enabled, there is no need to click the Start button. It will start when launch" &
        "ed.")
        Me.SequentialCheckBox1.UseVisualStyleBackColor = True
        '
        'ARTimerBox
        '
        Me.ARTimerBox.AutoSize = True
        Me.ARTimerBox.Location = New System.Drawing.Point(25, 38)
        Me.ARTimerBox.Name = "ARTimerBox"
        Me.ARTimerBox.Size = New System.Drawing.Size(115, 17)
        Me.ARTimerBox.TabIndex = 1863
        Me.ARTimerBox.Text = "Restart periodically"
        Me.ToolTip1.SetToolTip(Me.ARTimerBox, "If enabled the Regions will all auto restart after Inteveral many minutes running" &
        ".")
        Me.ARTimerBox.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(67, 64)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(130, 13)
        Me.Label25.TabIndex = 1862
        Me.Label25.Text = "Restart Interval in Minutes"
        '
        'AutoRestartBox
        '
        Me.AutoRestartBox.Location = New System.Drawing.Point(25, 61)
        Me.AutoRestartBox.Name = "AutoRestartBox"
        Me.AutoRestartBox.Size = New System.Drawing.Size(36, 20)
        Me.AutoRestartBox.TabIndex = 47
        Me.ToolTip1.SetToolTip(Me.AutoRestartBox, "0 = off, expressed in minutes until auto restart occurs.")
        '
        'RunOnBoot
        '
        Me.RunOnBoot.Image = Global.Outworldz.My.Resources.Resources.about
        Me.RunOnBoot.Location = New System.Drawing.Point(165, 19)
        Me.RunOnBoot.Name = "RunOnBoot"
        Me.RunOnBoot.Size = New System.Drawing.Size(30, 34)
        Me.RunOnBoot.TabIndex = 1859
        Me.RunOnBoot.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(78, 173)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(0, 13)
        Me.Label13.TabIndex = 24
        '
        'AutoStartCheckbox
        '
        Me.AutoStartCheckbox.AutoSize = True
        Me.AutoStartCheckbox.Location = New System.Drawing.Point(25, 139)
        Me.AutoStartCheckbox.Name = "AutoStartCheckbox"
        Me.AutoStartCheckbox.Size = New System.Drawing.Size(133, 17)
        Me.AutoStartCheckbox.TabIndex = 45
        Me.AutoStartCheckbox.Text = "Enable One Click Start"
        Me.ToolTip1.SetToolTip(Me.AutoStartCheckbox, "if enabled, there is no need to click the Start button. It will start when launch" &
        "ed.")
        Me.AutoStartCheckbox.UseVisualStyleBackColor = True
        '
        'RestartOnCrash
        '
        Me.RestartOnCrash.AutoSize = True
        Me.RestartOnCrash.Location = New System.Drawing.Point(25, 169)
        Me.RestartOnCrash.Name = "RestartOnCrash"
        Me.RestartOnCrash.Size = New System.Drawing.Size(105, 17)
        Me.RestartOnCrash.TabIndex = 1865
        Me.RestartOnCrash.Text = "Restart on Crash"
        Me.ToolTip1.SetToolTip(Me.RestartOnCrash, "if enabled, there is no need to click the Start button. It will start when launch" &
        "ed.")
        Me.RestartOnCrash.UseVisualStyleBackColor = True
        '
        'RestartOnPhysicsCrash
        '
        Me.RestartOnPhysicsCrash.AutoSize = True
        Me.RestartOnPhysicsCrash.Location = New System.Drawing.Point(25, 203)
        Me.RestartOnPhysicsCrash.Name = "RestartOnPhysicsCrash"
        Me.RestartOnPhysicsCrash.Size = New System.Drawing.Size(144, 17)
        Me.RestartOnPhysicsCrash.TabIndex = 1866
        Me.RestartOnPhysicsCrash.Text = "Restart on Physics Crash"
        Me.ToolTip1.SetToolTip(Me.RestartOnPhysicsCrash, "if enabled, there is no need to click the Start button. It will start when launch" &
        "ed.")
        Me.RestartOnPhysicsCrash.UseVisualStyleBackColor = True
        '
        'FormRestart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(250, 302)
        Me.Controls.Add(Me.AutoStart)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormRestart"
        Me.Text = "Restart"
        Me.AutoStart.ResumeLayout(False)
        Me.AutoStart.PerformLayout()
        CType(Me.RunOnBoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents AutoStart As GroupBox
    Friend WithEvents Label25 As Label
    Friend WithEvents AutoRestartBox As TextBox
    Friend WithEvents RunOnBoot As PictureBox
    Friend WithEvents Label13 As Label
    Friend WithEvents AutoStartCheckbox As CheckBox
    Friend WithEvents ARTimerBox As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SequentialCheckBox1 As CheckBox
    Friend WithEvents RestartOnCrash As CheckBox
    Friend WithEvents RestartOnPhysicsCrash As CheckBox
End Class
