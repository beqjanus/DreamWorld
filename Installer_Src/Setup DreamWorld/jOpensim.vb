Module Joomla

    Public Function IsjOpensimInstalled() As Boolean

        Dim x = IO.File.Exists(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\JOpensim\administrator\index.php"))
        Return x

    End Function

    Public Sub CheckForjOpensimUpdate()

        Dim installed As Boolean = IsjOpensimInstalled()

        Dim file = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\jOpensim\" & FormSetup.JRev)

        If Not IO.File.Exists(file) And installed Then
            HelpManual("Joomla Update")
        End If

    End Sub

End Module
