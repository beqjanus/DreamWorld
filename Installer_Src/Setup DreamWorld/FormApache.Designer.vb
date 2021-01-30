<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormApache
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Sitemap = New System.Windows.Forms.CheckBox()
        Me.ApachePort = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ApacheCheckbox = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Other = New System.Windows.Forms.TextBox()
        Me.EnableOther = New System.Windows.Forms.RadioButton()
        Me.EnableJOpensim = New System.Windows.Forms.RadioButton()
        Me.EnableDiva = New System.Windows.Forms.RadioButton()
        Me.EnableWP = New System.Windows.Forms.RadioButton()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Sitemap)
        Me.GroupBox2.Controls.Add(Me.ApachePort)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.ApacheCheckbox)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 45)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(330, 181)
        Me.GroupBox2.TabIndex = 186739
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Apache"
        '
        'Sitemap
        '
        Me.Sitemap.AutoSize = True
        Me.Sitemap.Location = New System.Drawing.Point(24, 131)
        Me.Sitemap.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Sitemap.Name = "Sitemap"
        Me.Sitemap.Size = New System.Drawing.Size(174, 24)
        Me.Sitemap.TabIndex = 186741
        Me.Sitemap.Text = "Automatic Site Map"
        Me.Sitemap.UseVisualStyleBackColor = True
        '
        'ApachePort
        '
        Me.ApachePort.Location = New System.Drawing.Point(24, 44)
        Me.ApachePort.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ApachePort.Name = "ApachePort"
        Me.ApachePort.Size = New System.Drawing.Size(44, 26)
        Me.ApachePort.TabIndex = 186736
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(83, 47)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(169, 20)
        Me.Label3.TabIndex = 186737
        Me.Label3.Text = "Web Port (80, or 8000)"
        '
        'ApacheCheckbox
        '
        Me.ApacheCheckbox.AutoSize = True
        Me.ApacheCheckbox.Location = New System.Drawing.Point(24, 95)
        Me.ApacheCheckbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ApacheCheckbox.Name = "ApacheCheckbox"
        Me.ApacheCheckbox.Size = New System.Drawing.Size(228, 24)
        Me.ApacheCheckbox.TabIndex = 1866
        Me.ApacheCheckbox.Text = Global.Outworldz.My.Resources.Resources.EnableApache
        Me.ApacheCheckbox.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(16, 16)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(6, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(622, 31)
        Me.MenuStrip1.TabIndex = 186740
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ApacheToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(85, 29)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'ApacheToolStripMenuItem
        '
        Me.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ApacheToolStripMenuItem.Name = "ApacheToolStripMenuItem"
        Me.ApacheToolStripMenuItem.Size = New System.Drawing.Size(270, 34)
        Me.ApacheToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Apache_word
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Other)
        Me.GroupBox3.Controls.Add(Me.EnableOther)
        Me.GroupBox3.Controls.Add(Me.EnableJOpensim)
        Me.GroupBox3.Controls.Add(Me.EnableDiva)
        Me.GroupBox3.Controls.Add(Me.EnableWP)
        Me.GroupBox3.Location = New System.Drawing.Point(352, 42)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(225, 249)
        Me.GroupBox3.TabIndex = 186742
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Content Manager"
        '
        'Other
        '
        Me.Other.Location = New System.Drawing.Point(8, 192)
        Me.Other.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Other.Name = "Other"
        Me.Other.Size = New System.Drawing.Size(208, 26)
        Me.Other.TabIndex = 186741
        '
        'EnableOther
        '
        Me.EnableOther.AutoSize = True
        Me.EnableOther.Location = New System.Drawing.Point(21, 158)
        Me.EnableOther.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EnableOther.Name = "EnableOther"
        Me.EnableOther.Size = New System.Drawing.Size(128, 24)
        Me.EnableOther.TabIndex = 186745
        Me.EnableOther.Text = Global.Outworldz.My.Resources.Resources.EnableOther_Word
        Me.EnableOther.UseVisualStyleBackColor = True
        '
        'EnableJOpensim
        '
        Me.EnableJOpensim.AutoSize = True
        Me.EnableJOpensim.Location = New System.Drawing.Point(21, 120)
        Me.EnableJOpensim.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EnableJOpensim.Name = "EnableJOpensim"
        Me.EnableJOpensim.Size = New System.Drawing.Size(105, 24)
        Me.EnableJOpensim.TabIndex = 186743
        Me.EnableJOpensim.Text = Global.Outworldz.My.Resources.Resources.JOpensim_word
        Me.EnableJOpensim.UseVisualStyleBackColor = True
        '
        'EnableDiva
        '
        Me.EnableDiva.AutoSize = True
        Me.EnableDiva.Location = New System.Drawing.Point(21, 48)
        Me.EnableDiva.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EnableDiva.Name = "EnableDiva"
        Me.EnableDiva.Size = New System.Drawing.Size(160, 24)
        Me.EnableDiva.TabIndex = 186744
        Me.EnableDiva.Text = Global.Outworldz.My.Resources.Resources.EnableDiva
        Me.EnableDiva.UseVisualStyleBackColor = True
        '
        'EnableWP
        '
        Me.EnableWP.AutoSize = True
        Me.EnableWP.Location = New System.Drawing.Point(21, 82)
        Me.EnableWP.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EnableWP.Name = "EnableWP"
        Me.EnableWP.Size = New System.Drawing.Size(112, 24)
        Me.EnableWP.TabIndex = 186742
        Me.EnableWP.Text = Global.Outworldz.My.Resources.Resources.WordPress_Word
        Me.EnableWP.UseVisualStyleBackColor = True
        '
        'FormApache
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 315)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FormApache"
        Me.Text = "Apache"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ApachePort As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ApacheCheckbox As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ApacheToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Other As TextBox
    Friend WithEvents EnableOther As RadioButton
    Friend WithEvents EnableJOpensim As RadioButton
    Friend WithEvents EnableDiva As RadioButton
    Friend WithEvents EnableWP As RadioButton
    Friend WithEvents Sitemap As CheckBox
End Class
