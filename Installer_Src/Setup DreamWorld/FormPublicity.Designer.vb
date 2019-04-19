<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPublicity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPublicity))
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.PublicPhoto = New System.Windows.Forms.PictureBox()
        Me.GDPRCheckBox = New System.Windows.Forms.CheckBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.GroupBox11.SuspendLayout()
        CType(Me.PublicPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.PublicPhoto)
        Me.GroupBox11.Controls.Add(Me.GDPRCheckBox)
        Me.GroupBox11.Controls.Add(Me.PictureBox9)
        Me.GroupBox11.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(221, 183)
        Me.GroupBox11.TabIndex = 1866
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Public Photo"
        '
        'PublicPhoto
        '
        Me.PublicPhoto.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PublicPhoto.Location = New System.Drawing.Point(174, 12)
        Me.PublicPhoto.Name = "PublicPhoto"
        Me.PublicPhoto.Size = New System.Drawing.Size(28, 27)
        Me.PublicPhoto.TabIndex = 1865
        Me.PublicPhoto.TabStop = False
        '
        'GDPRCheckBox
        '
        Me.GDPRCheckBox.AutoSize = True
        Me.GDPRCheckBox.Location = New System.Drawing.Point(6, 19)
        Me.GDPRCheckBox.Name = "GDPRCheckBox"
        Me.GDPRCheckBox.Size = New System.Drawing.Size(162, 17)
        Me.GDPRCheckBox.TabIndex = 7
        Me.GDPRCheckBox.Text = "Publish Grid to Hyperica.com"
        Me.GDPRCheckBox.UseVisualStyleBackColor = True
        '
        'PictureBox9
        '
        Me.PictureBox9.InitialImage = Global.Outworldz.My.Resources.Resources.ClicktoInsertPhoto
        Me.PictureBox9.Location = New System.Drawing.Point(12, 45)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(203, 123)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox9.TabIndex = 1864
        Me.PictureBox9.TabStop = False
        '
        'FormPublicity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(245, 207)
        Me.Controls.Add(Me.GroupBox11)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormPublicity"
        Me.Text = "Publicity"
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        CType(Me.PublicPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox11 As GroupBox
    Friend WithEvents PublicPhoto As PictureBox
    Friend WithEvents GDPRCheckBox As CheckBox
    Friend WithEvents PictureBox9 As PictureBox
End Class
