Module AllBirds

    Public Sub SetBirdsOnOrOff()

        If Settings.BirdsModuleStartup Then
            Try
                If Not IO.File.Exists(Settings.OpensimBinPath & "OpenSimBirds.Module.dll") Then
                    CopyFileFast(Settings.OpensimBinPath & "OpenSimBirds.Module.bak", Settings.OpensimBinPath & "OpenSimBirds.Module.dll")
                End If
            Catch ex As Exception
                BreakPoint.Dump(ex)
            End Try
        Else
            Try
                If Not IO.File.Exists(Settings.OpensimBinPath & "OpenSimBirds.Module.bak") Then
                    CopyFileFast(Settings.OpensimBinPath & "OpenSimBirds.Module.dll", Settings.OpensimBinPath & "OpenSimBirds.Module.bak")
                End If
            Catch
            End Try

            DeleteFile(Settings.OpensimBinPath & "\OpenSimBirds.Module.dll")
        End If

    End Sub

End Module
