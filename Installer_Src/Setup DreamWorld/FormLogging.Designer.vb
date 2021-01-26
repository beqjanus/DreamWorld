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
        Me.RadioFatal = New System.Windows.Forms.RadioButton()
        Me.RadioError = New System.Windows.Forms.RadioButton()
        Me.RadioWarn = New System.Windows.Forms.RadioButton()
        Me.RadioInfo = New System.Windows.Forms.RadioButton()
        Me.RadioDebug = New System.Windows.Forms.RadioButton()
        Me.RadioOff = New System.Windows.Forms.RadioButton()
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
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(165, 26)
        Me.MenuStrip1.TabIndex = 186741
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoggingToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(75, 24)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'LoggingToolStripMenuItem
        '
        Me.LoggingToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.LoggingToolStripMenuItem.Name = "LoggingToolStripMenuItem"
        Me.LoggingToolStripMenuItem.Size = New System.Drawing.Size(147, 26)
        Me.LoggingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Logging_word
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioFatal)
        Me.GroupBox1.Controls.Add(Me.RadioError)
        Me.GroupBox1.Controls.Add(Me.RadioWarn)
        Me.GroupBox1.Controls.Add(Me.RadioInfo)
        Me.GroupBox1.Controls.Add(Me.RadioDebug)
        Me.GroupBox1.Controls.Add(Me.RadioOff)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 58)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(128, 222)
        Me.GroupBox1.TabIndex = 186742
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Logging"
        '
        'RadioFatal
        '
        Me.RadioFatal.AutoSize = True
        Me.RadioFatal.Location = New System.Drawing.Point(18, 148)
        Me.RadioFatal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioFatal.Name = "RadioFatal"
        Me.RadioFatal.Size = New System.Drawing.Size(60, 21)
        Me.RadioFatal.TabIndex = 5
        Me.RadioFatal.TabStop = True
        Me.RadioFatal.Text = Global.Outworldz.My.Resources.Resources.Fatal_word
        Me.RadioFatal.UseVisualStyleBackColor = True
        '
        'RadioError
        '
        Me.RadioError.AutoSize = True
        Me.RadioError.Location = New System.Drawing.Point(18, 120)
        Me.RadioError.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioError.Name = "RadioError"
        Me.RadioError.Size = New System.Drawing.Size(61, 21)
        Me.RadioError.TabIndex = 4
        Me.RadioError.TabStop = True
        Me.RadioError.Text = Global.Outworldz.My.Resources.Resources.Error_word
        Me.RadioError.UseVisualStyleBackColor = True
        '
        'RadioWarn
        '
        Me.RadioWarn.AutoSize = True
        Me.RadioWarn.Location = New System.Drawing.Point(18, 92)
        Me.RadioWarn.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioWarn.Name = "RadioWarn"
        Me.RadioWarn.Size = New System.Drawing.Size(63, 21)
        Me.RadioWarn.TabIndex = 3
        Me.RadioWarn.TabStop = True
        Me.RadioWarn.Text = Global.Outworldz.My.Resources.Resources.Warn_word
        Me.RadioWarn.UseVisualStyleBackColor = True
        '
        'RadioInfo
        '
        Me.RadioInfo.AutoSize = True
        Me.RadioInfo.Location = New System.Drawing.Point(18, 64)
        Me.RadioInfo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioInfo.Name = "RadioInfo"
        Me.RadioInfo.Size = New System.Drawing.Size(52, 21)
        Me.RadioInfo.TabIndex = 2
        Me.RadioInfo.TabStop = True
        Me.RadioInfo.Text = Global.Outworldz.My.Resources.Resources.Info_word
        Me.RadioInfo.UseVisualStyleBackColor = True
        '
        'RadioDebug
        '
        Me.RadioDebug.AutoSize = True
        Me.RadioDebug.Location = New System.Drawing.Point(18, 35)
        Me.RadioDebug.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioDebug.Name = "RadioDebug"
        Me.RadioDebug.Size = New System.Drawing.Size(71, 21)
        Me.RadioDebug.TabIndex = 1
        Me.RadioDebug.TabStop = True
        Me.RadioDebug.Text = Global.Outworldz.My.Resources.Resources.Debug_word
        Me.RadioDebug.UseVisualStyleBackColor = True
        '
        'RadioOff
        '
        Me.RadioOff.AutoSize = True
        Me.RadioOff.Location = New System.Drawing.Point(18, 177)
        Me.RadioOff.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioOff.Name = "RadioOff"
        Me.RadioOff.Size = New System.Drawing.Size(48, 21)
        Me.RadioOff.TabIndex = 0
        Me.RadioOff.TabStop = True
        Me.RadioOff.Text = Global.Outworldz.My.Resources.Resources.Off
        Me.RadioOff.UseVisualStyleBackColor = True
        '
        'FormLogging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(165, 293)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FormLogging"
        Me.Text = "Logging"
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
    Friend WithEvents RadioFatal As RadioButton
    Friend WithEvents RadioError As RadioButton
    Friend WithEvents RadioWarn As RadioButton
    Friend WithEvents RadioInfo As RadioButton
    Friend WithEvents RadioDebug As RadioButton
    Friend WithEvents RadioOff As RadioButton
End Class
