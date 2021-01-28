#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

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
    Private path As String
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
        If setpassword And PropOpensimIsRunning() And Settings.Password.Length > 5 Then
            ConsoleCommand(RobustName(), "reset user password " & Settings.AdminFirst & " " & Settings.AdminLast & " " & Settings.Password)
        End If

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        AccountConfirmationRequired.Text = Global.Outworldz.My.Resources.Confirm
        ApacheToolStripMenuItem.Image = Global.Outworldz.My.Resources.window_environment
        ApacheToolStripMenuItem.Text = Global.Outworldz.My.Resources.Apache_word
        BlackRadioButton.Text = Global.Outworldz.My.Resources.Black_word
        GroupBox1.Text = Global.Outworldz.My.Resources.SplashScreen
        GroupBox6.Text = Global.Outworldz.My.Resources.SMTP
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        HelpToolStripMenuItem1.Image = Global.Outworldz.My.Resources.about
        HelpToolStripMenuItem1.Text = Global.Outworldz.My.Resources.Diva_Panel_word
        Label1.Text = Global.Outworldz.My.Resources.Theme_word
        Label10.Text = Global.Outworldz.My.Resources.Password_word
        Label11.Text = Global.Outworldz.My.Resources.First_name_word
        Label12.Text = Global.Outworldz.My.Resources.Last_Name_Word
        Label14.Text = Global.Outworldz.My.Resources.User_Name_word
        Label17.Text = Global.Outworldz.My.Resources.Notify_Email
        Label18.Text = Global.Outworldz.My.Resources.SMTPPassword_word
        Label19.Text = Global.Outworldz.My.Resources.SplashScreen
        Label2.Text = Global.Outworldz.My.Resources.Friendly
        Label23.Text = Global.Outworldz.My.Resources.SMTPHost_word
        Label24.Text = Global.Outworldz.My.Resources.SMTPPort_word
        Label4.Text = Global.Outworldz.My.Resources.Viewer_Greeting_word
        Text = Global.Outworldz.My.Resources.WebServerPanel
        ToolTip1.SetToolTip(AdminPassword, Global.Outworldz.My.Resources.Password_Text)
        Web.Text = Global.Outworldz.My.Resources.Wifi_interface
        WhiteRadioButton.Text = Global.Outworldz.My.Resources.White_word
        CustomRadioButton.Text = Global.Outworldz.My.Resources.Custom_word
        WiFi.Image = Global.Outworldz.My.Resources.about
        WifiEnabled.Text = Global.Outworldz.My.Resources.Diva_Wifi_Enabled_word

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
        If Settings.Theme = "Custom" Then CustomRadioButton.Checked = True

        'Gmail
        'passwords are asterisks
        AdminPassword.UseSystemPasswordChar = True
        GmailPassword.UseSystemPasswordChar = True

        ' ports
        AdminPassword.Text = Settings.Password
        AdminLast.Text = Settings.AdminLast
        AdminFirst.Text = Settings.AdminFirst

        If Settings.Theme = "White" Then
            WhiteRadioButton.Checked = True
        ElseIf Settings.Theme = "Black" Then
            BlackRadioButton.Checked = True
        ElseIf Settings.Theme = "Custom" Then
            CustomRadioButton.Checked = True
        End If

        If PropOpensimIsRunning Then
            AdminPassword.Enabled = True
        Else
            AdminPassword.Enabled = False
        End If

        GreetingTextBox.Text = Settings.WelcomeMessage
        HelpOnce("Diva")

        LoadPhoto()

        initted = True

    End Sub

#End Region

#Region "Photo"

    Private Sub LoadPhoto()

        If Settings.Theme = "Black" Then
            path = IO.Path.Combine(Settings.CurrentDirectory, "Black.png")
        ElseIf Settings.Theme = "White" Then
            path = IO.Path.Combine(Settings.CurrentDirectory, "White.png")
        ElseIf Settings.Theme = "Custom" Then
            path = IO.Path.Combine(Settings.CurrentDirectory, "Custom.png")
        End If

        If Not System.IO.File.Exists(path) Then
            Dim Pic As Bitmap = My.Resources.NoImage
            PictureBox1.Image = Pic
        Else
            Dim img As Image
            Using bmpTemp As New Bitmap(path)
                img = New Bitmap(bmpTemp)
            End Using

            PictureBox1.Image = img
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Using ofd As New OpenFileDialog With {
                .Filter = Global.Outworldz.My.Resources.picfilter,
                .FilterIndex = 1,
                .Multiselect = False
            }
            If ofd.ShowDialog = DialogResult.OK Then
                If ofd.FileName.Length > 0 Then

                    Dim pattern As Regex = New Regex("PNG$|png$")
                    Dim match As Match = pattern.Match(ofd.FileName)
                    If Not match.Success Then
                        MsgBox(My.Resources.Must_PNG, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
                        Return
                    End If

                    PictureBox1.Image = Nothing

                    PictureBox1.Image = Bitmap.FromFile(ofd.FileName)

                    DeleteFile(path)

                    Try
                        Using newBitmap As New Bitmap(PictureBox1.Image)
                            newBitmap.Save(path, Imaging.ImageFormat.Png)
                        End Using
                    Catch ex As Exception
                        BreakPoint.Show(ex.Message)
                    Finally

                    End Try

                    LoadPhoto()

                End If
            End If
        End Using

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
        If AdminPassword.Text.Length > 5 Then
            Settings.Password = AdminPassword.Text
            Settings.SaveSettings()
        End If

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
        If AdminPassword.Text.Length > 5 Then
            Settings.Password = AdminPassword.Text
            Settings.SaveSettings()
            setpassword = True
        Else
            MsgBox(My.Resources.Passwordtooshort_word, MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxSetForeground)
        End If

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
            Settings.Theme = "Black"
            Settings.SaveSettings()
            LoadPhoto()
            CopyWifi()
            TextPrint(My.Resources.Theme_Black)

        End If

    End Sub

    Private Sub CustomRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles CustomRadioButton.CheckedChanged

        If Not initted Then Return

        If CustomRadioButton.Checked Then
            Settings.Theme = "Custom"
            Settings.SaveSettings()
            PictureBox1.Image = My.Resources.NoImage
            TextPrint(My.Resources.Theme_Custom)
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles SplashPage.TextChanged

        If Not initted Then Return
        Settings.SplashPage = SplashPage.Text

    End Sub

    Private Sub WhiteRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles WhiteRadioButton.CheckedChanged

        If Not initted Then Return

        If WhiteRadioButton.Checked Then
            Settings.Theme = "White"
            Settings.SaveSettings()
            LoadPhoto()
            CopyWifi()
            TextPrint(My.Resources.Theme_White)
        End If

    End Sub

#End Region

End Class
