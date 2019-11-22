Module RandomNumber

    Public Function Between(ByVal MaxNumber As Integer, Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        Dim r = New Random(System.DateTime.Now.Millisecond)

        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)

    End Function

    Public Function Random() As String
        Dim value As Integer = CInt(Int((600000000 * Rnd(System.DateTime.Now.Millisecond)) + 1))
        Random = System.Convert.ToString(value, Globalization.CultureInfo.InvariantCulture)
    End Function

End Module