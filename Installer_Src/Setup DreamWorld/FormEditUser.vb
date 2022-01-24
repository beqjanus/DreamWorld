Public Class FormEditUser


    Private _Username As String
    Private _UUID As String

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load


    End Sub
    Public Sub init(Username As String, UUID As String)

        _Username = Username
        _UUID = UUID

    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioNologin.CheckedChanged

        Dim stm = $"Update useraccounts set level='-1' where uuid = '{_UUID}'"
        QueryString(stm)

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioLogin.CheckedChanged

        Dim stm = $"Update useraccounts set level='0' where uuid = '{_UUID}'"
        QueryString(stm)

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioDiva.CheckedChanged

        Dim stm = $"Update useraccounts set level='100' where uuid = '{_UUID}'"
        QueryString(stm)

    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioGid.CheckedChanged

        Dim stm = $"Update useraccounts set level='200' where uuid = '{_UUID}'"
        QueryString(stm)

    End Sub
End Class