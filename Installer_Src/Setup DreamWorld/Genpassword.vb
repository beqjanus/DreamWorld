Imports System.Security.Cryptography
Imports System
Imports System.Collections.Generic
Imports System.Text

Public Class PassGen
    Private possibleChars As String
    Private len As Int32

    Public Function GeneratePass() As String
        possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()"
        len = 6

        Try
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
        Catch ex As Exception
            Form1.ErrorLog("exception on password:" + ex.Message)
        End Try
        Return "secret"
    End Function

    Private Function GetRandomInt() As Integer
        Dim randomBytes As Byte() = New Byte(3) {}
        Dim rng As New RNGCryptoServiceProvider()
        rng.GetBytes(randomBytes)
        Dim randomInt As Integer = BitConverter.ToInt32(randomBytes, 0)
        Return randomInt
    End Function

End Class