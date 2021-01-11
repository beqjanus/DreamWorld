Module Runtimes

    Public Sub UpgradeMysql()

        If MySqlRev = Settings.MysqlRev Then Return

        TextPrint(My.Resources.Update_word & " MySQL")

        Using UpgradeProcess As New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo With {
              .Arguments = "",
              .FileName = """" & IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Mysql\bin\mysql_upgrade.exe") & """"
          }

            pi.WindowStyle = ProcessWindowStyle.Normal
            UpgradeProcess.StartInfo = pi

            Try
                UpgradeProcess.Start()
                UpgradeProcess.WaitForExit()
                Settings.MysqlRev = MySqlRev
                Settings.SaveSettings()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                TextPrint(My.Resources.Error_word)
            End Try

        End Using
    End Sub

    Public Sub UpgradeDotNet()

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
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
                TextPrint(My.Resources.Error_word)
            End Try

        End Using

    End Sub

End Module
