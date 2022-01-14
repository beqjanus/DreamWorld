Imports System
Imports System.Security.Cryptography
Imports System.Text

Namespace HashingAlgorithm.Library

    Public Enum HashingClassAlgorithms
        MD5 = 0
        SHA1 = 1
        SHA256 = 2
        SHA384 = 3
        SHA512 = 4
    End Enum

    Public NotInheritable Class Hashing

        Public Sub New(ByVal hashingAlgorithm As HashingClassAlgorithms)
            Me.HashingAlgorithms = hashingAlgorithm
        End Sub

        Public Property HashingAlgorithms As HashingClassAlgorithms

        Public Function CreateNewInstance() As HashAlgorithm
            Dim result As HashAlgorithm = Nothing

            Select Case Me.HashingAlgorithms
                Case HashingClassAlgorithms.MD5
                    result = MD5.Create()
                Case HashingClassAlgorithms.SHA1
                    result = SHA1Managed.Create()
                Case HashingClassAlgorithms.SHA256
                    result = SHA256Managed.Create()
                Case HashingClassAlgorithms.SHA384
                    result = SHA384.Create()
                Case HashingClassAlgorithms.SHA512
                    result = SHA512Managed.Create()
            End Select

            Return result
        End Function

    End Class

    Public Class HashingHelper

        Public Shared Function ComputeHashInLowerCase(ByVal input As String, ByVal algorithm As HashingClassAlgorithms) As String
            Dim result As HashAlgorithm = ProcessAlgorithm(input, algorithm)
            Return ComputeHash(input, result, False)
        End Function

        Public Shared Function ComputeHashInUpperCase(ByVal input As String, ByVal algorithm As HashingClassAlgorithms) As String
            Dim result As HashAlgorithm = ProcessAlgorithm(input, algorithm)
            Return ComputeHash(input, result)
        End Function

        Private Shared Function ComputeHash(ByVal input As String, ByVal algorithm As HashAlgorithm, ByVal Optional upperCase As Boolean = True) As String
            Dim result As String = String.Empty
            Dim hashingService = algorithm

            Using hashingService
                Dim hash As Byte() = hashingService.ComputeHash(Encoding.UTF8.GetBytes(input))
                result = String.Concat(Array.ConvertAll(hash, Function(h) h.ToString($"{(If(upperCase, "X2", "x2"))}")))
            End Using

            Return result
        End Function

        Private Shared Function CreateNewInstance(ByVal algorithm As HashingClassAlgorithms) As HashAlgorithm
            Return New Hashing(algorithm).CreateNewInstance()
        End Function

        Private Shared Function ProcessAlgorithm(ByVal input As String, ByVal algorithm As HashingClassAlgorithms) As HashAlgorithm
            Dim result As HashAlgorithm = CreateNewInstance(algorithm)
            ValidateParams(input, result)
            Return result
        End Function

        Private Shared Sub Validate(ByVal input As String)
            If String.IsNullOrWhiteSpace(input) Then
                Throw New ArgumentNullException("Input parameter is empty or only contains whitespace")
            End If
        End Sub

        Private Shared Sub Validate(ByVal algorithm As HashAlgorithm)
            If algorithm Is Nothing Then
                Throw New ArgumentNullException("algorithm is null")
            End If
        End Sub

        Private Shared Sub ValidateParams(ByVal input As String, ByVal algorithm As HashAlgorithm)
            Validate(input)
            Validate(algorithm)
        End Sub

    End Class

End Namespace
