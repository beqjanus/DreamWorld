<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPorts
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPorts))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.MaxX = New System.Windows.Forms.Label()
        Me.MaxXLabel = New System.Windows.Forms.Label()
        Me.FirstXMLRegionPort = New System.Windows.Forms.TextBox()
        Me.OverrideNameLabel = New System.Windows.Forms.Label()
        Me.ExternalHostName = New System.Windows.Forms.TextBox()
        Me.MaxP = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.FirstRegionPort = New System.Windows.Forms.TextBox()
        Me.Upnp = New System.Windows.Forms.PictureBox()
        Me.uPnPEnabled = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.HTTPPort = New System.Windows.Forms.TextBox()
        Me.PrivatePort = New System.Windows.Forms.TextBox()
        Me.DiagnosticPort = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2.SuspendLayout()
        CType(Me.Upnp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.MaxX)
        Me.GroupBox2.Controls.Add(Me.MaxXLabel)
        Me.GroupBox2.Controls.Add(Me.FirstXMLRegionPort)
        Me.GroupBox2.Controls.Add(Me.OverrideNameLabel)
        Me.GroupBox2.Controls.Add(Me.ExternalHostName)
        Me.GroupBox2.Controls.Add(Me.MaxP)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.FirstRegionPort)
        Me.GroupBox2.Controls.Add(Me.Upnp)
        Me.GroupBox2.Controls.Add(Me.uPnPEnabled)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.HTTPPort)
        Me.GroupBox2.Controls.Add(Me.PrivatePort)
        Me.GroupBox2.Controls.Add(Me.DiagnosticPort)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(21, 67)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.GroupBox2.Size = New System.Drawing.Size(473, 560)
        Me.GroupBox2.TabIndex = 45
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Ports"
        '
        'MaxX
        '
        Me.MaxX.AutoSize = True
        Me.MaxX.Location = New System.Drawing.Point(55, 349)
        Me.MaxX.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.MaxX.Name = "MaxX"
        Me.MaxX.Size = New System.Drawing.Size(95, 17)
        Me.MaxX.TabIndex = 1867
        Me.MaxX.Text = "Highest used:"
        '
        'MaxXLabel
        '
        Me.MaxXLabel.AutoSize = True
        Me.MaxXLabel.Location = New System.Drawing.Point(34, 309)
        Me.MaxXLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.MaxXLabel.Name = "MaxXLabel"
        Me.MaxXLabel.Size = New System.Drawing.Size(140, 17)
        Me.MaxXLabel.TabIndex = 1866
        Me.MaxXLabel.Text = "XMLRPC Port Start #"
        '
        'FirstXMLRegionPort
        '
        Me.FirstXMLRegionPort.Location = New System.Drawing.Point(291, 301)
        Me.FirstXMLRegionPort.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.FirstXMLRegionPort.Name = "FirstXMLRegionPort"
        Me.FirstXMLRegionPort.Size = New System.Drawing.Size(80, 22)
        Me.FirstXMLRegionPort.TabIndex = 1865
        '
        'OverrideNameLabel
        '
        Me.OverrideNameLabel.AutoSize = True
        Me.OverrideNameLabel.Location = New System.Drawing.Point(35, 452)
        Me.OverrideNameLabel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.OverrideNameLabel.Name = "OverrideNameLabel"
        Me.OverrideNameLabel.Size = New System.Drawing.Size(256, 17)
        Me.OverrideNameLabel.TabIndex = 1864
        Me.OverrideNameLabel.Text = "External HostName For Region Servers"
        Me.ToolTip1.SetToolTip(Me.OverrideNameLabel, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'ExternalHostName
        '
        Me.ExternalHostName.Location = New System.Drawing.Point(38, 496)
        Me.ExternalHostName.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ExternalHostName.Name = "ExternalHostName"
        Me.ExternalHostName.Size = New System.Drawing.Size(379, 22)
        Me.ExternalHostName.TabIndex = 1863
        Me.ToolTip1.SetToolTip(Me.ExternalHostName, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'MaxP
        '
        Me.MaxP.AutoSize = True
        Me.MaxP.Location = New System.Drawing.Point(55, 267)
        Me.MaxP.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.MaxP.Name = "MaxP"
        Me.MaxP.Size = New System.Drawing.Size(95, 17)
        Me.MaxP.TabIndex = 1862
        Me.MaxP.Text = "Highest used:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(31, 227)
        Me.Label26.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(129, 17)
        Me.Label26.TabIndex = 1861
        Me.Label26.Text = "Region Port Start #"
        '
        'FirstRegionPort
        '
        Me.FirstRegionPort.Location = New System.Drawing.Point(288, 218)
        Me.FirstRegionPort.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.FirstRegionPort.Name = "FirstRegionPort"
        Me.FirstRegionPort.Size = New System.Drawing.Size(80, 22)
        Me.FirstRegionPort.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.FirstRegionPort, Global.Outworldz.My.Resources.Resources.Default_8004_word)
        '
        'Upnp
        '
        Me.Upnp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.Upnp.Location = New System.Drawing.Point(319, 31)
        Me.Upnp.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Upnp.Name = "Upnp"
        Me.Upnp.Size = New System.Drawing.Size(37, 40)
        Me.Upnp.TabIndex = 1859
        Me.Upnp.TabStop = False
        '
        'uPnPEnabled
        '
        Me.uPnPEnabled.AutoSize = True
        Me.uPnPEnabled.Location = New System.Drawing.Point(39, 41)
        Me.uPnPEnabled.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.uPnPEnabled.Name = "uPnPEnabled"
        Me.uPnPEnabled.Size = New System.Drawing.Size(126, 21)
        Me.uPnPEnabled.TabIndex = 21
        Me.uPnPEnabled.Text = Global.Outworldz.My.Resources.Resources.UPnP_Enabled_word
        Me.ToolTip1.SetToolTip(Me.uPnPEnabled, Global.Outworldz.My.Resources.Resources.UPnP_Enabled_text)
        Me.uPnPEnabled.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 181)
        Me.Label7.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 17)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Private Port"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(34, 90)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 17)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Diagnostics Port"
        '
        'HTTPPort
        '
        Me.HTTPPort.Location = New System.Drawing.Point(288, 127)
        Me.HTTPPort.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.HTTPPort.Name = "HTTPPort"
        Me.HTTPPort.Size = New System.Drawing.Size(80, 22)
        Me.HTTPPort.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.HTTPPort, Global.Outworldz.My.Resources.Resources.Default_8002_word)
        '
        'PrivatePort
        '
        Me.PrivatePort.Location = New System.Drawing.Point(288, 174)
        Me.PrivatePort.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.PrivatePort.Name = "PrivatePort"
        Me.PrivatePort.Size = New System.Drawing.Size(80, 22)
        Me.PrivatePort.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.PrivatePort, Global.Outworldz.My.Resources.Resources.Default_8003_word)
        '
        'DiagnosticPort
        '
        Me.DiagnosticPort.Location = New System.Drawing.Point(288, 83)
        Me.DiagnosticPort.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.DiagnosticPort.Name = "DiagnosticPort"
        Me.DiagnosticPort.Size = New System.Drawing.Size(80, 22)
        Me.DiagnosticPort.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.DiagnosticPort, Global.Outworldz.My.Resources.Resources.Default_8001_word)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 133)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 17)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Http Port"
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(7, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(540, 36)
        Me.MenuStrip2.TabIndex = 1890
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(94, 34)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(174, 40)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormPorts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(168.0!, 168.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(540, 666)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Name = "FormPorts"
        Me.Text = "Region Ports"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.Upnp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents MaxP As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents FirstRegionPort As TextBox
    Friend WithEvents Upnp As PictureBox
    Friend WithEvents uPnPEnabled As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents HTTPPort As TextBox
    Friend WithEvents PrivatePort As TextBox
    Friend WithEvents DiagnosticPort As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents OverrideNameLabel As Label
    Friend WithEvents ExternalHostName As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MaxX As Label
    Friend WithEvents MaxXLabel As Label
    Friend WithEvents FirstXMLRegionPort As TextBox
End Class
