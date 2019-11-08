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
        Me.CheckBox1.Location = New System.Drawing.Point(30, 34)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(89, 17)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = " Script cache"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(30, 57)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(124, 17)
        Me.CheckBox2.TabIndex = 1
        Me.CheckBox2.Text = "Avatar Bakes Cache"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(30, 80)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(86, 17)
        Me.CheckBox3.TabIndex = 2
        Me.CheckBox3.Text = "Asset Cache"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(30, 103)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(89, 17)
        Me.CheckBox4.TabIndex = 3
        Me.CheckBox4.Text = "Image Cache"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(30, 126)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(86, 17)
        Me.CheckBox5.TabIndex = 4
        Me.CheckBox5.Text = "Mesh Cache"
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
        Me.GroupBox1.Location = New System.Drawing.Point(12, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(183, 204)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Choose which cache to empty"
        '
        'MapHelp
        '
        Me.MapHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.MapHelp.Location = New System.Drawing.Point(126, 19)
        Me.MapHelp.Name = "MapHelp"
        Me.MapHelp.Size = New System.Drawing.Size(28, 27)
        Me.MapHelp.TabIndex = 1858
        Me.MapHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.MapHelp, "Click For Help")
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(24, 156)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(130, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Clear Selected Caches"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(574, 24)
        Me.MenuStrip1.TabIndex = 18601
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = My.Resources.Help
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(99, 22)
        Me.HelpToolStripMenuItem1.Text = My.Resources.Help
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.PictureBox2)
        Me.GroupBox2.Controls.Add(Me.CacheTimeout)
        Me.GroupBox2.Controls.Add(Me.CacheEnabledBox)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.CacheFolder)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.LogLevelBox)
        Me.GroupBox2.Location = New System.Drawing.Point(205, 80)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(361, 154)
        Me.GroupBox2.TabIndex = 18602
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Flotsam Asset Cache"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Outworldz.My.Resources.Resources.folder
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(308, 43)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(41, 36)
        Me.PictureBox1.TabIndex = 1872
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox2.Location = New System.Drawing.Point(166, 15)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 27)
        Me.PictureBox2.TabIndex = 1860
        Me.PictureBox2.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox2, "Click For Help")
        '
        'CacheTimeout
        '
        Me.CacheTimeout.Location = New System.Drawing.Point(131, 107)
        Me.CacheTimeout.Name = "CacheTimeout"
        Me.CacheTimeout.Size = New System.Drawing.Size(52, 20)
        Me.CacheTimeout.TabIndex = 1868
        Me.ToolTip1.SetToolTip(Me.CacheTimeout, "Thebigger the number, the larger the cache.  Useful values can be up to several d" &
        "ays.")
        '
        'CacheEnabledBox
        '
        Me.CacheEnabledBox.AutoSize = True
        Me.CacheEnabledBox.Location = New System.Drawing.Point(136, 30)
        Me.CacheEnabledBox.Name = "CacheEnabledBox"
        Me.CacheEnabledBox.Size = New System.Drawing.Size(15, 14)
        Me.CacheEnabledBox.TabIndex = 1867
        Me.ToolTip1.SetToolTip(Me.CacheEnabledBox, "Default: Checked")
        Me.CacheEnabledBox.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 1865
        Me.Label5.Text = "Timeout in hours"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 1864
        Me.Label4.Text = "Cache Enabled"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 1863
        Me.Label2.Text = "Log Level"
        '
        'CacheFolder
        '
        Me.CacheFolder.Location = New System.Drawing.Point(131, 50)
        Me.CacheFolder.Name = "CacheFolder"
        Me.CacheFolder.Size = New System.Drawing.Size(175, 20)
        Me.CacheFolder.TabIndex = 1862
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 1861
        Me.Label1.Text = "Cache Directory"
        '
        'LogLevelBox
        '
        Me.LogLevelBox.FormattingEnabled = True
        Me.LogLevelBox.Items.AddRange(New Object() {"0 - (Error) Errors only", "1 - (Info)  Hit Rate Stats", "2 - (Debug) Cache Activity"})
        Me.LogLevelBox.Location = New System.Drawing.Point(131, 77)
        Me.LogLevelBox.Name = "LogLevelBox"
        Me.LogLevelBox.Size = New System.Drawing.Size(148, 21)
        Me.LogLevelBox.TabIndex = 1860
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ViewerCacheCheckbox)
        Me.GroupBox3.Location = New System.Drawing.Point(205, 24)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(357, 50)
        Me.GroupBox3.TabIndex = 18603
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Viewer Cache"
        '
        'ViewerCacheCheckbox
        '
        Me.ViewerCacheCheckbox.AutoSize = True
        Me.ViewerCacheCheckbox.Location = New System.Drawing.Point(19, 22)
        Me.ViewerCacheCheckbox.Name = "ViewerCacheCheckbox"
        Me.ViewerCacheCheckbox.Size = New System.Drawing.Size(65, 17)
        Me.ViewerCacheCheckbox.TabIndex = 1
        Me.ViewerCacheCheckbox.Text = "Enabled"
        Me.ToolTip1.SetToolTip(Me.ViewerCacheCheckbox, "Default: Enabled. Users may need to reduce viewer bandwidth if some prims Or terr" &
        "ain parts fail to rez. Change to false if you need to use old viewers that do no" &
        "t support this feature")
        Me.ViewerCacheCheckbox.UseVisualStyleBackColor = True
        '
        'FormCaches
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(574, 246)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormCaches"
        Me.Text = "Cache Control"
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
