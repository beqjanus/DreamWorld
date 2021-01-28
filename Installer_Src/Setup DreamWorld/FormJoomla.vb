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

Imports System.Threading
Imports System.IO
Imports Ionic.Zip
Imports System.Text.RegularExpressions

Public Class FormJoomla
    Private Const Hyperica As String = "Hyperica"
    Private Const JOpensim As String = "JOpensim"

    Public Sub LoadSub() Handles Me.Load

        AdminButton.Text = Global.Outworldz.My.Resources.AdministerJoomla_word
        ButtonBox.Text = Global.Outworldz.My.Resources.Settings_word
        SearchBox.Text = Global.Outworldz.My.Resources.Options
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        HypericaRadioButton.Text = Global.Outworldz.My.Resources.HypericaSearch_word
        InstallButton.Image = Global.Outworldz.My.Resources.gear_run
        InstallButton.Text = Global.Outworldz.My.Resources.InstallJoomla_word
        JOpensimRadioButton.Text = Global.Outworldz.My.Resources.JOpensimSearch_word
        ViewButton.Image = Global.Outworldz.My.Resources.edge
        ViewButton.Text = Global.Outworldz.My.Resources.ViewJoomla_word
        ButtonBox.Text = Global.Outworldz.My.Resources.Settings_word
        SearchBox.Text = Global.Outworldz.My.Resources.SearchOptions_word
        ReinstallButton.Text = Global.Outworldz.My.Resources.Restore_word
        UpdateButton.Text = Global.Outworldz.My.Resources.Update_word
        BackupButton.Text = Global.Outworldz.My.Resources.Backup_word

        SetDefaults()
        HelpOnce(JOpensim)

    End Sub

    Private Sub AdminButton_Click(sender As Object, e As EventArgs) Handles AdminButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/jOpensim/administrator"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub BackupButton_Click(sender As Object, e As EventArgs) Handles BackupButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/jOpensim/administrator/index.php?option=com_akeeba"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles InstallButton.Click

        Dim result = MsgBox(My.Resources.InstallOpensim, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground)
        If result = vbYes Then
            InstallJoomla()
        End If

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual(JOpensim)

    End Sub

    Private Sub InstallJoomla()

        StartMySQL()

        Dim m As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\jOpensim_Files\" & FormSetup.JOpensimRev1 & ".zip")
        If System.IO.File.Exists(m) Then
            InstallButton.Text = Global.Outworldz.My.Resources.Installing_word
            InstallButton.Image = Nothing
            StartApache()

            Dim JoomlaProcess As New Process()
            JoomlaProcess.StartInfo.FileName = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\MySQL\bin\Create_Joomla.bat")
            JoomlaProcess.StartInfo.WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\MySQL\bin\")
            JoomlaProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized

            Try
                JoomlaProcess.Start()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
            JoomlaProcess.WaitForExit()
            JoomlaProcess.Dispose()
            Dim ctr As Integer = 0
            Dim extractPath = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\JOpensim")
            InstallButton.Text = Global.Outworldz.My.Resources.Busy_word
            Application.DoEvents()
            Try
                Using zip As ZipFile = New ZipFile(m)
                    zip.ExtractAll(extractPath)
                End Using
            Catch ex As Exception
                TextPrint($"Unable to extract file {ex.Message}")
                Thread.Sleep(3000)
                InstallButton.Text = Global.Outworldz.My.Resources.Error_word
                Return
            End Try
            Application.DoEvents()
            InstallButton.Text = Global.Outworldz.My.Resources.Installed_word

            HelpManual(JOpensim)

            Dim webAddress As String = "http://127.0.0.1:" & Settings.ApachePort & "/jOpensim"
            Try
                Process.Start(webAddress)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

        End If

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

    Private Sub RadioButton2_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

        Settings.JOpensimSearch = ""
        Settings.SaveSettings()

    End Sub

    Private Sub ReinstallButton_Click(sender As Object, e As EventArgs) Handles ReinstallButton.Click

        Dim path = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles")
        CopyFileFast(IO.Path.Combine(path, "jOpensim_Files\kickstart.php"), IO.Path.Combine(path, "Apache\htdocs\JOpensim\kickstart.php"))
        CopyFileFast(IO.Path.Combine(path, "jOpensim_Files\en-GB.kickstart.ini"), IO.Path.Combine(path, "Apache\htdocs\JOpensim\en-GB.kickstart.ini"))
        CopyFileFast(IO.Path.Combine(path, "jOpensim_Files\Joomla+JOpensimV5.jpa"), IO.Path.Combine(path, "Apache\htdocs\JOpensim\Joomla+JOpensimV5.jpa"))

        Dim webAddress As String = "http://127.0.0.1/jOpensim/kickstart.php"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub SetDefaults()

        Dim installed As Boolean = Joomla.IsjOpensimInstalled()

        Select Case Settings.JOpensimSearch
            Case ""
                RadioButton2.Checked = True
            Case JOpensim
                JOpensimRadioButton.Checked = True
            Case "Hyperica"
                HypericaRadioButton.Checked = True
        End Select

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

        If Not installed Then
            HypericaRadioButton.Checked = True
            JOpensimRadioButton.Enabled = False
            InstallButton.Enabled = True
            AdminButton.Enabled = False
            ViewButton.Enabled = False
            UpdateButton.Enabled = False
            BackupButton.Enabled = False
            ReinstallButton.Enabled = False
        Else
            JOpensimRadioButton.Enabled = True
            AdminButton.Enabled = True
            ViewButton.Enabled = True
            UpdateButton.Enabled = True
            BackupButton.Enabled = True
            ReinstallButton.Enabled = True
        End If

    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/jOpensim/administrator/index.php?option=com_installer"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/jOpensim/index.php?r=" & Random.ToString
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

End Class
