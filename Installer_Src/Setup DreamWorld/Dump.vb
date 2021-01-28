#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

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
