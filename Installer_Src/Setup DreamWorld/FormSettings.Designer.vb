<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdvancedForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdvancedForm))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TOSButton = New System.Windows.Forms.Button()
        Me.TideButton = New System.Windows.Forms.Button()
        Me.GloebitsButton = New System.Windows.Forms.Button()
        Me.VoiceButton1 = New System.Windows.Forms.Button()
        Me.Shoutcast = New System.Windows.Forms.Button()
        Me.MapsButton = New System.Windows.Forms.Button()
        Me.Birds = New System.Windows.Forms.Button()
        Me.BackupButton1 = New System.Windows.Forms.Button()
        Me.RegionsButton1 = New System.Windows.Forms.Button()
        Me.DivaButton1 = New System.Windows.Forms.Button()
        Me.PortsButton1 = New System.Windows.Forms.Button()
        Me.PhysicsButton1 = New System.Windows.Forms.Button()
        Me.DatabaseButton2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DNSButton = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.CacheButton1 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ApacheButton = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'TOSButton
        '
        Me.TOSButton.Location = New System.Drawing.Point(167, 221)
        Me.TOSButton.Name = "TOSButton"
        Me.TOSButton.Size = New System.Drawing.Size(145, 23)
        Me.TOSButton.TabIndex = 15
        Me.TOSButton.Text = My.Resources.Terms_of_Service
        Me.ToolTip1.SetToolTip(Me.TOSButton, My.Resources.Setup_TOS)
        Me.TOSButton.UseVisualStyleBackColor = True
        '
        'TideButton
        '
        Me.TideButton.Location = New System.Drawing.Point(167, 192)
        Me.TideButton.Name = "TideButton"
        Me.TideButton.Size = New System.Drawing.Size(145, 23)
        Me.TideButton.TabIndex = 14
        Me.TideButton.Text = My.Resources.Tides
        Me.ToolTip1.SetToolTip(Me.TideButton, My.Resources.Click_Tides)
        Me.TideButton.UseVisualStyleBackColor = True
        '
        'GloebitsButton
        '
        Me.GloebitsButton.Location = New System.Drawing.Point(15, 163)
        Me.GloebitsButton.Name = "GloebitsButton"
        Me.GloebitsButton.Size = New System.Drawing.Size(143, 23)
        Me.GloebitsButton.TabIndex = 4
        Me.GloebitsButton.Text = My.Resources.Gloebits_Currency
        Me.ToolTip1.SetToolTip(Me.GloebitsButton, My.Resources.Click_Currency)
        Me.GloebitsButton.UseVisualStyleBackColor = True
        '
        'VoiceButton1
        '
        Me.VoiceButton1.Location = New System.Drawing.Point(167, 250)
        Me.VoiceButton1.Name = "VoiceButton1"
        Me.VoiceButton1.Size = New System.Drawing.Size(143, 23)
        Me.VoiceButton1.TabIndex = 16
        Me.VoiceButton1.Text = My.Resources.Vivox_Voice
        Me.ToolTip1.SetToolTip(Me.VoiceButton1, My.Resources.Click_Voice)
        Me.VoiceButton1.UseVisualStyleBackColor = True
        '
        'Shoutcast
        '
        Me.Shoutcast.Location = New System.Drawing.Point(15, 221)
        Me.Shoutcast.Name = "Shoutcast"
        Me.Shoutcast.Size = New System.Drawing.Size(143, 23)
        Me.Shoutcast.TabIndex = 6
        Me.Shoutcast.Text = My.Resources.Icecast_Shoutcast
        Me.ToolTip1.SetToolTip(Me.Shoutcast, My.Resources.Click_Icecast)
        Me.Shoutcast.UseVisualStyleBackColor = True
        '
        'MapsButton
        '
        Me.MapsButton.Location = New System.Drawing.Point(16, 251)
        Me.MapsButton.Name = "MapsButton"
        Me.MapsButton.Size = New System.Drawing.Size(143, 23)
        Me.MapsButton.TabIndex = 7
        Me.MapsButton.Text = My.Resources.Maps
        Me.ToolTip1.SetToolTip(Me.MapsButton, My.Resources.Click_Maps)
        Me.MapsButton.UseVisualStyleBackColor = True
        '
        'Birds
        '
        Me.Birds.Location = New System.Drawing.Point(15, 77)
        Me.Birds.Name = "Birds"
        Me.Birds.Size = New System.Drawing.Size(145, 23)
        Me.Birds.TabIndex = 2
        Me.Birds.Text = My.Resources.Bird_Settings
        Me.ToolTip1.SetToolTip(Me.Birds, My.Resources.Click_Birds)
        Me.Birds.UseVisualStyleBackColor = True
        '
        'BackupButton1
        '
        Me.BackupButton1.Location = New System.Drawing.Point(15, 48)
        Me.BackupButton1.Name = "BackupButton1"
        Me.BackupButton1.Size = New System.Drawing.Size(145, 23)
        Me.BackupButton1.TabIndex = 1
        Me.BackupButton1.Text = My.Resources.Backup_Settings
        Me.ToolTip1.SetToolTip(Me.BackupButton1, My.Resources.Backup_Schedule)
        Me.BackupButton1.UseVisualStyleBackColor = True
        '
        'RegionsButton1
        '
        Me.RegionsButton1.Location = New System.Drawing.Point(166, 105)
        Me.RegionsButton1.Name = "RegionsButton1"
        Me.RegionsButton1.Size = New System.Drawing.Size(145, 23)
        Me.RegionsButton1.TabIndex = 12
        Me.RegionsButton1.Text = My.Resources.Regions
        Me.ToolTip1.SetToolTip(Me.RegionsButton1, My.Resources.Click_Regions)
        Me.RegionsButton1.UseVisualStyleBackColor = True
        '
        'DivaButton1
        '
        Me.DivaButton1.Location = New System.Drawing.Point(167, 279)
        Me.DivaButton1.Name = "DivaButton1"
        Me.DivaButton1.Size = New System.Drawing.Size(145, 23)
        Me.DivaButton1.TabIndex = 18
        Me.DivaButton1.Text = My.Resources.Web
        Me.ToolTip1.SetToolTip(Me.DivaButton1, My.Resources.Click_Web)
        Me.DivaButton1.UseVisualStyleBackColor = True
        '
        'PortsButton1
        '
        Me.PortsButton1.Location = New System.Drawing.Point(16, 278)
        Me.PortsButton1.Name = "PortsButton1"
        Me.PortsButton1.Size = New System.Drawing.Size(145, 23)
        Me.PortsButton1.TabIndex = 8
        Me.PortsButton1.Text = My.Resources.Network_Ports
        Me.ToolTip1.SetToolTip(Me.PortsButton1, My.Resources.Click_Ports)
        Me.PortsButton1.UseVisualStyleBackColor = True
        '
        'PhysicsButton1
        '
        Me.PhysicsButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PhysicsButton1.Location = New System.Drawing.Point(166, 48)
        Me.PhysicsButton1.Name = "PhysicsButton1"
        Me.PhysicsButton1.Size = New System.Drawing.Size(145, 23)
        Me.PhysicsButton1.TabIndex = 10
        Me.PhysicsButton1.Text = My.Resources.Physics
        Me.ToolTip1.SetToolTip(Me.PhysicsButton1, My.Resources.Click_Physics)
        Me.PhysicsButton1.UseVisualStyleBackColor = True
        '
        'DatabaseButton2
        '
        Me.DatabaseButton2.Location = New System.Drawing.Point(15, 134)
        Me.DatabaseButton2.Name = "DatabaseButton2"
        Me.DatabaseButton2.Size = New System.Drawing.Size(145, 23)
        Me.DatabaseButton2.TabIndex = 3
        Me.DatabaseButton2.Text = My.Resources.Database_Setup
        Me.ToolTip1.SetToolTip(Me.DatabaseButton2, My.Resources.Click_Database)
        Me.DatabaseButton2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(167, 134)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(143, 23)
        Me.Button3.TabIndex = 13
        Me.Button3.Text = My.Resources.Restart_Settings
        Me.ToolTip1.SetToolTip(Me.Button3, My.Resources.Click_Restart)
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(166, 19)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(143, 23)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Permissions"
        Me.ToolTip1.SetToolTip(Me.Button2, "Click to set up God mode and other permissions")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'DNSButton
        '
        Me.DNSButton.Location = New System.Drawing.Point(15, 192)
        Me.DNSButton.Name = "DNSButton"
        Me.DNSButton.Size = New System.Drawing.Size(143, 23)
        Me.DNSButton.TabIndex = 5
        Me.DNSButton.Text = My.Resources.Hypergrid
        Me.ToolTip1.SetToolTip(Me.DNSButton, My.Resources.Click_HG)
        Me.DNSButton.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(166, 76)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(145, 23)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = My.Resources.Publicity
        Me.ToolTip1.SetToolTip(Me.Button4, My.Resources.Click_Publicity)
        Me.Button4.UseVisualStyleBackColor = True
        '
        'CacheButton1
        '
        Me.CacheButton1.Location = New System.Drawing.Point(15, 105)
        Me.CacheButton1.Name = "CacheButton1"
        Me.CacheButton1.Size = New System.Drawing.Size(145, 23)
        Me.CacheButton1.TabIndex = 19
        Me.CacheButton1.Text = My.Resources.Caches
        Me.ToolTip1.SetToolTip(Me.CacheButton1, My.Resources.Click_Caches)
        Me.CacheButton1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(167, 163)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(145, 23)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = My.Resources.Server_Type
        Me.ToolTip1.SetToolTip(Me.Button1, My.Resources.Click_Server)
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ApacheButton
        '
        Me.ApacheButton.Location = New System.Drawing.Point(16, 18)
        Me.ApacheButton.Name = "ApacheButton"
        Me.ApacheButton.Size = New System.Drawing.Size(145, 23)
        Me.ApacheButton.TabIndex = 21
        Me.ApacheButton.Text = My.Resources.Apache_Webserver
        Me.ToolTip1.SetToolTip(Me.ApacheButton, My.Resources.ApacheWebServer)
        Me.ApacheButton.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.ApacheButton)
        Me.GroupBox8.Controls.Add(Me.Button1)
        Me.GroupBox8.Controls.Add(Me.CacheButton1)
        Me.GroupBox8.Controls.Add(Me.Button4)
        Me.GroupBox8.Controls.Add(Me.Button3)
        Me.GroupBox8.Controls.Add(Me.PhysicsButton1)
        Me.GroupBox8.Controls.Add(Me.VoiceButton1)
        Me.GroupBox8.Controls.Add(Me.Button2)
        Me.GroupBox8.Controls.Add(Me.DNSButton)
        Me.GroupBox8.Controls.Add(Me.DatabaseButton2)
        Me.GroupBox8.Controls.Add(Me.PortsButton1)
        Me.GroupBox8.Controls.Add(Me.DivaButton1)
        Me.GroupBox8.Controls.Add(Me.RegionsButton1)
        Me.GroupBox8.Controls.Add(Me.BackupButton1)
        Me.GroupBox8.Controls.Add(Me.MapsButton)
        Me.GroupBox8.Controls.Add(Me.TideButton)
        Me.GroupBox8.Controls.Add(Me.Birds)
        Me.GroupBox8.Controls.Add(Me.GloebitsButton)
        Me.GroupBox8.Controls.Add(Me.TOSButton)
        Me.GroupBox8.Controls.Add(Me.Shoutcast)
        Me.GroupBox8.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(332, 324)
        Me.GroupBox8.TabIndex = 1870
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Global Settings"
        '
        'AdvancedForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(359, 348)
        Me.Controls.Add(Me.GroupBox8)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "AdvancedForm"
        Me.Text = My.Resources.Common_Settings
        Me.GroupBox8.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GloebitsButton As Button
    Friend WithEvents VoiceButton1 As Button
    Friend WithEvents Shoutcast As Button
    Friend WithEvents TOSButton As Button
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents TideButton As Button
    Friend WithEvents MapsButton As Button
    Friend WithEvents BackupButton1 As Button
    Friend WithEvents Birds As Button
    Friend WithEvents RegionsButton1 As Button
    Friend WithEvents DivaButton1 As Button
    Friend WithEvents PortsButton1 As Button
    Friend WithEvents PhysicsButton1 As Button
    Friend WithEvents DatabaseButton2 As Button
    Friend WithEvents DNSButton As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents CacheButton1 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ApacheButton As Button
End Class
