Imports System.Threading.Tasks
Imports Certes
Imports Certes.Acme
Imports Certes.Acme.Resource

'https://github.com/fszlin/certes/blob/main/docs/APIv2.md

Public Class SSL
    Private ReadOnly context As AcmeContext
    Private order As IOrderContext

    Public Sub New()

        If Not Settings.DNSName.EndsWith(".outworldz.net", StringComparison.InvariantCulture) Or
               Settings.DNSName.EndsWith(".inworldz.net", StringComparison.InvariantCulture) Then
            Return
        End If

        If Settings.PemKey.Length > 0 Then
            ' use an existing ACME account:
            'Load the saved account key
            Dim accountKey = KeyFactory.FromPem(Settings.PemKey)
            context = New AcmeContext(WellKnownServers.LetsEncryptStagingV2, accountKey)
        Else
            context = New AcmeContext(WellKnownServers.LetsEncryptStagingV2)
            Dim result = context.NewAccount("fred@outworldz.com", True)
            ' Save the account key for later use
            Settings.PemKey = context.AccountKey.ToPem()
        End If

        Dim accountInfo = MyAccountAsync()
        Dim orderUri = NewOrderAsync()

    End Sub

    Private Async Function MyAccountAsync() As Task(Of Account)

        Dim account = Await context.Account()
        Dim res = Await account.Resource()
        Return res

    End Function

    Private Async Function NewOrderAsync() As Task(Of Object)

        order = Await context.NewOrder({Settings.DNSName})

        ' get the Get the token and key authorization string
        Dim Authz = (Await order.Authorizations()).First()
        Dim httpChallenge = Await Authz.Http()
        Dim keyAuthz = httpChallenge.KeyAuthz
        Dim token = httpChallenge.Token
        'Save the key authorization String In a text file, And upload it to http://your.domain.name/.well-known/acme-challenge/<token>
        SaveCert(keyAuthz, $"Outworldzfiles/Apache/htdocs/.well-known/acme-challenge/{token}")

        'Ask the ACME server to validate our domain ownership
        Dim result = httpChallenge.Validate()

        Dim attempts = 10
        While attempts > 0 And result.Status = ChallengeStatus.Pending Or result.Status = ChallengeStatus.Processing
            result = httpChallenge.Resource()
            Sleep(1000)
            attempts -= 1
        End While

        If attempts = 0 Then Return False

        If result.Status = ChallengeStatus.Invalid Then
            Return False
        End If

        'Download the certificate once validation is done
        Dim privateKey = KeyFactory.NewKey(KeyAlgorithm.ES256)
        Dim cert = Await order.Generate(New CsrInfo With {
            .CountryName = "US",
            .State = "Texas",
            .Locality = "Allen",
            .Organization = "Outworldz, LLC.",
            .OrganizationUnit = "Outworldz",
            .CommonName = Settings.DNSName
        }, privateKey)

        'Export full chain certification
        Dim certPem = cert.ToPem()

        'Download the certificate for a finalized order.
        Dim certChain = Await order.Download()

        SaveCert(privateKey.ToString, "Outworldzfiles/Apache/conf/ssl/private.key")
        SaveCert(certChain.ToString, "Outworldzfiles/Apache/conf/ssl/freessl.key")

        'Export PFX
        Dim pfxBuilder = cert.ToPfx(privateKey)
        Dim pfx = pfxBuilder.Build("my-cert", "abcd1234")

        Return True

    End Function

    Private Sub SaveCert(Content As String, file As String)

        Dim foldername = IO.Path.Combine(Settings.CurrentDirectory, file)

        Try
            DeleteFile(file)
            Dim f = My.Computer.FileSystem.OpenTextFileWriter(file, False)
            f.WriteLine(Content)
            f.Close()
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

    End Sub

End Class
