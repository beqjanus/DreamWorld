Module Joomla

    Public Function IsjOpensimInstalled() As Boolean

        Return IO.File.Exists(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\JOpensim\administrator\index.php"))

    End Function

    Public Sub CheckForjOpensimUpdate()

        Dim installed As Boolean = IsjOpensimInstalled()

        Dim file = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\jOpensim\" & FormSetup.JRev)

        If Not IO.File.Exists(file) And installed Then
            HelpManual("Joomla Update")
        End If

    End Sub

End Module
