#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Text.RegularExpressions

Public Class FormLogging

#Region "Private Fields"

    Private _Avictr As Integer
    Dim _changed As Boolean
    Private _Ctr As Integer
    Private _Err As Integer
    Dim initted As Boolean

#End Region

#Region "FormPos"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Start/Stop"

    Private Sub Close_form(sender As Object, e As EventArgs) Handles Me.Closed

        Settings.SaveSettings()

        If _changed Then SendMsg(Settings.LogLevel.ToUpperInvariant)

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        GroupBox1.Text = Global.Outworldz.My.Resources.Log_Level
        HelpToolStripMenuItem.Image = Global.Outworldz.My.Resources.question_and_answer
        HelpToolStripMenuItem.Text = Global.Outworldz.My.Resources.Help_word
        RadioDebug.Text = Global.Outworldz.My.Resources.Debug_word
        RadioError.Text = Global.Outworldz.My.Resources.Error_word
        RadioFatal.Text = Global.Outworldz.My.Resources.Fatal_word
        RadioInfo.Text = Global.Outworldz.My.Resources.Info_word
        RadioOff.Text = Global.Outworldz.My.Resources.Off
        RadioWarn.Text = Global.Outworldz.My.Resources.Warn_word
        DeleteOnBoot.Text = Global.Outworldz.My.Resources.DeletebyAge
        KeepLog.Text = Global.Outworldz.My.Resources.KeepAlways
        ViewLogButton.Text = Global.Outworldz.My.Resources.View_Logs
        AnalyzeButton.Text = Global.Outworldz.My.Resources.AnalyzeLogButton

        DeleteOnBoot.Checked = Settings.DeleteByDate
        KeepLog.Checked = Not Settings.DeleteByDate

        SetScreen()

        Select Case Settings.LogLevel.ToUpperInvariant
            Case "OFF"
                RadioOff.Checked = True
            Case "DEBUG"
                RadioDebug.Checked = True
            Case "INFO"
                RadioInfo.Checked = True
            Case "WARN"
                RadioWarn.Checked = True
            Case "ERROR"
                RadioError.Checked = True
            Case "FATAL"
                RadioFatal.Checked = True
            Case Else
                RadioInfo.Checked = True
        End Select

        HelpOnce("Logging")
        initted = True

    End Sub

#End Region

#Region "Help"

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        HelpManual("Logging")
    End Sub

#End Region

#Region "SetLogging"

    Private Sub Delete_CheckedChanged(sender As Object, e As EventArgs) Handles DeleteOnBoot.CheckedChanged

        If Not initted Then Return
        If DeleteOnBoot.Checked Then Settings.DeleteByDate = True

    End Sub

    Private Sub KeepLog_CheckedChanged(sender As Object, e As EventArgs) Handles KeepLog.CheckedChanged

        If Not initted Then Return
        If KeepLog.Checked Then Settings.DeleteByDate = False

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioOff.CheckedChanged
        If Not initted Then Return
        Settings.LogLevel = "OFF"
        _changed = True
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioDebug.CheckedChanged
        If Not initted Then Return
        Settings.LogLevel = "DEBUG"
        _changed = True
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioInfo.CheckedChanged
        If Not initted Then Return
        Settings.LogLevel = "INFO"
        _changed = True
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioWarn.CheckedChanged
        If Not initted Then Return
        Settings.LogLevel = "WARN"
        _changed = True
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioError.CheckedChanged
        If Not initted Then Return
        Settings.LogLevel = "ERROR"
        _changed = True
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioFatal.CheckedChanged
        If Not initted Then Return
        Settings.LogLevel = "FATAL"
        _changed = True
    End Sub

#End Region

#Region "ExamineLogs"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ViewLogButton.Click
        Process.Start("explorer.exe", IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs"))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles AnalyzeButton.Click

        AnalyzeButton.Text = Global.Outworldz.My.Resources.Busy_word
        DeleteFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Avatars.csv"))

        ExamineAvatars(IO.Path.Combine(Settings.OpensimBinPath, "Robust.log"))

        Dim filename As String = IO.Path.Combine(Settings.CurrentDirectory, $"OutworldzFiles\Logs\Regions.csv")
        Using outputFile As New StreamWriter(filename, True)
            outputFile.WriteLine("""Date"",""Type"",""Problem"",""Coordinates"",""UUID"",""Hop"", ""Group""")

            For Each UUID As String In PropRegionClass.RegionUuids
                Application.DoEvents()
                Dim GroupName = PropRegionClass.GroupName(UUID)
                _Err += ExamineOpensim(outputFile, $"{Settings.OpensimBinPath()}\Regions\{GroupName}\Opensim.log", GroupName)
            Next
        End Using
        If _Err > 0 Then Process.Start(IO.Path.Combine(Settings.CurrentDirectory, $"OutworldzFiles\Logs\Regions.csv"))

        ExamineAllLogs(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Logs"))

        AnalyzeButton.Text = Global.Outworldz.My.Resources.AnalyzeLogButton

    End Sub

    Private Sub ExamineAllLogs(Log As String)

    End Sub

    Private Sub ExamineAvatars(Log As String)

        Try
            Dim filename As String = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Avatars.csv")
            Using outputFile As New StreamWriter(filename, True)
                outputFile.WriteLine("""Date"",""Avatar"",""UUID"",""Viewer"",""Channel"",""IP"",""MAC"",""ID0"",""Region"",""Grid""")

                If System.IO.File.Exists(Log) Then
                    Using reader As StreamReader = System.IO.File.OpenText(Log)
                        'now loop through each line
                        While reader.Peek <> -1
                            Application.DoEvents()
                            Lookat(reader.ReadLine(), outputFile)
                        End While
                    End Using
                End If

            End Using
            If _Avictr > 0 Then Process.Start(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Logs\Avatars.csv"))
        Catch ex As Exception
            MsgBox("File in use, try later:  " & ex.Message)
        End Try

    End Sub

    Private Function ExamineOpensim(outputfile As StreamWriter, Log As String, GroupName As String) As Integer

        Dim E As Integer
        Try

            If System.IO.File.Exists(Log) Then
                Using reader As StreamReader = System.IO.File.OpenText(Log)
                    'now loop through each line
                    While reader.Peek <> -1
                        _Ctr += 1
                        E += LookatOpensim(reader.ReadLine(), outputfile, GroupName)
                        ToolStripStatusLabel1.Text = $"{CStr(_Err)} Errors,  {CStr(_Ctr)} Lines"
                        Application.DoEvents()
                    End While
                End Using
            End If
        Catch ex As Exception
            MsgBox("File in use, try later:  " & ex.Message)
        End Try

        Return E

    End Function

    Private Sub Lookat(line As String, outputfile As StreamWriter)

        _Ctr += 1
        Dim pattern = New Regex("(.*?),.*?INFO.*?Login request for (.*?) \((.*?)\).*?viewer (.*?), channel (.*?), IP (.*?), Mac (.*?), Id0 (.*?),.*?region (.*?) \(.*?\@ (.*)")

        Dim match As Match = pattern.Match(line)
        If match.Success Then
            Dim DateTime = match.Groups(1).Value
            Dim Avatar = match.Groups(2).Value.Replace(" ", "")
            Dim UUID = match.Groups(3).Value
            Dim Viewer = match.Groups(4).Value
            Dim Channel = match.Groups(5).Value
            Dim IP = match.Groups(6).Value
            Dim MAC = match.Groups(7).Value
            Dim Id0 = match.Groups(8).Value
            Dim Region = match.Groups(9).Value
            Dim Grid = match.Groups(10).Value
            Grid = Grid.Replace("\n", "").Replace("\r", "")
            _Avictr += 1
            ToolStripStatusLabel1.Text = $"{CStr(_Avictr)} Avatars,  {CStr(_Ctr)} Lines"
            outputfile.WriteLine($"""{DateTime}"",""{Avatar}"",""{UUID}"",""{Viewer}"",""{Channel}"",""{IP}"",""{MAC}"",""{Id0}"",""{Region}"",""{Grid}""")
        End If

    End Sub

    Private Function LookatOpensim(line As String, outputfile As StreamWriter, GroupName As String) As Integer

        ' Dim pattern = New Regex("(.*?),.*?ERROR(.*?)(<.*?,.*?,.*?>)(.*)")
        Dim pattern = New Regex("(.*?),.*?ERROR(.*?)(<.*?,.*?,.*?>)(.*)")
        Dim match As Match = pattern.Match(line)
        If match.Success Then
            Dim DateTime = match.Groups(1).Value
            Dim Preamble = match.Groups(2).Value
            Dim Vector = match.Groups(3).Value
            Dim Last = match.Groups(4).Value
            ToolStripStatusLabel1.Text = $"{CStr(_Err)} Errors,  {CStr(_Ctr)} Lines"
            outputfile.WriteLine($"""{DateTime}"",""ERROR"",""{Preamble}"",""{Vector}"",""{Last}"", ""{GroupName}""")
            Return 1
        End If
        Return 0

    End Function

#End Region

End Class
