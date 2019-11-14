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
        Me.Web.Location = New System.Drawing.Point(33, 60)
        Me.Web.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Web.Name = "Web"
        Me.Web.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Web.Size = New System.Drawing.Size(418, 321)
        Me.Web.TabIndex = 48
        Me.Web.TabStop = False
        Me.Web.Text = My.Resources.Wifi_interface
        '
        'WiFi
        '
        Me.WiFi.Image = Global.Outworldz.My.Resources.Resources.about
        Me.WiFi.Location = New System.Drawing.Point(285, 69)
        Me.WiFi.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.WiFi.Name = "WiFi"
        Me.WiFi.Size = New System.Drawing.Size(45, 51)
        Me.WiFi.TabIndex = 1858
        Me.WiFi.TabStop = False
        '
        'WifiEnabled
        '
        Me.WifiEnabled.AutoSize = True
        Me.WifiEnabled.Location = New System.Drawing.Point(28, 42)
        Me.WifiEnabled.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.WifiEnabled.Name = "WifiEnabled"
        Me.WifiEnabled.Size = New System.Drawing.Size(159, 24)
        Me.WifiEnabled.TabIndex = 26
        Me.WifiEnabled.Text = Global.Outworldz.My.Resources.Resources.DivaEnabled
        Me.WifiEnabled.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 208)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(92, 20)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = My.Resources.Notify_Email
        '
        'AdminEmail
        '
        Me.AdminEmail.Location = New System.Drawing.Point(129, 198)
        Me.AdminEmail.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdminEmail.Name = "AdminEmail"
        Me.AdminEmail.Size = New System.Drawing.Size(229, 26)
        Me.AdminEmail.TabIndex = 30
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 134)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(86, 20)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = My.Resources.Last_Name
        '
        'AccountConfirmationRequired
        '
        Me.AccountConfirmationRequired.AutoSize = True
        Me.AccountConfirmationRequired.Location = New System.Drawing.Point(28, 260)
        Me.AccountConfirmationRequired.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AccountConfirmationRequired.Name = "AccountConfirmationRequired"
        Me.AccountConfirmationRequired.Size = New System.Drawing.Size(261, 24)
        Me.AccountConfirmationRequired.TabIndex = 31
        Me.AccountConfirmationRequired.Text = Global.Outworldz.My.Resources.Resources.Confirm
        Me.AccountConfirmationRequired.UseVisualStyleBackColor = True
        '
        'AdminLast
        '
        Me.AdminLast.Location = New System.Drawing.Point(129, 129)
        Me.AdminLast.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdminLast.Name = "AdminLast"
        Me.AdminLast.Size = New System.Drawing.Size(148, 26)
        Me.AdminLast.TabIndex = 28
        '
        'AdminFirst
        '
        Me.AdminFirst.Location = New System.Drawing.Point(129, 96)
        Me.AdminFirst.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdminFirst.Name = "AdminFirst"
        Me.AdminFirst.Size = New System.Drawing.Size(148, 26)
        Me.AdminFirst.TabIndex = 27
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 100)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(86, 20)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = My.Resources.First_name
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 170)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 20)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = My.Resources.Password
        '
        'AdminPassword
        '
        Me.AdminPassword.Location = New System.Drawing.Point(129, 165)
        Me.AdminPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdminPassword.Name = "AdminPassword"
        Me.AdminPassword.Size = New System.Drawing.Size(148, 26)
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
        Me.GroupBox6.Location = New System.Drawing.Point(33, 399)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox6.Size = New System.Drawing.Size(418, 225)
        Me.GroupBox6.TabIndex = 1862
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = My.Resources.SMTP
        '
        'SmtpPort
        '
        Me.SmtpPort.Location = New System.Drawing.Point(144, 160)
        Me.SmtpPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SmtpPort.Name = "SmtpPort"
        Me.SmtpPort.Size = New System.Drawing.Size(48, 26)
        Me.SmtpPort.TabIndex = 36
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(15, 165)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(85, 20)
        Me.Label24.TabIndex = 1870
        Me.Label24.Text = My.Resources.SMTPPort_word
        '
        'SmtpHost
        '
        Me.SmtpHost.Location = New System.Drawing.Point(146, 122)
        Me.SmtpHost.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SmtpHost.Name = "SmtpHost"
        Me.SmtpHost.Size = New System.Drawing.Size(219, 26)
        Me.SmtpHost.TabIndex = 186735
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(14, 126)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(90, 20)
        Me.Label23.TabIndex = 1868
        Me.Label23.Text = My.Resources.SMTPHost_word
        '
        'GmailPassword
        '
        Me.GmailPassword.Location = New System.Drawing.Point(142, 82)
        Me.GmailPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GmailPassword.Name = "GmailPassword"
        Me.GmailPassword.Size = New System.Drawing.Size(140, 26)
        Me.GmailPassword.TabIndex = 34
        Me.GmailPassword.UseSystemPasswordChar = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(10, 87)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(125, 20)
        Me.Label18.TabIndex = 1866
        Me.Label18.Text = My.Resources.SMTPPassword_word
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(10, 56)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(89, 20)
        Me.Label14.TabIndex = 1865
        Me.Label14.Text = my.Resources.User_Name_word
        '
        'GmailUsername
        '
        Me.GmailUsername.Location = New System.Drawing.Point(142, 45)
        Me.GmailUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GmailUsername.Name = "GmailUsername"
        Me.GmailUsername.Size = New System.Drawing.Size(140, 26)
        Me.GmailUsername.TabIndex = 33
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 100)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(188, 20)
        Me.Label2.TabIndex = 1867
        Me.Label2.Text = My.Resources.Friendly
        '
        'GridName
        '
        Me.GridName.Location = New System.Drawing.Point(32, 124)
        Me.GridName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridName.Name = "GridName"
        Me.GridName.Size = New System.Drawing.Size(361, 26)
        Me.GridName.TabIndex = 1869
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(27, 159)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(206, 20)
        Me.Label19.TabIndex = 1868
        Me.Label19.Text = My.Resources.Splash
        '
        'SplashPage
        '
        Me.SplashPage.Location = New System.Drawing.Point(27, 183)
        Me.SplashPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplashPage.Name = "SplashPage"
        Me.SplashPage.Size = New System.Drawing.Size(366, 26)
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
        Me.GroupBox1.Location = New System.Drawing.Point(33, 646)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(418, 285)
        Me.GroupBox1.TabIndex = 186736
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = My.Resources.SplashScreen
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 218)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 20)
        Me.Label4.TabIndex = 1876
        Me.Label4.Text = My.Resources.Greeting
        '
        'GreetingTextBox
        '
        Me.GreetingTextBox.Location = New System.Drawing.Point(27, 242)
        Me.GreetingTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GreetingTextBox.Name = "GreetingTextBox"
        Me.GreetingTextBox.Size = New System.Drawing.Size(366, 26)
        Me.GreetingTextBox.TabIndex = 1875
        '
        'CustomButton1
        '
        Me.CustomButton1.AutoSize = True
        Me.CustomButton1.Location = New System.Drawing.Point(276, 48)
        Me.CustomButton1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CustomButton1.Name = "CustomButton1"
        Me.CustomButton1.Size = New System.Drawing.Size(89, 24)
        Me.CustomButton1.TabIndex = 1874
        Me.CustomButton1.TabStop = True
        Me.CustomButton1.Text = Global.Outworldz.My.Resources.Resources.Custom
        Me.CustomButton1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 48)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 20)
        Me.Label1.TabIndex = 1859
        Me.Label1.Text = My.Resources.Theme_word
        '
        'WhiteRadioButton
        '
        Me.WhiteRadioButton.AutoSize = True
        Me.WhiteRadioButton.Location = New System.Drawing.Point(100, 48)
        Me.WhiteRadioButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.WhiteRadioButton.Name = "WhiteRadioButton"
        Me.WhiteRadioButton.Size = New System.Drawing.Size(75, 24)
        Me.WhiteRadioButton.TabIndex = 1873
        Me.WhiteRadioButton.TabStop = True
        Me.WhiteRadioButton.Text = Global.Outworldz.My.Resources.Resources.White_word
        Me.WhiteRadioButton.UseVisualStyleBackColor = True
        '
        'BlackRadioButton
        '
        Me.BlackRadioButton.AutoSize = True
        Me.BlackRadioButton.Location = New System.Drawing.Point(189, 48)
        Me.BlackRadioButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BlackRadioButton.Name = "BlackRadioButton"
        Me.BlackRadioButton.Size = New System.Drawing.Size(73, 24)
        Me.BlackRadioButton.TabIndex = 1872
        Me.BlackRadioButton.TabStop = True
        Me.BlackRadioButton.Text = Global.Outworldz.My.Resources.Resources.Black_word
        Me.BlackRadioButton.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(467, 35)
        Me.MenuStrip1.TabIndex = 186739
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1, Me.ApacheToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(85, 29)
        Me.HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Resources.Help
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(195, 34)
        Me.HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Resources.DivaPanel
        '
        'ApacheToolStripMenuItem
        '
        Me.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ApacheToolStripMenuItem.Name = "ApacheToolStripMenuItem"
        Me.ApacheToolStripMenuItem.Size = New System.Drawing.Size(195, 34)
        Me.ApacheToolStripMenuItem.Text = "Apache"
        '
        'FormDiva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(467, 954)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Web)
        Me.Controls.Add(Me.GroupBox6)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "FormDiva"
        Me.Text = My.Resources.WebServerPanel
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
