Imports System.Text.RegularExpressions

Public Class FormDiva

    Dim initted As Boolean = False
    Dim setpassword As Boolean = False
#Region "FormPos"

    Public ScreenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)
    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        SetScreen()

        'Wifi
        WifiEnabled.Checked = Form1.MySetting.WifiEnabled
        AdminEmail.Text = Form1.MySetting.AdminEmail
        AccountConfirmationRequired.Checked = Form1.MySetting.AccountConfirmationRequired
        GmailPassword.Text = Form1.MySetting.SmtpPassword
        GmailUsername.Text = Form1.MySetting.SmtpUsername
        SmtpPort.Text = Form1.MySetting.SmtpPort
        SmtpHost.Text = Form1.MySetting.SmtpHost
        SplashPage.Text = Form1.MySetting.SplashPage
        GridName.Text = Form1.MySetting.SimName

        If Form1.MySetting.Theme = "White" Then WhiteRadioButton.Checked = True
        If Form1.MySetting.Theme = "Black" Then BlackRadioButton.Checked = True
        If Form1.MySetting.Theme = "Custom" Then CustomButton1.Checked = True

        'Gmail
        'passwords are asterisks
        AdminPassword.UseSystemPasswordChar = True
        GmailPassword.UseSystemPasswordChar = True

        ' ports
        AdminPassword.Text = Form1.MySetting.Password
        AdminLast.Text = Form1.MySetting.AdminLast
        AdminFirst.Text = Form1.MySetting.AdminFirst

        If Form1.MySetting.Theme = "White" Then
            BlackRadioButton.Checked = False
            WhiteRadioButton.Checked = True
            CustomButton1.Checked = False
        ElseIf Form1.MySetting.Theme = "Black" Then
            BlackRadioButton.Checked = True
            WhiteRadioButton.Checked = False
            CustomButton1.Checked = False
        ElseIf Form1.MySetting.Theme = "Custom" Then
            BlackRadioButton.Checked = False
            WhiteRadioButton.Checked = False
            CustomButton1.Checked = True
        End If

        If Form1.OpensimIsRunning Then
            AdminPassword.Enabled = True
        Else
            AdminPassword.Enabled = False
        End If

        ApacheCheckbox.Checked = Form1.MySetting.ApacheEnable
        ApachePort.Text = Form1.MySetting.ApachePort
        ApacheServiceCheckBox.Checked = Form1.MySetting.ApacheService

        Form1.HelpOnce("Diva")

        initted = True

    End Sub

    Private Sub Close_form(sender As Object, e As EventArgs) Handles Me.Closed

        Form1.MySetting.SaveSettings()

        If setpassword And Form1.OpensimIsRunning() Then
            Form1.ConsoleCommand("Robust", "reset user password " & Form1.MySetting.AdminFirst & " " & Form1.MySetting.AdminLast & " " & Form1.MySetting.Password & "{ENTER}" + vbCrLf)
        End If

    End Sub

#Region "Wifi"

    Private Sub WifiEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles WifiEnabled.CheckedChanged
        If Not initted Then Return
        Form1.MySetting.WifiEnabled = WifiEnabled.Checked
        Form1.MySetting.SaveSettings()

        If WifiEnabled.Checked Then
            AdminFirst.Enabled = True
            AdminLast.Enabled = True
            AdminPassword.Enabled = True
            AdminEmail.Enabled = True
            AccountConfirmationRequired.Enabled = True
            GmailUsername.Enabled = True
            GmailPassword.Enabled = True
        Else

            AdminFirst.Enabled = False
            AdminLast.Enabled = False
            AdminPassword.Enabled = False
            AdminEmail.Enabled = False
            AccountConfirmationRequired.Enabled = False
            GmailUsername.Enabled = False
            GmailPassword.Enabled = False
        End If

    End Sub

    Private Sub AccountConfirmationRequired_CheckedChanged(sender As Object, e As EventArgs) Handles AccountConfirmationRequired.CheckedChanged

        If Not initted Then Return
        Form1.MySetting.AccountConfirmationRequired = AccountConfirmationRequired.Checked
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles WiFi.Click

        Form1.Help("Diva")

    End Sub

#End Region

#Region "Gmail"

    Private Sub AdminPassword_Click(sender As Object, e As EventArgs) Handles AdminPassword.Click

        AdminPassword.UseSystemPasswordChar = False

    End Sub
    Private Sub AdminPassword_TextChanged(sender As Object, e As EventArgs) Handles AdminPassword.TextChanged

        If Not initted Then Return
        Form1.MySetting.Password = AdminPassword.Text
        Form1.MySetting.SaveSettings()

    End Sub
    Private Sub GmailUsername_TextChanged(sender As Object, e As EventArgs) Handles GmailUsername.TextChanged

        If Not initted Then Return
        Form1.MySetting.SmtpUsername = GmailUsername.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub GmailPassword_Click(sender As Object, e As EventArgs) Handles GmailPassword.Click

        GmailPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub GmailPassword_TextChanged(sender As Object, e As EventArgs) Handles GmailPassword.TextChanged

        If Not initted Then Return
        Form1.MySetting.SmtpPassword = GmailPassword.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub AdminFirst_TextChanged_2(sender As Object, e As EventArgs) Handles AdminFirst.TextChanged

        If Not initted Then Return
        Form1.MySetting.AdminFirst = AdminFirst.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub AdminLast_TextChanged(sender As Object, e As EventArgs) Handles AdminLast.TextChanged

        If Not initted Then Return
        Form1.MySetting.AdminLast = AdminLast.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub Password_TextChanged(sender As Object, e As EventArgs) Handles AdminPassword.TextChanged

        If Not initted Then Return
        Form1.MySetting.Password = AdminPassword.Text
        Form1.MySetting.SaveSettings()

        Setpassword = True

    End Sub

    Private Sub TextBox1_TextChanged_3(sender As Object, e As EventArgs) Handles AdminEmail.TextChanged

        If Not initted Then Return
        Form1.MySetting.AdminEmail = AdminEmail.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub SmtpHost_TextChanged(sender As Object, e As EventArgs) Handles SmtpHost.TextChanged

        If Not initted Then Return
        Form1.MySetting.SmtpHost = SmtpHost.Text
        Form1.MySetting.SaveSettings()

    End Sub

    Private Sub SmtpPort_TextChanged(sender As Object, e As EventArgs) Handles SmtpPort.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        SmtpPort.Text = digitsOnly.Replace(SmtpPort.Text, "")
        If Not initted Then Return
        Form1.MySetting.SmtpPort = SmtpPort.Text
        Form1.MySetting.SaveSettings()

    End Sub


#End Region

#Region "Splash"

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles SplashPage.TextChanged

        If Not initted Then Return
        Form1.MySetting.SplashPage = SplashPage.Text

    End Sub

    Private Sub GridName_TextChanged(sender As Object, e As EventArgs) Handles GridName.TextChanged

        Form1.MySetting.SimName = GridName.Text

    End Sub

    Private Sub BlackRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles BlackRadioButton.CheckedChanged

        If BlackRadioButton.Checked Then
            Form1.CopyWifi("Black")
            Form1.Print("Theme set to Black")
            Form1.MySetting.Theme = "Black"
        End If


    End Sub

    Private Sub WhiteRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles WhiteRadioButton.CheckedChanged

        If WhiteRadioButton.Checked Then
            Form1.CopyWifi("White")
            Form1.Print("Theme set to White")
            Form1.MySetting.Theme = "White"
        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles CustomButton1.CheckedChanged

        If CustomButton1.Checked Then
            Form1.CopyWifi("Custom")
            Form1.Print("Theme set to Custom")
            Form1.MySetting.Theme = "Custom"
        End If

    End Sub

    Private Sub ApacheCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles ApacheCheckbox.CheckedChanged

        If Not initted Then Return
        Form1.MySetting.ApacheEnable = ApacheCheckbox.Checked

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Form1.Help("Apache")

    End Sub

    Private Sub ApacheServiceCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ApacheServiceCheckBox.CheckedChanged

        If Not initted Then Return
        Form1.MySetting.ApacheService = ApacheServiceCheckBox.Checked

    End Sub

    Private Sub ApachePort_TextChanged(sender As Object, e As EventArgs) Handles ApachePort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        ApachePort.Text = digitsOnly.Replace(ApachePort.Text, "")
        Form1.MySetting.ApachePort = ApachePort.Text

    End Sub

#End Region

End Class