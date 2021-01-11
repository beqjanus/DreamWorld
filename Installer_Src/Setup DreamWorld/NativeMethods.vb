Imports System.Runtime.InteropServices

Friend Module NativeMethods

    Public Declare Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As SHOWWINDOWENUM) As Boolean

#Disable Warning CA2101 ' Specify marshaling for P/Invoke string arguments

    <DllImport("ntdll.dll", SetLastError:=False)>
    Public Function NtSuspendProcess(ByVal ProcessHandle As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Public Function OpenThread(ByVal dwDesiredAccess As ThreadAccess, ByVal bInheritHandle As Boolean, ByVal dwThreadId As UInteger) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Public Function ResumeThread(ByVal hThread As IntPtr) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Public Function SetWindowText(ByVal hwnd As IntPtr, ByVal windowName As String) As Boolean
    End Function

    Public Declare Function SetWindowPos Lib "user32" _
            (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer,
            ByVal x As Integer, ByVal y As Integer, ByVal cX As Integer,
            ByVal cY As Integer, ByVal wFlags_ As Integer) As Long

#Enable Warning CA2101 ' Specify marshaling for P/Invoke string arguments
End Module
