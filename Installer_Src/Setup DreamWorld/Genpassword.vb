#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.Security.Cryptography
Imports System.Text

Public Class PassGen

#Region "Private Fields"

    Private len As Int32
    Private possibleChars As String

#End Region

#Region "Public Methods"

    Public Function GeneratePass() As String
        possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        len = 15

        Dim cpossibleChars() As Char
        cpossibleChars = possibleChars.ToCharArray()

        Dim builder As New StringBuilder()

        For i As Integer = 0 To len - 1
            Dim randInt32 As Integer = GetRandomInt()
            Dim r As New Random(randInt32)

            Dim nextInt As Integer = r.[Next](cpossibleChars.Length)
            Dim c As Char = cpossibleChars(nextInt)
            builder.Append(c)
        Next
        Return builder.ToString()

    End Function

#End Region

#Region "Private Methods"

    Private Shared Function GetRandomInt() As Integer
        Dim randomBytes As Byte() = New Byte(3) {}
        Dim rng As New RNGCryptoServiceProvider()
        rng.GetBytes(randomBytes)
        rng.Dispose()
        Dim randomInt As Integer = BitConverter.ToInt32(randomBytes, 0)
        Return randomInt
    End Function

#End Region

End Class
