﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFlotsamCache
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFlotsamCache))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CacheSizeLabel = New System.Windows.Forms.Label()
        Me.CacheTimeout = New System.Windows.Forms.TextBox()
        Me.CacheEnabledBox = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CacheFolder = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LogLevelBox = New System.Windows.Forms.ComboBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Outworldz.My.Resources.Resources.folder
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(191, 53)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 27)
        Me.PictureBox1.TabIndex = 1870
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, Global.Outworldz.My.Resources.Resources.Click_to_change_the_folder)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.CacheSizeLabel)
        Me.GroupBox1.Controls.Add(Me.CacheTimeout)
        Me.GroupBox1.Controls.Add(Me.CacheEnabledBox)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.CacheFolder)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.LogLevelBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 37)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(320, 181)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Asset Cache"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(19, 144)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(129, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Clear_Cache_word
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CacheSizeLabel
        '
        Me.CacheSizeLabel.AutoSize = True
        Me.CacheSizeLabel.Location = New System.Drawing.Point(189, 154)
        Me.CacheSizeLabel.Name = "CacheSizeLabel"
        Me.CacheSizeLabel.Size = New System.Drawing.Size(32, 13)
        Me.CacheSizeLabel.TabIndex = 8
        Me.CacheSizeLabel.Text = "0 MB"
        '
        'CacheTimeout
        '
        Me.CacheTimeout.Location = New System.Drawing.Point(19, 111)
        Me.CacheTimeout.Name = "CacheTimeout"
        Me.CacheTimeout.Size = New System.Drawing.Size(52, 20)
        Me.CacheTimeout.TabIndex = 1868
        '
        'CacheEnabledBox
        '
        Me.CacheEnabledBox.AutoSize = True
        Me.CacheEnabledBox.Location = New System.Drawing.Point(19, 29)
        Me.CacheEnabledBox.Name = "CacheEnabledBox"
        Me.CacheEnabledBox.Size = New System.Drawing.Size(15, 14)
        Me.CacheEnabledBox.TabIndex = 0
        Me.CacheEnabledBox.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(189, 133)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Current Size on Disk"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(82, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Timeout in hours"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(39, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 1864
        Me.Label4.Text = "Cache Enabled"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(225, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Log Level"
        '
        'CacheFolder
        '
        Me.CacheFolder.Location = New System.Drawing.Point(19, 58)
        Me.CacheFolder.Name = "CacheFolder"
        Me.CacheFolder.Size = New System.Drawing.Size(168, 20)
        Me.CacheFolder.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(225, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Cache Directory"
        '
        'LogLevelBox
        '
        Me.LogLevelBox.FormattingEnabled = True
        Me.LogLevelBox.Items.AddRange(New Object() {"0 - (Error) Errors only", "1 - (Info)  Hit Rate Stats", "2 - (Debug) Cache Activity"})
        Me.LogLevelBox.Location = New System.Drawing.Point(19, 84)
        Me.LogLevelBox.Name = "LogLevelBox"
        Me.LogLevelBox.Size = New System.Drawing.Size(170, 21)
        Me.LogLevelBox.TabIndex = 3
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(345, 34)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(72, 32)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormFlotsamCache
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(345, 227)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormFlotsamCache"
        Me.Text = "Asset Cache"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents CacheSizeLabel As Label
    Friend WithEvents CacheTimeout As TextBox
    Friend WithEvents CacheEnabledBox As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CacheFolder As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LogLevelBox As ComboBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents Button1 As Button
End Class
