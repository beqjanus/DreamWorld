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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDisplacement))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Objects = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ClearObjects = New System.Windows.Forms.ToolStripMenuItem()
        Me.MergeObject = New System.Windows.Forms.ToolStripMenuItem()
        Me.TerrainToolstrip = New System.Windows.Forms.ToolStripDropDownButton()
        Me.LoadTerrainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnoreTerrainMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParcelToolstrip = New System.Windows.Forms.ToolStripDropDownButton()
        Me.MergeParcelsMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnoreParcels = New System.Windows.Forms.ToolStripMenuItem()
        Me.OwnerMenu = New System.Windows.Forms.ToolStripButton()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem, Me.PrintToolStripMenuItem, Me.ExportToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(363, 26)
        Me.MenuStrip1.TabIndex = 7
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.printer3
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.PrintToolStripMenuItem.Text = "Print"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.download
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(73, 24)
        Me.ExportToolStripMenuItem.Text = "Export"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.Objects, Me.TerrainToolstrip, Me.ParcelToolstrip, Me.OwnerMenu})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 26)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(363, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.Outworldz.My.Resources.Resources.package_add
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'Objects
        '
        Me.Objects.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClearObjects, Me.MergeObject})
        Me.Objects.Name = "Objects"
        Me.Objects.Size = New System.Drawing.Size(60, 22)
        Me.Objects.Text = "Objects"
        '
        'ClearObjects
        '
        Me.ClearObjects.Image = Global.Outworldz.My.Resources.Resources.table_sql_delete
        Me.ClearObjects.Name = "ClearObjects"
        Me.ClearObjects.Size = New System.Drawing.Size(180, 22)
        Me.ClearObjects.Text = "Clear All Objects"
        '
        'MergeObject
        '
        Me.MergeObject.Image = Global.Outworldz.My.Resources.Resources.table_sql_add
        Me.MergeObject.Name = "MergeObject"
        Me.MergeObject.Size = New System.Drawing.Size(180, 22)
        Me.MergeObject.Text = "Merge Objects"
        '
        'TerrainToolstrip
        '
        Me.TerrainToolstrip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadTerrainMenu, Me.IgnoreTerrainMenu})
        Me.TerrainToolstrip.Image = Global.Outworldz.My.Resources.Resources.pictures
        Me.TerrainToolstrip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TerrainToolstrip.Name = "TerrainToolstrip"
        Me.TerrainToolstrip.Size = New System.Drawing.Size(71, 22)
        Me.TerrainToolstrip.Text = "Terrain"
        '
        'LoadTerrainMenu
        '
        Me.LoadTerrainMenu.Image = Global.Outworldz.My.Resources.Resources.picture_add
        Me.LoadTerrainMenu.Name = "LoadTerrainMenu"
        Me.LoadTerrainMenu.Size = New System.Drawing.Size(146, 22)
        Me.LoadTerrainMenu.Text = "Load Terrain"
        '
        'IgnoreTerrainMenu
        '
        Me.IgnoreTerrainMenu.Image = Global.Outworldz.My.Resources.Resources.picture_empty
        Me.IgnoreTerrainMenu.Name = "IgnoreTerrainMenu"
        Me.IgnoreTerrainMenu.Size = New System.Drawing.Size(146, 22)
        Me.IgnoreTerrainMenu.Text = "Ignore Terrain"
        '
        'ParcelToolstrip
        '
        Me.ParcelToolstrip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MergeParcelsMenu, Me.IgnoreParcels})
        Me.ParcelToolstrip.Image = Global.Outworldz.My.Resources.Resources.map
        Me.ParcelToolstrip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ParcelToolstrip.Name = "ParcelToolstrip"
        Me.ParcelToolstrip.Size = New System.Drawing.Size(73, 22)
        Me.ParcelToolstrip.Text = "Parcels"
        '
        'MergeParcelsMenu
        '
        Me.MergeParcelsMenu.Image = Global.Outworldz.My.Resources.Resources.map_add
        Me.MergeParcelsMenu.Name = "MergeParcelsMenu"
        Me.MergeParcelsMenu.Size = New System.Drawing.Size(180, 22)
        Me.MergeParcelsMenu.Text = "Merge Parcels"
        '
        'IgnoreParcels
        '
        Me.IgnoreParcels.Image = Global.Outworldz.My.Resources.Resources.map_delete
        Me.IgnoreParcels.Name = "IgnoreParcels"
        Me.IgnoreParcels.Size = New System.Drawing.Size(180, 22)
        Me.IgnoreParcels.Text = "Ignore Parcels"
        '
        'OwnerMenu
        '
        Me.OwnerMenu.Image = Global.Outworldz.My.Resources.Resources.user1_into
        Me.OwnerMenu.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OwnerMenu.Name = "OwnerMenu"
        Me.OwnerMenu.Size = New System.Drawing.Size(62, 22)
        Me.OwnerMenu.Text = "Owner"
        '
        'FormDisplacement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(363, 518)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormDisplacement"
        Me.Text = "1X1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents PrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents Objects As ToolStripDropDownButton
    Friend WithEvents ClearObjects As ToolStripMenuItem
    Friend WithEvents TerrainToolstrip As ToolStripDropDownButton
    Friend WithEvents LoadTerrainMenu As ToolStripMenuItem
    Friend WithEvents IgnoreTerrainMenu As ToolStripMenuItem
    Friend WithEvents ParcelToolstrip As ToolStripDropDownButton
    Friend WithEvents MergeParcelsMenu As ToolStripMenuItem
    Friend WithEvents IgnoreParcels As ToolStripMenuItem
    Friend WithEvents OwnerMenu As ToolStripButton
    Friend WithEvents MergeObject As ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As ToolStripMenuItem
End Class
