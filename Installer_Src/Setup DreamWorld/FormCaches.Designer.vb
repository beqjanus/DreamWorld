<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCaches
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCaches))
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MapHelp = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.CacheTimeout = New System.Windows.Forms.TextBox()
        Me.CacheEnabledBox = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CacheFolder = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LogLevelBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ViewerCacheCheckbox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.MapHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(45, 51)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(123, 24)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = Global.Outworldz.My.Resources.Resources.Script_cache
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(45, 86)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(180, 24)
        Me.CheckBox2.TabIndex = 1
        Me.CheckBox2.Text = Global.Outworldz.My.Resources.Resources.Avatar_Cache
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(45, 120)
        Me.CheckBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(126, 24)
        Me.CheckBox3.TabIndex = 2
        Me.CheckBox3.Text = Global.Outworldz.My.Resources.Resources.Asset_Cache
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(45, 154)
        Me.CheckBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(130, 24)
        Me.CheckBox4.TabIndex = 3
        Me.CheckBox4.Text = Global.Outworldz.My.Resources.Resources.Image_Cache
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(45, 189)
        Me.CheckBox5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(124, 24)
        Me.CheckBox5.TabIndex = 4
        Me.CheckBox5.Text = Global.Outworldz.My.Resources.Resources.Mesh_Cache
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MapHelp)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.CheckBox2)
        Me.GroupBox1.Controls.Add(Me.CheckBox5)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.CheckBox4)
        Me.GroupBox1.Controls.Add(Me.CheckBox3)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 45)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(274, 306)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Choose_Cache
        '
        'MapHelp
        '
        Me.MapHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.MapHelp.Location = New System.Drawing.Point(189, 28)
        Me.MapHelp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MapHelp.Name = "MapHelp"
        Me.MapHelp.Size = New System.Drawing.Size(42, 40)
        Me.MapHelp.TabIndex = 1858
        Me.MapHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.MapHelp, Global.Outworldz.My.Resources.Resources.Click_For_Help)
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(36, 234)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(195, 34)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Clear_Selected_Caches
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(814, 35)
        Me.MenuStrip1.TabIndex = 18601
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(89, 29)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(151, 34)
        Me.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.PictureBox2)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.CacheTimeout)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.CacheEnabledBox)
        Me.GroupBox2.Controls.Add(Me.CacheFolder)
        Me.GroupBox2.Controls.Add(Me.LogLevelBox)
        Me.GroupBox2.Location = New System.Drawing.Point(308, 121)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(495, 231)
        Me.GroupBox2.TabIndex = 18602
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = Global.Outworldz.My.Resources.Resources.Flotsam_Asset_Cache
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Outworldz.My.Resources.Resources.folder
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(251, 44)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(51, 35)
        Me.PictureBox1.TabIndex = 1872
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox2.Location = New System.Drawing.Point(205, 23)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(31, 21)
        Me.PictureBox2.TabIndex = 1860
        Me.PictureBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox2, Global.Outworldz.My.Resources.Resources.Click_For_Help)
        '
        'CacheTimeout
        '
        Me.CacheTimeout.Location = New System.Drawing.Point(53, 164)
        Me.CacheTimeout.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CacheTimeout.Name = "CacheTimeout"
        Me.CacheTimeout.Size = New System.Drawing.Size(65, 26)
        Me.CacheTimeout.TabIndex = 1868
        Me.ToolTip1.SetToolTip(Me.CacheTimeout, Global.Outworldz.My.Resources.Resources.Timeout_in_hours)
        '
        'CacheEnabledBox
        '
        Me.CacheEnabledBox.AutoSize = True
        Me.CacheEnabledBox.Location = New System.Drawing.Point(61, 49)
        Me.CacheEnabledBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CacheEnabledBox.Name = "CacheEnabledBox"
        Me.CacheEnabledBox.Size = New System.Drawing.Size(22, 21)
        Me.CacheEnabledBox.TabIndex = 1867
        Me.ToolTip1.SetToolTip(Me.CacheEnabledBox, Global.Outworldz.My.Resources.Resources.Default_Checked)
        Me.CacheEnabledBox.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(136, 167)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(126, 20)
        Me.Label5.TabIndex = 1865
        Me.Label5.Text = Global.Outworldz.My.Resources.Resources.Timeout_in_hours
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(91, 48)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(118, 20)
        Me.Label4.TabIndex = 1864
        Me.Label4.Text = Global.Outworldz.My.Resources.Resources.Cache_Enabled
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(310, 120)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 20)
        Me.Label2.TabIndex = 1863
        Me.Label2.Text = Global.Outworldz.My.Resources.Resources.Log_Level
        '
        'CacheFolder
        '
        Me.CacheFolder.Location = New System.Drawing.Point(53, 79)
        Me.CacheFolder.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CacheFolder.Name = "CacheFolder"
        Me.CacheFolder.Size = New System.Drawing.Size(249, 26)
        Me.CacheFolder.TabIndex = 1862
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(310, 78)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(122, 20)
        Me.Label1.TabIndex = 1861
        Me.Label1.Text = Global.Outworldz.My.Resources.Resources.Cache_Directory
        '
        'LogLevelBox
        '
        Me.LogLevelBox.FormattingEnabled = True
        Me.LogLevelBox.Items.AddRange(New Object() {Global.Outworldz.My.Resources.Resources.ErrorLevel0, Global.Outworldz.My.Resources.Resources.ErrorLevel1, Global.Outworldz.My.Resources.Resources.ErrorLevel2})
        Me.LogLevelBox.Location = New System.Drawing.Point(53, 120)
        Me.LogLevelBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LogLevelBox.Name = "LogLevelBox"
        Me.LogLevelBox.Size = New System.Drawing.Size(209, 28)
        Me.LogLevelBox.TabIndex = 1860
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ViewerCacheCheckbox)
        Me.GroupBox3.Location = New System.Drawing.Point(308, 36)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(236, 77)
        Me.GroupBox3.TabIndex = 18603
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = Global.Outworldz.My.Resources.Resources.Viewer_Cache
        '
        'ViewerCacheCheckbox
        '
        Me.ViewerCacheCheckbox.AutoSize = True
        Me.ViewerCacheCheckbox.Location = New System.Drawing.Point(28, 33)
        Me.ViewerCacheCheckbox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ViewerCacheCheckbox.Name = "ViewerCacheCheckbox"
        Me.ViewerCacheCheckbox.Size = New System.Drawing.Size(94, 24)
        Me.ViewerCacheCheckbox.TabIndex = 1
        Me.ViewerCacheCheckbox.Text = Global.Outworldz.My.Resources.Resources.Enabled
        Me.ToolTip1.SetToolTip(Me.ViewerCacheCheckbox, Global.Outworldz.My.Resources.Resources.Viewer_Cache_text)
        Me.ViewerCacheCheckbox.UseVisualStyleBackColor = True
        '
        'FormCaches
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(814, 369)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "FormCaches"
        Me.Text = Global.Outworldz.My.Resources.Resources.Cache_Control
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MapHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents MapHelp As PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CacheTimeout As TextBox
    Friend WithEvents CacheEnabledBox As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CacheFolder As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LogLevelBox As ComboBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ViewerCacheCheckbox As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
