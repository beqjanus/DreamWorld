<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSmartStart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSmartStart))
        Me.SmartStartEnabled = New System.Windows.Forms.CheckBox()
        Me.Seconds = New System.Windows.Forms.TextBox()
        Me.DelayLabel = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Waitcheck = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SmartStartEnabled
        '
        Me.SmartStartEnabled.AutoSize = True
        Me.SmartStartEnabled.Location = New System.Drawing.Point(39, 27)
        Me.SmartStartEnabled.Margin = New System.Windows.Forms.Padding(1)
        Me.SmartStartEnabled.Name = "SmartStartEnabled"
        Me.SmartStartEnabled.Size = New System.Drawing.Size(114, 17)
        Me.SmartStartEnabled.TabIndex = 12
        Me.SmartStartEnabled.Text = Global.Outworldz.My.Resources.Resources.Smart_Start_Enable_word
        Me.SmartStartEnabled.UseVisualStyleBackColor = True
        '
        'Seconds
        '
        Me.Seconds.Location = New System.Drawing.Point(32, 89)
        Me.Seconds.Margin = New System.Windows.Forms.Padding(1)
        Me.Seconds.Name = "Seconds"
        Me.Seconds.Size = New System.Drawing.Size(40, 20)
        Me.Seconds.TabIndex = 13
        '
        'DelayLabel
        '
        Me.DelayLabel.AutoSize = True
        Me.DelayLabel.Location = New System.Drawing.Point(76, 89)
        Me.DelayLabel.Name = "DelayLabel"
        Me.DelayLabel.Size = New System.Drawing.Size(39, 13)
        Me.DelayLabel.TabIndex = 14
        Me.DelayLabel.Text = "Label1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Waitcheck)
        Me.GroupBox1.Controls.Add(Me.SmartStartEnabled)
        Me.GroupBox1.Controls.Add(Me.DelayLabel)
        Me.GroupBox1.Controls.Add(Me.Seconds)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(257, 144)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'Waitcheck
        '
        Me.Waitcheck.AutoSize = True
        Me.Waitcheck.Location = New System.Drawing.Point(39, 57)
        Me.Waitcheck.Margin = New System.Windows.Forms.Padding(1)
        Me.Waitcheck.Name = "Waitcheck"
        Me.Waitcheck.Size = New System.Drawing.Size(96, 17)
        Me.Waitcheck.TabIndex = 15
        Me.Waitcheck.Text = "Wait for scripts"

        Me.Waitcheck.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(286, 24)
        Me.MenuStrip1.TabIndex = 16
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'FormSmartStart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(286, 203)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormSmartStart"
        Me.Text = "Smart Start"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SmartStartEnabled As CheckBox
    Friend WithEvents Seconds As TextBox
    Friend WithEvents DelayLabel As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Waitcheck As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
