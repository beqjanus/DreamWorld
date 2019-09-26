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

Public Class Handler

    Private WithEvents MyProcess As New Process()

    Dim RegionHandles1 As New Dictionary(Of Integer, String)
    Dim Exitlist1 As New ArrayList()

    Private Sub OpensimProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess.Exited

        ' Handle any process that exits by stacking it. DoExitHandlerPoll will clean up stack
        Dim pid = 0
        pid = CType(sender.Id, Integer)

        Try
            Exitlist1.Add(RegionHandles1.Item(pid))
            Debug.Print(RegionHandles1.Item(pid) & " Exited")
        Catch ex As KeyNotFoundException
        Catch ex As NotSupportedException
        End Try

        Try
            RegionHandles1.Remove(pid)
        Catch ex As ArgumentNullException
        End Try

    End Sub

    Public Function Init(ByRef RegionHandles As Dictionary(Of Integer, String), ByRef ExitList As ArrayList) As Process

        Exitlist1 = ExitList
        RegionHandles1 = RegionHandles
        Return MyProcess

    End Function

End Class