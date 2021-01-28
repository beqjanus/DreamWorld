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

Module Runtimes

    Public Sub UpgradeDotNet()

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
                Settings.DotnetUpgraded() = True
                Settings.SaveSettings()
                End
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
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

End Module
