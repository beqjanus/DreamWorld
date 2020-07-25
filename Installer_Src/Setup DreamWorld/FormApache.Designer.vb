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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.X86Button = New System.Windows.Forms.Button()
        Me.ApachePort = New System.Windows.Forms.TextBox()
        Me.ApacheServiceCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ApacheCheckbox = New System.Windows.Forms.CheckBox()
        Me.EventsCheckBox = New System.Windows.Forms.CheckBox()
        Me.EnableSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.AllGridSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Other = New System.Windows.Forms.TextBox()
        Me.EnableOther = New System.Windows.Forms.RadioButton()
        Me.EnableJOpensim = New System.Windows.Forms.RadioButton()
        Me.EnableDiva = New System.Windows.Forms.RadioButton()
        Me.EnableWP = New System.Windows.Forms.RadioButton()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.X86Button)
        Me.GroupBox2.Controls.Add(Me.ApachePort)
        Me.GroupBox2.Controls.Add(Me.ApacheServiceCheckBox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.ApacheCheckbox)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 29)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(220, 162)
        Me.GroupBox2.TabIndex = 186739
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Webserver + Search and Map"
        '
        'X86Button
        '
        Me.X86Button.Location = New System.Drawing.Point(16, 31)
        Me.X86Button.Name = "X86Button"
        Me.X86Button.Size = New System.Drawing.Size(199, 24)
        Me.X86Button.TabIndex = 186740
        Me.X86Button.Text = Global.Outworldz.My.Resources.Resources.InstallRuntime
        Me.X86Button.UseVisualStyleBackColor = True
        '
        'ApachePort
        '
        Me.ApachePort.Location = New System.Drawing.Point(15, 75)
        Me.ApachePort.Name = "ApachePort"
        Me.ApachePort.Size = New System.Drawing.Size(31, 20)
        Me.ApachePort.TabIndex = 186736
        '
        'ApacheServiceCheckBox
        '
        Me.ApacheServiceCheckBox.AutoSize = True
        Me.ApacheServiceCheckBox.Location = New System.Drawing.Point(16, 128)
        Me.ApacheServiceCheckBox.Name = "ApacheServiceCheckBox"
        Me.ApacheServiceCheckBox.Size = New System.Drawing.Size(110, 17)
        Me.ApacheServiceCheckBox.TabIndex = 1868
        Me.ApacheServiceCheckBox.Text = Global.Outworldz.My.Resources.Resources.Run_as_a_Service_word
        Me.ApacheServiceCheckBox.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(52, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 13)
        Me.Label3.TabIndex = 186737
        Me.Label3.Text = "Web Port (80, or 8000)"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox1.Location = New System.Drawing.Point(173, 101)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(21, 20)
        Me.PictureBox1.TabIndex = 1859
        Me.PictureBox1.TabStop = False
        '
        'ApacheCheckbox
        '
        Me.ApacheCheckbox.AutoSize = True
        Me.ApacheCheckbox.Location = New System.Drawing.Point(16, 106)
        Me.ApacheCheckbox.Name = "ApacheCheckbox"
        Me.ApacheCheckbox.Size = New System.Drawing.Size(157, 17)
        Me.ApacheCheckbox.TabIndex = 1866
        Me.ApacheCheckbox.Text = Global.Outworldz.My.Resources.Resources.EnableApache
        Me.ApacheCheckbox.UseVisualStyleBackColor = True
        '
        'EventsCheckBox
        '
        Me.EventsCheckBox.AutoSize = True
        Me.EventsCheckBox.Location = New System.Drawing.Point(14, 102)
        Me.EventsCheckBox.Name = "EventsCheckBox"
        Me.EventsCheckBox.Size = New System.Drawing.Size(95, 17)
        Me.EventsCheckBox.TabIndex = 186745
        Me.EventsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Enable_Events_word
        Me.EventsCheckBox.UseVisualStyleBackColor = True
        '
        'EnableSearchCheckBox
        '
        Me.EnableSearchCheckBox.AutoSize = True
        Me.EnableSearchCheckBox.Location = New System.Drawing.Point(14, 31)
        Me.EnableSearchCheckBox.Name = "EnableSearchCheckBox"
        Me.EnableSearchCheckBox.Size = New System.Drawing.Size(96, 17)
        Me.EnableSearchCheckBox.TabIndex = 186744
        Me.EnableSearchCheckBox.Text = Global.Outworldz.My.Resources.Resources.Enable_Search_word
        Me.EnableSearchCheckBox.UseVisualStyleBackColor = True
        '
        'AllGridSearchCheckBox
        '
        Me.AllGridSearchCheckBox.AutoSize = True
        Me.AllGridSearchCheckBox.Location = New System.Drawing.Point(14, 78)
        Me.AllGridSearchCheckBox.Name = "AllGridSearchCheckBox"
        Me.AllGridSearchCheckBox.Size = New System.Drawing.Size(101, 17)
        Me.AllGridSearchCheckBox.TabIndex = 186743
        Me.AllGridSearchCheckBox.Text = Global.Outworldz.My.Resources.Resources.Search_All_Grids_word
        Me.AllGridSearchCheckBox.UseVisualStyleBackColor = True
        '
        'LocalSearchCheckBox
        '
        Me.LocalSearchCheckBox.AutoSize = True
        Me.LocalSearchCheckBox.Location = New System.Drawing.Point(14, 54)
        Me.LocalSearchCheckBox.Name = "LocalSearchCheckBox"
        Me.LocalSearchCheckBox.Size = New System.Drawing.Size(89, 17)
        Me.LocalSearchCheckBox.TabIndex = 186742
        Me.LocalSearchCheckBox.Text = Global.Outworldz.My.Resources.Resources.Local_Search
        Me.LocalSearchCheckBox.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(574, 26)
        Me.MenuStrip1.TabIndex = 186740
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ApacheToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'ApacheToolStripMenuItem
        '
        Me.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ApacheToolStripMenuItem.Name = "ApacheToolStripMenuItem"
        Me.ApacheToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.ApacheToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Apache_word
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.EventsCheckBox)
        Me.GroupBox1.Controls.Add(Me.AllGridSearchCheckBox)
        Me.GroupBox1.Controls.Add(Me.EnableSearchCheckBox)
        Me.GroupBox1.Controls.Add(Me.LocalSearchCheckBox)
        Me.GroupBox1.Location = New System.Drawing.Point(235, 29)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(150, 162)
        Me.GroupBox1.TabIndex = 186741
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Other)
        Me.GroupBox3.Controls.Add(Me.EnableOther)
        Me.GroupBox3.Controls.Add(Me.EnableJOpensim)
        Me.GroupBox3.Controls.Add(Me.EnableDiva)
        Me.GroupBox3.Controls.Add(Me.EnableWP)
        Me.GroupBox3.Location = New System.Drawing.Point(403, 28)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Size = New System.Drawing.Size(150, 162)
        Me.GroupBox3.TabIndex = 186742
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "CMS"
        '
        'Other
        '
        Me.Other.Location = New System.Drawing.Point(5, 125)
        Me.Other.Name = "Other"
        Me.Other.Size = New System.Drawing.Size(140, 20)
        Me.Other.TabIndex = 186741
        '
        'EnableOther
        '
        Me.EnableOther.AutoSize = True
        Me.EnableOther.Location = New System.Drawing.Point(14, 102)
        Me.EnableOther.Name = "EnableOther"
        Me.EnableOther.Size = New System.Drawing.Size(90, 17)
        Me.EnableOther.TabIndex = 186745
        Me.EnableOther.Text = "Enable Other:"
        Me.EnableOther.UseVisualStyleBackColor = True
        '
        'EnableJOpensim
        '
        Me.EnableJOpensim.AutoSize = True
        Me.EnableJOpensim.Location = New System.Drawing.Point(14, 78)
        Me.EnableJOpensim.Name = "EnableJOpensim"
        Me.EnableJOpensim.Size = New System.Drawing.Size(107, 17)
        Me.EnableJOpensim.TabIndex = 186743
        Me.EnableJOpensim.Text = "Enable JOpensim"
        Me.EnableJOpensim.UseVisualStyleBackColor = True
        '
        'EnableDiva
        '
        Me.EnableDiva.AutoSize = True
        Me.EnableDiva.Location = New System.Drawing.Point(14, 31)
        Me.EnableDiva.Name = "EnableDiva"
        Me.EnableDiva.Size = New System.Drawing.Size(113, 17)
        Me.EnableDiva.TabIndex = 186744
        Me.EnableDiva.Text = "Enable Diva Panel"
        Me.EnableDiva.UseVisualStyleBackColor = True
        '
        'EnableWP
        '
        Me.EnableWP.AutoSize = True
        Me.EnableWP.Location = New System.Drawing.Point(14, 54)
        Me.EnableWP.Name = "EnableWP"
        Me.EnableWP.Size = New System.Drawing.Size(112, 17)
        Me.EnableWP.TabIndex = 186742
        Me.EnableWP.Text = "Enable Wordpress"
        Me.EnableWP.UseVisualStyleBackColor = True
        '
        'FormApache
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 205)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "FormApache"
        Me.Text = "Apache Webserver"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents EventsCheckBox As CheckBox
    Friend WithEvents EnableSearchCheckBox As CheckBox
    Friend WithEvents AllGridSearchCheckBox As CheckBox
    Friend WithEvents LocalSearchCheckBox As CheckBox
    Friend WithEvents X86Button As Button
    Friend WithEvents ApachePort As TextBox
    Friend WithEvents ApacheServiceCheckBox As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ApacheCheckbox As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ApacheToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Other As TextBox
    Friend WithEvents EnableOther As RadioButton
    Friend WithEvents EnableJOpensim As RadioButton
    Friend WithEvents EnableDiva As RadioButton
    Friend WithEvents EnableWP As RadioButton
End Class
