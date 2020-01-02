Imports Newtonsoft.Json
Imports System.Net

Public Class FormOAR



#Region "Private Fields"

    Private json As New JSONresult

#End Region



#Region "Private Methods"

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim result As String = Nothing
        Using client As New WebClient ' download client for web pages
            Try
                Dim str = Form1.SecureDomain() & "/outworldz_installer/JSON/OAR.json?r=1" & Form1.GetPostData()
                result = client.DownloadString(str)
            Catch ex As ArgumentNullException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
            Catch ex As WebException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
            Catch ex As NotSupportedException
                Form1.ErrorLog(My.Resources.Wrong & " " & ex.Message)
            End Try
        End Using
        Try
            Dim json() As JSONresult = JsonConvert.DeserializeObject(Of JSONresult())(result)
            For Each item In json

                Diagnostics.Debug.Print(item.name)

            Next
        Catch
        End Try


    End Sub

#End Region



#Region "Private Classes"

    Private Class JSONresult

#Region "Public Fields"

        Public [Date] As String
        Public license As String
        Public name As String
        Public photo As String
        Public size As String

#End Region

    End Class

#End Region

End Class
