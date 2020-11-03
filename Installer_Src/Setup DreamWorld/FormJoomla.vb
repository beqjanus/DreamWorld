Imports Ionic.Zip

Public Class FormJoomla

    Public Sub LoadSub() Handles Me.Load

        SetDefaults()
        HelpOnce("JOpensim")

    End Sub

    Private Sub AdminButton_Click(sender As Object, e As EventArgs) Handles AdminButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/JOpensim/administrator"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles InstallButton.Click

        Dim result = MsgBox("This will install Joomla. Do you wish to continue?", vbYesNo)
        If result = vbYes Then
            InstallJoomla()
        End If

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("JOpensim")

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="System.Windows.Forms.ButtonBase.set_Text(System.String)")>
    Private Sub InstallJoomla()

        Dim m As String = Settings.CurrentDirectory & "\OutworldzFiles\Apache\Jopensim_Files\Joomla+Jopensim.zip"
        If System.IO.File.Exists(m) Then
            InstallButton.Text = Global.Outworldz.My.Resources.Installing_word
            Form1.StartApache()

            Dim JoomlaProcess As New Process()
            JoomlaProcess.StartInfo.FileName = Settings.CurrentDirectory & "\OutworldzFiles\MySQL\bin\Create_Joomla.bat"
            JoomlaProcess.StartInfo.WorkingDirectory = Settings.CurrentDirectory & "\OutworldzFiles\MySQL\bin\"
            JoomlaProcess.StartInfo.CreateNoWindow = True
            Try
                JoomlaProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            JoomlaProcess.WaitForExit()

            Dim ctr As Integer = 0
            Dim n = Settings.CurrentDirectory & "\OutworldzFiles\Apache\htdocs\JOpensim"
            Using zip As ZipFile = ZipFile.Read(m)
                For Each ZipEntry In zip
                    ZipEntry.Extract(n, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                    InstallButton.Text = Global.Outworldz.My.Resources.Installing_word & " " & CStr(ctr)
                    Application.DoEvents()
                    ctr += 1
                Next
            End Using
            InstallButton.Text = Global.Outworldz.My.Resources.Installed_word

        End If

        HelpManual("JOpensim")
        AdminButton.Enabled = True
        ViewButton.Enabled = True
        InstallButton.Enabled = False
        Dim webAddress As String = "http://" & Settings.PublicIP & "/JOpensim"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub JEnableCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles JEnableCheckBox.CheckedChanged

        If JEnableCheckBox.Checked Then
            Settings.CMS = "Joomla"
        Else
            Settings.CMS = "DreamGrid"
        End If

        Settings.SaveSettings()
        SetDefaults()

    End Sub

    Private Sub SetDefaults()

        Dim folders() = IO.Directory.GetFiles(Settings.CurrentDirectory & "\Outworldzfiles\Apache\htdocs\JOpensim")
        Dim count = folders.Length
        InstallButton.Enabled = False

        If count <= 1 Then
            If Settings.ApacheEnable Then
                InstallButton.Enabled = True
            End If
        End If

        If Settings.CMS = "Joomla" Then
            JEnableCheckBox.Checked = True
            If count > 1 Then
                AdminButton.Enabled = True
                ViewButton.Enabled = True
            End If
        Else
            AdminButton.Enabled = False
            ViewButton.Enabled = False
        End If

    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/JOpensim?r=" & Random.ToString
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

End Class
