Imports System.Text.RegularExpressions

Public Class FormEmailSetup

    Dim initted As Boolean

#Region "FormPos"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ClassScreenpos

    Public Property ScreenPosition As ClassScreenpos
        Get
            Return _screenPosition
        End Get
        Set(value As ClassScreenpos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()

        ScreenPosition = New ClassScreenpos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Startup"

    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Me.Text = Global.Outworldz.My.Resources.SMTP
        EmailPassword.Text = Settings.SmtpPassword
        EmailPassword.UseSystemPasswordChar = True
        EmailPasswordLabel.Text = Global.Outworldz.My.Resources.SMTPPassword_word

        UserNameLabel.Text = Global.Outworldz.My.Resources.User_Name_word
        EmailUsername.Text = Settings.SmtPropUserName

        ';# {enabled} {[Startup]emailmoduleDefaultEmailModule} {Enable module?} {true false} false
        ';; Enable the email module
        EmailEnabledCheckBox.Checked = Settings.EmailEnabledCheckBox
        EmailEnabledCheckBox.Text = Global.Outworldz.My.Resources.EmailEnabled

        EmailHostLabel.Text = Global.Outworldz.My.Resources.SMTPHost_word

        EmailPortLabel.Text = Global.Outworldz.My.Resources.SMTPPort_word

        SmtpHost.Text = Settings.SmtpHost

        SmtpPort.Text = CStr(Settings.SmtpPort)

        SSLEnabled.Checked = Settings.SmtpSecure
        SSLEnabled.Text = Global.Outworldz.My.Resources.SSLTLS

        'extended factors

        VerifyCertificateCheckBox.Checked = Settings.VerifyCertCheckBox
        VerifyCertificateCheckBox.Text = Global.Outworldz.My.Resources.VerifyCertificate

        ';# {enabled} {[Startup]emailmoduleDefaultEmailModule} {Enable send to objects to regions Not on instance?} {true false} true
        ';; Enable sending email to regions Not on current opensimulator instance. Currently does Not work
        enableEmailToExternalObjectsCheckBox.Text = Global.Outworldz.My.Resources.EmailToObjectsEnabled
        enableEmailToExternalObjectsCheckBox.Checked = Settings.enableEmailToExternalObjects

        enableEmailToSMTPCheckBox.Checked = Settings.enableEmailToSMTPCheckBox
        enableEmailToSMTPCheckBox.Text = Global.Outworldz.My.Resources.enableEmailToSMTP

        ';# {enabled} {[Startup]emailmoduleDefaultEmailModule} {maximum number of emails from a object owner per hour} {} 500
        MailsFromOwnerPerHourTextBox.Text = CStr(Settings.MailsFromOwnerPerHour)
        MailsFromPrimOwnerPerHourLabel.Text = Global.Outworldz.My.Resources.EmailsFromOwnerPerHour

        ';# {enabled} {[Startup]emailmoduleDefaultEmailModule} {maximum number of emails to a prim address per hour} {} 50
        MailsToPrimAddressPerHourTextBox.Text = CStr(Settings.MailsToPrimAddressPerHour)
        LabelMailsToPrimAddressPerHour.Text = Global.Outworldz.My.Resources.EmailsToPrimAddressPerHour

        ';# {enabled} {[Startup]emailmoduleDefaultEmailModule} {maximum number of emails via smtp per day} {} 100
        '; MailsToAddressPerHour = 500  ' UBIT MANTIS
        SMTP_MailsPerDayTextBox.Text = CStr(Settings.MailsPerDay)
        MailsPerDayLabel.Text = Global.Outworldz.My.Resources.EmailsPerDay

        ';# {enabled} {[Startup]emailmoduleDefaultEmailModule} {maximum number of emails to a SMTP address per hour} {} 10
        MailsToSMTPAddressPerHourTextBox.Text = CStr(Settings.EmailsToSMTPAddressPerHour)
        MailsToSMTPAddressPerHourLabel.Text = Global.Outworldz.My.Resources.EmailsToSMTPAddressPerHour

        ';# {email_pause_time} {[Startup]emailmoduleDefaultEmailModule Enabled: true} {Period in seconds to delay after an email Is sent.} {} 20
        email_pause_timeTextBox.Text = CStr(Settings.Email_pause_time)
        EmailPauseTimeLabel.Text = Global.Outworldz.My.Resources.Emailpausetime

        ';# {email_max_size} {[Startup]emailmoduleDefaultEmailModule Enabled: true} {Maximum total size of email in bytes.} {} 4096
        MaxMailSizeTextBox.Text = CStr(Settings.MaxMailSize)
        MaxMailSizeTextBoxLabel.Text = Global.Outworldz.My.Resources.EmailMaxMailSize

        HelpOnce("EmailSetup")

        SetScreen()
        initted = True

    End Sub

#End Region

#Region "TextBoxes"

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles VerifyCertificateCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.VerifyCertCheckBox = VerifyCertificateCheckBox.Checked

    End Sub

    Private Sub email_pause_timeTextBox_TextChanged(sender As Object, e As EventArgs) Handles email_pause_timeTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d]")
        email_pause_timeTextBox.Text = digitsOnly.Replace(email_pause_timeTextBox.Text, "")
        Settings.Email_pause_time = CInt("0" & email_pause_timeTextBox.Text)

    End Sub

    Private Sub EmailEnabledCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles EmailEnabledCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.EmailEnabledCheckBox = EmailEnabledCheckBox.Checked

    End Sub

    Private Sub EmailPassword_Click(sender As Object, e As EventArgs) Handles EmailPassword.Click
        EmailPassword.UseSystemPasswordChar = False
    End Sub

    Private Sub EmailPassword_TextChanged(sender As Object, e As EventArgs) Handles EmailPassword.TextChanged

        If Not initted Then Return
        Settings.SmtPropUserName = EmailUsername.Text

    End Sub

    Private Sub EmailUsername_TextChanged(sender As Object, e As EventArgs) Handles EmailUsername.TextChanged

        If Not initted Then Return
        Settings.SmtPropUserName = EmailUsername.Text

    End Sub

    Private Sub enableEmailToExternalObjectsCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles enableEmailToExternalObjectsCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.enableEmailToExternalObjects = enableEmailToExternalObjectsCheckBox.Checked

    End Sub

    Private Sub enableEmailToSMTPCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles enableEmailToSMTPCheckBox.CheckedChanged

        If Not initted Then Return
        Settings.enableEmailToSMTPCheckBox = enableEmailToSMTPCheckBox.Checked

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("EmailSetup")

    End Sub

    Private Sub MailsFromOwnerPerHourTextBox_TextChanged(sender As Object, e As EventArgs) Handles MailsFromOwnerPerHourTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d]")
        MailsFromOwnerPerHourTextBox.Text = digitsOnly.Replace(MailsFromOwnerPerHourTextBox.Text, "")
        Settings.MailsFromOwnerPerHour = CInt("0" & MailsFromOwnerPerHourTextBox.Text)

    End Sub

    Private Sub MailsToPrimAddressPerHourTextBox_TextChanged(sender As Object, e As EventArgs) Handles MailsToPrimAddressPerHourTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d]")
        MailsToPrimAddressPerHourTextBox.Text = digitsOnly.Replace(MailsToPrimAddressPerHourTextBox.Text, "")
        Settings.MailsToPrimAddressPerHour = CInt("0" & MailsToPrimAddressPerHourTextBox.Text)

    End Sub

    Private Sub MailsToSMTPAddressPerHourTextBox_TextChanged(sender As Object, e As EventArgs) Handles MailsToSMTPAddressPerHourTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d]")
        MailsToSMTPAddressPerHourTextBox.Text = digitsOnly.Replace(MailsToSMTPAddressPerHourTextBox.Text, "")
        Settings.EmailsToSMTPAddressPerHour = CInt("0" & MailsToSMTPAddressPerHourTextBox.Text)

    End Sub

    Private Sub SMTP_MailsPerDayTextBox_TextChanged(sender As Object, e As EventArgs) Handles SMTP_MailsPerDayTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d]")
        SMTP_MailsPerDayTextBox.Text = digitsOnly.Replace(SMTP_MailsPerDayTextBox.Text, "")
        Settings.MailsPerDay = CInt("0" & SMTP_MailsPerDayTextBox.Text)

    End Sub

    Private Sub SmtpHost_TextChanged(sender As Object, e As EventArgs) Handles SmtpHost.TextChanged

        If Not initted Then Return
        Settings.SmtpHost = SmtpHost.Text

    End Sub

    Private Sub SmtpPort_TextChanged(sender As Object, e As EventArgs) Handles SmtpPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly = New Regex("[^\d]")
        SmtpPort.Text = digitsOnly.Replace(SmtpPort.Text, "")
        Settings.SmtpPort = CInt("0" & SmtpPort.Text)

    End Sub

    Private Sub SSLEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles SSLEnabled.CheckedChanged

        If Not initted Then Return
        Settings.SmtpSecure = SSLEnabled.Checked

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles MaxMailSizeTextBox.TextChanged

        If Not initted Then Return
        Dim digitsOnly = New Regex("[^\d]")
        MaxMailSizeTextBox.Text = digitsOnly.Replace(MaxMailSizeTextBox.Text, "")
        Settings.MaxMailSize = CInt("0" & MaxMailSizeTextBox.Text)

    End Sub

#End Region

End Class