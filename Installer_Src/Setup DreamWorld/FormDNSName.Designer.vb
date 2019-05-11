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
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.DNSNameBox = New System.Windows.Forms.TextBox()
        Me.OsGridButton = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.GridServerButton = New System.Windows.Forms.RadioButton()
        Me.GridRegionButton = New System.Windows.Forms.RadioButton()
        Me.osGridRadioButton1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MetroRadioButton2 = New System.Windows.Forms.RadioButton()
        CType(Me.DynDNSHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OsGridButton.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.TestButton1.Location = New System.Drawing.Point(180, 162)
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
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(316, 164)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(99, 23)
        Me.SaveButton.TabIndex = 1871
        Me.SaveButton.Text = "Save Settings"
        Me.ToolTip1.SetToolTip(Me.SaveButton, "Save DNS name")
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'DNSNameBox
        '
        Me.DNSNameBox.Location = New System.Drawing.Point(16, 87)
        Me.DNSNameBox.Name = "DNSNameBox"
        Me.DNSNameBox.Size = New System.Drawing.Size(253, 20)
        Me.DNSNameBox.TabIndex = 1869
        Me.ToolTip1.SetToolTip(Me.DNSNameBox, "Alpha-Numeric plus - ( no spaces or special chars)")
        '
        'OsGridButton
        '
        Me.OsGridButton.Controls.Add(Me.Label2)
        Me.OsGridButton.Controls.Add(Me.SuitcaseCheckbox)
        Me.OsGridButton.Controls.Add(Me.Label1)
        Me.OsGridButton.Controls.Add(Me.EnableHypergrid)
        Me.OsGridButton.Controls.Add(Me.DynDNSHelp)
        Me.OsGridButton.Controls.Add(Me.UniqueId)
        Me.OsGridButton.Controls.Add(Me.TestButton1)
        Me.OsGridButton.Controls.Add(Me.NextNameButton)
        Me.OsGridButton.Controls.Add(Me.DNSNameBox)
        Me.OsGridButton.Location = New System.Drawing.Point(12, 2)
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
        'GridServerButton
        '
        Me.GridServerButton.AutoSize = True
        Me.GridServerButton.Location = New System.Drawing.Point(17, 32)
        Me.GridServerButton.Name = "GridServerButton"
        Me.GridServerButton.Size = New System.Drawing.Size(140, 17)
        Me.GridServerButton.TabIndex = 1879
        Me.GridServerButton.TabStop = True
        Me.GridServerButton.Text = "Grid Server With Robust"
        Me.GridServerButton.UseVisualStyleBackColor = True
        '
        'GridRegionButton
        '
        Me.GridRegionButton.AutoSize = True
        Me.GridRegionButton.Location = New System.Drawing.Point(17, 55)
        Me.GridRegionButton.Name = "GridRegionButton"
        Me.GridRegionButton.Size = New System.Drawing.Size(93, 17)
        Me.GridRegionButton.TabIndex = 1880
        Me.GridRegionButton.TabStop = True
        Me.GridRegionButton.Text = "Region Server"
        Me.GridRegionButton.UseVisualStyleBackColor = True
        '
        'osGridRadioButton1
        '
        Me.osGridRadioButton1.AutoSize = True
        Me.osGridRadioButton1.Location = New System.Drawing.Point(17, 77)
        Me.osGridRadioButton1.Name = "osGridRadioButton1"
        Me.osGridRadioButton1.Size = New System.Drawing.Size(130, 17)
        Me.osGridRadioButton1.TabIndex = 1881
        Me.osGridRadioButton1.TabStop = True
        Me.osGridRadioButton1.Text = "OSGrid Region Server"
        Me.osGridRadioButton1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MetroRadioButton2)
        Me.GroupBox1.Controls.Add(Me.GridRegionButton)
        Me.GroupBox1.Controls.Add(Me.osGridRadioButton1)
        Me.GroupBox1.Controls.Add(Me.GridServerButton)
        Me.GroupBox1.Location = New System.Drawing.Point(305, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(190, 142)
        Me.GroupBox1.TabIndex = 1882
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Server Type"
        '
        'MetroRadioButton2
        '
        Me.MetroRadioButton2.AutoSize = True
        Me.MetroRadioButton2.Location = New System.Drawing.Point(17, 100)
        Me.MetroRadioButton2.Name = "MetroRadioButton2"
        Me.MetroRadioButton2.Size = New System.Drawing.Size(162, 17)
        Me.MetroRadioButton2.TabIndex = 1882
        Me.MetroRadioButton2.TabStop = True
        Me.MetroRadioButton2.Text = "Hypergrid.org Region  Server"
        Me.MetroRadioButton2.UseVisualStyleBackColor = True
        '
        'FormDNSName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(534, 227)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.OsGridButton)
        Me.Controls.Add(Me.SaveButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormDNSName"
        Me.Text = "DNS Name & Hypergrid"
        Me.ToolTip1.SetToolTip(Me, "Get Help")
        CType(Me.DynDNSHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.OsGridButton.ResumeLayout(False)
        Me.OsGridButton.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

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
    Friend WithEvents SaveButton As Button
    Friend WithEvents DNSNameBox As TextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents osGridRadioButton1 As RadioButton
    Friend WithEvents GridRegionButton As RadioButton
    Friend WithEvents GridServerButton As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MetroRadioButton2 As RadioButton
End Class
