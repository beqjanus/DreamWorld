Imports System
Imports System.Net.Security
Imports System.Collections.Generic
Imports System.Security.Cryptography.X509Certificates
Imports MimeKit
Imports MailKit
Imports MailKit.Security
Imports MailKit.Net.Smtp

Namespace MailKit.SSL
    Module SslCertificateValidation
        Sub SendMessage(ByVal message As MimeMessage)
            Try
                Using client = New SmtpClient()
                    client.ServerCertificateValidationCallback = AddressOf MySslCertificateValidationCallback
                    client.Connect(Settings.SmtpHost, Settings.SmtpPort, SecureSocketOptions.SslOnConnect)
                    client.Authenticate(Settings.SmtPropUserName, Settings.SmtpPassword)
                    client.Send(message)
                    client.Disconnect(True)
                End Using
            Catch ex As Exception
                TextPrint(ex.Message)
            End Try

        End Sub

        Private Function MySslCertificateValidationCallback(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) As Boolean
            If sslPolicyErrors = SslPolicyErrors.None Then Return True
            Dim host = CStr(sender)

            If (sslPolicyErrors And SslPolicyErrors.RemoteCertificateNotAvailable) <> 0 Then
                TextPrint($"The SSL certificate was not available for {host}")
                Return False
            End If

            If (sslPolicyErrors And SslPolicyErrors.RemoteCertificateNameMismatch) <> 0 Then
                Dim certificate2 = TryCast(certificate, X509Certificate2)
                Dim cn = If(certificate2 IsNot Nothing, certificate2.GetNameInfo(X509NameType.SimpleName, False), certificate.Subject)
                TextPrint($"The Common Name for the SSL certificate did not match {host}. Instead, it was {cn}.")
                Return False
            End If

            TextPrint("The SSL certificate for the server could not be validated for the following reasons:")

            For Each element In chain.ChainElements
                If element.ChainElementStatus.Length = 0 Then Continue For
                TextPrint(element.Certificate.Subject)

                For Each E In element.ChainElementStatus
                    TextPrint(vbTab & $"• {E.StatusInformation}")
                Next
            Next

            Return False
        End Function
    End Module
End Namespace
