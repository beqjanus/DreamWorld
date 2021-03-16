<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDebug
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.RadioFalse = New System.Windows.Forms.RadioButton()
        Me.RadioTrue = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.RadioFalse)
        Me.GroupBox1.Controls.Add(Me.RadioTrue)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(427, 253)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Debug"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {""})
        Me.ComboBox1.Location = New System.Drawing.Point(20, 30)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(149, 21)
        Me.ComboBox1.TabIndex = 6
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(291, 28)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 23)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Apply"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(6, 97)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(415, 150)
        Me.TextBox1.TabIndex = 3
        '
        'RadioFalse
        '
        Me.RadioFalse.AutoSize = True
        Me.RadioFalse.Location = New System.Drawing.Point(199, 44)
        Me.RadioFalse.Name = "RadioFalse"
        Me.RadioFalse.Size = New System.Drawing.Size(50, 17)
        Me.RadioFalse.TabIndex = 2
        Me.RadioFalse.TabStop = True
        Me.RadioFalse.Text = "False"
        Me.RadioFalse.UseVisualStyleBackColor = True
        '
        'RadioTrue
        '
        Me.RadioTrue.AutoSize = True
        Me.RadioTrue.Location = New System.Drawing.Point(199, 21)
        Me.RadioTrue.Name = "RadioTrue"
        Me.RadioTrue.Size = New System.Drawing.Size(47, 17)
        Me.RadioTrue.TabIndex = 1
        Me.RadioTrue.TabStop = True
        Me.RadioTrue.Text = "True"
        Me.RadioTrue.UseVisualStyleBackColor = True
        '
        'FormDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 286)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormDebug"
        Me.Text = "Debug"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioFalse As RadioButton
    Friend WithEvents RadioTrue As RadioButton
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button2 As Button
End Class
