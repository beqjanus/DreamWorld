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
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(254, 36)
        Me.MenuStrip1.TabIndex = 186741
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(102, 34)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioFatal)
        Me.GroupBox1.Controls.Add(Me.RadioError)
        Me.GroupBox1.Controls.Add(Me.RadioWarn)
        Me.GroupBox1.Controls.Add(Me.RadioInfo)
        Me.GroupBox1.Controls.Add(Me.RadioDebug)
        Me.GroupBox1.Controls.Add(Me.RadioOff)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 86)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(176, 334)
        Me.GroupBox1.TabIndex = 186742
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Logging"
        '
        'RadioFatal
        '
        Me.RadioFatal.AutoSize = True
        Me.RadioFatal.Location = New System.Drawing.Point(24, 222)
        Me.RadioFatal.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RadioFatal.Name = "RadioFatal"
        Me.RadioFatal.Size = New System.Drawing.Size(80, 29)
        Me.RadioFatal.TabIndex = 5
        Me.RadioFatal.TabStop = True
        Me.RadioFatal.Text = Global.Outworldz.My.Resources.Resources.Fatal_word
        Me.RadioFatal.UseVisualStyleBackColor = True
        '
        'RadioError
        '
        Me.RadioError.AutoSize = True
        Me.RadioError.Location = New System.Drawing.Point(24, 180)
        Me.RadioError.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RadioError.Name = "RadioError"
        Me.RadioError.Size = New System.Drawing.Size(79, 29)
        Me.RadioError.TabIndex = 4
        Me.RadioError.TabStop = True
        Me.RadioError.Text = Global.Outworldz.My.Resources.Resources.Error_word
        Me.RadioError.UseVisualStyleBackColor = True
        '
        'RadioWarn
        '
        Me.RadioWarn.AutoSize = True
        Me.RadioWarn.Location = New System.Drawing.Point(24, 138)
        Me.RadioWarn.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RadioWarn.Name = "RadioWarn"
        Me.RadioWarn.Size = New System.Drawing.Size(85, 29)
        Me.RadioWarn.TabIndex = 3
        Me.RadioWarn.TabStop = True
        Me.RadioWarn.Text = Global.Outworldz.My.Resources.Resources.Warn_word
        Me.RadioWarn.UseVisualStyleBackColor = True
        '
        'RadioInfo
        '
        Me.RadioInfo.AutoSize = True
        Me.RadioInfo.Location = New System.Drawing.Point(24, 96)
        Me.RadioInfo.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RadioInfo.Name = "RadioInfo"
        Me.RadioInfo.Size = New System.Drawing.Size(69, 29)
        Me.RadioInfo.TabIndex = 2
        Me.RadioInfo.TabStop = True
        Me.RadioInfo.Text = Global.Outworldz.My.Resources.Resources.Info_word
        Me.RadioInfo.UseVisualStyleBackColor = True
        '
        'RadioDebug
        '
        Me.RadioDebug.AutoSize = True
        Me.RadioDebug.Location = New System.Drawing.Point(24, 53)
        Me.RadioDebug.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RadioDebug.Name = "RadioDebug"
        Me.RadioDebug.Size = New System.Drawing.Size(95, 29)
        Me.RadioDebug.TabIndex = 1
        Me.RadioDebug.TabStop = True
        Me.RadioDebug.Text = Global.Outworldz.My.Resources.Resources.Debug_word
        Me.RadioDebug.UseVisualStyleBackColor = True
        '
        'RadioOff
        '
        Me.RadioOff.AutoSize = True
        Me.RadioOff.Location = New System.Drawing.Point(24, 265)
        Me.RadioOff.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.RadioOff.Name = "RadioOff"
        Me.RadioOff.Size = New System.Drawing.Size(63, 29)
        Me.RadioOff.TabIndex = 0
        Me.RadioOff.TabStop = True
        Me.RadioOff.Text = Global.Outworldz.My.Resources.Resources.Off
        Me.RadioOff.UseVisualStyleBackColor = True
        '
        'FormLogging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(254, 439)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
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
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioFatal As RadioButton
    Friend WithEvents RadioError As RadioButton
    Friend WithEvents RadioWarn As RadioButton
    Friend WithEvents RadioInfo As RadioButton
    Friend WithEvents RadioDebug As RadioButton
    Friend WithEvents RadioOff As RadioButton
End Class
