<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFsAssets
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
        Me.EnableFsAssetsCheckbox = New System.Windows.Forms.CheckBox()
        Me.b = New System.Windows.Forms.GroupBox()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ShowStatsCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.DataFolder = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.b.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.EnableFsAssetsCheckbox.Text = "Enable"
        Me.EnableFsAssetsCheckbox.UseVisualStyleBackColor = True
        '
        'b
        '
        Me.b.Controls.Add(Me.SaveButton)
        Me.b.Controls.Add(Me.ShowStatsCheckBox)
        Me.b.Controls.Add(Me.Label6)
        Me.b.Controls.Add(Me.PictureBox2)
        Me.b.Controls.Add(Me.PictureBox1)
        Me.b.Controls.Add(Me.DataFolder)
        Me.b.Controls.Add(Me.EnableFsAssetsCheckbox)
        Me.b.Location = New System.Drawing.Point(15, 44)
        Me.b.Name = "b"
        Me.b.Size = New System.Drawing.Size(280, 207)
        Me.b.TabIndex = 44
        Me.b.TabStop = False
        Me.b.Text = "File System Assets Database"
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(21, 155)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 1893
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ShowStatsCheckBox
        '
        Me.ShowStatsCheckBox.AutoSize = True
        Me.ShowStatsCheckBox.Location = New System.Drawing.Point(21, 51)
        Me.ShowStatsCheckBox.Name = "ShowStatsCheckBox"
        Me.ShowStatsCheckBox.Size = New System.Drawing.Size(133, 17)
        Me.ShowStatsCheckBox.TabIndex = 1892
        Me.ShowStatsCheckBox.Text = "Show stats on console"
        Me.ShowStatsCheckBox.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 1888
        Me.Label6.Text = "Data  Folder:"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.Outworldz.My.Resources.Resources.folder
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox2.Location = New System.Drawing.Point(222, 98)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(41, 33)
        Me.PictureBox2.TabIndex = 1887
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox1.Location = New System.Drawing.Point(222, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 32)
        Me.PictureBox1.TabIndex = 1886
        Me.PictureBox1.TabStop = False
        '
        'DataFolder
        '
        Me.DataFolder.Location = New System.Drawing.Point(21, 111)
        Me.DataFolder.Name = "DataFolder"
        Me.DataFolder.Size = New System.Drawing.Size(195, 20)
        Me.DataFolder.TabIndex = 44
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(307, 24)
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
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.HelpToolStripMenuItem1.Text = My.Resources.Help
        '
        'FormFsAssets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(307, 263)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.b)
        Me.Name = "FormFsAssets"
        Me.Text = "File System Assets"
        Me.b.ResumeLayout(False)
        Me.b.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents EnableFsAssetsCheckbox As CheckBox
    Friend WithEvents b As GroupBox
    Friend WithEvents DataFolder As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ShowStatsCheckBox As CheckBox
    Friend WithEvents SaveButton As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
End Class
