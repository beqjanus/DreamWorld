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
        Me.ScriptCheckBox1 = New System.Windows.Forms.CheckBox()
        Me.AvatarCheckBox2 = New System.Windows.Forms.CheckBox()
        Me.AssetCheckBox3 = New System.Windows.Forms.CheckBox()
        Me.ImageCheckBox4 = New System.Windows.Forms.CheckBox()
        Me.MeshCheckBox5 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CacheTimeout = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CacheEnabledBox = New System.Windows.Forms.CheckBox()
        Me.CacheFolder = New System.Windows.Forms.TextBox()
        Me.LogLevelBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ViewerCacheCheckbox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ScriptCheckBox1
        '
        Me.ScriptCheckBox1.AutoSize = True
        Me.ScriptCheckBox1.Location = New System.Drawing.Point(54, 58)
        Me.ScriptCheckBox1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ScriptCheckBox1.Name = "ScriptCheckBox1"
        Me.ScriptCheckBox1.Size = New System.Drawing.Size(146, 29)
        Me.ScriptCheckBox1.TabIndex = 0
        Me.ScriptCheckBox1.Text = Global.Outworldz.My.Resources.Resources.Script_cache_word
        Me.ScriptCheckBox1.UseVisualStyleBackColor = True
        '
        'AvatarCheckBox2
        '
        Me.AvatarCheckBox2.AutoSize = True
        Me.AvatarCheckBox2.Location = New System.Drawing.Point(54, 99)
        Me.AvatarCheckBox2.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.AvatarCheckBox2.Name = "AvatarCheckBox2"
        Me.AvatarCheckBox2.Size = New System.Drawing.Size(218, 29)
        Me.AvatarCheckBox2.TabIndex = 1
        Me.AvatarCheckBox2.Text = Global.Outworldz.My.Resources.Resources.Avatar_Bakes_Cache_word
        Me.AvatarCheckBox2.UseVisualStyleBackColor = True
        '
        'AssetCheckBox3
        '
        Me.AssetCheckBox3.AutoSize = True
        Me.AssetCheckBox3.Location = New System.Drawing.Point(54, 140)
        Me.AssetCheckBox3.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.AssetCheckBox3.Name = "AssetCheckBox3"
        Me.AssetCheckBox3.Size = New System.Drawing.Size(151, 29)
        Me.AssetCheckBox3.TabIndex = 2
        Me.AssetCheckBox3.Text = Global.Outworldz.My.Resources.Resources.Asset_Cache_word
        Me.AssetCheckBox3.UseVisualStyleBackColor = True
        '
        'ImageCheckBox4
        '
        Me.ImageCheckBox4.AutoSize = True
        Me.ImageCheckBox4.Location = New System.Drawing.Point(54, 181)
        Me.ImageCheckBox4.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ImageCheckBox4.Name = "ImageCheckBox4"
        Me.ImageCheckBox4.Size = New System.Drawing.Size(155, 29)
        Me.ImageCheckBox4.TabIndex = 3
        Me.ImageCheckBox4.Text = Global.Outworldz.My.Resources.Resources.Image_Cache_word
        Me.ImageCheckBox4.UseVisualStyleBackColor = True
        '
        'MeshCheckBox5
        '
        Me.MeshCheckBox5.AutoSize = True
        Me.MeshCheckBox5.Location = New System.Drawing.Point(54, 222)
        Me.MeshCheckBox5.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.MeshCheckBox5.Name = "MeshCheckBox5"
        Me.MeshCheckBox5.Size = New System.Drawing.Size(150, 29)
        Me.MeshCheckBox5.TabIndex = 4
        Me.MeshCheckBox5.Text = Global.Outworldz.My.Resources.Resources.Mesh_Cache_word
        Me.MeshCheckBox5.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.AvatarCheckBox2)
        Me.GroupBox1.Controls.Add(Me.MeshCheckBox5)
        Me.GroupBox1.Controls.Add(Me.ScriptCheckBox1)
        Me.GroupBox1.Controls.Add(Me.ImageCheckBox4)
        Me.GroupBox1.Controls.Add(Me.AssetCheckBox3)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 54)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.GroupBox1.Size = New System.Drawing.Size(321, 357)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Choose which cache to empty"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(42, 273)
        Me.Button1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(226, 41)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Clear_Selected_Caches_word
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(1072, 36)
        Me.MenuStrip1.TabIndex = 18601
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(90, 34)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.CacheTimeout)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.CacheEnabledBox)
        Me.GroupBox2.Controls.Add(Me.CacheFolder)
        Me.GroupBox2.Controls.Add(Me.LogLevelBox)
        Me.GroupBox2.Location = New System.Drawing.Point(358, 141)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.GroupBox2.Size = New System.Drawing.Size(665, 268)
        Me.GroupBox2.TabIndex = 18602
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Asset Cache"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Outworldz.My.Resources.Resources.folder
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(448, 121)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(58, 41)
        Me.PictureBox1.TabIndex = 1872
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(83, 41)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(147, 25)
        Me.Label4.TabIndex = 1864
        Me.Label4.Text = "Cache Enabled"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(76, 92)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 25)
        Me.Label1.TabIndex = 1861
        Me.Label1.Text = "Cache Directory"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(443, 184)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 25)
        Me.Label2.TabIndex = 1863
        Me.Label2.Text = "Log Level"
        '
        'CacheTimeout
        '
        Me.CacheTimeout.Location = New System.Drawing.Point(36, 223)
        Me.CacheTimeout.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.CacheTimeout.Name = "CacheTimeout"
        Me.CacheTimeout.Size = New System.Drawing.Size(75, 29)
        Me.CacheTimeout.TabIndex = 1868
        Me.ToolTip1.SetToolTip(Me.CacheTimeout, Global.Outworldz.My.Resources.Resources.Timeout_in_hours_word)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(134, 225)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(157, 25)
        Me.Label5.TabIndex = 1865
        Me.Label5.Text = "Timeout in hours"
        '
        'CacheEnabledBox
        '
        Me.CacheEnabledBox.AutoSize = True
        Me.CacheEnabledBox.Location = New System.Drawing.Point(36, 41)
        Me.CacheEnabledBox.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.CacheEnabledBox.Name = "CacheEnabledBox"
        Me.CacheEnabledBox.Size = New System.Drawing.Size(22, 21)
        Me.CacheEnabledBox.TabIndex = 1867
        Me.ToolTip1.SetToolTip(Me.CacheEnabledBox, Global.Outworldz.My.Resources.Resources.Default_Checked_word)
        Me.CacheEnabledBox.UseVisualStyleBackColor = True
        '
        'CacheFolder
        '
        Me.CacheFolder.Location = New System.Drawing.Point(36, 125)
        Me.CacheFolder.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.CacheFolder.Name = "CacheFolder"
        Me.CacheFolder.Size = New System.Drawing.Size(398, 29)
        Me.CacheFolder.TabIndex = 1862
        '
        'LogLevelBox
        '
        Me.LogLevelBox.FormattingEnabled = True
        Me.LogLevelBox.Location = New System.Drawing.Point(36, 170)
        Me.LogLevelBox.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.LogLevelBox.Name = "LogLevelBox"
        Me.LogLevelBox.Size = New System.Drawing.Size(395, 32)
        Me.LogLevelBox.TabIndex = 1860
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ViewerCacheCheckbox)
        Me.GroupBox3.Location = New System.Drawing.Point(358, 42)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.GroupBox3.Size = New System.Drawing.Size(665, 90)
        Me.GroupBox3.TabIndex = 18603
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Viewer Cache"
        '
        'ViewerCacheCheckbox
        '
        Me.ViewerCacheCheckbox.AutoSize = True
        Me.ViewerCacheCheckbox.Location = New System.Drawing.Point(34, 40)
        Me.ViewerCacheCheckbox.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ViewerCacheCheckbox.Name = "ViewerCacheCheckbox"
        Me.ViewerCacheCheckbox.Size = New System.Drawing.Size(110, 29)
        Me.ViewerCacheCheckbox.TabIndex = 1
        Me.ViewerCacheCheckbox.Text = Global.Outworldz.My.Resources.Resources.Enabled_word
        Me.ToolTip1.SetToolTip(Me.ViewerCacheCheckbox, Global.Outworldz.My.Resources.Resources.Viewer_Cache_text)
        Me.ViewerCacheCheckbox.UseVisualStyleBackColor = True
        '
        'FormCaches
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(168.0!, 168.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1072, 432)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.MaximizeBox = False
        Me.Name = "FormCaches"
        Me.Text = "Cache Control"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ScriptCheckBox1 As CheckBox
    Friend WithEvents AvatarCheckBox2 As CheckBox
    Friend WithEvents AssetCheckBox3 As CheckBox
    Friend WithEvents ImageCheckBox4 As CheckBox
    Friend WithEvents MeshCheckBox5 As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CacheTimeout As TextBox
    Friend WithEvents CacheEnabledBox As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CacheFolder As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LogLevelBox As ComboBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ViewerCacheCheckbox As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
