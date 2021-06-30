Module Quickedit

    Public Sub SetQuickEditOff()

        Dim pi As ProcessStartInfo = New ProcessStartInfo With {
            .Arguments = "Set-ItemProperty -path HKCU:\Console -name QuickEdit -value 0",
            .FileName = "powershell.exe",
            .WindowStyle = ProcessWindowStyle.Hidden,
            .CreateNoWindow = True,
            .Verb = "runas"
        }
        Using PowerShell As Process = New Process With {
             .StartInfo = pi
            }

            Try
                PowerShell.Start()
            Catch ex As Exception
                ErrorLog("Cannot set Quick edit off")
            End Try
        End Using

    End Sub

End Module
