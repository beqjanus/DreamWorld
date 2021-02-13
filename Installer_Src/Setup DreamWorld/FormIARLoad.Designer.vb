<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormIARLoad
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
        Me.FolderTextbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.AviName = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FolderTextbox
        '
        Me.FolderTextbox.Location = New System.Drawing.Point(21, 97)
        Me.FolderTextbox.Name = "FolderTextbox"
        Me.FolderTextbox.Size = New System.Drawing.Size(180, 22)
        Me.FolderTextbox.TabIndex = 0
        Me.FolderTextbox.Text = "/"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(246, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Enter first and last name of the avatar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Folder to save to"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.AviName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.FolderTextbox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(421, 173)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(25, 136)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 31)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'AviName
        '
        Me.AviName.Location = New System.Drawing.Point(25, 54)
        Me.AviName.Name = "AviName"
        Me.AviName.Size = New System.Drawing.Size(243, 22)
        Me.AviName.TabIndex = 3
        '
        'FormIARLoad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 198)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormIARLoad"
        Me.Text = "IAR"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FolderTextbox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents AviName As TextBox
End Class
