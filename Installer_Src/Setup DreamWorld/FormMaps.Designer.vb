<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMaps
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
    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ToolTip.SetToolTip(System.Windows.Forms.Control,System.String)")>
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMaps))
        Me.MapBox = New System.Windows.Forms.GroupBox()
        Me.ViewMap = New System.Windows.Forms.Button()
        Me.MapHelp = New System.Windows.Forms.PictureBox()
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
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MapBox.SuspendLayout()
        CType(Me.MapHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MapBox
        '
        Me.MapBox.Controls.Add(Me.ViewMap)
        Me.MapBox.Controls.Add(Me.MapHelp)
        Me.MapBox.Controls.Add(Me.MapPicture)
        Me.MapBox.Controls.Add(Me.MapNone)
        Me.MapBox.Controls.Add(Me.MapSimple)
        Me.MapBox.Controls.Add(Me.MapBetter)
        Me.MapBox.Controls.Add(Me.MapBest)
        Me.MapBox.Controls.Add(Me.MapGood)
        Me.MapBox.Location = New System.Drawing.Point(15, 32)
        Me.MapBox.Name = "MapBox"
        Me.MapBox.Size = New System.Drawing.Size(171, 294)
        Me.MapBox.TabIndex = 1866
        Me.MapBox.TabStop = False
        Me.MapBox.Text = Global.Outworldz.My.Resources.Maps_word
        '
        'ViewMap
        '
        Me.ViewMap.Location = New System.Drawing.Point(7, 259)
        Me.ViewMap.Name = "ViewMap"
        Me.ViewMap.Size = New System.Drawing.Size(143, 23)
        Me.ViewMap.TabIndex = 1858
        Me.ViewMap.Text = Global.Outworldz.My.Resources.DelMaps
        Me.ToolTip1.SetToolTip(Me.ViewMap, Global.Outworldz.My.Resources.Regen_Map)
        Me.ViewMap.UseVisualStyleBackColor = True
        '
        'MapHelp
        '
        Me.MapHelp.Image = Global.Outworldz.My.Resources.about
        Me.MapHelp.Location = New System.Drawing.Point(112, 17)
        Me.MapHelp.Name = "MapHelp"
        Me.MapHelp.Size = New System.Drawing.Size(28, 27)
        Me.MapHelp.TabIndex = 1857
        Me.MapHelp.TabStop = False
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
        Me.MapNone.Text = Global.Outworldz.My.Resources.None
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
        Me.MapSimple.Text = Global.Outworldz.My.Resources.Simple_but_Fast_word
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
        Me.MapBetter.Text = Global.Outworldz.My.Resources.Better_Prims
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
        Me.MapBest.Text = Global.Outworldz.My.Resources.Best_Prims
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
        Me.MapGood.Text = Global.Outworldz.My.Resources.Good_Warp3D_word
        Me.MapGood.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(31, 23)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(141, 23)
        Me.Button2.TabIndex = 1859
        Me.Button2.Text = Global.Outworldz.My.Resources.View_Maps
        Me.ToolTip1.SetToolTip(Me.Button2, Global.Outworldz.My.Resources.WifiMap)
        Me.Button2.UseVisualStyleBackColor = True
        '
        'LargeMapButton
        '
        Me.LargeMapButton.Location = New System.Drawing.Point(29, 80)
        Me.LargeMapButton.Name = "LargeMapButton"
        Me.LargeMapButton.Size = New System.Drawing.Size(140, 23)
        Me.LargeMapButton.TabIndex = 1860
        Me.LargeMapButton.Text = Global.Outworldz.My.Resources.LargeMap
        Me.LargeMapButton.UseVisualStyleBackColor = True
        '
        'SmallMapButton
        '
        Me.SmallMapButton.Location = New System.Drawing.Point(29, 51)
        Me.SmallMapButton.Name = "SmallMapButton"
        Me.SmallMapButton.Size = New System.Drawing.Size(141, 23)
        Me.SmallMapButton.TabIndex = 1867
        Me.SmallMapButton.Text = Global.Outworldz.My.Resources.Small_Map
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
        Me.GroupBox2.Location = New System.Drawing.Point(192, 32)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(184, 294)
        Me.GroupBox2.TabIndex = 1869
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = My.Resources.Maps_word
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 1876
        Me.Label5.Text = Global.Outworldz.My.Resources.RenderMin
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 13)
        Me.Label4.TabIndex = 1875
        Me.Label4.Text = Global.Outworldz.My.Resources.RenderMax
        '
        'RenderMinH
        '
        Me.RenderMinH.Location = New System.Drawing.Point(133, 161)
        Me.RenderMinH.Name = "RenderMinH"
        Me.RenderMinH.Size = New System.Drawing.Size(39, 20)
        Me.RenderMinH.TabIndex = 1874
        Me.ToolTip1.SetToolTip(Me.RenderMinH, "Min -100, Max +100")
        '
        'RenderMaxH
        '
        Me.RenderMaxH.Location = New System.Drawing.Point(133, 138)
        Me.RenderMaxH.Name = "RenderMaxH"
        Me.RenderMaxH.Size = New System.Drawing.Size(39, 20)
        Me.RenderMaxH.TabIndex = 1873
        Me.ToolTip1.SetToolTip(Me.RenderMaxH, Global.Outworldz.My.Resources.Max4096)
        '
        'MapYStart
        '
        Me.MapYStart.Location = New System.Drawing.Point(117, 241)
        Me.MapYStart.Name = "MapYStart"
        Me.MapYStart.Size = New System.Drawing.Size(39, 20)
        Me.MapYStart.TabIndex = 1872
        Me.ToolTip1.SetToolTip(Me.MapYStart, Global.Outworldz.My.Resources.CenterMap)
        '
        'MapXStart
        '
        Me.MapXStart.Location = New System.Drawing.Point(48, 241)
        Me.MapXStart.Name = "MapXStart"
        Me.MapXStart.Size = New System.Drawing.Size(39, 20)
        Me.MapXStart.TabIndex = 1871
        Me.ToolTip1.SetToolTip(Me.MapXStart, Global.Outworldz.My.Resources.CenterMap)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(97, 241)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 13)
        Me.Label3.TabIndex = 1870
        Me.Label3.Text = My.Resources.Y
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 241)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 1869
        Me.Label2.Text = My.Resources.X
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 220)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 1868
        Me.Label1.Text = Global.Outworldz.My.Resources.Map_Center_Location_word
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(400, 26)
        Me.MenuStrip2.TabIndex = 1890
        Me.MenuStrip2.Text = My.Resources._0
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(64, 24)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Help_word
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.DatabaseSetupToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        '
        'FormMaps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(400, 345)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.MapBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormMaps"
        Me.Text = Global.Outworldz.My.Resources.Maps_word
        Me.MapBox.ResumeLayout(False)
        Me.MapBox.PerformLayout()
        CType(Me.MapHelp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MapBox As GroupBox
    Friend WithEvents MapHelp As PictureBox
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
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
