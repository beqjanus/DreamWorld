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
        Me.AvatarView = New System.Windows.Forms.ListView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SmallListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MapsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AvatarsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        ListViewItem1.ToolTipText = "Click to Start or Stop Robust"
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListView1.Location = New System.Drawing.Point(12, 66)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.ShowItemToolTips = True
        Me.ListView1.Size = New System.Drawing.Size(393, 188)
        Me.ListView1.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ListView1, "Regions may start/stop in groups, depending upon how your bin\Regions folder is o" &
        "rganized.")
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(80, 28)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(53, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Refresh"
        Me.ToolTip1.SetToolTip(Me.Button1, "Reload the grid")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Click an enabled row to start or stop the region.  Click a disabled row to edit t" &
    "he region"
        '
        'Addregion
        '
        Me.Addregion.Location = New System.Drawing.Point(139, 28)
        Me.Addregion.Name = "Addregion"
        Me.Addregion.Size = New System.Drawing.Size(57, 23)
        Me.Addregion.TabIndex = 18593
        Me.Addregion.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.Addregion, "Add a Region")
        Me.Addregion.UseVisualStyleBackColor = True
        '
        'AllNome
        '
        Me.AllNome.AutoSize = True
        Me.AllNome.Location = New System.Drawing.Point(6, 34)
        Me.AllNome.Name = "AllNome"
        Me.AllNome.Size = New System.Drawing.Size(68, 17)
        Me.AllNome.TabIndex = 4
        Me.AllNome.Text = "All/None"
        Me.ToolTip1.SetToolTip(Me.AllNome, "Selects all or none of the checkboxes")
        Me.AllNome.UseVisualStyleBackColor = True
        '
        'RunAllButton
        '
        Me.RunAllButton.Location = New System.Drawing.Point(202, 28)
        Me.RunAllButton.Name = "RunAllButton"
        Me.RunAllButton.Size = New System.Drawing.Size(57, 23)
        Me.RunAllButton.TabIndex = 18594
        Me.RunAllButton.Text = "Run All"
        Me.ToolTip1.SetToolTip(Me.RunAllButton, "Strats all checked regions")
        Me.RunAllButton.UseVisualStyleBackColor = True
        '
        'StopAllButton
        '
        Me.StopAllButton.Location = New System.Drawing.Point(263, 28)
        Me.StopAllButton.Name = "StopAllButton"
        Me.StopAllButton.Size = New System.Drawing.Size(57, 23)
        Me.StopAllButton.TabIndex = 18595
        Me.StopAllButton.Text = "Stop All"
        Me.ToolTip1.SetToolTip(Me.StopAllButton, "Stops all checked Regions")
        Me.StopAllButton.UseVisualStyleBackColor = True
        '
        'RestartButton
        '
        Me.RestartButton.Location = New System.Drawing.Point(326, 28)
        Me.RestartButton.Name = "RestartButton"
        Me.RestartButton.Size = New System.Drawing.Size(70, 23)
        Me.RestartButton.TabIndex = 18596
        Me.RestartButton.Text = "Restart All"
        Me.ToolTip1.SetToolTip(Me.RestartButton, "restarts all Checked Regions")
        Me.RestartButton.UseVisualStyleBackColor = True
        '
        'AvatarView
        '
        Me.AvatarView.AllowColumnReorder = True
        Me.AvatarView.FullRowSelect = True
        Me.AvatarView.GridLines = True
        Me.AvatarView.HideSelection = False
        ListViewItem2.ToolTipText = "Click to Start or Stop Robust"
        Me.AvatarView.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem2})
        Me.AvatarView.Location = New System.Drawing.Point(12, 66)
        Me.AvatarView.MultiSelect = False
        Me.AvatarView.Name = "AvatarView"
        Me.AvatarView.ShowItemToolTips = True
        Me.AvatarView.Size = New System.Drawing.Size(393, 188)
        Me.AvatarView.TabIndex = 18597
        Me.ToolTip1.SetToolTip(Me.AvatarView, "Regions may start/stop in groups, depending upon how your bin\Regions folder is o" &
        "rganized.")
        Me.AvatarView.UseCompatibleStateImageBehavior = False
        Me.AvatarView.View = System.Windows.Forms.View.Details
        Me.AvatarView.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(424, 24)
        Me.MenuStrip1.TabIndex = 18598
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DetailsToolStripMenuItem, Me.SmallListToolStripMenuItem, Me.MapsToolStripMenuItem, Me.AvatarsToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'DetailsToolStripMenuItem
        '
        Me.DetailsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.text_align_justified
        Me.DetailsToolStripMenuItem.Name = "DetailsToolStripMenuItem"
        Me.DetailsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.DetailsToolStripMenuItem.Text = "Details"
        '
        'SmallListToolStripMenuItem
        '
        Me.SmallListToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.navigate_check
        Me.SmallListToolStripMenuItem.Name = "SmallListToolStripMenuItem"
        Me.SmallListToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SmallListToolStripMenuItem.Text = "Compact"
        '
        'MapsToolStripMenuItem
        '
        Me.MapsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.water
        Me.MapsToolStripMenuItem.Name = "MapsToolStripMenuItem"
        Me.MapsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.MapsToolStripMenuItem.Text = "Maps"
        '
        'AvatarsToolStripMenuItem
        '
        Me.AvatarsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.users1
        Me.AvatarsToolStripMenuItem.Name = "AvatarsToolStripMenuItem"
        Me.AvatarsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AvatarsToolStripMenuItem.Text = "Avatars"
        '
        'RegionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(424, 269)
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
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DetailsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SmallListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MapsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AvatarsToolStripMenuItem As ToolStripMenuItem
End Class
