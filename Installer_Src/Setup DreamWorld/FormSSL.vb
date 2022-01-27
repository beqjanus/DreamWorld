Public Class FormSSL

    Private initted As Boolean
    Private changed As Boolean

    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If changed Then
            Dim result = MsgBox(My.Resources.Changes_Made, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Exclamation, My.Resources.Save_changes_word)
            If result <> vbYes Then
                Return
            End If
        End If

        Settings.SaveSettings()

    End Sub

    Private Sub SSL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Email.Text = Settings.SSLEmail
        OU.Text = Settings.SSLOrganization
        State.Text = Settings.SSLState
        CountryName.Text = Settings.SSLCountry
        Locale.Text = Settings.SSLLocale

        HelpOnce("SSL")
        initted = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If changed Then Settings.SaveSettings()
        Dim ssl = New SSL
        ssl.DoSSL()

    End Sub

    Private Sub Email_TextChanged(sender As Object, e As EventArgs) Handles Email.TextChanged

        If Not initted Then Return
        Settings.SSLEmail = Email.Text
        changed = True

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("SSL")

    End Sub

    Private Sub Locale_TextChanged(sender As Object, e As EventArgs) Handles Locale.TextChanged

        If Not initted Then Return
        Settings.SSLLocale = Locale.Text
        changed = True

    End Sub

    Private Sub OU_TextChanged(sender As Object, e As EventArgs) Handles OU.TextChanged

        If Not initted Then Return
        Settings.SSLOrganization = OU.Text
        changed = True

    End Sub

    Private Sub State_TextChanged(sender As Object, e As EventArgs) Handles State.TextChanged

        If Not initted Then Return
        Settings.SSLState = State.Text
        changed = True

    End Sub


End Class