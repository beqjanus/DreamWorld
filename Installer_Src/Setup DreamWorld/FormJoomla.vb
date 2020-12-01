Imports System.Threading
Imports System.IO
Imports System.IO.Compression
Imports System.Text.RegularExpressions

Public Class FormJoomla
    Private Const JOpensim As String = "JOpensim"
    Private Const Hyperica As String = "Hyperica"

    Public Sub LoadSub() Handles Me.Load

        Translate.Run(Name)
        SetDefaults()
        HelpOnce(JOpensim)

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

        Dim result = MsgBox(My.Resources.InstallOpensim, vbYesNo)
        If result = vbYes Then
            InstallJOpensim()
        End If

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual(JOpensim)

    End Sub

    Private Sub InstallJOpensim()

        FormSetup.StartMySQL()

        Dim m As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\Jopensim_Files\Joomla+JOpensim.zip")
        If System.IO.File.Exists(m) Then
            InstallButton.Text = Global.Outworldz.My.Resources.Installing_word
            InstallButton.Image = Nothing
            FormSetup.StartApache()

            Dim JoomlaProcess As New Process()
            JoomlaProcess.StartInfo.FileName = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\MySQL\bin\Create_Joomla.bat")
            JoomlaProcess.StartInfo.WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\MySQL\bin\")
            JoomlaProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal

            Try
                JoomlaProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            JoomlaProcess.WaitForExit()

            Dim ctr As Integer = 0
            Dim extractPath = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\JOpensim")
            Dim fname As String = ""

            Try
                Using zip As ZipArchive = ZipFile.Open(m, ZipArchiveMode.Read)

                    For Each ZipEntry In zip.Entries

                        fname = ZipEntry.Name
                        If fname.Length = 0 Then
                            Continue For
                        End If

                        Application.DoEvents()
                        Dim destinationPath As String = Path.GetFullPath(Path.Combine(extractPath, ZipEntry.FullName))

                        FileStuff.DeleteFile(destinationPath)

                        Dim folder = System.IO.Path.GetDirectoryName(destinationPath)
                        Directory.CreateDirectory(folder)
                        InstallButton.Text = Global.Outworldz.My.Resources.Install_word & " " & CStr(ctr) & " of " & CStr(zip.Entries.Count)
                        ZipEntry.ExtractToFile(folder & "\" & ZipEntry.Name)
                        ctr += 1
                    Next
                End Using
            Catch ex As Exception
                FormSetup.Print($"Unable to extract file: {fname}:{ex.Message}")
                Thread.Sleep(3000)
                InstallButton.Text = Global.Outworldz.My.Resources.Error_word
                Return
            End Try

            InstallButton.Text = Global.Outworldz.My.Resources.Installed_word

        End If

        HelpManual(JOpensim)

        Dim webAddress As String = "http://127.0.0.1:" & Settings.ApachePort & "/JOpensim"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

        Me.Close()

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles HypericaRadioButton.CheckedChanged

        If HypericaRadioButton.Checked Then
            Settings.JOpensimSearch = Hyperica
            Settings.SaveSettings()

        End If

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles JOpensimRadioButton.CheckedChanged

        If JOpensimRadioButton.Checked Then
            Settings.JOpensimSearch = JOpensim
            Settings.SaveSettings()
        End If

    End Sub

    Private Sub SetDefaults()

        Dim count As Integer
        Try
            Dim folders() = IO.Directory.GetFiles(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\JOpensim"))
            count = folders.Length
        Catch
        End Try

        InstallButton.Enabled = False

        If Not Settings.ApacheEnable Then
            InstallButton.Enabled = False
            AdminButton.Enabled = False
            ViewButton.Enabled = False
            UpdateButton.Enabled = False
            BackupButton.Enabled = False
            ReinstallButton.Enabled = False
            MsgBox(My.Resources.Apache_Disabled)
            JOpensimRadioButton.Checked = False
            JOpensimRadioButton.Enabled = False
            Return
        End If

        If count <= 1 Then
            InstallButton.Enabled = True
        End If

        If count > 1 Then
            JOpensimRadioButton.Enabled = True
            JOpensimRadioButton.Checked = True
            AdminButton.Enabled = True
            ViewButton.Enabled = True
            UpdateButton.Enabled = True
            BackupButton.Enabled = True
            ReinstallButton.Enabled = True
        End If

    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/JOpensim/index.php?r=" & Random.ToString
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub RadioButton2_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

        Settings.JOpensimSearch = ""
        Settings.SaveSettings()

    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/JOpensim/administrator/index.php?option=com_installer&r=" & Random.ToString
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub BackupButton_Click(sender As Object, e As EventArgs) Handles BackupButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/JOpensim?r=" & Random.ToString
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub ReinstallButton_Click(sender As Object, e As EventArgs) Handles ReinstallButton.Click

        Dim path = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache")
        FileStuff.CopyFile(IO.Path.Combine(path, "Jopensim_Files\kickstart.php"), IO.Path.Combine(path, "htdocs\JOpensim\kickstart.php"), True)
        FileStuff.CopyFile(IO.Path.Combine(path, "Jopensim_Files\en-GB.kickstart.ini"), IO.Path.Combine(path, "htdocs\JOpensim\en-GB.kickstart.ini"), True)

        Dim webAddress As String = "http://" & Settings.PublicIP & "/JOpensim/kickstart.php?r=" & Random.ToString
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

End Class
