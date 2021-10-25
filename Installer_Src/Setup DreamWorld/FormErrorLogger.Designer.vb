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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormErrorLogger))
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
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'QuitButton
        '
        Me.QuitButton.Location = New System.Drawing.Point(190, 270)
        Me.QuitButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.QuitButton.Name = "QuitButton"
        Me.QuitButton.Size = New System.Drawing.Size(81, 30)
        Me.QuitButton.TabIndex = 4
        Me.QuitButton.Text = "Cancel"
        Me.QuitButton.UseVisualStyleBackColor = True
        '
        'ReasonText
        '
        Me.ReasonText.Location = New System.Drawing.Point(9, 117)
        Me.ReasonText.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ReasonText.MaxLength = 5000
        Me.ReasonText.Multiline = True
        Me.ReasonText.Name = "ReasonText"
        Me.ReasonText.Size = New System.Drawing.Size(375, 83)
        Me.ReasonText.TabIndex = 0
        Me.ReasonText.Text = "Please tell us what may have caused this."
        '
        'SendButton
        '
        Me.SendButton.Location = New System.Drawing.Point(97, 270)
        Me.SendButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.SendButton.Name = "SendButton"
        Me.SendButton.Size = New System.Drawing.Size(81, 30)
        Me.SendButton.TabIndex = 3
        Me.SendButton.Text = "Send Report"
        Me.SendButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(75, 25)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(301, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "The system has crashed and must exit.  "
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Global.Outworldz.My.Resources.Resources.warning
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.warning
        Me.PictureBox1.InitialImage = Global.Outworldz.My.Resources.Resources.warning
        Me.PictureBox1.Location = New System.Drawing.Point(23, 10)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(43, 31)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'PrivacyButton
        '
        Me.PrivacyButton.Image = Global.Outworldz.My.Resources.Resources.edge
        Me.PrivacyButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrivacyButton.Location = New System.Drawing.Point(275, 270)
        Me.PrivacyButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PrivacyButton.Name = "PrivacyButton"
        Me.PrivacyButton.Size = New System.Drawing.Size(116, 30)
        Me.PrivacyButton.TabIndex = 5
        Me.PrivacyButton.Text = "Privacy Policy"
        Me.PrivacyButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrivacyButton.UseVisualStyleBackColor = True
        '
        'EmailTextBox
        '
        Me.EmailTextBox.Location = New System.Drawing.Point(97, 244)
        Me.EmailTextBox.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.EmailTextBox.Name = "EmailTextBox"
        Me.EmailTextBox.Size = New System.Drawing.Size(230, 20)
        Me.EmailTextBox.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(36, 244)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 17)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Email"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.MenuText
        Me.TextBox1.Location = New System.Drawing.Point(9, 204)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox1.MaxLength = 5000
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(373, 38)
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
        Me.TextBox2.Location = New System.Drawing.Point(9, 56)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox2.MaxLength = 5000
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(373, 56)
        Me.TextBox2.TabIndex = 15
        Me.TextBox2.Text = "The system and other applications have not been affected. A report has been creat" &
    "ed that you can send to help identify this problem."
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(11, 270)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(73, 30)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "View"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FormErrorLogger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(402, 307)
        Me.Controls.Add(Me.Button1)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
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
    Friend WithEvents Button1 As Button
End Class
