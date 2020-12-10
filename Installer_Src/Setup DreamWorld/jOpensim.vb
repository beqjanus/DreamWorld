Module Joomla

    Public Function IsjOpensimInstalled() As Boolean

        Dim count As Integer
        Try
            Dim folders() = IO.Directory.GetFiles(IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\JOpensim"))
            count = folders.Length
        Catch
        End Try

        Return count > 1

    End Function

    Public Sub CheckForjOpensimUpdate()

        Dim installed As Boolean = IsjOpensimInstalled()

        Dim file = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\jOpensim\" & FormSetup.JRev)

        If Not IO.File.Exists(file) And installed Then
            HelpManual("Joomla Update")
        End If

    End Sub

End Module
