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

    ''' <summary>
    ''' Resumes a  suspended region
    ''' </summary>
    ''' <param name="PID">Process ID</param>
    ''' <returns>false if it succeeds</returns>
    <Extension()>
    Public Function RestoreRegion(PID As Integer) As Boolean

        Dim result As Boolean = True    ' assume success
        Dim process As Process = ProcessIdDict(PID)
        For Each thread As ProcessThread In process.Threads
            Dim pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, False, CUInt(thread.Id))
            If pOpenThread = IntPtr.Zero Then
                Continue For
            End If
            Dim suspendCount As Integer
            suspendCount = ResumeThread(pOpenThread)
            While (suspendCount > 0)
                suspendCount = ResumeThread(pOpenThread)
                result = False
            End While
        Next
        Return result
    End Function

    <Extension()>
    Public Function SuspendRegion(PID As Integer) As Boolean

        Dim process As Process = ProcessIdDict(PID)
        For Each thread As ProcessThread In process.Threads
            Dim pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, False, CUInt(thread.Id))

            If pOpenThread = IntPtr.Zero Then
                Continue For
            End If

            NtSuspendProcess(pOpenThread)
        Next
        Return False
    End Function

End Module
