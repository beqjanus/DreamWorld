#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports System.Threading
Imports System.Runtime.InteropServices

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

    Public Function CachedProcess(PID As Integer) As Process

        If Not ProcessIdDict.ContainsKey(PID) Then
            Try
                Dim Pr = Process.GetProcessById(PID)
                If Pr IsNot Nothing Then
                    ProcessIdDict.Add(PID, Pr)
                    Return Pr
                End If
            Catch ex As Exception
            End Try
        End If
        Return ProcessIdDict(PID)

    End Function

    Public Function ConsoleCommand(RegionUUID As String, command As String, Optional noChange As Boolean = False) As Boolean

        ''' <summary>Sends keystrokes to Opensim. Always sends and enter button before to clear and use keys</summary>
        ''' <param name="ProcessID">PID of the DOS box</param>
        ''' <param name="command">String</param>
        ''' <returns></returns>
        If command Is Nothing Then Return True
        If command.Length > 0 Then
            command = ToLowercaseKeys(command)
            Dim PID As Integer
            If RegionUUID <> RobustName() Then

                PID = ProcessID(RegionUUID)

                If PID > 0 Then
                    Try
                        If Not noChange Then ShowDOSWindow(Process.GetProcessById(PID).MainWindowHandle, MaybeShowWindow())
                    Catch ex As Exception
                        'BreakPoint.Dump(ex)
                        Return True
                    End Try
                Else
                    'BreakPoint.Print("No PID")
                    Return True
                End If
                DoType(RegionUUID, "{ENTER}" & command & "{ENTER}")
                Sleep(1000)
                Try
                    If Not noChange Then ShowDOSWindow(CachedProcess(PID).MainWindowHandle, MaybeHideWindow())
                Catch ex As Exception
                End Try
            Else ' Robust
                Try
                    'Sleep(1000)
                    If Not noChange Then ShowDOSWindow(Process.GetProcessById(PropRobustProcID).MainWindowHandle, MaybeShowWindow())
                Catch ex As Exception
                    'BreakPoint.Dump(ex)
                    Return True
                End Try
                DoType("Robust", "{ENTER}" & command & "{ENTER}")
            End If
        End If
        Return False

    End Function

    Public Sub DoType(RegionUUID As String, command As String)

        Dim PID As Integer
        If RegionUUID = "Robust" Then
            If IsRobustRunning() Then
                PID = GetPIDofRobust()
            End If
        Else
            PID = ProcessID(RegionUUID)
        End If

        'plus sign(+), caret(^), percent sign (%), tilde (~), And parentheses ()
        command = command.Replace("+", "{+}")
        command = command.Replace("^", "{^}")
        command = command.Replace("%", "{%}")
        command = command.Replace("(", "{(}")
        command = command.Replace(")", "{)}")

        If PID <> 0 Then
            Try
                AppActivate(PID)
                Sleep(100)

                SendKeys.SendWait(command)
                SendKeys.SendWait("{ENTER}")
            Catch ex As Exception
            End Try
        ElseIf RegionUUID = "Robust" And command = "{ENTER}q{ENTER}" Then
            Zap("Robust")
        Else
            BreakPoint.Print("No PID for this window")
        End If

    End Sub

    ''' <summary>
    ''' Returns a handle to the window, by process list, or by reading the PID file.
    ''' </summary>
    ''' <param name="Groupname">Name of the DOS box</param>
    ''' <returns>Handle to a window to Intptr.zero</returns>

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

        ' file may be gone or locked so as a last resort, so look at window name which is somewhat unreliable
        Dim AllProcesses = Process.GetProcessesByName("Opensim")
        For Each p As Process In AllProcesses
            If p.MainWindowTitle = Groupname Then
                Return p.MainWindowHandle
            End If
        Next

        Return IntPtr.Zero

    End Function

    Public Function GetPIDofRobust() As Integer

        For Each pList As Process In Process.GetProcessesByName("Robust")
            Try
                If pList.MainWindowTitle = RobustName() Then
                    Return pList.Id
                End If
            Catch
            End Try
        Next
        Return 0

    End Function

    Public Function GetPIDofWindow(GroupName As String) As Integer

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
            For Each RegionUUID As String In RegionUuids()

                If Not l.Contains(Group_Name(RegionUUID)) Then
                    l.Add(Group_Name(RegionUUID))
                    If IsBooted(RegionUUID) Then
                        RPC_Region_Command(RegionUUID, "set log level " & msg)
                    End If
                End If

            Next
            ConsoleCommand(RobustName, "set log level " & msg)
        End If

    End Sub

    Public Sub SendScriptCmd(cmd As String)
        If Not PropOpensimIsRunning() Then
            TextPrint(My.Resources.Not_Running)
            Return
        End If
        Dim rname = ChooseRegion(False)
        Dim RegionUUID As String = FindRegionByName(rname)
        If RegionUUID.Length > 0 Then
            ConsoleCommand(RegionUUID, "change region " & """" & rname & """")
            ConsoleCommand(RegionUUID, cmd)
        End If

    End Sub

    ''' <summary>
    ''' Sets the window title text
    ''' </summary>
    ''' <param name="myProcess">PID</param>
    ''' <param name="windowName">WindowName</param>
    ''' <returns>True if window is set</returns>
    Public Function SetWindowTextCall(myProcess As Process, windowName As String) As Boolean
        ''' <summary>
        ''' SetWindowTextCall is here to wrap the SetWindowtext API call. This call fails when there is no hwnd as Windows takes its sweet time to get that. Also, may fail to write the title. It has a
        ''' timer to make sure we do not get stuck
        ''' </summary>
        ''' <param name="hwnd">Handle to the window to change the text on</param>
        ''' <param name="windowName">the name of the Window</param>

        If myProcess Is Nothing Then
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
                Thread.Sleep(100)
                myProcess.Refresh()
                myhandle = myProcess.MainWindowHandle
            End While
        Catch ex As Exception
            ErrorLog(windowName & ":" & ex.Message)
            Return False
        End Try

        Dim status As Boolean = False
        WindowCounter = 0
        While True
            Try
                status = SetWindowText(myhandle, windowName)
                If Not status Then
                    Dim err = GetLastError()
                Else
                    If myProcess Is Nothing Then Return False
                    myProcess.Refresh()
                    Thread.Sleep(10)
                    If myProcess.MainWindowTitle = windowName Then
                        Return True
                    Else
                        'Dim err = GetLastError()
                        status = False
                    End If
                End If
            Catch ex As Exception ' can fail to be a valid window handle
                BreakPoint.Dump(ex)
            End Try

            WindowCounter += 1
            If WindowCounter > 600 Then '  1 minute
                ErrorLog(windowName & " timeout setting title")
                Return False
            End If
            Thread.Sleep(100)
            myProcess.Refresh()

        End While

        Return False

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
                    BreakPoint.Dump(ex)
                End Try
                ctr -= 1
                Sleep(100)
                Application.DoEvents()
            End While
        End If
        Return False

    End Function

    Public Function WaitForPID(myProcess As Process) As Integer

        If myProcess Is Nothing Then
            BreakPoint.Print("No Process!")
            Return 0
        End If

        Dim TooMany As Integer = 0
        Dim p As Process = Nothing
        ' 2 minutes for old hardware and it to build DB
        Do While TooMany < 60
            Try
                p = Process.GetProcessById(myProcess.Id)
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
        BreakPoint.Print("No Pid")
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
                BreakPoint.Dump(ex)
            End Try
            Application.DoEvents()
        Next

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
