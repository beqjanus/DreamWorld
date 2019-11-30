#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

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
        possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()"
        len = 6

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
