#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO

Module Apache

    Private WithEvents ApacheProcess As New Process()
    Private _ApacheCrashCounter As Integer
    Private _ApacheExited As Boolean
    Private _ApacheProcessID As Integer
    Private _ApacheUninstalling As Boolean

#Region "Properties"

    Public Property ApacheCrashCounter As Integer
        Get
            Return _ApacheCrashCounter
        End Get
        Set(value As Integer)
            _ApacheCrashCounter = value
        End Set
    End Property

    Public Property PropApacheExited() As Boolean
        Get
            Return _ApacheExited
        End Get
        Set(ByVal Value As Boolean)
            _ApacheExited = Value
        End Set
    End Property

    Public Property PropApacheProcessID As Integer
        Get
            Return _ApacheProcessID
        End Get
        Set(value As Integer)
            _ApacheProcessID = value
        End Set
    End Property

    Public Property PropApacheUninstalling() As Boolean
        Get
            Return _ApacheUninstalling
        End Get
        Set(ByVal Value As Boolean)
            _ApacheUninstalling = Value
        End Set
    End Property

    Public Sub ApacheIcon(Running As Boolean)

        If Not Running Then
            FormSetup.RestartApacheIcon.Image = Global.Outworldz.My.Resources.nav_plain_red
        Else
            FormSetup.RestartApacheIcon.Image = Global.Outworldz.My.Resources.check2
        End If
        Application.DoEvents()

    End Sub

#End Region

    Public Function CheckApache() As Boolean
        ''' <summary>Check is Apache port 80 or 8000 is up</summary>
        ''' <returns>boolean</returns>
        Using client As New Net.WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.PublicIP & ":" & CStr(Settings.ApachePort) & "/?_Opensim=" & RandomNumber.Random)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                If ex.Message.Contains("200 OK") Then Return True
                Return False
            End Try
            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If

        End Using

        Return True

    End Function

    Public Sub StartApache()

        ' Depends upon PHP for home page
        DoPHPDBSetup()

        SetPath()

        If Settings.SiteMap Then
            Dim SiteMapContents = "<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf
            SiteMapContents += "<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.0909"">" & vbCrLf
            SiteMapContents += "<url>" & vbCrLf
            SiteMapContents += "<loc>http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/" & "</loc>" & vbCrLf

            If Settings.CMS = JOpensim Then
                SiteMapContents += "<loc>http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/jOpensim" & "</loc>" & vbCrLf
            End If

            If Settings.CMS = "WordPress" Then
                SiteMapContents += "<loc>http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/DreamGrid" & "</loc>" & vbCrLf
            End If

            If Settings.CMS = "Other" Then
                SiteMapContents += "<loc>http://" & Settings.PublicIP & ":" & Convert.ToString(Settings.ApachePort, Globalization.CultureInfo.InvariantCulture) & "/Other" & "</loc>" & vbCrLf
            End If

            SiteMapContents += "<changefreq>daily</changefreq>" & vbCrLf
            SiteMapContents += "<priority>1.0</priority>" & vbCrLf
            SiteMapContents += "</url>" & vbCrLf
            SiteMapContents += "</urlset>" & vbCrLf

            Using outputFile As New StreamWriter(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\Sitemap.xml"), False)
                outputFile.WriteLine(SiteMapContents)
            End Using

        End If

        If Not Settings.ApacheEnable Then
            ApacheIcon(False)
            TextPrint(My.Resources.Apache_Disabled)
            Return
        End If

        If Settings.ApachePort = 80 Then
            ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            ApacheProcess.StartInfo.FileName = "net"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.Arguments = "stop W3SVC"
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            ApacheProcess.StartInfo.CreateNoWindow = True
            Try
                ApacheProcess.Start()
                ApacheProcess.WaitForExit()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

        End If

        If Settings.CurrentDirectory <> Settings.LastDirectory Or Not ApacheExists() Then

            Settings.LastDirectory = Settings.CurrentDirectory
            Settings.SaveSettings()

            ' Stop MSFT server if we are on port 80 and enabled
            PropApacheUninstalling = True

            ' old stuff we had named this way
            ApacheProcess.StartInfo.Arguments = "stop " & """" & "Apache HTTP Server" & """"
            Try
                ApacheProcess.Start()
                ApacheProcess.WaitForExit()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()

            'delete really old service
            ApacheProcess.StartInfo.FileName = "sc"
            ApacheProcess.StartInfo.Arguments = " delete  " & """" & "Apache HTTP Server" & """"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            Try
                ApacheProcess.Start()
                ApacheProcess.WaitForExit()
            Catch ex As Exception
            End Try
            Application.DoEvents()

            Application.DoEvents()
            Using ApacheProcess As New Process With {
                    .EnableRaisingEvents = False
                }
                ApacheProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
                ApacheProcess.StartInfo.FileName = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\bin\httpd.exe")
                ApacheProcess.StartInfo.Arguments = "-k install -n " & """" & "ApacheHTTPServer" & """"
                ApacheProcess.StartInfo.CreateNoWindow = True
                ApacheProcess.StartInfo.WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\bin\")
                ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

                DoApache()

                Try
                    ApacheProcess.Start()
                    ApacheProcess.WaitForExit()
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                    ApacheIcon(False)
                End Try
                Application.DoEvents()

                If ApacheProcess.ExitCode <> 0 Then
                    TextPrint(My.Resources.ApacheFailed)
                    ApacheIcon(False)
                Else
                    PropApacheUninstalling = False ' installed now, trap errors
                    Settings.OldInstallFolder = Settings.CurrentDirectory
                End If

                Application.DoEvents()
            End Using

        End If

        TextPrint(My.Resources.Apache_starting)
        DoApache()

        Using ApacheProcess As New Process With {
                    .EnableRaisingEvents = False
                }
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            ApacheProcess.StartInfo.FileName = "net"
            ApacheProcess.StartInfo.Arguments = "start ApacheHTTPServer"
            ApacheProcess.StartInfo.UseShellExecute = False
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.RedirectStandardError = True
            ApacheProcess.StartInfo.RedirectStandardOutput = True
            Dim response As String = ""

            Try
                ApacheProcess.Start()
                response = ApacheProcess.StandardOutput.ReadToEnd() & ApacheProcess.StandardError.ReadToEnd()
                ApacheProcess.WaitForExit()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                TextPrint(My.Resources.Apache_Failed & ":" & ex.Message)
            End Try
            Application.DoEvents()

            If ApacheProcess.ExitCode <> 0 Then
                If response.Contains("has already been started") Then
                    ApacheIcon(True)
                    Return
                End If
                TextPrint(My.Resources.Apache_Failed & ":" & CStr(ApacheProcess.ExitCode))
                ApacheIcon(False)
            Else
                TextPrint(My.Resources.Apache_running & ":" & Settings.ApachePort)
                ApacheIcon(True)
            End If

        End Using

    End Sub

    Public Sub StopApache(force As Boolean)

        If Not Settings.ApacheEnable Then Return
        If Not force Then Return

        Using ApacheProcess As New Process()
            TextPrint(My.Resources.Stopping_Apache)

            ApacheProcess.StartInfo.FileName = "net.exe"
            ApacheProcess.StartInfo.Arguments = "stop ApacheHTTPServer"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            Try
                ApacheProcess.Start()
                ApacheProcess.WaitForExit()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                TextPrint(My.Resources.ApacheNot_Stopping & ":" & ex.Message)
            End Try

        End Using

        ApacheIcon(False)

    End Sub

    Private Function ApacheExists() As Boolean

        Using ApacheProcess As New Process()
            ApacheProcess.StartInfo.RedirectStandardOutput = True
            ApacheProcess.StartInfo.RedirectStandardError = True
            ApacheProcess.StartInfo.RedirectStandardInput = True
            ApacheProcess.StartInfo.UseShellExecute = False
            ApacheProcess.StartInfo.FileName = "sc.exe"
            ApacheProcess.StartInfo.Arguments = "query ApacheHTTPServer"
            ApacheProcess.StartInfo.CreateNoWindow = True
            ApacheProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

            Dim console As String = ""
            Try
                ApacheProcess.Start()
                console = ApacheProcess.StandardOutput.ReadToEnd()

                ApacheProcess.WaitForExit()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                TextPrint(My.Resources.ApacheNot_Stopping & ":" & ex.Message)
            End Try
            If console.Contains("does not exist") Then Return False
            Return True

        End Using

    End Function

    'Handle Exited Event And display process information.
    Private Sub ApacheProcess_Exited(ByVal sender As Object, ByVal e As EventArgs) Handles ApacheProcess.Exited

        FormSetup.RestartApacheIcon.Image = Global.Outworldz.My.Resources.nav_plain_red

        If PropAborting Then Return
        If PropApacheUninstalling Then Return

        If Settings.RestartOnCrash And ApacheCrashCounter < 10 Then
            ApacheCrashCounter += 1
            PropApacheExited = True
            ApacheIcon(False)
            Return
        End If
        ApacheCrashCounter = 0
        PropApacheProcessID = Nothing

        Dim yesno = MsgBox(My.Resources.Apache_Exited, MsgBoxStyle.YesNo Or MsgBoxStyle.MsgBoxSetForeground, Global.Outworldz.My.Resources.Error_word)
        If (yesno = vbYes) Then
            Dim Apachelog As String = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Logs\Apache\error*.log")
            Try
                System.Diagnostics.Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "baretail.exe"), """" & Apachelog & """")
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End If

    End Sub

    Private Sub SetPath()

        Dim DLLList As New List(Of String) From {"libeay32.dll", "libssh2.dll", "ssleay32.dll"}

        For Each item In DLLList
            Dim dest As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), item)
            If Not IO.File.Exists(dest) Then
                CopyFileFast(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\PHP7\curl\" & item), dest)
            End If
        Next

    End Sub

End Module
