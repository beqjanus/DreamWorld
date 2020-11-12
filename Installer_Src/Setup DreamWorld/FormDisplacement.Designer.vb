<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormDisplacement
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MergingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MergeOARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearOARToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TerrainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ForceTerrainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OriginalTererainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParcelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadParcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnoreParcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetOwnerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(454, 30)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = ""
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MergingToolStripMenuItem, Me.TerrainToolStripMenuItem, Me.ParcelsToolStripMenuItem, Me.SetOwnerToolStripMenuItem})
        Me.ToolStripMenuItem1.Image = Global.Outworldz.My.Resources.package
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(97, 28)
        Me.ToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Options
        '
        'MergingToolStripMenuItem
        '
        Me.MergingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MergeOARToolStripMenuItem, Me.ClearOARToolStripMenuItem})
        Me.MergingToolStripMenuItem.Image = Global.Outworldz.My.Resources.cube_blue
        Me.MergingToolStripMenuItem.Name = "MergingToolStripMenuItem"
        Me.MergingToolStripMenuItem.Size = New System.Drawing.Size(220, 30)
        Me.MergingToolStripMenuItem.Text = Global.Outworldz.My.Resources.Merge_Objects_word
        '
        'MergeOARToolStripMenuItem
        '
        Me.MergeOARToolStripMenuItem.Checked = True
        Me.MergeOARToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MergeOARToolStripMenuItem.Name = "MergeOARToolStripMenuItem"
        Me.MergeOARToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.MergeOARToolStripMenuItem.Text = Global.Outworldz.My.Resources.Merge_OAR_word
        '
        'ClearOARToolStripMenuItem
        '
        Me.ClearOARToolStripMenuItem.Name = "ClearOARToolStripMenuItem"
        Me.ClearOARToolStripMenuItem.Size = New System.Drawing.Size(184, 26)
        Me.ClearOARToolStripMenuItem.Text = Global.Outworldz.My.Resources.Clear_and_Load_word
        '
        'TerrainToolStripMenuItem
        '
        Me.TerrainToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ForceTerrainToolStripMenuItem, Me.OriginalTererainToolStripMenuItem})
        Me.TerrainToolStripMenuItem.Image = Global.Outworldz.My.Resources.Good
        Me.TerrainToolStripMenuItem.Name = "TerrainToolStripMenuItem"
        Me.TerrainToolStripMenuItem.Size = New System.Drawing.Size(220, 30)
        Me.TerrainToolStripMenuItem.Text = Global.Outworldz.My.Resources.Terrain_word
        '
        'ForceTerrainToolStripMenuItem
        '
        Me.ForceTerrainToolStripMenuItem.Checked = True
        Me.ForceTerrainToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ForceTerrainToolStripMenuItem.Name = "ForceTerrainToolStripMenuItem"
        Me.ForceTerrainToolStripMenuItem.Size = New System.Drawing.Size(176, 26)
        Me.ForceTerrainToolStripMenuItem.Text = Global.Outworldz.My.Resources.Load_Terrain
        '
        'OriginalTererainToolStripMenuItem
        '
        Me.OriginalTererainToolStripMenuItem.Name = "OriginalTererainToolStripMenuItem"
        Me.OriginalTererainToolStripMenuItem.Size = New System.Drawing.Size(176, 26)
        Me.OriginalTererainToolStripMenuItem.Text = Global.Outworldz.My.Resources.Ignore_Terrain_word
        '
        'ParcelsToolStripMenuItem
        '
        Me.ParcelsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadParcelToolStripMenuItem, Me.IgnoreParcelToolStripMenuItem})
        Me.ParcelsToolStripMenuItem.Image = Global.Outworldz.My.Resources.text_align_justified
        Me.ParcelsToolStripMenuItem.Name = "ParcelsToolStripMenuItem"
        Me.ParcelsToolStripMenuItem.Size = New System.Drawing.Size(220, 30)
        Me.ParcelsToolStripMenuItem.Text = Global.Outworldz.My.Resources.Parcels
        '
        'LoadParcelToolStripMenuItem
        '
        Me.LoadParcelToolStripMenuItem.Checked = True
        Me.LoadParcelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LoadParcelToolStripMenuItem.Name = "LoadParcelToolStripMenuItem"
        Me.LoadParcelToolStripMenuItem.Size = New System.Drawing.Size(170, 26)
        Me.LoadParcelToolStripMenuItem.Text = Global.Outworldz.My.Resources.Load_Parcel
        '
        'IgnoreParcelToolStripMenuItem
        '
        Me.IgnoreParcelToolStripMenuItem.Name = "IgnoreParcelToolStripMenuItem"
        Me.IgnoreParcelToolStripMenuItem.Size = New System.Drawing.Size(170, 26)
        Me.IgnoreParcelToolStripMenuItem.Text = Global.Outworldz.My.Resources.Ignore_Parcel_word
        '
        'SetOwnerToolStripMenuItem
        '
        Me.SetOwnerToolStripMenuItem.Image = Global.Outworldz.My.Resources.user3
        Me.SetOwnerToolStripMenuItem.Name = "SetOwnerToolStripMenuItem"
        Me.SetOwnerToolStripMenuItem.Size = New System.Drawing.Size(220, 30)
        Me.SetOwnerToolStripMenuItem.Text = Global.Outworldz.My.Resources.Set_Owner_word
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(77, 28)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        '
        'FormDisplacement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(454, 192)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormDisplacement"
        Me.Text = "1X1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents MergingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MergeOARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearOARToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TerrainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ForceTerrainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OriginalTererainToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ParcelsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadParcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IgnoreParcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetOwnerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
End Class
