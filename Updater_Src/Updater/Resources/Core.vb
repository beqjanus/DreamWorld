Imports System.IO

Public Module Core

    Private _initialized As Boolean

    Public Sub EnsureInitialized()
        If Not _initialized Then
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf AssemblyResolve
            _initialized = True
        End If
    End Sub

    Private Function AssemblyResolve(ByVal sender As Object, ByVal e As ResolveEventArgs) As Reflection.Assembly

        MsgBox(e.Name)
        Dim resourceFullName As String = String.Format("Ionic.{0}.dll", e.Name.Split(","c)(0))
        Dim thisAssembly As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly()
        Using resource As Stream = thisAssembly.GetManifestResourceStream(resourceFullName)
            If resource IsNot Nothing Then Return Reflection.Assembly.Load(ToBytes(resource))
            Return Nothing
        End Using
    End Function

    Private Function ToBytes(ByVal instance As Stream) As Byte()
        Dim capacity As Integer = If(instance.CanSeek, Convert.ToInt32(instance.Length), 0)

        Using result As New MemoryStream(capacity)
            Dim readLength As Integer
            Dim buffer(4096) As Byte

            Do
                readLength = instance.Read(buffer, 0, buffer.Length)
                result.Write(buffer, 0, readLength)
            Loop While readLength > 0

            Return result.ToArray()
        End Using
    End Function

End Module