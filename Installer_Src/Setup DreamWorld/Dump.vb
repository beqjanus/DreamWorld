Module Dump

    Public Function DisplayObjectInfo(o As Object) As String

        Dim sb = New System.Text.StringBuilder()

        'Include the type of the object
        Dim type = o.GetType()
        sb.Append("Dump:" & vbCrLf)

        sb.Append("Type: " + type.Name)
        sb.Append(vbCrLf)

        ' Include information for each Field
        sb.Append("Fields:")
        sb.Append(vbCrLf)

        Dim fi = type.GetFields()
        If fi IsNot Nothing Then
            If fi.Length > 0 Then
                For Each p In fi
                    sb.Append(vbCrLf)
                    sb.Append(p.ToString())
                    sb.Append(" = ")
                    sb.Append(p.GetValue(o)).ToString()
                Next
            Else
                sb.Append("None")
                sb.Append(vbCrLf)
            End If
        End If

        Try
            ' Include information for each Property
            sb.Append(vbCrLf)
            sb.Append("Properties:")
            Dim pi = type.GetProperties()
            If pi IsNot Nothing Then
                If pi.Length > 0 Then
                    For Each p In pi
                        sb.Append(vbCrLf)
                        sb.Append(p.ToString())
                        sb.Append(" = ")
                        sb.Append(p.GetValue(o, Nothing)).ToString()
                    Next
                Else
                    sb.Append(vbCrLf)
                    sb.Append("None")
                End If
            End If
        Catch
        End Try

        Return sb.ToString

    End Function

End Module
