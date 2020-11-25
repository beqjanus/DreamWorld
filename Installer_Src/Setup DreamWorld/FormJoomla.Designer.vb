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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.AdminButton = New System.Windows.Forms.Button()
        Me.ViewButton = New System.Windows.Forms.Button()
        Me.InstallButton = New System.Windows.Forms.Button()
        Me.JEnableCheckBox = New System.Windows.Forms.CheckBox()
        Me.NoSearch = New System.Windows.Forms.GroupBox()
        Me.JOpensimRadioButton = New System.Windows.Forms.RadioButton()
        Me.HypericaRadioButton = New System.Windows.Forms.RadioButton()
        Me.NoSearchRadioButton = New System.Windows.Forms.RadioButton()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.NoSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(422, 28)
        Me.MenuStrip1.TabIndex = 0
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.AdminButton)
        Me.GroupBox1.Controls.Add(Me.ViewButton)
        Me.GroupBox1.Controls.Add(Me.InstallButton)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 73)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(192, 161)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Settings"
        '
        'AdminButton
        '
        Me.AdminButton.Image = CType(resources.GetObject("AdminButton.Image"), System.Drawing.Image)
        Me.AdminButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.AdminButton.Location = New System.Drawing.Point(24, 70)
        Me.AdminButton.Name = "AdminButton"
        Me.AdminButton.Size = New System.Drawing.Size(148, 36)
        Me.AdminButton.TabIndex = 4
        Me.AdminButton.Text = Global.Outworldz.My.Resources.Resources.AdministerJoomla_word
        Me.AdminButton.UseVisualStyleBackColor = True
        '
        'ViewButton
        '
        Me.ViewButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.ViewButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.ViewButton.Location = New System.Drawing.Point(24, 112)
        Me.ViewButton.Name = "ViewButton"
        Me.ViewButton.Size = New System.Drawing.Size(148, 33)
        Me.ViewButton.TabIndex = 3
        Me.ViewButton.Text = Global.Outworldz.My.Resources.Resources.ViewJoomla_word
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'InstallButton
        '
        Me.InstallButton.Image = Global.Outworldz.My.Resources.Resources.gear_run
        Me.InstallButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InstallButton.Location = New System.Drawing.Point(24, 33)
        Me.InstallButton.Name = "InstallButton"
        Me.InstallButton.Size = New System.Drawing.Size(148, 30)
        Me.InstallButton.TabIndex = 0
        Me.InstallButton.Text = Global.Outworldz.My.Resources.Resources.InstallJoomla_word
        Me.InstallButton.UseVisualStyleBackColor = True
        '
        'JEnableCheckBox
        '
        Me.JEnableCheckBox.AutoSize = True
        Me.JEnableCheckBox.Location = New System.Drawing.Point(36, 39)
        Me.JEnableCheckBox.Name = "JEnableCheckBox"
        Me.JEnableCheckBox.Size = New System.Drawing.Size(146, 17)
        Me.JEnableCheckBox.TabIndex = 2
        Me.JEnableCheckBox.Text = "Enable Joomla/JOpensim"
        Me.JEnableCheckBox.UseVisualStyleBackColor = True
        '
        'NoSearch
        '
        Me.NoSearch.Controls.Add(Me.NoSearchRadioButton)
        Me.NoSearch.Controls.Add(Me.JOpensimRadioButton)
        Me.NoSearch.Controls.Add(Me.HypericaRadioButton)
        Me.NoSearch.Location = New System.Drawing.Point(228, 73)
        Me.NoSearch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.NoSearch.Name = "NoSearch"
        Me.NoSearch.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.NoSearch.Size = New System.Drawing.Size(183, 161)
        Me.NoSearch.TabIndex = 3
        Me.NoSearch.TabStop = False
        Me.NoSearch.Text = "Options"
        '
        'JOpensimRadioButton
        '
        Me.JOpensimRadioButton.AutoSize = True
        Me.JOpensimRadioButton.Location = New System.Drawing.Point(21, 92)
        Me.JOpensimRadioButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.JOpensimRadioButton.Name = "JOpensimRadioButton"
        Me.JOpensimRadioButton.Size = New System.Drawing.Size(108, 17)
        Me.JOpensimRadioButton.TabIndex = 5
        Me.JOpensimRadioButton.TabStop = True
        Me.JOpensimRadioButton.Text = Global.Outworldz.My.Resources.Resources.JOpensimSearch_word
        Me.JOpensimRadioButton.UseVisualStyleBackColor = True
        '
        'HypericaRadioButton
        '
        Me.HypericaRadioButton.AutoSize = True
        Me.HypericaRadioButton.Location = New System.Drawing.Point(21, 70)
        Me.HypericaRadioButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.HypericaRadioButton.Name = "HypericaRadioButton"
        Me.HypericaRadioButton.Size = New System.Drawing.Size(104, 17)
        Me.HypericaRadioButton.TabIndex = 4
        Me.HypericaRadioButton.TabStop = True
        Me.HypericaRadioButton.Text = Global.Outworldz.My.Resources.Resources.HypericaSearch_word
        Me.HypericaRadioButton.UseVisualStyleBackColor = True
        '
        'NoSearchRadioButton
        '
        Me.NoSearchRadioButton.AutoSize = True
        Me.NoSearchRadioButton.Location = New System.Drawing.Point(21, 49)
        Me.NoSearchRadioButton.Margin = New System.Windows.Forms.Padding(2)
        Me.NoSearchRadioButton.Name = "NoSearchRadioButton"
        Me.NoSearchRadioButton.Size = New System.Drawing.Size(97, 17)
        Me.NoSearchRadioButton.TabIndex = 6
        Me.NoSearchRadioButton.TabStop = True
        Me.NoSearchRadioButton.Text = "Disable Search"
        Me.NoSearchRadioButton.UseVisualStyleBackColor = True
        '
        'FormJoomla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(422, 245)
        Me.Controls.Add(Me.NoSearch)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.JEnableCheckBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormJoomla"
        Me.Text = "JOpensim"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.NoSearch.ResumeLayout(False)
        Me.NoSearch.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents JEnableCheckBox As CheckBox
    Friend WithEvents InstallButton As Button
    Friend WithEvents ViewButton As Button
    Friend WithEvents AdminButton As Button
    Friend WithEvents NoSearch As GroupBox
    Friend WithEvents JOpensimRadioButton As RadioButton
    Friend WithEvents HypericaRadioButton As RadioButton
    Friend WithEvents NoSearchRadioButton As RadioButton
End Class
