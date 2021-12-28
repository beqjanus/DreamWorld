#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Icecast

    Public WithEvents IcecastProcess As New Process()
    Private _IcecastProcID As Integer

    Public Property PropIcecastProcID As Integer
        Get
            Return _IcecastProcID
        End Get
        Set(value As Integer)
            _IcecastProcID = value
        End Set
    End Property

#Region "Icecast"

    Public Function StartIcecast() As Boolean

        If Not Settings.SCEnable Then
            TextPrint(Global.Outworldz.My.Resources.IceCast_disabled)
            IceCastIcon(False)
            Return True
        End If

        Try
            ' Check if DOS box exists, first, if so, its running.
            For Each p In Process.GetProcesses
                If p.ProcessName = "icecast" Then
                    PropIcecastProcID = p.Id

                    p.EnableRaisingEvents = True
                    AddHandler p.Exited, AddressOf FormSetup.IceCastExited

                    IceCastIcon(True)
                    Return True
                End If
            Next
        Catch
        End Try

        DoIceCast()

        DeleteFile(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Icecast\log\access.log"))
        'Launch .\bin\icecast.exe -c .\icecast_run.xml

        PropIcecastProcID = 0
        TextPrint(My.Resources.Icecast_starting)
        IcecastProcess.EnableRaisingEvents = True
        IcecastProcess.StartInfo.UseShellExecute = True
        IcecastProcess.StartInfo.FileName = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\icecast\bin\icecast.exe")
        IcecastProcess.StartInfo.CreateNoWindow = True
        IcecastProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        IcecastProcess.StartInfo.Arguments = "-c .\icecast_run.xml"
        IcecastProcess.StartInfo.WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\icecast")
        AddHandler IcecastProcess.Exited, AddressOf FormSetup.IceCastExited

        Try
            IcecastProcess.Start()
        Catch ex As Exception
            BreakPoint.Dump(ex)
            TextPrint(My.Resources.Icecast_failed & ":" & ex.Message)
            IceCastIcon(False)
            Return False
        End Try

        PropIcecastProcID = WaitForPID(IcecastProcess)
        If PropIcecastProcID = 0 Then
            BreakPoint.Print(My.Resources.Icecast_failed)
            TextPrint(My.Resources.Icecast_failed)
            IceCastIcon(False)
            Return False
        End If

        IceCastIcon(True)

        FormSetup.PropIceCastExited = False
        Return True

    End Function

#End Region

    Public Function CheckIcecast() As Boolean
        ''' <summary>Check is Icecast port 8081 is up</summary>
        ''' <returns>boolean</returns>
        Using client As New Net.WebClient ' download client for web pages
            Dim Up As String
            Try
                Up = client.DownloadString("http://" & Settings.PublicIP & ":" & Settings.SCPortBase & "/?_Opensim=" & RandomNumber.Random())
            Catch ex As Exception
                BreakPoint.Dump(ex)
                IceCastIcon(False)
                Return False
            End Try

            If Up.Length = 0 And PropOpensimIsRunning() Then
                Return False
            End If
        End Using
        Return True

    End Function

    Public Sub IceCastIcon(Running As Boolean)

        If Not Running Then
            FormSetup.RestartIcecastIcon.Image = Global.Outworldz.My.Resources.nav_plain_red
        Else
            FormSetup.RestartIcecastIcon.Image = Global.Outworldz.My.Resources.check2
        End If
        Application.DoEvents()

    End Sub

    Public Sub StopIcecast()

        Zap("icecast")
        IceCastIcon(False)

    End Sub

End Module
