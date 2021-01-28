#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

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

#Disable Warning IDE0060 ' Remove unused parameter

    <Extension()>
    Sub SuspendP(ByVal processId As Integer)
#Enable Warning IDE0060 ' Remove unused parameter

    End Sub

    'Private Sub SuspendThread(ByVal hThread As IntPtr)
    ' removed suspend code

    'End Sub

End Module
