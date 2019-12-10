<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RegionList
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegionList))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.RefreshButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AddRegionButton = New System.Windows.Forms.Button()
        Me.AllNome = New System.Windows.Forms.CheckBox()
        Me.RunAllButton = New System.Windows.Forms.Button()
        Me.StopAllButton = New System.Windows.Forms.Button()
        Me.RestartButton = New System.Windows.Forms.Button()
        Me.DetailsButton = New System.Windows.Forms.Button()
        Me.IconsButton = New System.Windows.Forms.Button()
        Me.MapsButton = New System.Windows.Forms.Button()
        Me.AvatarsButton = New System.Windows.Forms.Button()
        Me.ImportButton = New System.Windows.Forms.Button()
        Me.RestartRobustButton = New System.Windows.Forms.Button()
        Me.StatsButton = New System.Windows.Forms.Button()
        Me.AvatarView = New System.Windows.Forms.ListView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
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
        ListViewItem1.ToolTipText = Global.Outworldz.My.Resources.Resources.ClickStartStop
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListView1.Location = New System.Drawing.Point(17, 69)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.ShowItemToolTips = True
        Me.ListView1.Size = New System.Drawing.Size(694, 188)
        Me.ListView1.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ListView1, Global.Outworldz.My.Resources.Resources.ClickStartStoptxt)
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'RefreshButton
        '
        Me.RefreshButton.Location = New System.Drawing.Point(117, 36)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(93, 23)
        Me.RefreshButton.TabIndex = 1
        Me.RefreshButton.Text = Global.Outworldz.My.Resources.Resources.Refresh1
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
        Me.AddRegionButton.Location = New System.Drawing.Point(219, 37)
        Me.AddRegionButton.Name = "AddRegionButton"
        Me.AddRegionButton.Size = New System.Drawing.Size(93, 23)
        Me.AddRegionButton.TabIndex = 18593
        Me.AddRegionButton.Text = Global.Outworldz.My.Resources.Resources.Add_word
        Me.ToolTip1.SetToolTip(Me.AddRegionButton, Global.Outworldz.My.Resources.Resources.Add_Region_word)
        Me.AddRegionButton.UseVisualStyleBackColor = True
        '
        'AllNome
        '
        Me.AllNome.AutoSize = True
        Me.AllNome.Location = New System.Drawing.Point(12, 41)
        Me.AllNome.Name = "AllNome"
        Me.AllNome.Size = New System.Drawing.Size(68, 17)
        Me.AllNome.TabIndex = 4
        Me.AllNome.Text = Global.Outworldz.My.Resources.Resources.AllNone_word
        Me.ToolTip1.SetToolTip(Me.AllNome, Global.Outworldz.My.Resources.Resources.Selectallnone)
        Me.AllNome.UseVisualStyleBackColor = True
        '
        'RunAllButton
        '
        Me.RunAllButton.Location = New System.Drawing.Point(317, 37)
        Me.RunAllButton.Name = "RunAllButton"
        Me.RunAllButton.Size = New System.Drawing.Size(93, 23)
        Me.RunAllButton.TabIndex = 18594
        Me.RunAllButton.Text = Global.Outworldz.My.Resources.Resources.Run_All_word
        Me.ToolTip1.SetToolTip(Me.RunAllButton, Global.Outworldz.My.Resources.Resources.StartAll)
        Me.RunAllButton.UseVisualStyleBackColor = True
        '
        'StopAllButton
        '
        Me.StopAllButton.Location = New System.Drawing.Point(415, 37)
        Me.StopAllButton.Name = "StopAllButton"
        Me.StopAllButton.Size = New System.Drawing.Size(93, 23)
        Me.StopAllButton.TabIndex = 18595
        Me.StopAllButton.Text = Global.Outworldz.My.Resources.Resources.Stop_All_word
        Me.ToolTip1.SetToolTip(Me.StopAllButton, Global.Outworldz.My.Resources.Resources.Stopsall)
        Me.StopAllButton.UseVisualStyleBackColor = True
        '
        'RestartButton
        '
        Me.RestartButton.Location = New System.Drawing.Point(514, 36)
        Me.RestartButton.Name = "RestartButton"
        Me.RestartButton.Size = New System.Drawing.Size(93, 23)
        Me.RestartButton.TabIndex = 18596
        Me.RestartButton.Text = Global.Outworldz.My.Resources.Resources.Restart_All_word
        Me.ToolTip1.SetToolTip(Me.RestartButton, Global.Outworldz.My.Resources.Resources.Restart_All_Checked)
        Me.RestartButton.UseVisualStyleBackColor = True
        '
        'DetailsButton
        '
        Me.DetailsButton.Location = New System.Drawing.Point(117, 8)
        Me.DetailsButton.Name = "DetailsButton"
        Me.DetailsButton.Size = New System.Drawing.Size(93, 23)
        Me.DetailsButton.TabIndex = 18599
        Me.DetailsButton.Text = Global.Outworldz.My.Resources.Resources.Details_word
        Me.ToolTip1.SetToolTip(Me.DetailsButton, Global.Outworldz.My.Resources.Resources.View_Details)
        Me.DetailsButton.UseVisualStyleBackColor = True
        '
        'IconsButton
        '
        Me.IconsButton.Location = New System.Drawing.Point(219, 8)
        Me.IconsButton.Name = "IconsButton"
        Me.IconsButton.Size = New System.Drawing.Size(93, 23)
        Me.IconsButton.TabIndex = 18600
        Me.IconsButton.Text = Global.Outworldz.My.Resources.Resources.Icons
        Me.ToolTip1.SetToolTip(Me.IconsButton, Global.Outworldz.My.Resources.Resources.View_as_Icons)
        Me.IconsButton.UseVisualStyleBackColor = True
        '
        'MapsButton
        '
        Me.MapsButton.Location = New System.Drawing.Point(317, 8)
        Me.MapsButton.Name = "MapsButton"
        Me.MapsButton.Size = New System.Drawing.Size(93, 23)
        Me.MapsButton.TabIndex = 18601
        Me.MapsButton.Text = Global.Outworldz.My.Resources.Resources.Maps_word
        Me.ToolTip1.SetToolTip(Me.MapsButton, Global.Outworldz.My.Resources.Resources.View_Maps)
        Me.MapsButton.UseVisualStyleBackColor = True
        '
        'AvatarsButton
        '
        Me.AvatarsButton.Location = New System.Drawing.Point(415, 8)
        Me.AvatarsButton.Name = "AvatarsButton"
        Me.AvatarsButton.Size = New System.Drawing.Size(93, 23)
        Me.AvatarsButton.TabIndex = 18602
        Me.AvatarsButton.Text = Global.Outworldz.My.Resources.Resources.Avatars_word
        Me.ToolTip1.SetToolTip(Me.AvatarsButton, Global.Outworldz.My.Resources.Resources.ListAvatars)
        Me.AvatarsButton.UseVisualStyleBackColor = True
        '
        'ImportButton
        '
        Me.ImportButton.Location = New System.Drawing.Point(514, 8)
        Me.ImportButton.Name = "ImportButton"
        Me.ImportButton.Size = New System.Drawing.Size(93, 23)
        Me.ImportButton.TabIndex = 18603
        Me.ImportButton.Text = Global.Outworldz.My.Resources.Resources.Import
        Me.ToolTip1.SetToolTip(Me.ImportButton, Global.Outworldz.My.Resources.Resources.Importtext)
        Me.ImportButton.UseVisualStyleBackColor = True
        '
        'RestartRobustButton
        '
        Me.RestartRobustButton.Location = New System.Drawing.Point(613, 36)
        Me.RestartRobustButton.Name = "RestartRobustButton"
        Me.RestartRobustButton.Size = New System.Drawing.Size(93, 23)
        Me.RestartRobustButton.TabIndex = 18604
        Me.RestartRobustButton.Text = Global.Outworldz.My.Resources.Resources.Restart_Robust_word
        Me.ToolTip1.SetToolTip(Me.RestartRobustButton, Global.Outworldz.My.Resources.Resources.Restart_Robust_word)
        Me.RestartRobustButton.UseVisualStyleBackColor = True
        '
        'StatsButton
        '
        Me.StatsButton.Location = New System.Drawing.Point(613, 8)
        Me.StatsButton.Name = "StatsButton"
        Me.StatsButton.Size = New System.Drawing.Size(93, 23)
        Me.StatsButton.TabIndex = 18605
        Me.StatsButton.Text = Global.Outworldz.My.Resources.Resources.Region_Stats
        Me.ToolTip1.SetToolTip(Me.StatsButton, Global.Outworldz.My.Resources.Resources.View_Stats)
        Me.StatsButton.UseVisualStyleBackColor = True
        '
        'AvatarView
        '
        Me.AvatarView.AllowColumnReorder = True
        Me.AvatarView.FullRowSelect = True
        Me.AvatarView.GridLines = True
        Me.AvatarView.HideSelection = False
        Me.AvatarView.Location = New System.Drawing.Point(16, 69)
        Me.AvatarView.MultiSelect = False
        Me.AvatarView.Name = "AvatarView"
        Me.AvatarView.ShowItemToolTips = True
        Me.AvatarView.Size = New System.Drawing.Size(695, 188)
        Me.AvatarView.TabIndex = 18597
        Me.AvatarView.UseCompatibleStateImageBehavior = False
        Me.AvatarView.View = System.Windows.Forms.View.Details
        Me.AvatarView.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(723, 30)
        Me.MenuStrip1.TabIndex = 18598
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(68, 28)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(99, 22)
        Me.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(279, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 25)
        Me.Label1.TabIndex = 18606
        Me.Label1.Text = "Please wait ..."
        '
        'RegionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(723, 269)
        Me.Controls.Add(Me.StatsButton)
        Me.Controls.Add(Me.RestartRobustButton)
        Me.Controls.Add(Me.ImportButton)
        Me.Controls.Add(Me.AvatarsButton)
        Me.Controls.Add(Me.MapsButton)
        Me.Controls.Add(Me.IconsButton)
        Me.Controls.Add(Me.DetailsButton)
        Me.Controls.Add(Me.AvatarView)
        Me.Controls.Add(Me.RestartButton)
        Me.Controls.Add(Me.StopAllButton)
        Me.Controls.Add(Me.RunAllButton)
        Me.Controls.Add(Me.AllNome)
        Me.Controls.Add(Me.AddRegionButton)
        Me.Controls.Add(Me.RefreshButton)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "RegionList"
        Me.Text = "Region List"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents ListView1 As ListView
    Friend WithEvents RefreshButton As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents AddRegionButton As Button
    Friend WithEvents AllNome As CheckBox
    Friend WithEvents RunAllButton As Button
    Friend WithEvents StopAllButton As Button
    Friend WithEvents RestartButton As Button
    Friend WithEvents AvatarView As ListView
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DetailsButton As Button
    Friend WithEvents IconsButton As Button
    Friend WithEvents MapsButton As Button
    Friend WithEvents AvatarsButton As Button
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ImportButton As Button
    Friend WithEvents RestartRobustButton As Button
    Friend WithEvents StatsButton As Button
    Friend WithEvents Label1 As Label
End Class
