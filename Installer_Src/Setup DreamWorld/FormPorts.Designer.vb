<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPorts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPorts))
        Me.GroupBoxA = New System.Windows.Forms.GroupBox()
        Me.OverrideNameLabel = New System.Windows.Forms.Label()
        Me.ExternalHostName = New System.Windows.Forms.TextBox()
        Me.Upnp = New System.Windows.Forms.PictureBox()
        Me.uPnPEnabled = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.HTTPPort = New System.Windows.Forms.TextBox()
        Me.PrivatePort = New System.Windows.Forms.TextBox()
        Me.DiagnosticPort = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MaxX = New System.Windows.Forms.Label()
        Me.MaxXLabel = New System.Windows.Forms.Label()
        Me.FirstXMLRegionPort = New System.Windows.Forms.TextBox()
        Me.MaxP = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.FirstRegionPort = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBoxB = New System.Windows.Forms.GroupBox()
        Me.GroupBoxA.SuspendLayout()
        CType(Me.Upnp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.GroupBoxB.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxA
        '
        Me.GroupBoxA.Controls.Add(Me.OverrideNameLabel)
        Me.GroupBoxA.Controls.Add(Me.ExternalHostName)
        Me.GroupBoxA.Controls.Add(Me.Upnp)
        Me.GroupBoxA.Controls.Add(Me.uPnPEnabled)
        Me.GroupBoxA.Controls.Add(Me.Label7)
        Me.GroupBoxA.Controls.Add(Me.Label5)
        Me.GroupBoxA.Controls.Add(Me.HTTPPort)
        Me.GroupBoxA.Controls.Add(Me.PrivatePort)
        Me.GroupBoxA.Controls.Add(Me.DiagnosticPort)
        Me.GroupBoxA.Controls.Add(Me.Label4)
        Me.GroupBoxA.Location = New System.Drawing.Point(15, 48)
        Me.GroupBoxA.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBoxA.Name = "GroupBoxA"
        Me.GroupBoxA.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBoxA.Size = New System.Drawing.Size(338, 251)
        Me.GroupBoxA.TabIndex = 45
        Me.GroupBoxA.TabStop = False
        Me.GroupBoxA.Text = "Ports"
        '
        'OverrideNameLabel
        '
        Me.OverrideNameLabel.AutoSize = True
        Me.OverrideNameLabel.Location = New System.Drawing.Point(25, 172)
        Me.OverrideNameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.OverrideNameLabel.Name = "OverrideNameLabel"
        Me.OverrideNameLabel.Size = New System.Drawing.Size(256, 17)
        Me.OverrideNameLabel.TabIndex = 1864
        Me.OverrideNameLabel.Text = "External HostName For Region Servers"
        Me.ToolTip1.SetToolTip(Me.OverrideNameLabel, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'ExternalHostName
        '
        Me.ExternalHostName.Location = New System.Drawing.Point(28, 204)
        Me.ExternalHostName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ExternalHostName.Name = "ExternalHostName"
        Me.ExternalHostName.Size = New System.Drawing.Size(272, 22)
        Me.ExternalHostName.TabIndex = 1863
        Me.ToolTip1.SetToolTip(Me.ExternalHostName, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'Upnp
        '
        Me.Upnp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.Upnp.Location = New System.Drawing.Point(228, 22)
        Me.Upnp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Upnp.Name = "Upnp"
        Me.Upnp.Size = New System.Drawing.Size(26, 29)
        Me.Upnp.TabIndex = 1859
        Me.Upnp.TabStop = False
        '
        'uPnPEnabled
        '
        Me.uPnPEnabled.AutoSize = True
        Me.uPnPEnabled.Location = New System.Drawing.Point(28, 29)
        Me.uPnPEnabled.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uPnPEnabled.Name = "uPnPEnabled"
        Me.uPnPEnabled.Size = New System.Drawing.Size(122, 21)
        Me.uPnPEnabled.TabIndex = 21
        Me.uPnPEnabled.Text = Global.Outworldz.My.Resources.Resources.UPnP_Enabled_word
        Me.ToolTip1.SetToolTip(Me.uPnPEnabled, Global.Outworldz.My.Resources.Resources.UPnP_Enabled_text)
        Me.uPnPEnabled.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(22, 129)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 17)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Private Port"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 64)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 17)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Diagnostics Port"
        '
        'HTTPPort
        '
        Me.HTTPPort.Location = New System.Drawing.Point(206, 91)
        Me.HTTPPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.HTTPPort.Name = "HTTPPort"
        Me.HTTPPort.Size = New System.Drawing.Size(58, 22)
        Me.HTTPPort.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.HTTPPort, Global.Outworldz.My.Resources.Resources.Default_8002_word)
        '
        'PrivatePort
        '
        Me.PrivatePort.Location = New System.Drawing.Point(206, 124)
        Me.PrivatePort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PrivatePort.Name = "PrivatePort"
        Me.PrivatePort.Size = New System.Drawing.Size(58, 22)
        Me.PrivatePort.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.PrivatePort, Global.Outworldz.My.Resources.Resources.Default_8003_word)
        '
        'DiagnosticPort
        '
        Me.DiagnosticPort.Location = New System.Drawing.Point(206, 59)
        Me.DiagnosticPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DiagnosticPort.Name = "DiagnosticPort"
        Me.DiagnosticPort.Size = New System.Drawing.Size(58, 22)
        Me.DiagnosticPort.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.DiagnosticPort, Global.Outworldz.My.Resources.Resources.Default_8001_word)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 95)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 17)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Http Port"
        '
        'MaxX
        '
        Me.MaxX.AutoSize = True
        Me.MaxX.Location = New System.Drawing.Point(35, 129)
        Me.MaxX.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MaxX.Name = "MaxX"
        Me.MaxX.Size = New System.Drawing.Size(95, 17)
        Me.MaxX.TabIndex = 1867
        Me.MaxX.Text = "Highest used:"
        '
        'MaxXLabel
        '
        Me.MaxXLabel.AutoSize = True
        Me.MaxXLabel.Location = New System.Drawing.Point(20, 101)
        Me.MaxXLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MaxXLabel.Name = "MaxXLabel"
        Me.MaxXLabel.Size = New System.Drawing.Size(140, 17)
        Me.MaxXLabel.TabIndex = 1866
        Me.MaxXLabel.Text = "XMLRPC Port Start #"
        '
        'FirstXMLRegionPort
        '
        Me.FirstXMLRegionPort.Location = New System.Drawing.Point(251, 92)
        Me.FirstXMLRegionPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.FirstXMLRegionPort.Name = "FirstXMLRegionPort"
        Me.FirstXMLRegionPort.Size = New System.Drawing.Size(58, 22)
        Me.FirstXMLRegionPort.TabIndex = 1865
        '
        'MaxP
        '
        Me.MaxP.AutoSize = True
        Me.MaxP.Location = New System.Drawing.Point(35, 71)
        Me.MaxP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MaxP.Name = "MaxP"
        Me.MaxP.Size = New System.Drawing.Size(95, 17)
        Me.MaxP.TabIndex = 1862
        Me.MaxP.Text = "Highest used:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(19, 42)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(129, 17)
        Me.Label26.TabIndex = 1861
        Me.Label26.Text = "Region Port Start #"
        '
        'FirstRegionPort
        '
        Me.FirstRegionPort.Location = New System.Drawing.Point(250, 34)
        Me.FirstRegionPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.FirstRegionPort.Name = "FirstRegionPort"
        Me.FirstRegionPort.Size = New System.Drawing.Size(58, 22)
        Me.FirstRegionPort.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.FirstRegionPort, Global.Outworldz.My.Resources.Resources.Default_8004_word)
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(5, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(714, 26)
        Me.MenuStrip2.TabIndex = 1890
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(75, 24)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(124, 26)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'GroupBoxB
        '
        Me.GroupBoxB.Controls.Add(Me.MaxX)
        Me.GroupBoxB.Controls.Add(Me.FirstXMLRegionPort)
        Me.GroupBoxB.Controls.Add(Me.MaxXLabel)
        Me.GroupBoxB.Controls.Add(Me.FirstRegionPort)
        Me.GroupBoxB.Controls.Add(Me.Label26)
        Me.GroupBoxB.Controls.Add(Me.MaxP)
        Me.GroupBoxB.Location = New System.Drawing.Point(360, 48)
        Me.GroupBoxB.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBoxB.Name = "GroupBoxB"
        Me.GroupBoxB.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBoxB.Size = New System.Drawing.Size(339, 251)
        Me.GroupBoxB.TabIndex = 1891
        Me.GroupBoxB.TabStop = False
        Me.GroupBoxB.Text = "Ports"
        '
        'FormPorts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(714, 334)
        Me.Controls.Add(Me.GroupBoxB)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBoxA)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FormPorts"
        Me.Text = "Region Ports"
        Me.GroupBoxA.ResumeLayout(False)
        Me.GroupBoxA.PerformLayout()
        CType(Me.Upnp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.GroupBoxB.ResumeLayout(False)
        Me.GroupBoxB.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBoxA As GroupBox
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
    Friend WithEvents GroupBoxB As GroupBox
End Class
