Public Class FormCopyright

    Public Sub N() Handles MyBase.Load

        Label1.Text = ("Version " & PropMyVersion)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

        Dim webAddress As String = "https://opensource.org/licenses/AGPL-3.0"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.DUmp(ex)
        End Try
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

        Dim webAddress As String = "https://outworldz.com"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.DUmp(ex)
        End Try

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

        Throw New System.Exception("A Test Exception has occurred.")

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

        Dim webAddress As String = "https://outworldz.com"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.DUmp(ex)
        End Try

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

        Dim webAddress As String = "https://opensource.org/licenses/AGPL-3.0"
        Try
            Process.Start(webAddress)
        Catch ex As Exception
            BreakPoint.DUmp(ex)
        End Try
    End Sub

End Class