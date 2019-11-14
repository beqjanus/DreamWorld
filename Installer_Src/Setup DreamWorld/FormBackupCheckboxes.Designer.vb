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
        Me.TextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SettingsBox = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CustomCheckBox = New System.Windows.Forms.CheckBox()
        Me.FSAssetsCheckBox = New System.Windows.Forms.CheckBox()
        Me.MySqlCheckBox = New System.Windows.Forms.CheckBox()
        Me.RegionCheckBox = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.SettingsBox)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.CustomCheckBox)
        Me.GroupBox1.Controls.Add(Me.FSAssetsCheckBox)
        Me.GroupBox1.Controls.Add(Me.MySqlCheckBox)
        Me.GroupBox1.Controls.Add(Me.RegionCheckBox)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 42)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(902, 312)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Backup"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(383, 15)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox1.MaxLength = 15000
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(510, 288)
        Me.TextBox1.TabIndex = 31
        Me.TextBox1.Text = ""
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(176, 245)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(112, 35)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = Global.Outworldz.My.Resources.Resources.Skip_word
        Me.Button2.UseVisualStyleBackColor = True
        '
        'SettingsBox
        '
        Me.SettingsBox.AutoSize = True
        Me.SettingsBox.Checked = True
        Me.SettingsBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SettingsBox.Location = New System.Drawing.Point(34, 191)
        Me.SettingsBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SettingsBox.Name = "SettingsBox"
        Me.SettingsBox.Size = New System.Drawing.Size(152, 24)
        Me.SettingsBox.TabIndex = 11
        Me.SettingsBox.Text = Global.Outworldz.My.Resources.Resources.Backup_Settings_word
        Me.SettingsBox.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(34, 245)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 35)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Backup_word
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CustomCheckBox
        '
        Me.CustomCheckBox.AutoSize = True
        Me.CustomCheckBox.Checked = True
        Me.CustomCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CustomCheckBox.Location = New System.Drawing.Point(34, 155)
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
        Me.FSAssetsCheckBox.Location = New System.Drawing.Point(34, 120)
        Me.FSAssetsCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.FSAssetsCheckBox.Name = "FSAssetsCheckBox"
        Me.FSAssetsCheckBox.Size = New System.Drawing.Size(207, 24)
        Me.FSAssetsCheckBox.TabIndex = 8
        Me.FSAssetsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_FSAssets
        Me.FSAssetsCheckBox.UseVisualStyleBackColor = True
        '
        'MySqlCheckBox
        '
        Me.MySqlCheckBox.AutoSize = True
        Me.MySqlCheckBox.Checked = True
        Me.MySqlCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MySqlCheckBox.Location = New System.Drawing.Point(34, 85)
        Me.MySqlCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MySqlCheckBox.Name = "MySqlCheckBox"
        Me.MySqlCheckBox.Size = New System.Drawing.Size(216, 24)
        Me.MySqlCheckBox.TabIndex = 7
        Me.MySqlCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_Mysql
        Me.MySqlCheckBox.UseVisualStyleBackColor = True
        '
        'RegionCheckBox
        '
        Me.RegionCheckBox.AutoSize = True
        Me.RegionCheckBox.Checked = True
        Me.RegionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RegionCheckBox.Location = New System.Drawing.Point(34, 49)
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
        Me.MenuStrip1.Size = New System.Drawing.Size(963, 35)
        Me.MenuStrip1.TabIndex = 18602
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
        'FormBackupCheckboxes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 374)
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
    Friend WithEvents MySqlCheckBox As CheckBox
    Friend WithEvents RegionCheckBox As CheckBox
    Friend WithEvents Button2 As Button
    Friend WithEvents SettingsBox As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents TextBox1 As RichTextBox
End Class
