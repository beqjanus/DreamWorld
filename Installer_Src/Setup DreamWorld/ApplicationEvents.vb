#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

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

Namespace My

    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed. This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication



#Region "Private Methods"

        Private Sub MyApplication_UnhandledException(
                    ByVal sender As Object,
            ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs
         ) Handles Me.UnhandledException

            Form1.Log("FATAL", e.Exception.Message)
            Form1.Log("FATAL:", DisplayObjectInfo(sender))

            Dim resp As MsgBoxResult = MsgBox(My.Resources.Appexception + e.Exception.Message, vbOKCancel)
            If resp = vbOK Then
                e.ExitApplication = False
            Else
                Try
                    System.Diagnostics.Process.Start(Form1.PropMyFolder + "\baretail.exe", """" + Form1.PropMyFolder + "\OutworldzFiles\Outworldz.log" + """")
                Catch ex As ObjectDisposedException
                Catch ex As InvalidOperationException
                Catch ex As System.ComponentModel.Win32Exception
                End Try
                e.ExitApplication = False
            End If
        End Sub

#End Region

    End Class

End Namespace
