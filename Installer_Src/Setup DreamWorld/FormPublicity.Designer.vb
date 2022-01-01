<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPublicity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPublicity))
        Me.GroupBoxPhoto = New System.Windows.Forms.GroupBox()
        Me.GDPRCheckBox = New System.Windows.Forms.CheckBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBoxCategory = New System.Windows.Forms.GroupBox()
        Me.CategoryCheckbox = New System.Windows.Forms.CheckedListBox()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.GroupBoxDescription = New System.Windows.Forms.GroupBox()
        Me.DescriptionBox = New System.Windows.Forms.TextBox()
        Me.ViewOutworldz = New System.Windows.Forms.Button()
        Me.GroupBoxPhoto.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.GroupBoxCategory.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxDescription.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxPhoto
        '
        Me.GroupBoxPhoto.Controls.Add(Me.GDPRCheckBox)
        Me.GroupBoxPhoto.Controls.Add(Me.PictureBox9)
        Me.GroupBoxPhoto.Location = New System.Drawing.Point(12, 29)
        Me.GroupBoxPhoto.Name = "GroupBoxPhoto"
        Me.GroupBoxPhoto.Size = New System.Drawing.Size(221, 188)
        Me.GroupBoxPhoto.TabIndex = 3
        Me.GroupBoxPhoto.TabStop = False
        Me.GroupBoxPhoto.Text = "Photo"
        '
        'GDPRCheckBox
        '
        Me.GDPRCheckBox.AutoSize = True
        Me.GDPRCheckBox.Location = New System.Drawing.Point(5, 19)
        Me.GDPRCheckBox.Name = "GDPRCheckBox"
        Me.GDPRCheckBox.Size = New System.Drawing.Size(167, 17)
        Me.GDPRCheckBox.TabIndex = 0
        Me.GDPRCheckBox.Text = "Publish Grid"
        Me.GDPRCheckBox.UseVisualStyleBackColor = True
        '
        'PictureBox9
        '
        Me.PictureBox9.InitialImage = Global.Outworldz.My.Resources.Resources.ClicktoInsertPhoto
        Me.PictureBox9.Location = New System.Drawing.Point(5, 52)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(203, 123)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox9.TabIndex = 1864
        Me.PictureBox9.TabStop = False
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(701, 30)
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
        'GroupBoxCategory
        '
        Me.GroupBoxCategory.Controls.Add(Me.CategoryCheckbox)
        Me.GroupBoxCategory.Location = New System.Drawing.Point(239, 29)
        Me.GroupBoxCategory.Name = "GroupBoxCategory"
        Me.GroupBoxCategory.Size = New System.Drawing.Size(216, 188)
        Me.GroupBoxCategory.TabIndex = 1
        Me.GroupBoxCategory.TabStop = False
        Me.GroupBoxCategory.Text = "Category"
        '
        'CategoryCheckbox
        '
        Me.CategoryCheckbox.FormattingEnabled = True
        Me.CategoryCheckbox.Items.AddRange(New Object() {"Adult", "Art", "Charity", "Child Friendly", "Commercial", "Educational", "Education - School", "Education - College", "Experimental", "Fantasy", "Freebies", "Free Land", "Furry", "Hideout", "Hyperport", "Gaming", "LGBT", "Personal", "Newcomer Friendly", "Parks & Nature", "R-Rated", "Rental", "Residential", "Role play", "Romance", "Sandbox", "Sci-Fi", "Science", "Scripting", "Shopping", "Testing", "X-Rated"})
        Me.CategoryCheckbox.Location = New System.Drawing.Point(11, 19)
        Me.CategoryCheckbox.Name = "CategoryCheckbox"
        Me.CategoryCheckbox.Size = New System.Drawing.Size(204, 154)
        Me.CategoryCheckbox.TabIndex = 0
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'GroupBoxDescription
        '
        Me.GroupBoxDescription.Controls.Add(Me.DescriptionBox)
        Me.GroupBoxDescription.Location = New System.Drawing.Point(459, 28)
        Me.GroupBoxDescription.Name = "GroupBoxDescription"
        Me.GroupBoxDescription.Size = New System.Drawing.Size(221, 189)
        Me.GroupBoxDescription.TabIndex = 2
        Me.GroupBoxDescription.TabStop = False
        Me.GroupBoxDescription.Text = "Description"
        '
        'DescriptionBox
        '
        Me.DescriptionBox.Location = New System.Drawing.Point(3, 21)
        Me.DescriptionBox.Multiline = True
        Me.DescriptionBox.Name = "DescriptionBox"
        Me.DescriptionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DescriptionBox.Size = New System.Drawing.Size(215, 153)
        Me.DescriptionBox.TabIndex = 0
        '
        'ViewOutworldz
        '
        Me.ViewOutworldz.Location = New System.Drawing.Point(462, 223)
        Me.ViewOutworldz.Name = "ViewOutworldz"
        Me.ViewOutworldz.Size = New System.Drawing.Size(117, 23)
        Me.ViewOutworldz.TabIndex = 1
        Me.ViewOutworldz.Text = "View"
        Me.ViewOutworldz.UseVisualStyleBackColor = True
        '
        'FormPublicity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(701, 257)
        Me.Controls.Add(Me.ViewOutworldz)
        Me.Controls.Add(Me.GroupBoxDescription)
        Me.Controls.Add(Me.GroupBoxCategory)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBoxPhoto)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormPublicity"
        Me.Text = "Publicity"
        Me.GroupBoxPhoto.ResumeLayout(False)
        Me.GroupBoxPhoto.PerformLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.GroupBoxCategory.ResumeLayout(False)
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxDescription.ResumeLayout(False)
        Me.GroupBoxDescription.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBoxPhoto As GroupBox
    Friend WithEvents GDPRCheckBox As CheckBox
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents GroupBoxCategory As GroupBox
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents GroupBoxDescription As GroupBox
    Friend WithEvents DescriptionBox As TextBox
    Friend WithEvents CategoryCheckbox As CheckedListBox
    Friend WithEvents ViewOutworldz As Button
End Class
