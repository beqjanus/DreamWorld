Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Module ProcessExtension

    Public Enum ThreadAccess As Integer

        TERMINATE = (&H1)
        SUSPEND_RESUME = (&H2)
        GET_CONTEXT = (&H8)
        SET_CONTEXT = (&H10)
        SET_INFORMATION = (&H20)
        QUERY_INFORMATION = (&H40)
        SET_THREAD_TOKEN = (&H80)
        IMPERSONATE = (&H100)
        DIRECT_IMPERSONATION = (&H200)

    End Enum

    <Extension()>
    Sub [Resume](ByVal process As Process)
        For Each thread As ProcessThread In process.Threads
            Dim pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, False, CUInt(thread.Id))

            If pOpenThread = IntPtr.Zero Then
                Continue For
            End If
            Dim suspendCount As Integer
            suspendCount = ResumeThread(pOpenThread)
            While (suspendCount > 0)
                suspendCount = ResumeThread(pOpenThread)
            End While

        Next
    End Sub

    <Extension()>
    Sub Suspend(ByVal process As Process)

        For Each thread As ProcessThread In process.Threads
            Dim pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, False, CUInt(thread.Id))

            If pOpenThread = IntPtr.Zero Then
                Continue For
            End If

            SuspendThread(pOpenThread)

        Next
    End Sub

    <Extension()>
    Sub SuspendP(ByVal processId As Integer)

    End Sub

    <DllImport("ntdll.dll", SetLastError:=False)>
    Private Function NtSuspendProcess(ByVal ProcessHandle As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Private Function OpenThread(ByVal dwDesiredAccess As ThreadAccess, ByVal bInheritHandle As Boolean, ByVal dwThreadId As UInteger) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Private Function ResumeThread(ByVal hThread As IntPtr) As Integer
    End Function

    Private Function SuspendThread(ByVal hThread As IntPtr) As UInteger
#Disable Warning BC42353 ' Function doesn't return a value on all code paths
    End Function

#Enable Warning BC42353 ' Function doesn't return a value on all code paths

End Module
