<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormJoomla
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormJoomla))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ButtonBox = New System.Windows.Forms.GroupBox()
        Me.BackupButton = New System.Windows.Forms.Button()
        Me.ReinstallButton = New System.Windows.Forms.Button()
        Me.UpdateButton = New System.Windows.Forms.Button()
        Me.AdminButton = New System.Windows.Forms.Button()
        Me.ViewButton = New System.Windows.Forms.Button()
        Me.InstallButton = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.ButtonBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(6, 3, 0, 3)
        Me.MenuStrip1.Size = New System.Drawing.Size(330, 38)
        Me.MenuStrip1.TabIndex = 0
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(93, 32)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'ButtonBox
        '
        Me.ButtonBox.Controls.Add(Me.BackupButton)
        Me.ButtonBox.Controls.Add(Me.ReinstallButton)
        Me.ButtonBox.Controls.Add(Me.UpdateButton)
        Me.ButtonBox.Controls.Add(Me.AdminButton)
        Me.ButtonBox.Controls.Add(Me.ViewButton)
        Me.ButtonBox.Controls.Add(Me.InstallButton)
        Me.ButtonBox.Location = New System.Drawing.Point(18, 71)
        Me.ButtonBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ButtonBox.Name = "ButtonBox"
        Me.ButtonBox.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ButtonBox.Size = New System.Drawing.Size(289, 430)
        Me.ButtonBox.TabIndex = 1
        Me.ButtonBox.TabStop = False
        Me.ButtonBox.Text = "Settings"
        '
        'BackupButton
        '
        Me.BackupButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.BackupButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BackupButton.Location = New System.Drawing.Point(36, 295)
        Me.BackupButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BackupButton.Name = "BackupButton"
        Me.BackupButton.Size = New System.Drawing.Size(214, 51)
        Me.BackupButton.TabIndex = 4
        Me.BackupButton.Text = "Backup"
        Me.BackupButton.UseVisualStyleBackColor = True
        '
        'ReinstallButton
        '
        Me.ReinstallButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.ReinstallButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ReinstallButton.Location = New System.Drawing.Point(36, 357)
        Me.ReinstallButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ReinstallButton.Name = "ReinstallButton"
        Me.ReinstallButton.Size = New System.Drawing.Size(214, 51)
        Me.ReinstallButton.TabIndex = 5
        Me.ReinstallButton.Text = "Restore"
        Me.ReinstallButton.UseVisualStyleBackColor = True
        '
        'UpdateButton
        '
        Me.UpdateButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.UpdateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UpdateButton.Location = New System.Drawing.Point(36, 234)
        Me.UpdateButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(214, 51)
        Me.UpdateButton.TabIndex = 3
        Me.UpdateButton.Text = "Update"
        Me.UpdateButton.UseVisualStyleBackColor = True
        '
        'AdminButton
        '
        Me.AdminButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.AdminButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AdminButton.Location = New System.Drawing.Point(36, 108)
        Me.AdminButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.AdminButton.Name = "AdminButton"
        Me.AdminButton.Size = New System.Drawing.Size(214, 55)
        Me.AdminButton.TabIndex = 1
        Me.AdminButton.Text = Global.Outworldz.My.Resources.Resources.AdministerJoomla_word
        Me.AdminButton.UseVisualStyleBackColor = True
        '
        'ViewButton
        '
        Me.ViewButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.ViewButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ViewButton.Location = New System.Drawing.Point(36, 172)
        Me.ViewButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ViewButton.Name = "ViewButton"
        Me.ViewButton.Size = New System.Drawing.Size(214, 55)
        Me.ViewButton.TabIndex = 2
        Me.ViewButton.Text = Global.Outworldz.My.Resources.Resources.ViewJoomla_word
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'InstallButton
        '
        Me.InstallButton.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.InstallButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InstallButton.Location = New System.Drawing.Point(36, 51)
        Me.InstallButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.InstallButton.Name = "InstallButton"
        Me.InstallButton.Size = New System.Drawing.Size(214, 46)
        Me.InstallButton.TabIndex = 0
        Me.InstallButton.Text = Global.Outworldz.My.Resources.Resources.InstallJoomla_word
        Me.InstallButton.UseVisualStyleBackColor = True
        '
        'FormJoomla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(330, 518)
        Me.Controls.Add(Me.ButtonBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FormJoomla"
        Me.Text = "JOpensim"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ButtonBox.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ButtonBox As GroupBox
    Friend WithEvents InstallButton As Button
    Friend WithEvents ViewButton As Button
    Friend WithEvents AdminButton As Button
    Friend WithEvents ReinstallButton As Button
    Friend WithEvents UpdateButton As Button
    Friend WithEvents BackupButton As Button
End Class
