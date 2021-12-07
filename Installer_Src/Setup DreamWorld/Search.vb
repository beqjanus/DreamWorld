Public Class SearchHelp

    Public Sub New()
        Levenshtein("", "")
    End Sub

    Private Shared Function Levenshtein(a As String, b As String) As Integer

        Dim i As Integer
        Dim j As Integer
        Dim cost As Integer
        Dim d(,) As Integer
        Dim min1 As Integer
        Dim min2 As Integer
        Dim min3 As Integer

        If Len(a) = 0 Then
            Return Len(b)
        End If

        If Len(b) = 0 Then
            Return Len(a)
        End If

        ReDim d(Len(a), Len(b))

        For i = 0 To Len(a)
            d(i, 0) = i
        Next

        For j = 0 To Len(b)
            d(0, j) = j
        Next

        For i = 1 To Len(a)
            For j = 1 To Len(b)
                If Mid(a, i, 1) = Mid(b, j, 1) Then
                    cost = 0
                Else
                    cost = 1
                End If

                ' Since Min() function is not a part of VBA, we'll "emulate" it below
                min1 = (d(i - 1, j) + 1)
                min2 = (d(i, j - 1) + 1)
                min3 = (d(i - 1, j - 1) + cost)

                If min1 <= min2 And min1 <= min3 Then
                    d(i, j) = min1
                ElseIf min2 <= min1 And min2 <= min3 Then
                    d(i, j) = min2
                Else
                    d(i, j) = min3
                End If

                ' In Excel we can use Min() function that is included
                ' as a method of WorksheetFunction object
                'd(i, j) = Application.WorksheetFunction.Min(min1, min2, min3)
            Next
        Next

        Return d(Len(a), Len(b))

    End Function

End Class
