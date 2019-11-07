Module RandomNumber

    Public Function Random() As String
        Dim value As Integer = CInt(Int((600000000 * Rnd()) + 1))
        Random = System.Convert.ToString(value, Form1.Invarient)
    End Function

End Module
