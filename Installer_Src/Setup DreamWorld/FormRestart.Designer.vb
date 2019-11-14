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
        Me.RestartOnPhysicsCrash = New System.Windows.Forms.CheckBox()
        Me.RestartOnCrash = New System.Windows.Forms.CheckBox()
        Me.SequentialCheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ARTimerBox = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.AutoRestartBox = New System.Windows.Forms.TextBox()
        Me.RunOnBoot = New System.Windows.Forms.PictureBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.AutoStartCheckbox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoStart.SuspendLayout()
        CType(Me.RunOnBoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
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
        Me.AutoStart.Location = New System.Drawing.Point(22, 63)
        Me.AutoStart.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AutoStart.Name = "AutoStart"
        Me.AutoStart.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AutoStart.Size = New System.Drawing.Size(392, 296)
        Me.AutoStart.TabIndex = 45
        Me.AutoStart.TabStop = False
        Me.AutoStart.Text = My.Resources.Auto_Startup_word
        '
        'RestartOnPhysicsCrash
        '
        Me.RestartOnPhysicsCrash.AutoSize = True
        Me.RestartOnPhysicsCrash.Location = New System.Drawing.Point(38, 243)
        Me.RestartOnPhysicsCrash.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RestartOnPhysicsCrash.Name = "RestartOnPhysicsCrash"
        Me.RestartOnPhysicsCrash.Size = New System.Drawing.Size(213, 24)
        Me.RestartOnPhysicsCrash.TabIndex = 1866
        Me.RestartOnPhysicsCrash.Text = Global.Outworldz.My.Resources.Resources.Restart_on_Physics_Crash
        Me.ToolTip1.SetToolTip(Me.RestartOnPhysicsCrash, Global.Outworldz.My.Resources.Resources.Restart_on_Physics_Crash_Text)
        Me.RestartOnPhysicsCrash.UseVisualStyleBackColor = True
        '
        'RestartOnCrash
        '
        Me.RestartOnCrash.AutoSize = True
        Me.RestartOnCrash.Location = New System.Drawing.Point(38, 209)
        Me.RestartOnCrash.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RestartOnCrash.Name = "RestartOnCrash"
        Me.RestartOnCrash.Size = New System.Drawing.Size(156, 24)
        Me.RestartOnCrash.TabIndex = 1865
        Me.RestartOnCrash.Text = My.Resources.Restart_On_Crash
        Me.ToolTip1.SetToolTip(Me.RestartOnCrash, Global.Outworldz.My.Resources.Resources.Restart_On_Crash)
        Me.RestartOnCrash.UseVisualStyleBackColor = True
        '
        'SequentialCheckBox1
        '
        Me.SequentialCheckBox1.AutoSize = True
        Me.SequentialCheckBox1.Location = New System.Drawing.Point(38, 139)
        Me.SequentialCheckBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SequentialCheckBox1.Name = "SequentialCheckBox1"
        Me.SequentialCheckBox1.Size = New System.Drawing.Size(223, 24)
        Me.SequentialCheckBox1.TabIndex = 1864
        Me.SequentialCheckBox1.Text = Global.Outworldz.My.Resources.Resources.StartSequentially
        Me.ToolTip1.SetToolTip(Me.SequentialCheckBox1, Global.Outworldz.My.Resources.Resources.Sequentially_text)
        Me.SequentialCheckBox1.UseVisualStyleBackColor = True
        '
        'ARTimerBox
        '
        Me.ARTimerBox.AutoSize = True
        Me.ARTimerBox.Location = New System.Drawing.Point(38, 57)
        Me.ARTimerBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ARTimerBox.Name = "ARTimerBox"
        Me.ARTimerBox.Size = New System.Drawing.Size(169, 24)
        Me.ARTimerBox.TabIndex = 1863
        Me.ARTimerBox.Text = Global.Outworldz.My.Resources.Resources.Restart_Periodically_word
        Me.ToolTip1.SetToolTip(Me.ARTimerBox, Global.Outworldz.My.Resources.Resources.Restart_Periodically_Minutes)
        Me.ARTimerBox.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(100, 96)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(194, 20)
        Me.Label25.TabIndex = 1862
        Me.Label25.Text = My.Resources.Restart_Interval
        '
        'AutoRestartBox
        '
        Me.AutoRestartBox.Location = New System.Drawing.Point(38, 92)
        Me.AutoRestartBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AutoRestartBox.Name = "AutoRestartBox"
        Me.AutoRestartBox.Size = New System.Drawing.Size(52, 26)
        Me.AutoRestartBox.TabIndex = 47
        Me.ToolTip1.SetToolTip(Me.AutoRestartBox, Global.Outworldz.My.Resources.Resources.AutorestartBox)
        '
        'RunOnBoot
        '
        Me.RunOnBoot.Image = Global.Outworldz.My.Resources.Resources.about
        Me.RunOnBoot.Location = New System.Drawing.Point(248, 28)
        Me.RunOnBoot.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RunOnBoot.Name = "RunOnBoot"
        Me.RunOnBoot.Size = New System.Drawing.Size(45, 51)
        Me.RunOnBoot.TabIndex = 1859
        Me.RunOnBoot.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(117, 260)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(0, 20)
        Me.Label13.TabIndex = 24
        '
        'AutoStartCheckbox
        '
        Me.AutoStartCheckbox.AutoSize = True
        Me.AutoStartCheckbox.Location = New System.Drawing.Point(38, 174)
        Me.AutoStartCheckbox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AutoStartCheckbox.Name = "AutoStartCheckbox"
        Me.AutoStartCheckbox.Size = New System.Drawing.Size(196, 24)
        Me.AutoStartCheckbox.TabIndex = 45
        Me.AutoStartCheckbox.Text = Global.Outworldz.My.Resources.Resources.EnableOneClick
        Me.ToolTip1.SetToolTip(Me.AutoStartCheckbox, Global.Outworldz.My.Resources.Resources.StartLaunch)
        Me.AutoStartCheckbox.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(438, 35)
        Me.MenuStrip2.TabIndex = 1889
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(85, 29)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(151, 34)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'FormRestart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(438, 396)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.AutoStart)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "FormRestart"
        Me.Text = My.Resources.Restart_word
        Me.AutoStart.ResumeLayout(False)
        Me.AutoStart.PerformLayout()
        CType(Me.RunOnBoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
