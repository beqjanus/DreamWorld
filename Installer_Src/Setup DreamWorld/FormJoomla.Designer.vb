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
        Me.SearchBox = New System.Windows.Forms.GroupBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.JOpensimRadioButton = New System.Windows.Forms.RadioButton()
        Me.HypericaRadioButton = New System.Windows.Forms.RadioButton()
        Me.MenuStrip1.SuspendLayout()
        Me.ButtonBox.SuspendLayout()
        Me.SearchBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(563, 28)
        Me.MenuStrip1.TabIndex = 0
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(75, 24)
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
        Me.ButtonBox.Location = New System.Drawing.Point(16, 57)
        Me.ButtonBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonBox.Name = "ButtonBox"
        Me.ButtonBox.Padding = New System.Windows.Forms.Padding(4)
        Me.ButtonBox.Size = New System.Drawing.Size(256, 359)
        Me.ButtonBox.TabIndex = 1
        Me.ButtonBox.TabStop = False
        Me.ButtonBox.Text = "Settings"
        '
        'BackupButton
        '
        Me.BackupButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.BackupButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BackupButton.Location = New System.Drawing.Point(32, 236)
        Me.BackupButton.Margin = New System.Windows.Forms.Padding(4)
        Me.BackupButton.Name = "BackupButton"
        Me.BackupButton.Size = New System.Drawing.Size(197, 41)
        Me.BackupButton.TabIndex = 7
        Me.BackupButton.Text = "Backup"
        Me.BackupButton.UseVisualStyleBackColor = True
        '
        'ReinstallButton
        '
        Me.ReinstallButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.ReinstallButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ReinstallButton.Location = New System.Drawing.Point(32, 285)
        Me.ReinstallButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ReinstallButton.Name = "ReinstallButton"
        Me.ReinstallButton.Size = New System.Drawing.Size(197, 41)
        Me.ReinstallButton.TabIndex = 6
        Me.ReinstallButton.Text = "Restore"
        Me.ReinstallButton.UseVisualStyleBackColor = True
        '
        'UpdateButton
        '
        Me.UpdateButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.UpdateButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.UpdateButton.Location = New System.Drawing.Point(32, 187)
        Me.UpdateButton.Margin = New System.Windows.Forms.Padding(4)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(197, 41)
        Me.UpdateButton.TabIndex = 5
        Me.UpdateButton.Text = "Update"
        Me.UpdateButton.UseVisualStyleBackColor = True
        '
        'AdminButton
        '
        Me.AdminButton.Image = CType(resources.GetObject("AdminButton.Image"), System.Drawing.Image)
        Me.AdminButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.AdminButton.Location = New System.Drawing.Point(32, 86)
        Me.AdminButton.Margin = New System.Windows.Forms.Padding(4)
        Me.AdminButton.Name = "AdminButton"
        Me.AdminButton.Size = New System.Drawing.Size(197, 44)
        Me.AdminButton.TabIndex = 4
        Me.AdminButton.Text = Global.Outworldz.My.Resources.Resources.AdministerJoomla_word
        Me.AdminButton.UseVisualStyleBackColor = True
        '
        'ViewButton
        '
        Me.ViewButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.ViewButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ViewButton.Location = New System.Drawing.Point(32, 138)
        Me.ViewButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ViewButton.Name = "ViewButton"
        Me.ViewButton.Size = New System.Drawing.Size(197, 41)
        Me.ViewButton.TabIndex = 3
        Me.ViewButton.Text = Global.Outworldz.My.Resources.Resources.ViewJoomla_word
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'InstallButton
        '
        Me.InstallButton.Image = Global.Outworldz.My.Resources.Resources.gear_run
        Me.InstallButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InstallButton.Location = New System.Drawing.Point(32, 41)
        Me.InstallButton.Margin = New System.Windows.Forms.Padding(4)
        Me.InstallButton.Name = "InstallButton"
        Me.InstallButton.Size = New System.Drawing.Size(197, 37)
        Me.InstallButton.TabIndex = 0
        Me.InstallButton.Text = Global.Outworldz.My.Resources.Resources.InstallJoomla_word
        Me.InstallButton.UseVisualStyleBackColor = True
        '
        'SearchBox
        '
        Me.SearchBox.Controls.Add(Me.RadioButton2)
        Me.SearchBox.Controls.Add(Me.JOpensimRadioButton)
        Me.SearchBox.Controls.Add(Me.HypericaRadioButton)
        Me.SearchBox.Location = New System.Drawing.Point(299, 57)
        Me.SearchBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SearchBox.Size = New System.Drawing.Size(244, 139)
        Me.SearchBox.TabIndex = 3
        Me.SearchBox.TabStop = False
        Me.SearchBox.Text = "Search Options"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(29, 41)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(96, 21)
        Me.RadioButton2.TabIndex = 6
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "No Search"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'JOpensimRadioButton
        '
        Me.JOpensimRadioButton.AutoSize = True
        Me.JOpensimRadioButton.Location = New System.Drawing.Point(29, 98)
        Me.JOpensimRadioButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.JOpensimRadioButton.Name = "JOpensimRadioButton"
        Me.JOpensimRadioButton.Size = New System.Drawing.Size(141, 21)
        Me.JOpensimRadioButton.TabIndex = 5
        Me.JOpensimRadioButton.TabStop = True
        Me.JOpensimRadioButton.Text = Global.Outworldz.My.Resources.Resources.JOpensimSearch_word
        Me.JOpensimRadioButton.UseVisualStyleBackColor = True
        '
        'HypericaRadioButton
        '
        Me.HypericaRadioButton.AutoSize = True
        Me.HypericaRadioButton.Location = New System.Drawing.Point(29, 71)
        Me.HypericaRadioButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.HypericaRadioButton.Name = "HypericaRadioButton"
        Me.HypericaRadioButton.Size = New System.Drawing.Size(134, 21)
        Me.HypericaRadioButton.TabIndex = 4
        Me.HypericaRadioButton.TabStop = True
        Me.HypericaRadioButton.Text = Global.Outworldz.My.Resources.Resources.HypericaSearch_word
        Me.HypericaRadioButton.UseVisualStyleBackColor = True
        '
        'FormJoomla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 433)
        Me.Controls.Add(Me.SearchBox)
        Me.Controls.Add(Me.ButtonBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormJoomla"
        Me.Text = "JOpensim"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ButtonBox.ResumeLayout(False)
        Me.SearchBox.ResumeLayout(False)
        Me.SearchBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ButtonBox As GroupBox
    Friend WithEvents InstallButton As Button
    Friend WithEvents ViewButton As Button
    Friend WithEvents AdminButton As Button
    Friend WithEvents SearchBox As GroupBox
    Friend WithEvents JOpensimRadioButton As RadioButton
    Friend WithEvents HypericaRadioButton As RadioButton
    Friend WithEvents ReinstallButton As Button
    Friend WithEvents UpdateButton As Button
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents BackupButton As Button
End Class
