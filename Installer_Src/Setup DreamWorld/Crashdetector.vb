#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

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

Imports System.IO

Module CrashDetector

    Public Sub Find()

        If Not Form1.Settings.RestartonPhysics() Then Return

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
                                        Form1.Print(My.Resources.Restarting & " " & RegionName)
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

End Module