#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Threading
Imports Ionic.Zip

Public Class FormJoomla

#Region "ScreenSize"

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

    Public Sub LoadSub() Handles Me.Load

        AdminButton.Text = Global.Outworldz.My.Resources.AdministerJoomla_word
        ButtonBox.Text = Global.Outworldz.My.Resources.Settings_word

        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word

        InstallButton.Image = Global.Outworldz.My.Resources.gear
        InstallButton.Text = Global.Outworldz.My.Resources.InstallJoomla_word

        ViewButton.Image = Global.Outworldz.My.Resources.edge
        ViewButton.Text = Global.Outworldz.My.Resources.ViewJoomla_word
        ButtonBox.Text = Global.Outworldz.My.Resources.Settings_word

        ReinstallButton.Text = Global.Outworldz.My.Resources.Restore_word
        UpdateButton.Text = Global.Outworldz.My.Resources.Update_word
        BackupButton.Text = Global.Outworldz.My.Resources.Backup_word
        SetScreen()
        SetDefaults()
        HelpOnce(JOpensim)

    End Sub

    Private Sub AdminButton_Click(sender As Object, e As EventArgs) Handles AdminButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/jOpensim/administrator"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

    Private Sub BackupButton_Click(sender As Object, e As EventArgs) Handles BackupButton.Click

        Dim webAddress As String = "http://" & Settings.PublicIP & "/jOpensim/administrator/index.php?option=com_akeeba"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Dump(ex)
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

        Dim m As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\jOpensim_Files\" & jOpensimRev & ".zip")
        If System.IO.File.Exists(m) Then
            InstallButton.Text = Global.Outworldz.My.Resources.Installing_word
            InstallButton.Image = Nothing
            StartApache()

            Using JoomlaProcess As New Process()
                JoomlaProcess.StartInfo.FileName = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\MySQL\bin\Create_Joomla.bat")
                JoomlaProcess.StartInfo.WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\MySQL\bin\")
                JoomlaProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized

                Try
                    JoomlaProcess.Start()
                Catch ex As Exception
                    BreakPoint.Dump(ex)
                End Try
                Application.DoEvents()
                JoomlaProcess.WaitForExit()
            End Using

            Dim ctr As Integer = 0
            Dim extractPath = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\JOpensim")
            InstallButton.Text = Global.Outworldz.My.Resources.Busy_word
            Application.DoEvents()
            Try
                Using zip As ZipFile = New ZipFile(m)
                    zip.UseZip64WhenSaving = Zip64Option.AsNecessary
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
                BreakPoint.Dump(ex)
            End Try

        End If

        Me.Close()

    End Sub

    Private Sub ReinstallButton_Click(sender As Object, e As EventArgs) Handles ReinstallButton.Click

        Dim path = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles")
        CopyFileFast(IO.Path.Combine(path, "jOpensim_Files\kickstart.php"), IO.Path.Combine(path, "Apache\htdocs\JOpensim\kickstart.php"))
        CopyFileFast(IO.Path.Combine(path, "jOpensim_Files\en-GB.kickstart.ini"), IO.Path.Combine(path, "Apache\htdocs\JOpensim\en-GB.kickstart.ini"))
        CopyFileFast(IO.Path.Combine(path, "jOpensim_Files\Joomla+JOpensimV5.jpa"), IO.Path.Combine(path, "Apache\htdocs\JOpensim\Joomla+JOpensimV5.jpa"))

        Dim webAddress As String = "http://127.0.0.1/jOpensim/kickstart.php"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

    Private Sub SetDefaults()

        Dim installed As Boolean = Joomla.IsjOpensimInstalled()

        InstallButton.Enabled = False

        If Not Settings.ApacheEnable Then
            InstallButton.Enabled = False
            AdminButton.Enabled = False
            ViewButton.Enabled = False
            UpdateButton.Enabled = False
            BackupButton.Enabled = False
            ReinstallButton.Enabled = False
            MsgBox(My.Resources.Apache_Disabled, vbInformation Or MsgBoxStyle.MsgBoxSetForeground)
            Return
        End If

        If Not installed Then
            InstallButton.Enabled = True
            AdminButton.Enabled = False
            ViewButton.Enabled = False
            UpdateButton.Enabled = False
            BackupButton.Enabled = False
            ReinstallButton.Enabled = False
        Else
            AdminButton.Enabled = True
            ViewButton.Enabled = True
            UpdateButton.Enabled = True
            BackupButton.Enabled = True
            ReinstallButton.Enabled = True
        End If

    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click

        Dim webAddress As String = $"http://{Settings.PublicIP}/jOpensim/administrator/index.php?option=com_installer"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim webAddress As String = $"http://{Settings.PublicIP}/jOpensim/index.php?r={Random.ToString}"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

End Class
