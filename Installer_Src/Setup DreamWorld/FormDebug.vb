Imports System.Text.RegularExpressions

Public Class FormDebug

    Private _abort As Boolean
    Private _backup As Boolean
    Private _command As String
    Private _value As Boolean

#Region "Properties"

    Public Property Abort As Boolean
        Get
            Return _abort
        End Get
        Set(value As Boolean)
            _abort = value
        End Set
    End Property

    Public Property Backup As Boolean
        Get
            Return _backup
        End Get
        Set(value As Boolean)
            _backup = value
        End Set
    End Property

    Public Property Command As String
        Get
            Return _command
        End Get
        Set(value As String)
            _command = value
        End Set
    End Property

    Public Property Value As Boolean
        Get
            Return _value
        End Get
        Set(value As Boolean)
            _value = value
        End Set
    End Property

    Public Sub ProgressPrint(Value As String)

        Log(My.Resources.Info_word, Value)
        TextBox1.Text += Value & vbCrLf
        If TextBox1.Text.Length > TextBox1.MaxLength - 1000 Then
            TextBox1.Text = Mid(TextBox1.Text, 1000)
        End If
    End Sub

#End Region

#Region "Start/Stop"

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Command = My.Resources.SmartStartEnable Then
            If Value Then
                ProgressPrint(My.Resources.SSisEnabled)
                Settings.SSVisible = True
                Settings.SmartStart = True
                HelpManual("SmartStart")
            Else
                ProgressPrint(My.Resources.SSisDisabled)
                Settings.SSVisible = False
                Settings.SmartStart = False
            End If
            Settings.SaveSettings()

        ElseIf Command = My.Resources.LoadFreeOars Then

            If Value = False Then Return

            If Button2.Text <> My.Resources.Apply_word Then
                Abort = True
                TextPrint(My.Resources.Stopping_word)
            End If

            Button2.Text = My.Resources.Stop_word

            Dim Caution = MsgBox(My.Resources.CautionOAR, vbYesNoCancel Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.Critical, My.Resources.Caution_word)
            If Caution <> MsgBoxResult.Yes Then Return

            If Abort Then
                Button2.Text = My.Resources.Apply_word
                Return
            End If

            Dim Estate = InputBox(My.Resources.WhatEstateName, My.Resources.WhatEstate, "")

            If Abort Then
                Button2.Text = My.Resources.Apply_word
                Return
            End If

            Dim CoordX = CStr(PropRegionClass.LargestX() + 18)
            Dim CoordY = CStr(PropRegionClass.LargestY() + 18)

            Dim coord = InputBox(My.Resources.WheretoStart, My.Resources.StartingLocation, CoordX & "," & CoordY)

            If Abort Then
                Button2.Text = My.Resources.Apply_word
                Return
            End If

            Dim pattern As Regex = New Regex("(\d+),(\d+)")
            Dim match As Match = pattern.Match(coord)
            If Not match.Success Then
                MsgBox(My.Resources.BadCoordinates, MsgBoxStyle.Exclamation Or MsgBoxStyle.MsgBoxSetForeground, My.Resources.Error_word)
                Return
            End If

            Dim X As Integer = CInt(match.Groups(1).Value)
            Dim Y As Integer = CInt(match.Groups(2).Value)
            Dim StartX As Integer = X

            If Abort Then
                Button2.Text = My.Resources.Apply_word
                Return
            End If

            If Not PropOpensimIsRunning() Then
                MysqlInterface.DeregisterRegions(False)
            End If

            FormSetup.Buttons(FormSetup.BusyButton)
            PropOpensimIsRunning() = True
            If Not StartMySQL() Then Return
            If Not StartRobust() Then Return
            If FormSetup.Timer1.Enabled = False Then
                FormSetup.Timer1.Interval = 1000
                FormSetup.Timer1.Start() 'Timer starts functioning
            End If

            Dim Max As Integer
            Try
                For Each J In FormSetup.ContentOAR.GetJson

                    If Abort Then Exit For
                    Dim Name = J.Name

                    Dim shortname = IO.Path.GetFileNameWithoutExtension(Name)
                    Dim RegionUUID As String
                    Dim p = IO.Path.Combine(Settings.OpensimBinPath, "Regions\" & shortname & "\Region\" & shortname & ".ini")
                    If Not IO.File.Exists(p) Then
                        RegionUUID = PropRegionClass.CreateRegion(shortname)
                    Else
                        RegionUUID = PropRegionClass.FindRegionByName(shortname)
                    End If

                    Dim g = New Guid
                    If Not Guid.TryParse(RegionUUID, g) Then
                        Continue For
                    End If

                    PropRegionClass.CrashCounter(RegionUUID) = 0

                    ' setup parameters for the load
                    Dim sizerow As Integer = 256

                    ' convert 1,2,3 to 256, 512, etc
                    Dim pattern1 As Regex = New Regex("(.*?)-(\d+)[xX](\d+)")
                    Dim match1 As Match = pattern1.Match(Name)
                    If match1.Success Then
                        Name = match1.Groups(1).Value
                        sizerow = CInt(match1.Groups(3).Value) * 256
                    End If

                    PropRegionClass.CoordX(RegionUUID) = X
                    PropRegionClass.CoordY(RegionUUID) = Y
                    PropRegionClass.Concierge(RegionUUID) = "True"
                    PropRegionClass.SmartStart(RegionUUID) = "True"
                    PropRegionClass.Teleport(RegionUUID) = "True"
                    PropRegionClass.SizeX(RegionUUID) = sizerow
                    PropRegionClass.SizeY(RegionUUID) = sizerow
                    PropRegionClass.GroupName(RegionUUID) = shortname
                    PropRegionClass.RegionIniFilePath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region\{shortname}.ini")
                    PropRegionClass.RegionIniFolderPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region")
                    PropRegionClass.OpensimIniPath(RegionUUID) = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}")

                    Dim port = PropRegionClass.LargestPort
                    PropRegionClass.GroupPort(RegionUUID) = port
                    PropRegionClass.RegionPort(RegionUUID) = port
                    PropRegionClass.WriteRegionObject(shortname)

                    ProgressPrint($"{My.Resources.Add_Region_word} {J.Name} @ {CStr(X)},{CStr(Y)}")
                    PropUpdateView = True ' make form refresh
                    Application.DoEvents()

                    If sizerow > Max Then Max = sizerow
                    X += CInt((sizerow / 256) + 1)
                    If X > StartX + 50 Then
                        X = StartX
                        Y += CInt((Max / 256) + 1)
                        sizerow = 256
                    End If

                    Dim RegionName = PropRegionClass.RegionName(RegionUUID)
                    If RegionName = Settings.WelcomeRegion Then Continue For

                    ProgressPrint($"{My.Resources.Start_word} {RegionName}")

                    If Abort Then Exit For
                    ReBoot(RegionUUID)

                    Sleep(3000)

                    If Abort Then Exit For
                    Dim c = 60
                    While PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Booting And Not Abort And c > 0
                        Sleep(1000)
                        c -= 1
                    End While
                    If c = 0 Then Continue For

                    If Abort Then Exit For
                    c = 120
                    While PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Booted And Not Abort And c > 0
                        Sleep(1000)
                        c -= 1
                    End While
                    If c = 0 Then Continue For

                    If GetPrimCount(RegionUUID) = 0 Then
                        Dim File = $"{PropDomain}/Outworldz_Installer/OAR/{J.Name}"
                        PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.NoError

                        If Estate.Length > 0 Then
                            ConsoleCommand(RegionUUID, "{ENTER}")
                            ConsoleCommand(RegionUUID, Estate)
                        Else
                            ProgressPrint(My.Resources.EnterEstateName)
                        End If

                        ConsoleCommand(RegionUUID, $"change region ""{RegionName}""")
                        ConsoleCommand(RegionUUID, $"load oar --force-terrain --force-parcels ""{File}""")
                        ConsoleCommand(RegionUUID, "generate map")
                        ConsoleCommand(RegionUUID, "backup")
                        ConsoleCommand(RegionUUID, "alert Power off!")
                        Sleep(5000)
                    End If

                    PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
                    PropUpdateView = True
                    ConsoleCommand(RegionUUID, "q")
                    ConsoleCommand(RegionUUID, "q")
                    Sleep(2000)
                    PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
                    PropUpdateView = True
                    Dim ctr = 120
                    If Settings.Sequential Then
                        If Abort Then Exit For
                        While PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Stopped
                            Sleep(1000)
                            Application.DoEvents()
                            ctr -= 1
                            If ctr = 0 Then Exit While
                        End While
                    End If
                    ProgressPrint($"{RegionName} {My.Resources.Loaded_word}")
                Next

                If Abort Then TextPrint(My.Resources.Stopped_word)
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

            Button2.Text = My.Resources.Apply_word

            TextPrint(My.Resources.New_is_Done)
            Settings.SaveSettings()
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        Command = CStr(ComboBox1.SelectedItem)
        Value = RadioTrue.Checked

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        ComboBox1.Items.Add(My.Resources.LoadFreeOars)
        ComboBox1.Items.Add(My.Resources.SmartStartEnable)

    End Sub

    Private Sub RadioTrue_CheckedChanged(sender As Object, e As EventArgs) Handles RadioTrue.CheckedChanged

        Value = RadioTrue.Checked

    End Sub

#End Region

End Class