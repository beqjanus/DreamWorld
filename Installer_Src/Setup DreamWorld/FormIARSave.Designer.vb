<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormIARSave
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormIARSave))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AviName = New System.Windows.Forms.TextBox()
        Me.BackupNameTextBox = New System.Windows.Forms.TextBox()
        Me.ObjectNameBox = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FilterGroupBox = New System.Windows.Forms.GroupBox()
        Me.CopyCheckBox = New System.Windows.Forms.CheckBox()
        Me.TransferCheckBox = New System.Windows.Forms.CheckBox()
        Me.ModifyCheckBox = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FilterGroupBox.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'AviName
        '
        Me.AviName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.AviName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.AviName.Location = New System.Drawing.Point(8, 119)
        Me.AviName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.AviName.Name = "AviName"
        Me.AviName.Size = New System.Drawing.Size(266, 26)
        Me.AviName.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.AviName, Global.Outworldz.My.Resources.Resources.Avatar_First_and_Last_Name_word)
        '
        'BackupNameTextBox
        '
        Me.BackupNameTextBox.Location = New System.Drawing.Point(8, 79)
        Me.BackupNameTextBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BackupNameTextBox.Name = "BackupNameTextBox"
        Me.BackupNameTextBox.Size = New System.Drawing.Size(266, 26)
        Me.BackupNameTextBox.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.BackupNameTextBox, "/Path/To/Backup.IAR")
        '
        'ObjectNameBox
        '
        Me.ObjectNameBox.Location = New System.Drawing.Point(8, 39)
        Me.ObjectNameBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ObjectNameBox.Name = "ObjectNameBox"
        Me.ObjectNameBox.Size = New System.Drawing.Size(313, 26)
        Me.ObjectNameBox.TabIndex = 12
        Me.ObjectNameBox.Text = "/=everything, /Objects/Folder, etc."
        Me.ToolTip1.SetToolTip(Me.ObjectNameBox, Global.Outworldz.My.Resources.Resources.Enter_Name)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.AviName)
        Me.GroupBox1.Controls.Add(Me.BackupNameTextBox)
        Me.GroupBox1.Controls.Add(Me.ObjectNameBox)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 58)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(554, 176)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Save Inventory IAR"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.folder
        Me.PictureBox1.Location = New System.Drawing.Point(282, 75)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(36, 36)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 20
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(332, 122)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Avatar Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(332, 82)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 20)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Backup Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(332, 42)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(167, 20)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Object Path and name"
        '
        'FilterGroupBox
        '
        Me.FilterGroupBox.Controls.Add(Me.CopyCheckBox)
        Me.FilterGroupBox.Controls.Add(Me.TransferCheckBox)
        Me.FilterGroupBox.Controls.Add(Me.ModifyCheckBox)
        Me.FilterGroupBox.Location = New System.Drawing.Point(593, 69)
        Me.FilterGroupBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FilterGroupBox.Name = "FilterGroupBox"
        Me.FilterGroupBox.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FilterGroupBox.Size = New System.Drawing.Size(170, 165)
        Me.FilterGroupBox.TabIndex = 26
        Me.FilterGroupBox.TabStop = False
        Me.FilterGroupBox.Text = "Filter"
        '
        'CopyCheckBox
        '
        Me.CopyCheckBox.AutoSize = True
        Me.CopyCheckBox.Checked = True
        Me.CopyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CopyCheckBox.Location = New System.Drawing.Point(18, 41)
        Me.CopyCheckBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CopyCheckBox.Name = "CopyCheckBox"
        Me.CopyCheckBox.Size = New System.Drawing.Size(71, 24)
        Me.CopyCheckBox.TabIndex = 23
        Me.CopyCheckBox.Text = "Copy"
        Me.CopyCheckBox.UseVisualStyleBackColor = True
        '
        'TransferCheckBox
        '
        Me.TransferCheckBox.AutoSize = True
        Me.TransferCheckBox.Checked = True
        Me.TransferCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TransferCheckBox.Location = New System.Drawing.Point(18, 109)
        Me.TransferCheckBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TransferCheckBox.Name = "TransferCheckBox"
        Me.TransferCheckBox.Size = New System.Drawing.Size(94, 24)
        Me.TransferCheckBox.TabIndex = 25
        Me.TransferCheckBox.Text = "Transfer"
        Me.TransferCheckBox.UseVisualStyleBackColor = True
        '
        'ModifyCheckBox
        '
        Me.ModifyCheckBox.AutoSize = True
        Me.ModifyCheckBox.Checked = True
        Me.ModifyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ModifyCheckBox.Location = New System.Drawing.Point(18, 75)
        Me.ModifyCheckBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ModifyCheckBox.Name = "ModifyCheckBox"
        Me.ModifyCheckBox.Size = New System.Drawing.Size(81, 24)
        Me.ModifyCheckBox.TabIndex = 24
        Me.ModifyCheckBox.Text = "Modify"
        Me.ModifyCheckBox.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(593, 256)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(162, 35)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Save_IAR_word
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(6, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(795, 34)
        Me.MenuStrip1.TabIndex = 18599
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(93, 32)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormIARSave
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(795, 309)
        Me.Controls.Add(Me.FilterGroupBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FormIARSave"
        Me.Text = "Save Inventory IAR"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FilterGroupBox.ResumeLayout(False)
        Me.FilterGroupBox.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents AviName As TextBox
    Friend WithEvents BackupNameTextBox As TextBox
    Friend WithEvents ObjectNameBox As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransferCheckBox As CheckBox
    Friend WithEvents ModifyCheckBox As CheckBox
    Friend WithEvents CopyCheckBox As CheckBox
    Friend WithEvents FilterGroupBox As GroupBox
End Class
