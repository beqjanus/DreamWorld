#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Threading

Imports System.Windows.Automation.AutomationElement

Module WindowHandlers

#Region "Enum"

    Public Enum SHOWWINDOWENUM As Integer
        SWHIDE = 0
        SWNORMAL = 1
        SWSHOWMINIMIZED = 2
        SWMAXIMIZE = 3
        SWSHOWNOACTIVATE = 4
        SWSHOW = 5
        SWMINIMIZE = 6
        SWSHOWMINNOACTIVE = 7
        SWSHOWNA = 8
        SWRESTORE = 9
        SWSHOWDEFAULT = 10
        SWFORCEMINIMIZE = 11

    End Enum

#End Region

    Public Sub ConsoleCommand(RegionUUID As String, command As String)

        ''' <summary>Sends keystrokes to Opensim. Always sends and enter button before to clear and use keys</summary>
        ''' <param name="ProcessID">PID of the DOS box</param>
        ''' <param name="command">String</param>
        ''' <returns></returns>
        If command Is Nothing Then Return
        If command.Length > 0 Then

            Dim PID As Integer
            If RegionUUID <> RobustName() And RegionUUID <> "Robust" Then

                PID = PropRegionClass.ProcessID(RegionUUID)

                If PID > 0 Then
                    Try
                        ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, MaybeShowWindow())
                    Catch ex As Exception
                    End Try
                End If
                DoType(RegionUUID, "{ENTER}")
                DoType(RegionUUID, command)

                Try
                    ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, MaybeHideWindow())
                Catch ex As Exception
                End Try
            Else ' Robust
                DoType("Robust", "{ENTER}")
                DoType("Robust", command)
            End If
        End If

    End Sub

    Public Function GetHwnd(Groupname As String) As IntPtr

        If Groupname = RobustName() Then
            For Each pList As Process In Process.GetProcessesByName("Robust")
                If pList.ProcessName = "Robust" Then
                    Try
                        pList.Refresh()
                        Return pList.MainWindowHandle
                    Catch
                    End Try
                End If
            Next
            Return IntPtr.Zero

        End If

        Dim AllProcesses = Process.GetProcessesByName("Opensim")
        For Each p As Process In AllProcesses
            If p.MainWindowTitle = Groupname Then
                p.Refresh()
                Try
                    Return p.MainWindowHandle
                Catch
                    Return IntPtr.Zero
                End Try
            End If
        Next

        Return IntPtr.Zero

    End Function

    Public Function GetPIDofWindow(GroupName As String) As Integer

        Dim path = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/Opensim/bin/Regions/" & GroupName & "/PID.pid")

        If IO.File.Exists(path) Then
            Using reader As StreamReader = System.IO.File.OpenText(path)
                While reader.Peek <> -1
                    Return CInt(reader.ReadLine())
                End While
            End Using
        End If

        For Each pList As Process In Process.GetProcessesByName("Opensim")
            Try
                If pList.MainWindowTitle = GroupName Then
                    Return pList.Id
                End If
            Catch
            End Try
        Next
        Return 0

    End Function

    Public Function MaybeHideWindow() As SHOWWINDOWENUM

        Dim w As SHOWWINDOWENUM
        Select Case Settings.ConsoleShow
            Case "True"
                w = SHOWWINDOWENUM.SWRESTORE
            Case "False"
                w = SHOWWINDOWENUM.SWMINIMIZE
            Case "None"
                w = SHOWWINDOWENUM.SWMINIMIZE
        End Select

        Return w

    End Function

    Public Function MaybeShowWindow() As SHOWWINDOWENUM

        Dim w As SHOWWINDOWENUM
        Select Case Settings.ConsoleShow
            Case "True"
                w = SHOWWINDOWENUM.SWRESTORE
            Case "False"
                w = SHOWWINDOWENUM.SWRESTORE
            Case "None"
                w = SHOWWINDOWENUM.SWMINIMIZE
        End Select

        Return w

    End Function

    Public Sub SendMsg(msg As String)

        Dim l As New List(Of String)
        If PropOpensimIsRunning() Then
            For Each RegionUUID As String In PropRegionClass.RegionUuids
                If Not l.Contains(PropRegionClass.GroupName(RegionUUID)) Then
                    l.Add(PropRegionClass.GroupName(RegionUUID))
                    If PropRegionClass.IsBooted(RegionUUID) Then
                        RPC_Region_Command(RegionUUID, "set log level " & msg)
                    End If
                End If

            Next
            ConsoleCommand(RobustName, "set log level " & msg)
        End If

        Settings.LogLevel = msg
        Settings.SaveSettings()

    End Sub

    Public Sub SendScriptCmd(cmd As String)
        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return
        End If
        Dim rname = ChooseRegion(True)
        Dim RegionUUID As String = PropRegionClass.FindRegionByName(rname)
        If RegionUUID.Length > 0 Then
            ConsoleCommand(RegionUUID, "change region " & """" & rname & """")
            ConsoleCommand(RegionUUID, cmd)
        End If

    End Sub

    Public Function SetWindowTextCall(myProcess As Process, windowName As String) As Boolean
        ''' <summary>
        ''' SetWindowTextCall is here to wrap the SetWindowtext API call. This call fails when there is no hwnd as Windows takes its sweet time to get that. Also, may fail to write the title. It has a
        ''' timer to make sure we do not get stuck
        ''' </summary>
        ''' <param name="hwnd">Handle to the window to change the text on</param>
        ''' <param name="windowName">the name of the Window</param>

        If myProcess Is Nothing Then
            ErrorLog("Process is nothing " & windowName)
            Return False
        End If


        Dim WindowCounter As Integer = 0
        Dim myhandle As IntPtr
        Try
            myProcess.Refresh()
            myhandle = myProcess.MainWindowHandle
            While myhandle = IntPtr.Zero
                WindowCounter += 1
                If WindowCounter > 600 Then '  60 seconds for process to start
                    ErrorLog("Cannot get MainWindowHandle for " & windowName)
                    Return False
                End If
                Sleep(100)
                Application.DoEvents()
                myProcess.Refresh()
                myhandle = myProcess.MainWindowHandle
            End While
        Catch ex As Exception
            ErrorLog(windowName & ":" & ex.Message)
            Return False
        End Try

        Dim status As Boolean = False
        WindowCounter = 0
        While Not status
            Try
                Thread.Sleep(100)
                Application.DoEvents()
                status = SetWindowText(myhandle, windowName)
            Catch ' can fail to be a valid window handle
                Return False
            End Try
            WindowCounter += 1
            If WindowCounter > 200 Then '  20 seconds
                status = True
            End If
        End While

        Return True

    End Function

    Public Function ShowDOSWindow(handle As IntPtr, command As SHOWWINDOWENUM) As Boolean

        If Settings.ConsoleShow = "None" And command <> SHOWWINDOWENUM.SWMINIMIZE Then
            Return True
        End If

        Dim ctr = 50    ' 5 seconds
        If handle <> IntPtr.Zero Then
            Dim HandleValid As Boolean = False

            While Not HandleValid And ctr > 0
                Try
                    HandleValid = ShowWindow(handle, command)
                    If HandleValid Then Return True
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
                ctr -= 1
                Sleep(100)
                Application.DoEvents()
            End While
        End If
        Return False

    End Function

    Public Function WaitForPID(myProcess As Process) As Integer

        If myProcess Is Nothing Then Return 0

        Dim TooMany As Integer = 0
        Dim p As Process = Nothing

        Do While TooMany <60
                             Try
                p= Process.GetProcessById(myProcess.Id)
            Catch ex As Exception
            End Try

            If p IsNot Nothing Then
                If p.ProcessName.Length > 0 Then
                    Return myProcess.Id
                End If
            End If

            Sleep(1000)
            TooMany += 1
        Loop

        Return 0

    End Function

    Public Sub Zap(processName As String)

        ''' <summary>Kill processes by name</summary>
        ''' <param name="processName"></param>
        ''' <returns></returns>

        ' Kill process by name
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName(processName)
            Log(My.Resources.Info_word, "Stopping process " & processName)
            Try
                P.Kill()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            Application.DoEvents()
        Next

    End Sub

    Private Sub DoType(RegionUUID As String, command As String)

        Dim PID As Integer
        If RegionUUID = "Robust" Then
            PID = PropRobustProcID
        Else
            PID = PropRegionClass.ProcessID(RegionUUID)
            Try
                ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, MaybeShowWindow())
            Catch ex As Exception
            End Try
        End If

        'plus sign(+), caret(^), percent sign (%), tilde (~), And parentheses ()
        command = command.Replace("+", "{+}")
        command = command.Replace("^", "{^}")
        command = command.Replace("%", "{%}")
        command = command.Replace("(", "{(}")
        command = command.Replace(")", "{)}")

        If PID = 0 Then
            ' BreakPoint.Show("PID = 0")
        Else
            Try
                AppActivate(PID)

                SendKeys.SendWait(command)
                SendKeys.SendWait("{ENTER}")
                ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, MaybeHideWindow())
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Function ToLowercaseKeys(Str As String) As String

        If My.Computer.Keyboard.CapsLock Then
            For Pos = 1 To Len(Str)
                Dim C As String = Mid(Str, Pos, 1)
                Mid(Str, Pos) = CStr(IIf(UCase(C) = C, LCase(C), UCase(C)))
            Next
        End If
        Return Str

    End Function

End Module
