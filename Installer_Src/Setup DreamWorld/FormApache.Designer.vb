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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormApache))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SSLButton = New System.Windows.Forms.Button()
        Me.Sitemap = New System.Windows.Forms.CheckBox()
        Me.ApachePort = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ApacheCheckbox = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Other = New System.Windows.Forms.TextBox()
        Me.EnableOther = New System.Windows.Forms.RadioButton()
        Me.EnableJOpensim = New System.Windows.Forms.RadioButton()
        Me.EnableDiva = New System.Windows.Forms.RadioButton()
        Me.EnableWP = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.SSLButton)
        Me.GroupBox2.Controls.Add(Me.Sitemap)
        Me.GroupBox2.Controls.Add(Me.ApachePort)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.ApacheCheckbox)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 37)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(197, 156)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Apache"
        '
        'SSLButton
        '
        Me.SSLButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SSLButton.Image = CType(resources.GetObject("SSLButton.Image"), System.Drawing.Image)
        Me.SSLButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SSLButton.Location = New System.Drawing.Point(16, 110)
        Me.SSLButton.Name = "SSLButton"
        Me.SSLButton.Size = New System.Drawing.Size(128, 33)
        Me.SSLButton.TabIndex = 30
        Me.SSLButton.Text = "SSL"
        Me.ToolTip1.SetToolTip(Me.SSLButton, "!!!")
        Me.SSLButton.UseVisualStyleBackColor = True
        '
        'Sitemap
        '
        Me.Sitemap.AutoSize = True
        Me.Sitemap.Location = New System.Drawing.Point(16, 85)
        Me.Sitemap.Name = "Sitemap"
        Me.Sitemap.Size = New System.Drawing.Size(118, 17)
        Me.Sitemap.TabIndex = 2
        Me.Sitemap.Text = My.Resources.AutomaticSiteMap
        Me.Sitemap.UseVisualStyleBackColor = True
        '
        'ApachePort
        '
        Me.ApachePort.Location = New System.Drawing.Point(16, 29)
        Me.ApachePort.Name = "ApachePort"
        Me.ApachePort.Size = New System.Drawing.Size(31, 20)
        Me.ApachePort.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(55, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = My.Resources.WebPort
        '
        'ApacheCheckbox
        '
        Me.ApacheCheckbox.AutoSize = True
        Me.ApacheCheckbox.Location = New System.Drawing.Point(16, 62)
        Me.ApacheCheckbox.Name = "ApacheCheckbox"
        Me.ApacheCheckbox.Size = New System.Drawing.Size(157, 17)
        Me.ApacheCheckbox.TabIndex = 1
        Me.ApacheCheckbox.Text = Global.Outworldz.My.Resources.Resources.EnableApache
        Me.ApacheCheckbox.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(491, 34)
        Me.MenuStrip1.TabIndex = 2
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(72, 32)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Other)
        Me.GroupBox3.Controls.Add(Me.EnableOther)
        Me.GroupBox3.Controls.Add(Me.EnableJOpensim)
        Me.GroupBox3.Controls.Add(Me.EnableDiva)
        Me.GroupBox3.Controls.Add(Me.EnableWP)
        Me.GroupBox3.Location = New System.Drawing.Point(237, 35)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.GroupBox3.Size = New System.Drawing.Size(224, 150)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = My.Resources.ContentManager
        '
        'Other
        '
        Me.Other.Location = New System.Drawing.Point(5, 125)
        Me.Other.Name = "Other"
        Me.Other.Size = New System.Drawing.Size(181, 20)
        Me.Other.TabIndex = 4
        '
        'EnableOther
        '
        Me.EnableOther.AutoSize = True
        Me.EnableOther.Location = New System.Drawing.Point(14, 103)
        Me.EnableOther.Name = "EnableOther"
        Me.EnableOther.Size = New System.Drawing.Size(87, 17)
        Me.EnableOther.TabIndex = 3
        Me.EnableOther.Text = Global.Outworldz.My.Resources.Resources.EnableOther_Word
        Me.EnableOther.UseVisualStyleBackColor = True
        '
        'EnableJOpensim
        '
        Me.EnableJOpensim.AutoSize = True
        Me.EnableJOpensim.Location = New System.Drawing.Point(14, 78)
        Me.EnableJOpensim.Name = "EnableJOpensim"
        Me.EnableJOpensim.Size = New System.Drawing.Size(71, 17)
        Me.EnableJOpensim.TabIndex = 2
        Me.EnableJOpensim.Text = Global.Outworldz.My.Resources.Resources.JOpensim_word
        Me.EnableJOpensim.UseVisualStyleBackColor = True
        '
        'EnableDiva
        '
        Me.EnableDiva.AutoSize = True
        Me.EnableDiva.Location = New System.Drawing.Point(14, 31)
        Me.EnableDiva.Name = "EnableDiva"
        Me.EnableDiva.Size = New System.Drawing.Size(111, 17)
        Me.EnableDiva.TabIndex = 0
        Me.EnableDiva.Text = Global.Outworldz.My.Resources.Resources.EnableDiva
        Me.EnableDiva.UseVisualStyleBackColor = True
        '
        'EnableWP
        '
        Me.EnableWP.AutoSize = True
        Me.EnableWP.Location = New System.Drawing.Point(14, 53)
        Me.EnableWP.Name = "EnableWP"
        Me.EnableWP.Size = New System.Drawing.Size(77, 17)
        Me.EnableWP.TabIndex = 1
        Me.EnableWP.Text = Global.Outworldz.My.Resources.Resources.WordPress_Word
        Me.EnableWP.UseVisualStyleBackColor = True
        '
        'FormApache
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(491, 205)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
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
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Other As TextBox
    Friend WithEvents EnableOther As RadioButton
    Friend WithEvents EnableJOpensim As RadioButton
    Friend WithEvents EnableDiva As RadioButton
    Friend WithEvents EnableWP As RadioButton
    Friend WithEvents Sitemap As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SSLButton As Button
End Class
