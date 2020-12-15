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
        Me.AutoStart.Controls.Add(Me.RestartOnCrash)
        Me.AutoStart.Controls.Add(Me.SequentialCheckBox1)
        Me.AutoStart.Controls.Add(Me.ARTimerBox)
        Me.AutoStart.Controls.Add(Me.Label25)
        Me.AutoStart.Controls.Add(Me.AutoRestartBox)
        Me.AutoStart.Controls.Add(Me.RunOnBoot)
        Me.AutoStart.Controls.Add(Me.Label13)
        Me.AutoStart.Controls.Add(Me.AutoStartCheckbox)
        Me.AutoStart.Location = New System.Drawing.Point(19, 52)
        Me.AutoStart.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AutoStart.Name = "AutoStart"
        Me.AutoStart.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AutoStart.Size = New System.Drawing.Size(326, 222)
        Me.AutoStart.TabIndex = 45
        Me.AutoStart.TabStop = False
        Me.AutoStart.Text = "Automatic Startup"
        '
        'RestartOnCrash
        '
        Me.RestartOnCrash.AutoSize = True
        Me.RestartOnCrash.Location = New System.Drawing.Point(31, 174)
        Me.RestartOnCrash.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RestartOnCrash.Name = "RestartOnCrash"
        Me.RestartOnCrash.Size = New System.Drawing.Size(322, 21)
        Me.RestartOnCrash.TabIndex = 1865
        Me.RestartOnCrash.Text = Global.Outworldz.My.Resources.Resources.Restart_On_Crash
        Me.ToolTip1.SetToolTip(Me.RestartOnCrash, Global.Outworldz.My.Resources.Resources.Restart_On_Crash)
        Me.RestartOnCrash.UseVisualStyleBackColor = True
        '
        'SequentialCheckBox1
        '
        Me.SequentialCheckBox1.AutoSize = True
        Me.SequentialCheckBox1.Location = New System.Drawing.Point(31, 116)
        Me.SequentialCheckBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SequentialCheckBox1.Name = "SequentialCheckBox1"
        Me.SequentialCheckBox1.Size = New System.Drawing.Size(197, 21)
        Me.SequentialCheckBox1.TabIndex = 1864
        Me.SequentialCheckBox1.Text = Global.Outworldz.My.Resources.Resources.StartSequentially
        Me.ToolTip1.SetToolTip(Me.SequentialCheckBox1, Global.Outworldz.My.Resources.Resources.Sequentially_text)
        Me.SequentialCheckBox1.UseVisualStyleBackColor = True
        '
        'ARTimerBox
        '
        Me.ARTimerBox.AutoSize = True
        Me.ARTimerBox.Location = New System.Drawing.Point(31, 48)
        Me.ARTimerBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ARTimerBox.Name = "ARTimerBox"
        Me.ARTimerBox.Size = New System.Drawing.Size(151, 21)
        Me.ARTimerBox.TabIndex = 1863
        Me.ARTimerBox.Text = Global.Outworldz.My.Resources.Resources.Restart_Periodically_word
        Me.ToolTip1.SetToolTip(Me.ARTimerBox, Global.Outworldz.My.Resources.Resources.Restart_Periodically_Minutes)
        Me.ARTimerBox.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(84, 80)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(172, 17)
        Me.Label25.TabIndex = 1862
        Me.Label25.Text = "Restart Interval in Minutes"
        '
        'AutoRestartBox
        '
        Me.AutoRestartBox.Location = New System.Drawing.Point(31, 76)
        Me.AutoRestartBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AutoRestartBox.Name = "AutoRestartBox"
        Me.AutoRestartBox.Size = New System.Drawing.Size(44, 22)
        Me.AutoRestartBox.TabIndex = 47
        Me.ToolTip1.SetToolTip(Me.AutoRestartBox, Global.Outworldz.My.Resources.Resources.AutorestartBox)
        '
        'RunOnBoot
        '
        Me.RunOnBoot.Image = Global.Outworldz.My.Resources.Resources.about
        Me.RunOnBoot.Location = New System.Drawing.Point(206, 24)
        Me.RunOnBoot.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RunOnBoot.Name = "RunOnBoot"
        Me.RunOnBoot.Size = New System.Drawing.Size(38, 42)
        Me.RunOnBoot.TabIndex = 1859
        Me.RunOnBoot.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(98, 216)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(0, 17)
        Me.Label13.TabIndex = 24
        '
        'AutoStartCheckbox
        '
        Me.AutoStartCheckbox.AutoSize = True
        Me.AutoStartCheckbox.Location = New System.Drawing.Point(31, 145)
        Me.AutoStartCheckbox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AutoStartCheckbox.Name = "AutoStartCheckbox"
        Me.AutoStartCheckbox.Size = New System.Drawing.Size(173, 21)
        Me.AutoStartCheckbox.TabIndex = 45
        Me.AutoStartCheckbox.Text = Global.Outworldz.My.Resources.Resources.EnableOneClickStart_word
        Me.ToolTip1.SetToolTip(Me.AutoStartCheckbox, Global.Outworldz.My.Resources.Resources.StartLaunch)
        Me.AutoStartCheckbox.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(5, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(365, 26)
        Me.MenuStrip2.TabIndex = 1889
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(75, 24)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(124, 26)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormRestart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(365, 330)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.AutoStart)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "FormRestart"
        Me.Text = "Restart"
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
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
