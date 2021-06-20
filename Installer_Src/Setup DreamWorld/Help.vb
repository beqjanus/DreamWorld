#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module Help

    Public Sub HelpManual(page As String)

        FormHelp.Activate()
        FormHelp.Visible = True
        FormHelp.Init(page)
        FormHelp.Select()
        FormHelp.BringToFront()

    End Sub

    Public Sub HelpOnce(Webpage As String)

        Using NewScreenPosition1 = New ScreenPos(Webpage)
            If Not NewScreenPosition1.Exists() Then
                Dim FormHelp As New FormHelp
                FormHelp.Init(Webpage)
                FormHelp.Activate()
                Try
                    FormHelp.Select()
                    FormHelp.BringToFront()
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try
            End If

        End Using

    End Sub

End Module
