Imports System.Text.RegularExpressions

Public Class FormDebug

    Private _abort As Boolean
    Private _backup As Boolean
    Private _command As String
    Private _value As Boolean

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

#Region "Scrolling text box"

    Private Sub TextBox1_Changed(sender As System.Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim ln As Integer = TextBox1.Text.Length
        TextBox1.SelectionStart = ln
        TextBox1.ScrollToCaret()
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

        ElseIf Command = $"{My.Resources.Debug_word} Off" Then
            Settings.StatusInterval = 0
            Settings.SaveSettings()
        ElseIf Command = $"{My.Resources.Debug_word} 1 Minute" Then
            Settings.StatusInterval = 60
            Settings.SaveSettings()
        ElseIf Command = $"{My.Resources.Debug_word} 10 Minutes" Then
            Settings.StatusInterval = 10 * 60
            Settings.SaveSettings()
        ElseIf Command = $"{My.Resources.Debug_word} 60 Minutes" Then
            Settings.StatusInterval = 60 * 60
            Settings.SaveSettings()
        ElseIf Command = $"{My.Resources.Debug_word} 24 Hours" Then
            Settings.StatusInterval = 60 * 60 * 24
            Settings.SaveSettings()
        ElseIf Command = My.Resources.AutoFill Then
            Settings.AutoFill = Value
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

            Dim Estate = InputBox(My.Resources.WhatEstateName, My.Resources.WhatEstate, "Outworldz")

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
                    Dim p = IO.Path.Combine(Settings.OpensimBinPath, $"Regions\{shortname}\Region\{shortname}.ini")

                    If Not IO.File.Exists(p) Then
                        ProgressPrint($"{My.Resources.Add_Region_word} {J.Name} ")
                        RegionUUID = PropRegionClass.CreateRegion(shortname)
                    Else
                        RegionUUID = PropRegionClass.FindRegionByName(shortname)
                        If GetPrimCount(RegionUUID) > 0 Then
                            ProgressPrint($"{J.Name} {My.Resources.Ok} ")
                            Continue For
                        End If
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

                    ' Wait for it to start booting
                    If Not WaitForBooting(RegionUUID) Then Continue For
                    If Abort Then Exit For

                    If Settings.Sequential Then
                        If Not WaitForBooted(RegionUUID) Then Continue For
                    End If

                    If Abort Then Exit For

                    If GetPrimCount(RegionUUID) = 0 Then
                        Dim File = $"{PropDomain}/Outworldz_Installer/OAR/{J.Name}"
                        PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.NoError

                        If EstateName(RegionUUID).Length = 0 Then
                            ConsoleCommand(RegionUUID, Estate)
                        End If

                        ConsoleCommand(RegionUUID, $"change region ""{RegionName}""")
                        ConsoleCommand(RegionUUID, $"load oar --force-terrain --force-parcels ""{File}""")

                        If Settings.MapType <> "None" Or PropRegionClass.MapType(RegionUUID).Length > 0 Then
                            ConsoleCommand(RegionUUID, "generate map")
                            Sleep(10000) ' wait a bit to let it make a mas
                        End If

                        ConsoleCommand(RegionUUID, "backup")
                        ConsoleCommand(RegionUUID, "alert Power off!")
                        PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
                        ConsoleCommand(RegionUUID, "quit")
                        ConsoleCommand(RegionUUID, "quit")
                        Sleep(100)
                    Else
                        PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
                        ConsoleCommand(RegionUUID, "quit")
                        ConsoleCommand(RegionUUID, "quit")
                    End If

                    PropUpdateView = True

                    If Abort Then Exit For

                    PropUpdateView = True
                    Dim ctr = 120
                    If Settings.Sequential Then
                        If Abort Then Exit For
                        While PropRegionClass.Status(RegionUUID) <> RegionMaker.SIMSTATUSENUM.Stopped AndAlso Not Abort
                            Sleep(1000)
                            Application.DoEvents()
                            ctr -= 1
                            If ctr = 0 Then Exit While
                        End While
                    End If
                    ProgressPrint($"{RegionName} {My.Resources.Loaded_word}")
                Next

                If Abort Then
                    TextPrint(My.Resources.Stopped_word)
                End If
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

        If Debugger.IsAttached Then
            ComboBox1.Items.Add(My.Resources.SmartStartEnable)
            ComboBox1.Items.Add(My.Resources.Trees)
            ComboBox1.Items.Add(My.Resources.AutoFill)
        End If

        RadioTrue.Checked = False
        RadioFalse.Checked = True

        RadioTrue.Text = My.Resources.True_word
        RadioFalse.Text = My.Resources.False_word

        ComboBox1.Items.Add(My.Resources.LoadFreeOars)
        ComboBox1.Items.Add($"{My.Resources.Debug_word} Off")
        ComboBox1.Items.Add($"{My.Resources.Debug_word} 1 Minute")
        ComboBox1.Items.Add($"{My.Resources.Debug_word} 10 Minutes")
        ComboBox1.Items.Add($"{My.Resources.Debug_word} 60 Minutes")
        ComboBox1.Items.Add($"{My.Resources.Debug_word} 24 hours")

        SetScreen()

    End Sub

#End Region

#Region "Radio"

    Private Sub RadioTrue_CheckedChanged(sender As Object, e As EventArgs) Handles RadioTrue.CheckedChanged

        Value = RadioTrue.Checked

    End Sub

#End Region

#Region "Trees"

    Private Sub PlantTrees()
#Disable Warning CA2000
        Dim TreeForm = New FormTrees
#Enable Warning CA2000
        TreeForm.BringToFront()
        TreeForm.Activate()
        TreeForm.Visible = True
        TreeForm.Select()
    End Sub

#End Region

End Class