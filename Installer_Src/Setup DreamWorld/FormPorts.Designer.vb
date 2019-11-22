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
        Me.GroupBox2.Location = New System.Drawing.Point(18, 57)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(405, 350)
        Me.GroupBox2.TabIndex = 45
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = My.Resources.Ports
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 273)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(288, 20)
        Me.Label1.TabIndex = 1864
        Me.Label1.Text = My.Resources.External
        Me.ToolTip1.SetToolTip(Me.Label1, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'ExternalHostName
        '
        Me.ExternalHostName.Location = New System.Drawing.Point(34, 310)
        Me.ExternalHostName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ExternalHostName.Name = "ExternalHostName"
        Me.ExternalHostName.Size = New System.Drawing.Size(325, 26)
        Me.ExternalHostName.TabIndex = 1863
        Me.ToolTip1.SetToolTip(Me.ExternalHostName, Global.Outworldz.My.Resources.Resources.External_text)
        '
        'MaxP
        '
        Me.MaxP.AutoSize = True
        Me.MaxP.Location = New System.Drawing.Point(28, 230)
        Me.MaxP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.MaxP.Name = "MaxP"
        Me.MaxP.Size = New System.Drawing.Size(147, 20)
        Me.MaxP.TabIndex = 1862
        Me.MaxP.Text = my.Resources.Highest_Used
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(27, 195)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(145, 20)
        Me.Label26.TabIndex = 1861
        Me.Label26.Text = My.Resources.Region_Port_Start
        '
        'FirstRegionPort
        '
        Me.FirstRegionPort.Location = New System.Drawing.Point(248, 187)
        Me.FirstRegionPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.FirstRegionPort.Name = "FirstRegionPort"
        Me.FirstRegionPort.Size = New System.Drawing.Size(68, 26)
        Me.FirstRegionPort.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.FirstRegionPort, Global.Outworldz.My.Resources.Resources.Default_8004_word)
        '
        'Upnp
        '
        Me.Upnp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.Upnp.Location = New System.Drawing.Point(273, 27)
        Me.Upnp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Upnp.Name = "Upnp"
        Me.Upnp.Size = New System.Drawing.Size(45, 44)
        Me.Upnp.TabIndex = 1859
        Me.Upnp.TabStop = False
        '
        'uPnPEnabled
        '
        Me.uPnPEnabled.AutoSize = True
        Me.uPnPEnabled.Location = New System.Drawing.Point(33, 34)
        Me.uPnPEnabled.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uPnPEnabled.Name = "uPnPEnabled"
        Me.uPnPEnabled.Size = New System.Drawing.Size(139, 24)
        Me.uPnPEnabled.TabIndex = 21
        Me.uPnPEnabled.Text = Global.Outworldz.My.Resources.Resources.UPnP_Enabled_word
        Me.ToolTip1.SetToolTip(Me.uPnPEnabled, Global.Outworldz.My.Resources.Resources.UPnP_Enabled_text)
        Me.uPnPEnabled.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 154)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 20)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = My.Resources.Private_Port_Word
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 76)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 20)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = My.Resources.Diag_Port_word
        '
        'HTTPPort
        '
        Me.HTTPPort.Location = New System.Drawing.Point(248, 110)
        Me.HTTPPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.HTTPPort.Name = "HTTPPort"
        Me.HTTPPort.Size = New System.Drawing.Size(68, 26)
        Me.HTTPPort.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.HTTPPort, Global.Outworldz.My.Resources.Resources.Default_8002_word)
        '
        'PrivatePort
        '
        Me.PrivatePort.Location = New System.Drawing.Point(248, 149)
        Me.PrivatePort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PrivatePort.Name = "PrivatePort"
        Me.PrivatePort.Size = New System.Drawing.Size(68, 26)
        Me.PrivatePort.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.PrivatePort, Global.Outworldz.My.Resources.Resources.Default_8003_word)
        '
        'DiagnosticPort
        '
        Me.DiagnosticPort.Location = New System.Drawing.Point(248, 71)
        Me.DiagnosticPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DiagnosticPort.Name = "DiagnosticPort"
        Me.DiagnosticPort.Size = New System.Drawing.Size(68, 26)
        Me.DiagnosticPort.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.DiagnosticPort, Global.Outworldz.My.Resources.Resources.Default_8001_word)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 114)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 20)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = My.Resources.Http_Port
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(464, 35)
        Me.MenuStrip2.TabIndex = 1890
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(85, 29)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(151, 34)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'FormPorts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(464, 424)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FormPorts"
        Me.Text = My.Resources.Region_Port_word
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
    Friend WithEvents Label1 As Label
    Friend WithEvents ExternalHostName As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
