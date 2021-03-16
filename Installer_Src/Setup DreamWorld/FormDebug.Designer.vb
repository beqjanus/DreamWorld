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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.RadioTrue = New System.Windows.Forms.RadioButton()
        Me.RadioFalse = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioFalse)
        Me.GroupBox1.Controls.Add(Me.RadioTrue)
        Me.GroupBox1.Controls.Add(Me.ListBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(307, 100)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Debug"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Items.AddRange(New Object() {"Smart Start", "Load Free Oars"})
        Me.ListBox1.Location = New System.Drawing.Point(18, 34)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(137, 17)
        Me.ListBox1.TabIndex = 0
        '
        'RadioTrue
        '
        Me.RadioTrue.AutoSize = True
        Me.RadioTrue.Location = New System.Drawing.Point(209, 34)
        Me.RadioTrue.Name = "RadioTrue"
        Me.RadioTrue.Size = New System.Drawing.Size(47, 17)
        Me.RadioTrue.TabIndex = 1
        Me.RadioTrue.TabStop = True
        Me.RadioTrue.Text = "True"
        Me.RadioTrue.UseVisualStyleBackColor = True
        '
        'RadioFalse
        '
        Me.RadioFalse.AutoSize = True
        Me.RadioFalse.Location = New System.Drawing.Point(209, 57)
        Me.RadioFalse.Name = "RadioFalse"
        Me.RadioFalse.Size = New System.Drawing.Size(50, 17)
        Me.RadioFalse.TabIndex = 2
        Me.RadioFalse.TabStop = True
        Me.RadioFalse.Text = "False"
        Me.RadioFalse.UseVisualStyleBackColor = True
        '
        'FormDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 134)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormDebug"
        Me.Text = "Debug"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents RadioFalse As RadioButton
    Friend WithEvents RadioTrue As RadioButton
End Class
