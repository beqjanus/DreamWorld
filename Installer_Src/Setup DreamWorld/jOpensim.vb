#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Joomla

    Public Sub CheckForjOpensimUpdate()

        Dim installed As Boolean = IsjOpensimInstalled()

        Dim file = IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles\Apache\htdocs\jOpensim\" & jRev)

        If Not IO.File.Exists(file) And installed Then
            HelpManual("Joomla Update")
        End If

    End Sub

    Public Function IsjOpensimInstalled() As Boolean

        Return IO.File.Exists(IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs\JOpensim\administrator\index.php"))

    End Function

End Module
