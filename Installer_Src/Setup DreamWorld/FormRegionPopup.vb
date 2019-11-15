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

Public Class FormRegionPopup

#Region "Private Fields"

    Private _RegionName As String = ""
    Dim gPick As String = ""

#End Region

#Region "ScreenSize"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

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

        _RegionName = RegionName

        Dim X = Form1.PropRegionClass.FindRegionByName(RegionName)
        Me.Text = RegionName
        GroupBox1.Text = Form1.PropRegionClass.GroupName(X)

        If Not Form1.PropRegionClass.RegionEnabled(X) Then
            StartButton3.Enabled = False
            StopButton1.Enabled = False
            RecycleButton2.Enabled = False
            StatsButton.Enabled = False
        Else

            If Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Suspended Then
                StartButton3.Enabled = True
                StopButton1.Enabled = True
                RecycleButton2.Enabled = True
                StatsButton.Enabled = True
            End If

            If Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booted Then
                StartButton3.Enabled = False
                StopButton1.Enabled = True
                RecycleButton2.Enabled = True
                StatsButton.Enabled = True
            End If

            If Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingDown Or
                Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.ShuttingDown Then
                StartButton3.Enabled = False
                StopButton1.Enabled = True
                RecycleButton2.Enabled = False
                StatsButton.Enabled = False
            End If

            If Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Booting Or
                Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.RecyclingUp Then
                StartButton3.Enabled = False
                StopButton1.Enabled = True
                RecycleButton2.Enabled = False
                StatsButton.Enabled = False
            End If

            ' stopped
            If Form1.PropRegionClass.Status(X) = RegionMaker.SIMSTATUSENUM.Stopped Then
                StartButton3.Enabled = True
                StopButton1.Enabled = False
                RecycleButton2.Enabled = False
                StatsButton.Enabled = False
            End If
        End If

        If Not Form1.PropOpensimIsRunning() Then
            StartButton3.Enabled = False
        End If

    End Sub

#End Region

#Region "Private Methods"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles StatsButton.Click
        gPick = "Teleport"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles StatsButton1.Click

        Dim RegionNum = Form1.PropRegionClass.FindRegionByName(_RegionName)
        Dim RegionPort = Form1.PropRegionClass.GroupPort(RegionNum)
        Dim webAddress As String = "http://" & Form1.Settings.PublicIP & ":" & CType(RegionPort, String) & "/SStats/"
        Try
            Process.Start(webAddress)
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try
    End Sub

    Private Sub EditButton1_Click(sender As Object, e As EventArgs) Handles EditButton1.Click
        gPick = "Edit"
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Popup_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

#End Region

End Class