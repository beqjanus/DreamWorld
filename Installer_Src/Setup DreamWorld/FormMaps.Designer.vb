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
        Me.ViewMap = New System.Windows.Forms.Button()
        Me.MapPicture = New System.Windows.Forms.PictureBox()
        Me.MapNone = New System.Windows.Forms.RadioButton()
        Me.MapSimple = New System.Windows.Forms.RadioButton()
        Me.MapBetter = New System.Windows.Forms.RadioButton()
        Me.MapBest = New System.Windows.Forms.RadioButton()
        Me.MapGood = New System.Windows.Forms.RadioButton()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.LargeMapButton = New System.Windows.Forms.Button()
        Me.SmallMapButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
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
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MapBox.SuspendLayout()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MapBox
        '
        Me.MapBox.Controls.Add(Me.ViewMap)
        Me.MapBox.Controls.Add(Me.MapPicture)
        Me.MapBox.Controls.Add(Me.MapNone)
        Me.MapBox.Controls.Add(Me.MapSimple)
        Me.MapBox.Controls.Add(Me.MapBetter)
        Me.MapBox.Controls.Add(Me.MapBest)
        Me.MapBox.Controls.Add(Me.MapGood)
        Me.MapBox.Location = New System.Drawing.Point(26, 56)
        Me.MapBox.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapBox.Name = "MapBox"
        Me.MapBox.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapBox.Size = New System.Drawing.Size(299, 514)
        Me.MapBox.TabIndex = 1866
        Me.MapBox.TabStop = False
        Me.MapBox.Text = "Maps"
        '
        'ViewMap
        '
        Me.ViewMap.Location = New System.Drawing.Point(12, 453)
        Me.ViewMap.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ViewMap.Name = "ViewMap"
        Me.ViewMap.Size = New System.Drawing.Size(250, 40)
        Me.ViewMap.TabIndex = 1858
        Me.ViewMap.Text = Global.Outworldz.My.Resources.Resources.DelMaps
        Me.ToolTip1.SetToolTip(Me.ViewMap, Global.Outworldz.My.Resources.Resources.Regen_Map)
        Me.ViewMap.UseVisualStyleBackColor = True
        '
        'MapPicture
        '
        Me.MapPicture.InitialImage = CType(resources.GetObject("MapPicture.InitialImage"), System.Drawing.Image)
        Me.MapPicture.Location = New System.Drawing.Point(12, 231)
        Me.MapPicture.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapPicture.Name = "MapPicture"
        Me.MapPicture.Size = New System.Drawing.Size(248, 201)
        Me.MapPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.MapPicture.TabIndex = 138
        Me.MapPicture.TabStop = False
        '
        'MapNone
        '
        Me.MapNone.AutoSize = True
        Me.MapNone.Location = New System.Drawing.Point(10, 51)
        Me.MapNone.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapNone.Name = "MapNone"
        Me.MapNone.Size = New System.Drawing.Size(84, 29)
        Me.MapNone.TabIndex = 7
        Me.MapNone.TabStop = True
        Me.MapNone.Text = Global.Outworldz.My.Resources.Resources.None
        Me.MapNone.UseVisualStyleBackColor = True
        '
        'MapSimple
        '
        Me.MapSimple.AutoSize = True
        Me.MapSimple.Location = New System.Drawing.Point(10, 84)
        Me.MapSimple.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapSimple.Name = "MapSimple"
        Me.MapSimple.Size = New System.Drawing.Size(165, 29)
        Me.MapSimple.TabIndex = 8
        Me.MapSimple.TabStop = True
        Me.MapSimple.Text = Global.Outworldz.My.Resources.Resources.Simple_but_Fast_word
        Me.MapSimple.UseVisualStyleBackColor = True
        '
        'MapBetter
        '
        Me.MapBetter.AutoSize = True
        Me.MapBetter.Location = New System.Drawing.Point(12, 152)
        Me.MapBetter.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapBetter.Name = "MapBetter"
        Me.MapBetter.Size = New System.Drawing.Size(209, 29)
        Me.MapBetter.TabIndex = 10
        Me.MapBetter.TabStop = True
        Me.MapBetter.Text = Global.Outworldz.My.Resources.Resources.Better_Prims
        Me.MapBetter.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.MapBetter.UseVisualStyleBackColor = True
        '
        'MapBest
        '
        Me.MapBest.AutoSize = True
        Me.MapBest.Location = New System.Drawing.Point(9, 184)
        Me.MapBest.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapBest.Name = "MapBest"
        Me.MapBest.Size = New System.Drawing.Size(314, 29)
        Me.MapBest.TabIndex = 11
        Me.MapBest.TabStop = True
        Me.MapBest.Text = Global.Outworldz.My.Resources.Resources.Best_Prims
        Me.MapBest.UseVisualStyleBackColor = True
        '
        'MapGood
        '
        Me.MapGood.AutoSize = True
        Me.MapGood.Location = New System.Drawing.Point(12, 117)
        Me.MapGood.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapGood.Name = "MapGood"
        Me.MapGood.Size = New System.Drawing.Size(177, 29)
        Me.MapGood.TabIndex = 9
        Me.MapGood.TabStop = True
        Me.MapGood.Text = Global.Outworldz.My.Resources.Resources.Good_Warp3D_word
        Me.MapGood.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(54, 40)
        Me.Button2.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(247, 40)
        Me.Button2.TabIndex = 1859
        Me.Button2.Text = Global.Outworldz.My.Resources.Resources.View_Maps
        Me.ToolTip1.SetToolTip(Me.Button2, Global.Outworldz.My.Resources.Resources.WifiMap)
        Me.Button2.UseVisualStyleBackColor = True
        '
        'LargeMapButton
        '
        Me.LargeMapButton.Location = New System.Drawing.Point(51, 140)
        Me.LargeMapButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LargeMapButton.Name = "LargeMapButton"
        Me.LargeMapButton.Size = New System.Drawing.Size(245, 40)
        Me.LargeMapButton.TabIndex = 1860
        Me.LargeMapButton.Text = Global.Outworldz.My.Resources.Resources.LargeMap
        Me.LargeMapButton.UseVisualStyleBackColor = True
        '
        'SmallMapButton
        '
        Me.SmallMapButton.Location = New System.Drawing.Point(51, 89)
        Me.SmallMapButton.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.SmallMapButton.Name = "SmallMapButton"
        Me.SmallMapButton.Size = New System.Drawing.Size(247, 40)
        Me.SmallMapButton.TabIndex = 1867
        Me.SmallMapButton.Text = Global.Outworldz.My.Resources.Resources.Small_Map
        Me.SmallMapButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.RenderMinH)
        Me.GroupBox2.Controls.Add(Me.RenderMaxH)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.MapYStart)
        Me.GroupBox2.Controls.Add(Me.MapXStart)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.SmallMapButton)
        Me.GroupBox2.Controls.Add(Me.LargeMapButton)
        Me.GroupBox2.Location = New System.Drawing.Point(336, 56)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(322, 514)
        Me.GroupBox2.TabIndex = 1869
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Maps"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 290)
        Me.Label5.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(173, 25)
        Me.Label5.TabIndex = 1876
        Me.Label5.Text = "Render Min Height"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 245)
        Me.Label4.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(179, 25)
        Me.Label4.TabIndex = 1875
        Me.Label4.Text = "Render Max Height"
        '
        'RenderMinH
        '
        Me.RenderMinH.Location = New System.Drawing.Point(233, 282)
        Me.RenderMinH.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.RenderMinH.Name = "RenderMinH"
        Me.RenderMinH.Size = New System.Drawing.Size(65, 29)
        Me.RenderMinH.TabIndex = 1874
        Me.ToolTip1.SetToolTip(Me.RenderMinH, "Min -100, Max +100")
        '
        'RenderMaxH
        '
        Me.RenderMaxH.Location = New System.Drawing.Point(233, 242)
        Me.RenderMaxH.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.RenderMaxH.Name = "RenderMaxH"
        Me.RenderMaxH.Size = New System.Drawing.Size(65, 29)
        Me.RenderMaxH.TabIndex = 1873
        Me.ToolTip1.SetToolTip(Me.RenderMaxH, Global.Outworldz.My.Resources.Resources.Max4096)
        '
        'MapYStart
        '
        Me.MapYStart.Location = New System.Drawing.Point(205, 422)
        Me.MapYStart.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapYStart.Name = "MapYStart"
        Me.MapYStart.Size = New System.Drawing.Size(65, 29)
        Me.MapYStart.TabIndex = 1872
        Me.ToolTip1.SetToolTip(Me.MapYStart, Global.Outworldz.My.Resources.Resources.CenterMap)
        '
        'MapXStart
        '
        Me.MapXStart.Location = New System.Drawing.Point(84, 422)
        Me.MapXStart.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MapXStart.Name = "MapXStart"
        Me.MapXStart.Size = New System.Drawing.Size(65, 29)
        Me.MapXStart.TabIndex = 1871
        Me.ToolTip1.SetToolTip(Me.MapXStart, Global.Outworldz.My.Resources.Resources.CenterMap)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(170, 422)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 25)
        Me.Label3.TabIndex = 1870
        Me.Label3.Text = "Y"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(47, 422)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 25)
        Me.Label2.TabIndex = 1869
        Me.Label2.Text = "Y"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(47, 385)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 25)
        Me.Label1.TabIndex = 1868
        Me.Label1.Text = "Map Center Location:"
        '
        'MenuStrip2
        '
        Me.MenuStrip2.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip2.Size = New System.Drawing.Size(700, 38)
        Me.MenuStrip2.TabIndex = 1890
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.about
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(90, 34)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormMaps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(168.0!, 168.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(700, 604)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MapBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
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
    Friend WithEvents ViewMap As Button
    Friend WithEvents Button2 As Button
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
End Class
