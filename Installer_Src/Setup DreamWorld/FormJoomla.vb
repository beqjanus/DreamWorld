Imports Ionic.Zip

Public Class FormJoomla

    Public Sub LoadSub() Handles Me.Load

        Dim folders() = IO.Directory.GetFiles(Settings.CurrentDirectory & "\Outworldzfiles\Apache\htdocs\JOpensim")
        Dim count = folders.Length

        If count <= 1 Then InstallButton.Enabled = True

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

        HelpOnce("JOpensim")

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

        HelpManual("JOpensim")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles InstallButton.Click

        Dim result = MsgBox("This will install Joomla. Do you wish to continue?", vbYesNo)
        If result = vbYes Then
            InstallJoomla()
        End If

    End Sub

    Private Sub AdminButton_Click(sender As Object, e As EventArgs) Handles AdminButton.Click

        Dim webAddress As String = Settings.PublicIP & "/JOpensim/administrator"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim webAddress As String = Settings.PublicIP & "/JOpensim"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
        End Try

    End Sub

    Private Shared Sub InstallJoomla()

        Dim m As String = Settings.CurrentDirectory & "\Outworldzfiles\Apache\htdocs\Jopensim_Files\Joomla+Jopensim.zip"
        If System.IO.File.Exists(m) Then
            Using zip As ZipFile = ZipFile.Read(m)
                For Each ZipEntry In zip
                    ChDir(Settings.CurrentDirectory & "\Outworldzfiles\Apache\htdocs\JOpensim")
                    ZipEntry.Extract(m, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                Next
            End Using
        End If
        ChDir(Settings.CurrentDirectory)

        HelpManual("Joomla")

    End Sub

End Class