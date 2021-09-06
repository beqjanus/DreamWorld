<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormIARSaveAll
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
        Me.FilterGroupBox = New System.Windows.Forms.GroupBox()
        Me.CopyCheckBox = New System.Windows.Forms.CheckBox()
        Me.TransferCheckBox = New System.Windows.Forms.CheckBox()
        Me.ModifyCheckBox = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ObjectNameBox = New System.Windows.Forms.TextBox()
        Me.FilterGroupBox.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FilterGroupBox
        '
        Me.FilterGroupBox.Controls.Add(Me.CopyCheckBox)
        Me.FilterGroupBox.Controls.Add(Me.TransferCheckBox)
        Me.FilterGroupBox.Controls.Add(Me.ModifyCheckBox)
        Me.FilterGroupBox.Location = New System.Drawing.Point(405, 53)
        Me.FilterGroupBox.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.FilterGroupBox.Name = "FilterGroupBox"
        Me.FilterGroupBox.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.FilterGroupBox.Size = New System.Drawing.Size(113, 107)
        Me.FilterGroupBox.TabIndex = 7
        Me.FilterGroupBox.TabStop = False
        Me.FilterGroupBox.Text = "Filter"
        '
        'CopyCheckBox
        '
        Me.CopyCheckBox.AutoSize = True
        Me.CopyCheckBox.Checked = True
        Me.CopyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CopyCheckBox.Location = New System.Drawing.Point(12, 27)
        Me.CopyCheckBox.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CopyCheckBox.Name = "CopyCheckBox"
        Me.CopyCheckBox.Size = New System.Drawing.Size(50, 17)
        Me.CopyCheckBox.TabIndex = 0
        Me.CopyCheckBox.Text = "Copy"
        Me.CopyCheckBox.UseVisualStyleBackColor = True
        '
        'TransferCheckBox
        '
        Me.TransferCheckBox.AutoSize = True
        Me.TransferCheckBox.Checked = True
        Me.TransferCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TransferCheckBox.Location = New System.Drawing.Point(12, 71)
        Me.TransferCheckBox.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TransferCheckBox.Name = "TransferCheckBox"
        Me.TransferCheckBox.Size = New System.Drawing.Size(65, 17)
        Me.TransferCheckBox.TabIndex = 2
        Me.TransferCheckBox.Text = "Transfer"
        Me.TransferCheckBox.UseVisualStyleBackColor = True
        '
        'ModifyCheckBox
        '
        Me.ModifyCheckBox.AutoSize = True
        Me.ModifyCheckBox.Checked = True
        Me.ModifyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ModifyCheckBox.Location = New System.Drawing.Point(12, 49)
        Me.ModifyCheckBox.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ModifyCheckBox.Name = "ModifyCheckBox"
        Me.ModifyCheckBox.Size = New System.Drawing.Size(57, 17)
        Me.ModifyCheckBox.TabIndex = 1
        Me.ModifyCheckBox.Text = "Modify"
        Me.ModifyCheckBox.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(535, 24)
        Me.MenuStrip1.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(35, 125)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.Save_IAR_word
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ObjectNameBox)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 46)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(369, 73)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Save Inventory IAR"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(221, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Object Path and name"
        '
        'ObjectNameBox
        '
        Me.ObjectNameBox.Location = New System.Drawing.Point(5, 25)
        Me.ObjectNameBox.Name = "ObjectNameBox"
        Me.ObjectNameBox.Size = New System.Drawing.Size(210, 20)
        Me.ObjectNameBox.TabIndex = 0
        Me.ObjectNameBox.Text = "/=everything, /Objects/Folder, etc."
        '
        'FormIARSaveAll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(535, 169)
        Me.Controls.Add(Me.FilterGroupBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormIARSaveAll"
        Me.Text = "FormIARSaveAll"
        Me.FilterGroupBox.ResumeLayout(False)
        Me.FilterGroupBox.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FilterGroupBox As GroupBox
    Friend WithEvents CopyCheckBox As CheckBox
    Friend WithEvents TransferCheckBox As CheckBox
    Friend WithEvents ModifyCheckBox As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ObjectNameBox As TextBox
End Class
