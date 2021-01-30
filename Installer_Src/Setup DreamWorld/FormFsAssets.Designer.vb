<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormFsAssets
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
        Me.EnableFsAssetsCheckbox = New System.Windows.Forms.CheckBox()
        Me.b = New System.Windows.Forms.GroupBox()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ShowStatsCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.DataFolder = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.b.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'EnableFsAssetsCheckbox
        '
        Me.EnableFsAssetsCheckbox.AutoSize = True
        Me.EnableFsAssetsCheckbox.Location = New System.Drawing.Point(39, 52)
        Me.EnableFsAssetsCheckbox.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.EnableFsAssetsCheckbox.Name = "EnableFsAssetsCheckbox"
        Me.EnableFsAssetsCheckbox.Size = New System.Drawing.Size(99, 29)
        Me.EnableFsAssetsCheckbox.TabIndex = 43
        Me.EnableFsAssetsCheckbox.Text = Global.Outworldz.My.Resources.Resources.Enable_word
        Me.EnableFsAssetsCheckbox.UseVisualStyleBackColor = True
        '
        'b
        '
        Me.b.Controls.Add(Me.SaveButton)
        Me.b.Controls.Add(Me.ShowStatsCheckBox)
        Me.b.Controls.Add(Me.Label6)
        Me.b.Controls.Add(Me.PictureBox2)
        Me.b.Controls.Add(Me.DataFolder)
        Me.b.Controls.Add(Me.EnableFsAssetsCheckbox)
        Me.b.Location = New System.Drawing.Point(16, 82)
        Me.b.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.b.Name = "b"
        Me.b.Padding = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.b.Size = New System.Drawing.Size(513, 280)
        Me.b.TabIndex = 44
        Me.b.TabStop = False
        Me.b.Text = "Fie System Assets Server"
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(42, 217)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(185, 42)
        Me.SaveButton.TabIndex = 1893
        Me.SaveButton.Text = Global.Outworldz.My.Resources.Resources.Save_word
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ShowStatsCheckBox
        '
        Me.ShowStatsCheckBox.AutoSize = True
        Me.ShowStatsCheckBox.Location = New System.Drawing.Point(39, 94)
        Me.ShowStatsCheckBox.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.ShowStatsCheckBox.Name = "ShowStatsCheckBox"
        Me.ShowStatsCheckBox.Size = New System.Drawing.Size(234, 29)
        Me.ShowStatsCheckBox.TabIndex = 1892
        Me.ShowStatsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Show_Stats
        Me.ShowStatsCheckBox.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(35, 144)
        Me.Label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(113, 25)
        Me.Label6.TabIndex = 1888
        Me.Label6.Text = "Data Folder"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.Outworldz.My.Resources.Resources.folder
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox2.Location = New System.Drawing.Point(406, 174)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(42, 31)
        Me.PictureBox2.TabIndex = 1887
        Me.PictureBox2.TabStop = False
        '
        'DataFolder
        '
        Me.DataFolder.Location = New System.Drawing.Point(42, 174)
        Me.DataFolder.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.DataFolder.Name = "DataFolder"
        Me.DataFolder.Size = New System.Drawing.Size(354, 29)
        Me.DataFolder.TabIndex = 44
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(562, 38)
        Me.MenuStrip1.TabIndex = 18601
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(98, 34)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormFsAssets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 398)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.b)
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Name = "FormFsAssets"
        Me.Text = "Fie System Assets Server"
        Me.b.ResumeLayout(False)
        Me.b.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents EnableFsAssetsCheckbox As CheckBox
    Friend WithEvents b As GroupBox
    Friend WithEvents DataFolder As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ShowStatsCheckBox As CheckBox
    Friend WithEvents SaveButton As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
End Class
