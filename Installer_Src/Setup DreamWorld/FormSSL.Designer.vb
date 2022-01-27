<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSSL
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Email = New System.Windows.Forms.TextBox()
        Me.OU = New System.Windows.Forms.TextBox()
        Me.State = New System.Windows.Forms.TextBox()
        Me.Label1Email = New System.Windows.Forms.Label()
        Me.LabelOrganization = New System.Windows.Forms.Label()
        Me.LabelState = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Locale = New System.Windows.Forms.TextBox()
        Me.CountryName = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(16, 182)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 28)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Create Certificate"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Email
        '
        Me.Email.Location = New System.Drawing.Point(16, 20)
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(174, 20)
        Me.Email.TabIndex = 6
        '
        'OU
        '
        Me.OU.Location = New System.Drawing.Point(16, 46)
        Me.OU.Name = "OU"
        Me.OU.Size = New System.Drawing.Size(174, 20)
        Me.OU.TabIndex = 7
        '
        'State
        '
        Me.State.Location = New System.Drawing.Point(16, 72)
        Me.State.Name = "State"
        Me.State.Size = New System.Drawing.Size(174, 20)
        Me.State.TabIndex = 8
        '
        'Label1Email
        '
        Me.Label1Email.AutoSize = True
        Me.Label1Email.Location = New System.Drawing.Point(197, 26)
        Me.Label1Email.Name = "Label1Email"
        Me.Label1Email.Size = New System.Drawing.Size(35, 13)
        Me.Label1Email.TabIndex = 9
        Me.Label1Email.Text = "Email "
        '
        'LabelOrganization
        '
        Me.LabelOrganization.AutoSize = True
        Me.LabelOrganization.Location = New System.Drawing.Point(197, 49)
        Me.LabelOrganization.Name = "LabelOrganization"
        Me.LabelOrganization.Size = New System.Drawing.Size(66, 13)
        Me.LabelOrganization.TabIndex = 10
        Me.LabelOrganization.Text = "Organization"
        '
        'LabelState
        '
        Me.LabelState.AutoSize = True
        Me.LabelState.Location = New System.Drawing.Point(197, 75)
        Me.LabelState.Name = "LabelState"
        Me.LabelState.Size = New System.Drawing.Size(32, 13)
        Me.LabelState.TabIndex = 11
        Me.LabelState.Text = "State"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Locale)
        Me.GroupBox1.Controls.Add(Me.CountryName)
        Me.GroupBox1.Controls.Add(Me.LabelState)
        Me.GroupBox1.Controls.Add(Me.LabelOrganization)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label1Email)
        Me.GroupBox1.Controls.Add(Me.Email)
        Me.GroupBox1.Controls.Add(Me.State)
        Me.GroupBox1.Controls.Add(Me.OU)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 241)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SSL"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(200, 124)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Locale"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(200, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "CountryName"
        '
        'Locale
        '
        Me.Locale.Location = New System.Drawing.Point(16, 124)
        Me.Locale.Name = "Locale"
        Me.Locale.Size = New System.Drawing.Size(174, 20)
        Me.Locale.TabIndex = 13
        '
        'CountryName
        '
        Me.CountryName.Location = New System.Drawing.Point(16, 98)
        Me.CountryName.Name = "CountryName"
        Me.CountryName.Size = New System.Drawing.Size(174, 20)
        Me.CountryName.TabIndex = 12
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(344, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'FormSSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 281)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormSSL"
        Me.Text = "SSL"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Email As TextBox
    Friend WithEvents OU As TextBox
    Friend WithEvents State As TextBox
    Friend WithEvents Label1Email As Label
    Friend WithEvents LabelOrganization As Label
    Friend WithEvents LabelState As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Locale As TextBox
    Friend WithEvents CountryName As TextBox
End Class
