Imports System.Runtime.InteropServices

Friend Module NativeMethods

    Public Declare Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Form1.SHOWWINDOWENUM) As Boolean

#Disable Warning CA2101 ' Specify marshaling for P/Invoke string arguments

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Function SetWindowText(ByVal hwnd As IntPtr, ByVal windowName As String) As Boolean
    End Function

#Enable Warning CA2101 ' Specify marshaling for P/Invoke string arguments

End Module
