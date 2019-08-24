Imports System.IO

Public Class CrashDetector

    Public Sub Find()

        If Not Form1.PropMySetting.RestartonPhysics() Then Return

        Dim Used As New List(Of String)
        For Each RegionNum As Integer In Form1.PropRegionClass.RegionNumbers
            If Form1.PropRegionClass.IsBooted(RegionNum) Then
                Dim Group = Form1.PropRegionClass.GroupName(RegionNum)
                If Not Used.Contains(Group) Then
                    Used.Add(Group)
                    Dim logline = Form1.PropRegionClass.LineCounter(RegionNum)
                    Dim RegionName = Form1.PropRegionClass.RegionName(RegionNum)
                    Dim ctr As Integer = 0
                    Dim line As String = ""
                    Dim fname = Form1.PropRegionClass.IniPath(RegionNum) & "Opensim.log"

                    If File.Exists(fname) Then
                        Dim fs = New FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                        Using sr = New StreamReader(fs)

                            While sr.Peek() > -1
                                line = sr.ReadLine()
                                ctr += 1
                                If ctr > logline Then
                                    If line.Contains("Timeout detected for thread " & """bulletunmanaged") Then
                                        ' Restart
                                        Form1.Print("Restarting " & RegionName & " due to Bullet crash")
                                        Form1.PropRestartNow = True
                                        Form1.SequentialPause()
                                        Form1.ConsoleCommand(Form1.PropRegionClass.GroupName(RegionNum), "q{ENTER}" + vbCrLf)
                                        Form1.PropRegionClass.Timer(RegionNum) = RegionMaker.REGIONTIMER.Stopped
                                        Form1.PropRegionClass.Status(RegionNum) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
                                    End If
                                End If
                            End While
                            Form1.PropRegionClass.LineCounter(RegionNum) = ctr
                        End Using
                    End If
                End If
            End If
        Next

    End Sub

End Class