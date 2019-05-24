Imports System.Net.Sockets
Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices

Public Class UPnp
    Implements IDisposable

    Dim UPnpnat As NATUPNPLib.UPnPNAT
    Dim staticMapping As NATUPNPLib.IStaticPortMappingCollection
    Dim dynamicMapping As NATUPNPLib.IDynamicPortMappingCollection

    Private staticEnabled As Boolean = True
    Private dynamicEnabled As Boolean = True
    Private myfolder As String = ""
    Private CacheIP As String = ""

    ''' <summary>
    ''' The different supported protocols
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Protocol
        ''' <summary>
        ''' Transmission Control Protocol
        ''' </summary>

        TCP

        ''' <summary>
        ''' User Datagram Protocol
        ''' </summary>
        UDP

    End Enum

    ''' <summary>
    ''' Returns if UPnp is enabled.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UPnpEnabled As Boolean
        Get
            Return staticEnabled = True OrElse dynamicEnabled = True
        End Get
    End Property



    ''' <summary>
    ''' The UPnp Managed Class
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(Folder As String)

        myfolder = Folder

        'Create the new NAT Class
        UPnpnat = New NATUPNPLib.UPnPNAT

        'generate the static mappings
        Me.GetStaticMappings()
        Me.GetDynamicMappings()

        Print()

    End Sub

    ''' <summary>
    ''' Returns all static port mappings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetStaticMappings()
        Try

            staticMapping = UPnpnat.StaticPortMappingCollection()
            If staticMapping Is Nothing Then
                Log("Router does not support Static mappings.")
                staticEnabled = False
                Return
            End If

            If staticMapping.Count = 0 Then
                Log("Router does not have any active UPnP mappings.")
            End If

        Catch ex As NotImplementedException
            Log("Router does not support Static mappings.")
            staticEnabled = False
        End Try
    End Sub

    ''' <summary>
    ''' Returns all dynamic port mappings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDynamicMappings()
        Try
            dynamicMapping = UPnpnat.DynamicPortMappingCollection()
            If dynamicMapping Is Nothing Then
                dynamicEnabled = False
            End If
        Catch ex As NotImplementedException
            Log("Router does not support Dynamic mappings.")
            dynamicEnabled = False
        End Try
    End Sub

    ''' <summary>
    ''' Adds a port mapping to the UPnp enabled device.
    ''' </summary>
    ''' <param name="localIP">The local IP address to map to.</param>
    ''' <param name="Port">The port to forward.</param>
    ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
    ''' <param name="desc">A small description of the port.</param>
    ''' <exception cref="ApplicationException">This exception is thrown when UPnp is disabled.</exception>
    ''' <exception cref="ObjectDisposedException">This exception is thrown when this class has been disposed.</exception>
    ''' <exception cref="ArgumentException">This exception is thrown when any of the supplied arguments are invalid.</exception>
    ''' <remarks></remarks>
    Public Sub Add(ByVal localIP As String, ByVal port As Integer, ByVal prot As Protocol, ByVal desc As String)

        ' Begin utilizing
        If Exists(port, prot) Then Throw New ArgumentException("This mapping already exists:", port.ToString)

        ' Check
        If Not IsPrivateIP(localIP) Then Throw New ArgumentException("This is not a local IP address:", localIP)

        ' Final check!
        If Not staticEnabled Then Throw New ApplicationException("UPnP is not enabled, or there was an error with UPnP Initialization.")

        ' Okay, continue on
        staticMapping.Add(port, prot.ToString(), port, localIP, True, desc + ":" + port.ToString)

    End Sub

    ''' <summary>
    ''' Removes a port mapping from the UPnp enabled device.
    ''' </summary>
    ''' <param name="Port">The port to remove.</param>
    ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
    ''' <exception cref="ApplicationException">This exception is thrown when UPnp is disabled.</exception>
    ''' <exception cref="ObjectDisposedException">This exception is thrown when this class has been disposed.</exception>
    ''' <exception cref="ArgumentException">This exception is thrown when the port [or protocol] is invalid.</exception>
    ''' <remarks></remarks>
    Public Sub Remove(ByVal port As Integer, ByVal prot As Protocol)

        ' Begin utilizing
        If Not Exists(port, prot) Then Throw New ArgumentException("This mapping doesn't exist!", port.ToString)

        ' Final check!
        If Not staticEnabled Then Throw New ApplicationException("UPnp is not enabled, or there was an error with UPnp Initialization.")

        ' Okay, continue on
        staticMapping.Remove(port, prot.ToString)

    End Sub

    ''' <summary>
    ''' Checks to see if a port exists in the mapping.
    ''' </summary>
    ''' <param name="Port">The port to check.</param>
    ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
    ''' <exception cref="ApplicationException">This exception is thrown when UPnp is disabled.</exception>
    ''' <exception cref="ObjectDisposedException">This exception is thrown when this class has been disposed.</exception>
    ''' <exception cref="ArgumentException">This exception is thrown when the port [or protocol] is invalid.</exception>
    ''' <remarks></remarks>
    Public Function Exists(ByVal port As Integer, ByVal prot As Protocol) As Boolean

        ' Final check!
        If Not staticEnabled Then Throw New ApplicationException("UPnp is not enabled, or there was an error with UPnp Initialization.")

        ' Begin checking
        For Each mapping As NATUPNPLib.IStaticPortMapping In staticMapping

            ' Compare
            If mapping.ExternalPort.Equals(port) AndAlso mapping.Protocol.ToString.Equals(prot.ToString) Then
                Return True
            End If

        Next

        'Nothing!
        Return False

    End Function

    Public Function LocalIP() As String

        If CacheIP.Length = 0 Then
            If Form1.MySetting.DNSName = "localhost" Or Form1.MySetting.DNSName = "127.0.0.1" Then
                Return Form1.MySetting.DNSName
            End If

            Dim sock As Socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0)
            Try
                Using sock
                    sock.Connect("8.8.8.8", 65530)  ' try Google
                    Dim EndPoint As IPEndPoint = TryCast(sock.LocalEndPoint, IPEndPoint)
                    LocalIP = EndPoint.Address.ToString()
                End Using
            Catch ex As Exception
                LocalIP = LocalIPForced()

                If LocalIP.Length = 0 Then
                    LocalIP = "127.0.0.1"
                End If
            End Try
            CacheIP = LocalIP

        Else
            LocalIP = CacheIP
        End If



    End Function

    ''' <summary>
    ''' Attempts to locate the local IP address of this computer.
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function LocalIPForced() As String
        Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)

        For Each IPaddress In IPList.AddressList
            If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) AndAlso IsPrivateIP(IPaddress.ToString()) Then
                Dim ip = IPaddress.ToString()
                Return ip
            End If
        Next
        Return String.Empty
    End Function

    ''' <summary>
    ''' Checks to see if an IP address is a local IP address.
    ''' </summary>
    ''' <param name="CheckIP">The IP address to check.</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Shared Function IsPrivateIP(ByVal CheckIP As String) As Boolean

        If CheckIP = "localhost" Then Return True

        Dim Quad1, Quad2 As Integer
        Try
            Quad1 = CInt(CheckIP.Substring(0, CheckIP.IndexOf(".")))
            Quad2 = CInt(CheckIP.Substring(CheckIP.IndexOf(".") + 1).Substring(0, CheckIP.IndexOf(".")))
        Catch
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

    ''' <summary>
    ''' Disposes of the UPnp class
    ''' </summary>
    ''' <param name="disposing">True or False makes no difference.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If staticMapping IsNot Nothing Then Marshal.ReleaseComObject(staticMapping)
        If dynamicMapping IsNot Nothing Then Marshal.ReleaseComObject(dynamicMapping)
        Marshal.ReleaseComObject(UPnpnat)
    End Sub

    ''' <summary>
    ''' Dispose!
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub


    ''' Prints out some debugging information to use.

    Public Sub Print()

        Try
            '
            ' Loop through all the data after a check
            If staticEnabled Then

                Log("---------Static Mappings-----------------------")

                For Each mapping As NATUPNPLib.IStaticPortMapping In staticMapping
                    If mapping.Enabled Then
                        Log(String.Format("Enabled: {0}", mapping.Description))
                    Else
                        Log(String.Format("**Disabled**: {0}", mapping.Description))
                    End If
                    Log(String.Format("Port: {0}", Convert.ToString(mapping.InternalPort)))
                    Log(String.Format("Protocol: {0}", Convert.ToString(mapping.Protocol)))
                    Log(String.Format("External IP Address: {0}", Convert.ToString(mapping.ExternalIPAddress)))
                    Log("--------------------------------------")
                Next
            End If
        Catch
        End Try


    End Sub

    Public Sub Log(message As String)
        Try
            Using outputFile As New StreamWriter(myfolder & "\OutworldzFiles\UPnp.log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message)
            End Using
        Catch
        End Try
    End Sub


End Class