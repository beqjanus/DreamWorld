<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormFSAssets
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFSAssets))
        Me.EnableFsAssetsCheckbox = New System.Windows.Forms.CheckBox()
        Me.FSAssetsGroupBox = New System.Windows.Forms.GroupBox()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ShowStatsCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.DataFolder = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FSAssetsGroupBox.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'EnableFsAssetsCheckbox
        '
        Me.EnableFsAssetsCheckbox.AutoSize = True
        Me.EnableFsAssetsCheckbox.Location = New System.Drawing.Point(21, 28)
        Me.EnableFsAssetsCheckbox.Name = "EnableFsAssetsCheckbox"
        Me.EnableFsAssetsCheckbox.Size = New System.Drawing.Size(59, 17)
        Me.EnableFsAssetsCheckbox.TabIndex = 43
        Me.EnableFsAssetsCheckbox.Text = Global.Outworldz.My.Resources.Resources.Enable_word
        Me.EnableFsAssetsCheckbox.UseVisualStyleBackColor = True
        '
        'FSAssetsGroupBox
        '
        Me.FSAssetsGroupBox.Controls.Add(Me.SaveButton)
        Me.FSAssetsGroupBox.Controls.Add(Me.ShowStatsCheckBox)
        Me.FSAssetsGroupBox.Controls.Add(Me.Label6)
        Me.FSAssetsGroupBox.Controls.Add(Me.PictureBox2)
        Me.FSAssetsGroupBox.Controls.Add(Me.DataFolder)
        Me.FSAssetsGroupBox.Controls.Add(Me.EnableFsAssetsCheckbox)
        Me.FSAssetsGroupBox.Location = New System.Drawing.Point(9, 44)
        Me.FSAssetsGroupBox.Name = "FSAssetsGroupBox"
        Me.FSAssetsGroupBox.Size = New System.Drawing.Size(280, 151)
        Me.FSAssetsGroupBox.TabIndex = 44
        Me.FSAssetsGroupBox.TabStop = False
        Me.FSAssetsGroupBox.Text = "File System Assets Server"
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(23, 118)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(101, 23)
        Me.SaveButton.TabIndex = 1893
        Me.SaveButton.Text = Global.Outworldz.My.Resources.Resources.Save_word
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ShowStatsCheckBox
        '
        Me.ShowStatsCheckBox.AutoSize = True
        Me.ShowStatsCheckBox.Location = New System.Drawing.Point(21, 51)
        Me.ShowStatsCheckBox.Name = "ShowStatsCheckBox"
        Me.ShowStatsCheckBox.Size = New System.Drawing.Size(133, 17)
        Me.ShowStatsCheckBox.TabIndex = 1892
        Me.ShowStatsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Show_Stats
        Me.ShowStatsCheckBox.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 1888
        Me.Label6.Text = "Data Folder"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.Outworldz.My.Resources.Resources.folder
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox2.Location = New System.Drawing.Point(221, 94)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(38, 27)
        Me.PictureBox2.TabIndex = 1887
        Me.PictureBox2.TabStop = False
        '
        'DataFolder
        '
        Me.DataFolder.Location = New System.Drawing.Point(23, 94)
        Me.DataFolder.Name = "DataFolder"
        Me.DataFolder.Size = New System.Drawing.Size(195, 20)
        Me.DataFolder.TabIndex = 44
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(307, 30)
        Me.MenuStrip1.TabIndex = 18601
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(68, 28)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormFSAssets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(307, 216)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.FSAssetsGroupBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormFSAssets"
        Me.Text = "Fie System Assets Server"
        Me.FSAssetsGroupBox.ResumeLayout(False)
        Me.FSAssetsGroupBox.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents EnableFsAssetsCheckbox As CheckBox
    Friend WithEvents FSAssetsGroupBox As GroupBox
    Friend WithEvents DataFolder As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ShowStatsCheckBox As CheckBox
    Friend WithEvents SaveButton As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
End Class
