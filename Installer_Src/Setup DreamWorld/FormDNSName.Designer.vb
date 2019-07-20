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
        Me.DynDNSHelp = New System.Windows.Forms.PictureBox()
        Me.UniqueId = New System.Windows.Forms.TextBox()
        Me.TestButton1 = New System.Windows.Forms.Button()
        Me.NextNameButton = New System.Windows.Forms.Button()
        Me.DNSNameBox = New System.Windows.Forms.TextBox()
        Me.SaveButton1 = New System.Windows.Forms.Button()
        Me.OsGridButton = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DynDNSHelp, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SuitcaseCheckbox.Location = New System.Drawing.Point(16, 42)
        Me.SuitcaseCheckbox.Name = "SuitcaseCheckbox"
        Me.SuitcaseCheckbox.Size = New System.Drawing.Size(150, 17)
        Me.SuitcaseCheckbox.TabIndex = 1877
        Me.SuitcaseCheckbox.Text = "Enable Inventory Suitcase"
        Me.ToolTip1.SetToolTip(Me.SuitcaseCheckbox, "Disbale the suitcase is less secure but easier to travel with")
        Me.SuitcaseCheckbox.UseVisualStyleBackColor = True
        '
        'EnableHypergrid
        '
        Me.EnableHypergrid.AutoSize = True
        Me.EnableHypergrid.Location = New System.Drawing.Point(16, 19)
        Me.EnableHypergrid.Name = "EnableHypergrid"
        Me.EnableHypergrid.Size = New System.Drawing.Size(107, 17)
        Me.EnableHypergrid.TabIndex = 1873
        Me.EnableHypergrid.Text = "Enable Hypergrid"
        Me.ToolTip1.SetToolTip(Me.EnableHypergrid, "Enables Hypergrid to all sims.")
        Me.EnableHypergrid.UseVisualStyleBackColor = True
        '
        'DynDNSHelp
        '
        Me.DynDNSHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DynDNSHelp.Location = New System.Drawing.Point(239, 47)
        Me.DynDNSHelp.Name = "DynDNSHelp"
        Me.DynDNSHelp.Size = New System.Drawing.Size(30, 34)
        Me.DynDNSHelp.TabIndex = 1874
        Me.DynDNSHelp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.DynDNSHelp, "Click for Help")
        '
        'UniqueId
        '
        Me.UniqueId.Location = New System.Drawing.Point(137, 120)
        Me.UniqueId.Name = "UniqueId"
        Me.UniqueId.Size = New System.Drawing.Size(132, 20)
        Me.UniqueId.TabIndex = 1875
        Me.ToolTip1.SetToolTip(Me.UniqueId, "Reserves your DYN DNS name, this is like a password to your name.")
        '
        'TestButton1
        '
        Me.TestButton1.Location = New System.Drawing.Point(112, 162)
        Me.TestButton1.Name = "TestButton1"
        Me.TestButton1.Size = New System.Drawing.Size(75, 23)
        Me.TestButton1.TabIndex = 1872
        Me.TestButton1.Text = "Test DNS"
        Me.ToolTip1.SetToolTip(Me.TestButton1, "test DNS and return IP address")
        Me.TestButton1.UseVisualStyleBackColor = True
        '
        'NextNameButton
        '
        Me.NextNameButton.Location = New System.Drawing.Point(19, 162)
        Me.NextNameButton.Name = "NextNameButton"
        Me.NextNameButton.Size = New System.Drawing.Size(78, 23)
        Me.NextNameButton.TabIndex = 1870
        Me.NextNameButton.Text = "Next Name"
        Me.ToolTip1.SetToolTip(Me.NextNameButton, "Get a free DYN DNS Name")
        Me.NextNameButton.UseVisualStyleBackColor = True
        '
        'DNSNameBox
        '
        Me.DNSNameBox.Location = New System.Drawing.Point(16, 87)
        Me.DNSNameBox.Name = "DNSNameBox"
        Me.DNSNameBox.Size = New System.Drawing.Size(253, 20)
        Me.DNSNameBox.TabIndex = 1869
        Me.ToolTip1.SetToolTip(Me.DNSNameBox, "Alpha-Numeric plus - ( no spaces or special chars)")
        '
        'SaveButton1
        '
        Me.SaveButton1.Location = New System.Drawing.Point(206, 162)
        Me.SaveButton1.Name = "SaveButton1"
        Me.SaveButton1.Size = New System.Drawing.Size(63, 23)
        Me.SaveButton1.TabIndex = 1879
        Me.SaveButton1.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.SaveButton1, "test DNS and return IP address")
        Me.SaveButton1.UseVisualStyleBackColor = True
        '
        'OsGridButton
        '
        Me.OsGridButton.Controls.Add(Me.SaveButton1)
        Me.OsGridButton.Controls.Add(Me.Label2)
        Me.OsGridButton.Controls.Add(Me.SuitcaseCheckbox)
        Me.OsGridButton.Controls.Add(Me.Label1)
        Me.OsGridButton.Controls.Add(Me.EnableHypergrid)
        Me.OsGridButton.Controls.Add(Me.DynDNSHelp)
        Me.OsGridButton.Controls.Add(Me.UniqueId)
        Me.OsGridButton.Controls.Add(Me.TestButton1)
        Me.OsGridButton.Controls.Add(Me.NextNameButton)
        Me.OsGridButton.Controls.Add(Me.DNSNameBox)
        Me.OsGridButton.Location = New System.Drawing.Point(21, 34)
        Me.OsGridButton.Name = "OsGridButton"
        Me.OsGridButton.Size = New System.Drawing.Size(287, 213)
        Me.OsGridButton.TabIndex = 1869
        Me.OsGridButton.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(224, 13)
        Me.Label2.TabIndex = 1878
        Me.Label2.Text = "DNS Name or SomeName.Outworldz.net or IP"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 13)
        Me.Label1.TabIndex = 1876
        Me.Label1.Text = "DynDNS password"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(320, 24)
        Me.MenuStrip1.TabIndex = 18600
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'FormDNSName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(320, 273)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.OsGridButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormDNSName"
        Me.Text = "DNS Name & Hypergrid"
        Me.ToolTip1.SetToolTip(Me, "Get Help")
        CType(Me.DynDNSHelp, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents DynDNSHelp As PictureBox
    Friend WithEvents UniqueId As TextBox
    Friend WithEvents TestButton1 As Button
    Friend WithEvents NextNameButton As Button
    Friend WithEvents DNSNameBox As TextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents SaveButton1 As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
End Class
