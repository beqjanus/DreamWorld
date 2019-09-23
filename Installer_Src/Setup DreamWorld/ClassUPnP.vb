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

Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

Public Class UPnp
    Implements IDisposable

    Dim UPnpnat As NATUPNPLib.UPnPNAT
    Dim staticMapping As NATUPNPLib.IStaticPortMappingCollection
    Dim dynamicMapping As NATUPNPLib.IDynamicPortMappingCollection

    Private staticEnabled As Boolean = True
    Private dynamicEnabled As Boolean = True
    Private _MyFolder As String = ""
    Private CacheIP As String = ""

    ''' <summary>
    ''' The different supported protocols
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MyProtocol
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

    Public Property Myfolder1 As String
        Get
            Return _MyFolder
        End Get
        Set(value As String)
            _MyFolder = value
        End Set
    End Property

    ''' <summary>
    ''' The UPnp Managed Class
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(Folder As String)

        Myfolder1 = Folder

        'Create the new NAT Class
        Try
            UPnpnat = New NATUPNPLib.UPnPNAT
        Catch ex As Exception
            Log(ex.Message)
        End Try

        'generate the static mappings
        GetStaticMappings()
        GetDynamicMappings()

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
        Catch ex As Exception
            Log("Router does not support Static mappings." & ex.Message)
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
        Catch ex As Exception
            Log("Router does not support Dynamic mappings." & ex.Message)
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
    ''' <remarks></remarks>
    Public Sub Add(ByVal localIP As String, ByVal port As Integer, ByVal prot As MyProtocol, ByVal desc As String)

        Try
            ' Begin utilizing
            If Exists(port, prot) Then Log("This mapping already exists:" & CStr(port))

            ' Check
            If Not IPCheck.IsPrivateIP(localIP) Then Log("This is not a local IP address:" & localIP)

            ' Final check!
            If Not staticEnabled Then Log("UPnP is not enabled, or there was an error with UPnP Initialization.")

            ' Okay, continue on
            staticMapping.Add(port, CStr(prot), port, localIP, True, desc + ":" + CStr(port))
        Catch ex As Exception
            Log(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Removes a port mapping from the UPnp enabled device.
    ''' </summary>
    ''' <param name="Port">The port to remove.</param>
    ''' <param name="prot">The protocol of the port [TCP/UDP]</param>

    ''' <remarks></remarks>
    Public Sub Remove(ByVal port As Integer, ByVal prot As MyProtocol)

        Try
            ' Begin utilizing
            If Not Exists(port, prot) Then Log("This mapping doesn't exist!" & CStr(port))

            ' Final check!
            If Not staticEnabled Then Log("UPnp is not enabled, or there was an error with UPnp Initialization.")

            ' Okay, continue on
            staticMapping.Remove(port, prot.ToString)
        Catch ex As Exception
            Log(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Checks to see if a port exists in the mapping.
    ''' </summary>
    ''' <param name="Port">The port to check.</param>
    ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
    ''' <remarks></remarks>
    Public Function Exists(ByVal port As Integer, ByVal prot As MyProtocol) As Boolean
        Try
            ' Final check!
            If Not staticEnabled Then Log("UPnp is not enabled, or there was an error with UPnp Initialization.")

            ' Begin checking
            For Each mapping As NATUPNPLib.IStaticPortMapping In staticMapping

                ' Compare
                If mapping.ExternalPort.Equals(port) AndAlso mapping.Protocol = prot.ToString() Then
                    Return True
                End If

            Next
        Catch ex As Exception
            Log(ex.Message)
        End Try

        'Nothing!
        Return False

    End Function

    Public Function LocalIP() As String

        Dim LIP As String = ""
        Try
            If CacheIP.Length = 0 Then
                If Form1.PropMySetting.DNSName = "localhost" Or Form1.PropMySetting.DNSName = "127.0.0.1" Then
                    Return Form1.PropMySetting.DNSName
                End If

                Dim sock As Socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0)
                Try
                    Using sock
                        sock.Connect("8.8.8.8", 65530)  ' try Google
                        Dim EndPoint As IPEndPoint = TryCast(sock.LocalEndPoint, IPEndPoint)
                        LIP = EndPoint.Address.ToString()
                    End Using
                Catch ex As Exception
                    LIP = LocalIPForced()

                    If LIP.Length = 0 Then
                        LIP = "127.0.0.1"
                    End If
                End Try
                CacheIP = LIP
            Else
                LIP = CacheIP
            End If
        Catch ex As Exception
            Log(ex.Message)
        End Try
        Return LIP

    End Function

    ''' <summary>
    ''' Attempts to locate the local IP address of this computer.
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Function LocalIPForced() As String
        Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)

        For Each IPaddress In IPList.AddressList
            If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) AndAlso IPCheck.IsPrivateIP(IPaddress.ToString()) Then
                Dim ip = IPaddress.ToString()
                Return ip
            End If
        Next
        Return String.Empty
    End Function

    ''' <summary>
    ''' Disposes of the UPnp class
    ''' </summary>
    ''' <param name="disposing">True or False makes no difference.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub Dispose(disposing As Boolean)
        Try
            If staticMapping IsNot Nothing Then Marshal.ReleaseComObject(staticMapping)
            If dynamicMapping IsNot Nothing Then Marshal.ReleaseComObject(dynamicMapping)
            Marshal.ReleaseComObject(UPnpnat)
        Catch ex As Exception
            Log(ex.Message)
        End Try
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

        ' Loop through all the data after a check
        If staticEnabled Then

            Log("---------Static Mappings-----------------------")
            Try
                For Each mapping As NATUPNPLib.IStaticPortMapping In staticMapping
                    If mapping.Enabled Then
                        Log(String.Format(Form1.Invarient, "Enabled: {0}", mapping.Description))
                    Else
                        Log(String.Format(Form1.Invarient, "**Disabled**: {0}", mapping.Description))
                    End If
                    Log(String.Format(Form1.Invarient, "Port: {0}", CStr(mapping.InternalPort)))
                    Log(String.Format(Form1.Invarient, "Protocol: {0}", CStr(mapping.Protocol)))
                    Log(String.Format(Form1.Invarient, "External IP Address: {0}", CStr(mapping.ExternalIPAddress)))
                    Log("--------------------------------------")
                Next
            Catch ex As Exception
                Log(ex.Message)
            End Try

        End If

    End Sub

    Public Sub Log(message As String)
        Try
            Using outputFile As New StreamWriter(Myfolder1 & "\OutworldzFiles\UPnp.log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", Form1.Invarient) + ":" + message)
            End Using
        Catch ex As Exception
        End Try
    End Sub

End Class