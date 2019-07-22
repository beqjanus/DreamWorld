<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            If disposing AndAlso PropMyUPnpMap IsNot Nothing Then
                PropMyUPnpMap.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.StopButton = New System.Windows.Forms.Button()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.InstallButton = New System.Windows.Forms.Button()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JustQuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsoleToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHide = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShow = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdvancedSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowHyperGridAddressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpStartingUpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpOnSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PDFManualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoopBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpOnIARSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpOnOARsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TroubleshootingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TechnicalInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsoleCOmmandsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommonConsoleCommandsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowUserDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendAlertToAllUsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllUsersAllSimsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JustOneRegionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.All = New System.Windows.Forms.ToolStripMenuItem()
        Me.Debug = New System.Windows.Forms.ToolStripMenuItem()
        Me.Info = New System.Windows.Forms.ToolStripMenuItem()
        Me.Warn = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Fatal1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Off1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartRegionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartOneRegionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartTheInstanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsStopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsStartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsSuspendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScriptsResumeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ThreadpoolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.XengineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.JobEngineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewLogsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SimulatorStatsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewWebUI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewIcecastWebPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.DiagnosticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CHeckForUpdatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.RevisionHistoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MnuContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.IslandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClothingInventoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadLocalOARSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadLocalIARsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadRegionOarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveRegionOARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllTheRegionsOarsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadInventoryIARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveInventoryIARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoreContentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CheckAndRepairDatbaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupRestoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestoreDatabaseToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupCriticalFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.BusyButton = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.TextBox1 = New System.Windows.Forms.RichTextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ApachePictureBox = New System.Windows.Forms.PictureBox()
        Me.MysqlPictureBox = New System.Windows.Forms.PictureBox()
        Me.RobustPictureBox = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ChartWrapper1 = New MSChartWrapper.ChartWrapper()
        Me.AvatarLabel = New System.Windows.Forms.Label()
        Me.ChartWrapper2 = New MSChartWrapper.ChartWrapper()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PercentCPU = New System.Windows.Forms.Label()
        Me.PercentRAM = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.ApachePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MysqlPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RobustPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StopButton
        '
        Me.StopButton.Location = New System.Drawing.Point(214, -1)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(63, 23)
        Me.StopButton.TabIndex = 17
        Me.StopButton.Text = "Stop"
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(213, 0)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(63, 23)
        Me.StartButton.TabIndex = 16
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'InstallButton
        '
        Me.InstallButton.Location = New System.Drawing.Point(213, 0)
        Me.InstallButton.Name = "InstallButton"
        Me.InstallButton.Size = New System.Drawing.Size(63, 23)
        Me.InstallButton.TabIndex = 15
        Me.InstallButton.Text = "Install"
        Me.InstallButton.UseVisualStyleBackColor = True
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JustQuitToolStripMenuItem, Me.mnuExit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'JustQuitToolStripMenuItem
        '
        Me.JustQuitToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flash
        Me.JustQuitToolStripMenuItem.Name = "JustQuitToolStripMenuItem"
        Me.JustQuitToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.JustQuitToolStripMenuItem.Text = "Just Quit"
        '
        'mnuExit
        '
        Me.mnuExit.Image = Global.Outworldz.My.Resources.Resources.exit_icon
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mnuExit.Size = New System.Drawing.Size(133, 22)
        Me.mnuExit.Text = "Exit"
        '
        'mnuSettings
        '
        Me.mnuSettings.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RegionsToolStripMenuItem, Me.ConsoleToolStripMenuItem1, Me.AdvancedSettingsToolStripMenuItem})
        Me.mnuSettings.Name = "mnuSettings"
        Me.mnuSettings.Size = New System.Drawing.Size(61, 20)
        Me.mnuSettings.Text = "Settings"
        '
        'RegionsToolStripMenuItem
        '
        Me.RegionsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.server_gWSCLient
        Me.RegionsToolStripMenuItem.Name = "RegionsToolStripMenuItem"
        Me.RegionsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RegionsToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.RegionsToolStripMenuItem.Text = "Regions"
        '
        'ConsoleToolStripMenuItem1
        '
        Me.ConsoleToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHide, Me.mnuShow})
        Me.ConsoleToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.window_add
        Me.ConsoleToolStripMenuItem1.Name = "ConsoleToolStripMenuItem1"
        Me.ConsoleToolStripMenuItem1.Size = New System.Drawing.Size(157, 22)
        Me.ConsoleToolStripMenuItem1.Text = "Consoles"
        Me.ConsoleToolStripMenuItem1.ToolTipText = "The Opensim Dos Box can be minimized automatically"
        '
        'mnuHide
        '
        Me.mnuHide.Checked = True
        Me.mnuHide.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuHide.Name = "mnuHide"
        Me.mnuHide.Size = New System.Drawing.Size(103, 22)
        Me.mnuHide.Text = "Hide"
        '
        'mnuShow
        '
        Me.mnuShow.Name = "mnuShow"
        Me.mnuShow.Size = New System.Drawing.Size(103, 22)
        Me.mnuShow.Text = "Show"
        '
        'AdvancedSettingsToolStripMenuItem
        '
        Me.AdvancedSettingsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.earth_network
        Me.AdvancedSettingsToolStripMenuItem.Name = "AdvancedSettingsToolStripMenuItem"
        Me.AdvancedSettingsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.AdvancedSettingsToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.AdvancedSettingsToolStripMenuItem.Text = "Settings"
        Me.AdvancedSettingsToolStripMenuItem.ToolTipText = "Deep stuff."
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowHyperGridAddressToolStripMenuItem, Me.ToolStripSeparator1, Me.HelpStartingUpToolStripMenuItem1, Me.HelpOnSettingsToolStripMenuItem, Me.LoopBackToolStripMenuItem, Me.ToolStripMenuItem1, Me.HelpOnIARSToolStripMenuItem, Me.HelpOnOARsToolStripMenuItem, Me.TroubleshootingToolStripMenuItem, Me.TechnicalInfoToolStripMenuItem, Me.ConsoleCOmmandsToolStripMenuItem1, Me.CommonConsoleCommandsToolStripMenuItem, Me.ToolStripSeparator2, Me.ToolStripSeparator7, Me.ViewLogsToolStripMenuItem, Me.SimulatorStatsToolStripMenuItem, Me.ViewWebUI, Me.ViewIcecastWebPageToolStripMenuItem, Me.ToolStripSeparator4, Me.DiagnosticsToolStripMenuItem, Me.ToolStripMenuItem2, Me.CHeckForUpdatesToolStripMenuItem, Me.ToolStripSeparator5, Me.RevisionHistoryToolStripMenuItem, Me.mnuAbout})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ShowHyperGridAddressToolStripMenuItem
        '
        Me.ShowHyperGridAddressToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ShowHyperGridAddressToolStripMenuItem.Name = "ShowHyperGridAddressToolStripMenuItem"
        Me.ShowHyperGridAddressToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
        Me.ShowHyperGridAddressToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.ShowHyperGridAddressToolStripMenuItem.Text = "Show Grid Address"
        Me.ShowHyperGridAddressToolStripMenuItem.ToolTipText = "You can give this address out to people and they can visit your grid"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(262, 6)
        '
        'HelpStartingUpToolStripMenuItem1
        '
        Me.HelpStartingUpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.box_tall
        Me.HelpStartingUpToolStripMenuItem1.Name = "HelpStartingUpToolStripMenuItem1"
        Me.HelpStartingUpToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.D1), System.Windows.Forms.Keys)
        Me.HelpStartingUpToolStripMenuItem1.Size = New System.Drawing.Size(265, 22)
        Me.HelpStartingUpToolStripMenuItem1.Text = "Help Starting Up"
        '
        'HelpOnSettingsToolStripMenuItem
        '
        Me.HelpOnSettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PDFManualToolStripMenuItem})
        Me.HelpOnSettingsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.gear
        Me.HelpOnSettingsToolStripMenuItem.Name = "HelpOnSettingsToolStripMenuItem"
        Me.HelpOnSettingsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.HelpOnSettingsToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.HelpOnSettingsToolStripMenuItem.Text = "Help Manuals"
        '
        'PDFManualToolStripMenuItem
        '
        Me.PDFManualToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.pdf
        Me.PDFManualToolStripMenuItem.Name = "PDFManualToolStripMenuItem"
        Me.PDFManualToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.PDFManualToolStripMenuItem.Text = "PDF Manual"
        '
        'LoopBackToolStripMenuItem
        '
        Me.LoopBackToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.refresh
        Me.LoopBackToolStripMenuItem.Name = "LoopBackToolStripMenuItem"
        Me.LoopBackToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.LoopBackToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.LoopBackToolStripMenuItem.Text = "Help on LoopBack "
        Me.LoopBackToolStripMenuItem.ToolTipText = "How to fix Loopback on Windows"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.document_connection
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(265, 22)
        Me.ToolStripMenuItem1.Text = "Help on Port Forwarding"
        Me.ToolStripMenuItem1.ToolTipText = "Web Help for Port Forwarding"
        '
        'HelpOnIARSToolStripMenuItem
        '
        Me.HelpOnIARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.HelpOnIARSToolStripMenuItem.Name = "HelpOnIARSToolStripMenuItem"
        Me.HelpOnIARSToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.HelpOnIARSToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.HelpOnIARSToolStripMenuItem.Text = "Help on IARS"
        Me.HelpOnIARSToolStripMenuItem.ToolTipText = "Wiki Page on IAR's"
        '
        'HelpOnOARsToolStripMenuItem
        '
        Me.HelpOnOARsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
        Me.HelpOnOARsToolStripMenuItem.Name = "HelpOnOARsToolStripMenuItem"
        Me.HelpOnOARsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.HelpOnOARsToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.HelpOnOARsToolStripMenuItem.Text = "Help on OAR's"
        Me.HelpOnOARsToolStripMenuItem.ToolTipText = "Wiki Page on OARS"
        '
        'TroubleshootingToolStripMenuItem
        '
        Me.TroubleshootingToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.TroubleshootingToolStripMenuItem.Name = "TroubleshootingToolStripMenuItem"
        Me.TroubleshootingToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.TroubleshootingToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.TroubleshootingToolStripMenuItem.Text = "Help Troubleshooting"
        '
        'TechnicalInfoToolStripMenuItem
        '
        Me.TechnicalInfoToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_dirty
        Me.TechnicalInfoToolStripMenuItem.Name = "TechnicalInfoToolStripMenuItem"
        Me.TechnicalInfoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.TechnicalInfoToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.TechnicalInfoToolStripMenuItem.Text = "Help with Technical Info"
        Me.TechnicalInfoToolStripMenuItem.ToolTipText = "Technical Mumbo Jumnbio on how to configure things"
        '
        'ConsoleCOmmandsToolStripMenuItem1
        '
        Me.ConsoleCOmmandsToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.text_marked
        Me.ConsoleCOmmandsToolStripMenuItem1.Name = "ConsoleCOmmandsToolStripMenuItem1"
        Me.ConsoleCOmmandsToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.ConsoleCOmmandsToolStripMenuItem1.Size = New System.Drawing.Size(265, 22)
        Me.ConsoleCOmmandsToolStripMenuItem1.Text = "Help on Console Commands"
        Me.ConsoleCOmmandsToolStripMenuItem1.ToolTipText = "Wiki Page on Console COmmands"
        '
        'CommonConsoleCommandsToolStripMenuItem
        '
        Me.CommonConsoleCommandsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UsersToolStripMenuItem, Me.SendAlertToAllUsersToolStripMenuItem, Me.DebugToolStripMenuItem, Me.RestartRegionToolStripMenuItem, Me.ScriptsToolStripMenuItem, Me.ShowStatusToolStripMenuItem})
        Me.CommonConsoleCommandsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_new
        Me.CommonConsoleCommandsToolStripMenuItem.Name = "CommonConsoleCommandsToolStripMenuItem"
        Me.CommonConsoleCommandsToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.CommonConsoleCommandsToolStripMenuItem.Text = "Issue Console Commands"
        '
        'UsersToolStripMenuItem
        '
        Me.UsersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddUserToolStripMenuItem, Me.ChangePasswordToolStripMenuItem, Me.ShowUserDetailsToolStripMenuItem})
        Me.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem"
        Me.UsersToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.UsersToolStripMenuItem.Text = "Users"
        '
        'AddUserToolStripMenuItem
        '
        Me.AddUserToolStripMenuItem.Name = "AddUserToolStripMenuItem"
        Me.AddUserToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.AddUserToolStripMenuItem.Text = "Add User"
        '
        'ChangePasswordToolStripMenuItem
        '
        Me.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem"
        Me.ChangePasswordToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ChangePasswordToolStripMenuItem.Text = "Change Password"
        '
        'ShowUserDetailsToolStripMenuItem
        '
        Me.ShowUserDetailsToolStripMenuItem.Name = "ShowUserDetailsToolStripMenuItem"
        Me.ShowUserDetailsToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ShowUserDetailsToolStripMenuItem.Text = "Show User Details"
        '
        'SendAlertToAllUsersToolStripMenuItem
        '
        Me.SendAlertToAllUsersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllUsersAllSimsToolStripMenuItem, Me.JustOneRegionToolStripMenuItem})
        Me.SendAlertToAllUsersToolStripMenuItem.Name = "SendAlertToAllUsersToolStripMenuItem"
        Me.SendAlertToAllUsersToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SendAlertToAllUsersToolStripMenuItem.Text = "Send Alert Message"
        '
        'AllUsersAllSimsToolStripMenuItem
        '
        Me.AllUsersAllSimsToolStripMenuItem.Name = "AllUsersAllSimsToolStripMenuItem"
        Me.AllUsersAllSimsToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.AllUsersAllSimsToolStripMenuItem.Text = "All Users, All Sims"
        '
        'JustOneRegionToolStripMenuItem
        '
        Me.JustOneRegionToolStripMenuItem.Name = "JustOneRegionToolStripMenuItem"
        Me.JustOneRegionToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.JustOneRegionToolStripMenuItem.Text = "Just one region"
        '
        'DebugToolStripMenuItem
        '
        Me.DebugToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.All, Me.Debug, Me.Info, Me.Warn, Me.ErrorToolStripMenuItem, Me.Fatal1, Me.Off1})
        Me.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem"
        Me.DebugToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.DebugToolStripMenuItem.Text = "Set Debug Level"
        '
        'All
        '
        Me.All.Name = "All"
        Me.All.Size = New System.Drawing.Size(109, 22)
        Me.All.Text = "All"
        '
        'Debug
        '
        Me.Debug.Name = "Debug"
        Me.Debug.Size = New System.Drawing.Size(109, 22)
        Me.Debug.Text = "Debug"
        '
        'Info
        '
        Me.Info.Name = "Info"
        Me.Info.Size = New System.Drawing.Size(109, 22)
        Me.Info.Text = "Info"
        '
        'Warn
        '
        Me.Warn.Name = "Warn"
        Me.Warn.Size = New System.Drawing.Size(109, 22)
        Me.Warn.Text = "Warn"
        '
        'ErrorToolStripMenuItem
        '
        Me.ErrorToolStripMenuItem.Name = "ErrorToolStripMenuItem"
        Me.ErrorToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.ErrorToolStripMenuItem.Text = "Error"
        '
        'Fatal1
        '
        Me.Fatal1.Name = "Fatal1"
        Me.Fatal1.Size = New System.Drawing.Size(109, 22)
        Me.Fatal1.Text = "Fatal"
        '
        'Off1
        '
        Me.Off1.Name = "Off1"
        Me.Off1.Size = New System.Drawing.Size(109, 22)
        Me.Off1.Text = "Off"
        '
        'RestartRegionToolStripMenuItem
        '
        Me.RestartRegionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestartOneRegionToolStripMenuItem, Me.RestartTheInstanceToolStripMenuItem})
        Me.RestartRegionToolStripMenuItem.Name = "RestartRegionToolStripMenuItem"
        Me.RestartRegionToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RestartRegionToolStripMenuItem.Text = "Restart Region"
        '
        'RestartOneRegionToolStripMenuItem
        '
        Me.RestartOneRegionToolStripMenuItem.Name = "RestartOneRegionToolStripMenuItem"
        Me.RestartOneRegionToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RestartOneRegionToolStripMenuItem.Text = "Restart one region"
        '
        'RestartTheInstanceToolStripMenuItem
        '
        Me.RestartTheInstanceToolStripMenuItem.Name = "RestartTheInstanceToolStripMenuItem"
        Me.RestartTheInstanceToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RestartTheInstanceToolStripMenuItem.Text = "Restart one instance"
        '
        'ScriptsToolStripMenuItem
        '
        Me.ScriptsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScriptsStopToolStripMenuItem, Me.ScriptsStartToolStripMenuItem, Me.ScriptsSuspendToolStripMenuItem, Me.ScriptsResumeToolStripMenuItem})
        Me.ScriptsToolStripMenuItem.Name = "ScriptsToolStripMenuItem"
        Me.ScriptsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ScriptsToolStripMenuItem.Text = "Scripts"
        '
        'ScriptsStopToolStripMenuItem
        '
        Me.ScriptsStopToolStripMenuItem.Name = "ScriptsStopToolStripMenuItem"
        Me.ScriptsStopToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ScriptsStopToolStripMenuItem.Text = "Scripts Stop"
        '
        'ScriptsStartToolStripMenuItem
        '
        Me.ScriptsStartToolStripMenuItem.Name = "ScriptsStartToolStripMenuItem"
        Me.ScriptsStartToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ScriptsStartToolStripMenuItem.Text = "Scripts Start"
        '
        'ScriptsSuspendToolStripMenuItem
        '
        Me.ScriptsSuspendToolStripMenuItem.Name = "ScriptsSuspendToolStripMenuItem"
        Me.ScriptsSuspendToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ScriptsSuspendToolStripMenuItem.Text = "Scripts Suspend"
        '
        'ScriptsResumeToolStripMenuItem
        '
        Me.ScriptsResumeToolStripMenuItem.Name = "ScriptsResumeToolStripMenuItem"
        Me.ScriptsResumeToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ScriptsResumeToolStripMenuItem.Text = "Scripts Resume"
        '
        'ShowStatusToolStripMenuItem
        '
        Me.ShowStatusToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ThreadpoolsToolStripMenuItem, Me.XengineToolStripMenuItem, Me.JobEngineToolStripMenuItem})
        Me.ShowStatusToolStripMenuItem.Name = "ShowStatusToolStripMenuItem"
        Me.ShowStatusToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ShowStatusToolStripMenuItem.Text = "Show Status"
        '
        'ThreadpoolsToolStripMenuItem
        '
        Me.ThreadpoolsToolStripMenuItem.Name = "ThreadpoolsToolStripMenuItem"
        Me.ThreadpoolsToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.ThreadpoolsToolStripMenuItem.Text = "Threadpools"
        '
        'XengineToolStripMenuItem
        '
        Me.XengineToolStripMenuItem.Name = "XengineToolStripMenuItem"
        Me.XengineToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.XengineToolStripMenuItem.Text = "Xengine"
        '
        'JobEngineToolStripMenuItem
        '
        Me.JobEngineToolStripMenuItem.Name = "JobEngineToolStripMenuItem"
        Me.JobEngineToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.JobEngineToolStripMenuItem.Text = "JobEngine"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(262, 6)
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(262, 6)
        '
        'ViewLogsToolStripMenuItem
        '
        Me.ViewLogsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.ViewLogsToolStripMenuItem.Name = "ViewLogsToolStripMenuItem"
        Me.ViewLogsToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.ViewLogsToolStripMenuItem.Text = "View Logs"
        '
        'SimulatorStatsToolStripMenuItem
        '
        Me.SimulatorStatsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.SimulatorStatsToolStripMenuItem.Name = "SimulatorStatsToolStripMenuItem"
        Me.SimulatorStatsToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.SimulatorStatsToolStripMenuItem.Text = "View Simulator Stats"
        Me.SimulatorStatsToolStripMenuItem.Visible = False
        '
        'ViewWebUI
        '
        Me.ViewWebUI.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.ViewWebUI.Name = "ViewWebUI"
        Me.ViewWebUI.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.ViewWebUI.Size = New System.Drawing.Size(265, 22)
        Me.ViewWebUI.Text = "View Web Interface"
        Me.ViewWebUI.ToolTipText = "The WIfi Interface can be used to add new users"
        '
        'ViewIcecastWebPageToolStripMenuItem
        '
        Me.ViewIcecastWebPageToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.cube_blue
        Me.ViewIcecastWebPageToolStripMenuItem.Name = "ViewIcecastWebPageToolStripMenuItem"
        Me.ViewIcecastWebPageToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.ViewIcecastWebPageToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.ViewIcecastWebPageToolStripMenuItem.Text = "View Icecast Web Page"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(262, 6)
        '
        'DiagnosticsToolStripMenuItem
        '
        Me.DiagnosticsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.server_gWSCLient
        Me.DiagnosticsToolStripMenuItem.Name = "DiagnosticsToolStripMenuItem"
        Me.DiagnosticsToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.DiagnosticsToolStripMenuItem.Text = "Network Diagnostics"
        Me.DiagnosticsToolStripMenuItem.ToolTipText = "Re-Run the installation diagnostics"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = Global.Outworldz.My.Resources.Resources.earth_network
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(265, 22)
        Me.ToolStripMenuItem2.Text = "UPnP Setup Program"
        '
        'CHeckForUpdatesToolStripMenuItem
        '
        Me.CHeckForUpdatesToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
        Me.CHeckForUpdatesToolStripMenuItem.Name = "CHeckForUpdatesToolStripMenuItem"
        Me.CHeckForUpdatesToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.CHeckForUpdatesToolStripMenuItem.Text = "Check for Updates"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(262, 6)
        '
        'RevisionHistoryToolStripMenuItem
        '
        Me.RevisionHistoryToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_dirty
        Me.RevisionHistoryToolStripMenuItem.Name = "RevisionHistoryToolStripMenuItem"
        Me.RevisionHistoryToolStripMenuItem.Size = New System.Drawing.Size(265, 22)
        Me.RevisionHistoryToolStripMenuItem.Text = "Revision History"
        '
        'mnuAbout
        '
        Me.mnuAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.mnuAbout.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(265, 22)
        Me.mnuAbout.Text = "About"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.mnuSettings, Me.MnuContent, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(304, 24)
        Me.MenuStrip1.TabIndex = 21
        Me.MenuStrip1.Text = "0"
        '
        'MnuContent
        '
        Me.MnuContent.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IslandToolStripMenuItem, Me.ClothingInventoryToolStripMenuItem, Me.LoadLocalOARSToolStripMenuItem, Me.LoadLocalIARsToolStripMenuItem, Me.OARToolStripMenuItem, Me.IARToolStripMenuItem, Me.MoreContentToolStripMenuItem, Me.ToolStripSeparator3, Me.CheckAndRepairDatbaseToolStripMenuItem, Me.BackupRestoreToolStripMenuItem, Me.BackupCriticalFilesToolStripMenuItem, Me.ToolStripSeparator6})
        Me.MnuContent.Name = "MnuContent"
        Me.MnuContent.Size = New System.Drawing.Size(62, 20)
        Me.MnuContent.Text = "Content"
        '
        'IslandToolStripMenuItem
        '
        Me.IslandToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_tall
        Me.IslandToolStripMenuItem.Name = "IslandToolStripMenuItem"
        Me.IslandToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.IslandToolStripMenuItem.Text = "Load Free Dreamworld OARs"
        Me.IslandToolStripMenuItem.ToolTipText = "OAR files are backups of entire Islands"
        '
        'ClothingInventoryToolStripMenuItem
        '
        Me.ClothingInventoryToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.user1_into
        Me.ClothingInventoryToolStripMenuItem.Name = "ClothingInventoryToolStripMenuItem"
        Me.ClothingInventoryToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.ClothingInventoryToolStripMenuItem.Text = "Load Free Avatar Inventory"
        Me.ClothingInventoryToolStripMenuItem.ToolTipText = "IAR files are backups of inventory items"
        '
        'LoadLocalOARSToolStripMenuItem
        '
        Me.LoadLocalOARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_tall
        Me.LoadLocalOARSToolStripMenuItem.Name = "LoadLocalOARSToolStripMenuItem"
        Me.LoadLocalOARSToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.LoadLocalOARSToolStripMenuItem.Text = "Load Local OARs from the OAR folder"
        '
        'LoadLocalIARsToolStripMenuItem
        '
        Me.LoadLocalIARsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.user1_into
        Me.LoadLocalIARsToolStripMenuItem.Name = "LoadLocalIARsToolStripMenuItem"
        Me.LoadLocalIARsToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.LoadLocalIARsToolStripMenuItem.Text = "Load Local IARs from the IAR folder"
        '
        'OARToolStripMenuItem
        '
        Me.OARToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadRegionOarToolStripMenuItem, Me.SaveRegionOARToolStripMenuItem, Me.AllTheRegionsOarsToolStripMenuItem})
        Me.OARToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_green
        Me.OARToolStripMenuItem.Name = "OARToolStripMenuItem"
        Me.OARToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.OARToolStripMenuItem.Text = "OAR Load and Save"
        '
        'LoadRegionOarToolStripMenuItem
        '
        Me.LoadRegionOarToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.data
        Me.LoadRegionOarToolStripMenuItem.Name = "LoadRegionOarToolStripMenuItem"
        Me.LoadRegionOarToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.LoadRegionOarToolStripMenuItem.Text = "Load Region OAR"
        '
        'SaveRegionOARToolStripMenuItem
        '
        Me.SaveRegionOARToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_green
        Me.SaveRegionOARToolStripMenuItem.Name = "SaveRegionOARToolStripMenuItem"
        Me.SaveRegionOARToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.SaveRegionOARToolStripMenuItem.Text = "Save Region OAR"
        '
        'AllTheRegionsOarsToolStripMenuItem
        '
        Me.AllTheRegionsOarsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_green
        Me.AllTheRegionsOarsToolStripMenuItem.Name = "AllTheRegionsOarsToolStripMenuItem"
        Me.AllTheRegionsOarsToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.AllTheRegionsOarsToolStripMenuItem.Text = "Save All Regions to OARs"
        '
        'IARToolStripMenuItem
        '
        Me.IARToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadInventoryIARToolStripMenuItem, Me.SaveInventoryIARToolStripMenuItem})
        Me.IARToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_yellow
        Me.IARToolStripMenuItem.Name = "IARToolStripMenuItem"
        Me.IARToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.IARToolStripMenuItem.Text = "IAR Load and Save"
        '
        'LoadInventoryIARToolStripMenuItem
        '
        Me.LoadInventoryIARToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.data
        Me.LoadInventoryIARToolStripMenuItem.Name = "LoadInventoryIARToolStripMenuItem"
        Me.LoadInventoryIARToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.LoadInventoryIARToolStripMenuItem.Text = "Load Inventory IAR"
        '
        'SaveInventoryIARToolStripMenuItem
        '
        Me.SaveInventoryIARToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_yellow
        Me.SaveInventoryIARToolStripMenuItem.Name = "SaveInventoryIARToolStripMenuItem"
        Me.SaveInventoryIARToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.SaveInventoryIARToolStripMenuItem.Text = "Save Inventory IAR"
        '
        'MoreContentToolStripMenuItem
        '
        Me.MoreContentToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
        Me.MoreContentToolStripMenuItem.Name = "MoreContentToolStripMenuItem"
        Me.MoreContentToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.MoreContentToolStripMenuItem.Text = "More Free Islands and Parts"
        Me.MoreContentToolStripMenuItem.ToolTipText = "Outworldz has free DLC"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(270, 6)
        '
        'CheckAndRepairDatbaseToolStripMenuItem
        '
        Me.CheckAndRepairDatbaseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.server_gWSCLient
        Me.CheckAndRepairDatbaseToolStripMenuItem.Name = "CheckAndRepairDatbaseToolStripMenuItem"
        Me.CheckAndRepairDatbaseToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.CheckAndRepairDatbaseToolStripMenuItem.Text = "Check and Repair Database"
        '
        'BackupRestoreToolStripMenuItem
        '
        Me.BackupRestoreToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BackupDatabaseToolStripMenuItem, Me.RestoreDatabaseToolStripMenuItem1})
        Me.BackupRestoreToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.BackupRestoreToolStripMenuItem.Name = "BackupRestoreToolStripMenuItem"
        Me.BackupRestoreToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.BackupRestoreToolStripMenuItem.Text = "SQL Database Backup/Restore "
        '
        'BackupDatabaseToolStripMenuItem
        '
        Me.BackupDatabaseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.BackupDatabaseToolStripMenuItem.Name = "BackupDatabaseToolStripMenuItem"
        Me.BackupDatabaseToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BackupDatabaseToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.BackupDatabaseToolStripMenuItem.Text = "Backup Databases"
        '
        'RestoreDatabaseToolStripMenuItem1
        '
        Me.RestoreDatabaseToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.cube_blue
        Me.RestoreDatabaseToolStripMenuItem1.Name = "RestoreDatabaseToolStripMenuItem1"
        Me.RestoreDatabaseToolStripMenuItem1.Size = New System.Drawing.Size(210, 22)
        Me.RestoreDatabaseToolStripMenuItem1.Text = "Restore Database"
        '
        'BackupCriticalFilesToolStripMenuItem
        '
        Me.BackupCriticalFilesToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
        Me.BackupCriticalFilesToolStripMenuItem.Name = "BackupCriticalFilesToolStripMenuItem"
        Me.BackupCriticalFilesToolStripMenuItem.Size = New System.Drawing.Size(273, 22)
        Me.BackupCriticalFilesToolStripMenuItem.Text = "System Backup"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(270, 6)
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(16, 30)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(261, 13)
        Me.ProgressBar1.TabIndex = 24
        '
        'BusyButton
        '
        Me.BusyButton.Location = New System.Drawing.Point(214, -1)
        Me.BusyButton.Name = "BusyButton"
        Me.BusyButton.Size = New System.Drawing.Size(63, 23)
        Me.BusyButton.TabIndex = 18
        Me.BusyButton.Text = "Busy"
        Me.BusyButton.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.FileName = "OpenFileDialog2"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.EnableAutoDragDrop = True
        Me.TextBox1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(15, 49)
        Me.TextBox1.MaxLength = 15000
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(261, 85)
        Me.TextBox1.TabIndex = 29
        Me.TextBox1.Text = ""
        '
        'ApachePictureBox
        '
        Me.ApachePictureBox.Image = CType(resources.GetObject("ApachePictureBox.Image"), System.Drawing.Image)
        Me.ApachePictureBox.Location = New System.Drawing.Point(363, 5)
        Me.ApachePictureBox.Name = "ApachePictureBox"
        Me.ApachePictureBox.Size = New System.Drawing.Size(17, 17)
        Me.ApachePictureBox.TabIndex = 39
        Me.ApachePictureBox.TabStop = False
        Me.ToolTip1.SetToolTip(Me.ApachePictureBox, "Apache Status")
        '
        'MysqlPictureBox
        '
        Me.MysqlPictureBox.Image = CType(resources.GetObject("MysqlPictureBox.Image"), System.Drawing.Image)
        Me.MysqlPictureBox.Location = New System.Drawing.Point(325, 5)
        Me.MysqlPictureBox.Name = "MysqlPictureBox"
        Me.MysqlPictureBox.Size = New System.Drawing.Size(17, 17)
        Me.MysqlPictureBox.TabIndex = 40
        Me.MysqlPictureBox.TabStop = False
        Me.ToolTip1.SetToolTip(Me.MysqlPictureBox, "MySQL Status")
        '
        'RobustPictureBox
        '
        Me.RobustPictureBox.Image = CType(resources.GetObject("RobustPictureBox.Image"), System.Drawing.Image)
        Me.RobustPictureBox.Location = New System.Drawing.Point(402, 5)
        Me.RobustPictureBox.Name = "RobustPictureBox"
        Me.RobustPictureBox.Size = New System.Drawing.Size(17, 17)
        Me.RobustPictureBox.TabIndex = 43
        Me.RobustPictureBox.TabStop = False
        Me.ToolTip1.SetToolTip(Me.RobustPictureBox, "Apache Status")
        '
        'PictureBox1
        '
        Me.PictureBox1.AccessibleName = "Arrow2Right"
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.Arrow2Right
        Me.PictureBox1.Location = New System.Drawing.Point(282, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(14, 14)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 45
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "MySQL Status")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(283, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 30
        '
        'ChartWrapper1
        '
        Me.ChartWrapper1.AddMarkers = True
        Me.ChartWrapper1.AxisXTitle = "Seconds"
        Me.ChartWrapper1.AxisYTitle = "CPU %"
        Me.ChartWrapper1.LegendVisible = False
        Me.ChartWrapper1.Location = New System.Drawing.Point(312, 49)
        Me.ChartWrapper1.MarkerCount = 15
        Me.ChartWrapper1.MarkerFreq = 0
        Me.ChartWrapper1.MarkerSize = 8
        Me.ChartWrapper1.Name = "ChartWrapper1"
        Me.ChartWrapper1.SideLegendVisible = True
        Me.ChartWrapper1.Size = New System.Drawing.Size(225, 159)
        Me.ChartWrapper1.TabIndex = 31
        Me.ChartWrapper1.Title = ""
        '
        'AvatarLabel
        '
        Me.AvatarLabel.AutoSize = True
        Me.AvatarLabel.Location = New System.Drawing.Point(23, 30)
        Me.AvatarLabel.Name = "AvatarLabel"
        Me.AvatarLabel.Size = New System.Drawing.Size(13, 13)
        Me.AvatarLabel.TabIndex = 32
        Me.AvatarLabel.Text = "0"
        '
        'ChartWrapper2
        '
        Me.ChartWrapper2.AddMarkers = True
        Me.ChartWrapper2.AxisXTitle = "Seconds"
        Me.ChartWrapper2.AxisYTitle = "% Memory"
        Me.ChartWrapper2.LegendVisible = False
        Me.ChartWrapper2.Location = New System.Drawing.Point(312, 214)
        Me.ChartWrapper2.MarkerCount = 15
        Me.ChartWrapper2.MarkerFreq = 0
        Me.ChartWrapper2.MarkerSize = 8
        Me.ChartWrapper2.Name = "ChartWrapper2"
        Me.ChartWrapper2.SideLegendVisible = True
        Me.ChartWrapper2.Size = New System.Drawing.Size(225, 159)
        Me.ChartWrapper2.TabIndex = 33
        Me.ChartWrapper2.Title = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(42, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Avatars"
        '
        'PercentCPU
        '
        Me.PercentCPU.AutoSize = True
        Me.PercentCPU.Location = New System.Drawing.Point(115, 30)
        Me.PercentCPU.Name = "PercentCPU"
        Me.PercentCPU.Size = New System.Drawing.Size(13, 13)
        Me.PercentCPU.TabIndex = 35
        Me.PercentCPU.Text = "0"
        '
        'PercentRAM
        '
        Me.PercentRAM.AutoSize = True
        Me.PercentRAM.Location = New System.Drawing.Point(176, 30)
        Me.PercentRAM.Name = "PercentRAM"
        Me.PercentRAM.Size = New System.Drawing.Size(13, 13)
        Me.PercentRAM.TabIndex = 37
        Me.PercentRAM.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(309, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "MySQL"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(348, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Apache"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(389, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "Robust"
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(304, 141)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.RobustPictureBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.MysqlPictureBox)
        Me.Controls.Add(Me.ApachePictureBox)
        Me.Controls.Add(Me.PercentRAM)
        Me.Controls.Add(Me.PercentCPU)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ChartWrapper2)
        Me.Controls.Add(Me.AvatarLabel)
        Me.Controls.Add(Me.ChartWrapper1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.BusyButton)
        Me.Controls.Add(Me.StopButton)
        Me.Controls.Add(Me.StartButton)
        Me.Controls.Add(Me.InstallButton)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.TextBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(320, 180)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DreamGrid"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.ApachePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MysqlPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RobustPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Friend WithEvents BusyButton As System.Windows.Forms.Button
    Friend WithEvents StopButton As System.Windows.Forms.Button
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents InstallButton As System.Windows.Forms.Button
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuExit As ToolStripMenuItem
    Friend WithEvents mnuSettings As ToolStripMenuItem
    Friend WithEvents ConsoleToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents mnuHide As ToolStripMenuItem
    Friend WithEvents mnuShow As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuAbout As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ViewWebUI As ToolStripMenuItem
    Friend WithEvents MnuContent As ToolStripMenuItem
    Friend WithEvents IslandToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClothingInventoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MensClothingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FemaleClothingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoopBackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MoreContentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AdvancedSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents CHeckForUpdatesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DiagnosticsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents OpenFileDialog2 As OpenFileDialog
    Friend WithEvents ShowHyperGridAddressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBox1 As RichTextBox
    Friend WithEvents ConsoleCOmmandsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents RegionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackupRestoreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackupDatabaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestoreDatabaseToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents OARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveRegionOARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadRegionOarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadInventoryIARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveInventoryIARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents AllTheRegionsOarsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CommonConsoleCommandsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SendAlertToAllUsersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DebugToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents All As ToolStripMenuItem
    Friend WithEvents Debug As ToolStripMenuItem
    Friend WithEvents Info As ToolStripMenuItem
    Friend WithEvents Warn As ToolStripMenuItem
    Friend WithEvents ErrorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Fatal1 As ToolStripMenuItem
    Friend WithEvents Off1 As ToolStripMenuItem
    Friend WithEvents ViewIcecastWebPageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestartRegionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestartOneRegionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RestartTheInstanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsStopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsStartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsSuspendToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScriptsResumeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllUsersAllSimsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JustOneRegionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UsersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddUserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChangePasswordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowUserDetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SimulatorStatsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents LoadLocalOARSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadLocalIARsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpOnOARsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpOnIARSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackupCriticalFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents TechnicalInfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TroubleshootingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckAndRepairDatbaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpStartingUpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents HelpOnSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewLogsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents RevisionHistoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ThreadpoolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents XengineToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents JobEngineToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents ChartWrapper1 As MSChartWrapper.ChartWrapper
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents AvatarLabel As Label
    Friend WithEvents PDFManualToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChartWrapper2 As MSChartWrapper.ChartWrapper
    Friend WithEvents Label3 As Label
    Friend WithEvents PercentCPU As Label
    Friend WithEvents PercentRAM As Label
    Friend WithEvents JustQuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ApachePictureBox As PictureBox
    Friend WithEvents MysqlPictureBox As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents RobustPictureBox As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
