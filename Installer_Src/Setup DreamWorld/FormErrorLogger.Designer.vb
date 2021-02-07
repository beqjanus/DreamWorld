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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PrivacyButton = New System.Windows.Forms.Button()
        Me.EmailTextBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'QuitButton
        '
        Me.QuitButton.Location = New System.Drawing.Point(175, 327)
        Me.QuitButton.Name = "QuitButton"
        Me.QuitButton.Size = New System.Drawing.Size(131, 37)
        Me.QuitButton.TabIndex = 0
        Me.QuitButton.Text = "Cancel"
        Me.QuitButton.UseVisualStyleBackColor = True
        '
        'ReasonText
        '
        Me.ReasonText.Location = New System.Drawing.Point(12, 155)
        Me.ReasonText.MaxLength = 5000
        Me.ReasonText.Multiline = True
        Me.ReasonText.Name = "ReasonText"
        Me.ReasonText.Size = New System.Drawing.Size(498, 90)
        Me.ReasonText.TabIndex = 1
        Me.ReasonText.Text = "What happened?"
        '
        'SendButton
        '
        Me.SendButton.Location = New System.Drawing.Point(25, 327)
        Me.SendButton.Name = "SendButton"
        Me.SendButton.Size = New System.Drawing.Size(124, 37)
        Me.SendButton.TabIndex = 2
        Me.SendButton.Text = "Send Report"
        Me.SendButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(100, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(351, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "The system has crashed and must exit.  "
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Global.Outworldz.My.Resources.Resources.warning
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.warning
        Me.PictureBox1.InitialImage = Global.Outworldz.My.Resources.Resources.warning
        Me.PictureBox1.Location = New System.Drawing.Point(31, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(57, 38)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'PrivacyButton
        '
        Me.PrivacyButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.PrivacyButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrivacyButton.Location = New System.Drawing.Point(331, 327)
        Me.PrivacyButton.Name = "PrivacyButton"
        Me.PrivacyButton.Size = New System.Drawing.Size(155, 37)
        Me.PrivacyButton.TabIndex = 10
        Me.PrivacyButton.Text = "Privacy Policy"
        Me.PrivacyButton.UseVisualStyleBackColor = True
        '
        'EmailTextBox
        '
        Me.EmailTextBox.Location = New System.Drawing.Point(129, 300)
        Me.EmailTextBox.Name = "EmailTextBox"
        Me.EmailTextBox.Size = New System.Drawing.Size(305, 22)
        Me.EmailTextBox.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(48, 301)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 20)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Email"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.MenuText
        Me.TextBox1.Location = New System.Drawing.Point(12, 251)
        Me.TextBox1.MaxLength = 5000
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(498, 47)
        Me.TextBox1.TabIndex = 14
        Me.TextBox1.Text = "Your email address will allow us to contact you in case we need more information." &
    ""
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.MenuText
        Me.TextBox2.Location = New System.Drawing.Point(12, 69)
        Me.TextBox2.MaxLength = 5000
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(498, 69)
        Me.TextBox2.TabIndex = 15
        Me.TextBox2.Text = "The system and other applictions have not been affected. A report has been create" &
    "d that you can send to Outworldz, LLC to help identify this problem."
        '
        'FormErrorLogger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(536, 378)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.EmailTextBox)
        Me.Controls.Add(Me.PrivacyButton)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SendButton)
        Me.Controls.Add(Me.ReasonText)
        Me.Controls.Add(Me.QuitButton)
        Me.Name = "FormErrorLogger"
        Me.Text = "Help us out"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents QuitButton As Button
    Friend WithEvents ReasonText As TextBox
    Friend WithEvents SendButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PrivacyButton As Button
    Friend WithEvents EmailTextBox As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
End Class
