Module Translate

    Public Sub Run(Name As String)

        Select Case Name

            Case "FormApache"
                FormApache.ApacheCheckbox.Text = Global.Outworldz.My.Resources.Resources.EnableApache
                FormApache.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
                FormApache.ApacheToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Apache_word
                FormApache.EnableDiva.Text = Global.Outworldz.My.Resources.Resources.EnableDiva
                FormApache.EnableJOpensim.Text = Global.Outworldz.My.Resources.Resources.JOpensim_word
                FormApache.EnableOther.Text = Global.Outworldz.My.Resources.Resources.EnableOther_Word
                FormApache.EnableWP.Text = My.Resources.WordPress_Word
                FormApache.GroupBox2.Text = My.Resources.Apache_word
                FormApache.GroupBox3.Text = My.Resources.Content_Manager_Word
                FormApache.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormApache.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormApache.Label3.Text = My.Resources.Web_Port
                FormApache.Text = My.Resources.Apache_word
                FormApache.X86Button.Text = Global.Outworldz.My.Resources.Resources.InstallRuntime
            Case "FormAutoBackups"
                FormAutoBackups.AutoBackup.Text = Global.Outworldz.My.Resources.Enabled_word
                FormAutoBackups.AutoBackupHelp.Image = Global.Outworldz.My.Resources.about
                FormAutoBackups.BackupToolStripMenuItem.Image = Global.Outworldz.My.Resources.disks
                FormAutoBackups.BackupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Backup_word
                FormAutoBackups.DataOnlyToolStripMenuItem.Image = Global.Outworldz.My.Resources.disk_yellow
                FormAutoBackups.DataOnlyToolStripMenuItem.Text = Global.Outworldz.My.Resources.Export_SQL_file_word
                FormAutoBackups.FullSQLBackupToolStripMenuItem.Image = Global.Outworldz.My.Resources.disk_blue
                FormAutoBackups.FullSQLBackupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Backup_Data_Files_word
                FormAutoBackups.GroupBox3.Text = Global.Outworldz.My.Resources.Auto_Backup_word
                FormAutoBackups.Label6.Text = Global.Outworldz.My.Resources.Backup_Folder
                FormAutoBackups.Label8.Text = Global.Outworldz.My.Resources.Interval_word
                FormAutoBackups.Label9.Text = Global.Outworldz.My.Resources.Keep_for_Days_word
                FormAutoBackups.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormAutoBackups.PictureBox1.BackgroundImage = Global.Outworldz.My.Resources.folder
                FormAutoBackups.ServerTypeToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormAutoBackups.ServerTypeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormAutoBackups.Text = Global.Outworldz.My.Resources.Auto_Backup_word
                FormAutoBackups.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormAutoBackups.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormAutoBackups.ToolTip1.SetToolTip(FormAutoBackups.AutoBackup, Global.Outworldz.My.Resources.If_Enabled_Save_Oars)
                FormAutoBackups.ToolTip1.SetToolTip(FormAutoBackups.AutoBackupInterval, Global.Outworldz.My.Resources.How_Long_runs)
                FormAutoBackups.ToolTip1.SetToolTip(FormAutoBackups.AutoBackupKeepFilesForDays, Global.Outworldz.My.Resources.How_Long)
                FormAutoBackups.ToolTip1.SetToolTip(FormAutoBackups.BaseFolder, Global.Outworldz.My.Resources.Normally_Set)
                FormAutoBackups.ToolTip1.SetToolTip(FormAutoBackups.PictureBox1, Global.Outworldz.My.Resources.Click_to_change_the_folder)
            Case "FormBackupCheckboxes"
                FormBackupCheckboxes.Button1.Text = Global.Outworldz.My.Resources.Resources.Backup_word
                FormBackupCheckboxes.CustomCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_Custom
                FormBackupCheckboxes.FSAssetsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_FSAssets
                FormBackupCheckboxes.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Backup_word
                FormBackupCheckboxes.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormBackupCheckboxes.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormBackupCheckboxes.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
                FormBackupCheckboxes.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormBackupCheckboxes.MySqlCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_Mysql
                FormBackupCheckboxes.RegionCheckBox.Text = Global.Outworldz.My.Resources.Resources.Backup_Region
                FormBackupCheckboxes.SettingsBox.Text = Global.Outworldz.My.Resources.Resources.Backup_Settings_word
                FormBackupCheckboxes.Text = Global.Outworldz.My.Resources.Resources.System_Backup_word
            Case "FormBanList"
                FormBanList.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormBanList.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormBanList.Text = Global.Outworldz.My.Resources.Ban_List_word
            Case "FormBird"
                FormBird.BirdHelp.Image = Global.Outworldz.My.Resources.about
                FormBird.BirdsModuleStartupbox.Text = Global.Outworldz.My.Resources.Enable_Birds_word
                FormBird.Button1.Text = Global.Outworldz.My.Resources.Load_Bird_IAR_word
                FormBird.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormBird.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormBird.GroupBox1.Text = Global.Outworldz.My.Resources.Bird_Module_word
                FormBird.Label1.Text = Global.Outworldz.My.Resources.Bird_Flock_Size_word
                FormBird.Label10.Text = Global.Outworldz.My.Resources.Max_Height
                FormBird.Label11.Text = Global.Outworldz.My.Resources.Prim_Name
                FormBird.Label2.Text = Global.Outworldz.My.Resources.Chat_Channel_word
                FormBird.Label3.Text = Global.Outworldz.My.Resources.Max_Speed
                FormBird.Label4.Text = Global.Outworldz.My.Resources.Max_Force
                FormBird.Label5.Text = Global.Outworldz.My.Resources.Neighbor_Distance
                FormBird.Label6.Text = Global.Outworldz.My.Resources.Desired_Separation_word
                FormBird.Label7.Text = Global.Outworldz.My.Resources.Tolerance
                FormBird.Label9.Text = Global.Outworldz.My.Resources.Border_Size
                FormBird.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormBird.Text = Global.Outworldz.My.Resources.Bird_Module_word
                FormBird.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormBird.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormBird.ToolTip1.SetToolTip(FormBird.BirdHelp, Global.Outworldz.My.Resources.Bird_help)
                FormBird.ToolTip1.SetToolTip(FormBird.BirdsModuleStartupbox, Global.Outworldz.My.Resources.Determines)
                FormBird.ToolTip1.SetToolTip(FormBird.BirdsNeighbourDistanceTextBox, Global.Outworldz.My.Resources.Max_Dist)
                FormBird.ToolTip1.SetToolTip(FormBird.DesiredSeparationTextBox, Global.Outworldz.My.Resources.How_Far)
                FormBird.ToolTip1.SetToolTip(FormBird.Label1, Global.Outworldz.My.Resources.Num_Birds)
                FormBird.ToolTip1.SetToolTip(FormBird.Label11, Global.Outworldz.My.Resources.How_High)
                FormBird.ToolTip1.SetToolTip(FormBird.Label2, Global.Outworldz.My.Resources.Which_Channel)
                FormBird.ToolTip1.SetToolTip(FormBird.Label4, Global.Outworldz.My.Resources.Max_Accel)
                FormBird.ToolTip1.SetToolTip(FormBird.Label5, Global.Outworldz.My.Resources.How_Far)
                FormBird.ToolTip1.SetToolTip(FormBird.Label6, Global.Outworldz.My.Resources.How_Far)
                FormBird.ToolTip1.SetToolTip(FormBird.Label7, Global.Outworldz.My.Resources.Tolerance)
                FormBird.ToolTip1.SetToolTip(FormBird.MaxForceTextBox, Global.Outworldz.My.Resources.How_Far_Travel)
                FormBird.ToolTip1.SetToolTip(FormBird.MaxSpeedTextBox, Global.Outworldz.My.Resources.How_Far_Travel)
                FormBird.ToolTip1.SetToolTip(FormBird.PrimNameTextBox, Global.Outworldz.My.Resources.Prim_Name)
            Case "FormCaches"
                FormCaches.Button1.Text = Global.Outworldz.My.Resources.Resources.Clear_Selected_Caches_word
                FormCaches.CheckBox1.Text = Global.Outworldz.My.Resources.Resources.Script_cache_word
                FormCaches.CheckBox2.Text = Global.Outworldz.My.Resources.Resources.Avatar_Bakes_Cache_word
                FormCaches.CheckBox3.Text = Global.Outworldz.My.Resources.Resources.Asset_Cache_word
                FormCaches.CheckBox4.Text = Global.Outworldz.My.Resources.Resources.Image_Cache_word
                FormCaches.CheckBox5.Text = Global.Outworldz.My.Resources.Resources.Mesh_Cache_word
                FormCaches.GroupBox1.Text = Global.Outworldz.My.Resources.Choose_Cache ' "Choose which cache to empty"
                FormCaches.GroupBox2.Text = Global.Outworldz.My.Resources.Asset_Cache_word
                FormCaches.GroupBox3.Text = Global.Outworldz.My.Resources.Viewer_Cache_word
                FormCaches.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormCaches.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormCaches.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
                FormCaches.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormCaches.Label1.Text = Global.Outworldz.My.Resources.Cache_Directory_word
                FormCaches.Label2.Text = Global.Outworldz.My.Resources.Log_Level
                FormCaches.Label4.Text = Global.Outworldz.My.Resources.Cache_Enabled_word
                FormCaches.Label5.Text = Global.Outworldz.My.Resources.Timeout_in_hours_word
                FormCaches.LogLevelBox.Items.AddRange(New Object() {Global.Outworldz.My.Resources.Resources.ErrorLevel0, Global.Outworldz.My.Resources.Resources.ErrorLevel1, Global.Outworldz.My.Resources.Resources.ErrorLevel2})
                FormCaches.MapHelp.Image = Global.Outworldz.My.Resources.Resources.about
                FormCaches.PictureBox1.BackgroundImage = Global.Outworldz.My.Resources.Resources.folder
                FormCaches.PictureBox2.Image = Global.Outworldz.My.Resources.Resources.about
                FormCaches.Text = Global.Outworldz.My.Resources.Cache_Control_word
                FormCaches.ToolTip1.SetToolTip(FormCaches.CacheEnabledBox, Global.Outworldz.My.Resources.Resources.Default_Checked_word)
                FormCaches.ToolTip1.SetToolTip(FormCaches.CacheTimeout, Global.Outworldz.My.Resources.Resources.Timeout_in_hours_word)
                FormCaches.ToolTip1.SetToolTip(FormCaches.MapHelp, Global.Outworldz.My.Resources.Resources.Click_For_Help)
                FormCaches.ToolTip1.SetToolTip(FormCaches.PictureBox2, Global.Outworldz.My.Resources.Resources.Click_For_Help)
                FormCaches.ToolTip1.SetToolTip(FormCaches.ViewerCacheCheckbox, Global.Outworldz.My.Resources.Resources.Viewer_Cache_text)
                FormCaches.ViewerCacheCheckbox.Text = Global.Outworldz.My.Resources.Resources.Enabled_word
            Case "FormChooser"
                FormChooser.CancelButton1.Text = Global.Outworldz.My.Resources.Cancel_word
                FormChooser.Group.HeaderText = Global.Outworldz.My.Resources.Group_word
                FormChooser.Group.Name = Global.Outworldz.My.Resources.Group_word
                FormChooser.Group.ToolTipText = Global.Outworldz.My.Resources.Click_2_Choose
                FormChooser.OKButton1.Text = Global.Outworldz.My.Resources.Ok
                FormChooser.Text = Global.Outworldz.My.Resources.Choose_Region_word
            Case "FormDNSName"
                FormDNSName.EnableHypergrid.Text = Global.Outworldz.My.Resources.Resources.Enable_Hypergrid_word
                FormDNSName.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormDNSName.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormDNSName.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
                FormDNSName.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormDNSName.Label1.Text = Global.Outworldz.My.Resources.Resources.DynDNS_password_word
                FormDNSName.Label2.Text = Global.Outworldz.My.Resources.Resources.DNSNameText
                FormDNSName.Label3.Text = Global.Outworldz.My.Resources.Resources.DNSNameText
                FormDNSName.NextNameButton.Text = Global.Outworldz.My.Resources.Resources.Next_Name
                FormDNSName.SaveButton1.Text = Global.Outworldz.My.Resources.Resources.Save_word
                FormDNSName.SuitcaseCheckbox.Text = Global.Outworldz.My.Resources.Resources.Suitcase_enable
                FormDNSName.TestButton1.Text = Global.Outworldz.My.Resources.Resources.Test_DNS_word
                FormDNSName.Text = Global.Outworldz.My.Resources.Resources.DNS_HG_Name
                FormDNSName.ToolTip1.SetToolTip(FormDNSName, Global.Outworldz.My.Resources.Resources.Help_word)
                FormDNSName.ToolTip1.SetToolTip(FormDNSName.DNSAliasTextBox, Global.Outworldz.My.Resources.Resources.DNSAlt)
                FormDNSName.ToolTip1.SetToolTip(FormDNSName.DNSNameBox, Global.Outworldz.My.Resources.Resources.AlphaNum)
                FormDNSName.ToolTip1.SetToolTip(FormDNSName.EnableHypergrid, Global.Outworldz.My.Resources.Resources.Enable_Hypergrid_word)
                FormDNSName.ToolTip1.SetToolTip(FormDNSName.NextNameButton, Global.Outworldz.My.Resources.Resources.FreeName)
                FormDNSName.ToolTip1.SetToolTip(FormDNSName.SaveButton1, Global.Outworldz.My.Resources.Resources.Save_word)
                FormDNSName.ToolTip1.SetToolTip(FormDNSName.SuitcaseCheckbox, Global.Outworldz.My.Resources.Resources.Disable_Suitcase_txt)
                FormDNSName.ToolTip1.SetToolTip(FormDNSName.TestButton1, Global.Outworldz.My.Resources.Resources.Test_DNS_word)
                FormDNSName.ToolTip1.SetToolTip(FormDNSName.UniqueId, Global.Outworldz.My.Resources.Resources.Reserve_Password)
            Case "FormDatabase"
                FormDatabase.BackupToolStripMenuItem.Image = Global.Outworldz.My.Resources.disks
                FormDatabase.BackupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Backup_word
                FormDatabase.Button1.Text = Global.Outworldz.My.Resources.FSassets_Server_word
                FormDatabase.ClearRegionTable.Text = Global.Outworldz.My.Resources.ClearRegion
                FormDatabase.DBHelp.Image = Global.Outworldz.My.Resources.about
                FormDatabase.DataOnlyToolStripMenuItem.Image = Global.Outworldz.My.Resources.disk_yellow
                FormDatabase.DataOnlyToolStripMenuItem.Text = Global.Outworldz.My.Resources.Export_Backup_file_word
                FormDatabase.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormDatabase.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Database_Setup_word
                FormDatabase.Dbnameindex.Text = Global.Outworldz.My.Resources.DBName_word
                FormDatabase.FullSQLBackupToolStripMenuItem.Image = Global.Outworldz.My.Resources.disk_blue
                FormDatabase.FullSQLBackupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Backup_Data_Files_word
                FormDatabase.GridGroup.Text = Global.Outworldz.My.Resources.Robust_word
                FormDatabase.Label1.Text = Global.Outworldz.My.Resources.Region_Server_word
                FormDatabase.Label15.Text = Global.Outworldz.My.Resources.User_Name_word
                FormDatabase.Label16.Text = Global.Outworldz.My.Resources.Robust_word
                FormDatabase.Label2.Text = Global.Outworldz.My.Resources.MySqlPort_word
                FormDatabase.Label20.Text = Outworldz.My.Resources.Region_Database
                FormDatabase.Label21.Text = Global.Outworldz.My.Resources.User_Name_word
                FormDatabase.Label22.Text = Global.Outworldz.My.Resources.Password_word
                FormDatabase.Label3.Text = Global.Outworldz.My.Resources.Assets_as_Files_word
                FormDatabase.Label8.Text = Global.Outworldz.My.Resources.MySqlPort_word
                FormDatabase.Label9.Text = Global.Outworldz.My.Resources.Password_word
                FormDatabase.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormDatabase.StandaloneGroup.Text = Global.Outworldz.My.Resources.Region_Database
                FormDatabase.Text = Global.Outworldz.My.Resources.Database_word
                FormDatabase.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormDatabase.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Region_Database
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RegionDBUsername, Global.Outworldz.My.Resources.Do_NotChange)
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RegionDbName, Global.Outworldz.My.Resources.Do_NotChange)
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RegionMySqlPassword, Global.Outworldz.My.Resources.Do_NotChange)
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RegionServer, Global.Outworldz.My.Resources.Region_ServerName)
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RobustDBPassword, Global.Outworldz.My.Resources.Do_NotChange)
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RobustDBUsername, Global.Outworldz.My.Resources.Do_NotChange)
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RobustDbName, Global.Outworldz.My.Resources.Do_NotChange)
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RobustDbPort, Global.Outworldz.My.Resources.MySQL_Port_Default)
                FormDatabase.ToolTip1.SetToolTip(FormDatabase.RobustServer, Global.Outworldz.My.Resources.Region_ServerName)
            Case "FormDisplacement"
                FormDisplacement.ClearOARToolStripMenuItem.Text = Global.Outworldz.My.Resources.Clear_and_Load_word
                FormDisplacement.ForceTerrainToolStripMenuItem.Text = Global.Outworldz.My.Resources.Load_Terrain
                FormDisplacement.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
                FormDisplacement.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormDisplacement.IgnoreParcelToolStripMenuItem.Text = Global.Outworldz.My.Resources.Ignore_Parcel_word
                FormDisplacement.LoadParcelToolStripMenuItem.Text = Global.Outworldz.My.Resources.Load_Parcel
                FormDisplacement.MergeOARToolStripMenuItem.Text = Global.Outworldz.My.Resources.Merge_OAR_word
                FormDisplacement.MergingToolStripMenuItem.Image = Global.Outworldz.My.Resources.cube_blue
                FormDisplacement.MergingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Merge_Objects_word
                FormDisplacement.OriginalTererainToolStripMenuItem.Text = Global.Outworldz.My.Resources.Ignore_Terrain_word
                FormDisplacement.ParcelsToolStripMenuItem.Image = Global.Outworldz.My.Resources.text_align_justified
                FormDisplacement.ParcelsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Parcels
                FormDisplacement.SetOwnerToolStripMenuItem.Image = Global.Outworldz.My.Resources.user3
                FormDisplacement.SetOwnerToolStripMenuItem.Text = Global.Outworldz.My.Resources.Set_Owner_word
                FormDisplacement.TerrainToolStripMenuItem.Image = Global.Outworldz.My.Resources.Good
                FormDisplacement.TerrainToolStripMenuItem.Text = Global.Outworldz.My.Resources.Terrain_word
                FormDisplacement.ToolStripMenuItem1.Image = Global.Outworldz.My.Resources.package
                FormDisplacement.ToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Options
            Case "FormDiva"
                FormDiva.AccountConfirmationRequired.Text = Global.Outworldz.My.Resources.Confirm
                FormDiva.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.window_environment
                FormDiva.ApacheToolStripMenuItem.Text = Global.Outworldz.My.Resources.Apache_word
                FormDiva.BlackRadioButton.Text = Global.Outworldz.My.Resources.Black_word
                FormDiva.CustomButton1.Text = Global.Outworldz.My.Resources.Custom_word
                FormDiva.GroupBox1.Text = Global.Outworldz.My.Resources.SplashScreen
                FormDiva.GroupBox6.Text = Global.Outworldz.My.Resources.SMTP
                FormDiva.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
                FormDiva.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormDiva.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.about
                FormDiva.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Diva_Panel_word
                FormDiva.Label1.Text = Global.Outworldz.My.Resources.Theme_word
                FormDiva.Label10.Text = Global.Outworldz.My.Resources.Password_word
                FormDiva.Label11.Text = Global.Outworldz.My.Resources.First_name_word
                FormDiva.Label12.Text = Global.Outworldz.My.Resources.Last_Name_Word
                FormDiva.Label14.Text = Global.Outworldz.My.Resources.User_Name_word
                FormDiva.Label17.Text = Global.Outworldz.My.Resources.Notify_Email
                FormDiva.Label18.Text = Global.Outworldz.My.Resources.SMTPPassword_word
                FormDiva.Label19.Text = Global.Outworldz.My.Resources.SplashScreen
                FormDiva.Label2.Text = Global.Outworldz.My.Resources.Friendly
                FormDiva.Label23.Text = Global.Outworldz.My.Resources.SMTPHost_word
                FormDiva.Label24.Text = Global.Outworldz.My.Resources.SMTPPort_word
                FormDiva.Label4.Text = Global.Outworldz.My.Resources.Viewer_Greeting_word
                FormDiva.Text = Global.Outworldz.My.Resources.WebServerPanel
                FormDiva.ToolTip1.SetToolTip(FormDiva.AdminPassword, Global.Outworldz.My.Resources.Password_Text)
                FormDiva.Web.Text = Global.Outworldz.My.Resources.Wifi_interface
                FormDiva.WhiteRadioButton.Text = Global.Outworldz.My.Resources.White_word
                FormDiva.WiFi.Image = Global.Outworldz.My.Resources.about
                FormDiva.WifiEnabled.Text = Global.Outworldz.My.Resources.Diva_Wifi_Enabled_word
            Case "FormFlotsamCache"
                FormFlotsamCache.Button1.Text = Global.Outworldz.My.Resources.Clear_Cache_word
                FormFlotsamCache.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormFlotsamCache.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormFlotsamCache.GroupBox1.Text = Global.Outworldz.My.Resources.Asset_Cache_word
                FormFlotsamCache.Label1.Text = Global.Outworldz.My.Resources.Cache_Directory_word
                FormFlotsamCache.Label2.Text = Global.Outworldz.My.Resources.Log_Level
                FormFlotsamCache.Label4.Text = Global.Outworldz.My.Resources.Cache_Enabled_word
                FormFlotsamCache.Label5.Text = Global.Outworldz.My.Resources.Timeout_in_hours_word
                FormFlotsamCache.Label6.Text = Global.Outworldz.My.Resources.Current_Size '"Current Size on Disk"
                FormFlotsamCache.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormFlotsamCache.PictureBox1.BackgroundImage = Global.Outworldz.My.Resources.folder
                FormFlotsamCache.Text = Global.Outworldz.My.Resources.Asset_Cache_word
                FormFlotsamCache.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormFlotsamCache.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormFlotsamCache.ToolTip1.SetToolTip(FormFlotsamCache.PictureBox1, Global.Outworldz.My.Resources.Click_to_change_the_folder)
            Case "FormFsAssets"
                FormFsAssets.EnableFsAssetsCheckbox.Text = Global.Outworldz.My.Resources.Enable_word
                FormFsAssets.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
                FormFsAssets.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormFsAssets.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.about
                FormFsAssets.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Help_word
                FormFsAssets.Label6.Text = Global.Outworldz.My.Resources.Data_Folder_word
                FormFsAssets.PictureBox1.Image = Global.Outworldz.My.Resources.about
                FormFsAssets.PictureBox2.BackgroundImage = Global.Outworldz.My.Resources.folder
                FormFsAssets.SaveButton.Text = Global.Outworldz.My.Resources.Save_word
                FormFsAssets.ShowStatsCheckBox.Text = Global.Outworldz.My.Resources.Show_Stats
                FormFsAssets.Text = Global.Outworldz.My.Resources.FSassets_Server_word
                FormFsAssets.b.Text = Global.Outworldz.My.Resources.FSassets_Server_word
            Case "FormGloebits"
                FormGloebits.Button4.Text = Global.Outworldz.My.Resources.Resources.Free_Account
                FormGloebits.GLBShowNewSessionAuthIMCheckBox.Text = Global.Outworldz.My.Resources.Resources.GLBShowNewSessionAuthIM_text
                FormGloebits.GLBShowNewSessionPurchaseIMCheckBox.Text = Global.Outworldz.My.Resources.Resources.GLBShowNewSessionPurchaseIM_text
                FormGloebits.GLBShowWelcomeMessageCheckBox.Text = Global.Outworldz.My.Resources.Resources.GLBShowWelcomeMessage_text
                FormGloebits.GloebitsEnabled.Text = Global.Outworldz.My.Resources.Resources.EnableGloebit_word
                FormGloebits.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormGloebits.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormGloebits.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
                FormGloebits.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormGloebits.PictureBox3.Image = Global.Outworldz.My.Resources.Resources.about
                FormGloebits.ProductionButton.Text = Global.Outworldz.My.Resources.Resources.Production_Mode_Word
                FormGloebits.ProductionCreateAppButton.Text = Global.Outworldz.My.Resources.Resources.CreateApp
                FormGloebits.ProductionCreateButton.Text = Global.Outworldz.My.Resources.Resources.Create_Account
                FormGloebits.ProductionReqAppButton.Text = Global.Outworldz.My.Resources.Resources.Request_App
                FormGloebits.SandBoxCreateAppButton.Text = Global.Outworldz.My.Resources.Resources.CreateApp
                FormGloebits.SandBoxReqAppButton.Text = Global.Outworldz.My.Resources.Resources.Request_App
                FormGloebits.SandBoxSignUpButton.Text = Global.Outworldz.My.Resources.Resources.Create_Sandbox_word
                FormGloebits.SandboxButton.Text = Global.Outworldz.My.Resources.Resources.Sandbox_Mode_word
            Case "FormHelp"
                FormHelp.DatabaseHelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.data
                FormHelp.DatabaseHelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Database_Help_word
                FormHelp.DreamgridToolStripMenuItem.Image = Global.Outworldz.My.Resources.cube_blue
                FormHelp.DreamgridToolStripMenuItem.Text = Global.Outworldz.My.Resources.Home_word
                FormHelp.ExitToolStripMenuItem.Image = Global.Outworldz.My.Resources.exit_icon
                FormHelp.ExitToolStripMenuItem.Text = Global.Outworldz.My.Resources.Exit__word
                FormHelp.FileToolStripMenuItem.Text = Global.Outworldz.My.Resources.File_word
                FormHelp.HomeToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormHelp.LoopbackToolStripMenuItem.Image = Global.Outworldz.My.Resources.replace2
                FormHelp.LoopbackToolStripMenuItem.Text = Global.Outworldz.My.Resources.Loopback_Help
                FormHelp.PortsToolStripMenuItem.Image = Global.Outworldz.My.Resources.earth_network
                FormHelp.PortsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Port_Forwarding_Help
                FormHelp.PrintToolStripMenuItem.Text = Global.Outworldz.My.Resources.Print
                FormHelp.PrintToolStripMenuItem1.Image = Global.Outworldz.My.Resources.printer3
                FormHelp.PrintToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Print
                FormHelp.SourceCodeToolStripMenuItem.Image = Global.Outworldz.My.Resources.transform
                FormHelp.SourceCodeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Source_Code_word
                FormHelp.StepbyStepInstallationToolStripMenuItem.Image = Global.Outworldz.My.Resources.document_connection
                FormHelp.StepbyStepInstallationToolStripMenuItem.Text = Global.Outworldz.My.Resources.Starting_up
                FormHelp.TechnicalInfoToolStripMenuItem.Image = Global.Outworldz.My.Resources.gear
                FormHelp.TechnicalInfoToolStripMenuItem.Text = Global.Outworldz.My.Resources.TechInfo
                FormHelp.Text = Global.Outworldz.My.Resources.Help_word
                FormHelp.TroubleshootingToolStripMenuItem.Image = Global.Outworldz.My.Resources.gear_run
                FormHelp.TroubleshootingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Troubleshooting_word
                FormHelp.WebSiteToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormHelp.WebSiteToolStripMenuItem.Text = Global.Outworldz.My.Resources.More_Help
            Case "FormIARSave"
                FormIARSave.Button1.Text = Global.Outworldz.My.Resources.Save_IAR_word
                FormIARSave.Button2.Text = Global.Outworldz.My.Resources.Cancel_word
                FormIARSave.GroupBox1.Text = Global.Outworldz.My.Resources.Save_Inventory_IAR_word '"Save Inventory IAR"
                FormIARSave.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
                FormIARSave.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormIARSave.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.about
                FormIARSave.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Help_word
                FormIARSave.Label1.Text = Global.Outworldz.My.Resources.Object_Path_and_name
                FormIARSave.Label2.Text = Global.Outworldz.My.Resources.Backup_Name
                FormIARSave.Label3.Text = Global.Outworldz.My.Resources.Avatar_Name_word
                FormIARSave.PictureBox1.Image = Global.Outworldz.My.Resources.folder
                FormIARSave.Pwd.Text = Global.Outworldz.My.Resources.Password_word
                FormIARSave.Text = Global.Outworldz.My.Resources.Save_Inventory_IAR_word
                FormIARSave.ToolTip1.SetToolTip(FormIARSave.AviName, Global.Outworldz.My.Resources.Avatar_First_and_Last_Name_word)
                FormIARSave.ToolTip1.SetToolTip(FormIARSave.ObjectNameBox, Global.Outworldz.My.Resources.Enter_Name)
            Case "FormIceCast"
                FormIcecast.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormIcecast.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormIcecast.GroupBox1.Text = Global.Outworldz.My.Resources.IceCast_Server_word
                FormIcecast.Label1.Text = Global.Outworldz.My.Resources.Password_word
                FormIcecast.Label2.Text = Global.Outworldz.My.Resources.Port1
                FormIcecast.Label3.Text = Global.Outworldz.My.Resources.Admin_Password_word
                FormIcecast.Label4.Text = Global.Outworldz.My.Resources.port2
                FormIcecast.LoadURL.Text = Global.Outworldz.My.Resources.Admin_Web_Page_word
                FormIcecast.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormIcecast.PictureBox1.Image = Global.Outworldz.My.Resources.about
                FormIcecast.ShoutcastEnable.Text = Global.Outworldz.My.Resources.Enable_word
                FormIcecast.Text = Global.Outworldz.My.Resources.Icecast_word
                FormIcecast.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormIcecast.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormIcecast.ToolTip1.SetToolTip(FormIcecast, Global.Outworldz.My.Resources.icecast_help)
            Case "FormInitialSetup"
                FormInitialSetup.GroupBox1.Text = Global.Outworldz.My.Resources.Enter_the_Grid_Owner_Information_word
                FormInitialSetup.Label1.Text = Global.Outworldz.My.Resources.First_name_word
                FormInitialSetup.Label2.Text = Global.Outworldz.My.Resources.Last_Name_Word
                FormInitialSetup.Label3.Text = Global.Outworldz.My.Resources.Password_word
                FormInitialSetup.Label5.Text = Global.Outworldz.My.Resources.Repeat_Password_word
                FormInitialSetup.Label6.Text = Global.Outworldz.My.Resources.Email_word
                FormInitialSetup.PictureBox1.Image = Global.Outworldz.My.Resources.document_view
                FormInitialSetup.SaveButton.Text = Global.Outworldz.My.Resources.Save_word
            Case "FormJoomla"
                FormJoomla.AdminButton.Text = Global.Outworldz.My.Resources.Resources.AdministerJoomla_word
                FormJoomla.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Settings_word
                FormJoomla.GroupBox2.Text = Global.Outworldz.My.Resources.Resources.Options
                FormJoomla.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormJoomla.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormJoomla.HypericaRadioButton.Text = Global.Outworldz.My.Resources.Resources.HypericaSearch_word
                FormJoomla.InstallButton.Image = Global.Outworldz.My.Resources.Resources.gear_run
                FormJoomla.InstallButton.Text = Global.Outworldz.My.Resources.Resources.InstallJoomla_word
                FormJoomla.JEnableCheckBox.Text = Global.Outworldz.My.Resources.Resources.EnableJOpensim
                FormJoomla.JOpensimRadioButton.Text = Global.Outworldz.My.Resources.Resources.JOpensimSearch_word
                FormJoomla.ViewButton.Image = Global.Outworldz.My.Resources.Resources.edge
                FormJoomla.ViewButton.Text = Global.Outworldz.My.Resources.Resources.ViewJoomla_word
            Case "FormLogging"
                FormLogging.GroupBox1.Text = Global.Outworldz.My.Resources.Logging_word
                FormLogging.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
                FormLogging.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormLogging.LoggingToolStripMenuItem.Image = Global.Outworldz.My.Resources.window_environment
                FormLogging.LoggingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Logging_word
                FormLogging.RadioAll.Text = Global.Outworldz.My.Resources.All_word
                FormLogging.RadioDebug.Text = Global.Outworldz.My.Resources.Debug_word
                FormLogging.RadioError.Text = Global.Outworldz.My.Resources.Error_word
                FormLogging.RadioFatal.Text = Global.Outworldz.My.Resources.Fatal_word
                FormLogging.RadioInfo.Text = Global.Outworldz.My.Resources.Info_word
                FormLogging.RadioOff.Text = Global.Outworldz.My.Resources.Off
                FormLogging.RadioWarn.Text = Global.Outworldz.My.Resources.Warn_word
                FormLogging.Text = Global.Outworldz.My.Resources.Logging_word
            Case "FormMaps"
                FormMaps.Button2.Text = Global.Outworldz.My.Resources.View_Maps
                FormMaps.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormMaps.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormMaps.GroupBox2.Text = Global.Outworldz.My.Resources.Maps_word
                FormMaps.Label1.Text = Global.Outworldz.My.Resources.Map_Center_Location_word
                FormMaps.Label2.Text = Global.Outworldz.My.Resources.X
                FormMaps.Label3.Text = Global.Outworldz.My.Resources.Y
                FormMaps.Label4.Text = Global.Outworldz.My.Resources.RenderMax
                FormMaps.Label5.Text = Global.Outworldz.My.Resources.RenderMin
                FormMaps.LargeMapButton.Text = Global.Outworldz.My.Resources.LargeMap
                FormMaps.MapBest.Text = Global.Outworldz.My.Resources.Best_Prims
                FormMaps.MapBetter.Text = Global.Outworldz.My.Resources.Better_Prims
                FormMaps.MapBox.Text = Global.Outworldz.My.Resources.Maps_word
                FormMaps.MapGood.Text = Global.Outworldz.My.Resources.Good_Warp3D_word
                FormMaps.MapHelp.Image = Global.Outworldz.My.Resources.about
                FormMaps.MapNone.Text = Global.Outworldz.My.Resources.None
                FormMaps.MapSimple.Text = Global.Outworldz.My.Resources.Simple_but_Fast_word
                FormMaps.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormMaps.SmallMapButton.Text = Global.Outworldz.My.Resources.Small_Map
                FormMaps.Text = Global.Outworldz.My.Resources.Maps_word
                FormMaps.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormMaps.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormMaps.ToolTip1.SetToolTip(FormMaps.Button2, Global.Outworldz.My.Resources.WifiMap)
                FormMaps.ToolTip1.SetToolTip(FormMaps.MapXStart, Global.Outworldz.My.Resources.CenterMap)
                FormMaps.ToolTip1.SetToolTip(FormMaps.MapYStart, Global.Outworldz.My.Resources.CenterMap)
                FormMaps.ToolTip1.SetToolTip(FormMaps.RenderMaxH, Global.Outworldz.My.Resources.Max4096)
                FormMaps.ToolTip1.SetToolTip(FormMaps.ViewMap, Global.Outworldz.My.Resources.Regen_Map)
                FormMaps.ViewMap.Text = Global.Outworldz.My.Resources.DelMaps
            Case "FormOAR"
                FormOAR.MenuStrip2.Text = Global.Outworldz.My.Resources.Resources._0
                FormOAR.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.view1
                FormOAR.RefreshToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.refresh
                FormOAR.RefreshToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Refresh_word
                FormOAR.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormOAR.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
            Case "FormPermissions"
                FormPermissions.AllowGods.Text = Global.Outworldz.My.Resources.Resources.Allow_Or_Disallow_Gods_word
                FormPermissions.Clouds.Text = Global.Outworldz.My.Resources.Resources.Enable_word
                FormPermissions.EnableMaxPrims.Text = Global.Outworldz.My.Resources.Resources.Max_Prims
                FormPermissions.GodHelp.Image = Global.Outworldz.My.Resources.Resources.about
                FormPermissions.GroupBox1.Text = Global.Outworldz.My.Resources.Export_Permission_word '"Export Permission"
                FormPermissions.GroupBox4.Text = Global.Outworldz.My.Resources.Permissions_word '"Permissions"
                FormPermissions.GroupBox7.Text = Global.Outworldz.My.Resources.Clouds_word '"Clouds"
                FormPermissions.LimitsBox.Text = Global.Outworldz.My.Resources.Prim_Limits '"Prim Limits"
                FormPermissions.ManagerGod.Text = Global.Outworldz.My.Resources.Resources.Region_manager_god
                FormPermissions.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormPermissions.OutBoundPermissionsCheckbox.Text = Global.Outworldz.My.Resources.Resources.Allow_Items_to_leave_word
                FormPermissions.RegionGod.Text = Global.Outworldz.My.Resources.Resources.Allow_Region_Owner_Gods_word
                FormPermissions.Text = Global.Outworldz.My.Resources.Permissions_word
                FormPermissions.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormPermissions.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormPermissions.ToolTip1.SetToolTip(FormPermissions.AllowGods, Global.Outworldz.My.Resources.Resources.AllowGodsTooltip)
                FormPermissions.ToolTip1.SetToolTip(FormPermissions.Clouds, Global.Outworldz.My.Resources.Resources.Allow_cloud)
                FormPermissions.ToolTip1.SetToolTip(FormPermissions.EnableMaxPrims, Global.Outworldz.My.Resources.Resources.Max_PrimLimit)
                FormPermissions.ToolTip1.SetToolTip(FormPermissions.GodHelp, Global.Outworldz.My.Resources.Resources.Help_Godmodes)
                FormPermissions.ToolTip1.SetToolTip(FormPermissions.ManagerGod, Global.Outworldz.My.Resources.Resources.Region_Manager_is_God)
                FormPermissions.ToolTip1.SetToolTip(FormPermissions.OutBoundPermissionsCheckbox, Global.Outworldz.My.Resources.Resources.Allow_objects)
                FormPermissions.ToolTip1.SetToolTip(FormPermissions.RegionGod, Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word)
            Case "FormPhysics"
                FormPhysics.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
                FormPhysics.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormPhysics.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Physics_Engine
                FormPhysics.MenuStrip2.Text = Global.Outworldz.My.Resources.Resources._0
                FormPhysics.PhysicsNone.Text = Global.Outworldz.My.Resources.Resources.None
                FormPhysics.PhysicsSeparate.Text = Global.Outworldz.My.Resources.Resources.BP
                FormPhysics.PhysicsubODE.Text = Global.Outworldz.My.Resources.Resources.UBODE_words
                FormPhysics.Text = Global.Outworldz.My.Resources.Resources.Physics_word
                FormPhysics.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormPhysics.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
            Case "FormPorts"
                FormPorts.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
                FormPorts.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormPorts.GroupBox2.Text = Global.Outworldz.My.Resources.Resources.Ports
                FormPorts.Label26.Text = Global.Outworldz.My.Resources.Resources.Region_Port_Start
                FormPorts.Label4.Text = Global.Outworldz.My.Resources.Resources.Http_Port_word
                FormPorts.Label5.Text = Global.Outworldz.My.Resources.Resources.Diagnostics_port_word
                FormPorts.Label7.Text = Global.Outworldz.My.Resources.Resources.Private_Port_Word
                FormPorts.MaxP.Text = Global.Outworldz.My.Resources.Resources.Highest_Used_word
                FormPorts.MaxX.Text = Global.Outworldz.My.Resources.Resources.Highest_Used_word
                FormPorts.MaxXLabel.Text = Global.Outworldz.My.Resources.Resources.XMLRP_start
                FormPorts.MenuStrip2.Text = Global.Outworldz.My.Resources.Resources._0
                FormPorts.OverrideNameLabel.Text = Global.Outworldz.My.Resources.Resources.External
                FormPorts.Text = Global.Outworldz.My.Resources.Resources.Region_Ports_word
                FormPorts.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormPorts.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormPorts.ToolTip1.SetToolTip(FormPorts.DiagnosticPort, Global.Outworldz.My.Resources.Resources.Default_8001_word)
                FormPorts.ToolTip1.SetToolTip(FormPorts.ExternalHostName, Global.Outworldz.My.Resources.Resources.External_text)
                FormPorts.ToolTip1.SetToolTip(FormPorts.FirstRegionPort, Global.Outworldz.My.Resources.Resources.Default_8004_word)
                FormPorts.ToolTip1.SetToolTip(FormPorts.HTTPPort, Global.Outworldz.My.Resources.Resources.Default_8002_word)
                FormPorts.ToolTip1.SetToolTip(FormPorts.OverrideNameLabel, Global.Outworldz.My.Resources.Resources.External_text)
                FormPorts.ToolTip1.SetToolTip(FormPorts.PrivatePort, Global.Outworldz.My.Resources.Resources.Default_8003_word)
                FormPorts.ToolTip1.SetToolTip(FormPorts.uPnPEnabled, Global.Outworldz.My.Resources.Resources.UPnP_Enabled_text)
                FormPorts.Upnp.Image = Global.Outworldz.My.Resources.Resources.about
                FormPorts.uPnPEnabled.Text = Global.Outworldz.My.Resources.Resources.UPnP_Enabled_word
            Case "FormPublicity"
                FormPublicity.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormPublicity.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormPublicity.GDPRCheckBox.Text = Global.Outworldz.My.Resources.Publish_grid
                FormPublicity.GroupBox1.Text = Global.Outworldz.My.Resources.Category_word
                FormPublicity.GroupBox11.Text = Global.Outworldz.My.Resources.Photo_Word
                FormPublicity.GroupBox2.Text = Global.Outworldz.My.Resources.Description_word
                FormPublicity.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormPublicity.PictureBox9.InitialImage = Global.Outworldz.My.Resources.ClicktoInsertPhoto
                FormPublicity.PublicPhoto.Image = Global.Outworldz.My.Resources.about
                FormPublicity.Text = Global.Outworldz.My.Resources.Publicity_Word
                FormPublicity.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
            Case "FormRegion"
                FormRegion.Advanced.Text = Global.Outworldz.My.Resources.Resources.Regions_word
                FormRegion.AllowGods.Text = Global.Outworldz.My.Resources.Resources.Allow_Or_Disallow_Gods_word
                FormRegion.BirdsCheckBox.Text = Global.Outworldz.My.Resources.Resources.Bird_Module_word
                FormRegion.Button1.Text = Global.Outworldz.My.Resources.Resources.Save_word
                FormRegion.DeleteButton.Text = Global.Outworldz.My.Resources.Resources.Delete_word
                FormRegion.DeregisterButton.Text = Global.Outworldz.My.Resources.Resources.Deregister_word
                FormRegion.DisableGBCheckBox.Text = Global.Outworldz.My.Resources.Resources.Disable_Gloebits_word
                FormRegion.DisallowForeigners.Text = Global.Outworldz.My.Resources.Resources.Disable_Foreigners_word
                FormRegion.DisallowResidents.Text = Global.Outworldz.My.Resources.Resources.Disable_Residents
                FormRegion.EnabledCheckBox.Text = Global.Outworldz.My.Resources.Resources.Enabled_word
                FormRegion.GodHelp.Image = Global.Outworldz.My.Resources.Resources.about
                FormRegion.Gods_Use_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
                FormRegion.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Physics_word
                FormRegion.GroupBox2.Text = Global.Outworldz.My.Resources.Resources.Sim_Size_word
                FormRegion.GroupBox3.Text = My.Resources.Publicity_Word
                FormRegion.GroupBox4.Text = Global.Outworldz.My.Resources.Resources.Permissions_word
                FormRegion.GroupBox6.Text = Global.Outworldz.My.Resources.Resources.Region_Specific_Settings_word
                FormRegion.GroupBox7.Text = Global.Outworldz.My.Resources.Resources.Modules_word
                FormRegion.GroupBox8.Text = Global.Outworldz.My.Resources.Resources.Script_Engine_word  '
                FormRegion.Label10.Text = Global.Outworldz.My.Resources.Resources.Clamp_Prim_Size_word
                FormRegion.Label11.Text = Global.Outworldz.My.Resources.Resources.Max_NumPrims
                FormRegion.Label12.Text = Global.Outworldz.My.Resources.Resources.Max_Avatars
                FormRegion.Label13.Text = Global.Outworldz.My.Resources.Resources.Region_Specific_Settings_word
                FormRegion.Label14.Text = Global.Outworldz.My.Resources.Resources.Script_Timer_Rate
                FormRegion.Label15.Text = Global.Outworldz.My.Resources.Resources.FrameRate
                FormRegion.Label16.Text = Global.Outworldz.My.Resources.Resources.Region_Port_word
                FormRegion.Label4.Text = Global.Outworldz.My.Resources.Resources.Maps_X
                FormRegion.Label5.Text = Global.Outworldz.My.Resources.Resources.Nonphysical_Prim
                FormRegion.Label9.Text = Global.Outworldz.My.Resources.Resources.Physical_Prim
                FormRegion.ManagerGod.Text = Global.Outworldz.My.Resources.Resources.EstateManagerIsGod_word
                FormRegion.MapBest.Text = Global.Outworldz.My.Resources.Resources.Best_Prims
                FormRegion.MapBetter.Text = Global.Outworldz.My.Resources.Resources.Better_Prims
                FormRegion.MapBox.Text = Global.Outworldz.My.Resources.Resources.Maps_word
                FormRegion.MapGood.Text = Global.Outworldz.My.Resources.Resources.Good_Warp3D_word
                FormRegion.MapHelp.Image = Global.Outworldz.My.Resources.Resources.about
                FormRegion.MapNone.Text = Global.Outworldz.My.Resources.Resources.None
                FormRegion.MapSimple.Text = Global.Outworldz.My.Resources.Resources.Simple_but_Fast_word
                FormRegion.Maps_Use_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
                FormRegion.MenuStrip2.Text = Global.Outworldz.My.Resources.Resources._0
                FormRegion.NameTip.Text = Global.Outworldz.My.Resources.Resources.AlphaNum
                FormRegion.NoPublish.Text = Global.Outworldz.My.Resources.Resources.No_Publish_Items
                FormRegion.PhysicsSeparate.Text = Global.Outworldz.My.Resources.Resources.BP
                FormRegion.Physics_Default.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
                FormRegion.PhysicsubODE.Text = Global.Outworldz.My.Resources.Resources.UBODE_words
                FormRegion.Publish.Text = Global.Outworldz.My.Resources.Resources.Publish_Items
                FormRegion.PublishDefault.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
                FormRegion.RegionGod.Text = Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word
                FormRegion.ScriptDefaultButton.Text = Global.Outworldz.My.Resources.Resources.Use_Default_word
                FormRegion.SkipAutoCheckBox.Text = Global.Outworldz.My.Resources.Resources.Skip_Autobackup_word
                FormRegion.SmartStartCheckBox.Text = Global.Outworldz.My.Resources.Resources.Smart_Start_word
                FormRegion.TPCheckBox1.Text = Global.Outworldz.My.Resources.Resources.Teleporter_Enable_word
                FormRegion.Text = Global.Outworldz.My.Resources.Resources.Regions_word
                FormRegion.TidesCheckbox.Text = Global.Outworldz.My.Resources.Resources.Tide_Enable
                FormRegion.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormRegion.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormRegion.ToolTip1.SetToolTip(FormRegion.AllowGods, Global.Outworldz.My.Resources.Resources.AllowGodsTooltip)
                FormRegion.ToolTip1.SetToolTip(FormRegion.BirdsCheckBox, Global.Outworldz.My.Resources.Resources.GBoids)
                FormRegion.ToolTip1.SetToolTip(FormRegion.ClampPrimSize, Global.Outworldz.My.Resources.Resources.ClampSize)
                FormRegion.ToolTip1.SetToolTip(FormRegion.CoordX, Global.Outworldz.My.Resources.Resources.Coordx)
                FormRegion.ToolTip1.SetToolTip(FormRegion.CoordY, Global.Outworldz.My.Resources.Resources.CoordY)
                FormRegion.ToolTip1.SetToolTip(FormRegion.DisableGBCheckBox, Global.Outworldz.My.Resources.Resources.Disable_Gloebits_text)
                FormRegion.ToolTip1.SetToolTip(FormRegion.DisallowForeigners, Global.Outworldz.My.Resources.Resources.No_HG)
                FormRegion.ToolTip1.SetToolTip(FormRegion.DisallowResidents, Global.Outworldz.My.Resources.Resources.Only_Owners)
                FormRegion.ToolTip1.SetToolTip(FormRegion.FrametimeBox, Global.Outworldz.My.Resources.Resources.FrameTime)
                FormRegion.ToolTip1.SetToolTip(FormRegion.GodHelp, Global.Outworldz.My.Resources.Resources.Help_word)
                FormRegion.ToolTip1.SetToolTip(FormRegion.GroupBox1, Global.Outworldz.My.Resources.Resources.Sim_Rate)
                FormRegion.ToolTip1.SetToolTip(FormRegion.Label10, Global.Outworldz.My.Resources.Resources.ClampSize)
                FormRegion.ToolTip1.SetToolTip(FormRegion.Label11, Global.Outworldz.My.Resources.Resources.Viewer_Stops_Counting)
                FormRegion.ToolTip1.SetToolTip(FormRegion.Label12, Global.Outworldz.My.Resources.Resources.Max_Agents)
                FormRegion.ToolTip1.SetToolTip(FormRegion.Label14, Global.Outworldz.My.Resources.Resources.Script_Timer_Text)
                FormRegion.ToolTip1.SetToolTip(FormRegion.Label15, Global.Outworldz.My.Resources.Resources.FRText)
                FormRegion.ToolTip1.SetToolTip(FormRegion.Label5, Global.Outworldz.My.Resources.Resources.Max_NonPhys)
                FormRegion.ToolTip1.SetToolTip(FormRegion.Label9, Global.Outworldz.My.Resources.Resources.Max_Phys)
                FormRegion.ToolTip1.SetToolTip(FormRegion.ManagerGod, Global.Outworldz.My.Resources.Resources.EMGod)
                FormRegion.ToolTip1.SetToolTip(FormRegion.MapHelp, Global.Outworldz.My.Resources.Resources.OverridesMap)
                FormRegion.ToolTip1.SetToolTip(FormRegion.MaxAgents, Global.Outworldz.My.Resources.Resources.Max_Agents)
                FormRegion.ToolTip1.SetToolTip(FormRegion.MaxPrims, Global.Outworldz.My.Resources.Resources.Not_Normal)
                FormRegion.ToolTip1.SetToolTip(FormRegion.NonphysicalPrimMax, Global.Outworldz.My.Resources.Resources.Normal_Prim)
                FormRegion.ToolTip1.SetToolTip(FormRegion.PhysicalPrimMax, Global.Outworldz.My.Resources.Resources.Max_Phys)
                FormRegion.ToolTip1.SetToolTip(FormRegion.RegionGod, Global.Outworldz.My.Resources.Resources.Region_Owner_Is_God_word)
                FormRegion.ToolTip1.SetToolTip(FormRegion.RegionName, Global.Outworldz.My.Resources.Resources.Region_Name)
                FormRegion.ToolTip1.SetToolTip(FormRegion.ScriptTimerTextBox, Global.Outworldz.My.Resources.Resources.STComment)
                FormRegion.ToolTip1.SetToolTip(FormRegion.SkipAutoCheckBox, Global.Outworldz.My.Resources.Resources.WillNotSave)
                FormRegion.ToolTip1.SetToolTip(FormRegion.SmartStartCheckBox, Global.Outworldz.My.Resources.Resources.GTide)
                FormRegion.ToolTip1.SetToolTip(FormRegion.TPCheckBox1, Global.Outworldz.My.Resources.Resources.Teleport_Tooltip)
                FormRegion.ToolTip1.SetToolTip(FormRegion.TidesCheckbox, Global.Outworldz.My.Resources.Resources.GTide)
                FormRegion.UUID.Name = Global.Outworldz.My.Resources.UUID
                FormRegion.XEngineButton.Text = Global.Outworldz.My.Resources.Resources.XEngine_word
                FormRegion.YEngineButton.Text = Global.Outworldz.My.Resources.Resources.YEngine_word
            Case "FormRegionList"
                FormRegionlist.AddRegionButton.Text = Global.Outworldz.My.Resources.Resources.Add_word
                FormRegionlist.AllNone.Text = Global.Outworldz.My.Resources.Resources.AllNone_word
                FormRegionlist.AvatarsButton.Text = Global.Outworldz.My.Resources.Resources.Avatars_word
                FormRegionlist.DetailsButton.Text = Global.Outworldz.My.Resources.Resources.Details_word
                FormRegionlist.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormRegionlist.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormRegionlist.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
                FormRegionlist.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormRegionlist.IconsButton.Text = Global.Outworldz.My.Resources.Resources.Icons_word
                FormRegionlist.ImportButton.Text = Global.Outworldz.My.Resources.Resources.Import_word
                FormRegionlist.MapsButton.Text = Global.Outworldz.My.Resources.Resources.Maps_word
                FormRegionlist.RefreshButton.Text = Global.Outworldz.My.Resources.Resources.Refresh1
                FormRegionlist.RestartButton.Text = Global.Outworldz.My.Resources.Resources.Restart_All_word
                FormRegionlist.RunAllButton.Text = Global.Outworldz.My.Resources.Resources.Run_All_word
                FormRegionlist.StopAllButton.Text = Global.Outworldz.My.Resources.Resources.Stop_All_word
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.AddRegionButton, Global.Outworldz.My.Resources.Resources.Add_Region_word)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.AllNone, Global.Outworldz.My.Resources.Resources.Selectallnone)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.AvatarsButton, Global.Outworldz.My.Resources.Resources.ListAvatars)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.DetailsButton, Global.Outworldz.My.Resources.Resources.View_Details)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.IconsButton, Global.Outworldz.My.Resources.Resources.View_as_Icons)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.ImportButton, Global.Outworldz.My.Resources.Resources.Importtext)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.ListView1, Global.Outworldz.My.Resources.Resources.ClickStartStoptxt)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.MapsButton, Global.Outworldz.My.Resources.Resources.View_Maps)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.RefreshButton, Global.Outworldz.My.Resources.Resources.Reload)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.RestartButton, Global.Outworldz.My.Resources.Resources.Restart_All_Checked)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.RunAllButton, Global.Outworldz.My.Resources.Resources.StartAll)
                FormRegionlist.ToolTip1.SetToolTip(FormRegionlist.StopAllButton, Global.Outworldz.My.Resources.Resources.Stopsall)
                FormRegionlist.ToolTip1.ToolTipTitle = Global.Outworldz.My.Resources.Resources.Row
            Case "FormRegionPopup"
                FormRegionPopup.Button1.Image = Global.Outworldz.My.Resources.Resources.document_view1
                FormRegionPopup.Button1.Text = Global.Outworldz.My.Resources.Resources.View_Log_word
                FormRegionPopup.EditButton1.Image = Global.Outworldz.My.Resources.Resources.document_dirty
                FormRegionPopup.EditButton1.Text = Global.Outworldz.My.Resources.Resources.Edit_word
                FormRegionPopup.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.Region_Controls
                FormRegionPopup.RecycleButton2.Image = Global.Outworldz.My.Resources.Resources.recycle
                FormRegionPopup.RecycleButton2.Text = Global.Outworldz.My.Resources.Resources.Restart_word
                FormRegionPopup.ShowConsoleButton.Image = Global.Outworldz.My.Resources.Resources.document_view1
                FormRegionPopup.ShowConsoleButton.Text = Global.Outworldz.My.Resources.Resources.View_Console_word
                FormRegionPopup.StartButton3.Image = Global.Outworldz.My.Resources.Resources.media_play
                FormRegionPopup.StartButton3.Text = Global.Outworldz.My.Resources.Resources.Start_word
                FormRegionPopup.StatsButton.Image = Global.Outworldz.My.Resources.Resources.user1_into
                FormRegionPopup.StatsButton.Text = Global.Outworldz.My.Resources.Resources.Teleport_word
                FormRegionPopup.StatsButton1.Image = Global.Outworldz.My.Resources.Resources.user1_into
                FormRegionPopup.StatsButton1.Text = Global.Outworldz.My.Resources.Resources.View_Statistics_Word
                FormRegionPopup.StopButton1.Image = Global.Outworldz.My.Resources.Resources.media_stop_red1
                FormRegionPopup.StopButton1.Text = Global.Outworldz.My.Resources.Resources.Stop_word
                FormRegionPopup.ViewMapButton.Image = Global.Outworldz.My.Resources.Resources.document_view1
                FormRegionPopup.ViewMapButton.Text = Global.Outworldz.My.Resources.Resources.View_Map_word
            Case "FormRegions"
                FormRegions.AddRegion.Text = Global.Outworldz.My.Resources.Add_Region_word
                FormRegions.Button1.Text = Global.Outworldz.My.Resources.ClearReg
                FormRegions.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormRegions.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormRegions.GroupBox2.Text = Global.Outworldz.My.Resources.Region_word
                FormRegions.Label1.Text = Global.Outworldz.My.Resources.EditRegion
                FormRegions.Label2.Text = Global.Outworldz.My.Resources.New_User_Home
                FormRegions.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormRegions.NormalizeButton1.Text = Global.Outworldz.My.Resources.NormalizeRegions
                FormRegions.RegionBox.Items.AddRange(New Object() {Global.Outworldz.My.Resources.Choose_Region_word})
                FormRegions.RegionButton.Text = Global.Outworldz.My.Resources.Configger
                FormRegions.RegionHelp.Image = Global.Outworldz.My.Resources.about
                FormRegions.SmartStartEnabled.Text = Global.Outworldz.My.Resources.Smart_Start_Enable_word
                FormRegions.Text = Global.Outworldz.My.Resources.Region_word
                FormRegions.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormRegions.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormRegions.WelcomeRegion.Text = Global.Outworldz.My.Resources.Default_Region_word
                FormRegions.X.Name = Global.Outworldz.My.Resources.X
                FormRegions.Y.Name = Global.Outworldz.My.Resources.Y
                FormRegions.Z.Name = Global.Outworldz.My.Resources.Z
            Case "FormRestart"
                FormRestart.ARTimerBox.Text = Global.Outworldz.My.Resources.Restart_Periodically_word
                FormRestart.AutoStart.Text = Global.Outworldz.My.Resources.Auto_Startup_word
                FormRestart.AutoStartCheckbox.Text = Global.Outworldz.My.Resources.EnableOneClickStart_word
                FormRestart.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormRestart.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormRestart.Label25.Text = Global.Outworldz.My.Resources.Restart_Interval
                FormRestart.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormRestart.RestartOnCrash.Text = Global.Outworldz.My.Resources.Restart_On_Crash
                FormRestart.RunOnBoot.Image = Global.Outworldz.My.Resources.about
                FormRestart.SequentialCheckBox1.Text = Global.Outworldz.My.Resources.StartSequentially
                FormRestart.Text = Global.Outworldz.My.Resources.Restart_word
                FormRestart.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormRestart.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormRestart.ToolTip1.SetToolTip(FormRestart.ARTimerBox, Global.Outworldz.My.Resources.Restart_Periodically_Minutes)
                FormRestart.ToolTip1.SetToolTip(FormRestart.AutoRestartBox, Global.Outworldz.My.Resources.AutorestartBox)
                FormRestart.ToolTip1.SetToolTip(FormRestart.AutoStartCheckbox, Global.Outworldz.My.Resources.StartLaunch)
                FormRestart.ToolTip1.SetToolTip(FormRestart.RestartOnCrash, Global.Outworldz.My.Resources.Restart_On_Crash)
                FormRestart.ToolTip1.SetToolTip(FormRestart.SequentialCheckBox1, Global.Outworldz.My.Resources.Sequentially_text)
            Case "FormScripts"
                FormScripts.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormScripts.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormScripts.GroupBox1.Text = Global.Outworldz.My.Resources.Script_Engine_word '"Script Engine"
                FormScripts.GroupBox8.Text = Global.Outworldz.My.Resources.Allow_LSL
                FormScripts.LSLCheckbox.Text = Global.Outworldz.My.Resources.Enable_word
                FormScripts.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormScripts.Text = Global.Outworldz.My.Resources.Scripts_word
                FormScripts.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormScripts.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormScripts.XengineButton.Text = Global.Outworldz.My.Resources.XEngine_word
                FormScripts.YengineButton.Text = Global.Outworldz.My.Resources.YEngine_word
            Case "FormServerType"
                FormServerType.GridRegionButton.Text = Global.Outworldz.My.Resources.Region_Server_word
                FormServerType.GridServerButton.Text = Global.Outworldz.My.Resources.Grid_Server_With_Robust_word
                FormServerType.GroupBox1.Text = Global.Outworldz.My.Resources.Server_Type_word
                FormServerType.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
                FormServerType.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormServerType.MetroRadioButton2.Text = Global.Outworldz.My.Resources.MetroOrg
                FormServerType.SaveButton.Text = Global.Outworldz.My.Resources.Save_word
                FormServerType.ServerTypeToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormServerType.ServerTypeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Server_Type_word
                FormServerType.Text = Global.Outworldz.My.Resources.Server_Type_word
                FormServerType.osGridRadioButton1.Text = Global.Outworldz.My.Resources.OSGrid_Region_Server
            Case "FormSettings"
                FormSettings.ApacheButton.Text = Global.Outworldz.My.Resources.Resources.Apache_Webserver
                FormSettings.BackupButton1.Text = Global.Outworldz.My.Resources.Resources.Backup_Settings_word
                FormSettings.BanListButton.Text = Global.Outworldz.My.Resources.Resources.Ban_List_word
                FormSettings.Birds.Text = Global.Outworldz.My.Resources.Resources.Bird_Settings_word
                FormSettings.Button1.Text = Global.Outworldz.My.Resources.Resources.Server_Type_word
                FormSettings.Button2.Text = Global.Outworldz.My.Resources.Resources.Permissions_word
                FormSettings.Button3.Text = Global.Outworldz.My.Resources.Resources.Restart_Settings_word
                FormSettings.Button4.Text = Global.Outworldz.My.Resources.Resources.Publicity_Word
                FormSettings.CacheButton1.Text = Global.Outworldz.My.Resources.Resources.Caches_word
                FormSettings.DNSButton.Text = Global.Outworldz.My.Resources.Resources.Hypergrid
                FormSettings.DatabaseButton2.Text = Global.Outworldz.My.Resources.Resources.Database_Setup_word
                FormSettings.DivaButton1.Text = Global.Outworldz.My.Resources.Resources.Web
                FormSettings.GloebitsButton.Text = Global.Outworldz.My.Resources.Resources.Currency_word
                FormSettings.LoggingButton.Text = Global.Outworldz.My.Resources.Resources.Logging_word
                FormSettings.MapsButton.Text = Global.Outworldz.My.Resources.Resources.Maps_word
                FormSettings.PhysicsButton1.Text = Global.Outworldz.My.Resources.Resources.Physics_word
                FormSettings.PortsButton1.Text = Global.Outworldz.My.Resources.Resources.Network_Ports
                FormSettings.RegionsButton1.Text = Global.Outworldz.My.Resources.Resources.Regions_word
                FormSettings.ScriptButton.Text = Global.Outworldz.My.Resources.Resources.Scripts_word
                FormSettings.Shoutcast.Text = Global.Outworldz.My.Resources.Resources.Icecast_word
                FormSettings.TOSButton.Text = Global.Outworldz.My.Resources.Resources.Terms_of_Service
                FormSettings.TideButton.Text = Global.Outworldz.My.Resources.Resources.Tides_word
                FormSettings.ToolTip1.SetToolTip(FormSettings.ApacheButton, Global.Outworldz.My.Resources.Resources.ApacheWebServer)
                FormSettings.ToolTip1.SetToolTip(FormSettings.BackupButton1, Global.Outworldz.My.Resources.Resources.Backup_Schedule)
                FormSettings.ToolTip1.SetToolTip(FormSettings.BanListButton, Global.Outworldz.My.Resources.Resources.BanList_string)
                FormSettings.ToolTip1.SetToolTip(FormSettings.Birds, Global.Outworldz.My.Resources.Resources.Click_Birds)
                FormSettings.ToolTip1.SetToolTip(FormSettings.Button1, Global.Outworldz.My.Resources.Resources.Click_Server)
                FormSettings.ToolTip1.SetToolTip(FormSettings.Button2, Global.Outworldz.My.Resources.Resources.Click_for_God_Mode)
                FormSettings.ToolTip1.SetToolTip(FormSettings.Button3, Global.Outworldz.My.Resources.Resources.Click_Restart)
                FormSettings.ToolTip1.SetToolTip(FormSettings.Button4, Global.Outworldz.My.Resources.Resources.Click_Publicity)
                FormSettings.ToolTip1.SetToolTip(FormSettings.Button5, Global.Outworldz.My.Resources.Resources.Click_Setup)
                FormSettings.ToolTip1.SetToolTip(FormSettings.CacheButton1, Global.Outworldz.My.Resources.Resources.Click_Caches)
                FormSettings.ToolTip1.SetToolTip(FormSettings.DNSButton, Global.Outworldz.My.Resources.Resources.Click_HG)
                FormSettings.ToolTip1.SetToolTip(FormSettings.DatabaseButton2, Global.Outworldz.My.Resources.Resources.Click_Database)
                FormSettings.ToolTip1.SetToolTip(FormSettings.DivaButton1, Global.Outworldz.My.Resources.Resources.Click_Web)
                FormSettings.ToolTip1.SetToolTip(FormSettings.GloebitsButton, Global.Outworldz.My.Resources.Resources.Click_Currency)
                FormSettings.ToolTip1.SetToolTip(FormSettings.LoggingButton, Global.Outworldz.My.Resources.Resources.Log_Level)
                FormSettings.ToolTip1.SetToolTip(FormSettings.MapsButton, Global.Outworldz.My.Resources.Resources.Click_Maps)
                FormSettings.ToolTip1.SetToolTip(FormSettings.PhysicsButton1, Global.Outworldz.My.Resources.Resources.Click_Physics)
                FormSettings.ToolTip1.SetToolTip(FormSettings.PortsButton1, Global.Outworldz.My.Resources.Resources.Click_Ports)
                FormSettings.ToolTip1.SetToolTip(FormSettings.RegionsButton1, Global.Outworldz.My.Resources.Resources.Click_Regions)
                FormSettings.ToolTip1.SetToolTip(FormSettings.ScriptButton, Global.Outworldz.My.Resources.Resources.Click_to_View_this_word)
                FormSettings.ToolTip1.SetToolTip(FormSettings.Shoutcast, Global.Outworldz.My.Resources.Resources.Click_Icecast)
                FormSettings.ToolTip1.SetToolTip(FormSettings.TOSButton, Global.Outworldz.My.Resources.Resources.Setup_TOS)
                FormSettings.ToolTip1.SetToolTip(FormSettings.TideButton, Global.Outworldz.My.Resources.Resources.Click_Tides)
                FormSettings.ToolTip1.SetToolTip(FormSettings.VoiceButton1, Global.Outworldz.My.Resources.Resources.Click_Voice)
                FormSettings.VoiceButton1.Text = Global.Outworldz.My.Resources.Resources.Vivox_Voice_word
            Case "FormSetup"
                FormSetup.AddUserToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Add_User_word
                FormSetup.AdvancedSettingsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.earth_network
                FormSetup.AdvancedSettingsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Settings_word
                FormSetup.AdvancedSettingsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.All_Global_Settings_word
                FormSetup.All.Text = Global.Outworldz.My.Resources.Resources.All_word

                FormSetup.AllUsersAllSimsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.All_Users_All_Sims_word
                FormSetup.ArabicToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_saudi_arabia1
                FormSetup.BackupCriticalFilesToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
                FormSetup.BackupCriticalFilesToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.System_Backup_word
                FormSetup.BackupDatabaseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
                FormSetup.BackupDatabaseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Backup_Databases
                FormSetup.BackupRestoreToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disk_blue
                FormSetup.BackupRestoreToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.SQL_Database_Backup_Restore
                FormSetup.BasqueToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.basque
                FormSetup.BasqueToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Basque_word
                FormSetup.BrazilToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_brazil
                FormSetup.BusyButton.Text = Global.Outworldz.My.Resources.Resources.Busy_word
                FormSetup.CHeckForUpdatesToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
                FormSetup.CHeckForUpdatesToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Check_for_Updates_word
                FormSetup.CatalanToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_catalan
                FormSetup.CatalanToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Catalan
                FormSetup.ChangePasswordToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Change_Password_word
                FormSetup.ChartWrapper1.AxisXTitle = Global.Outworldz.My.Resources.Resources.Minutes_word
                FormSetup.ChartWrapper2.AxisXTitle = Global.Outworldz.My.Resources.Resources.Minutes_word
                FormSetup.CheckAndRepairDatbaseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Server_Client
                FormSetup.CheckAndRepairDatbaseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Check_and_Repair_Database_word
                FormSetup.ChineseSimplifedToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_china
                FormSetup.ChineseSimplifedToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Chinese_Simplifed
                FormSetup.ChineseTraditionalToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_taiwan
                FormSetup.ChineseTraditionalToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Chinese_Traditional
                FormSetup.ClothingInventoryToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.user1_into
                FormSetup.ClothingInventoryToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Load_Free_Avatar_Inventory_word
                FormSetup.ClothingInventoryToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Load_Free_Avatar_Inventory_text
                FormSetup.CommonConsoleCommandsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.text_marked
                FormSetup.CommonConsoleCommandsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Issue_Commands
                FormSetup.ConsoleCOmmandsToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.text_marked
                FormSetup.ConsoleCOmmandsToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_Console
                FormSetup.ConsoleCOmmandsToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_Console_text
                FormSetup.ConsoleToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.window_add
                FormSetup.ConsoleToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Consoles_word
                FormSetup.ConsoleToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Resources.Consoletext
                FormSetup.CzechToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_czech_republic
                FormSetup.CzechToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Czech
                FormSetup.Debug.Text = Global.Outworldz.My.Resources.Resources.Debug_word
                FormSetup.DebugToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Set_Debug_Level_word
                FormSetup.DiagnosticsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Server_Client
                FormSetup.DiagnosticsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Network_Diagnostics
                FormSetup.DiagnosticsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Network_Diagnostics_text
                FormSetup.DutchToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_netherlands
                FormSetup.DutchToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Dutch
                FormSetup.EnglishToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_usa
                FormSetup.EnglishToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.English
                FormSetup.ErrorToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Error_word
                FormSetup.FarsiToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_iran
                FormSetup.Fatal1.Text = Global.Outworldz.My.Resources.Resources.Fatal_word
                FormSetup.FileToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.File_word
                FormSetup.FinnishToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_finland
                FormSetup.FinnishToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Finnish
                FormSetup.FrenchToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_france
                FormSetup.FrenchToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.French
                FormSetup.GermanToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_germany
                FormSetup.GermanToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.German
                FormSetup.GreekToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_greece
                FormSetup.GreekToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Greek
                FormSetup.HebrewToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_israel
                FormSetup.HebrewToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Hebrew
                FormSetup.HelpOnIARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
                FormSetup.HelpOnIARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_On_IARS_word
                FormSetup.HelpOnIARSToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_IARS_text
                FormSetup.HelpOnOARsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.disks
                FormSetup.HelpOnOARsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_OARS
                FormSetup.HelpOnOARsToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_OARS_text
                FormSetup.HelpOnSettingsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.gear
                FormSetup.HelpOnSettingsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_Manuals_word
                FormSetup.HelpStartingUpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.box_tall
                FormSetup.HelpStartingUpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_Startup
                FormSetup.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormSetup.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormSetup.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormSetup.HelpToolStripMenuItem2.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormSetup.HelpToolStripMenuItem2.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormSetup.HelpToolStripMenuItem3.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormSetup.HelpToolStripMenuItem3.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormSetup.HelpToolStripMenuItem4.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormSetup.HelpToolStripMenuItem4.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormSetup.IcelandicToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_iceland
                FormSetup.IcelandicToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Icelandic
                FormSetup.Info.Text = Global.Outworldz.My.Resources.Resources.Info_word
                FormSetup.IrishToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_ireland
                FormSetup.IrishToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Irish
                FormSetup.IslandToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_tall
                FormSetup.IslandToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Load_Free_DreamGrid_OARs_word
                FormSetup.JobEngineToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.JobEngine_word
                FormSetup.JustOneRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Just_one_region_word
                FormSetup.JustQuitToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flash
                FormSetup.JustQuitToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Quit_Now_Word
                FormSetup.LanguageToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.users3
                FormSetup.LanguageToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Language

                FormSetup.LoadIARsToolMenuItem.Image = Global.Outworldz.My.Resources.Resources.user1_into
                FormSetup.LoadIARsToolMenuItem.Text = Global.Outworldz.My.Resources.Resources.Inventory_IAR_Load_and_Save_words
                FormSetup.LoadLocalOARSToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.box_tall
                FormSetup.LoadLocalOARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Load_Local_OARs_word

                FormSetup.LoopBackToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.refresh
                FormSetup.LoopBackToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_On_LoopBack_word
                FormSetup.LoopBackToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_Loopback_Text
                FormSetup.MnuContent.Text = Global.Outworldz.My.Resources.Resources.Content_word
                FormSetup.MoreFreeIslandsandPartsContentToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
                FormSetup.MoreFreeIslandsandPartsContentToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.More_Free_Islands_and_Parts_word
                FormSetup.MoreFreeIslandsandPartsContentToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Free_DLC_word
                FormSetup.MysqlToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.gear_run
                FormSetup.MysqlToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Mysql_Word
                FormSetup.NorwegianToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_norway
                FormSetup.NorwegianToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Norwegian

                FormSetup.Off1.Text = Global.Outworldz.My.Resources.Resources.Off
                FormSetup.PDFManualToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.pdf
                FormSetup.PDFManualToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.PDF_Manual_word
                FormSetup.PolishToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_poland
                FormSetup.PolishToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Polish
                FormSetup.PortgueseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_portugal
                FormSetup.PortgueseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Portuguese
                FormSetup.RegionsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Server_Client
                FormSetup.RegionsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Regions_word
                FormSetup.RestartApacheItem.Image = Global.Outworldz.My.Resources.Resources.gear_run
                FormSetup.RestartApacheItem.Text = Global.Outworldz.My.Resources.Resources.Apache_word
                FormSetup.RestartIceCastItem2.Image = Global.Outworldz.My.Resources.Resources.recycle
                FormSetup.RestartIceCastItem2.Text = Global.Outworldz.My.Resources.Resources.Restart_word
                FormSetup.RestartIcecastItem.Image = Global.Outworldz.My.Resources.Resources.gear_run
                FormSetup.RestartIcecastItem.Text = Global.Outworldz.My.Resources.Resources.Icecast_word
                FormSetup.RestartMysqlItem.Image = Global.Outworldz.My.Resources.Resources.recycle
                FormSetup.RestartMysqlItem.Text = Global.Outworldz.My.Resources.Resources.Restart_word
                FormSetup.RestartOneRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Restart_one_region_word
                FormSetup.RestartRegionToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Restart_Region_word
                FormSetup.RestartRobustItem.Image = Global.Outworldz.My.Resources.Resources.recycle
                FormSetup.RestartRobustItem.Text = Global.Outworldz.My.Resources.Resources.Restart_word
                FormSetup.RestartTheInstanceToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Restart_one_instance_word
                FormSetup.RestartToolStripMenuItem2.Image = Global.Outworldz.My.Resources.Resources.recycle
                FormSetup.RestartToolStripMenuItem2.Text = Global.Outworldz.My.Resources.Resources.Restart_word
                FormSetup.RestoreDatabaseToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.cube_blue
                FormSetup.RestoreDatabaseToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Restore_Database_word
                FormSetup.RevisionHistoryToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_dirty
                FormSetup.RevisionHistoryToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Revision_History_word
                FormSetup.RobustToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.gear_run
                FormSetup.RobustToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Robust_word
                FormSetup.RussianToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_russia1
                FormSetup.RussianToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Russian
                FormSetup.ScriptsResumeToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_Resume_word
                FormSetup.ScriptsStartToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_Start_word
                FormSetup.ScriptsStopToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_Stop_word
                FormSetup.ScriptsSuspendToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_Suspend_word
                FormSetup.ScriptsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Scripts_word
                FormSetup.SeePortsInUseToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.server_connection
                FormSetup.SeePortsInUseToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.See_Ports_In_Use_word
                FormSetup.SendAlertToAllUsersToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Send_Alert_Message_word
                FormSetup.ShowHyperGridAddressToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
                FormSetup.ShowHyperGridAddressToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Show_Grid_Address
                FormSetup.ShowHyperGridAddressToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Grid_Address_text
                FormSetup.ShowStatusToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Show_Status_word
                FormSetup.ShowUserDetailsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Show_User_Details_word
                FormSetup.SimulatorStatsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
                FormSetup.SimulatorStatsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.View_Simulator_Stats
                FormSetup.SpanishToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_spain
                FormSetup.SpanishToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Spanish
                FormSetup.StartButton.Text = Global.Outworldz.My.Resources.Resources.Start_word
                FormSetup.StopButton.Text = Global.Outworldz.My.Resources.Resources.Stop_word
                FormSetup.SwedishToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.flag_sweden
                FormSetup.SwedishToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Swedish
                FormSetup.TechnicalInfoToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_dirty
                FormSetup.TechnicalInfoToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_Technical
                FormSetup.TechnicalInfoToolStripMenuItem.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_Technical_text
                FormSetup.ThreadpoolsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Thread_pools_word
                FormSetup.ToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.document_connection
                FormSetup.ToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help_Forward
                FormSetup.ToolStripMenuItem1.ToolTipText = Global.Outworldz.My.Resources.Resources.Help_Forward_text
                FormSetup.TroubleshootingToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_view
                FormSetup.TroubleshootingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_Troubleshooting_word
                FormSetup.UsersToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Users_word
                FormSetup.ViewIcecastWebPageToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.cube_blue
                FormSetup.ViewIcecastWebPageToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.View_Icecast
                FormSetup.ViewLogsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.document_view
                FormSetup.ViewLogsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.View_Logs
                FormSetup.ViewRegionMapToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Good
                FormSetup.ViewRegionMapToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.View_Maps
                FormSetup.ViewWebUI.Image = Global.Outworldz.My.Resources.Resources.document_view
                FormSetup.ViewWebUI.Text = Global.Outworldz.My.Resources.Resources.View_Web_Interface
                FormSetup.ViewWebUI.ToolTipText = Global.Outworldz.My.Resources.Resources.View_Web_Interface_text
                FormSetup.Warn.Text = Global.Outworldz.My.Resources.Resources.Warn_word
                FormSetup.XengineToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.XEngine_word
                FormSetup.mnuAbout.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormSetup.mnuAbout.Text = Global.Outworldz.My.Resources.Resources.About_word
                FormSetup.mnuExit.Image = Global.Outworldz.My.Resources.Resources.exit_icon
                FormSetup.mnuExit.Text = Global.Outworldz.My.Resources.Resources.Exit__word
                FormSetup.mnuHide.Image = Global.Outworldz.My.Resources.Resources.navigate_down
                FormSetup.mnuHide.Text = Global.Outworldz.My.Resources.Resources.Hide
                FormSetup.mnuHideAllways.Image = Global.Outworldz.My.Resources.Resources.navigate_down2
                FormSetup.mnuHideAllways.Text = Global.Outworldz.My.Resources.Resources.Hide_Allways_word
                FormSetup.mnuSettings.Text = Global.Outworldz.My.Resources.Resources.Setup_word
                FormSetup.mnuShow.Image = Global.Outworldz.My.Resources.Resources.navigate_up
                FormSetup.mnuShow.Text = Global.Outworldz.My.Resources.Resources.Show_word

                ' OAR AND IAR MENU
                FormSetup.SearchForObjectsMenuItem.Text = Global.Outworldz.My.Resources.Search_Events
                FormSetup.SearchForGridsMenuItem.Text = Global.Outworldz.My.Resources.Search_grids
                FormSetup.LoadInventoryIARToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Load_Inventory_IAR
                FormSetup.SaveAllRunningRegiondsAsOARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Save_All_Regions
                FormSetup.LoadRegionOARToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Load_Region_OAR
                FormSetup.LoadLocalOARSToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.OAR_load_save_backupp_word
                FormSetup.SaveInventoryIARToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Save_Inventory_IAR_word
                FormSetup.SaveRegionOARToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Save_Region_OAR_word

            Case "FormTide"
                FormTide.BroadcastTideInfo.Text = Global.Outworldz.My.Resources.Resources.Broadcast_Tide_Info
                FormTide.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
                FormTide.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormTide.RunOnBoot.Image = Global.Outworldz.My.Resources.Resources.about
                FormTide.TideEnabledCheckbox.Text = Global.Outworldz.My.Resources.Resources.Enable_word
                FormTide.TideInfoDebugCheckBox.Text = Global.Outworldz.My.Resources.Resources.Send_Debug_Info
                FormTide.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
                FormTide.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
                FormTide.ToolTip1.SetToolTip(FormTide.BroadcastTideInfo, Global.Outworldz.My.Resources.Resources.Broadcast_Tide_Chat)
                FormTide.ToolTip1.SetToolTip(FormTide.CycleTimeTextBox, Global.Outworldz.My.Resources.Resources.Cycle_time_text)
                FormTide.ToolTip1.SetToolTip(FormTide.TideHighLevelTextBox, Global.Outworldz.My.Resources.Resources.High_Water_Level_text)
                FormTide.ToolTip1.SetToolTip(FormTide.TideInfoDebugCheckBox, Global.Outworldz.My.Resources.Resources.Provide_Info)
                FormTide.ToolTip1.SetToolTip(FormTide.TideLowLevelTextBox, Global.Outworldz.My.Resources.Resources.Low_High)
                FormTide.ToolTip1.ToolTipTitle = Global.Outworldz.My.Resources.Resources.Tide_Enable
            Case "FormVoice"
                FormVoice.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
                FormVoice.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
                FormVoice.GroupBox1.Text = Global.Outworldz.My.Resources.Setup_Voice_Service
                FormVoice.Label1.Text = Global.Outworldz.My.Resources.User_ID_word
                FormVoice.Label2.Text = Global.Outworldz.My.Resources.Password_word
                FormVoice.MenuStrip2.Text = Global.Outworldz.My.Resources._0
                FormVoice.RequestPassword.Text = Global.Outworldz.My.Resources.Click_to_Request_Voice_Service
                FormVoice.RunOnBoot.Image = Global.Outworldz.My.Resources.about
                FormVoice.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
                FormVoice.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
                FormVoice.VivoxEnable.Text = Global.Outworldz.My.Resources.Enable_word
            Case "Resource1"

        End Select

    End Sub

End Module
