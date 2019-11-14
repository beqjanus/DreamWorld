<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormDatabase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDatabase))
        Me.StandaloneGroup = New System.Windows.Forms.GroupBox()
        Me.ClearRegionTable = New System.Windows.Forms.Button()
        Me.MysqlRegionPort = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RegionServer = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.RegionDbName = New System.Windows.Forms.TextBox()
        Me.RegionDBUsername = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.RegionMySqlPassword = New System.Windows.Forms.TextBox()
        Me.GridGroup = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.RobustServer = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Dbnameindex = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RobustDbPort = New System.Windows.Forms.TextBox()
        Me.RobustDbName = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.RobustDBPassword = New System.Windows.Forms.TextBox()
        Me.RobustDBUsername = New System.Windows.Forms.TextBox()
        Me.DBHelp = New System.Windows.Forms.PictureBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FullSQLBackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataOnlyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.StandaloneGroup.SuspendLayout()
        Me.GridGroup.SuspendLayout()
        CType(Me.DBHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'StandaloneGroup
        '
        Me.StandaloneGroup.Controls.Add(Me.ClearRegionTable)
        Me.StandaloneGroup.Controls.Add(Me.MysqlRegionPort)
        Me.StandaloneGroup.Controls.Add(Me.Label2)
        Me.StandaloneGroup.Controls.Add(Me.Label1)
        Me.StandaloneGroup.Controls.Add(Me.RegionServer)
        Me.StandaloneGroup.Controls.Add(Me.Label22)
        Me.StandaloneGroup.Controls.Add(Me.Label20)
        Me.StandaloneGroup.Controls.Add(Me.RegionDbName)
        Me.StandaloneGroup.Controls.Add(Me.RegionDBUsername)
        Me.StandaloneGroup.Controls.Add(Me.Label21)
        Me.StandaloneGroup.Controls.Add(Me.RegionMySqlPassword)
        Me.StandaloneGroup.Location = New System.Drawing.Point(518, 56)
        Me.StandaloneGroup.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.StandaloneGroup.Name = "StandaloneGroup"
        Me.StandaloneGroup.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.StandaloneGroup.Size = New System.Drawing.Size(416, 315)
        Me.StandaloneGroup.TabIndex = 56
        Me.StandaloneGroup.TabStop = False
        Me.StandaloneGroup.Text = Global.Outworldz.My.Resources.Resources.Region_Database
        '
        'ClearRegionTable
        '
        Me.ClearRegionTable.Location = New System.Drawing.Point(25, 248)
        Me.ClearRegionTable.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ClearRegionTable.Name = "ClearRegionTable"
        Me.ClearRegionTable.Size = New System.Drawing.Size(160, 34)
        Me.ClearRegionTable.TabIndex = 1886
        Me.ClearRegionTable.Text = Global.Outworldz.My.Resources.Resources.ClearRegion
        Me.ClearRegionTable.UseVisualStyleBackColor = True
        '
        'MysqlRegionPort
        '
        Me.MysqlRegionPort.Location = New System.Drawing.Point(27, 199)
        Me.MysqlRegionPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MysqlRegionPort.Name = "MysqlRegionPort"
        Me.MysqlRegionPort.Size = New System.Drawing.Size(68, 26)
        Me.MysqlRegionPort.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.MysqlRegionPort, "3306")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(202, 202)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 20)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = Global.Outworldz.My.Resources.Resources.MySqlPort
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(199, 42)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 20)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = Global.Outworldz.My.Resources.Resources.Region_Server_word
        '
        'RegionServer
        '
        Me.RegionServer.Location = New System.Drawing.Point(27, 37)
        Me.RegionServer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RegionServer.Name = "RegionServer"
        Me.RegionServer.Size = New System.Drawing.Size(158, 26)
        Me.RegionServer.TabIndex = 46
        Me.ToolTip1.SetToolTip(Me.RegionServer, Global.Outworldz.My.Resources.Resources.Region_ServerName)
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(201, 162)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(78, 20)
        Me.Label22.TabIndex = 17
        Me.Label22.Text = Global.Outworldz.My.Resources.Resources.Password
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(199, 82)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(125, 20)
        Me.Label20.TabIndex = 13
        Me.Label20.Text = Global.Outworldz.My.Resources.Resources.Name
        '
        'RegionDbName
        '
        Me.RegionDbName.Location = New System.Drawing.Point(27, 78)
        Me.RegionDbName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RegionDbName.Name = "RegionDbName"
        Me.RegionDbName.Size = New System.Drawing.Size(158, 26)
        Me.RegionDbName.TabIndex = 42
        Me.ToolTip1.SetToolTip(Me.RegionDbName, Global.Outworldz.My.Resources.Resources.Do_NotChange)
        '
        'RegionDBUsername
        '
        Me.RegionDBUsername.Location = New System.Drawing.Point(27, 116)
        Me.RegionDBUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RegionDBUsername.Name = "RegionDBUsername"
        Me.RegionDBUsername.Size = New System.Drawing.Size(158, 26)
        Me.RegionDBUsername.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.RegionDBUsername, Global.Outworldz.My.Resources.Resources.Do_NotChange)
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(201, 121)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(83, 20)
        Me.Label21.TabIndex = 16
        Me.Label21.Text = Global.Outworldz.My.Resources.Resources.User_Name_word
        '
        'RegionMySqlPassword
        '
        Me.RegionMySqlPassword.Location = New System.Drawing.Point(27, 160)
        Me.RegionMySqlPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RegionMySqlPassword.Name = "RegionMySqlPassword"
        Me.RegionMySqlPassword.Size = New System.Drawing.Size(158, 26)
        Me.RegionMySqlPassword.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.RegionMySqlPassword, Global.Outworldz.My.Resources.Resources.Do_NotChange)
        Me.RegionMySqlPassword.UseSystemPasswordChar = True
        '
        'GridGroup
        '
        Me.GridGroup.Controls.Add(Me.Label3)
        Me.GridGroup.Controls.Add(Me.Button1)
        Me.GridGroup.Controls.Add(Me.RobustServer)
        Me.GridGroup.Controls.Add(Me.DBHelp)
        Me.GridGroup.Controls.Add(Me.Label16)
        Me.GridGroup.Controls.Add(Me.Dbnameindex)
        Me.GridGroup.Controls.Add(Me.Label9)
        Me.GridGroup.Controls.Add(Me.RobustDbPort)
        Me.GridGroup.Controls.Add(Me.RobustDbName)
        Me.GridGroup.Controls.Add(Me.Label15)
        Me.GridGroup.Controls.Add(Me.Label8)
        Me.GridGroup.Controls.Add(Me.RobustDBPassword)
        Me.GridGroup.Controls.Add(Me.RobustDBUsername)
        Me.GridGroup.Location = New System.Drawing.Point(38, 56)
        Me.GridGroup.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridGroup.Name = "GridGroup"
        Me.GridGroup.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridGroup.Size = New System.Drawing.Size(450, 315)
        Me.GridGroup.TabIndex = 55
        Me.GridGroup.TabStop = False
        Me.GridGroup.Text = "Robust"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 255)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 20)
        Me.Label3.TabIndex = 1885
        Me.Label3.Text = Global.Outworldz.My.Resources.Resources.FSA
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(147, 248)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(160, 34)
        Me.Button1.TabIndex = 1884
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.FSassets_Server
        Me.Button1.UseVisualStyleBackColor = True
        '
        'RobustServer
        '
        Me.RobustServer.Location = New System.Drawing.Point(41, 52)
        Me.RobustServer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RobustServer.Name = "RobustServer"
        Me.RobustServer.Size = New System.Drawing.Size(158, 26)
        Me.RobustServer.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.RobustServer, Global.Outworldz.My.Resources.Resources.Region_ServerName)
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(222, 53)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(61, 20)
        Me.Label16.TabIndex = 38
        Me.Label16.Text = "Robust"
        '
        'Dbnameindex
        '
        Me.Dbnameindex.AutoSize = True
        Me.Dbnameindex.Location = New System.Drawing.Point(222, 89)
        Me.Dbnameindex.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Dbnameindex.Name = "Dbnameindex"
        Me.Dbnameindex.Size = New System.Drawing.Size(51, 20)
        Me.Dbnameindex.TabIndex = 35
        Me.Dbnameindex.Text = GetType(Outworldz.My.Resources.Resources).Name
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(218, 167)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 20)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = Global.Outworldz.My.Resources.Resources.Password
        '
        'RobustDbPort
        '
        Me.RobustDbPort.Location = New System.Drawing.Point(41, 204)
        Me.RobustDbPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RobustDbPort.Name = "RobustDbPort"
        Me.RobustDbPort.Size = New System.Drawing.Size(68, 26)
        Me.RobustDbPort.TabIndex = 41
        Me.ToolTip1.SetToolTip(Me.RobustDbPort, Global.Outworldz.My.Resources.Resources.MySQL_Port_Default)
        '
        'RobustDbName
        '
        Me.RobustDbName.Location = New System.Drawing.Point(41, 90)
        Me.RobustDbName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RobustDbName.Name = "RobustDbName"
        Me.RobustDbName.Size = New System.Drawing.Size(158, 26)
        Me.RobustDbName.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.RobustDbName, Global.Outworldz.My.Resources.Resources.Do_NotChange)
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(218, 129)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(89, 20)
        Me.Label15.TabIndex = 34
        Me.Label15.Text = Global.Outworldz.My.Resources.Resources.User_Name_word
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(222, 207)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 20)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = Global.Outworldz.My.Resources.Resources.MySqlPort
        '
        'RobustDBPassword
        '
        Me.RobustDBPassword.Location = New System.Drawing.Point(41, 168)
        Me.RobustDBPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RobustDBPassword.Name = "RobustDBPassword"
        Me.RobustDBPassword.Size = New System.Drawing.Size(158, 26)
        Me.RobustDBPassword.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.RobustDBPassword, Global.Outworldz.My.Resources.Resources.Do_NotChange)
        Me.RobustDBPassword.UseSystemPasswordChar = True
        '
        'RobustDBUsername
        '
        Me.RobustDBUsername.Location = New System.Drawing.Point(41, 130)
        Me.RobustDBUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RobustDBUsername.Name = "RobustDBUsername"
        Me.RobustDBUsername.Size = New System.Drawing.Size(158, 26)
        Me.RobustDBUsername.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.RobustDBUsername, Global.Outworldz.My.Resources.Resources.Do_NotChange)
        '
        'DBHelp
        '
        Me.DBHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DBHelp.Location = New System.Drawing.Point(274, 18)
        Me.DBHelp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DBHelp.Name = "DBHelp"
        Me.DBHelp.Size = New System.Drawing.Size(42, 31)
        Me.DBHelp.TabIndex = 1859
        Me.DBHelp.TabStop = False
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30, Me.BackupToolStripMenuItem})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(949, 35)
        Me.MenuStrip2.TabIndex = 1885
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
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(239, 34)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Database_Setup
        '
        'BackupToolStripMenuItem
        '
        Me.BackupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FullSQLBackupToolStripMenuItem, Me.DataOnlyToolStripMenuItem})
        Me.BackupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.BackupToolStripMenuItem.Name = "BackupToolStripMenuItem"
        Me.BackupToolStripMenuItem.Size = New System.Drawing.Size(105, 29)
        Me.BackupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Backup_word
        '
        'FullSQLBackupToolStripMenuItem
        '
        Me.FullSQLBackupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.FullSQLBackupToolStripMenuItem.Name = "FullSQLBackupToolStripMenuItem"
        Me.FullSQLBackupToolStripMenuItem.Size = New System.Drawing.Size(256, 34)
        Me.FullSQLBackupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Backup_Data
        '
        'DataOnlyToolStripMenuItem
        '
        Me.DataOnlyToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_yellow
        Me.DataOnlyToolStripMenuItem.Name = "DataOnlyToolStripMenuItem"
        Me.DataOnlyToolStripMenuItem.Size = New System.Drawing.Size(256, 34)
        Me.DataOnlyToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.export
        '
        'FormDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(949, 411)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.StandaloneGroup)
        Me.Controls.Add(Me.GridGroup)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "FormDatabase"
        Me.Text = Global.Outworldz.My.Resources.Resources.Database
        Me.StandaloneGroup.ResumeLayout(False)
        Me.StandaloneGroup.PerformLayout()
        Me.GridGroup.ResumeLayout(False)
        Me.GridGroup.PerformLayout()
        CType(Me.DBHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StandaloneGroup As GroupBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents RegionDbName As TextBox
    Friend WithEvents RegionDBUsername As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents RegionMySqlPassword As TextBox
    Friend WithEvents GridGroup As GroupBox
    Friend WithEvents RobustServer As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Dbnameindex As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents RobustDbPort As TextBox
    Friend WithEvents RobustDbName As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents RobustDBPassword As TextBox
    Friend WithEvents RobustDBUsername As TextBox
    Friend WithEvents DBHelp As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents RegionServer As TextBox
    Friend WithEvents MysqlRegionPort As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FullSQLBackupToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataOnlyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearRegionTable As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
