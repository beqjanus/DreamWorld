#Region "Copyright"

' Copyright 2014 Fred Beckhusen for outworldz.com https://opensource.org/licenses/AGPL

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

Public Class Handler

#Region "Fields"

    Private WithEvents MyProcess As New Process()

#End Region

#Region "Private Fields"

    Dim Exitlist1 As New Dictionary(Of String, String)
    Dim RegionHandles1 As New Dictionary(Of Integer, String)

#End Region

#Region "Public Methods"

    Public Function Init(ByRef RegionHandles As Dictionary(Of Integer, String), ByRef ExitList As Dictionary(Of String, String)) As Process

        Exitlist1 = ExitList
        RegionHandles1 = RegionHandles
        Return MyProcess

    End Function

#End Region

#Region "Private Methods"

    Private Sub OpensimProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess.Exited
        ' Handle any process that exits by adding it to a dictionary. DoExitHandlerPoll will clean up.

        Dim pid = 0
        pid = CType(sender.Id, Integer)

        Try
            Dim name = RegionHandles1.Item(pid)
            If name.Length > 0 Then
                If Not Exitlist1.ContainsKey(name) Then
                    Exitlist1.Add(name, "")
                    Debug.Print(RegionHandles1.Item(pid) & " Exited")
                End If
            End If
            RegionHandles1.Remove(pid)
        Catch ex As KeyNotFoundException
        End Try

    End Sub

#End Region

End Class
