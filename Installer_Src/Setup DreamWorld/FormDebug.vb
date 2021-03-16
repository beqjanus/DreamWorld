Public Class FormDebug

    Private _command As String
    Private _value As String

    Public Property Command As String
        Get
            Return _command
        End Get
        Set(value As String)
            _command = value
        End Set
    End Property

    Public Property Value As String
        Get
            Return _value
        End Get
        Set(value As String)
            _value = value
        End Set
    End Property

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        Command = CStr(ListBox1.SelectedItem)
        Value = CStr(RadioTrue.Checked)

    End Sub

End Class