<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRestart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRestart))
        Me.AutoStart = New System.Windows.Forms.GroupBox()
        Me.NoDelayRadioButton = New System.Windows.Forms.RadioButton()
        Me.SequentialRadioButton = New System.Windows.Forms.RadioButton()
        Me.ConcurrentRadioButton = New System.Windows.Forms.RadioButton()
        Me.RestartOnCrash = New System.Windows.Forms.CheckBox()
        Me.ARTimerBox = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.AutoRestartBox = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.AutoStartCheckbox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.IntervalGroupBox = New System.Windows.Forms.GroupBox()
        Me.AutoStart.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.IntervalGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'AutoStart
        '
        Me.AutoStart.Controls.Add(Me.IntervalGroupBox)
        Me.AutoStart.Controls.Add(Me.RestartOnCrash)
        Me.AutoStart.Controls.Add(Me.ARTimerBox)
        Me.AutoStart.Controls.Add(Me.Label25)
        Me.AutoStart.Controls.Add(Me.AutoRestartBox)
        Me.AutoStart.Controls.Add(Me.Label13)
        Me.AutoStart.Controls.Add(Me.AutoStartCheckbox)
        Me.AutoStart.Location = New System.Drawing.Point(15, 42)
        Me.AutoStart.Name = "AutoStart"
        Me.AutoStart.Size = New System.Drawing.Size(324, 268)
        Me.AutoStart.TabIndex = 45
        Me.AutoStart.TabStop = False
        Me.AutoStart.Text = "Automatic Startup"
        '
        'NoDelayRadioButton
        '
        Me.NoDelayRadioButton.AutoSize = True
        Me.NoDelayRadioButton.Location = New System.Drawing.Point(30, 20)
        Me.NoDelayRadioButton.Name = "NoDelayRadioButton"
        Me.NoDelayRadioButton.Size = New System.Drawing.Size(69, 17)
        Me.NoDelayRadioButton.TabIndex = 1868
        Me.NoDelayRadioButton.TabStop = True
        Me.NoDelayRadioButton.Text = "No Delay"
        Me.NoDelayRadioButton.UseVisualStyleBackColor = True
        '
        'SequentialRadioButton
        '
        Me.SequentialRadioButton.AutoSize = True
        Me.SequentialRadioButton.Location = New System.Drawing.Point(30, 66)
        Me.SequentialRadioButton.Name = "SequentialRadioButton"
        Me.SequentialRadioButton.Size = New System.Drawing.Size(102, 17)
        Me.SequentialRadioButton.TabIndex = 1867
        Me.SequentialRadioButton.TabStop = True
        Me.SequentialRadioButton.Text = "Sequential order"
        Me.SequentialRadioButton.UseVisualStyleBackColor = True
        '
        'ConcurrentRadioButton
        '
        Me.ConcurrentRadioButton.AutoSize = True
        Me.ConcurrentRadioButton.Location = New System.Drawing.Point(30, 43)
        Me.ConcurrentRadioButton.Name = "ConcurrentRadioButton"
        Me.ConcurrentRadioButton.Size = New System.Drawing.Size(98, 17)
        Me.ConcurrentRadioButton.TabIndex = 1866
        Me.ConcurrentRadioButton.TabStop = True
        Me.ConcurrentRadioButton.Text = "Parallel Booting"
        Me.ConcurrentRadioButton.UseVisualStyleBackColor = True
        '
        'RestartOnCrash
        '
        Me.RestartOnCrash.AutoSize = True
        Me.RestartOnCrash.Location = New System.Drawing.Point(25, 97)
        Me.RestartOnCrash.Name = "RestartOnCrash"
        Me.RestartOnCrash.Size = New System.Drawing.Size(242, 17)
        Me.RestartOnCrash.TabIndex = 1865
        Me.RestartOnCrash.Text = Global.Outworldz.My.Resources.Resources.Restart_On_Crash
        Me.ToolTip1.SetToolTip(Me.RestartOnCrash, Global.Outworldz.My.Resources.Resources.Restart_On_Crash)
        Me.RestartOnCrash.UseVisualStyleBackColor = True
        '
        'ARTimerBox
        '
        Me.ARTimerBox.AutoSize = True
        Me.ARTimerBox.Location = New System.Drawing.Point(25, 38)
        Me.ARTimerBox.Name = "ARTimerBox"
        Me.ARTimerBox.Size = New System.Drawing.Size(115, 17)
        Me.ARTimerBox.TabIndex = 1863
        Me.ARTimerBox.Text = Global.Outworldz.My.Resources.Resources.Restart_Periodically_word
        Me.ToolTip1.SetToolTip(Me.ARTimerBox, Global.Outworldz.My.Resources.Resources.Restart_Periodically_Minutes)
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
        Me.ToolTip1.SetToolTip(Me.AutoRestartBox, Global.Outworldz.My.Resources.Resources.AutorestartBox)
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
        Me.AutoStartCheckbox.Location = New System.Drawing.Point(25, 120)
        Me.AutoStartCheckbox.Name = "AutoStartCheckbox"
        Me.AutoStartCheckbox.Size = New System.Drawing.Size(133, 17)
        Me.AutoStartCheckbox.TabIndex = 45
        Me.AutoStartCheckbox.Text = Global.Outworldz.My.Resources.Resources.EnableOneClickStart_word
        Me.ToolTip1.SetToolTip(Me.AutoStartCheckbox, Global.Outworldz.My.Resources.Resources.StartLaunch)
        Me.AutoStartCheckbox.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(351, 34)
        Me.MenuStrip2.TabIndex = 1889
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(72, 32)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'IntervalGroupBox
        '
        Me.IntervalGroupBox.Controls.Add(Me.NoDelayRadioButton)
        Me.IntervalGroupBox.Controls.Add(Me.ConcurrentRadioButton)
        Me.IntervalGroupBox.Controls.Add(Me.SequentialRadioButton)
        Me.IntervalGroupBox.Location = New System.Drawing.Point(25, 153)
        Me.IntervalGroupBox.Name = "IntervalGroupBox"
        Me.IntervalGroupBox.Size = New System.Drawing.Size(242, 94)
        Me.IntervalGroupBox.TabIndex = 1869
        Me.IntervalGroupBox.TabStop = False
        Me.IntervalGroupBox.Text = "Boot Interval"
        '
        'FormRestart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(351, 320)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.AutoStart)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormRestart"
        Me.Text = "Restart"
        Me.AutoStart.ResumeLayout(False)
        Me.AutoStart.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.IntervalGroupBox.ResumeLayout(False)
        Me.IntervalGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AutoStart As GroupBox
    Friend WithEvents Label25 As Label
    Friend WithEvents AutoRestartBox As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents AutoStartCheckbox As CheckBox
    Friend WithEvents ARTimerBox As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents RestartOnCrash As CheckBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents NoDelayRadioButton As RadioButton
    Friend WithEvents SequentialRadioButton As RadioButton
    Friend WithEvents ConcurrentRadioButton As RadioButton
    Friend WithEvents IntervalGroupBox As GroupBox
End Class
