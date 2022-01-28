Public Class FormSSL

    Private changed As Boolean
    Private initted As Boolean

    'https://www.win-acme.com/manual/advanced-use/examples/apache
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim result = MsgBox(My.Resources.AreYouSureSSL, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Exclamation, "SSL")

        If result <> vbYes Then
            Return
        End If

        Settings.SSLIsInstalled = False
        If changed Then Settings.SaveSettings()
        InstallSSL()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles StopButton.Click

        StartButton.Enabled = False
        StopButton.Enabled = False
        RestartButton.Enabled = False
        PictureBox2.Image = My.Resources.gear_time
        StopApache()
        If IsApacheRunning() Then
            PictureBox2.Image = My.Resources.gear_run
            StartButton.Enabled = False
            StopButton.Enabled = True
            RestartButton.Enabled = True
        Else
            PictureBox2.Image = My.Resources.gear_stop
            StartButton.Enabled = True
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles RestartButton.Click

        StopButton.Enabled = False
        StartButton.Enabled = False
        RestartButton.Enabled = False
        PictureBox2.Image = My.Resources.gear_time
        StopApache()
        PictureBox2.Image = My.Resources.gear_stop
        StartApache()
        If IsApacheRunning() Then
            PictureBox2.Image = My.Resources.gear_run
        Else
            PictureBox2.Image = My.Resources.gear_stop
        End If
        StartButton.Enabled = False
        StopButton.Enabled = True
        RestartButton.Enabled = True

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles StartButton.Click

        StartButton.Enabled = False
        RestartButton.Enabled = False
        StopButton.Enabled = False
        PictureBox2.Image = My.Resources.gear_time
        StartApache()
        If IsApacheRunning() Then
            PictureBox2.Image = My.Resources.gear_run
            StopButton.Enabled = True
            RestartButton.Enabled = True
        Else
            PictureBox2.Image = My.Resources.gear_stop
            StartButton.Enabled = True
        End If

    End Sub

    Private Sub EnableSSLCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles EnableSSLCheckbox.CheckedChanged

        If Not initted Then Return
        Settings.SSLEnabled = EnableSSLCheckbox.Checked
        changed = True

    End Sub

    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If changed Then
            Dim result = MsgBox(My.Resources.Changes_Made, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Exclamation, My.Resources.Save_changes_word)
            If result <> vbYes Then
                Return
            End If
        End If

        Settings.SaveSettings()

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("SSL")

    End Sub

    Private Sub InstallSSL()

        PictureBox1.Image = My.Resources.lock_time
        Using SSLProcess As New Process

            SSLProcess.StartInfo.UseShellExecute = True
            SSLProcess.StartInfo.WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "SSL")
            SSLProcess.StartInfo.FileName = "wacs.exe"
            SSLProcess.StartInfo.CreateNoWindow = False
            SSLProcess.StartInfo.UseShellExecute = False
            SSLProcess.StartInfo.RedirectStandardOutput = True

            SSLProcess.StartInfo.Arguments = $"--source manual --host {Settings.DNSName} --validation filesystem --webroot ""{IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs")}"" --store pemfiles --pemfilespath ""{IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\Certs")}"" "

            MakeSSLbatch($".\wacs.exe {SSLProcess.StartInfo.Arguments}")
            Dim log As String = ""
            Try
                SSLProcess.Start()
                log = SSLProcess.StandardOutput.ReadToEnd()
                SSLProcess.WaitForExit()
            Catch ex As Exception
                ErrorLog(ex.Message)
                PictureBox1.Image = My.Resources.lock_error
            End Try

            Dim Status = SSLProcess.ExitCode
            If Status = 0 Then
                Logger("Info", "Certificate installed", "SSL")
                ' It was not installed, so we need to restart Apache
                If Settings.SSLIsInstalled = False Then
                    Settings.SSLIsInstalled = True
                    PictureBox1.Image = My.Resources.lock_time
                End If
            Else
                Logger("SSL", log, "SSL")
                Baretail(IO.Path.Combine(Settings.CurrentDirectory, "Logs\SSL.log"))
                PictureBox1.Image = My.Resources.lock_error
            End If

        End Using
        Return
    End Sub

    Private Sub MakeSSLbatch(stuff As String)

        Dim filename = IO.Path.Combine(Settings.CurrentDirectory, "SSL\InstallSSL.bat")

        Using file As New System.IO.StreamWriter(filename, False)
            file.WriteLine("@REM program to renew SSL certificate")
            file.WriteLine($"cd {IO.Path.Combine(Settings.CurrentDirectory, "SSL")}")
            file.WriteLine(stuff)
            file.WriteLine("@pause")
        End Using

    End Sub

    Private Sub SSL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        EnableSSLCheckbox.Checked = Settings.SSLEnabled
        If Settings.SSLIsInstalled Then
            PictureBox1.Image = My.Resources.lock_ok
        Else
            PictureBox1.Image = My.Resources.lock_open
        End If

        If IsApacheRunning() Then
            StopButton.Enabled = True
            RestartButton.Enabled = True
            StartButton.Enabled = False
        Else
            StopButton.Enabled = False
            RestartButton.Enabled = False
            StartButton.Enabled = True
        End If

        If IsApacheRunning() Then
            PictureBox2.Image = My.Resources.gear_run
        Else
            PictureBox2.Image = My.Resources.gear_stop
        End If

        HelpOnce("SSL")
        initted = True

    End Sub

End Class