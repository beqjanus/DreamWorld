#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Module RandomNumber

    Public Function Between(Min As Integer, Max As Integer) As Integer

        If Max < Min Then
            BreakPoint.Print("Error")
            Dim tmp = Max
            Max = Min
            Min = tmp
        End If
        ' by making Generator static, we preserve the same instance '
        ' (i.e., do not create new instances with the same seed over and over) '
        ' between calls '
        Static Generator As New System.Random()
        Dim r = Generator.Next(Min, Max + 1)

        Return CInt(r)

    End Function

    Public Function Random() As String
        Dim value As Integer = CInt(Int((600000000 * Rnd(System.DateTime.Now.Millisecond)) + 1))
        Random = System.Convert.ToString(value, Globalization.CultureInfo.InvariantCulture)
    End Function

End Module
