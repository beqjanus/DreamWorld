Module Dump

    Public Function DisplayObjectInfo(o As Object) As String

        Dim sb = New System.Text.StringBuilder()

        'Include the type of the object
        Dim type = o.GetType()
        sb.Append("Type: " + type.Name)

        ' Include information for each Field
        sb.Append("\r\n\r\nFields:")
        Dim fi = type.GetFields()
        If fi.Length > 0 Then
            For Each f In fi
                sb.Append("\r\n " + f.ToString() + " = " + f.GetValue(o))
            Next
        Else
            sb.Append("\r\n None")
        End If

        ' Include information for each Property
        sb.Append("\r\n\r\nProperties:")
        Dim pi = type.GetProperties()
        If pi.Length > 0 Then
            For Each p In pi
                sb.Append("\r\n " & p.ToString() & " = " & p.GetValue(o, Nothing))
            Next
        Else
            sb.Append("\r\n None")
            Return sb.ToString()
        End If

        Return sb.ToString

    End Function

End Module