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
        Me.EventsCheckBox = New System.Windows.Forms.CheckBox()
        Me.EnableSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.AllGridSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.LocalSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.X86Button = New System.Windows.Forms.Button()
        Me.ApachePort = New System.Windows.Forms.TextBox()
        Me.ApacheServiceCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ApacheCheckbox = New System.Windows.Forms.CheckBox()
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
        Me.GroupBox2.Location = New System.Drawing.Point(13, 36)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(293, 200)
        Me.GroupBox2.TabIndex = 186739
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Apache Webserver + Search and Map"
        '
        'EventsCheckBox
        '
        Me.EventsCheckBox.AutoSize = True
        Me.EventsCheckBox.Location = New System.Drawing.Point(18, 125)
        Me.EventsCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.EventsCheckBox.Name = "EventsCheckBox"
        Me.EventsCheckBox.Size = New System.Drawing.Size(121, 21)
        Me.EventsCheckBox.TabIndex = 186745
        Me.EventsCheckBox.Text = "Enable Events"
        Me.EventsCheckBox.UseVisualStyleBackColor = True
        '
        'EnableSearchCheckBox
        '
        Me.EnableSearchCheckBox.AutoSize = True
        Me.EnableSearchCheckBox.Location = New System.Drawing.Point(18, 38)
        Me.EnableSearchCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.EnableSearchCheckBox.Name = "EnableSearchCheckBox"
        Me.EnableSearchCheckBox.Size = New System.Drawing.Size(123, 21)
        Me.EnableSearchCheckBox.TabIndex = 186744
        Me.EnableSearchCheckBox.Text = "Enable Search"
        Me.EnableSearchCheckBox.UseVisualStyleBackColor = True
        '
        'AllGridSearchCheckBox
        '
        Me.AllGridSearchCheckBox.AutoSize = True
        Me.AllGridSearchCheckBox.Location = New System.Drawing.Point(18, 96)
        Me.AllGridSearchCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AllGridSearchCheckBox.Name = "AllGridSearchCheckBox"
        Me.AllGridSearchCheckBox.Size = New System.Drawing.Size(129, 21)
        Me.AllGridSearchCheckBox.TabIndex = 186743
        Me.AllGridSearchCheckBox.Text = "Search All grids"
        Me.AllGridSearchCheckBox.UseVisualStyleBackColor = True
        '
        'LocalSearchCheckBox
        '
        Me.LocalSearchCheckBox.AutoSize = True
        Me.LocalSearchCheckBox.Location = New System.Drawing.Point(18, 67)
        Me.LocalSearchCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.LocalSearchCheckBox.Name = "LocalSearchCheckBox"
        Me.LocalSearchCheckBox.Size = New System.Drawing.Size(113, 21)
        Me.LocalSearchCheckBox.TabIndex = 186742
        Me.LocalSearchCheckBox.Text = "Local Search"
        Me.LocalSearchCheckBox.UseVisualStyleBackColor = True
        '
        'X86Button
        '
        Me.X86Button.Location = New System.Drawing.Point(22, 38)
        Me.X86Button.Margin = New System.Windows.Forms.Padding(4)
        Me.X86Button.Name = "X86Button"
        Me.X86Button.Size = New System.Drawing.Size(195, 29)
        Me.X86Button.TabIndex = 186740
        Me.X86Button.Text = "Install C++ Runtime"
        Me.X86Button.UseVisualStyleBackColor = True
        '
        'ApachePort
        '
        Me.ApachePort.Location = New System.Drawing.Point(20, 92)
        Me.ApachePort.Margin = New System.Windows.Forms.Padding(4)
        Me.ApachePort.Name = "ApachePort"
        Me.ApachePort.Size = New System.Drawing.Size(40, 22)
        Me.ApachePort.TabIndex = 186736
        '
        'ApacheServiceCheckBox
        '
        Me.ApacheServiceCheckBox.AutoSize = True
        Me.ApacheServiceCheckBox.Location = New System.Drawing.Point(22, 158)
        Me.ApacheServiceCheckBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ApacheServiceCheckBox.Name = "ApacheServiceCheckBox"
        Me.ApacheServiceCheckBox.Size = New System.Drawing.Size(144, 21)
        Me.ApacheServiceCheckBox.TabIndex = 1868
        Me.ApacheServiceCheckBox.Text = "Run  As A Service"
        Me.ApacheServiceCheckBox.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(69, 95)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 17)
        Me.Label3.TabIndex = 186737
        Me.Label3.Text = "Web Port (80, or 8000)"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox1.Location = New System.Drawing.Point(234, 26)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(38, 42)
        Me.PictureBox1.TabIndex = 1859
        Me.PictureBox1.TabStop = False
        '
        'ApacheCheckbox
        '
        Me.ApacheCheckbox.AutoSize = True
        Me.ApacheCheckbox.Location = New System.Drawing.Point(22, 130)
        Me.ApacheCheckbox.Margin = New System.Windows.Forms.Padding(4)
        Me.ApacheCheckbox.Name = "ApacheCheckbox"
        Me.ApacheCheckbox.Size = New System.Drawing.Size(203, 21)
        Me.ApacheCheckbox.TabIndex = 1866
        Me.ApacheCheckbox.Text = "Enable Apache Web server"
        Me.ApacheCheckbox.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(527, 28)
        Me.MenuStrip1.TabIndex = 186740
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ApacheToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(73, 24)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ApacheToolStripMenuItem
        '
        Me.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ApacheToolStripMenuItem.Name = "ApacheToolStripMenuItem"
        Me.ApacheToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.ApacheToolStripMenuItem.Text = "Apache"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.EventsCheckBox)
        Me.GroupBox1.Controls.Add(Me.AllGridSearchCheckBox)
        Me.GroupBox1.Controls.Add(Me.EnableSearchCheckBox)
        Me.GroupBox1.Controls.Add(Me.LocalSearchCheckBox)
        Me.GroupBox1.Location = New System.Drawing.Point(313, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 200)
        Me.GroupBox1.TabIndex = 186741
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search"
        '
        'FormApache
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(527, 252)
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
