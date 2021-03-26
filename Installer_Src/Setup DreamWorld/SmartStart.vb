#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC. AGPL3.0 https://opensource.org/licenses/AGPL

#End Region

Imports System.Globalization
Imports System.IO
Imports System.Threading

Module SmartStart

    Private WithEvents BootProcess As New Process

    Private ReadOnly Sleeping As New List(Of String)

#Region "Teleport"

    Public Function RegionListHTML(Settings As MySettings, PropRegionClass As RegionMaker) As String

        ' http://localhost:8001/teleports.htm
        ' http://YourURL:8001/teleports.htm
        'Outworldz|Welcome||outworldz.com:9000:Welcome|128,128,96|
        '*|Welcome||outworldz.com9000Welcome|128,128,96|
        Dim HTML As String = ""

        Dim ToSort As New List(Of String)

        For Each RegionUUID As String In PropRegionClass.RegionUuids
            Dim status = PropRegionClass.Status(RegionUUID)
            If (PropRegionClass.Teleport(RegionUUID) = "True" AndAlso
                status = RegionMaker.SIMSTATUSENUM.Booted) Or
               (PropRegionClass.Teleport(RegionUUID) = "True" AndAlso
                PropRegionClass.SmartStart(RegionUUID) = "True" AndAlso Settings.SmartStart) Then
                ToSort.Add(PropRegionClass.RegionName(RegionUUID))
            End If
        Next

        ToSort.Sort()

        'TODO   "||"  is coordinates for destinations

        For Each S As String In ToSort
            HTML += "*|" & S & "||" & Settings.PublicIP & ":" & Settings.HttpPort & ":" & S & "||" & S & "|" & vbCrLf
        Next

        Dim HTMLFILE = Settings.OpensimBinPath & "data\teleports.htm"
        DeleteFile(HTMLFILE)

        Try
            Using outputFile As New StreamWriter(HTMLFILE, True)
                outputFile.WriteLine(HTML)
            End Using
        Catch ex As Exception
            ' BreakPoint.Show(ex.Message)
        End Try

        Return HTML

    End Function

#End Region

    Public Function CalcDiskFree() As Long

        Dim d = DriveInfo.GetDrives()
        Dim c = CurDir()
        Dim Free As Long
        Try
            For Each drive As DriveInfo In d
                Dim x = Mid(c, 1, 1)
                If x = Mid(drive.Name, 1, 1) Then
                    Dim Percent = drive.AvailableFreeSpace / drive.TotalSize
                    Dim FreeDisk = Percent * 100
                    Free = drive.TotalSize - drive.AvailableFreeSpace

                    If Sleeping.Count = 0 Then
                        If Free < FreeDiskSpaceWarn Then
                            Dim SThread = New Thread(AddressOf FreezeAll)
                            SThread.SetApartmentState(ApartmentState.STA)
                            SThread.Priority = ThreadPriority.BelowNormal ' UI gets priority
                            SThread.Start()
                            Busy = True
                            MsgBox(My.Resources.Diskspacelow & $" {Free:n0} Bytes", vbInformation Or MsgBoxStyle.MsgBoxSetForeground)
                        End If
                    End If

                    Dim tt = My.Resources.Available
                    Dim Text = $"{Percent:P1} {tt}"

                    FormSetup.DiskSize.Text = $"Disk {x}: {Text} "
                    Exit For
                End If

            Next
        Catch
        End Try

        Return Free

    End Function

    Public Sub DoSuspend(RegionName As String)

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(RegionName)
        Dim PID = PropRegionClass.ProcessID(RegionUUID)

        Logger("State Changed to ShuttingDown", RegionName, "Teleport")
        Dim GroupName = PropRegionClass.GroupName(RegionUUID)
        For Each UUID In PropRegionClass.RegionUuidListByName(GroupName)
            PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.ShuttingDownForGood
            PropRegionClass.Timer(RegionUUID) = Date.Now ' wait another interval
        Next
        ShutDown(RegionUUID)
        Application.DoEvents()
        PropUpdateView = True ' make form refresh

    End Sub

    Public Sub FreezeAll()

        Dim running As Boolean
        For Each RegionUUID In PropRegionClass.RegionUuids()
            Dim status = PropRegionClass.Status(RegionUUID)
            If Not Sleeping.Contains(RegionUUID) Then
                Sleeping.Add(RegionUUID)
                Select Case status
                    Case RegionMaker.SIMSTATUSENUM.Booted
                        Pause(RegionUUID)
                        running = True
                    Case RegionMaker.SIMSTATUSENUM.Booting
                        Pause(RegionUUID)
                        running = True
                    Case RegionMaker.SIMSTATUSENUM.Error
                    Case RegionMaker.SIMSTATUSENUM.NoLogin
                        Pause(RegionUUID)
                        running = True
                    Case RegionMaker.SIMSTATUSENUM.RecyclingDown
                        Pause(RegionUUID)
                        running = True
                    Case RegionMaker.SIMSTATUSENUM.RecyclingUp
                    Case RegionMaker.SIMSTATUSENUM.RestartPending
                    Case RegionMaker.SIMSTATUSENUM.RestartStage2
                    Case RegionMaker.SIMSTATUSENUM.RetartingNow
                    Case RegionMaker.SIMSTATUSENUM.Stopped
                    Case RegionMaker.SIMSTATUSENUM.Suspended
                End Select
            End If
        Next

        PropUpdateView = True ' make form refresh
        If Not running Then
            Busy = False
            Sleeping.Clear()
            Return
        End If

        While CalcDiskFree() < FreeDiskSpaceWarn AndAlso PropOpensimIsRunning
            Sleep(1000)
        End While

        For Each RegionUUID In Sleeping
            Dim RegionName = PropRegionClass.RegionName(RegionUUID)
            FreezeThaw(RegionUUID, "-rpid " & PropRegionClass.ProcessID(RegionUUID))
            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted
        Next

        Sleeping.Clear()
        Busy = False

        PropUpdateView = True ' make form refresh

    End Sub

    Private Sub FreezeThaw(RegionUUID As String, Arg As String)

        Using SuspendProcess As New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                .Arguments = Arg,
                .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "NtSuspendProcess64.exe") & """"
            }

            pi.CreateNoWindow = True
            pi.WindowStyle = ProcessWindowStyle.Minimized

            SuspendProcess.StartInfo = pi

            Try
                SuspendProcess.Start()
                SuspendProcess.WaitForExit()
                PropRegionClass.Timer(RegionUUID) = Date.Now ' wait another interval
            Catch ex As Exception
                BreakPoint.Show(ex.Message)

            End Try
        End Using

    End Sub

    Private Sub Pause(RegionUUID As String)

        Dim RegionName = PropRegionClass.RegionName(RegionUUID)
        FreezeThaw(RegionUUID, "-pid " & PropRegionClass.ProcessID(RegionUUID))
        PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended
        Application.DoEvents()

    End Sub

#Region "BootUp"

    Public Function Boot(BootName As String) As Boolean
        ''' <summary>Starts Opensim for a given name</summary>
        ''' <param name="BootName">Name of region to start</param>
        ''' <returns>success = true</returns>
        Dim TheDate As Date = Date.Now()

        If FormSetup.Timer1.Enabled = False Then
            FormSetup.Timer1.Interval = 1000
            FormSetup.Timer1.Start() 'Timer starts functioning
        End If

        PropOpensimIsRunning() = True
        If PropAborting Then Return True

        Dim RegionUUID As String = PropRegionClass.FindRegionByName(BootName)
        Dim GroupName = PropRegionClass.GroupName(RegionUUID)

        If String.IsNullOrEmpty(RegionUUID) Then
            ErrorLog("Cannot find " & BootName & " to boot!")
            Return False
        End If

        PropRegionClass.CrashCounter(RegionUUID) = 0
        Dim GP = PropRegionClass.GroupPort(RegionUUID)
        Diagnostics.Debug.Print("Group port =" & CStr(GP))

        Dim isRegionRunning As Boolean = CheckPort("127.0.0.1", GP)
        If isRegionRunning Then
            If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Then
                Logger("Suspended, Resuming it", BootName, "Teleport")

                Dim PID As Integer = GetPIDofWindow(GroupName)
                If Not FormSetup.PropInstanceHandles.ContainsKey(PID) Then FormSetup.PropInstanceHandles.Add(PID, GroupName)
                For Each UUID As String In PropRegionClass.RegionUuidListByName(GroupName)
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Resume
                    PropRegionClass.ProcessID(UUID) = PID
                Next
                ShowDOSWindow(GetHwnd(PropRegionClass.GroupName(RegionUUID)), MaybeShowWindow())
                Logger("Info", "Region " & BootName & " skipped as it is Suspended, Resuming it instead", "Teleport")
                PropUpdateView = True ' make form refresh
                Return True
            Else    ' needs to be captured into the event handler
                TextPrint(BootName & " " & My.Resources.Running_word)
                Dim PID As Integer = GetPIDofWindow(GroupName)
                If Not FormSetup.PropInstanceHandles.ContainsKey(PID) Then FormSetup.PropInstanceHandles.Add(PID, GroupName)
                For Each UUID As String In PropRegionClass.RegionUuidListByName(GroupName)
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booted
                    PropRegionClass.Timer(UUID) = Date.Now
                    PropRegionClass.ProcessID(UUID) = PID
                Next
                ShowDOSWindow(GetHwnd(PropRegionClass.GroupName(RegionUUID)), MaybeHideWindow())
                PropUpdateView = True ' make form refresh
                Return True
            End If
        End If

        TextPrint(BootName & " " & Global.Outworldz.My.Resources.Starting_word)

        DoGloebits()

        PropRegionClass.CopyOpensimProto(RegionUUID)

        Dim ini = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Opensim\bin\OpenSim.exe.config")
        Settings.Grep(ini, Settings.LogLevel)

        BootProcess.EnableRaisingEvents = True
        BootProcess.StartInfo.UseShellExecute = True
        BootProcess.StartInfo.WorkingDirectory = Settings.OpensimBinPath()

        BootProcess.StartInfo.FileName = """" & Settings.OpensimBinPath() & "OpenSim.exe" & """"
        BootProcess.StartInfo.CreateNoWindow = False

        Select Case Settings.ConsoleShow
            Case "True"
                BootProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "False"
                BootProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Case "None"
                BootProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        End Select

        BootProcess.StartInfo.Arguments = " -inidirectory=" & """" & "./Regions/" & GroupName & """"

        Environment.SetEnvironmentVariable("OSIM_LOGPATH", Settings.OpensimBinPath() & "Regions\" & GroupName)

        FormSetup.SequentialPause()   ' wait for previous region to give us some CPU
        Logger("Booting", GroupName, "Teleport")

        Dim ok As Boolean = False
        Try
            ok = BootProcess.Start
            Application.DoEvents()
        Catch ex As Exception
            ErrorLog(ex.Message)
        End Try

        If ok Then
            Dim PID = WaitForPID(BootProcess)           ' check if it gave us a PID, if not, it failed.

            If PID > 0 Then
                SetWindowTextCall(BootProcess, GroupName)
                If Not FormSetup.PropInstanceHandles.ContainsKey(PID) Then
                    FormSetup.PropInstanceHandles.Add(PID, GroupName)
                End If
                ' Mark them before we boot as a crash will immediately trigger the event that it exited
                For Each UUID As String In PropRegionClass.RegionUuidListByName(GroupName)
                    PropRegionClass.Status(UUID) = RegionMaker.SIMSTATUSENUM.Booting
                    PropRegionClass.Timer(RegionUUID) = TheDate
                Next
            Else
                BreakPoint.Show("No PID for " & GroupName)
            End If

            For Each UUID As String In PropRegionClass.RegionUuidListByName(GroupName)
                PropRegionClass.ProcessID(UUID) = PID
            Next
            PropUpdateView = True ' make form refresh
            FormSetup.Buttons(FormSetup.StopButton)
            Return True
        End If
        PropUpdateView = True ' make form refresh
        Logger("Failed to boot ", BootName, "Teleport")
        TextPrint("Failed to boot region " & BootName)
        Return False

    End Function

    Public Sub ReBoot(RegionUUID As String)

        If PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Or
                PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped Then

            PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Resume
            Logger("State Changed to Resume", PropRegionClass.RegionName(RegionUUID), "Teleport")

        End If

    End Sub

#End Region

End Module
