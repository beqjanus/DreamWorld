<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMaps
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMaps))
        Me.MapBox = New System.Windows.Forms.GroupBox()
        Me.DelMapButton = New System.Windows.Forms.Button()
        Me.MapPicture = New System.Windows.Forms.PictureBox()
        Me.MapNone = New System.Windows.Forms.RadioButton()
        Me.MapSimple = New System.Windows.Forms.RadioButton()
        Me.MapBetter = New System.Windows.Forms.RadioButton()
        Me.MapBest = New System.Windows.Forms.RadioButton()
        Me.MapGood = New System.Windows.Forms.RadioButton()
        Me.ViewRegionMapsButton = New System.Windows.Forms.Button()
        Me.LargeMapButton = New System.Windows.Forms.Button()
        Me.SmallMapButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ExportAllMaps = New System.Windows.Forms.Button()
        Me.VieweAllMaps = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RenderMinH = New System.Windows.Forms.TextBox()
        Me.RenderMaxH = New System.Windows.Forms.TextBox()
        Me.MapYStart = New System.Windows.Forms.TextBox()
        Me.MapXStart = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ViewVisitorMapButton = New System.Windows.Forms.Button()
        Me.Days2KeepBox = New System.Windows.Forms.TextBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.VisitorGroup = New System.Windows.Forms.GroupBox()
        Me.PublicMapsCheckbox = New System.Windows.Forms.CheckBox()
        Me.K4Days = New System.Windows.Forms.Label()
        Me.ApacheRunning = New System.Windows.Forms.Label()
        Me.MapBox.SuspendLayout()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.VisitorGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'MapBox
        '
        Me.MapBox.Controls.Add(Me.DelMapButton)
        Me.MapBox.Controls.Add(Me.MapPicture)
        Me.MapBox.Controls.Add(Me.MapNone)
        Me.MapBox.Controls.Add(Me.MapSimple)
        Me.MapBox.Controls.Add(Me.MapBetter)
        Me.MapBox.Controls.Add(Me.MapBest)
        Me.MapBox.Controls.Add(Me.MapGood)
        Me.MapBox.Location = New System.Drawing.Point(15, 32)
        Me.MapBox.Name = "MapBox"
        Me.MapBox.Size = New System.Drawing.Size(171, 294)
        Me.MapBox.TabIndex = 1
        Me.MapBox.TabStop = False
        Me.MapBox.Text = "Maps"
        '
        'DelMapButton
        '
        Me.DelMapButton.Location = New System.Drawing.Point(7, 259)
        Me.DelMapButton.Name = "DelMapButton"
        Me.DelMapButton.Size = New System.Drawing.Size(143, 23)
        Me.DelMapButton.TabIndex = 0
        Me.DelMapButton.Text = Global.Outworldz.My.Resources.Resources.DelMaps
        Me.ToolTip1.SetToolTip(Me.DelMapButton, Global.Outworldz.My.Resources.Resources.Regen_Map)
        Me.DelMapButton.UseVisualStyleBackColor = True
        '
        'MapPicture
        '
        Me.MapPicture.InitialImage = CType(resources.GetObject("MapPicture.InitialImage"), System.Drawing.Image)
        Me.MapPicture.Location = New System.Drawing.Point(7, 132)
        Me.MapPicture.Name = "MapPicture"
        Me.MapPicture.Size = New System.Drawing.Size(142, 115)
        Me.MapPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.MapPicture.TabIndex = 138
        Me.MapPicture.TabStop = False
        '
        'MapNone
        '
        Me.MapNone.AutoSize = True
        Me.MapNone.Location = New System.Drawing.Point(6, 29)
        Me.MapNone.Name = "MapNone"
        Me.MapNone.Size = New System.Drawing.Size(51, 17)
        Me.MapNone.TabIndex = 7
        Me.MapNone.TabStop = True
        Me.MapNone.Text = Global.Outworldz.My.Resources.Resources.None
        Me.MapNone.UseVisualStyleBackColor = True
        '
        'MapSimple
        '
        Me.MapSimple.AutoSize = True
        Me.MapSimple.Location = New System.Drawing.Point(6, 48)
        Me.MapSimple.Name = "MapSimple"
        Me.MapSimple.Size = New System.Drawing.Size(94, 17)
        Me.MapSimple.TabIndex = 8
        Me.MapSimple.TabStop = True
        Me.MapSimple.Text = Global.Outworldz.My.Resources.Resources.Simple_but_Fast_word
        Me.MapSimple.UseVisualStyleBackColor = True
        '
        'MapBetter
        '
        Me.MapBetter.AutoSize = True
        Me.MapBetter.Location = New System.Drawing.Point(7, 87)
        Me.MapBetter.Name = "MapBetter"
        Me.MapBetter.Size = New System.Drawing.Size(116, 17)
        Me.MapBetter.TabIndex = 10
        Me.MapBetter.TabStop = True
        Me.MapBetter.Text = Global.Outworldz.My.Resources.Resources.Better_Prims
        Me.MapBetter.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.MapBetter.UseVisualStyleBackColor = True
        '
        'MapBest
        '
        Me.MapBest.AutoSize = True
        Me.MapBest.Location = New System.Drawing.Point(5, 105)
        Me.MapBest.Name = "MapBest"
        Me.MapBest.Size = New System.Drawing.Size(171, 17)
        Me.MapBest.TabIndex = 11
        Me.MapBest.TabStop = True
        Me.MapBest.Text = Global.Outworldz.My.Resources.Resources.Best_Prims
        Me.MapBest.UseVisualStyleBackColor = True
        '
        'MapGood
        '
        Me.MapGood.AutoSize = True
        Me.MapGood.Location = New System.Drawing.Point(7, 67)
        Me.MapGood.Name = "MapGood"
        Me.MapGood.Size = New System.Drawing.Size(100, 17)
        Me.MapGood.TabIndex = 9
        Me.MapGood.TabStop = True
        Me.MapGood.Text = Global.Outworldz.My.Resources.Resources.Good_Warp3D_word
        Me.MapGood.UseVisualStyleBackColor = True
        '
        'ViewRegionMapsButton
        '
        Me.ViewRegionMapsButton.Location = New System.Drawing.Point(31, 23)
        Me.ViewRegionMapsButton.Name = "ViewRegionMapsButton"
        Me.ViewRegionMapsButton.Size = New System.Drawing.Size(141, 23)
        Me.ViewRegionMapsButton.TabIndex = 0
        Me.ViewRegionMapsButton.Text = Global.Outworldz.My.Resources.Resources.View_Maps
        Me.ToolTip1.SetToolTip(Me.ViewRegionMapsButton, Global.Outworldz.My.Resources.Resources.WifiMap)
        Me.ViewRegionMapsButton.UseVisualStyleBackColor = True
        '
        'LargeMapButton
        '
        Me.LargeMapButton.Location = New System.Drawing.Point(29, 80)
        Me.LargeMapButton.Name = "LargeMapButton"
        Me.LargeMapButton.Size = New System.Drawing.Size(140, 23)
        Me.LargeMapButton.TabIndex = 2
        Me.LargeMapButton.Text = Global.Outworldz.My.Resources.Resources.LargeMap
        Me.LargeMapButton.UseVisualStyleBackColor = True
        '
        'SmallMapButton
        '
        Me.SmallMapButton.Location = New System.Drawing.Point(29, 51)
        Me.SmallMapButton.Name = "SmallMapButton"
        Me.SmallMapButton.Size = New System.Drawing.Size(141, 23)
        Me.SmallMapButton.TabIndex = 1
        Me.SmallMapButton.Text = Global.Outworldz.My.Resources.Resources.Small_Map
        Me.SmallMapButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ExportAllMaps)
        Me.GroupBox2.Controls.Add(Me.VieweAllMaps)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.RenderMinH)
        Me.GroupBox2.Controls.Add(Me.RenderMaxH)
        Me.GroupBox2.Controls.Add(Me.ViewRegionMapsButton)
        Me.GroupBox2.Controls.Add(Me.MapYStart)
        Me.GroupBox2.Controls.Add(Me.MapXStart)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.SmallMapButton)
        Me.GroupBox2.Controls.Add(Me.LargeMapButton)
        Me.GroupBox2.Location = New System.Drawing.Point(192, 32)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(196, 294)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Maps"
        '
        'ExportAllMaps
        '
        Me.ExportAllMaps.Location = New System.Drawing.Point(31, 138)
        Me.ExportAllMaps.Name = "ExportAllMaps"
        Me.ExportAllMaps.Size = New System.Drawing.Size(140, 23)
        Me.ExportAllMaps.TabIndex = 4
        Me.ExportAllMaps.Text = "Export All  Maps"
        Me.ExportAllMaps.UseVisualStyleBackColor = True
        '
        'VieweAllMaps
        '
        Me.VieweAllMaps.Location = New System.Drawing.Point(29, 109)
        Me.VieweAllMaps.Name = "VieweAllMaps"
        Me.VieweAllMaps.Size = New System.Drawing.Size(140, 23)
        Me.VieweAllMaps.TabIndex = 3
        Me.VieweAllMaps.Text = "View All  Maps"
        Me.VieweAllMaps.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 206)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Render Min Height"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 180)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Render Max Height"
        '
        'RenderMinH
        '
        Me.RenderMinH.Location = New System.Drawing.Point(145, 201)
        Me.RenderMinH.Name = "RenderMinH"
        Me.RenderMinH.Size = New System.Drawing.Size(39, 20)
        Me.RenderMinH.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.RenderMinH, "Min -100, Max +100")
        '
        'RenderMaxH
        '
        Me.RenderMaxH.Location = New System.Drawing.Point(145, 178)
        Me.RenderMaxH.Name = "RenderMaxH"
        Me.RenderMaxH.Size = New System.Drawing.Size(39, 20)
        Me.RenderMaxH.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.RenderMaxH, Global.Outworldz.My.Resources.Resources.Max4096)
        '
        'MapYStart
        '
        Me.MapYStart.Location = New System.Drawing.Point(118, 259)
        Me.MapYStart.Name = "MapYStart"
        Me.MapYStart.Size = New System.Drawing.Size(39, 20)
        Me.MapYStart.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.MapYStart, Global.Outworldz.My.Resources.Resources.CenterMap)
        '
        'MapXStart
        '
        Me.MapXStart.Location = New System.Drawing.Point(49, 259)
        Me.MapXStart.Name = "MapXStart"
        Me.MapXStart.Size = New System.Drawing.Size(39, 20)
        Me.MapXStart.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.MapXStart, Global.Outworldz.My.Resources.Resources.CenterMap)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(98, 259)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Y"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 259)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Y"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 238)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Map Center Location:"
        '
        'ViewVisitorMapButton
        '
        Me.ViewVisitorMapButton.Location = New System.Drawing.Point(15, 21)
        Me.ViewVisitorMapButton.Name = "ViewVisitorMapButton"
        Me.ViewVisitorMapButton.Size = New System.Drawing.Size(141, 23)
        Me.ViewVisitorMapButton.TabIndex = 1
        Me.ViewVisitorMapButton.Text = "View Visitor Maps"
        Me.ToolTip1.SetToolTip(Me.ViewVisitorMapButton, Global.Outworldz.My.Resources.Resources.WifiMap)
        Me.ViewVisitorMapButton.UseVisualStyleBackColor = True
        '
        'Days2KeepBox
        '
        Me.Days2KeepBox.Location = New System.Drawing.Point(15, 52)
        Me.Days2KeepBox.Name = "Days2KeepBox"
        Me.Days2KeepBox.Size = New System.Drawing.Size(39, 20)
        Me.Days2KeepBox.TabIndex = 14
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(584, 30)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(68, 28)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'VisitorGroup
        '
        Me.VisitorGroup.Controls.Add(Me.ApacheRunning)
        Me.VisitorGroup.Controls.Add(Me.PublicMapsCheckbox)
        Me.VisitorGroup.Controls.Add(Me.K4Days)
        Me.VisitorGroup.Controls.Add(Me.Days2KeepBox)
        Me.VisitorGroup.Controls.Add(Me.ViewVisitorMapButton)
        Me.VisitorGroup.Location = New System.Drawing.Point(395, 34)
        Me.VisitorGroup.Name = "VisitorGroup"
        Me.VisitorGroup.Size = New System.Drawing.Size(171, 159)
        Me.VisitorGroup.TabIndex = 3
        Me.VisitorGroup.TabStop = False
        Me.VisitorGroup.Text = "Visitors"
        '
        'PublicMapsCheckbox
        '
        Me.PublicMapsCheckbox.AutoSize = True
        Me.PublicMapsCheckbox.Location = New System.Drawing.Point(15, 86)
        Me.PublicMapsCheckbox.Name = "PublicMapsCheckbox"
        Me.PublicMapsCheckbox.Size = New System.Drawing.Size(84, 17)
        Me.PublicMapsCheckbox.TabIndex = 16
        Me.PublicMapsCheckbox.Text = "Public Maps"
        Me.PublicMapsCheckbox.UseVisualStyleBackColor = True
        '
        'K4Days
        '
        Me.K4Days.AutoSize = True
        Me.K4Days.Location = New System.Drawing.Point(60, 54)
        Me.K4Days.Name = "K4Days"
        Me.K4Days.Size = New System.Drawing.Size(74, 13)
        Me.K4Days.TabIndex = 15
        Me.K4Days.Text = "Keep for Days"
        '
        'ApacheRunning
        '
        Me.ApacheRunning.AutoSize = True
        Me.ApacheRunning.Location = New System.Drawing.Point(12, 130)
        Me.ApacheRunning.Name = "ApacheRunning"
        Me.ApacheRunning.Size = New System.Drawing.Size(74, 13)
        Me.ApacheRunning.TabIndex = 17
        Me.ApacheRunning.Text = "Keep for Days"
        '
        'FormMaps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(584, 345)
        Me.Controls.Add(Me.VisitorGroup)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MapBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormMaps"
        Me.Text = "Maps"
        Me.MapBox.ResumeLayout(False)
        Me.MapBox.PerformLayout()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.VisitorGroup.ResumeLayout(False)
        Me.VisitorGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MapBox As GroupBox
    Friend WithEvents MapPicture As PictureBox
    Friend WithEvents MapNone As RadioButton
    Friend WithEvents MapSimple As RadioButton
    Friend WithEvents MapBetter As RadioButton
    Friend WithEvents MapBest As RadioButton
    Friend WithEvents MapGood As RadioButton
    Friend WithEvents DelMapButton As Button
    Friend WithEvents ViewRegionMapsButton As Button
    Friend WithEvents LargeMapButton As Button
    Friend WithEvents SmallMapButton As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents MapYStart As TextBox
    Friend WithEvents MapXStart As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents RenderMinH As TextBox
    Friend WithEvents RenderMaxH As TextBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents VieweAllMaps As Button
    Friend WithEvents ExportAllMaps As Button
    Friend WithEvents VisitorGroup As GroupBox
    Friend WithEvents K4Days As Label
    Friend WithEvents Days2KeepBox As TextBox
    Friend WithEvents ViewVisitorMapButton As Button
    Friend WithEvents PublicMapsCheckbox As CheckBox
    Friend WithEvents ApacheRunning As Label
End Class
