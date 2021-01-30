<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRegionlist
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            colsize.Dispose()

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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRegionlist))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.RefreshButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AddRegionButton = New System.Windows.Forms.Button()
        Me.AllNone = New System.Windows.Forms.CheckBox()
        Me.RunAllButton = New System.Windows.Forms.Button()
        Me.StopAllButton = New System.Windows.Forms.Button()
        Me.RestartButton = New System.Windows.Forms.Button()
        Me.DetailsButton = New System.Windows.Forms.Button()
        Me.IconsButton = New System.Windows.Forms.Button()
        Me.AvatarsButton = New System.Windows.Forms.Button()
        Me.ImportButton = New System.Windows.Forms.Button()
        Me.AvatarView = New System.Windows.Forms.ListView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KOT = New System.Windows.Forms.ToolStripMenuItem()
        Me.OnTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FloatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListView1.Location = New System.Drawing.Point(30, 136)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.ShowItemToolTips = True
        Me.ListView1.Size = New System.Drawing.Size(1167, 326)
        Me.ListView1.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ListView1, Global.Outworldz.My.Resources.Resources.ClickStartStoptxt)
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'RefreshButton
        '
        Me.RefreshButton.AutoSize = True
        Me.RefreshButton.Image = Global.Outworldz.My.Resources.Resources.refresh
        Me.RefreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RefreshButton.Location = New System.Drawing.Point(190, 5)
        Me.RefreshButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.RefreshButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Padding = New System.Windows.Forms.Padding(3)
        Me.RefreshButton.Size = New System.Drawing.Size(175, 45)
        Me.RefreshButton.TabIndex = 1
        Me.RefreshButton.Text = Global.Outworldz.My.Resources.Resources.Refresh_word
        Me.ToolTip1.SetToolTip(Me.RefreshButton, Global.Outworldz.My.Resources.Resources.Reload)
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = Global.Outworldz.My.Resources.Resources.Row
        '
        'AddRegionButton
        '
        Me.AddRegionButton.AutoSize = True
        Me.AddRegionButton.Image = Global.Outworldz.My.Resources.Resources.package_add
        Me.AddRegionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddRegionButton.Location = New System.Drawing.Point(5, 60)
        Me.AddRegionButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.AddRegionButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.AddRegionButton.Name = "AddRegionButton"
        Me.AddRegionButton.Padding = New System.Windows.Forms.Padding(3)
        Me.AddRegionButton.Size = New System.Drawing.Size(175, 45)
        Me.AddRegionButton.TabIndex = 18593
        Me.AddRegionButton.Text = Global.Outworldz.My.Resources.Resources.Add_word
        Me.ToolTip1.SetToolTip(Me.AddRegionButton, Global.Outworldz.My.Resources.Resources.Add_Region_word)
        Me.AddRegionButton.UseVisualStyleBackColor = True
        '
        'AllNone
        '
        Me.AllNone.AutoSize = True
        Me.AllNone.Location = New System.Drawing.Point(128, 69)
        Me.AllNone.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.AllNone.Name = "AllNone"
        Me.AllNone.Size = New System.Drawing.Size(113, 29)
        Me.AllNone.TabIndex = 4
        Me.AllNone.Text = Global.Outworldz.My.Resources.Resources.AllNone_word
        Me.ToolTip1.SetToolTip(Me.AllNone, Global.Outworldz.My.Resources.Resources.Selectallnone)
        Me.AllNone.UseVisualStyleBackColor = True
        '
        'RunAllButton
        '
        Me.RunAllButton.AutoSize = True
        Me.RunAllButton.Image = Global.Outworldz.My.Resources.Resources.media_play
        Me.RunAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RunAllButton.Location = New System.Drawing.Point(190, 60)
        Me.RunAllButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.RunAllButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.RunAllButton.Name = "RunAllButton"
        Me.RunAllButton.Padding = New System.Windows.Forms.Padding(3)
        Me.RunAllButton.Size = New System.Drawing.Size(175, 45)
        Me.RunAllButton.TabIndex = 18594
        Me.RunAllButton.Text = Global.Outworldz.My.Resources.Resources.Run_All_word
        Me.ToolTip1.SetToolTip(Me.RunAllButton, Global.Outworldz.My.Resources.Resources.StartAll)
        Me.RunAllButton.UseVisualStyleBackColor = True
        '
        'StopAllButton
        '
        Me.StopAllButton.AutoSize = True
        Me.StopAllButton.Image = Global.Outworldz.My.Resources.Resources.media_stop
        Me.StopAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StopAllButton.Location = New System.Drawing.Point(375, 60)
        Me.StopAllButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.StopAllButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.StopAllButton.Name = "StopAllButton"
        Me.StopAllButton.Padding = New System.Windows.Forms.Padding(3)
        Me.StopAllButton.Size = New System.Drawing.Size(175, 45)
        Me.StopAllButton.TabIndex = 18595
        Me.StopAllButton.Text = Global.Outworldz.My.Resources.Resources.Stop_All_word
        Me.ToolTip1.SetToolTip(Me.StopAllButton, Global.Outworldz.My.Resources.Resources.Stopsall)
        Me.StopAllButton.UseVisualStyleBackColor = True
        '
        'RestartButton
        '
        Me.RestartButton.AutoSize = True
        Me.RestartButton.Image = Global.Outworldz.My.Resources.Resources.replace2
        Me.RestartButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RestartButton.Location = New System.Drawing.Point(560, 60)
        Me.RestartButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.RestartButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.RestartButton.Name = "RestartButton"
        Me.RestartButton.Padding = New System.Windows.Forms.Padding(3)
        Me.RestartButton.Size = New System.Drawing.Size(175, 45)
        Me.RestartButton.TabIndex = 18596
        Me.RestartButton.Text = "Restart"
        Me.ToolTip1.SetToolTip(Me.RestartButton, Global.Outworldz.My.Resources.Resources.Restart_All_Checked)
        Me.RestartButton.UseVisualStyleBackColor = True
        '
        'DetailsButton
        '
        Me.DetailsButton.AutoSize = True
        Me.DetailsButton.Image = Global.Outworldz.My.Resources.Resources.text_marked
        Me.DetailsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DetailsButton.Location = New System.Drawing.Point(375, 5)
        Me.DetailsButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.DetailsButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.DetailsButton.Name = "DetailsButton"
        Me.DetailsButton.Padding = New System.Windows.Forms.Padding(3)
        Me.DetailsButton.Size = New System.Drawing.Size(175, 45)
        Me.DetailsButton.TabIndex = 18599
        Me.DetailsButton.Text = Global.Outworldz.My.Resources.Resources.Details_word
        Me.ToolTip1.SetToolTip(Me.DetailsButton, Global.Outworldz.My.Resources.Resources.View_Details)
        Me.DetailsButton.UseVisualStyleBackColor = True
        '
        'IconsButton
        '
        Me.IconsButton.AutoSize = True
        Me.IconsButton.Image = Global.Outworldz.My.Resources.Resources.transform
        Me.IconsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IconsButton.Location = New System.Drawing.Point(560, 5)
        Me.IconsButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.IconsButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.IconsButton.Name = "IconsButton"
        Me.IconsButton.Padding = New System.Windows.Forms.Padding(3)
        Me.IconsButton.Size = New System.Drawing.Size(175, 45)
        Me.IconsButton.TabIndex = 18600
        Me.IconsButton.Text = Global.Outworldz.My.Resources.Resources.Icons_word
        Me.ToolTip1.SetToolTip(Me.IconsButton, Global.Outworldz.My.Resources.Resources.View_as_Icons)
        Me.IconsButton.UseVisualStyleBackColor = True
        '
        'AvatarsButton
        '
        Me.AvatarsButton.AutoSize = True
        Me.AvatarsButton.Image = Global.Outworldz.My.Resources.Resources.users2
        Me.AvatarsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AvatarsButton.Location = New System.Drawing.Point(745, 5)
        Me.AvatarsButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.AvatarsButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.AvatarsButton.Name = "AvatarsButton"
        Me.AvatarsButton.Padding = New System.Windows.Forms.Padding(3)
        Me.AvatarsButton.Size = New System.Drawing.Size(175, 45)
        Me.AvatarsButton.TabIndex = 18602
        Me.AvatarsButton.Text = Global.Outworldz.My.Resources.Resources.Avatars_word
        Me.ToolTip1.SetToolTip(Me.AvatarsButton, Global.Outworldz.My.Resources.Resources.ListAvatars)
        Me.AvatarsButton.UseVisualStyleBackColor = True
        '
        'ImportButton
        '
        Me.ImportButton.AutoSize = True
        Me.ImportButton.Image = Global.Outworldz.My.Resources.Resources.package
        Me.ImportButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ImportButton.Location = New System.Drawing.Point(745, 60)
        Me.ImportButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ImportButton.MinimumSize = New System.Drawing.Size(146, 45)
        Me.ImportButton.Name = "ImportButton"
        Me.ImportButton.Padding = New System.Windows.Forms.Padding(3)
        Me.ImportButton.Size = New System.Drawing.Size(175, 45)
        Me.ImportButton.TabIndex = 18603
        Me.ImportButton.Text = Global.Outworldz.My.Resources.Resources.Import_word
        Me.ToolTip1.SetToolTip(Me.ImportButton, Global.Outworldz.My.Resources.Resources.Importtext)
        Me.ImportButton.UseVisualStyleBackColor = True
        '
        'AvatarView
        '
        Me.AvatarView.AllowColumnReorder = True
        Me.AvatarView.FullRowSelect = True
        Me.AvatarView.GridLines = True
        Me.AvatarView.HideSelection = False
        Me.AvatarView.Location = New System.Drawing.Point(30, 136)
        Me.AvatarView.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.AvatarView.MultiSelect = False
        Me.AvatarView.Name = "AvatarView"
        Me.AvatarView.ShowItemToolTips = True
        Me.AvatarView.Size = New System.Drawing.Size(1167, 326)
        Me.AvatarView.TabIndex = 18597
        Me.AvatarView.UseCompatibleStateImageBehavior = False
        Me.AvatarView.View = System.Windows.Forms.View.Details
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem, Me.KOT})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1214, 42)
        Me.MenuStrip1.TabIndex = 18598
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(90, 38)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'KOT
        '
        Me.KOT.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnTopToolStripMenuItem, Me.FloatToolStripMenuItem})
        Me.KOT.Image = Global.Outworldz.My.Resources.Resources.tables
        Me.KOT.Name = "KOT"
        Me.KOT.Size = New System.Drawing.Size(123, 38)
        Me.KOT.Text = "Window"
        '
        'OnTopToolStripMenuItem
        '
        Me.OnTopToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.tables
        Me.OnTopToolStripMenuItem.Name = "OnTopToolStripMenuItem"
        Me.OnTopToolStripMenuItem.Size = New System.Drawing.Size(198, 40)
        Me.OnTopToolStripMenuItem.Text = "On Top"
        '
        'FloatToolStripMenuItem
        '
        Me.FloatToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.table
        Me.FloatToolStripMenuItem.Name = "FloatToolStripMenuItem"
        Me.FloatToolStripMenuItem.Size = New System.Drawing.Size(198, 40)
        Me.FloatToolStripMenuItem.Text = "Float"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(488, 184)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 39)
        Me.Label1.TabIndex = 18606
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.AddRegionButton, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DetailsButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.IconsButton, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RefreshButton, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AvatarsButton, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ImportButton, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.RunAllButton, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.StopAllButton, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.RestartButton, 3, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(276, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(925, 110)
        Me.TableLayoutPanel1.TabIndex = 18607
        '
        'FormRegionlist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(168.0!, 168.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1214, 471)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.AvatarView)
        Me.Controls.Add(Me.AllNone)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Name = "FormRegionlist"
        Me.Text = "Region List"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents ListView1 As ListView
    Friend WithEvents RefreshButton As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents AddRegionButton As Button
    Friend WithEvents AllNone As CheckBox
    Friend WithEvents RunAllButton As Button
    Friend WithEvents StopAllButton As Button
    Friend WithEvents RestartButton As Button
    Friend WithEvents AvatarView As ListView
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DetailsButton As Button
    Friend WithEvents IconsButton As Button
    Friend WithEvents AvatarsButton As Button
    Friend WithEvents ImportButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents KOT As ToolStripMenuItem
    Friend WithEvents OnTopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FloatToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
