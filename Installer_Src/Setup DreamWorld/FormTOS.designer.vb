<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TosForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TosForm))
        Me.ShowToHGUsersCheckbox = New System.Windows.Forms.CheckBox()
        Me.ShowToLocalUsersCheckbox = New System.Windows.Forms.CheckBox()
        Me.TOSEnable = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Editor1 = New LiveSwitch.TextControl.Editor()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ApplyButton = New System.Windows.Forms.Button()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ShowToHGUsersCheckbox
        '
        Me.ShowToHGUsersCheckbox.AutoSize = True
        Me.ShowToHGUsersCheckbox.Location = New System.Drawing.Point(22, 109)
        Me.ShowToHGUsersCheckbox.Name = "ShowToHGUsersCheckbox"
        Me.ShowToHGUsersCheckbox.Size = New System.Drawing.Size(298, 17)
        Me.ShowToHGUsersCheckbox.TabIndex = 3
        Me.ShowToHGUsersCheckbox.Text = "Show TOS To Hypergrid Users on First Hyper grid Login?"
        Me.ShowToHGUsersCheckbox.UseVisualStyleBackColor = True
        Me.ShowToHGUsersCheckbox.Visible = False
        '
        'ShowToLocalUsersCheckbox
        '
        Me.ShowToLocalUsersCheckbox.AutoSize = True
        Me.ShowToLocalUsersCheckbox.Location = New System.Drawing.Point(22, 86)
        Me.ShowToLocalUsersCheckbox.Name = "ShowToLocalUsersCheckbox"
        Me.ShowToLocalUsersCheckbox.Size = New System.Drawing.Size(225, 17)
        Me.ShowToLocalUsersCheckbox.TabIndex = 2
        Me.ShowToLocalUsersCheckbox.Text = "Show TOS To Local Users on First Login?"
        Me.ShowToLocalUsersCheckbox.UseVisualStyleBackColor = True
        Me.ShowToLocalUsersCheckbox.Visible = False
        '
        'TOSEnable
        '
        Me.TOSEnable.AutoSize = True
        Me.TOSEnable.Location = New System.Drawing.Point(22, 63)
        Me.TOSEnable.Name = "TOSEnable"
        Me.TOSEnable.Size = New System.Drawing.Size(121, 17)
        Me.TOSEnable.TabIndex = 1
        Me.TOSEnable.Text = "Enable TOS module"
        Me.TOSEnable.UseVisualStyleBackColor = True
        Me.TOSEnable.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(317, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "The TOS module shows your users and/or visitors this HTML text."
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(251, 489)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = My.Resources.Preview_in_Browser
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(411, 489)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(227, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Require all users to re-agree to the TOS"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Editor1
        '
        Me.Editor1.BodyBackgroundColor = System.Drawing.Color.White
        Me.Editor1.BodyHtml = Nothing
        Me.Editor1.BodyText = Nothing
        Me.Editor1.DocumentText = resources.GetString("Editor1.DocumentText")
        Me.Editor1.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Editor1.EditorForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Editor1.FontSize = LiveSwitch.TextControl.FontSize.Three
        Me.Editor1.Html = Nothing
        Me.Editor1.Location = New System.Drawing.Point(10, 132)
        Me.Editor1.Name = "Editor1"
        Me.Editor1.Size = New System.Drawing.Size(636, 338)
        Me.Editor1.TabIndex = 7
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(10, 489)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 8
        Me.SaveButton.Text = "Ok"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ApplyButton
        '
        Me.ApplyButton.Location = New System.Drawing.Point(109, 489)
        Me.ApplyButton.Name = "ApplyButton"
        Me.ApplyButton.Size = New System.Drawing.Size(75, 23)
        Me.ApplyButton.TabIndex = 9
        Me.ApplyButton.Text = My.Resources.Apply
        Me.ApplyButton.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(660, 28)
        Me.MenuStrip2.TabIndex = 1887
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(64, 24)
        Me.ToolStripMenuItem30.Text = My.Resources.Help
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.DatabaseSetupToolStripMenuItem.Text = My.Resources.Help
        '
        'TosForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(660, 541)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.ApplyButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.Editor1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TOSEnable)
        Me.Controls.Add(Me.ShowToLocalUsersCheckbox)
        Me.Controls.Add(Me.ShowToHGUsersCheckbox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "TosForm"
        Me.Text = My.Resources.Terms_of_Service
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ShowToHGUsersCheckbox As CheckBox
    Friend WithEvents ShowToLocalUsersCheckbox As CheckBox
    Friend WithEvents TOSEnable As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Editor1 As LiveSwitch.TextControl.Editor
    Friend WithEvents SaveButton As Button
    Friend WithEvents ApplyButton As Button
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
