Imports System.Threading.Tasks
Imports Certes
Imports Certes.Acme
Imports Certes.Acme.Resource
Imports System.IO
Imports System.Threading

Public Class SSL
    Private ReadOnly context As AcmeContext
    Private order As IOrderContext

#Region "Wacs"

    Public Sub New()

        Try
            Dim WebThread = New Thread(AddressOf InstallSSL)
            WebThread.SetApartmentState(ApartmentState.STA)
            WebThread.Priority = ThreadPriority.BelowNormal ' UI gets priority
            WebThread.Start()
        Catch
        End Try

    End Sub

    Private Sub InstallSSL()

        Using SSLProcess As New Process

            SSLProcess.StartInfo.UseShellExecute = True
            SSLProcess.StartInfo.WorkingDirectory = IO.Path.Combine(Settings.CurrentDirectory, "SSL")
            SSLProcess.StartInfo.FileName = "wacs.exe"
            SSLProcess.StartInfo.CreateNoWindow = False

            Select Case Settings.ConsoleShow
                Case "True"
                    SSLProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                Case "False"
                    SSLProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                Case "None"
                    SSLProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            End Select

            SSLProcess.StartInfo.Arguments = $"--source manual --host {Settings.DNSName} --validation filesystem --webroot ""{IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\htdocs")}"" --store pemfiles --pemfilespath ""{IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\Apache\Certs")}"""

            MakeSSLbatch($".\wacs.exe {SSLProcess.StartInfo.Arguments}")

            Try
                SSLProcess.Start()
                SSLProcess.WaitForExit()
            Catch ex As Exception
                ErrorLog(ex.Message)
            End Try

            Dim Status = SSLProcess.ExitCode
            If Status = 0 Then
                Logger("Info", "Certificate installed", "SSL")
                ' It was not installed, so we need to restart Apache
                If Settings.SSLIsInstalled = False Then
                    Settings.SSLIsInstalled = True

                    StopApache()
                    DoApache()
                    StartApache()

                End If
            End If

        End Using
        Return
    End Sub

    Private Sub MakeSSLbatch(stuff As String)

        Dim filename = IO.Path.Combine(Settings.CurrentDirectory, "SSL\InstallSSL.bat")

        Using file As New System.IO.StreamWriter(filename, False)
            file.WriteLine("@REM program to renew SSL certificate")
            file.WriteLine($"cd {IO.Path.Combine(Settings.CurrentDirectory, "SSL")}")
            file.WriteLine(stuff)
            file.WriteLine("@pause")
        End Using

    End Sub

#End Region

#Region "Certes"

    ''' old code

    Private Property ZPemKey As String
        Get
            Dim File = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/PemKey.key")
            If Not IO.File.Exists(File) Then Return ""

            Dim d As String = ""
            Using Reader As New StreamReader(File)
                While Not Reader.EndOfStream
                    d += Reader.ReadLine
                End While
            End Using
            Return d
        End Get
        Set(value As String)
            Dim f = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/PemKey.key")
            DeleteFile(f)
            Using file As New System.IO.StreamWriter(f, False)
                file.WriteLine(value)
            End Using
        End Set

    End Property

    Public Sub ZDoSSL()

        Dim accountInfo = MyAccountAsync()
        Dim orderUri = NewOrderAsync()

    End Sub

    'https://github.com/fszlin/certes/blob/main/docs/APIv2.md
    'https://docs.certes.app/APIv2.html
    '
    ' Command line tool install
    ' dotnet tool install --global dotnet-certes
    ' certes account new fred@outworldz.com'
    '
    ' certes order new smartboot.outworldz.net
    ' {"location":"https://acme-v02.api.letsencrypt.org/acme/order/382037190/58749705050",
    ' ' "resource":{"status":"pending",
    '   "expires":"2022-02-03T00:56:00+00:00",
    '   "identifiers":[{"type":"dns",
    '   "value":"smartboot.outworldz.net"}],
    '   "authorizations":["https://acme-v02.api.letsencrypt.org/acme/authz-v3/72254102330"],
    '   "finalize":"https://acme-v02.api.letsencrypt.org/acme/finalize/382037190/58749705050"}}
    '
    ' certes order list
    ' validate <order-id> <domain> <challenge-type>  Validate the authorization challenge.
    ' certes validate
    '
    '  dotnet-certes [options] order validate <order-id> <domain> <challenge-type>
    'Arguments:
    '<Order-id>        The URI of the certificate order.
    '<domain> The domain.
    '<Challenge-type>  The challenge type, http Or dns.
    '  validate https://acme-v02.api.letsencrypt.org/acme/order/382037190/58749705050 smartboot.outworldz.net
    '
    Private Async Function MyAccountAsync() As Task(Of Account)

        Dim account = Await context.Account()
        Console.WriteLine($" Location : {account.Location}")
        Dim res = Await account.Resource()
        Console.WriteLine($" Status   : {res.Status}")

        Logger("Info", "Resource created", "SSL")
        Return res

    End Function

    Private Async Function NewOrderAsync() As Task(Of Object)

        order = Await context.NewOrder({Settings.DNSName})

        ' get the Get the token and key authorization string
        Dim Authz = (Await order.Authorizations()).First()
        Dim httpChallenge = Await Authz.Http()
        Dim keyAuthz = httpChallenge.KeyAuthz
        Dim token = httpChallenge.Token
        Logger("Info", "Auth token and key ready", "SSL")

        'Save the key authorization String In a text file, And upload it to http://your.domain.name/.well-known/acme-challenge/<token>
        Dim folder As String
        If Debugger.IsAttached Then
            folder = IO.Path.Combine("C:/Inetpub/Medvik/.well-known/acme-challenge")
        Else
            folder = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles/Apache/htdocs/.well-known/acme-challenge")
        End If

        If Not SaveCert(keyAuthz, folder, token) Then Return False
        Logger("Info", "Well known challenge created", "SSL")

        'Ask the ACME server to validate our domain ownership
        Await httpChallenge.Validate()
        Dim result = Await httpChallenge.Resource
        Logger("Info", "Challenge made", "SSL")

        ' Invalid      The invalid status.
        ' Pending      The pending status.
        ' Processing   The processing status.
        ' Valid        The valid status

        If result.Status = ChallengeStatus.Invalid Then
            Logger("Error", result.Error.Detail, "SSL")
            Return True
        End If

        Dim attempts = 10
        While attempts > 0 And (result.Status = ChallengeStatus.Pending Or result.Status = ChallengeStatus.Processing)
            If result.Status <> ChallengeStatus.Valid Then
                Logger("Retry", result.Error.Detail, "SSL")
                Sleep(10000)
                attempts -= 1
                Logger("Error", $"Retry {CStr(attempts)}", "SSL")
                result = Await httpChallenge.Resource()
            Else
                Exit While
            End If

        End While

        If attempts = 0 Then
            Logger("Error", $"Timed Out at Challenge", "SSL")
            Return False
        End If

        If result.Status <> ChallengeStatus.Valid Then
            Logger("Error", $"Challenge Invalid", "SSL")
            Return False
        End If

        Dim pk = KeyFactory.NewKey(KeyAlgorithm.RS256)

        Await order.Generate(New CsrInfo With {
            .CountryName = "Country",
            .State = "State",
            .Locality = "City",
            .Organization = "Org",
            .CommonName = Settings.DNSName
        }, pk, "ISRG X1 Root")

        'Download the certificate for a finalized order.
        Dim certChain = Await order.Download("ISRG X1 Root")
        Logger("Success", $"Cert Chain received", "SSL")

        Dim Pem1 = certChain.Certificate.ToPem

        SaveCert(Pem1, IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles/Apache/Certs/"), "server.crt")
        Logger("Success", $"Certificate PEM chain saved to Apache as server.crt", "SSL")

        SaveCert(pk.ToPem, IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles/Apache/Certs/"), "server.key")
        Logger("Success", $"Private Key saved to Apache as server.key", "SSL")

        ' none of this works, docs do not match reality, CertificateInfo is not a class
        'Export PFX
        'Dim cert = New CertificateInfo(certChain, certKey)
        '
        '       Dim pem = cert.ToPem()
        '      Dim der = cert.ToDer()
        '     Dim pfx = cert.ToPfx("cert-name", "abcd1234")

        '     Dim keyPem = cert.Key.ToPem()

        'Dim pfxBuilder = certChain.ToPfx(pk)

        'Dim pfx() = pfxBuilder.Build("my-cert", Settings.MachineID)
        'Dim p = pfx.ToString
        'SaveCert(p, IO.Path.Combine(Settings.CurrentDirectory, "Outworldzfiles/Apache/Cert/"), "server-ca.crt")
        'Logger("Success", $"PFX saved to Apache as server-ca.crt", "SSL")

        Return True

    End Function

    Private Sub oldstuff()

        Dim context As AcmeContext
        Dim Key = ZPemKey()
        If Key.Length > 0 Then
            ' use an existing ACME account:
            'Load the saved account key
            Dim accountKey = KeyFactory.FromPem(Key)
            context = New AcmeContext(WellKnownServers.LetsEncryptStagingV2, accountKey)
            Logger("Info", "Using existing account", "SSL")
        Else
            context = New AcmeContext(WellKnownServers.LetsEncryptStagingV2)
            Dim result = context.NewAccount(Settings.SSLEmail, True)
            ' Save the account key for later use
            ZPemKey = context.AccountKey.ToPem()
            Logger("Info", $"Created new account for {Settings.SSLEmail}", "SSL")
        End If

        Dim TOS = context.TermsOfService()
        Logger("Info", $"TOS: {TOS}", "SSL")
        ' await account.UpdateUpdate(contact: New() { $"mailto:support@example.com" },agreeTermsOfService: true)

    End Sub

    ''' <summary>
    ''' 'Save the key authorization String In a text file, And upload it to http://your.domain.name/.well-known/acme-challenge/<token>
    ''' </summary>
    ''' <param name="Contents"></param>
    ''' <param name="folder"></param>
    ''' <param name="filename"></param>
    ''' <returns>true if it succeed</returns>
    Private Function SaveCert(Contents As String, path As String, filename As String) As Boolean

        Dim file = IO.Path.Combine(path, filename)

        MakeFolder(path)
        DeleteFile(file)

        Try
            Dim utf8WithoutBom = New System.Text.UTF8Encoding(False)
            Dim f = My.Computer.FileSystem.OpenTextFileWriter(file, False, utf8WithoutBom)
            f.WriteLine(Contents)
            f.Close()
        Catch ex As Exception
            BreakPoint.Dump(ex)
            Return False
        End Try

        Return True
    End Function

#End Region

End Class
