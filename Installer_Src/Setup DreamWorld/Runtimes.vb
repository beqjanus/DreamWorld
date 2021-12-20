#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Runtimes

    Public Sub UpgradeDotNet()

        ' Detect Operating System  Updates
        Using objOS = New Management.ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")

            For Each objMgmt In objOS.Get()
                Console.WriteLine("{0}: {1}", objMgmt.Properties("Caption").Name, objMgmt.Properties("Caption").Value)
                Dim OS = objMgmt.Properties("Caption").Value.ToString
                If OS <> Settings.OperatingSystem And
                    Settings.OperatingSystem.Length > 0 Then
                    Settings.DotnetUpgraded() = False
                End If
                Settings.OperatingSystem = OS
            Next

        End Using

        If Settings.DotnetUpgraded() Then Return

        TextPrint(My.Resources.Update_word & " Dot Net")
        Using UpgradeProcess As New Process()

            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
              .Arguments = "",
              .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "MSFT_Runtimes\VisualCppRedist_AIO_x86_x64.exe") & """"
            }

            pi.WindowStyle = ProcessWindowStyle.Normal
            UpgradeProcess.StartInfo = pi

            Try
                UpgradeProcess.Start()
                UpgradeProcess.WaitForExit()
                Settings.DotnetUpgraded() = True
                Settings.SaveSettings()
            Catch ex As Exception
                BreakPoint.Show(ex)
                TextPrint(My.Resources.Error_word)
            End Try

        End Using
        TextPrint(My.Resources.Update_word & " Dot Net 4.8")
        Using UpgradeProcess As New Process()

            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
              .Arguments = "",
              .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "MSFT_Runtimes\ndp48-web.exe") & """"
            }

            pi.WindowStyle = ProcessWindowStyle.Normal
            UpgradeProcess.StartInfo = pi

            Try
                UpgradeProcess.Start()
                UpgradeProcess.WaitForExit()
                Settings.DotnetUpgraded() = True
                Settings.SaveSettings()
            Catch ex As Exception
                BreakPoint.Show(ex)
                TextPrint(My.Resources.Error_word)
            End Try

        End Using

    End Sub

    Public Sub UpgradeMysql()

        If MySqlRev = Settings.MysqlRev Then Return

        TextPrint(My.Resources.Update_word & " MySQL")

        Using UpgradeProcess As New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
                  .Arguments = "",
                  .CreateNoWindow = True,
                  .WindowStyle = ProcessWindowStyle.Hidden,
                  .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\MySQL\bin\mysql_upgrade.exe") & """"
            }

            UpgradeProcess.StartInfo = pi

            Try
                UpgradeProcess.Start()
                UpgradeProcess.WaitForExit()
                Settings.MysqlRev = MySqlRev
                Settings.SaveSettings()
            Catch ex As Exception
                BreakPoint.Show(ex)
                TextPrint(My.Resources.Error_word)
            End Try

        End Using
    End Sub

End Module
