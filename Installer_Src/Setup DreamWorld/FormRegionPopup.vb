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

Public Class FormRegionPopup

#Region "Private Fields"

    Private _RegionName As String = ""
    Dim gPick As String = ""

#End Region

#Region "ScreenSize"

    Private ReadOnly Handler As New EventHandler(AddressOf Resize_page)
    Private _screenPosition As ScreenPos

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
        'Me.Text = "Form screen position = " + Me.Location.ToString
        ScreenPosition.SaveXY(Me.Left, Me.Top)
    End Sub

    Private Sub SetScreen()
        Me.Show()
        ScreenPosition = New ScreenPos(MyBase.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Public Methods"

    Public Function Choice() As String
        Return gPick
    End Function

    Public Sub Init(RegionName As String)

        If Settings.ServerType = "Robust" Then
            ViewMapButton.Enabled = True
        Else
            ViewMapButton.Enabled = False
        End If

        _RegionName = RegionName

        Dim RegionUUID As String = FormSetup.PropRegionClass.FindRegionByName(RegionName)
        Me.Text = RegionName
        GroupBox1.Text = FormSetup.PropRegionClass.GroupName(RegionUUID)

        If Not FormSetup.PropRegionClass.RegionEnabled(RegionUUID) Then
            ShowConsoleButton.Enabled = False
            StatsButton1.Enabled = False
            StartButton3.Enabled = False
            StopButton1.Enabled = False
            RecycleButton2.Enabled = False
            StatsButton.Enabled = False
            EditButton1.Enabled = True
        Else

            If FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Suspended Then
                ShowConsoleButton.Enabled = True
                'TODO: Unsuspend region

                StatsButton1.Enabled = False
                StartButton3.Enabled = True
                StopButton1.Enabled = True
                RecycleButton2.Enabled = True
                StatsButton.Enabled = True
                EditButton1.Enabled = False
            End If

            If FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booted Then
                ShowConsoleButton.Enabled = True
                StatsButton1.Enabled = True
                StartButton3.Enabled = False
                StopButton1.Enabled = True
                RecycleButton2.Enabled = True
                StatsButton.Enabled = True
                EditButton1.Enabled = True
            End If

            If FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingDown Or
                FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
                ShowConsoleButton.Enabled = True
                StatsButton1.Enabled = False
                StartButton3.Enabled = False
                StopButton1.Enabled = True
                RecycleButton2.Enabled = False
                StatsButton.Enabled = False
                EditButton1.Enabled = True
            End If

            If FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Booting Or
                FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
                ShowConsoleButton.Enabled = True
                StatsButton1.Enabled = False
                StartButton3.Enabled = False
                StopButton1.Enabled = True
                RecycleButton2.Enabled = False
                StatsButton.Enabled = False
                EditButton1.Enabled = False
            End If

            ' stopped
            If FormSetup.PropRegionClass.Status(RegionUUID) = RegionMaker.SIMSTATUSENUM.Stopped Then
                ShowConsoleButton.Enabled = False
                StatsButton1.Enabled = False
                StartButton3.Enabled = True
                StopButton1.Enabled = False
                RecycleButton2.Enabled = False
                StatsButton.Enabled = False
                EditButton1.Enabled = True
            End If
        End If

        For Each p In Process.GetProcesses
            If p.MainWindowTitle = GroupBox1.Text Then

                Exit For
            End If
        Next

        BringToFront()

    End Sub

#End Region

#Region "Private Methods"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles StatsButton.Click
        gPick = "Teleport"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            Dim RegionUUID As String = FormSetup.PropRegionClass.FindRegionByName(_RegionName)
            System.Diagnostics.Process.Start(IO.Path.Combine(FileSystem.CurDir(), "baretail.exe") & " " & """" & FormSetup.PropRegionClass.IniPath(RegionUUID) & "Opensim.log" & """")
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles StatsButton1.Click

        Dim RegionNum = FormSetup.PropRegionClass.FindRegionByName(_RegionName)
        Dim RegionPort = FormSetup.PropRegionClass.GroupPort(RegionNum)
        Dim webAddress As String = "http://" & Settings.PublicIP & ":" & CType(RegionPort, String) & "/SStats/"
        Try
            Process.Start(webAddress)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles ShowConsoleButton.Click
        gPick = "Console"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub EditButton1_Click(sender As Object, e As EventArgs) Handles EditButton1.Click
        gPick = "Edit"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Popup_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Translate.Run(Name)
        SetScreen()

    End Sub

    Private Sub RecycleButton2_Click(sender As Object, e As EventArgs) Handles RecycleButton2.Click
        gPick = "Recycle"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub StartButton3_Click(sender As Object, e As EventArgs) Handles StartButton3.Click
        gPick = "Start"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub StopButton1_Click(sender As Object, e As EventArgs) Handles StopButton1.Click
        gPick = "Stop"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub ViewMapButton_Click(sender As Object, e As EventArgs) Handles ViewMapButton.Click

        FormSetup.VarChooser(_RegionName, False, False)

    End Sub

#End Region

End Class
