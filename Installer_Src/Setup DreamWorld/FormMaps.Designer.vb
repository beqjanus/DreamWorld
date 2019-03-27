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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMaps))
        Me.MapBox = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ViewMap = New System.Windows.Forms.Button()
        Me.MapHelp = New System.Windows.Forms.PictureBox()
        Me.MapPicture = New System.Windows.Forms.PictureBox()
        Me.MapNone = New System.Windows.Forms.RadioButton()
        Me.MapSimple = New System.Windows.Forms.RadioButton()
        Me.MapBetter = New System.Windows.Forms.RadioButton()
        Me.MapBest = New System.Windows.Forms.RadioButton()
        Me.MapGood = New System.Windows.Forms.RadioButton()
        Me.MapBox.SuspendLayout()
        CType(Me.MapHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MapBox
        '
        Me.MapBox.Controls.Add(Me.Button2)
        Me.MapBox.Controls.Add(Me.ViewMap)
        Me.MapBox.Controls.Add(Me.MapHelp)
        Me.MapBox.Controls.Add(Me.MapPicture)
        Me.MapBox.Controls.Add(Me.MapNone)
        Me.MapBox.Controls.Add(Me.MapSimple)
        Me.MapBox.Controls.Add(Me.MapBetter)
        Me.MapBox.Controls.Add(Me.MapBest)
        Me.MapBox.Controls.Add(Me.MapGood)
        Me.MapBox.Location = New System.Drawing.Point(12, 12)
        Me.MapBox.Name = "MapBox"
        Me.MapBox.Size = New System.Drawing.Size(171, 333)
        Me.MapBox.TabIndex = 1866
        Me.MapBox.TabStop = False
        Me.MapBox.Text = "Maps"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(15, 253)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(125, 23)
        Me.Button2.TabIndex = 1859
        Me.Button2.Text = "View Map Web Page"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ViewMap
        '
        Me.ViewMap.Location = New System.Drawing.Point(15, 304)
        Me.ViewMap.Name = "ViewMap"
        Me.ViewMap.Size = New System.Drawing.Size(125, 23)
        Me.ViewMap.TabIndex = 1858
        Me.ViewMap.Text = "Delete All Maps"
        Me.ViewMap.UseVisualStyleBackColor = True
        '
        'MapHelp
        '
        Me.MapHelp.Image = Global.Outworldz.My.Resources.Resources.about
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
        Me.MapNone.Text = "None"
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
        Me.MapSimple.Text = "Simple but fast"
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
        Me.MapBetter.Text = "Better (Prims, Slow)"
        Me.MapBetter.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.MapBetter.UseVisualStyleBackColor = True
        '
        'MapBest
        '
        Me.MapBest.AutoSize = True
        Me.MapBest.Location = New System.Drawing.Point(6, 109)
        Me.MapBest.Name = "MapBest"
        Me.MapBest.Size = New System.Drawing.Size(144, 17)
        Me.MapBest.TabIndex = 11
        Me.MapBest.TabStop = True
        Me.MapBest.Text = "Best (Prims +Mesh, Slow)"
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
        Me.MapGood.Text = "Good (Warp3D)"
        Me.MapGood.UseVisualStyleBackColor = True
        '
        'FormMaps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(201, 357)
        Me.Controls.Add(Me.MapBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormMaps"
        Me.Text = "Maps"
        Me.MapBox.ResumeLayout(False)
        Me.MapBox.PerformLayout()
        CType(Me.MapHelp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MapPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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
End Class
