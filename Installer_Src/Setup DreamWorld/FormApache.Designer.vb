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
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.GroupBox2.Location = New System.Drawing.Point(15, 45)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(330, 249)
        Me.GroupBox2.TabIndex = 186739
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Webserver + Search and Map"
        '
        'X86Button
        '
        Me.X86Button.Location = New System.Drawing.Point(24, 48)
        Me.X86Button.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.X86Button.Name = "X86Button"
        Me.X86Button.Size = New System.Drawing.Size(298, 37)
        Me.X86Button.TabIndex = 186740
        Me.X86Button.Text = Global.Outworldz.My.Resources.Resources.InstallRuntime
        Me.X86Button.UseVisualStyleBackColor = True
        '
        'ApachePort
        '
        Me.ApachePort.Location = New System.Drawing.Point(22, 115)
        Me.ApachePort.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ApachePort.Name = "ApachePort"
        Me.ApachePort.Size = New System.Drawing.Size(44, 26)
        Me.ApachePort.TabIndex = 186736
        '
        'ApacheServiceCheckBox
        '
        Me.ApacheServiceCheckBox.AutoSize = True
        Me.ApacheServiceCheckBox.Location = New System.Drawing.Point(24, 197)
        Me.ApacheServiceCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ApacheServiceCheckBox.Name = "ApacheServiceCheckBox"
        Me.ApacheServiceCheckBox.Size = New System.Drawing.Size(159, 24)
        Me.ApacheServiceCheckBox.TabIndex = 1868
        Me.ApacheServiceCheckBox.Text = Global.Outworldz.My.Resources.Resources.Run_as_a_Service_word
        Me.ApacheServiceCheckBox.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(78, 118)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(169, 20)
        Me.Label3.TabIndex = 186737
        Me.Label3.Text = "Web Port (80, or 8000)"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox1.Location = New System.Drawing.Point(260, 156)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(31, 31)
        Me.PictureBox1.TabIndex = 1859
        Me.PictureBox1.TabStop = False
        '
        'ApacheCheckbox
        '
        Me.ApacheCheckbox.AutoSize = True
        Me.ApacheCheckbox.Location = New System.Drawing.Point(24, 163)
        Me.ApacheCheckbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ApacheCheckbox.Name = "ApacheCheckbox"
        Me.ApacheCheckbox.Size = New System.Drawing.Size(228, 24)
        Me.ApacheCheckbox.TabIndex = 1866
        Me.ApacheCheckbox.Text = Global.Outworldz.My.Resources.Resources.EnableApache
        Me.ApacheCheckbox.UseVisualStyleBackColor = True
        '
        'EventsCheckBox
        '
        Me.EventsCheckBox.AutoSize = True
        Me.EventsCheckBox.Location = New System.Drawing.Point(21, 157)
        Me.EventsCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EventsCheckBox.Name = "EventsCheckBox"
        Me.EventsCheckBox.Size = New System.Drawing.Size(138, 24)
        Me.EventsCheckBox.TabIndex = 186745
        Me.EventsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Enable_Events
        Me.EventsCheckBox.UseVisualStyleBackColor = True
        '
        'EnableSearchCheckBox
        '
        Me.EnableSearchCheckBox.AutoSize = True
        Me.EnableSearchCheckBox.Location = New System.Drawing.Point(21, 48)
        Me.EnableSearchCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EnableSearchCheckBox.Name = "EnableSearchCheckBox"
        Me.EnableSearchCheckBox.Size = New System.Drawing.Size(140, 24)
        Me.EnableSearchCheckBox.TabIndex = 186744
        Me.EnableSearchCheckBox.Text = Global.Outworldz.My.Resources.Resources.Enable_Search
        Me.EnableSearchCheckBox.UseVisualStyleBackColor = True
        '
        'AllGridSearchCheckBox
        '
        Me.AllGridSearchCheckBox.AutoSize = True
        Me.AllGridSearchCheckBox.Location = New System.Drawing.Point(21, 120)
        Me.AllGridSearchCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.AllGridSearchCheckBox.Name = "AllGridSearchCheckBox"
        Me.AllGridSearchCheckBox.Size = New System.Drawing.Size(149, 24)
        Me.AllGridSearchCheckBox.TabIndex = 186743
        Me.AllGridSearchCheckBox.Text = Global.Outworldz.My.Resources.Resources.Search_All_Grids_word
        Me.AllGridSearchCheckBox.UseVisualStyleBackColor = True
        '
        'LocalSearchCheckBox
        '
        Me.LocalSearchCheckBox.AutoSize = True
        Me.LocalSearchCheckBox.Location = New System.Drawing.Point(21, 83)
        Me.LocalSearchCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LocalSearchCheckBox.Name = "LocalSearchCheckBox"
        Me.LocalSearchCheckBox.Size = New System.Drawing.Size(128, 24)
        Me.LocalSearchCheckBox.TabIndex = 186742
        Me.LocalSearchCheckBox.Text = Global.Outworldz.My.Resources.Resources.Local_Search
        Me.LocalSearchCheckBox.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(592, 33)
        Me.MenuStrip1.TabIndex = 186740
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ApacheToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(85, 29)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'ApacheToolStripMenuItem
        '
        Me.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ApacheToolStripMenuItem.Name = "ApacheToolStripMenuItem"
        Me.ApacheToolStripMenuItem.Size = New System.Drawing.Size(173, 34)
        Me.ApacheToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Apache_word
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.EventsCheckBox)
        Me.GroupBox1.Controls.Add(Me.AllGridSearchCheckBox)
        Me.GroupBox1.Controls.Add(Me.EnableSearchCheckBox)
        Me.GroupBox1.Controls.Add(Me.LocalSearchCheckBox)
        Me.GroupBox1.Location = New System.Drawing.Point(352, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(225, 249)
        Me.GroupBox1.TabIndex = 186741
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search"
        '
        'FormApache
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 315)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "FormApache"
        Me.Text = "Apache Webserver"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
End Class
