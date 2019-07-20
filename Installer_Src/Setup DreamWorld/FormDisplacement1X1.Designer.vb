<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDisplacement1X1
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
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(166, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MergingToolStripMenuItem, Me.TerrainToolStripMenuItem, Me.ParcelsToolStripMenuItem, Me.SetOwnerToolStripMenuItem})
        Me.ToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.package
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(77, 20)
        Me.ToolStripMenuItem1.Text = "Options"
        '
        'MergingToolStripMenuItem
        '
        Me.MergingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MergeOARToolStripMenuItem, Me.ClearOARToolStripMenuItem})
        Me.MergingToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.cube_blue
        Me.MergingToolStripMenuItem.Name = "MergingToolStripMenuItem"
        Me.MergingToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.MergingToolStripMenuItem.Text = "Object Merge"
        '
        'MergeOARToolStripMenuItem
        '
        Me.MergeOARToolStripMenuItem.Checked = True
        Me.MergeOARToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MergeOARToolStripMenuItem.Name = "MergeOARToolStripMenuItem"
        Me.MergeOARToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.MergeOARToolStripMenuItem.Text = "Merge OAR"
        '
        'ClearOARToolStripMenuItem
        '
        Me.ClearOARToolStripMenuItem.Name = "ClearOARToolStripMenuItem"
        Me.ClearOARToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ClearOARToolStripMenuItem.Text = "Clear and Load"
        '
        'TerrainToolStripMenuItem
        '
        Me.TerrainToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ForceTerrainToolStripMenuItem, Me.OriginalTererainToolStripMenuItem})
        Me.TerrainToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.Good
        Me.TerrainToolStripMenuItem.Name = "TerrainToolStripMenuItem"
        Me.TerrainToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.TerrainToolStripMenuItem.Text = "Terrain"
        '
        'ForceTerrainToolStripMenuItem
        '
        Me.ForceTerrainToolStripMenuItem.Checked = True
        Me.ForceTerrainToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ForceTerrainToolStripMenuItem.Name = "ForceTerrainToolStripMenuItem"
        Me.ForceTerrainToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.ForceTerrainToolStripMenuItem.Text = "Load Terrain"
        '
        'OriginalTererainToolStripMenuItem
        '
        Me.OriginalTererainToolStripMenuItem.Name = "OriginalTererainToolStripMenuItem"
        Me.OriginalTererainToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.OriginalTererainToolStripMenuItem.Text = "Ignore Terrain"
        '
        'ParcelsToolStripMenuItem
        '
        Me.ParcelsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadParcelToolStripMenuItem, Me.IgnoreParcelToolStripMenuItem})
        Me.ParcelsToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.text_align_justified
        Me.ParcelsToolStripMenuItem.Name = "ParcelsToolStripMenuItem"
        Me.ParcelsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ParcelsToolStripMenuItem.Text = "Parcels"
        '
        'LoadParcelToolStripMenuItem
        '
        Me.LoadParcelToolStripMenuItem.Checked = True
        Me.LoadParcelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LoadParcelToolStripMenuItem.Name = "LoadParcelToolStripMenuItem"
        Me.LoadParcelToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.LoadParcelToolStripMenuItem.Text = "Load Parcel"
        '
        'IgnoreParcelToolStripMenuItem
        '
        Me.IgnoreParcelToolStripMenuItem.Name = "IgnoreParcelToolStripMenuItem"
        Me.IgnoreParcelToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.IgnoreParcelToolStripMenuItem.Text = "Ignore Parcel"
        '
        'SetOwnerToolStripMenuItem
        '
        Me.SetOwnerToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.user3
        Me.SetOwnerToolStripMenuItem.Name = "SetOwnerToolStripMenuItem"
        Me.SetOwnerToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SetOwnerToolStripMenuItem.Text = "Set Owner"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox3.ErrorImage = Global.Outworldz.My.Resources.Resources.water
        Me.PictureBox3.Image = Global.Outworldz.My.Resources.Resources.water
        Me.PictureBox3.InitialImage = Global.Outworldz.My.Resources.Resources.water
        Me.PictureBox3.Location = New System.Drawing.Point(33, 48)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(88, 81)
        Me.PictureBox3.TabIndex = 8
        Me.PictureBox3.TabStop = False
        '
        'FormDisplacement1X1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(166, 150)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "FormDisplacement1X1"
        Me.Text = "1X1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents PictureBox3 As PictureBox
End Class
