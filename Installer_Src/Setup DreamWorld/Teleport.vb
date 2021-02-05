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

Module Teleport

    Public TeleportAvatarDict As New Dictionary(Of String, String)

    Public Sub TeleportAgents()

        If Settings.SmartStart Then
            For Each Keypair In TeleportAvatarDict

                Dim AgentName = Keypair.Key
                Dim RegionToUUID = Keypair.Value

                If AgentName.Length > 0 Then
                    Dim DestinationName = PropRegionClass.RegionName(RegionToUUID)
                    If DestinationName.Length > 0 Then
                        TextPrint(My.Resources.Teleporting_word & " " & AgentName & " -> " & PropRegionClass.RegionName(RegionToUUID))
                        TeleportTo(DestinationName, AgentName)
                    Else
                        BreakPoint.Show("Unable to locate region " & RegionToUUID)
                    End If
                End If
            Next
        End If

        TeleportAvatarDict.Clear()

    End Sub

End Module
