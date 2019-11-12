Module RandomNumber

    Public Function Random() As String
        Dim value As Integer = CInt(Int((600000000 * Rnd()) + 1))
        Random = System.Convert.ToString(value, Globalization.CultureInfo.InvariantCulture)
    End Function

End Module
