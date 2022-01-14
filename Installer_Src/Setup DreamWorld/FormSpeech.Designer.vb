<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSpeech
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
        Me.GroupBoxSpeech = New System.Windows.Forms.GroupBox()
        Me.APILabel = New System.Windows.Forms.Label()
        Me.APIKeyTextBox = New System.Windows.Forms.TextBox()
        Me.SpeakButton = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.MakeSpeechButton = New System.Windows.Forms.Button()
        Me.SpeechBox = New System.Windows.Forms.ComboBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBoxSpeech.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxSpeech
        '
        Me.GroupBoxSpeech.Controls.Add(Me.APILabel)
        Me.GroupBoxSpeech.Controls.Add(Me.APIKeyTextBox)
        Me.GroupBoxSpeech.Controls.Add(Me.SpeakButton)
        Me.GroupBoxSpeech.Controls.Add(Me.Label4)
        Me.GroupBoxSpeech.Controls.Add(Me.PictureBox1)
        Me.GroupBoxSpeech.Controls.Add(Me.TextBox1)
        Me.GroupBoxSpeech.Controls.Add(Me.MakeSpeechButton)
        Me.GroupBoxSpeech.Controls.Add(Me.SpeechBox)
        Me.GroupBoxSpeech.Location = New System.Drawing.Point(12, 37)
        Me.GroupBoxSpeech.Name = "GroupBoxSpeech"
        Me.GroupBoxSpeech.Size = New System.Drawing.Size(422, 360)
        Me.GroupBoxSpeech.TabIndex = 1890
        Me.GroupBoxSpeech.TabStop = False
        Me.GroupBoxSpeech.Text = "Speech"
        '
        'APILabel
        '
        Me.APILabel.AutoSize = True
        Me.APILabel.Location = New System.Drawing.Point(259, 17)
        Me.APILabel.Name = "APILabel"
        Me.APILabel.Size = New System.Drawing.Size(24, 13)
        Me.APILabel.TabIndex = 1897
        Me.APILabel.Text = "API"
        '
        'APIKeyTextBox
        '
        Me.APIKeyTextBox.Location = New System.Drawing.Point(250, 33)
        Me.APIKeyTextBox.Name = "APIKeyTextBox"
        Me.APIKeyTextBox.Size = New System.Drawing.Size(141, 20)
        Me.APIKeyTextBox.TabIndex = 1896
        '
        'SpeakButton
        '
        Me.SpeakButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SpeakButton.Image = Global.Outworldz.My.Resources.Resources.loudspeaker
        Me.SpeakButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SpeakButton.Location = New System.Drawing.Point(250, 71)
        Me.SpeakButton.Margin = New System.Windows.Forms.Padding(1)
        Me.SpeakButton.Name = "SpeakButton"
        Me.SpeakButton.Size = New System.Drawing.Size(141, 35)
        Me.SpeakButton.TabIndex = 1895
        Me.SpeakButton.Text = "Speak"
        Me.SpeakButton.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(65, 331)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 1893
        Me.Label4.Text = "View Folder"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.document_view
        Me.PictureBox1.Location = New System.Drawing.Point(28, 322)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(31, 23)
        Me.PictureBox1.TabIndex = 1892
        Me.PictureBox1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(17, 132)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(374, 174)
        Me.TextBox1.TabIndex = 1891
        Me.TextBox1.Text = "M: Speaks with a Male Voice"
        '
        'MakeSpeechButton
        '
        Me.MakeSpeechButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MakeSpeechButton.Image = Global.Outworldz.My.Resources.Resources.loudspeaker
        Me.MakeSpeechButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MakeSpeechButton.Location = New System.Drawing.Point(17, 71)
        Me.MakeSpeechButton.Margin = New System.Windows.Forms.Padding(1)
        Me.MakeSpeechButton.Name = "MakeSpeechButton"
        Me.MakeSpeechButton.Size = New System.Drawing.Size(208, 35)
        Me.MakeSpeechButton.TabIndex = 1890
        Me.MakeSpeechButton.Text = "Make Speech (Wav + Mp3)"
        Me.MakeSpeechButton.UseVisualStyleBackColor = True
        '
        'SpeechBox
        '
        Me.SpeechBox.AutoCompleteCustomSource.AddRange(New String() {"1 Hour", "4 Hour", "12 Hour", "Daily", "Weekly"})
        Me.SpeechBox.FormattingEnabled = True
        Me.SpeechBox.Location = New System.Drawing.Point(17, 30)
        Me.SpeechBox.Margin = New System.Windows.Forms.Padding(1)
        Me.SpeechBox.MaxDropDownItems = 15
        Me.SpeechBox.Name = "SpeechBox"
        Me.SpeechBox.Size = New System.Drawing.Size(208, 21)
        Me.SpeechBox.Sorted = True
        Me.SpeechBox.TabIndex = 1889
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(446, 34)
        Me.MenuStrip2.TabIndex = 1889
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(72, 32)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'FormSpeech
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 408)
        Me.Controls.Add(Me.GroupBoxSpeech)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Name = "FormSpeech"
        Me.Text = "FormSpeech"
        Me.GroupBoxSpeech.ResumeLayout(False)
        Me.GroupBoxSpeech.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBoxSpeech As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents MakeSpeechButton As Button
    Friend WithEvents SpeechBox As ComboBox
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents SpeakButton As Button
    Friend WithEvents APILabel As Label
    Friend WithEvents APIKeyTextBox As TextBox
End Class
