<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormDNSName
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDNSName))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuitcaseCheckbox = New System.Windows.Forms.CheckBox()
        Me.EnableHypergrid = New System.Windows.Forms.CheckBox()
        Me.UniqueId = New System.Windows.Forms.TextBox()
        Me.TestButton1 = New System.Windows.Forms.Button()
        Me.NextNameButton = New System.Windows.Forms.Button()
        Me.DNSNameBox = New System.Windows.Forms.TextBox()
        Me.SaveButton1 = New System.Windows.Forms.Button()
        Me.DNSAliasTextBox = New System.Windows.Forms.TextBox()
        Me.OsGridButton = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OsGridButton.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipTitle = "If marked Public, this sim will be published to an online directory so others peo" &
    "pkle on the hypergrid can find it."
        '
        'SuitcaseCheckbox
        '
        Me.SuitcaseCheckbox.AutoSize = True
        Me.SuitcaseCheckbox.Location = New System.Drawing.Point(28, 73)
        Me.SuitcaseCheckbox.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.SuitcaseCheckbox.Name = "SuitcaseCheckbox"
        Me.SuitcaseCheckbox.Size = New System.Drawing.Size(265, 29)
        Me.SuitcaseCheckbox.TabIndex = 1877
        Me.SuitcaseCheckbox.Text = Global.Outworldz.My.Resources.Resources.Suitcase_enable
        Me.ToolTip1.SetToolTip(Me.SuitcaseCheckbox, Global.Outworldz.My.Resources.Resources.Disable_Suitcase_txt)
        Me.SuitcaseCheckbox.UseVisualStyleBackColor = True
        '
        'EnableHypergrid
        '
        Me.EnableHypergrid.AutoSize = True
        Me.EnableHypergrid.Location = New System.Drawing.Point(28, 33)
        Me.EnableHypergrid.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.EnableHypergrid.Name = "EnableHypergrid"
        Me.EnableHypergrid.Size = New System.Drawing.Size(188, 29)
        Me.EnableHypergrid.TabIndex = 1873
        Me.EnableHypergrid.Text = Global.Outworldz.My.Resources.Resources.Enable_Hypergrid_word
        Me.ToolTip1.SetToolTip(Me.EnableHypergrid, Global.Outworldz.My.Resources.Resources.Enable_Hypergrid_word)
        Me.EnableHypergrid.UseVisualStyleBackColor = True
        '
        'UniqueId
        '
        Me.UniqueId.Location = New System.Drawing.Point(30, 324)
        Me.UniqueId.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.UniqueId.Name = "UniqueId"
        Me.UniqueId.Size = New System.Drawing.Size(228, 29)
        Me.UniqueId.TabIndex = 1875
        Me.ToolTip1.SetToolTip(Me.UniqueId, Global.Outworldz.My.Resources.Resources.Reserve_Password)
        '
        'TestButton1
        '
        Me.TestButton1.Location = New System.Drawing.Point(250, 406)
        Me.TestButton1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TestButton1.Name = "TestButton1"
        Me.TestButton1.Size = New System.Drawing.Size(170, 40)
        Me.TestButton1.TabIndex = 1872
        Me.TestButton1.Text = Global.Outworldz.My.Resources.Resources.Test_DNS_word
        Me.ToolTip1.SetToolTip(Me.TestButton1, Global.Outworldz.My.Resources.Resources.Test_DNS_word)
        Me.TestButton1.UseVisualStyleBackColor = True
        '
        'NextNameButton
        '
        Me.NextNameButton.Location = New System.Drawing.Point(44, 406)
        Me.NextNameButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.NextNameButton.Name = "NextNameButton"
        Me.NextNameButton.Size = New System.Drawing.Size(178, 40)
        Me.NextNameButton.TabIndex = 1870
        Me.NextNameButton.Text = Global.Outworldz.My.Resources.Resources.Next_Name
        Me.ToolTip1.SetToolTip(Me.NextNameButton, Global.Outworldz.My.Resources.Resources.FreeName)
        Me.NextNameButton.UseVisualStyleBackColor = True
        '
        'DNSNameBox
        '
        Me.DNSNameBox.Location = New System.Drawing.Point(28, 152)
        Me.DNSNameBox.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.DNSNameBox.Name = "DNSNameBox"
        Me.DNSNameBox.Size = New System.Drawing.Size(515, 29)
        Me.DNSNameBox.TabIndex = 1869
        Me.ToolTip1.SetToolTip(Me.DNSNameBox, Global.Outworldz.My.Resources.Resources.AlphaNum)
        '
        'SaveButton1
        '
        Me.SaveButton1.Location = New System.Drawing.Point(446, 406)
        Me.SaveButton1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.SaveButton1.Name = "SaveButton1"
        Me.SaveButton1.Size = New System.Drawing.Size(159, 40)
        Me.SaveButton1.TabIndex = 1879
        Me.SaveButton1.Text = Global.Outworldz.My.Resources.Resources.Save_word
        Me.ToolTip1.SetToolTip(Me.SaveButton1, Global.Outworldz.My.Resources.Resources.Save_word)
        Me.SaveButton1.UseVisualStyleBackColor = True
        '
        'DNSAliasTextBox
        '
        Me.DNSAliasTextBox.Location = New System.Drawing.Point(30, 234)
        Me.DNSAliasTextBox.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.DNSAliasTextBox.Name = "DNSAliasTextBox"
        Me.DNSAliasTextBox.Size = New System.Drawing.Size(515, 29)
        Me.DNSAliasTextBox.TabIndex = 1880
        Me.ToolTip1.SetToolTip(Me.DNSAliasTextBox, Global.Outworldz.My.Resources.Resources.DNSAlt)
        '
        'OsGridButton
        '
        Me.OsGridButton.Controls.Add(Me.Label3)
        Me.OsGridButton.Controls.Add(Me.DNSAliasTextBox)
        Me.OsGridButton.Controls.Add(Me.SaveButton1)
        Me.OsGridButton.Controls.Add(Me.Label2)
        Me.OsGridButton.Controls.Add(Me.SuitcaseCheckbox)
        Me.OsGridButton.Controls.Add(Me.Label1)
        Me.OsGridButton.Controls.Add(Me.EnableHypergrid)
        Me.OsGridButton.Controls.Add(Me.UniqueId)
        Me.OsGridButton.Controls.Add(Me.TestButton1)
        Me.OsGridButton.Controls.Add(Me.NextNameButton)
        Me.OsGridButton.Controls.Add(Me.DNSNameBox)
        Me.OsGridButton.Location = New System.Drawing.Point(37, 59)
        Me.OsGridButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.OsGridButton.Name = "OsGridButton"
        Me.OsGridButton.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.OsGridButton.Size = New System.Drawing.Size(625, 457)
        Me.OsGridButton.TabIndex = 1869
        Me.OsGridButton.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 206)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(412, 25)
        Me.Label3.TabIndex = 1881
        Me.Label3.Text = "DNS Name or SomeName.Outworldz.net or IP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 124)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(412, 25)
        Me.Label2.TabIndex = 1878
        Me.Label2.Text = "DNS Name or SomeName.Outworldz.net or IP"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 296)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(224, 25)
        Me.Label1.TabIndex = 1876
        Me.Label1.Text = "Dynamic DNS password"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(677, 38)
        Me.MenuStrip1.TabIndex = 18600
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(102, 34)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormDNSName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(168.0!, 168.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(677, 537)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.OsGridButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Name = "FormDNSName"
        Me.Text = "DNS Name & Hypergrid"
        Me.ToolTip1.SetToolTip(Me, Global.Outworldz.My.Resources.Resources.Help_word)
        Me.OsGridButton.ResumeLayout(False)
        Me.OsGridButton.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents OsGridButton As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents SuitcaseCheckbox As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents EnableHypergrid As CheckBox
    Friend WithEvents UniqueId As TextBox
    Friend WithEvents TestButton1 As Button
    Friend WithEvents NextNameButton As Button
    Friend WithEvents DNSNameBox As TextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents SaveButton1 As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label3 As Label
    Friend WithEvents DNSAliasTextBox As TextBox
End Class
