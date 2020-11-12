#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

#End Region

Imports System.Text.RegularExpressions

Public Class FormDiva

#Region "Private Fields"

    Dim initted As Boolean
    Dim setpassword As Boolean

#End Region

#Region "FormPos"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

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

#Region "Private Methods"

    Private Sub Close_form(sender As Object, e As EventArgs) Handles Me.Closed

        Settings.SaveSettings()
        FormSetup.PropViewedSettings = True
        If setpassword And FormSetup.PropOpensimIsRunning() Then
            FormSetup.ConsoleCommand(RobustName(), "reset user password " & Settings.AdminFirst & " " & Settings.AdminLast & " " & Settings.Password & "{ENTER}" + vbCrLf)
        End If

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Translate.Run(Name)
        Translate.Run(Name)
        SetScreen()

        'Wifi
        WifiEnabled.Checked = Settings.WifiEnabled
        AdminEmail.Text = Settings.AdminEmail
        AccountConfirmationRequired.Checked = Settings.AccountConfirmationRequired
        GmailPassword.Text = Settings.SmtpPassword
        GmailUsername.Text = Settings.SmtPropUserName
        SmtpPort.Text = CStr(Settings.SmtpPort)
        SmtpHost.Text = Settings.SmtpHost
        SplashPage.Text = Settings.SplashPage
        GridName.Text = Settings.SimName

        If Settings.Theme = "White" Then WhiteRadioButton.Checked = True
        If Settings.Theme = "Black" Then BlackRadioButton.Checked = True
        If Settings.Theme = "Custom" Then CustomButton1.Checked = True

        'Gmail
        'passwords are asterisks
        AdminPassword.UseSystemPasswordChar = True
        GmailPassword.UseSystemPasswordChar = True

        ' ports
        AdminPassword.Text = Settings.Password
        AdminLast.Text = Settings.AdminLast
        AdminFirst.Text = Settings.AdminFirst

        If Settings.Theme = "White" Then
            BlackRadioButton.Checked = False
            WhiteRadioButton.Checked = True
            CustomButton1.Checked = False
        ElseIf Settings.Theme = "Black" Then
            BlackRadioButton.Checked = True
            WhiteRadioButton.Checked = False
            CustomButton1.Checked = False
        ElseIf Settings.Theme = "Custom" Then
            BlackRadioButton.Checked = False
            WhiteRadioButton.Checked = False
            CustomButton1.Checked = True
        End If

        If FormSetup.PropOpensimIsRunning Then
            AdminPassword.Enabled = True
        Else
            AdminPassword.Enabled = False
        End If

        GreetingTextBox.Text = Settings.WelcomeMessage
        HelpOnce("Diva")

        initted = True

    End Sub

#End Region

#Region "Wifi"

    Private Sub AccountConfirmationRequired_CheckedChanged(sender As Object, e As EventArgs) Handles AccountConfirmationRequired.CheckedChanged

        If Not initted Then Return
        Settings.AccountConfirmationRequired = AccountConfirmationRequired.Checked
        Settings.SaveSettings()

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles WiFi.Click

        HelpManual("Diva")

    End Sub

    Private Sub WifiEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles WifiEnabled.CheckedChanged

        If Not initted Then Return
        Settings.WifiEnabled = WifiEnabled.Checked
        Settings.SaveSettings()

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

#End Region

#Region "Gmail"

    Private Sub AdminFirst_TextChanged_2(sender As Object, e As EventArgs) Handles AdminFirst.TextChanged

        If Not initted Then Return
        Settings.AdminFirst = AdminFirst.Text
        Settings.SaveSettings()

    End Sub

    Private Sub AdminLast_TextChanged(sender As Object, e As EventArgs) Handles AdminLast.TextChanged

        If Not initted Then Return
        Settings.AdminLast = AdminLast.Text
        Settings.SaveSettings()

    End Sub

    Private Sub AdminPassword_Click(sender As Object, e As EventArgs) Handles AdminPassword.Click

        AdminPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub AdminPassword_TextChanged(sender As Object, e As EventArgs) Handles AdminPassword.TextChanged

        If Not initted Then Return
        Settings.Password = AdminPassword.Text
        Settings.SaveSettings()

    End Sub

    Private Sub GmailPassword_Click(sender As Object, e As EventArgs) Handles GmailPassword.Click

        GmailPassword.UseSystemPasswordChar = False

    End Sub

    Private Sub GmailPassword_TextChanged(sender As Object, e As EventArgs) Handles GmailPassword.TextChanged

        If Not initted Then Return
        Settings.SmtpPassword = GmailPassword.Text
        Settings.SaveSettings()

    End Sub

    Private Sub GmailUsername_TextChanged(sender As Object, e As EventArgs) Handles GmailUsername.TextChanged

        If Not initted Then Return
        Settings.SmtPropUserName = GmailUsername.Text
        Settings.SaveSettings()

    End Sub

    Private Sub Password_TextChanged(sender As Object, e As EventArgs) Handles AdminPassword.TextChanged

        If Not initted Then Return
        Settings.Password = AdminPassword.Text
        Settings.SaveSettings()

        setpassword = True

    End Sub

    Private Sub SmtpHost_TextChanged(sender As Object, e As EventArgs) Handles SmtpHost.TextChanged

        If Not initted Then Return
        Settings.SmtpHost = SmtpHost.Text
        Settings.SaveSettings()

    End Sub

    Private Sub SmtpPort_TextChanged(sender As Object, e As EventArgs) Handles SmtpPort.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        SmtpPort.Text = digitsOnly.Replace(SmtpPort.Text, "")
        If Not initted Then Return
        Settings.SmtpPort = CInt("0" & SmtpPort.Text)
        Settings.SaveSettings()

    End Sub

    Private Sub TextBox1_TextChanged_3(sender As Object, e As EventArgs) Handles AdminEmail.TextChanged

        If Not initted Then Return
        Settings.AdminEmail = AdminEmail.Text
        Settings.SaveSettings()

    End Sub

#End Region

#Region "Splash"

    Private Sub BlackRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles BlackRadioButton.CheckedChanged
        If Not initted Then Return
        If BlackRadioButton.Checked Then
            FormSetup.CopyWifi("Black")
            FormSetup.Print(My.Resources.Theme_Black)
            Settings.Theme = "Black"
        End If

    End Sub

    Private Sub GreetingTextBox_TextChanged(sender As Object, e As EventArgs) Handles GreetingTextBox.TextChanged

        If Not initted Then Return
        Settings.WelcomeMessage = GreetingTextBox.Text

    End Sub

    Private Sub GridName_TextChanged(sender As Object, e As EventArgs) Handles GridName.TextChanged
        If Not initted Then Return
        Settings.SimName = GridName.Text

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        HelpManual("Diva")
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles CustomButton1.CheckedChanged
        If Not initted Then Return
        If CustomButton1.Checked Then
            FormSetup.CopyWifi("Custom")
            FormSetup.Print(My.Resources.Theme_Custom)
            Settings.Theme = "Custom"
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles SplashPage.TextChanged

        If Not initted Then Return
        Settings.SplashPage = SplashPage.Text

    End Sub

    Private Sub WhiteRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles WhiteRadioButton.CheckedChanged

        If Not initted Then Return
        If WhiteRadioButton.Checked Then
            FormSetup.CopyWifi("White")
            FormSetup.Print(My.Resources.Theme_White)
            Settings.Theme = "White"
        End If

    End Sub

#End Region

End Class
