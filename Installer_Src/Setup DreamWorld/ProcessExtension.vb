#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Runtime.CompilerServices

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

            '        SuspendThread(pOpenThread)

        Next
    End Sub

End Module
