<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormErrorLogger
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
        Me.QuitButton = New System.Windows.Forms.Button()
        Me.ReasonText = New System.Windows.Forms.TextBox()
        Me.SendButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'QuitButton
        '
        Me.QuitButton.Location = New System.Drawing.Point(255, 176)
        Me.QuitButton.Name = "QuitButton"
        Me.QuitButton.Size = New System.Drawing.Size(143, 39)
        Me.QuitButton.TabIndex = 0
        Me.QuitButton.Text = "Quit"
        Me.QuitButton.UseVisualStyleBackColor = True
        '
        'ReasonText
        '
        Me.ReasonText.Location = New System.Drawing.Point(12, 68)
        Me.ReasonText.MaxLength = 5000
        Me.ReasonText.Multiline = True
        Me.ReasonText.Name = "ReasonText"
        Me.ReasonText.Size = New System.Drawing.Size(492, 90)
        Me.ReasonText.TabIndex = 1
        '
        'SendButton
        '
        Me.SendButton.Location = New System.Drawing.Point(31, 176)
        Me.SendButton.Name = "SendButton"
        Me.SendButton.Size = New System.Drawing.Size(171, 39)
        Me.SendButton.TabIndex = 2
        Me.SendButton.Text = "Send"
        Me.SendButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(262, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "The system has crashed and must exit.  "
        '
        'FormErrorLogger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(543, 237)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SendButton)
        Me.Controls.Add(Me.ReasonText)
        Me.Controls.Add(Me.QuitButton)
        Me.Name = "FormErrorLogger"
        Me.Text = "Help us out"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents QuitButton As Button
    Friend WithEvents ReasonText As TextBox
    Friend WithEvents SendButton As Button
    Friend WithEvents Label1 As Label
End Class
