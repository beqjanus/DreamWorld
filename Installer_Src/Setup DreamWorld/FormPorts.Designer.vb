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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPorts))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.GroupBox2.SuspendLayout()
        CType(Me.Upnp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
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
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(216, 233)
        Me.GroupBox2.TabIndex = 45
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Ports"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 182)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 13)
        Me.Label1.TabIndex = 1864
        Me.Label1.Text = "External HostName For Region Servers"
        Me.ToolTip1.SetToolTip(Me.Label1, "The default for External Host Name is a blank, which becomes your DNS name or IP." &
        "  Only set this if you are running a region server with no NAT.")
        '
        'ExternalHostName
        '
        Me.ExternalHostName.Location = New System.Drawing.Point(23, 207)
        Me.ExternalHostName.Name = "ExternalHostName"
        Me.ExternalHostName.Size = New System.Drawing.Size(187, 20)
        Me.ExternalHostName.TabIndex = 1863
        Me.ToolTip1.SetToolTip(Me.ExternalHostName, "The default for External Host Name is a blank, which becomes your DNS name or IP." &
        "  Only set this if you are running a region server with no NAT.")
        '
        'MaxP
        '
        Me.MaxP.AutoSize = True
        Me.MaxP.Location = New System.Drawing.Point(19, 153)
        Me.MaxP.Name = "MaxP"
        Me.MaxP.Size = New System.Drawing.Size(99, 13)
        Me.MaxP.TabIndex = 1862
        Me.MaxP.Text = "Highest used: 8004"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(18, 130)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(98, 13)
        Me.Label26.TabIndex = 1861
        Me.Label26.Text = "Region Port Start #"
        '
        'FirstRegionPort
        '
        Me.FirstRegionPort.Location = New System.Drawing.Point(138, 121)
        Me.FirstRegionPort.Name = "FirstRegionPort"
        Me.FirstRegionPort.Size = New System.Drawing.Size(47, 20)
        Me.FirstRegionPort.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.FirstRegionPort, "Default 8004")
        '
        'Upnp
        '
        Me.Upnp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.Upnp.Location = New System.Drawing.Point(155, 15)
        Me.Upnp.Name = "Upnp"
        Me.Upnp.Size = New System.Drawing.Size(30, 29)
        Me.Upnp.TabIndex = 1859
        Me.Upnp.TabStop = False
        '
        'uPnPEnabled
        '
        Me.uPnPEnabled.AutoSize = True
        Me.uPnPEnabled.Location = New System.Drawing.Point(22, 23)
        Me.uPnPEnabled.Name = "uPnPEnabled"
        Me.uPnPEnabled.Size = New System.Drawing.Size(96, 17)
        Me.uPnPEnabled.TabIndex = 21
        Me.uPnPEnabled.Text = "UPnP Enabled"
        Me.ToolTip1.SetToolTip(Me.uPnPEnabled, "Enable this is you have a small stysrtem and wants the router ports to be automat" &
        "ically set")
        Me.uPnPEnabled.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 103)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Private Port"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Diagnostic Port"
        '
        'HTTPPort
        '
        Me.HTTPPort.Location = New System.Drawing.Point(138, 70)
        Me.HTTPPort.Name = "HTTPPort"
        Me.HTTPPort.Size = New System.Drawing.Size(47, 20)
        Me.HTTPPort.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.HTTPPort, "Default 8002")
        '
        'PrivatePort
        '
        Me.PrivatePort.Location = New System.Drawing.Point(138, 96)
        Me.PrivatePort.Name = "PrivatePort"
        Me.PrivatePort.Size = New System.Drawing.Size(47, 20)
        Me.PrivatePort.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.PrivatePort, "Default 8003")
        '
        'DiagnosticPort
        '
        Me.DiagnosticPort.Location = New System.Drawing.Point(138, 44)
        Me.DiagnosticPort.Name = "DiagnosticPort"
        Me.DiagnosticPort.Size = New System.Drawing.Size(47, 20)
        Me.DiagnosticPort.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.DiagnosticPort, "Default 8002")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Http Port"
        '
        'FormPorts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(240, 276)
        Me.Controls.Add(Me.GroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormPorts"
        Me.Text = "Region Ports"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.Upnp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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
    Friend WithEvents Label1 As Label
    Friend WithEvents ExternalHostName As TextBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
