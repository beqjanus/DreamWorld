Public Class FormSSL

    Private initted As Boolean
    Private linecount As Integer = 0

    Private Sub Advanced_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Email.Text = Settings.SSLEmail
        OU.Text = Settings.SSLOrganization
        State.Text = Settings.SSLState
        CountryName.Text = Settings.SSLCountry
        Locale.Text = Settings.SSLLocale

        HelpOnce("SSL")
        initted = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        SSLRunning = False
        Sleep(1000)
        SSLLog.Clear()
        Dim ssl = New SSL
        SSLRunning = True
        ssl.DoSSL()

        While SSLRunning
            SSLPrint()
            Sleep(100)
        End While

    End Sub

#Region "Scrolling text box"

    Private Sub CountryName_TextChanged(sender As Object, e As EventArgs) Handles CountryName.TextChanged

        If Not initted Then Return
        Settings.SSLCountry = CountryName.Text

    End Sub

    Private Sub Email_TextChanged(sender As Object, e As EventArgs) Handles Email.TextChanged

        If Not initted Then Return
        Settings.SSLEmail = Email.Text

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("SSL")

    End Sub

    Private Sub Locale_TextChanged(sender As Object, e As EventArgs) Handles Locale.TextChanged

        If Not initted Then Return
        Settings.SSLLocale = Locale.Text

    End Sub

    Private Sub OU_TextChanged(sender As Object, e As EventArgs) Handles OU.TextChanged

        If Not initted Then Return
        Settings.SSLOrganization = OU.Text

    End Sub

    Private Sub SSLPrint()

        While SSLLog.Count > linecount
            TextBox1.Text += SSLLog.ElementAt(linecount) & vbCrLf
            If TextBox1.Text.Length > TextBox1.MaxLength - 1000 Then
                TextBox1.Text = Mid(TextBox1.Text, 1000)
            End If
            Sleep(10)
        End While

    End Sub

    Private Sub State_TextChanged(sender As Object, e As EventArgs) Handles State.TextChanged

        If Not initted Then Return
        Settings.SSLState = State.Text

    End Sub

    Private Sub TextBox1_Changed(sender As System.Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
    End Sub

#End Region

End Class