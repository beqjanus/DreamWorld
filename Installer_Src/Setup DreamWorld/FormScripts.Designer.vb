<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormScripts))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.YengineButton = New System.Windows.Forms.RadioButton()
        Me.XengineButton = New System.Windows.Forms.RadioButton()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.LSLCheckbox = New System.Windows.Forms.CheckBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsOff = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ScriptsOff)
        Me.GroupBox1.Controls.Add(Me.YengineButton)
        Me.GroupBox1.Controls.Add(Me.XengineButton)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(255, 93)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Script Engine"
        '
        'YengineButton
        '
        Me.YengineButton.AutoSize = True
        Me.YengineButton.Location = New System.Drawing.Point(12, 70)
        Me.YengineButton.Name = "YengineButton"
        Me.YengineButton.Size = New System.Drawing.Size(68, 17)
        Me.YengineButton.TabIndex = 1
        Me.YengineButton.TabStop = True
        Me.YengineButton.Text = Global.Outworldz.My.Resources.Resources.YEngine_word
        Me.YengineButton.UseVisualStyleBackColor = True
        '
        'XengineButton
        '
        Me.XengineButton.AutoSize = True
        Me.XengineButton.Location = New System.Drawing.Point(12, 47)
        Me.XengineButton.Name = "XengineButton"
        Me.XengineButton.Size = New System.Drawing.Size(68, 17)
        Me.XengineButton.TabIndex = 0
        Me.XengineButton.TabStop = True
        Me.XengineButton.Text = Global.Outworldz.My.Resources.Resources.XEngine_word
        Me.XengineButton.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.LSLCheckbox)
        Me.GroupBox8.Location = New System.Drawing.Point(12, 148)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(255, 48)
        Me.GroupBox8.TabIndex = 2
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Allow LSL to contact the server"
        '
        'LSLCheckbox
        '
        Me.LSLCheckbox.AutoSize = True
        Me.LSLCheckbox.Location = New System.Drawing.Point(12, 19)
        Me.LSLCheckbox.Name = "LSLCheckbox"
        Me.LSLCheckbox.Size = New System.Drawing.Size(59, 17)
        Me.LSLCheckbox.TabIndex = 0
        Me.LSLCheckbox.Text = Global.Outworldz.My.Resources.Resources.Enable_word
        Me.LSLCheckbox.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(279, 32)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(68, 28)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'ScriptsOff
        '
        Me.ScriptsOff.AutoSize = True
        Me.ScriptsOff.Location = New System.Drawing.Point(12, 24)
        Me.ScriptsOff.Name = "ScriptsOff"
        Me.ScriptsOff.Size = New System.Drawing.Size(39, 17)
        Me.ScriptsOff.TabIndex = 2
        Me.ScriptsOff.TabStop = True
        Me.ScriptsOff.Text = "Off"
        Me.ScriptsOff.UseVisualStyleBackColor = True
        '
        'FormScripts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(279, 216)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
    Friend WithEvents ScriptsOff As RadioButton
End Class
