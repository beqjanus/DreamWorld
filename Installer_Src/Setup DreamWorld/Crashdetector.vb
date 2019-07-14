Imports System.IO

Public Class CrashDetector

    Public Sub Find()

        If Not Form1.pMySetting.RestartonPhysics() Then Return

        Dim Used As New List(Of String)
        For Each RegionNum As Integer In Form1.pRegionClass.RegionNumbers
            If Form1.pRegionClass.IsBooted(RegionNum) Then
                Dim Group = Form1.pRegionClass.GroupName(RegionNum)
                If Not Used.Contains(Group) Then
                    Used.Add(Group)
                    Dim logline = Form1.pRegionClass.LineCounter(RegionNum)
                    Dim RegionName = Form1.pRegionClass.RegionName(RegionNum)
                    Dim ctr As Integer = 0
                    Dim line As String = ""
                    Dim fname = Form1.pRegionClass.IniPath(RegionNum) & "Opensim.log"

                    If File.Exists(fname) Then
                        Dim fs = New FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                        Using sr = New StreamReader(fs)

                            While sr.Peek() > -1
                                line = sr.ReadLine()
                                ctr = ctr + 1
                                If ctr > logline Then
                                    If line.Contains("Timeout detected for thread " & """bulletunmanaged") Then
                                        ' Restart
                                        Form1.Print("Restarting " & RegionName & " due to Bullet crash")
                                        Form1.pRestartNow = True
                                        Form1.SequentialPause()
                                        Form1.ConsoleCommand(Form1.pRegionClass.GroupName(RegionNum), "q{ENTER}" + vbCrLf)
                                        Form1.pRegionClass.Timer(RegionNum) = RegionMaker.REGIONTIMER.Stopped
                                        Form1.pRegionClass.Status(RegionNum) = RegionMaker.SIMSTATUSENUM.RecyclingDown ' request a recycle.
                                    End If
                                End If
                            End While
                            Form1.pRegionClass.LineCounter(RegionNum) = ctr
                        End Using
                    End If
                End If
            End If
        Next

    End Sub

End Class