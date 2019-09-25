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
        Me.LocalSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.AllGridSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.EnableSearchCheckBox = New System.Windows.Forms.CheckBox()
        Me.EventsCheckBox = New System.Windows.Forms.CheckBox()
        Me.ApacheCheckbox = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.X86Button = New System.Windows.Forms.Button()
        Me.ApachePort = New System.Windows.Forms.TextBox()
        Me.ApacheServiceCheckBox = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Web.SuspendLayout()
        CType(Me.WiFi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Web.Location = New System.Drawing.Point(28, 50)
        Me.Web.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Web.Name = "Web"
        Me.Web.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Web.Size = New System.Drawing.Size(250, 268)
        Me.Web.TabIndex = 48
        Me.Web.TabStop = False
        Me.Web.Text = "Wifi Interface Admin"
        '
        'WiFi
        '
        Me.WiFi.Image = Global.Outworldz.My.Resources.Resources.about
        Me.WiFi.Location = New System.Drawing.Point(179, 14)
        Me.WiFi.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.WiFi.Name = "WiFi"
        Me.WiFi.Size = New System.Drawing.Size(38, 42)
        Me.WiFi.TabIndex = 1858
        Me.WiFi.TabStop = False
        '
        'WifiEnabled
        '
        Me.WifiEnabled.AutoSize = True
        Me.WifiEnabled.Location = New System.Drawing.Point(24, 35)
        Me.WifiEnabled.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.WifiEnabled.Name = "WifiEnabled"
        Me.WifiEnabled.Size = New System.Drawing.Size(141, 21)
        Me.WifiEnabled.TabIndex = 26
        Me.WifiEnabled.Text = "Diva Wifi Enabled"
        Me.WifiEnabled.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(5, 174)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 17)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "Notify Email"
        '
        'AdminEmail
        '
        Me.AdminEmail.Location = New System.Drawing.Point(108, 165)
        Me.AdminEmail.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdminEmail.Name = "AdminEmail"
        Me.AdminEmail.Size = New System.Drawing.Size(124, 22)
        Me.AdminEmail.TabIndex = 30
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(5, 111)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 17)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Last Name"
        '
        'AccountConfirmationRequired
        '
        Me.AccountConfirmationRequired.AutoSize = True
        Me.AccountConfirmationRequired.Location = New System.Drawing.Point(24, 216)
        Me.AccountConfirmationRequired.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AccountConfirmationRequired.Name = "AccountConfirmationRequired"
        Me.AccountConfirmationRequired.Size = New System.Drawing.Size(233, 21)
        Me.AccountConfirmationRequired.TabIndex = 31
        Me.AccountConfirmationRequired.Text = "Confirmation required to Log in?"
        Me.AccountConfirmationRequired.UseVisualStyleBackColor = True
        '
        'AdminLast
        '
        Me.AdminLast.Location = New System.Drawing.Point(108, 108)
        Me.AdminLast.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdminLast.Name = "AdminLast"
        Me.AdminLast.Size = New System.Drawing.Size(124, 22)
        Me.AdminLast.TabIndex = 28
        '
        'AdminFirst
        '
        Me.AdminFirst.Location = New System.Drawing.Point(108, 80)
        Me.AdminFirst.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdminFirst.Name = "AdminFirst"
        Me.AdminFirst.Size = New System.Drawing.Size(124, 22)
        Me.AdminFirst.TabIndex = 27
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(5, 84)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 17)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "First Name"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(5, 141)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 17)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Password"
        '
        'AdminPassword
        '
        Me.AdminPassword.Location = New System.Drawing.Point(108, 138)
        Me.AdminPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AdminPassword.Name = "AdminPassword"
        Me.AdminPassword.Size = New System.Drawing.Size(124, 22)
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
        Me.GroupBox6.Location = New System.Drawing.Point(28, 332)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox6.Size = New System.Drawing.Size(250, 188)
        Me.GroupBox6.TabIndex = 1862
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "SMTP Send Email Account"
        '
        'SmtpPort
        '
        Me.SmtpPort.Location = New System.Drawing.Point(120, 134)
        Me.SmtpPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SmtpPort.Name = "SmtpPort"
        Me.SmtpPort.Size = New System.Drawing.Size(40, 22)
        Me.SmtpPort.TabIndex = 36
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(12, 138)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(70, 17)
        Me.Label24.TabIndex = 1870
        Me.Label24.Text = "Smtp Port"
        '
        'SmtpHost
        '
        Me.SmtpHost.Location = New System.Drawing.Point(121, 101)
        Me.SmtpHost.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SmtpHost.Name = "SmtpHost"
        Me.SmtpHost.Size = New System.Drawing.Size(118, 22)
        Me.SmtpHost.TabIndex = 186735
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(11, 105)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(73, 17)
        Me.Label23.TabIndex = 1868
        Me.Label23.Text = "Smtp Host"
        '
        'GmailPassword
        '
        Me.GmailPassword.Location = New System.Drawing.Point(119, 69)
        Me.GmailPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GmailPassword.Name = "GmailPassword"
        Me.GmailPassword.Size = New System.Drawing.Size(118, 22)
        Me.GmailPassword.TabIndex = 34
        Me.GmailPassword.UseSystemPasswordChar = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 72)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(111, 17)
        Me.Label18.TabIndex = 1866
        Me.Label18.Text = "SMTP Password"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 46)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 17)
        Me.Label14.TabIndex = 1865
        Me.Label14.Text = "Username"
        '
        'GmailUsername
        '
        Me.GmailUsername.Location = New System.Drawing.Point(119, 38)
        Me.GmailUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GmailUsername.Name = "GmailUsername"
        Me.GmailUsername.Size = New System.Drawing.Size(118, 22)
        Me.GmailUsername.TabIndex = 33
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 84)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(171, 17)
        Me.Label2.TabIndex = 1867
        Me.Label2.Text = "This Grid's Friendly Name"
        '
        'GridName
        '
        Me.GridName.Location = New System.Drawing.Point(26, 104)
        Me.GridName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridName.Name = "GridName"
        Me.GridName.Size = New System.Drawing.Size(273, 22)
        Me.GridName.TabIndex = 1869
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(22, 132)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(182, 17)
        Me.Label19.TabIndex = 1868
        Me.Label19.Text = "Viewer Splash Screen URL:"
        '
        'SplashPage
        '
        Me.SplashPage.Location = New System.Drawing.Point(22, 152)
        Me.SplashPage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplashPage.Name = "SplashPage"
        Me.SplashPage.Size = New System.Drawing.Size(276, 22)
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
        Me.GroupBox1.Location = New System.Drawing.Point(300, 50)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(349, 238)
        Me.GroupBox1.TabIndex = 186736
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Splash Screen"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 181)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 17)
        Me.Label4.TabIndex = 1876
        Me.Label4.Text = "Viewer Greeting"
        '
        'GreetingTextBox
        '
        Me.GreetingTextBox.Location = New System.Drawing.Point(22, 201)
        Me.GreetingTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GreetingTextBox.Name = "GreetingTextBox"
        Me.GreetingTextBox.Size = New System.Drawing.Size(276, 22)
        Me.GreetingTextBox.TabIndex = 1875
        '
        'CustomButton1
        '
        Me.CustomButton1.AutoSize = True
        Me.CustomButton1.Location = New System.Drawing.Point(230, 40)
        Me.CustomButton1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CustomButton1.Name = "CustomButton1"
        Me.CustomButton1.Size = New System.Drawing.Size(76, 21)
        Me.CustomButton1.TabIndex = 1874
        Me.CustomButton1.TabStop = True
        Me.CustomButton1.Text = "Custom"
        Me.CustomButton1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 40)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 17)
        Me.Label1.TabIndex = 1859
        Me.Label1.Text = "Theme:"
        '
        'WhiteRadioButton
        '
        Me.WhiteRadioButton.AutoSize = True
        Me.WhiteRadioButton.Location = New System.Drawing.Point(84, 40)
        Me.WhiteRadioButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.WhiteRadioButton.Name = "WhiteRadioButton"
        Me.WhiteRadioButton.Size = New System.Drawing.Size(65, 21)
        Me.WhiteRadioButton.TabIndex = 1873
        Me.WhiteRadioButton.TabStop = True
        Me.WhiteRadioButton.Text = "White"
        Me.WhiteRadioButton.UseVisualStyleBackColor = True
        '
        'BlackRadioButton
        '
        Me.BlackRadioButton.AutoSize = True
        Me.BlackRadioButton.Location = New System.Drawing.Point(158, 40)
        Me.BlackRadioButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BlackRadioButton.Name = "BlackRadioButton"
        Me.BlackRadioButton.Size = New System.Drawing.Size(63, 21)
        Me.BlackRadioButton.TabIndex = 1872
        Me.BlackRadioButton.TabStop = True
        Me.BlackRadioButton.Text = "Black"
        Me.BlackRadioButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(229, 112)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 17)
        Me.Label3.TabIndex = 186737
        Me.Label3.Text = "Web Port (80)"
        Me.ToolTip1.SetToolTip(Me.Label3, "80 or 8000")
        '
        'LocalSearchCheckBox
        '
        Me.LocalSearchCheckBox.AutoSize = True
        Me.LocalSearchCheckBox.Location = New System.Drawing.Point(22, 190)
        Me.LocalSearchCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LocalSearchCheckBox.Name = "LocalSearchCheckBox"
        Me.LocalSearchCheckBox.Size = New System.Drawing.Size(113, 21)
        Me.LocalSearchCheckBox.TabIndex = 186742
        Me.LocalSearchCheckBox.Text = "Local Search"
        Me.ToolTip1.SetToolTip(Me.LocalSearchCheckBox, "Search only your grid")
        Me.LocalSearchCheckBox.UseVisualStyleBackColor = True
        '
        'AllGridSearchCheckBox
        '
        Me.AllGridSearchCheckBox.AutoSize = True
        Me.AllGridSearchCheckBox.Location = New System.Drawing.Point(148, 190)
        Me.AllGridSearchCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.AllGridSearchCheckBox.Name = "AllGridSearchCheckBox"
        Me.AllGridSearchCheckBox.Size = New System.Drawing.Size(129, 21)
        Me.AllGridSearchCheckBox.TabIndex = 186743
        Me.AllGridSearchCheckBox.Text = "Search All grids"
        Me.ToolTip1.SetToolTip(Me.AllGridSearchCheckBox, "Search All Outworldz Systems including yours")
        Me.AllGridSearchCheckBox.UseVisualStyleBackColor = True
        '
        'EnableSearchCheckBox
        '
        Me.EnableSearchCheckBox.AutoSize = True
        Me.EnableSearchCheckBox.Location = New System.Drawing.Point(22, 161)
        Me.EnableSearchCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.EnableSearchCheckBox.Name = "EnableSearchCheckBox"
        Me.EnableSearchCheckBox.Size = New System.Drawing.Size(123, 21)
        Me.EnableSearchCheckBox.TabIndex = 186744
        Me.EnableSearchCheckBox.Text = "Enable Search"
        Me.ToolTip1.SetToolTip(Me.EnableSearchCheckBox, "Viewer can search for items marked ""Show In Search""")
        Me.EnableSearchCheckBox.UseVisualStyleBackColor = True
        '
        'EventsCheckBox
        '
        Me.EventsCheckBox.AutoSize = True
        Me.EventsCheckBox.Location = New System.Drawing.Point(148, 161)
        Me.EventsCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.EventsCheckBox.Name = "EventsCheckBox"
        Me.EventsCheckBox.Size = New System.Drawing.Size(121, 21)
        Me.EventsCheckBox.TabIndex = 186745
        Me.EventsCheckBox.Text = "Enable Events"
        Me.ToolTip1.SetToolTip(Me.EventsCheckBox, "Viewer can search for Events at many grids")
        Me.EventsCheckBox.UseVisualStyleBackColor = True
        '
        'ApacheCheckbox
        '
        Me.ApacheCheckbox.AutoSize = True
        Me.ApacheCheckbox.Location = New System.Drawing.Point(21, 84)
        Me.ApacheCheckbox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ApacheCheckbox.Name = "ApacheCheckbox"
        Me.ApacheCheckbox.Size = New System.Drawing.Size(203, 21)
        Me.ApacheCheckbox.TabIndex = 1866
        Me.ApacheCheckbox.Text = "Enable Apache Web server"
        Me.ApacheCheckbox.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.EventsCheckBox)
        Me.GroupBox2.Controls.Add(Me.EnableSearchCheckBox)
        Me.GroupBox2.Controls.Add(Me.AllGridSearchCheckBox)
        Me.GroupBox2.Controls.Add(Me.LocalSearchCheckBox)
        Me.GroupBox2.Controls.Add(Me.X86Button)
        Me.GroupBox2.Controls.Add(Me.ApachePort)
        Me.GroupBox2.Controls.Add(Me.ApacheServiceCheckBox)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Controls.Add(Me.ApacheCheckbox)
        Me.GroupBox2.Location = New System.Drawing.Point(300, 295)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(349, 225)
        Me.GroupBox2.TabIndex = 186738
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Apache Webserver + Search and Map"
        '
        'X86Button
        '
        Me.X86Button.Location = New System.Drawing.Point(22, 38)
        Me.X86Button.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.X86Button.Name = "X86Button"
        Me.X86Button.Size = New System.Drawing.Size(195, 29)
        Me.X86Button.TabIndex = 186740
        Me.X86Button.Text = "Install C++ Runtime"
        Me.X86Button.UseVisualStyleBackColor = True
        '
        'ApachePort
        '
        Me.ApachePort.Location = New System.Drawing.Point(180, 104)
        Me.ApachePort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ApachePort.Name = "ApachePort"
        Me.ApachePort.Size = New System.Drawing.Size(40, 22)
        Me.ApachePort.TabIndex = 186736
        '
        'ApacheServiceCheckBox
        '
        Me.ApacheServiceCheckBox.AutoSize = True
        Me.ApacheServiceCheckBox.Location = New System.Drawing.Point(21, 112)
        Me.ApacheServiceCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ApacheServiceCheckBox.Name = "ApacheServiceCheckBox"
        Me.ApacheServiceCheckBox.Size = New System.Drawing.Size(144, 21)
        Me.ApacheServiceCheckBox.TabIndex = 1868
        Me.ApacheServiceCheckBox.Text = "Run  As A Service"
        Me.ApacheServiceCheckBox.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.PictureBox1.Location = New System.Drawing.Point(234, 26)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(38, 42)
        Me.PictureBox1.TabIndex = 1859
        Me.PictureBox1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(679, 28)
        Me.MenuStrip1.TabIndex = 186739
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1, Me.ApacheToolStripMenuItem})
        Me.HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.question_and_answer
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(73, 24)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.Resources.about
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(153, 26)
        Me.HelpToolStripMenuItem1.Text = "Diva Panel"
        '
        'ApacheToolStripMenuItem
        '
        Me.ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.Resources.window_environment
        Me.ApacheToolStripMenuItem.Name = "ApacheToolStripMenuItem"
        Me.ApacheToolStripMenuItem.Size = New System.Drawing.Size(153, 26)
        Me.ApacheToolStripMenuItem.Text = "Apache"
        '
        'FormDiva
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(679, 541)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Web)
        Me.Controls.Add(Me.GroupBox6)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
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
    Friend WithEvents ApacheCheckbox As CheckBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ApachePort As TextBox
    Friend WithEvents ApacheServiceCheckBox As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents X86Button As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents GreetingTextBox As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ApacheToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllGridSearchCheckBox As CheckBox
    Friend WithEvents LocalSearchCheckBox As CheckBox
    Friend WithEvents EnableSearchCheckBox As CheckBox
    Friend WithEvents EventsCheckBox As CheckBox
End Class
