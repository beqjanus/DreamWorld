<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDiva
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDiva))
        Me.Web = New System.Windows.Forms.GroupBox()
        Me.WiFi = New System.Windows.Forms.PictureBox()
        Me.WifiEnabled = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.AdminEmail = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.AccountConfirmationRequired = New System.Windows.Forms.CheckBox()
        Me.AdminLast = New System.Windows.Forms.TextBox()
        Me.AdminFirst = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.AdminPassword = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.SmtpPort = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.SmtpHost = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GmailPassword = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GmailUsername = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GridName = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.SplashPage = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GreetingTextBox = New System.Windows.Forms.TextBox()
        Me.CustomButton1 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.WhiteRadioButton = New System.Windows.Forms.RadioButton()
        Me.BlackRadioButton = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Web.SuspendLayout()
        CType(Me.WiFi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Web
        '
        Me.Web.Controls.Add(Me.WiFi)
        Me.Web.Controls.Add(Me.WifiEnabled)
        Me.Web.Controls.Add(Me.Label17)
        Me.Web.Controls.Add(Me.AdminEmail)
        Me.Web.Controls.Add(Me.Label12)
        Me.Web.Controls.Add(Me.AccountConfirmationRequired)
        Me.Web.Controls.Add(Me.AdminLast)
        Me.Web.Controls.Add(Me.AdminFirst)
        Me.Web.Controls.Add(Me.Label11)
        Me.Web.Controls.Add(Me.Label10)
        Me.Web.Controls.Add(Me.AdminPassword)
        Me.Web.Location = New System.Drawing.Point(22, 40)
        Me.Web.Name = "Web"
        Me.Web.Size = New System.Drawing.Size(279, 214)
        Me.Web.TabIndex = 48
        Me.Web.TabStop = False
        Me.Web.Text = Global.Outworldz.My.Resources.Resources.Wifi_interface
        '
        'WiFi
        '
        Me.WiFi.Image = Global.Outworldz.My.Resources.Resources.about
        Me.WiFi.Location = New System.Drawing.Point(190, 46)
        Me.WiFi.Name = "WiFi"
        Me.WiFi.Size = New System.Drawing.Size(30, 34)
        Me.WiFi.TabIndex = 1858
        Me.WiFi.TabStop = False
        '
        'WifiEnabled
        '
        Me.WifiEnabled.AutoSize = True
        Me.WifiEnabled.Location = New System.Drawing.Point(19, 28)
        Me.WifiEnabled.Name = "WifiEnabled"
        Me.WifiEnabled.Size = New System.Drawing.Size(111, 17)
        Me.WifiEnabled.TabIndex = 26
        Me.WifiEnabled.Text = Global.Outworldz.My.Resources.Resources.Diva_Wifi_Enabled_word
        Me.WifiEnabled.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(4, 139)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(62, 13)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = Global.Outworldz.My.Resources.Resources.Notify_Email
        '
        'AdminEmail
        '
        Me.AdminEmail.Location = New System.Drawing.Point(86, 132)
        Me.AdminEmail.Name = "AdminEmail"
        Me.AdminEmail.Size = New System.Drawing.Size(154, 20)
        Me.AdminEmail.TabIndex = 30
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(4, 89)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 13)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = Global.Outworldz.My.Resources.Resources.Last_Name_Word
        '
        'AccountConfirmationRequired
        '
        Me.AccountConfirmationRequired.AutoSize = True
        Me.AccountConfirmationRequired.Location = New System.Drawing.Point(19, 173)
        Me.AccountConfirmationRequired.Name = "AccountConfirmationRequired"
        Me.AccountConfirmationRequired.Size = New System.Drawing.Size(171, 17)
        Me.AccountConfirmationRequired.TabIndex = 31
        Me.AccountConfirmationRequired.Text = Global.Outworldz.My.Resources.Resources.Confirm
        Me.AccountConfirmationRequired.UseVisualStyleBackColor = True
        '
        'AdminLast
        '
        Me.AdminLast.Location = New System.Drawing.Point(86, 86)
        Me.AdminLast.Name = "AdminLast"
        Me.AdminLast.Size = New System.Drawing.Size(100, 20)
        Me.AdminLast.TabIndex = 28
        '
        'AdminFirst
        '
        Me.AdminFirst.Location = New System.Drawing.Point(86, 64)
        Me.AdminFirst.Name = "AdminFirst"
        Me.AdminFirst.Size = New System.Drawing.Size(100, 20)
        Me.AdminFirst.TabIndex = 27
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 67)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(57, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = Global.Outworldz.My.Resources.Resources.First_name_word
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 113)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = Global.Outworldz.My.Resources.Resources.Password_word
        '
        'AdminPassword
        '
        Me.AdminPassword.Location = New System.Drawing.Point(86, 110)
        Me.AdminPassword.Name = "AdminPassword"
        Me.AdminPassword.Size = New System.Drawing.Size(100, 20)
        Me.AdminPassword.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.AdminPassword, Global.Outworldz.My.Resources.Resources.Password_Text)
        Me.AdminPassword.UseSystemPasswordChar = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.SmtpPort)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.SmtpHost)
        Me.GroupBox6.Controls.Add(Me.Label23)
        Me.GroupBox6.Controls.Add(Me.GmailPassword)
        Me.GroupBox6.Controls.Add(Me.Label18)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.GmailUsername)
        Me.GroupBox6.Location = New System.Drawing.Point(307, 42)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(279, 150)
        Me.GroupBox6.TabIndex = 1862
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = Global.Outworldz.My.Resources.Resources.SMTP
        '
        'SmtpPort
        '
        Me.SmtpPort.Location = New System.Drawing.Point(96, 107)
        Me.SmtpPort.Name = "SmtpPort"
        Me.SmtpPort.Size = New System.Drawing.Size(33, 20)
        Me.SmtpPort.TabIndex = 36
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(10, 110)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(59, 13)
        Me.Label24.TabIndex = 1870
        Me.Label24.Text = Global.Outworldz.My.Resources.Resources.SMTPPort_word
        '
        'SmtpHost
        '
        Me.SmtpHost.Location = New System.Drawing.Point(97, 81)
        Me.SmtpHost.Name = "SmtpHost"
        Me.SmtpHost.Size = New System.Drawing.Size(147, 20)
        Me.SmtpHost.TabIndex = 186735
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(9, 84)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(62, 13)
        Me.Label23.TabIndex = 1868
        Me.Label23.Text = Global.Outworldz.My.Resources.Resources.SMTPHost_word
        '
        'GmailPassword
        '
        Me.GmailPassword.Location = New System.Drawing.Point(95, 55)
        Me.GmailPassword.Name = "GmailPassword"
        Me.GmailPassword.Size = New System.Drawing.Size(95, 20)
        Me.GmailPassword.TabIndex = 34
        Me.GmailPassword.UseSystemPasswordChar = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(7, 58)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(86, 13)
        Me.Label18.TabIndex = 1866
        Me.Label18.Text = Global.Outworldz.My.Resources.Resources.SMTPPassword_word
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(7, 37)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(60, 13)
        Me.Label14.TabIndex = 1865
        Me.Label14.Text = Global.Outworldz.My.Resources.Resources.User_Name_word
        '
        'GmailUsername
        '
        Me.GmailUsername.Location = New System.Drawing.Point(95, 30)
        Me.GmailUsername.Name = "GmailUsername"
        Me.GmailUsername.Size = New System.Drawing.Size(95, 20)
        Me.GmailUsername.TabIndex = 33
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 13)
        Me.Label2.TabIndex = 1867
        Me.Label2.Text = Global.Outworldz.My.Resources.Resources.Friendly
        '
        'GridName
        '
        Me.GridName.Location = New System.Drawing.Point(21, 83)
        Me.GridName.Name = "GridName"
        Me.GridName.Size = New System.Drawing.Size(242, 20)
        Me.GridName.TabIndex = 1869
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(18, 106)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(139, 13)
        Me.Label19.TabIndex = 1868
        Me.Label19.Text = Global.Outworldz.My.Resources.Resources.SplashScreen
        '
        'SplashPage
        '
        Me.SplashPage.Location = New System.Drawing.Point(18, 122)
        Me.SplashPage.Name = "SplashPage"
        Me.SplashPage.Size = New System.Drawing.Size(245, 20)
        Me.SplashPage.TabIndex = 1866
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.GreetingTextBox)
        Me.GroupBox1.Controls.Add(Me.CustomButton1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.WhiteRadioButton)
        Me.GroupBox1.Controls.Add(Me.BlackRadioButton)
        Me.GroupBox1.Controls.Add(Me.GridName)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.SplashPage)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(592, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(279, 190)
        Me.GroupBox1.TabIndex = 186736
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = Global.Outworldz.My.Resources.Resources.SplashScreen
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 1876
        Me.Label4.Text = Global.Outworldz.My.Resources.Resources.Viewer_Greeting_word
        '
        'GreetingTextBox
        '
        Me.GreetingTextBox.Location = New System.Drawing.Point(18, 161)
        Me.GreetingTextBox.Name = "GreetingTextBox"
        Me.GreetingTextBox.Size = New System.Drawing.Size(245, 20)
        Me.GreetingTextBox.TabIndex = 1875
        '
        'CustomButton1
        '
        Me.CustomButton1.AutoSize = True
        Me.CustomButton1.Location = New System.Drawing.Point(184, 32)
        Me.CustomButton1.Name = "CustomButton1"
        Me.CustomButton1.Size = New System.Drawing.Size(60, 17)
        Me.CustomButton1.TabIndex = 1874
        Me.CustomButton1.TabStop = True
        Me.CustomButton1.Text = Global.Outworldz.My.Resources.Resources.Custom_word
        Me.CustomButton1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 1859
        Me.Label1.Text = Global.Outworldz.My.Resources.Resources.Theme_word
        '
        'WhiteRadioButton
        '
        Me.WhiteRadioButton.AutoSize = True
        Me.WhiteRadioButton.Location = New System.Drawing.Point(67, 32)
        Me.WhiteRadioButton.Name = "WhiteRadioButton"
        Me.WhiteRadioButton.Size = New System.Drawing.Size(53, 17)
        Me.WhiteRadioButton.TabIndex = 1873
        Me.WhiteRadioButton.TabStop = True
        Me.WhiteRadioButton.Text = Global.Outworldz.My.Resources.Resources.White_word
        Me.WhiteRadioButton.UseVisualStyleBackColor = True
        '
        'BlackRadioButton
        '
        Me.BlackRadioButton.AutoSize = True
        Me.BlackRadioButton.Location = New System.Drawing.Point(126, 32)
        Me.BlackRadioButton.Name = "BlackRadioButton"
        Me.BlackRadioButton.Size = New System.Drawing.Size(52, 17)
        Me.BlackRadioButton.TabIndex = 1872
        Me.BlackRadioButton.TabStop = True
        Me.BlackRadioButton.Text = Global.Outworldz.My.Resources.Resources.Black_word
        Me.BlackRadioButton.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(888, 26)
        Me.MenuStrip1.TabIndex = 186739
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1, Me.ApacheToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help_word
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(129, 22)
        Me.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.Diva_Panel_word
        '
        'ApacheToolStripMenuItem
        '
        Me.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ApacheToolStripMenuItem.Name = "ApacheToolStripMenuItem"
        Me.ApacheToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.ApacheToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Apache_word
        '
        'FormDiva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(888, 250)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Web)
        Me.Controls.Add(Me.GroupBox6)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormDiva"
        Me.Text = Global.Outworldz.My.Resources.Resources.WebServerPanel
        Me.Web.ResumeLayout(False)
        Me.Web.PerformLayout()
        CType(Me.WiFi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Web As GroupBox
    Friend WithEvents WiFi As PictureBox
    Friend WithEvents WifiEnabled As CheckBox
    Friend WithEvents Label17 As Label
    Friend WithEvents AdminEmail As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents AccountConfirmationRequired As CheckBox
    Friend WithEvents AdminLast As TextBox
    Friend WithEvents AdminFirst As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents AdminPassword As TextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents SmtpPort As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents SmtpHost As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents GmailPassword As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents GmailUsername As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GridName As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents SplashPage As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents WhiteRadioButton As RadioButton
    Friend WithEvents BlackRadioButton As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents CustomButton1 As RadioButton
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label4 As Label
    Friend WithEvents GreetingTextBox As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ApacheToolStripMenuItem As ToolStripMenuItem
End Class
