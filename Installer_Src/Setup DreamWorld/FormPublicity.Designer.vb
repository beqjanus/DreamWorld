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
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DatabaseSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox11.SuspendLayout()
        CType(Me.PublicPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.PublicPhoto)
        Me.GroupBox11.Controls.Add(Me.GDPRCheckBox)
        Me.GroupBox11.Controls.Add(Me.PictureBox9)
        Me.GroupBox11.Location = New System.Drawing.Point(12, 30)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(221, 183)
        Me.GroupBox11.TabIndex = 1866
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = My.Resources.Public_Photo
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
        Me.GDPRCheckBox.Text = My.Resources.Publish_grid
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
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(245, 24)
        Me.MenuStrip2.TabIndex = 1889
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSetupToolStripMenuItem})
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(44, 20)
        Me.ToolStripMenuItem30.Text = My.Resources.Help
        '
        'DatabaseSetupToolStripMenuItem
        '
        Me.DatabaseSetupToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.about
        Me.DatabaseSetupToolStripMenuItem.Name = "DatabaseSetupToolStripMenuItem"
        Me.DatabaseSetupToolStripMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.DatabaseSetupToolStripMenuItem.Text = My.Resources.Help
        '
        'FormPublicity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(245, 230)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox11)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormPublicity"
        Me.Text = My.Resources.Publicity
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        CType(Me.PublicPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox11 As GroupBox
    Friend WithEvents PublicPhoto As PictureBox
    Friend WithEvents GDPRCheckBox As CheckBox
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents DatabaseSetupToolStripMenuItem As ToolStripMenuItem
End Class
