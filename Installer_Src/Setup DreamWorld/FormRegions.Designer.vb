<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRegions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRegions))
        Me.GroupBoxRegion = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button_Clear = New System.Windows.Forms.Button()
        Me.Button_Normalize = New System.Windows.Forms.Button()
        Me.TextBoxZ = New System.Windows.Forms.TextBox()
        Me.TextBoxY = New System.Windows.Forms.TextBox()
        Me.TextBoxX = New System.Windows.Forms.TextBox()
        Me.LabelNewUser = New System.Windows.Forms.Label()
        Me.LabelEditRegion = New System.Windows.Forms.Label()
        Me.Button_Region = New System.Windows.Forms.Button()
        Me.RegionBox = New System.Windows.Forms.ComboBox()
        Me.WelcomeBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button_AddRegion = New System.Windows.Forms.Button()
        Me.CheckboxConcierge = New System.Windows.Forms.CheckBox()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextToSpeechToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.GroupBoxConcierge = New System.Windows.Forms.GroupBox()
        Me.Whisper_distance = New System.Windows.Forms.TextBox()
        Me.Label_whisper_distance = New System.Windows.Forms.Label()
        Me.labelSay = New System.Windows.Forms.Label()
        Me.Say_Distance = New System.Windows.Forms.TextBox()
        Me.LabelShout = New System.Windows.Forms.Label()
        Me.Shout_Distance = New System.Windows.Forms.TextBox()
        Me.GroupBoxChat = New System.Windows.Forms.GroupBox()
        Me.GroupBoxRegion.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.GroupBoxConcierge.SuspendLayout()
        Me.GroupBoxChat.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxRegion
        '
        Me.GroupBoxRegion.Controls.Add(Me.PictureBox2)
        Me.GroupBoxRegion.Controls.Add(Me.Button_Clear)
        Me.GroupBoxRegion.Controls.Add(Me.Button_Normalize)
        Me.GroupBoxRegion.Controls.Add(Me.TextBoxZ)
        Me.GroupBoxRegion.Controls.Add(Me.TextBoxY)
        Me.GroupBoxRegion.Controls.Add(Me.TextBoxX)
        Me.GroupBoxRegion.Controls.Add(Me.LabelNewUser)
        Me.GroupBoxRegion.Controls.Add(Me.LabelEditRegion)
        Me.GroupBoxRegion.Controls.Add(Me.Button_Region)
        Me.GroupBoxRegion.Controls.Add(Me.RegionBox)
        Me.GroupBoxRegion.Controls.Add(Me.WelcomeBox1)
        Me.GroupBoxRegion.Controls.Add(Me.Label3)
        Me.GroupBoxRegion.Controls.Add(Me.Button_AddRegion)
        Me.GroupBoxRegion.Location = New System.Drawing.Point(10, 35)
        Me.GroupBoxRegion.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBoxRegion.Name = "GroupBoxRegion"
        Me.GroupBoxRegion.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBoxRegion.Size = New System.Drawing.Size(216, 329)
        Me.GroupBoxRegion.TabIndex = 0
        Me.GroupBoxRegion.TabStop = False
        Me.GroupBoxRegion.Text = "Region"
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
        'Button_Clear
        '
        Me.Button_Clear.Image = Global.Outworldz.My.Resources.Resources.package_delete
        Me.Button_Clear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_Clear.Location = New System.Drawing.Point(16, 182)
        Me.Button_Clear.Margin = New System.Windows.Forms.Padding(1)
        Me.Button_Clear.Name = "Button_Clear"
        Me.Button_Clear.Size = New System.Drawing.Size(185, 35)
        Me.Button_Clear.TabIndex = 6
        Me.Button_Clear.Text = Global.Outworldz.My.Resources.Resources.ClearReg
        Me.Button_Clear.UseVisualStyleBackColor = True
        '
        'Button_Normalize
        '
        Me.Button_Normalize.Image = Global.Outworldz.My.Resources.Resources.package_preferences
        Me.Button_Normalize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_Normalize.Location = New System.Drawing.Point(16, 219)
        Me.Button_Normalize.Margin = New System.Windows.Forms.Padding(1)
        Me.Button_Normalize.Name = "Button_Normalize"
        Me.Button_Normalize.Size = New System.Drawing.Size(185, 35)
        Me.Button_Normalize.TabIndex = 7
        Me.Button_Normalize.Text = Global.Outworldz.My.Resources.Resources.NormalizeRegions
        Me.Button_Normalize.UseVisualStyleBackColor = True
        '
        'TextBoxZ
        '
        Me.TextBoxZ.Location = New System.Drawing.Point(108, 73)
        Me.TextBoxZ.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxZ.Name = "TextBoxZ"
        Me.TextBoxZ.Size = New System.Drawing.Size(29, 20)
        Me.TextBoxZ.TabIndex = 3
        '
        'TextBoxY
        '
        Me.TextBoxY.Location = New System.Drawing.Point(72, 73)
        Me.TextBoxY.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxY.Name = "TextBoxY"
        Me.TextBoxY.Size = New System.Drawing.Size(29, 20)
        Me.TextBoxY.TabIndex = 2
        '
        'TextBoxX
        '
        Me.TextBoxX.Location = New System.Drawing.Point(36, 73)
        Me.TextBoxX.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxX.Name = "TextBoxX"
        Me.TextBoxX.Size = New System.Drawing.Size(29, 20)
        Me.TextBoxX.TabIndex = 1
        '
        'LabelNewUser
        '
        Me.LabelNewUser.AutoSize = True
        Me.LabelNewUser.Location = New System.Drawing.Point(33, 56)
        Me.LabelNewUser.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LabelNewUser.Name = "LabelNewUser"
        Me.LabelNewUser.Size = New System.Drawing.Size(115, 13)
        Me.LabelNewUser.TabIndex = 1861
        Me.LabelNewUser.Text = "New User Home X,Y,Z"
        '
        'LabelEditRegion
        '
        Me.LabelEditRegion.AutoSize = True
        Me.LabelEditRegion.Location = New System.Drawing.Point(15, 274)
        Me.LabelEditRegion.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LabelEditRegion.Name = "LabelEditRegion"
        Me.LabelEditRegion.Size = New System.Drawing.Size(62, 13)
        Me.LabelEditRegion.TabIndex = 1860
        Me.LabelEditRegion.Text = "Edit Region"
        '
        'Button_Region
        '
        Me.Button_Region.Image = Global.Outworldz.My.Resources.Resources.package_find
        Me.Button_Region.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_Region.Location = New System.Drawing.Point(16, 145)
        Me.Button_Region.Margin = New System.Windows.Forms.Padding(1)
        Me.Button_Region.Name = "Button_Region"
        Me.Button_Region.Size = New System.Drawing.Size(185, 35)
        Me.Button_Region.TabIndex = 5
        Me.Button_Region.Text = Global.Outworldz.My.Resources.Resources.Configger
        Me.Button_Region.UseVisualStyleBackColor = True
        '
        'RegionBox
        '
        Me.RegionBox.AutoCompleteCustomSource.AddRange(New String() {"1 Hour", "4 Hour", "12 Hour", "Daily", "Weekly"})
        Me.RegionBox.FormattingEnabled = True
        Me.RegionBox.Items.AddRange(New Object() {Global.Outworldz.My.Resources.Resources.Choose_Region_word})
        Me.RegionBox.Location = New System.Drawing.Point(16, 290)
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
        'Button_AddRegion
        '
        Me.Button_AddRegion.Image = Global.Outworldz.My.Resources.Resources.package_add
        Me.Button_AddRegion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_AddRegion.Location = New System.Drawing.Point(16, 108)
        Me.Button_AddRegion.Margin = New System.Windows.Forms.Padding(1)
        Me.Button_AddRegion.Name = "Button_AddRegion"
        Me.Button_AddRegion.Size = New System.Drawing.Size(185, 35)
        Me.Button_AddRegion.TabIndex = 4
        Me.Button_AddRegion.Text = Global.Outworldz.My.Resources.Resources.Add_Region_word
        Me.Button_AddRegion.UseVisualStyleBackColor = True
        '
        'CheckboxConcierge
        '
        Me.CheckboxConcierge.AutoSize = True
        Me.CheckboxConcierge.Location = New System.Drawing.Point(15, 29)
        Me.CheckboxConcierge.Name = "CheckboxConcierge"
        Me.CheckboxConcierge.Size = New System.Drawing.Size(178, 17)
        Me.CheckboxConcierge.TabIndex = 9
        Me.CheckboxConcierge.Text = "Announce Visitors in region chat"
        Me.CheckboxConcierge.UseVisualStyleBackColor = True
        '
        'MenuStrip2
        '
        Me.MenuStrip2.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem30})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip2.Size = New System.Drawing.Size(464, 34)
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
        'TextToSpeechToolStripMenuItem
        '
        Me.TextToSpeechToolStripMenuItem.Name = "TextToSpeechToolStripMenuItem"
        Me.TextToSpeechToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.TextToSpeechToolStripMenuItem.Text = "Text To Speech"
        '
        'RegionsToolStripMenuItem
        '
        Me.RegionsToolStripMenuItem.Name = "RegionsToolStripMenuItem"
        Me.RegionsToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.RegionsToolStripMenuItem.Text = "Regions"
        '
        'GroupBoxConcierge
        '
        Me.GroupBoxConcierge.Controls.Add(Me.CheckboxConcierge)
        Me.GroupBoxConcierge.Location = New System.Drawing.Point(240, 37)
        Me.GroupBoxConcierge.Name = "GroupBoxConcierge"
        Me.GroupBoxConcierge.Size = New System.Drawing.Size(200, 60)
        Me.GroupBoxConcierge.TabIndex = 1888
        Me.GroupBoxConcierge.TabStop = False
        Me.GroupBoxConcierge.Text = "Concierge"
        '
        'Whisper_distance
        '
        Me.Whisper_distance.Location = New System.Drawing.Point(29, 35)
        Me.Whisper_distance.Margin = New System.Windows.Forms.Padding(1)
        Me.Whisper_distance.Name = "Whisper_distance"
        Me.Whisper_distance.Size = New System.Drawing.Size(29, 20)
        Me.Whisper_distance.TabIndex = 1889
        '
        'Label_whisper_distance
        '
        Me.Label_whisper_distance.AutoSize = True
        Me.Label_whisper_distance.Location = New System.Drawing.Point(70, 38)
        Me.Label_whisper_distance.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label_whisper_distance.Name = "Label_whisper_distance"
        Me.Label_whisper_distance.Size = New System.Drawing.Size(89, 13)
        Me.Label_whisper_distance.TabIndex = 1889
        Me.Label_whisper_distance.Text = "Whisper distance"
        '
        'labelSay
        '
        Me.labelSay.AutoSize = True
        Me.labelSay.Location = New System.Drawing.Point(70, 66)
        Me.labelSay.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.labelSay.Name = "labelSay"
        Me.labelSay.Size = New System.Drawing.Size(68, 13)
        Me.labelSay.TabIndex = 1890
        Me.labelSay.Text = "Say distance"
        '
        'Say_Distance
        '
        Me.Say_Distance.Location = New System.Drawing.Point(29, 63)
        Me.Say_Distance.Margin = New System.Windows.Forms.Padding(1)
        Me.Say_Distance.Name = "Say_Distance"
        Me.Say_Distance.Size = New System.Drawing.Size(29, 20)
        Me.Say_Distance.TabIndex = 1891
        '
        'LabelShout
        '
        Me.LabelShout.AutoSize = True
        Me.LabelShout.Location = New System.Drawing.Point(70, 95)
        Me.LabelShout.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.LabelShout.Name = "LabelShout"
        Me.LabelShout.Size = New System.Drawing.Size(78, 13)
        Me.LabelShout.TabIndex = 1892
        Me.LabelShout.Text = "Shout distance"
        '
        'Shout_Distance
        '
        Me.Shout_Distance.Location = New System.Drawing.Point(29, 92)
        Me.Shout_Distance.Margin = New System.Windows.Forms.Padding(1)
        Me.Shout_Distance.Name = "Shout_Distance"
        Me.Shout_Distance.Size = New System.Drawing.Size(29, 20)
        Me.Shout_Distance.TabIndex = 1893
        '
        'GroupBoxChat
        '
        Me.GroupBoxChat.Controls.Add(Me.Shout_Distance)
        Me.GroupBoxChat.Controls.Add(Me.LabelShout)
        Me.GroupBoxChat.Controls.Add(Me.labelSay)
        Me.GroupBoxChat.Controls.Add(Me.Say_Distance)
        Me.GroupBoxChat.Controls.Add(Me.Whisper_distance)
        Me.GroupBoxChat.Controls.Add(Me.Label_whisper_distance)
        Me.GroupBoxChat.Location = New System.Drawing.Point(240, 123)
        Me.GroupBoxChat.Name = "GroupBoxChat"
        Me.GroupBoxChat.Size = New System.Drawing.Size(200, 143)
        Me.GroupBoxChat.TabIndex = 1895
        Me.GroupBoxChat.TabStop = False
        Me.GroupBoxChat.Text = "Chat"
        '
        'FormRegions
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(464, 373)
        Me.Controls.Add(Me.GroupBoxChat)
        Me.Controls.Add(Me.GroupBoxConcierge)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.GroupBoxRegion)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.MaximizeBox = False
        Me.Name = "FormRegions"
        Me.Text = "Region"
        Me.GroupBoxRegion.ResumeLayout(False)
        Me.GroupBoxRegion.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.GroupBoxConcierge.ResumeLayout(False)
        Me.GroupBoxConcierge.PerformLayout()
        Me.GroupBoxChat.ResumeLayout(False)
        Me.GroupBoxChat.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBoxRegion As GroupBox
    Friend WithEvents WelcomeBox1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Button_AddRegion As Button
    Friend WithEvents Button_Region As Button
    Friend WithEvents LabelEditRegion As Label
    Friend WithEvents RegionBox As ComboBox
    Friend WithEvents TextBoxZ As TextBox
    Friend WithEvents TextBoxY As TextBox
    Friend WithEvents TextBoxX As TextBox
    Friend WithEvents LabelNewUser As Label
    Friend WithEvents Button_Normalize As Button
    Friend WithEvents Button_Clear As Button
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenuItem30 As ToolStripMenuItem
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents CheckboxConcierge As CheckBox
    Friend WithEvents TextToSpeechToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RegionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBoxConcierge As GroupBox
    Friend WithEvents Whisper_distance As TextBox
    Friend WithEvents Label_whisper_distance As Label
    Friend WithEvents labelSay As Label
    Friend WithEvents Say_Distance As TextBox
    Friend WithEvents LabelShout As Label
    Friend WithEvents Shout_Distance As TextBox
    Friend WithEvents GroupBoxChat As GroupBox
End Class
