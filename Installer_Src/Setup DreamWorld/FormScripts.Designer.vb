﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormScripts
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
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.Control.set_Text(System.String)")>
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.YengineButton = New System.Windows.Forms.RadioButton()
        Me.XengineButton = New System.Windows.Forms.RadioButton()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.LSLCheckbox = New System.Windows.Forms.CheckBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.YengineButton)
        Me.GroupBox1.Controls.Add(Me.XengineButton)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 77)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(383, 142)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Script Engine"
        '
        'YengineButton
        '
        Me.YengineButton.AutoSize = True
        Me.YengineButton.Location = New System.Drawing.Point(18, 83)
        Me.YengineButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.YengineButton.Name = "YengineButton"
        Me.YengineButton.Size = New System.Drawing.Size(99, 24)
        Me.YengineButton.TabIndex = 1
        Me.YengineButton.TabStop = True
        Me.YengineButton.Text = Global.Outworldz.My.Resources.Resources.YEngine_word
        Me.YengineButton.UseVisualStyleBackColor = True
        '
        'XengineButton
        '
        Me.XengineButton.AutoSize = True
        Me.XengineButton.Location = New System.Drawing.Point(18, 47)
        Me.XengineButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.XengineButton.Name = "XengineButton"
        Me.XengineButton.Size = New System.Drawing.Size(99, 24)
        Me.XengineButton.TabIndex = 0
        Me.XengineButton.TabStop = True
        Me.XengineButton.Text = Global.Outworldz.My.Resources.Resources.XEngine_word
        Me.XengineButton.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.LSLCheckbox)
        Me.GroupBox8.Location = New System.Drawing.Point(18, 227)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox8.Size = New System.Drawing.Size(383, 74)
        Me.GroupBox8.TabIndex = 1863
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Allow LSL to contact the server"
        '
        'LSLCheckbox
        '
        Me.LSLCheckbox.AutoSize = True
        Me.LSLCheckbox.Location = New System.Drawing.Point(18, 29)
        Me.LSLCheckbox.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LSLCheckbox.Name = "LSLCheckbox"
        Me.LSLCheckbox.Size = New System.Drawing.Size(85, 24)
        Me.LSLCheckbox.TabIndex = 210
        Me.LSLCheckbox.Text = Global.Outworldz.My.Resources.Resources.Enable_word
        Me.LSLCheckbox.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(9, 2, 0, 2)
        Me.MenuStrip2.Size = New System.Drawing.Size(419, 33)
        Me.MenuStrip2.TabIndex = 1890
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(89, 29)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormScripts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(419, 332)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Name = "FormScripts"
        Me.Text = "Scripts"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents YengineButton As RadioButton
    Friend WithEvents XengineButton As RadioButton
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents LSLCheckbox As CheckBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
End Class
