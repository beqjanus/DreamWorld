<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRegions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Synth.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRegions))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ConciergeCheckbox = New System.Windows.Forms.CheckBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.NormalizeButton1 = New System.Windows.Forms.Button()
        Me.Z = New System.Windows.Forms.TextBox()
        Me.Y = New System.Windows.Forms.TextBox()
        Me.X = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RegionButton = New System.Windows.Forms.Button()
        Me.RegionBox = New System.Windows.Forms.ComboBox()
        Me.WelcomeBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AddRegion = New System.Windows.Forms.Button()
        Me.SpeechBox = New System.Windows.Forms.ComboBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.MakeSpeechButton = New System.Windows.Forms.Button()
        Me.TextToSpeechToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ConciergeCheckbox)
        Me.GroupBox2.Controls.Add(Me.PictureBox2)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.NormalizeButton1)
        Me.GroupBox2.Controls.Add(Me.Z)
        Me.GroupBox2.Controls.Add(Me.Y)
        Me.GroupBox2.Controls.Add(Me.X)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.RegionButton)
        Me.GroupBox2.Controls.Add(Me.RegionBox)
        Me.GroupBox2.Controls.Add(Me.WelcomeBox1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.AddRegion)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 35)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox2.Size = New System.Drawing.Size(216, 387)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Region"
        '
        'ConciergeCheckbox
        '
        Me.ConciergeCheckbox.AutoSize = True
        Me.ConciergeCheckbox.Location = New System.Drawing.Point(16, 332)
        Me.ConciergeCheckbox.Name = "ConciergeCheckbox"
        Me.ConciergeCheckbox.Size = New System.Drawing.Size(178, 17)
        Me.ConciergeCheckbox.TabIndex = 9
        Me.ConciergeCheckbox.Text = "Announce Visitors in region chat"
        Me.ConciergeCheckbox.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Outworldz.My.Resources.Resources.home
        Me.PictureBox2.Location = New System.Drawing.Point(13, 25)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 17)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 1888
        Me.PictureBox2.TabStop = False
        '
        'Button1
        '
        Me.Button1.Image = Global.Outworldz.My.Resources.Resources.package_delete
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(16, 182)
        Me.Button1.Margin = New System.Windows.Forms.Padding(1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(185, 35)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = Global.Outworldz.My.Resources.Resources.ClearReg
        Me.Button1.UseVisualStyleBackColor = True
        '
        'NormalizeButton1
        '
        Me.NormalizeButton1.Image = Global.Outworldz.My.Resources.Resources.package_preferences
        Me.NormalizeButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NormalizeButton1.Location = New System.Drawing.Point(14, 221)
        Me.NormalizeButton1.Margin = New System.Windows.Forms.Padding(1)
        Me.NormalizeButton1.Name = "NormalizeButton1"
        Me.NormalizeButton1.Size = New System.Drawing.Size(185, 35)
        Me.NormalizeButton1.TabIndex = 7
        Me.NormalizeButton1.Text = Global.Outworldz.My.Resources.Resources.NormalizeRegions
        Me.NormalizeButton1.UseVisualStyleBackColor = True
        '
        'Z
        '
        Me.Z.Location = New System.Drawing.Point(108, 73)
        Me.Z.Margin = New System.Windows.Forms.Padding(1)
        Me.Z.Name = "Z"
        Me.Z.Size = New System.Drawing.Size(29, 20)
        Me.Z.TabIndex = 3
        '
        'Y
        '
        Me.Y.Location = New System.Drawing.Point(72, 73)
        Me.Y.Margin = New System.Windows.Forms.Padding(1)
        Me.Y.Name = "Y"
        Me.Y.Size = New System.Drawing.Size(29, 20)
        Me.Y.TabIndex = 2
        '
        'X
        '
        Me.X.Location = New System.Drawing.Point(36, 73)
        Me.X.Margin = New System.Windows.Forms.Padding(1)
        Me.X.Name = "X"
        Me.X.Size = New System.Drawing.Size(29, 20)
        Me.X.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 56)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 1861
        Me.Label2.Text = "New User Home X,Y,Z"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 271)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 1860
        Me.Label1.Text = "Edit Region"
        '
        'RegionButton
        '
        Me.RegionButton.Image = Global.Outworldz.My.Resources.Resources.package_find
        Me.RegionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RegionButton.Location = New System.Drawing.Point(16, 145)
        Me.RegionButton.Margin = New System.Windows.Forms.Padding(1)
        Me.RegionButton.Name = "RegionButton"
        Me.RegionButton.Size = New System.Drawing.Size(185, 35)
        Me.RegionButton.TabIndex = 5
        Me.RegionButton.Text = Global.Outworldz.My.Resources.Resources.Configger
        Me.RegionButton.UseVisualStyleBackColor = True
        '
        'RegionBox
        '
        Me.RegionBox.AutoCompleteCustomSource.AddRange(New String() {"1 Hour", "4 Hour", "12 Hour", "Daily", "Weekly"})
        Me.RegionBox.FormattingEnabled = True
        Me.RegionBox.Items.AddRange(New Object() {Global.Outworldz.My.Resources.Resources.Choose_Region_word})
        Me.RegionBox.Location = New System.Drawing.Point(14, 287)
        Me.RegionBox.Margin = New System.Windows.Forms.Padding(1)
        Me.RegionBox.MaxDropDownItems = 15
        Me.RegionBox.Name = "RegionBox"
        Me.RegionBox.Size = New System.Drawing.Size(185, 21)
        Me.RegionBox.Sorted = True
        Me.RegionBox.TabIndex = 8
        '
        'WelcomeBox1
        '
        Me.WelcomeBox1.AutoCompleteCustomSource.AddRange(New String() {"1 Hour", "4 Hour", "12 Hour", "Daily", "Weekly"})
        Me.WelcomeBox1.FormattingEnabled = True
        Me.WelcomeBox1.Items.AddRange(New Object() {"Hourly", "6 hour", "12 Hour", "Daily", "2 days", "3 days", "4 days", "5 days", "6 days", "Weekly"})
        Me.WelcomeBox1.Location = New System.Drawing.Point(41, 25)
        Me.WelcomeBox1.Margin = New System.Windows.Forms.Padding(1)
        Me.WelcomeBox1.Name = "WelcomeBox1"
        Me.WelcomeBox1.Size = New System.Drawing.Size(148, 21)
        Me.WelcomeBox1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 29)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 28
        '
        'AddRegion
        '
        Me.AddRegion.Image = Global.Outworldz.My.Resources.Resources.package_add
        Me.AddRegion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddRegion.Location = New System.Drawing.Point(16, 108)
        Me.AddRegion.Margin = New System.Windows.Forms.Padding(1)
        Me.AddRegion.Name = "AddRegion"
        Me.AddRegion.Size = New System.Drawing.Size(185, 35)
        Me.AddRegion.TabIndex = 4
        Me.AddRegion.Text = Global.Outworldz.My.Resources.Resources.Add_Region_word
        Me.AddRegion.UseVisualStyleBackColor = True
        '
        'SpeechBox
        '
        Me.SpeechBox.AutoCompleteCustomSource.AddRange(New String() {"1 Hour", "4 Hour", "12 Hour", "Daily", "Weekly"})
        Me.SpeechBox.FormattingEnabled = True
        Me.SpeechBox.Location = New System.Drawing.Point(17, 30)
        Me.SpeechBox.Margin = New System.Windows.Forms.Padding(1)
        Me.SpeechBox.MaxDropDownItems = 15
        Me.SpeechBox.Name = "SpeechBox"
        Me.SpeechBox.Size = New System.Drawing.Size(232, 21)
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
        Me.MenuStrip2.Size = New System.Drawing.Size(687, 34)
        Me.MenuStrip2.TabIndex = 1887
        Me.MenuStrip2.Text = "0"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TextToSpeechToolStripMenuItem, Me.RegionsToolStripMenuItem})
        Me.ToolStripMenuItem30.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(72, 32)
        Me.ToolStripMenuItem30.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.MakeSpeechButton)
        Me.GroupBox1.Controls.Add(Me.SpeechBox)
        Me.GroupBox1.Location = New System.Drawing.Point(253, 37)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 360)
        Me.GroupBox1.TabIndex = 1888
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Speech"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(259, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 1894
        Me.Label5.Text = "Voice"
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
        Me.TextBox1.Size = New System.Drawing.Size(386, 174)
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
        Me.MakeSpeechButton.Size = New System.Drawing.Size(232, 35)
        Me.MakeSpeechButton.TabIndex = 1890
        Me.MakeSpeechButton.Text = "Make Speech (Wav + Mp3)"
        Me.MakeSpeechButton.UseVisualStyleBackColor = True
        '
        'TextToSpeechToolStripMenuItem
        '
        Me.TextToSpeechToolStripMenuItem.Name = "TextToSpeechToolStripMenuItem"
        Me.TextToSpeechToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.TextToSpeechToolStripMenuItem.Text = "Text To Speech"
        '
        'RegionsToolStripMenuItem
        '
        Me.RegionsToolStripMenuItem.Name = "RegionsToolStripMenuItem"
        Me.RegionsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RegionsToolStripMenuItem.Text = "Regions"
        '
        'FormRegions
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(687, 420)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.MaximizeBox = False
        Me.Name = "FormRegions"
        Me.Text = "Region"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents WelcomeBox1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents AddRegion As Button
    Friend WithEvents RegionButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents RegionBox As ComboBox
    Friend WithEvents Z As TextBox
    Friend WithEvents Y As TextBox
    Friend WithEvents X As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NormalizeButton1 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents ConciergeCheckbox As CheckBox
    Friend WithEvents SpeechBox As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MakeSpeechButton As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextToSpeechToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RegionsToolStripMenuItem As ToolStripMenuItem
End Class
