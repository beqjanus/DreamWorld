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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.intIP = New System.Windows.Forms.TextBox()
        Me.FirstRegionPort = New System.Windows.Forms.TextBox()
        Me.OverrideNameLabel = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.ExternalHostName = New System.Windows.Forms.TextBox()
        Me.MaxP = New System.Windows.Forms.Label()
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
        Me.GroupBoxA.SuspendLayout()
        CType(Me.Upnp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxA
        '
        Me.GroupBoxA.Controls.Add(Me.Label1)
        Me.GroupBoxA.Controls.Add(Me.intIP)
        Me.GroupBoxA.Controls.Add(Me.FirstRegionPort)
        Me.GroupBoxA.Controls.Add(Me.OverrideNameLabel)
        Me.GroupBoxA.Controls.Add(Me.Label26)
        Me.GroupBoxA.Controls.Add(Me.ExternalHostName)
        Me.GroupBoxA.Controls.Add(Me.MaxP)
        Me.GroupBoxA.Controls.Add(Me.Upnp)
        Me.GroupBoxA.Controls.Add(Me.uPnPEnabled)
        Me.GroupBoxA.Controls.Add(Me.Label7)
        Me.GroupBoxA.Controls.Add(Me.Label5)
        Me.GroupBoxA.Controls.Add(Me.HTTPPort)
        Me.GroupBoxA.Controls.Add(Me.PrivatePort)
        Me.GroupBoxA.Controls.Add(Me.DiagnosticPort)
        Me.GroupBoxA.Controls.Add(Me.Label4)
        Me.GroupBoxA.Location = New System.Drawing.Point(12, 39)
        Me.GroupBoxA.Name = "GroupBoxA"
        Me.GroupBoxA.Size = New System.Drawing.Size(271, 266)
        Me.GroupBoxA.TabIndex = 0
        Me.GroupBoxA.TabStop = False
        Me.GroupBoxA.Text = "Ports"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 223)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 13)
        Me.Label1.TabIndex = 1860
        Me.Label1.Text = "Internal IP For Regions"
        Me.ToolTip1.SetToolTip(Me.Label1, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'intIP
        '
        Me.intIP.Location = New System.Drawing.Point(20, 239)
        Me.intIP.Name = "intIP"
        Me.intIP.Size = New System.Drawing.Size(219, 20)
        Me.intIP.TabIndex = 1861
        Me.ToolTip1.SetToolTip(Me.intIP, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'FirstRegionPort
        '
        Me.FirstRegionPort.Location = New System.Drawing.Point(165, 123)
        Me.FirstRegionPort.Name = "FirstRegionPort"
        Me.FirstRegionPort.Size = New System.Drawing.Size(47, 20)
        Me.FirstRegionPort.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.FirstRegionPort, Global.Outworldz.My.Resources.Resources.Default_8004_word)
        '
        'OverrideNameLabel
        '
        Me.OverrideNameLabel.AutoSize = True
        Me.OverrideNameLabel.Location = New System.Drawing.Point(20, 178)
        Me.OverrideNameLabel.Name = "OverrideNameLabel"
        Me.OverrideNameLabel.Size = New System.Drawing.Size(192, 13)
        Me.OverrideNameLabel.TabIndex = 6
        Me.OverrideNameLabel.Text = "External HostName For Region Servers"
        Me.ToolTip1.SetToolTip(Me.OverrideNameLabel, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(19, 129)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(98, 13)
        Me.Label26.TabIndex = 4
        Me.Label26.Text = "Region Port Start #"
        '
        'ExternalHostName
        '
        Me.ExternalHostName.Location = New System.Drawing.Point(23, 194)
        Me.ExternalHostName.Name = "ExternalHostName"
        Me.ExternalHostName.Size = New System.Drawing.Size(219, 20)
        Me.ExternalHostName.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.ExternalHostName, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'MaxP
        '
        Me.MaxP.AutoSize = True
        Me.MaxP.Location = New System.Drawing.Point(32, 153)
        Me.MaxP.Name = "MaxP"
        Me.MaxP.Size = New System.Drawing.Size(72, 13)
        Me.MaxP.TabIndex = 5
        Me.MaxP.Text = "Highest used:"
        '
        'Upnp
        '
        Me.Upnp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Upnp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.Upnp.Location = New System.Drawing.Point(183, 17)
        Me.Upnp.MinimumSize = New System.Drawing.Size(16, 16)
        Me.Upnp.Name = "Upnp"
        Me.Upnp.Size = New System.Drawing.Size(17, 17)
        Me.Upnp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Upnp.TabIndex = 1859
        Me.Upnp.TabStop = False
        '
        'uPnPEnabled
        '
        Me.uPnPEnabled.AutoSize = True
        Me.uPnPEnabled.Location = New System.Drawing.Point(23, 23)
        Me.uPnPEnabled.Name = "uPnPEnabled"
        Me.uPnPEnabled.Size = New System.Drawing.Size(96, 17)
        Me.uPnPEnabled.TabIndex = 0
        Me.uPnPEnabled.Text = Global.Outworldz.My.Resources.Resources.UPnP_Enabled_word
        Me.ToolTip1.SetToolTip(Me.uPnPEnabled, Global.Outworldz.My.Resources.Resources.UPnP_Enabled_text)
        Me.uPnPEnabled.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 103)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Private Port"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Diagnostics Port"
        '
        'HTTPPort
        '
        Me.HTTPPort.Location = New System.Drawing.Point(165, 73)
        Me.HTTPPort.Name = "HTTPPort"
        Me.HTTPPort.Size = New System.Drawing.Size(47, 20)
        Me.HTTPPort.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.HTTPPort, Global.Outworldz.My.Resources.Resources.Default_8002_word)
        '
        'PrivatePort
        '
        Me.PrivatePort.Location = New System.Drawing.Point(165, 99)
        Me.PrivatePort.Name = "PrivatePort"
        Me.PrivatePort.Size = New System.Drawing.Size(47, 20)
        Me.PrivatePort.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.PrivatePort, Global.Outworldz.My.Resources.Resources.Default_8003_word)
        '
        'DiagnosticPort
        '
        Me.DiagnosticPort.Location = New System.Drawing.Point(165, 47)
        Me.DiagnosticPort.Name = "DiagnosticPort"
        Me.DiagnosticPort.Size = New System.Drawing.Size(47, 20)
        Me.DiagnosticPort.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.DiagnosticPort, Global.Outworldz.My.Resources.Resources.Default_8001_word)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Http Port"
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(295, 30)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(68, 28)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormPorts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(295, 318)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBoxA)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormPorts"
        Me.Text = "Region Ports"
        Me.GroupBoxA.ResumeLayout(False)
        Me.GroupBoxA.PerformLayout()
        CType(Me.Upnp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
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
    Friend WithEvents Label1 As Label
    Friend WithEvents intIP As TextBox
End Class
