<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRegionPopup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRegionPopup))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.StatsButton = New System.Windows.Forms.Button()
        Me.EditButton1 = New System.Windows.Forms.Button()
        Me.StartButton3 = New System.Windows.Forms.Button()
        Me.RecycleButton2 = New System.Windows.Forms.Button()
        Me.StopButton1 = New System.Windows.Forms.Button()
        Me.StatsButton1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.StatsButton1)
        Me.GroupBox1.Controls.Add(Me.StatsButton)
        Me.GroupBox1.Controls.Add(Me.EditButton1)
        Me.GroupBox1.Controls.Add(Me.StartButton3)
        Me.GroupBox1.Controls.Add(Me.RecycleButton2)
        Me.GroupBox1.Controls.Add(Me.StopButton1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(157, 198)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Region Controls"
        '
        'StatsButton
        '
        Me.StatsButton.Image = Global.Outworldz.My.Resources.Resources.user1_into
        Me.StatsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StatsButton.Location = New System.Drawing.Point(19, 77)
        Me.StatsButton.Name = "StatsButton"
        Me.StatsButton.Size = New System.Drawing.Size(121, 23)
        Me.StatsButton.TabIndex = 9
        Me.StatsButton.Text = Global.Outworldz.My.Resources.Resources.Teleport_word
        Me.StatsButton.UseVisualStyleBackColor = True
        '
        'EditButton1
        '
        Me.EditButton1.Image = Global.Outworldz.My.Resources.Resources.document_dirty
        Me.EditButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditButton1.Location = New System.Drawing.Point(19, 164)
        Me.EditButton1.Name = "EditButton1"
        Me.EditButton1.Size = New System.Drawing.Size(121, 23)
        Me.EditButton1.TabIndex = 8
        Me.EditButton1.Text = Global.Outworldz.My.Resources.Resources.Edit_word
        Me.EditButton1.UseVisualStyleBackColor = True
        '
        'StartButton3
        '
        Me.StartButton3.Image = Global.Outworldz.My.Resources.Resources.media_play
        Me.StartButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StartButton3.Location = New System.Drawing.Point(19, 19)
        Me.StartButton3.Name = "StartButton3"
        Me.StartButton3.Size = New System.Drawing.Size(121, 23)
        Me.StartButton3.TabIndex = 7
        Me.StartButton3.Text = Global.Outworldz.My.Resources.Resources.Start_word
        Me.StartButton3.UseVisualStyleBackColor = True
        '
        'RecycleButton2
        '
        Me.RecycleButton2.Image = Global.Outworldz.My.Resources.Resources.recycle
        Me.RecycleButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RecycleButton2.Location = New System.Drawing.Point(15, 135)
        Me.RecycleButton2.Name = "RecycleButton2"
        Me.RecycleButton2.Size = New System.Drawing.Size(125, 23)
        Me.RecycleButton2.TabIndex = 6
        Me.RecycleButton2.Text = Global.Outworldz.My.Resources.Resources.Restart_word
        Me.RecycleButton2.UseVisualStyleBackColor = True
        '
        'StopButton1
        '
        Me.StopButton1.Image = Global.Outworldz.My.Resources.Resources.media_stop_red1
        Me.StopButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StopButton1.Location = New System.Drawing.Point(15, 106)
        Me.StopButton1.Name = "StopButton1"
        Me.StopButton1.Size = New System.Drawing.Size(125, 23)
        Me.StopButton1.TabIndex = 5
        Me.StopButton1.Text = Global.Outworldz.My.Resources.Resources.Stop_word
        Me.StopButton1.UseVisualStyleBackColor = True
        '
        'StatsButton1
        '
        Me.StatsButton1.Image = Global.Outworldz.My.Resources.Resources.user1_into
        Me.StatsButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.StatsButton1.Location = New System.Drawing.Point(19, 48)
        Me.StatsButton1.Name = "StatsButton1"
        Me.StatsButton1.Size = New System.Drawing.Size(121, 23)
        Me.StatsButton1.TabIndex = 10
        Me.StatsButton1.Text = "Statistics"
        Me.StatsButton1.UseVisualStyleBackColor = True
        '
        'FormRegionPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(173, 215)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormRegionPopup"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents EditButton1 As Button
    Friend WithEvents StartButton3 As Button
    Friend WithEvents RecycleButton2 As Button
    Friend WithEvents StopButton1 As Button
    Friend WithEvents StatsButton As Button
    Friend WithEvents StatsButton1 As Button
End Class
