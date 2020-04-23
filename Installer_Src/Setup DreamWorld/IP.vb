Public Module IPCheck

    Public Function IsPrivateIP(CheckIP As String) As Boolean

        ''' <summary>
        ''' Checks to see if an IP address is a local IP address.
        ''' </summary>
        ''' <param name="CheckIP">The IP address to check, or localhost.</param>
        ''' <returns>Boolean</returns>
        ''' <remarks></remarks>
        If CheckIP Is Nothing Then Return False
        If CheckIP = "localhost" Then Return True

        Dim Quad1, Quad2 As Integer

        Try
            Quad1 = CInt(CheckIP.Substring(0, CheckIP.IndexOf(".", StringComparison.InvariantCulture)))
            Quad2 = CInt(CheckIP.Substring(CheckIP.IndexOf(".", StringComparison.InvariantCulture) + 1).Substring(0, CheckIP.IndexOf(".", StringComparison.InvariantCulture)))
#Disable Warning CA1031
        Catch ex As Exception
#Enable Warning CA1031
        End Try

        Select Case Quad1
            Case 10
                Return True
            Case 172
                If Quad2 >= 16 And Quad2 <= 31 Then Return True
            Case 192
                If Quad2 = 168 Then Return True
        End Select
        Return False
    End Function

End Module
