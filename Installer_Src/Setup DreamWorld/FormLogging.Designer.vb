<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLogging
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoggingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioOff = New System.Windows.Forms.RadioButton()
        Me.RadioDebug = New System.Windows.Forms.RadioButton()
        Me.RadioInfo = New System.Windows.Forms.RadioButton()
        Me.RadioWarn = New System.Windows.Forms.RadioButton()
        Me.RadioError = New System.Windows.Forms.RadioButton()
        Me.RadioFatal = New System.Windows.Forms.RadioButton()
        Me.RadioAll = New System.Windows.Forms.RadioButton()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(144, 26)
        Me.MenuStrip1.TabIndex = 186741
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoggingToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        '
        'LoggingToolStripMenuItem
        '
        Me.LoggingToolStripMenuItem.Image = Global.Outworldz.My.Resources.window_environment
        Me.LoggingToolStripMenuItem.Name = "LoggingToolStripMenuItem"
        Me.LoggingToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.LoggingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Logging_word
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioAll)
        Me.GroupBox1.Controls.Add(Me.RadioFatal)
        Me.GroupBox1.Controls.Add(Me.RadioError)
        Me.GroupBox1.Controls.Add(Me.RadioWarn)
        Me.GroupBox1.Controls.Add(Me.RadioInfo)
        Me.GroupBox1.Controls.Add(Me.RadioDebug)
        Me.GroupBox1.Controls.Add(Me.RadioOff)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 47)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(120, 202)
        Me.GroupBox1.TabIndex = 186742
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = Global.Outworldz.My.Resources.Logging_word
        '
        'RadioOff
        '
        Me.RadioOff.AutoSize = True
        Me.RadioOff.Location = New System.Drawing.Point(17, 168)
        Me.RadioOff.Name = "RadioOff"
        Me.RadioOff.Size = New System.Drawing.Size(39, 17)
        Me.RadioOff.TabIndex = 0
        Me.RadioOff.TabStop = True
        Me.RadioOff.Text = "Off"
        Me.RadioOff.UseVisualStyleBackColor = True
        '
        'RadioDebug
        '
        Me.RadioDebug.AutoSize = True
        Me.RadioDebug.Location = New System.Drawing.Point(17, 53)
        Me.RadioDebug.Name = "RadioDebug"
        Me.RadioDebug.Size = New System.Drawing.Size(57, 17)
        Me.RadioDebug.TabIndex = 1
        Me.RadioDebug.TabStop = True
        Me.RadioDebug.Text = Global.Outworldz.My.Resources.Debug_word
        Me.RadioDebug.UseVisualStyleBackColor = True
        '
        'RadioInfo
        '
        Me.RadioInfo.AutoSize = True
        Me.RadioInfo.Location = New System.Drawing.Point(17, 76)
        Me.RadioInfo.Name = "RadioInfo"
        Me.RadioInfo.Size = New System.Drawing.Size(86, 17)
        Me.RadioInfo.TabIndex = 2
        Me.RadioInfo.TabStop = True
        Me.RadioInfo.Text = Global.Outworldz.My.Resources.Info_word
        Me.RadioInfo.UseVisualStyleBackColor = True
        '
        'RadioWarn
        '
        Me.RadioWarn.AutoSize = True
        Me.RadioWarn.Location = New System.Drawing.Point(17, 99)
        Me.RadioWarn.Name = "RadioWarn"
        Me.RadioWarn.Size = New System.Drawing.Size(51, 17)
        Me.RadioWarn.TabIndex = 3
        Me.RadioWarn.TabStop = True
        Me.RadioWarn.Text = Global.Outworldz.My.Resources.Warn_word
        Me.RadioWarn.UseVisualStyleBackColor = True
        '
        'RadioError
        '
        Me.RadioError.AutoSize = True
        Me.RadioError.Location = New System.Drawing.Point(17, 122)
        Me.RadioError.Name = "RadioError"
        Me.RadioError.Size = New System.Drawing.Size(47, 17)
        Me.RadioError.TabIndex = 4
        Me.RadioError.TabStop = True
        Me.RadioError.Text = Global.Outworldz.My.Resources.Error_word
        Me.RadioError.UseVisualStyleBackColor = True
        '
        'RadioFatal
        '
        Me.RadioFatal.AutoSize = True
        Me.RadioFatal.Location = New System.Drawing.Point(17, 145)
        Me.RadioFatal.Name = "RadioFatal"
        Me.RadioFatal.Size = New System.Drawing.Size(48, 17)
        Me.RadioFatal.TabIndex = 5
        Me.RadioFatal.TabStop = True
        Me.RadioFatal.Text = Global.Outworldz.My.Resources.Fatal_word
        Me.RadioFatal.UseVisualStyleBackColor = True
        '
        'RadioAll
        '
        Me.RadioAll.AutoSize = True
        Me.RadioAll.Location = New System.Drawing.Point(17, 30)
        Me.RadioAll.Name = "RadioAll"
        Me.RadioAll.Size = New System.Drawing.Size(36, 17)
        Me.RadioAll.TabIndex = 6
        Me.RadioAll.TabStop = True
        Me.RadioAll.Text = Global.Outworldz.My.Resources.All_word
        Me.RadioAll.UseVisualStyleBackColor = True
        '
        'FormLogging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(144, 269)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "FormLogging"
        Me.Text = Global.Outworldz.My.Resources.Logging_word
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoggingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioAll As RadioButton
    Friend WithEvents RadioFatal As RadioButton
    Friend WithEvents RadioError As RadioButton
    Friend WithEvents RadioWarn As RadioButton
    Friend WithEvents RadioInfo As RadioButton
    Friend WithEvents RadioDebug As RadioButton
    Friend WithEvents RadioOff As RadioButton
End Class
