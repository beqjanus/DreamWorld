Module Help

    Public Sub HelpManual(page As String)

        FormHelp.Activate()
        FormHelp.Visible = True
        FormHelp.Init(page)
        FormHelp.Select()
        FormHelp.BringToFront()

    End Sub

    Public Sub HelpOnce(Webpage As String)

        Dim NewScreenPosition1 = New ScreenPos(Webpage)
        If Not NewScreenPosition1.Exists() Then
            Dim FormHelp As New FormHelp

            FormHelp.Activate()
            FormHelp.Visible = True
            FormHelp.Init(Webpage)
            Try
                FormHelp.Select()
                FormHelp.BringToFront()
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End If

    End Sub

End Module
