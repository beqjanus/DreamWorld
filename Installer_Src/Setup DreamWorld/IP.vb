#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Public Module IPCheck

    Public Function CreateMD5(ByVal input As String) As String
#Disable Warning CA5351 ' Do Not Use Broken Cryptographic Algorithms
        Using md5 As System.Security.Cryptography.MD5 = System.Security.Cryptography.MD5.Create()
#Enable Warning CA5351 ' Do Not Use Broken Cryptographic Algorithms
            Dim inputBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(input)
            Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)
            Dim sb As String = ""

            For i As Integer = 0 To hashBytes.Length - 1
                sb += CStr(hashBytes(i).ToString("X2", Globalization.CultureInfo.CurrentCulture))
            Next

            Return sb
        End Using
    End Function

    Public Function GetMacByIp(IPIN As String) As String

        Dim IP As Net.IPAddress = Net.IPAddress.Parse(IPIN)

        Dim macAddr As Byte() = New Byte(5) {}
        Dim macAddrLen As UInteger = CUInt(macAddr.Length)

#Disable Warning BC40000 ' Type or member is obsolete
#Disable Warning BC42016 ' Implicit conversion
        Dim retval As Integer = SendARP(IP.Address, 0, macAddr, macAddrLen)
#Enable Warning BC42016 ' Implicit conversion
#Enable Warning BC40000 ' Type or member is obsolete
        If retval <> 0 Then
            Return ""
        End If
        Dim mac As String = BitConverter.ToString(macAddr, 0, 6)
        Return CreateMD5(mac)

    End Function

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
            Quad1 = CInt("0" & CheckIP.Substring(0, CheckIP.IndexOf(".", StringComparison.InvariantCulture)))
            Quad2 = CInt("0" & CheckIP.Substring(CheckIP.IndexOf(".", StringComparison.InvariantCulture) + 1).Substring(0, CheckIP.IndexOf(".", StringComparison.InvariantCulture)))
        Catch ex As Exception
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
