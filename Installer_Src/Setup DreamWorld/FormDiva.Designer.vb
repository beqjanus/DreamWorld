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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ApacheCheckbox = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.X86Button = New System.Windows.Forms.Button()
        Me.SearchAllRadioButton = New System.Windows.Forms.RadioButton()
        Me.SearchLocalRadioButton = New System.Windows.Forms.RadioButton()
        Me.ApachePort = New System.Windows.Forms.TextBox()
        Me.ApacheServiceCheckBox = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Web.SuspendLayout()
        CType(Me.WiFi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Web.Location = New System.Drawing.Point(12, 12)
        Me.Web.Name = "Web"
        Me.Web.Size = New System.Drawing.Size(200, 214)
        Me.Web.TabIndex = 48
        Me.Web.TabStop = False
        Me.Web.Text = "Wifi Interface Admin"
        '
        'WiFi
        '
        Me.WiFi.Image = Global.Outworldz.My.Resources.Resources.about
        Me.WiFi.Location = New System.Drawing.Point(143, 11)
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
        Me.WifiEnabled.Text = "Diva Wifi Enabled"
        Me.WifiEnabled.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(4, 139)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(62, 13)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "Notify Email"
        '
        'AdminEmail
        '
        Me.AdminEmail.Location = New System.Drawing.Point(86, 132)
        Me.AdminEmail.Name = "AdminEmail"
        Me.AdminEmail.Size = New System.Drawing.Size(100, 20)
        Me.AdminEmail.TabIndex = 30
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(4, 89)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 13)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Last Name"
        '
        'AccountConfirmationRequired
        '
        Me.AccountConfirmationRequired.AutoSize = True
        Me.AccountConfirmationRequired.Location = New System.Drawing.Point(19, 173)
        Me.AccountConfirmationRequired.Name = "AccountConfirmationRequired"
        Me.AccountConfirmationRequired.Size = New System.Drawing.Size(175, 17)
        Me.AccountConfirmationRequired.TabIndex = 31
        Me.AccountConfirmationRequired.Text = "Confirmation required to Log in?"
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
        Me.Label11.Text = "First Name"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 113)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Password"
        '
        'AdminPassword
        '
        Me.AdminPassword.Location = New System.Drawing.Point(86, 110)
        Me.AdminPassword.Name = "AdminPassword"
        Me.AdminPassword.Size = New System.Drawing.Size(100, 20)
        Me.AdminPassword.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.AdminPassword, "Can only change when Opensim is running")
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
        Me.GroupBox6.Location = New System.Drawing.Point(12, 238)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(200, 150)
        Me.GroupBox6.TabIndex = 1862
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "SMTP Send Email Account"
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
        Me.Label24.Size = New System.Drawing.Size(53, 13)
        Me.Label24.TabIndex = 1870
        Me.Label24.Text = "Smtp Port"
        '
        'SmtpHost
        '
        Me.SmtpHost.Location = New System.Drawing.Point(97, 81)
        Me.SmtpHost.Name = "SmtpHost"
        Me.SmtpHost.Size = New System.Drawing.Size(95, 20)
        Me.SmtpHost.TabIndex = 186735
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(9, 84)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(56, 13)
        Me.Label23.TabIndex = 1868
        Me.Label23.Text = "Smtp Host"
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
        Me.Label18.Text = "SMTP Password"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(7, 37)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 13)
        Me.Label14.TabIndex = 1865
        Me.Label14.Text = "Username"
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
        Me.Label2.Text = "This Grid's Friendly Name"
        '
        'GridName
        '
        Me.GridName.Location = New System.Drawing.Point(21, 83)
        Me.GridName.Name = "GridName"
        Me.GridName.Size = New System.Drawing.Size(219, 20)
        Me.GridName.TabIndex = 1869
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(18, 106)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(139, 13)
        Me.Label19.TabIndex = 1868
        Me.Label19.Text = "Viewer Splash Screen URL:"
        '
        'SplashPage
        '
        Me.SplashPage.Location = New System.Drawing.Point(18, 122)
        Me.SplashPage.Name = "SplashPage"
        Me.SplashPage.Size = New System.Drawing.Size(222, 20)
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
        Me.GroupBox1.Location = New System.Drawing.Point(230, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(279, 190)
        Me.GroupBox1.TabIndex = 186736
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Splash Screen"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 1876
        Me.Label4.Text = "Viewer Greeting"
        '
        'GreetingTextBox
        '
        Me.GreetingTextBox.Location = New System.Drawing.Point(18, 161)
        Me.GreetingTextBox.Name = "GreetingTextBox"
        Me.GreetingTextBox.Size = New System.Drawing.Size(222, 20)
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
        Me.CustomButton1.Text = "Custom"
        Me.CustomButton1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 1859
        Me.Label1.Text = "Theme:"
        '
        'WhiteRadioButton
        '
        Me.WhiteRadioButton.AutoSize = True
        Me.WhiteRadioButton.Location = New System.Drawing.Point(67, 32)
        Me.WhiteRadioButton.Name = "WhiteRadioButton"
        Me.WhiteRadioButton.Size = New System.Drawing.Size(53, 17)
        Me.WhiteRadioButton.TabIndex = 1873
        Me.WhiteRadioButton.TabStop = True
        Me.WhiteRadioButton.Text = "White"
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
        Me.BlackRadioButton.Text = "Black"
        Me.BlackRadioButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(184, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 186737
        Me.Label3.Text = "Web Port (80)"
        Me.ToolTip1.SetToolTip(Me.Label3, "80 or 8000")
        '
        'ApacheCheckbox
        '
        Me.ApacheCheckbox.AutoSize = True
        Me.ApacheCheckbox.Location = New System.Drawing.Point(18, 83)
        Me.ApacheCheckbox.Name = "ApacheCheckbox"
        Me.ApacheCheckbox.Size = New System.Drawing.Size(59, 17)
        Me.ApacheCheckbox.TabIndex = 1866
        Me.ApacheCheckbox.Text = "Enable"
        Me.ApacheCheckbox.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.X86Button)
        Me.GroupBox2.Controls.Add(Me.SearchAllRadioButton)
        Me.GroupBox2.Controls.Add(Me.SearchLocalRadioButton)
        Me.GroupBox2.Controls.Add(Me.ApachePort)
        Me.GroupBox2.Controls.Add(Me.ApacheServiceCheckBox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.ApacheCheckbox)
        Me.GroupBox2.Location = New System.Drawing.Point(230, 208)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(279, 180)
        Me.GroupBox2.TabIndex = 186738
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Apache Webserver + PHP"
        '
        'X86Button
        '
        Me.X86Button.Location = New System.Drawing.Point(18, 32)
        Me.X86Button.Name = "X86Button"
        Me.X86Button.Size = New System.Drawing.Size(156, 23)
        Me.X86Button.TabIndex = 186740
        Me.X86Button.Text = "Install C++ Runtime"
        Me.X86Button.UseVisualStyleBackColor = True
        '
        'SearchAllRadioButton
        '
        Me.SearchAllRadioButton.AutoSize = True
        Me.SearchAllRadioButton.Location = New System.Drawing.Point(18, 159)
        Me.SearchAllRadioButton.Name = "SearchAllRadioButton"
        Me.SearchAllRadioButton.Size = New System.Drawing.Size(100, 17)
        Me.SearchAllRadioButton.TabIndex = 186739
        Me.SearchAllRadioButton.TabStop = True
        Me.SearchAllRadioButton.Text = "Search All Grids"
        Me.SearchAllRadioButton.UseVisualStyleBackColor = True
        '
        'SearchLocalRadioButton
        '
        Me.SearchLocalRadioButton.AutoSize = True
        Me.SearchLocalRadioButton.Location = New System.Drawing.Point(18, 133)
        Me.SearchLocalRadioButton.Name = "SearchLocalRadioButton"
        Me.SearchLocalRadioButton.Size = New System.Drawing.Size(110, 17)
        Me.SearchLocalRadioButton.TabIndex = 186738
        Me.SearchLocalRadioButton.TabStop = True
        Me.SearchLocalRadioButton.Text = "Search Local Grid"
        Me.SearchLocalRadioButton.UseVisualStyleBackColor = True
        '
        'ApachePort
        '
        Me.ApachePort.Location = New System.Drawing.Point(145, 103)
        Me.ApachePort.Name = "ApachePort"
        Me.ApachePort.Size = New System.Drawing.Size(33, 20)
        Me.ApachePort.TabIndex = 186736
        '
        'ApacheServiceCheckBox
        '
        Me.ApacheServiceCheckBox.AutoSize = True
        Me.ApacheServiceCheckBox.Location = New System.Drawing.Point(18, 106)
        Me.ApacheServiceCheckBox.Name = "ApacheServiceCheckBox"
        Me.ApacheServiceCheckBox.Size = New System.Drawing.Size(113, 17)
        Me.ApacheServiceCheckBox.TabIndex = 1868
        Me.ApacheServiceCheckBox.Text = "Run  As A Service"
        Me.ApacheServiceCheckBox.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox1.Location = New System.Drawing.Point(187, 21)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 34)
        Me.PictureBox1.TabIndex = 1859
        Me.PictureBox1.TabStop = False
        '
        'FormDiva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(543, 424)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Web)
        Me.Controls.Add(Me.GroupBox6)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormDiva"
        Me.Text = "Web Server Panel"
        Me.Web.ResumeLayout(False)
        Me.Web.PerformLayout()
        CType(Me.WiFi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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
    Friend WithEvents ApacheCheckbox As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ApachePort As TextBox
    Friend WithEvents ApacheServiceCheckBox As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents SearchAllRadioButton As RadioButton
    Friend WithEvents SearchLocalRadioButton As RadioButton
    Friend WithEvents X86Button As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents GreetingTextBox As TextBox
End Class
