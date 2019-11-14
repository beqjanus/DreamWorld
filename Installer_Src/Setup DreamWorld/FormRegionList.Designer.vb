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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Robust", 0)
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Robust", 0)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegionList))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Addregion = New System.Windows.Forms.Button()
        Me.AllNome = New System.Windows.Forms.CheckBox()
        Me.RunAllButton = New System.Windows.Forms.Button()
        Me.StopAllButton = New System.Windows.Forms.Button()
        Me.RestartButton = New System.Windows.Forms.Button()
        Me.ViewDetail = New System.Windows.Forms.Button()
        Me.ViewCompact = New System.Windows.Forms.Button()
        Me.ViewMaps = New System.Windows.Forms.Button()
        Me.ViewAvatars = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.RestartRobustButton = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.AvatarView = New System.Windows.Forms.ListView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.ListView1.Location = New System.Drawing.Point(18, 99)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.ShowItemToolTips = True
        Me.ListView1.Size = New System.Drawing.Size(588, 280)
        Me.ListView1.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ListView1, Global.Outworldz.My.Resources.Resources.ClickStartStoptxt)
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(175, 54)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(140, 34)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Refresh1
        Me.ToolTip1.SetToolTip(Me.Button1, Global.Outworldz.My.Resources.Resources.Reload)
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = Global.Outworldz.My.Resources.Resources.Row
        '
        'Addregion
        '
        Me.Addregion.Location = New System.Drawing.Point(328, 56)
        Me.Addregion.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Addregion.Name = "Addregion"
        Me.Addregion.Size = New System.Drawing.Size(140, 34)
        Me.Addregion.TabIndex = 18593
        Me.Addregion.Text = Global.Outworldz.My.Resources.Resources.Add_word
        Me.ToolTip1.SetToolTip(Me.Addregion, Global.Outworldz.My.Resources.Resources.Add_Region_word)
        Me.Addregion.UseVisualStyleBackColor = True
        '
        'AllNome
        '
        Me.AllNome.AutoSize = True
        Me.AllNome.Location = New System.Drawing.Point(18, 62)
        Me.AllNome.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AllNome.Name = "AllNome"
        Me.AllNome.Size = New System.Drawing.Size(94, 24)
        Me.AllNome.TabIndex = 4
        Me.AllNome.Text = Global.Outworldz.My.Resources.Resources.AllNone_word
        Me.ToolTip1.SetToolTip(Me.AllNome, Global.Outworldz.My.Resources.Resources.Selectallnone)
        Me.AllNome.UseVisualStyleBackColor = True
        '
        'RunAllButton
        '
        Me.RunAllButton.Location = New System.Drawing.Point(475, 56)
        Me.RunAllButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RunAllButton.Name = "RunAllButton"
        Me.RunAllButton.Size = New System.Drawing.Size(140, 34)
        Me.RunAllButton.TabIndex = 18594
        Me.RunAllButton.Text = Global.Outworldz.My.Resources.Resources.Run_All_word
        Me.ToolTip1.SetToolTip(Me.RunAllButton, Global.Outworldz.My.Resources.Resources.StartAll)
        Me.RunAllButton.UseVisualStyleBackColor = True
        '
        'StopAllButton
        '
        Me.StopAllButton.Location = New System.Drawing.Point(623, 56)
        Me.StopAllButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.StopAllButton.Name = "StopAllButton"
        Me.StopAllButton.Size = New System.Drawing.Size(140, 34)
        Me.StopAllButton.TabIndex = 18595
        Me.StopAllButton.Text = Global.Outworldz.My.Resources.Resources.Stop_All_word
        Me.ToolTip1.SetToolTip(Me.StopAllButton, Global.Outworldz.My.Resources.Resources.Stopsall)
        Me.StopAllButton.UseVisualStyleBackColor = True
        '
        'RestartButton
        '
        Me.RestartButton.Location = New System.Drawing.Point(771, 54)
        Me.RestartButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RestartButton.Name = "RestartButton"
        Me.RestartButton.Size = New System.Drawing.Size(140, 34)
        Me.RestartButton.TabIndex = 18596
        Me.RestartButton.Text = Global.Outworldz.My.Resources.Resources.Restart_All_word
        Me.ToolTip1.SetToolTip(Me.RestartButton, Global.Outworldz.My.Resources.Resources.Restart_All_Checked)
        Me.RestartButton.UseVisualStyleBackColor = True
        '
        'ViewDetail
        '
        Me.ViewDetail.Location = New System.Drawing.Point(175, 12)
        Me.ViewDetail.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ViewDetail.Name = "ViewDetail"
        Me.ViewDetail.Size = New System.Drawing.Size(140, 34)
        Me.ViewDetail.TabIndex = 18599
        Me.ViewDetail.Text = Global.Outworldz.My.Resources.Resources.Details
        Me.ToolTip1.SetToolTip(Me.ViewDetail, Global.Outworldz.My.Resources.Resources.View_Details)
        Me.ViewDetail.UseVisualStyleBackColor = True
        '
        'ViewCompact
        '
        Me.ViewCompact.Location = New System.Drawing.Point(328, 12)
        Me.ViewCompact.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ViewCompact.Name = "ViewCompact"
        Me.ViewCompact.Size = New System.Drawing.Size(140, 34)
        Me.ViewCompact.TabIndex = 18600
        Me.ViewCompact.Text = Global.Outworldz.My.Resources.Resources.Icons
        Me.ToolTip1.SetToolTip(Me.ViewCompact, Global.Outworldz.My.Resources.Resources.View_as_Icons)
        Me.ViewCompact.UseVisualStyleBackColor = True
        '
        'ViewMaps
        '
        Me.ViewMaps.Location = New System.Drawing.Point(475, 12)
        Me.ViewMaps.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ViewMaps.Name = "ViewMaps"
        Me.ViewMaps.Size = New System.Drawing.Size(140, 34)
        Me.ViewMaps.TabIndex = 18601
        Me.ViewMaps.Text = Global.Outworldz.My.Resources.Resources.Maps
        Me.ToolTip1.SetToolTip(Me.ViewMaps, Global.Outworldz.My.Resources.Resources.View_Maps)
        Me.ViewMaps.UseVisualStyleBackColor = True
        '
        'ViewAvatars
        '
        Me.ViewAvatars.Location = New System.Drawing.Point(623, 12)
        Me.ViewAvatars.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ViewAvatars.Name = "ViewAvatars"
        Me.ViewAvatars.Size = New System.Drawing.Size(140, 34)
        Me.ViewAvatars.TabIndex = 18602
        Me.ViewAvatars.Text = Global.Outworldz.My.Resources.Resources.Avatars_word
        Me.ToolTip1.SetToolTip(Me.ViewAvatars, Global.Outworldz.My.Resources.Resources.ListAvatars)
        Me.ViewAvatars.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(771, 12)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(140, 34)
        Me.Button2.TabIndex = 18603
        Me.Button2.Text = Global.Outworldz.My.Resources.Resources.Import
        Me.ToolTip1.SetToolTip(Me.Button2, Global.Outworldz.My.Resources.Resources.Importtext)
        Me.Button2.UseVisualStyleBackColor = True
        '
        'RestartRobustButton
        '
        Me.RestartRobustButton.Location = New System.Drawing.Point(919, 54)
        Me.RestartRobustButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RestartRobustButton.Name = "RestartRobustButton"
        Me.RestartRobustButton.Size = New System.Drawing.Size(140, 34)
        Me.RestartRobustButton.TabIndex = 18604
        Me.RestartRobustButton.Text = Global.Outworldz.My.Resources.Resources.Restart_Robust_word
        Me.ToolTip1.SetToolTip(Me.RestartRobustButton, Global.Outworldz.My.Resources.Resources.Restart_Robust_word)
        Me.RestartRobustButton.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(919, 12)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(140, 34)
        Me.Button3.TabIndex = 18605
        Me.Button3.Text = Global.Outworldz.My.Resources.Resources.Region_Stats
        Me.ToolTip1.SetToolTip(Me.Button3, Global.Outworldz.My.Resources.Resources.View_Stats)
        Me.Button3.UseVisualStyleBackColor = True
        '
        'AvatarView
        '
        Me.AvatarView.AllowColumnReorder = True
        Me.AvatarView.FullRowSelect = True
        Me.AvatarView.GridLines = True
        Me.AvatarView.HideSelection = False
        ListViewItem2.ToolTipText = Global.Outworldz.My.Resources.Resources.Avatarview
        Me.AvatarView.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem2})
        Me.AvatarView.Location = New System.Drawing.Point(18, 99)
        Me.AvatarView.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AvatarView.MultiSelect = False
        Me.AvatarView.Name = "AvatarView"
        Me.AvatarView.ShowItemToolTips = True
        Me.AvatarView.Size = New System.Drawing.Size(1041, 280)
        Me.AvatarView.TabIndex = 18597
        Me.AvatarView.UseCompatibleStateImageBehavior = False
        Me.AvatarView.View = System.Windows.Forms.View.Details
        Me.AvatarView.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1084, 35)
        Me.MenuStrip1.TabIndex = 18598
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(89, 29)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(151, 34)
        Me.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'RegionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1084, 404)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.RestartRobustButton)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ViewAvatars)
        Me.Controls.Add(Me.ViewMaps)
        Me.Controls.Add(Me.ViewCompact)
        Me.Controls.Add(Me.ViewDetail)
        Me.Controls.Add(Me.AvatarView)
        Me.Controls.Add(Me.RestartButton)
        Me.Controls.Add(Me.StopAllButton)
        Me.Controls.Add(Me.RunAllButton)
        Me.Controls.Add(Me.AllNome)
        Me.Controls.Add(Me.Addregion)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "RegionList"
        Me.Text = "Region List"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Button1 As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Addregion As Button
    Friend WithEvents AllNome As CheckBox
    Friend WithEvents RunAllButton As Button
    Friend WithEvents StopAllButton As Button
    Friend WithEvents RestartButton As Button
    Friend WithEvents AvatarView As ListView
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewDetail As Button
    Friend WithEvents ViewCompact As Button
    Friend WithEvents ViewMaps As Button
    Friend WithEvents ViewAvatars As Button
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Button2 As Button
    Friend WithEvents RestartRobustButton As Button
    Friend WithEvents Button3 As Button
End Class
