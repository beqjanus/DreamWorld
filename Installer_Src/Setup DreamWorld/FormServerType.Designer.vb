<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormServerType
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MetroRadioButton2 = New System.Windows.Forms.RadioButton()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.GridRegionButton = New System.Windows.Forms.RadioButton()
        Me.osGridRadioButton1 = New System.Windows.Forms.RadioButton()
        Me.GridServerButton = New System.Windows.Forms.RadioButton()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServerTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MetroRadioButton2)
        Me.GroupBox1.Controls.Add(Me.SaveButton)
        Me.GroupBox1.Controls.Add(Me.GridRegionButton)
        Me.GroupBox1.Controls.Add(Me.osGridRadioButton1)
        Me.GroupBox1.Controls.Add(Me.GridServerButton)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 49)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(321, 263)
        Me.GroupBox1.TabIndex = 1885
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Server_Type_word
        '
        'MetroRadioButton2
        '
        Me.MetroRadioButton2.AutoSize = True
        Me.MetroRadioButton2.Location = New System.Drawing.Point(26, 154)
        Me.MetroRadioButton2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MetroRadioButton2.Name = "MetroRadioButton2"
        Me.MetroRadioButton2.Size = New System.Drawing.Size(234, 24)
        Me.MetroRadioButton2.TabIndex = 1882
        Me.MetroRadioButton2.Text = Global.Outworldz.My.Resources.Resources.MetroOrg
        Me.MetroRadioButton2.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(72, 215)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(93, 35)
        Me.SaveButton.TabIndex = 1883
        Me.SaveButton.Text = Global.Outworldz.My.Resources.Resources.Save_word
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'GridRegionButton
        '
        Me.GridRegionButton.AutoSize = True
        Me.GridRegionButton.Location = New System.Drawing.Point(26, 85)
        Me.GridRegionButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridRegionButton.Name = "GridRegionButton"
        Me.GridRegionButton.Size = New System.Drawing.Size(135, 24)
        Me.GridRegionButton.TabIndex = 1880
        Me.GridRegionButton.Text = Global.Outworldz.My.Resources.Resources.Region_Server_word
        Me.GridRegionButton.UseVisualStyleBackColor = True
        '
        'osGridRadioButton1
        '
        Me.osGridRadioButton1.AutoSize = True
        Me.osGridRadioButton1.Location = New System.Drawing.Point(26, 118)
        Me.osGridRadioButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.osGridRadioButton1.Name = "osGridRadioButton1"
        Me.osGridRadioButton1.Size = New System.Drawing.Size(192, 24)
        Me.osGridRadioButton1.TabIndex = 1881
        Me.osGridRadioButton1.Text = Global.Outworldz.My.Resources.Resources.OSGrid_Region_Server
        Me.osGridRadioButton1.UseVisualStyleBackColor = True
        '
        'GridServerButton
        '
        Me.GridServerButton.AutoSize = True
        Me.GridServerButton.Checked = True
        Me.GridServerButton.Location = New System.Drawing.Point(26, 49)
        Me.GridServerButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridServerButton.Name = "GridServerButton"
        Me.GridServerButton.Size = New System.Drawing.Size(206, 24)
        Me.GridServerButton.TabIndex = 1879
        Me.GridServerButton.TabStop = True
        Me.GridServerButton.Text = Global.Outworldz.My.Resources.Resources.Grid_Server_With_Robust_word
        Me.GridServerButton.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(372, 35)
        Me.MenuStrip1.TabIndex = 1886
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ServerTypeToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(89, 29)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'ServerTypeToolStripMenuItem
        '
        Me.ServerTypeToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ServerTypeToolStripMenuItem.Name = "ServerTypeToolStripMenuItem"
        Me.ServerTypeToolStripMenuItem.Size = New System.Drawing.Size(205, 34)
        Me.ServerTypeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Server_Type_word
        '
        'FormServerType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 331)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FormServerType"
        Me.Text = Global.Outworldz.My.Resources.Resources.Server_Type_word
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MetroRadioButton2 As RadioButton
    Friend WithEvents SaveButton As Button
    Friend WithEvents GridRegionButton As RadioButton
    Friend WithEvents osGridRadioButton1 As RadioButton
    Friend WithEvents GridServerButton As RadioButton
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ServerTypeToolStripMenuItem As ToolStripMenuItem
End Class
