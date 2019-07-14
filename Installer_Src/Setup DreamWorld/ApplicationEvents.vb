

Namespace My

    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(
            ByVal sender As Object,
            ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs
         ) Handles Me.UnhandledException

            Form1.Log("FATAL", e.Exception.Message)

            Dim resp As MsgBoxResult = MsgBox("An application exception has occurred. Do you want to try to continue, or abort/exit?" + e.Exception.Message, vbOKCancel)
            If resp = vbOK Then
                e.ExitApplication = False
            Else
                System.Diagnostics.Process.Start(Form1.pMyFolder + "\baretail.exe", """" + Form1.pMyFolder + "\OutworldzFiles\Outworldz.log" + """")
                End
            End If
        End Sub
        Private Sub MyApplication_NetworkAvailabilityChanged(
                ByVal sender As Object,
                ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs
            ) Handles Me.UnhandledException

            Form1.Print("A network change occured. A restart may be necessary.")
        End Sub
    End Class

End Namespace