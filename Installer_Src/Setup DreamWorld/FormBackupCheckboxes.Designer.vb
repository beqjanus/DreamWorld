<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormBackupCheckboxes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBackupCheckboxes))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BackupSQlCheckBox = New System.Windows.Forms.CheckBox()
        Me.BackupOarsCheckBox = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CustomCheckBox = New System.Windows.Forms.CheckBox()
        Me.FSAssetsCheckBox = New System.Windows.Forms.CheckBox()
        Me.SettingsCheckbox = New System.Windows.Forms.CheckBox()
        Me.RegionCheckBox = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BackupSQlCheckBox)
        Me.GroupBox1.Controls.Add(Me.BackupOarsCheckBox)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.CustomCheckBox)
        Me.GroupBox1.Controls.Add(Me.FSAssetsCheckBox)
        Me.GroupBox1.Controls.Add(Me.SettingsCheckbox)
        Me.GroupBox1.Controls.Add(Me.RegionCheckBox)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 41)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(332, 316)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Backup"
        '
        'BackupSQlCheckBox
        '
        Me.BackupSQlCheckBox.AutoSize = True
        Me.BackupSQlCheckBox.Checked = True
        Me.BackupSQlCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.BackupSQlCheckBox.Location = New System.Drawing.Point(35, 211)
        Me.BackupSQlCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BackupSQlCheckBox.Name = "BackupSQlCheckBox"
        Me.BackupSQlCheckBox.Size = New System.Drawing.Size(125, 24)
        Me.BackupSQlCheckBox.TabIndex = 12
        Me.BackupSQlCheckBox.Text = "Backup SQL"
        Me.BackupSQlCheckBox.UseVisualStyleBackColor = True
        '
        'BackupOarsCheckBox
        '
        Me.BackupOarsCheckBox.AutoSize = True
        Me.BackupOarsCheckBox.Checked = True
        Me.BackupOarsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.BackupOarsCheckBox.Location = New System.Drawing.Point(35, 175)
        Me.BackupOarsCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BackupOarsCheckBox.Name = "BackupOarsCheckBox"
        Me.BackupOarsCheckBox.Size = New System.Drawing.Size(136, 24)
        Me.BackupOarsCheckBox.TabIndex = 11
        Me.BackupOarsCheckBox.Text = "Backup OARs"
        Me.BackupOarsCheckBox.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(56, 248)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(181, 35)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Backup_word
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CustomCheckBox
        '
        Me.CustomCheckBox.AutoSize = True
        Me.CustomCheckBox.Checked = True
        Me.CustomCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CustomCheckBox.Location = New System.Drawing.Point(35, 139)
        Me.CustomCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CustomCheckBox.Name = "CustomCheckBox"
        Me.CustomCheckBox.Size = New System.Drawing.Size(234, 24)
        Me.CustomCheckBox.TabIndex = 9
        Me.CustomCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_Custom
        Me.CustomCheckBox.UseVisualStyleBackColor = True
        '
        'FSAssetsCheckBox
        '
        Me.FSAssetsCheckBox.AutoSize = True
        Me.FSAssetsCheckBox.Checked = True
        Me.FSAssetsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FSAssetsCheckBox.Location = New System.Drawing.Point(35, 104)
        Me.FSAssetsCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.FSAssetsCheckBox.Name = "FSAssetsCheckBox"
        Me.FSAssetsCheckBox.Size = New System.Drawing.Size(207, 24)
        Me.FSAssetsCheckBox.TabIndex = 8
        Me.FSAssetsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_FSAssets
        Me.FSAssetsCheckBox.UseVisualStyleBackColor = True
        '
        'SettingsCheckbox
        '
        Me.SettingsCheckbox.AutoSize = True
        Me.SettingsCheckbox.Checked = True
        Me.SettingsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SettingsCheckbox.Location = New System.Drawing.Point(35, 31)
        Me.SettingsCheckbox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SettingsCheckbox.Name = "SettingsCheckbox"
        Me.SettingsCheckbox.Size = New System.Drawing.Size(152, 24)
        Me.SettingsCheckbox.TabIndex = 7
        Me.SettingsCheckbox.Text = "Backup Settings"
        Me.SettingsCheckbox.UseVisualStyleBackColor = True
        '
        'RegionCheckBox
        '
        Me.RegionCheckBox.AutoSize = True
        Me.RegionCheckBox.Checked = True
        Me.RegionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RegionCheckBox.Location = New System.Drawing.Point(35, 68)
        Me.RegionCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RegionCheckBox.Name = "RegionCheckBox"
        Me.RegionCheckBox.Size = New System.Drawing.Size(201, 24)
        Me.RegionCheckBox.TabIndex = 6
        Me.RegionCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_Region
        Me.RegionCheckBox.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(6, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(388, 31)
        Me.MenuStrip1.TabIndex = 18602
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(89, 29)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormBackupCheckboxes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(388, 374)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FormBackupCheckboxes"
        Me.Text = "System Backup"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents CustomCheckBox As CheckBox
    Friend WithEvents FSAssetsCheckBox As CheckBox
    Friend WithEvents SettingsCheckbox As CheckBox
    Friend WithEvents RegionCheckBox As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackupOarsCheckBox As CheckBox
    Friend WithEvents BackupSQlCheckBox As CheckBox
End Class
