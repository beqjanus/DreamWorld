Imports System.IO
Public Class CrashDetector
    Dim Testing As Boolean = True

    Public Sub Find()

        For Each RegionNum As Integer In Form1.RegionClass.RegionNumbers

            If Form1.RegionClass.IsBooted(RegionNum) Then
                Dim logline = Form1.RegionClass.LineCounter(RegionNum)
                Dim RegionName = Form1.RegionClass.RegionName(RegionNum)
                Dim ctr As Integer = 0
                Dim line As String = ""
                Dim fname = Form1.RegionClass.IniPath(RegionNum) & "Opensim.log"
                Dim fs = New FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using sr = New StreamReader(fs)

                    While sr.Peek() > -1
                        line = sr.ReadLine()
                        ctr = ctr + 1
                        If ctr > logline Then
                            If line.Contains("Timeout detected for thread " & """bulletunmanaged") Then
                                ' Restart 
                                Form1.Print("Restarting " & RegionName & " due to Bullet crash")
                                Form1.gRestartNow = True
                                Form1.SequentialPause(RegionNum)
                                Form1.ConsoleCommand(Form1.RegionClass.GroupName(RegionNum), "q{ENTER}" + vbCrLf)
                                Form1.RegionClass.Timer(RegionNum) = RegionMaker.REGION_TIMER.Stopped
                                Form1.RegionClass.Status(RegionNum) = RegionMaker.SIM_STATUS.RecyclingDown ' request a recycle.
                            End If
                        End If
                    End While
                    Form1.RegionClass.LineCounter(RegionNum) = ctr
                End Using


            End If
        Next

    End Sub
End Class
