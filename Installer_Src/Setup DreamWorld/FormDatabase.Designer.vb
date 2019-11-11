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
        Me.StandaloneGroup.Location = New System.Drawing.Point(284, 37)
        Me.StandaloneGroup.Name = "StandaloneGroup"
        Me.StandaloneGroup.Size = New System.Drawing.Size(222, 210)
        Me.StandaloneGroup.TabIndex = 56
        Me.StandaloneGroup.TabStop = False
        Me.StandaloneGroup.Text = My.Resources.Region_Database
        '
        'ClearRegionTable
        '
        Me.ClearRegionTable.Location = New System.Drawing.Point(94, 165)
        Me.ClearRegionTable.Name = "ClearRegionTable"
        Me.ClearRegionTable.Size = New System.Drawing.Size(107, 23)
        Me.ClearRegionTable.TabIndex = 1886
        Me.ClearRegionTable.Text = My.Resources.ClearRegion
        Me.ClearRegionTable.UseVisualStyleBackColor = True
        '
        'MysqlRegionPort
        '
        Me.MysqlRegionPort.Location = New System.Drawing.Point(94, 130)
        Me.MysqlRegionPort.Name = "MysqlRegionPort"
        Me.MysqlRegionPort.Size = New System.Drawing.Size(47, 20)
        Me.MysqlRegionPort.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.MysqlRegionPort, "3306")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = My.Resources.MySqlPort
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = My.Resources.RegionServer
        '
        'RegionServer
        '
        Me.RegionServer.Location = New System.Drawing.Point(94, 22)
        Me.RegionServer.Name = "RegionServer"
        Me.RegionServer.Size = New System.Drawing.Size(107, 20)
        Me.RegionServer.TabIndex = 46
        Me.ToolTip1.SetToolTip(Me.RegionServer, My.Resources.Region_ServerName)
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(17, 105)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(53, 13)
        Me.Label22.TabIndex = 17
        Me.Label22.Text = My.Resources.Password
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(16, 52)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(53, 13)
        Me.Label20.TabIndex = 13
        Me.Label20.Text = My.Resources.DBName
        '
        'RegionDbName
        '
        Me.RegionDbName.Location = New System.Drawing.Point(94, 49)
        Me.RegionDbName.Name = "RegionDbName"
        Me.RegionDbName.Size = New System.Drawing.Size(107, 20)
        Me.RegionDbName.TabIndex = 42
        Me.ToolTip1.SetToolTip(Me.RegionDbName, My.Resources.Do_NotChange)
        '
        'RegionDBUsername
        '
        Me.RegionDBUsername.Location = New System.Drawing.Point(94, 75)
        Me.RegionDBUsername.Name = "RegionDBUsername"
        Me.RegionDBUsername.Size = New System.Drawing.Size(107, 20)
        Me.RegionDBUsername.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.RegionDBUsername, My.Resources.Do_NotChange)
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(17, 78)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(55, 13)
        Me.Label21.TabIndex = 16
        Me.Label21.Text = "Username"
        '
        'RegionMySqlPassword
        '
        Me.RegionMySqlPassword.Location = New System.Drawing.Point(94, 104)
        Me.RegionMySqlPassword.Name = "RegionMySqlPassword"
        Me.RegionMySqlPassword.Size = New System.Drawing.Size(107, 20)
        Me.RegionMySqlPassword.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.RegionMySqlPassword, My.Resources.Do_NotChange)
        Me.RegionMySqlPassword.UseSystemPasswordChar = True
        '
        'GridGroup
        '
        Me.GridGroup.Controls.Add(Me.Label3)
        Me.GridGroup.Controls.Add(Me.Button1)
        Me.GridGroup.Controls.Add(Me.RobustServer)
        Me.GridGroup.Controls.Add(Me.Label16)
        Me.GridGroup.Controls.Add(Me.Dbnameindex)
        Me.GridGroup.Controls.Add(Me.Label9)
        Me.GridGroup.Controls.Add(Me.RobustDbPort)
        Me.GridGroup.Controls.Add(Me.RobustDbName)
        Me.GridGroup.Controls.Add(Me.Label15)
        Me.GridGroup.Controls.Add(Me.Label8)
        Me.GridGroup.Controls.Add(Me.RobustDBPassword)
        Me.GridGroup.Controls.Add(Me.RobustDBUsername)
        Me.GridGroup.Location = New System.Drawing.Point(25, 37)
        Me.GridGroup.Name = "GridGroup"
        Me.GridGroup.Size = New System.Drawing.Size(219, 210)
        Me.GridGroup.TabIndex = 55
        Me.GridGroup.TabStop = False
        Me.GridGroup.Text = "Robust"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 170)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 1885
        Me.Label3.Text = My.Resources.FSA
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(98, 165)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(107, 23)
        Me.Button1.TabIndex = 1884
        Me.Button1.Text = My.Resources.FSassets_Server
        Me.Button1.UseVisualStyleBackColor = True
        '
        'RobustServer
        '
        Me.RobustServer.Location = New System.Drawing.Point(98, 25)
        Me.RobustServer.Name = "RobustServer"
        Me.RobustServer.Size = New System.Drawing.Size(107, 20)
        Me.RobustServer.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.RobustServer, My.Resources.Region_ServerName)
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(17, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 13)
        Me.Label16.TabIndex = 38
        Me.Label16.Text = "Robust"
        '
        'Dbnameindex
        '
        Me.Dbnameindex.AutoSize = True
        Me.Dbnameindex.Location = New System.Drawing.Point(17, 51)
        Me.Dbnameindex.Name = "Dbnameindex"
        Me.Dbnameindex.Size = New System.Drawing.Size(72, 13)
        Me.Dbnameindex.TabIndex = 35
        Me.Dbnameindex.Text = My.Resources.Name
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 103)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 13)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = My.Resources.Password
        '
        'RobustDbPort
        '
        Me.RobustDbPort.Location = New System.Drawing.Point(98, 127)
        Me.RobustDbPort.Name = "RobustDbPort"
        Me.RobustDbPort.Size = New System.Drawing.Size(47, 20)
        Me.RobustDbPort.TabIndex = 41
        Me.ToolTip1.SetToolTip(Me.RobustDbPort, My.Resources.MySQL_Port_Default)
        '
        'RobustDbName
        '
        Me.RobustDbName.Location = New System.Drawing.Point(98, 51)
        Me.RobustDbName.Name = "RobustDbName"
        Me.RobustDbName.Size = New System.Drawing.Size(107, 20)
        Me.RobustDbName.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.RobustDbName, My.Resources.Do_NotChange)
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(15, 77)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 13)
        Me.Label15.TabIndex = 34
        Me.Label15.Text = My.Resources.UserName
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 13)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = My.Resources.MySqlPort
        '
        'RobustDBPassword
        '
        Me.RobustDBPassword.Location = New System.Drawing.Point(98, 103)
        Me.RobustDBPassword.Name = "RobustDBPassword"
        Me.RobustDBPassword.Size = New System.Drawing.Size(107, 20)
        Me.RobustDBPassword.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.RobustDBPassword, My.Resources.Do_NotChange)
        Me.RobustDBPassword.UseSystemPasswordChar = True
        '
        'RobustDBUsername
        '
        Me.RobustDBUsername.Location = New System.Drawing.Point(98, 77)
        Me.RobustDBUsername.Name = "RobustDBUsername"
        Me.RobustDBUsername.Size = New System.Drawing.Size(107, 20)
        Me.RobustDBUsername.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.RobustDBUsername, My.Resources.Do_NotChange)
        '
        'DBHelp
        '
        Me.DBHelp.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DBHelp.Location = New System.Drawing.Point(250, 37)
        Me.DBHelp.Name = "DBHelp"
        Me.DBHelp.Size = New System.Drawing.Size(28, 32)
        Me.DBHelp.TabIndex = 1859
        Me.DBHelp.TabStop = False
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30, Me.BackupToolStripMenuItem})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(532, 28)
        Me.MenuStrip2.TabIndex = 1885
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(64, 24)
        Me.ToolStripMenuItem30.Text = My.Resources.Help
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.DatabaseSetupToolStripMenuItem.Text = My.Resources.Database_Setup
        '
        'BackupToolStripMenuItem
        '
        Me.BackupToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FullSQLBackupToolStripMenuItem, Me.DataOnlyToolStripMenuItem})
        Me.BackupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.BackupToolStripMenuItem.Name = "BackupToolStripMenuItem"
        Me.BackupToolStripMenuItem.Size = New System.Drawing.Size(78, 24)
        Me.BackupToolStripMenuItem.Text = My.Resources.Backup
        '
        'FullSQLBackupToolStripMenuItem
        '
        Me.FullSQLBackupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.FullSQLBackupToolStripMenuItem.Name = "FullSQLBackupToolStripMenuItem"
        Me.FullSQLBackupToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.FullSQLBackupToolStripMenuItem.Text = My.Resources.Backup_Data
        '
        'DataOnlyToolStripMenuItem
        '
        Me.DataOnlyToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_yellow
        Me.DataOnlyToolStripMenuItem.Name = "DataOnlyToolStripMenuItem"
        Me.DataOnlyToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.DataOnlyToolStripMenuItem.Text = My.Resources.export
        '
        'FormDatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(532, 274)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.StandaloneGroup)
        Me.Controls.Add(Me.GridGroup)
        Me.Controls.Add(Me.DBHelp)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormDatabase"
        Me.Text = My.Resources.Database
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
