﻿Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports Outworldz

Public Class FormMaps
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
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load
        If Form1.MySetting.MapType = "None" Then
            MapNone.Checked = True
            MapPicture.Image = My.Resources.blankbox
        ElseIf Form1.MySetting.MapType = "Simple" Then
            MapSimple.Checked = True
            MapPicture.Image = My.Resources.Simple
        ElseIf Form1.MySetting.MapType = "Good" Then
            MapGood.Checked = True
            MapPicture.Image = My.Resources.Good
        ElseIf Form1.MySetting.MapType = "Better" Then
            MapBetter.Checked = True
            MapPicture.Image = My.Resources.Better
        ElseIf Form1.MySetting.MapType = "Best" Then
            MapBest.Checked = True
            MapPicture.Image = My.Resources.Best
        End If

        If Form1.OpensimIsRunning Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If

        If Form1.MySetting.ApacheEnable Then
            MapYStart.Text = Form1.MySetting.MapCenterY
            MapXStart.Text = Form1.MySetting.MapCenterX
            MapYStart.Enabled = True
            MapXStart.Enabled = True
            LargeMapButton.Enabled = True
            SmallMapButton.Enabled = True
        Else
            MapYStart.Enabled = False
            MapXStart.Enabled = False
            LargeMapButton.Enabled = False
            SmallMapButton.Enabled = False
        End If


        Form1.HelpOnce("Maps")
        SetScreen()

    End Sub

    Private Sub MapHelp_Click(sender As Object, e As EventArgs) Handles MapHelp.Click

        Form1.Help("Maps")

    End Sub

    Private Sub MapNone_CheckedChanged(sender As Object, e As EventArgs) Handles MapNone.CheckedChanged

        Form1.MySetting.MapType = "None"
        Form1.MySetting.SaveSettings()
        MapPicture.Image = My.Resources.blankbox

    End Sub
    Private Sub MapSimple_CheckedChanged(sender As Object, e As EventArgs) Handles MapSimple.CheckedChanged

        Form1.MySetting.MapType = "Simple"
        Form1.MySetting.SaveSettings()
        MapPicture.Image = My.Resources.Simple

    End Sub

    Private Sub MapGood_CheckedChanged(sender As Object, e As EventArgs) Handles MapGood.CheckedChanged

        Form1.MySetting.MapType = "Good"
        Form1.MySetting.SaveSettings()
        MapPicture.Image = My.Resources.Good

    End Sub

    Private Sub MapBetter_CheckedChanged(sender As Object, e As EventArgs) Handles MapBetter.CheckedChanged

        Form1.MySetting.MapType = "Better"
        Form1.MySetting.SaveSettings()
        MapPicture.Image = My.Resources.Better

    End Sub

    Private Sub MapBest_CheckedChanged(sender As Object, e As EventArgs) Handles MapBest.CheckedChanged

        Form1.MySetting.MapType = "Best"
        Form1.MySetting.SaveSettings()
        MapPicture.Image = My.Resources.Best

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ViewMap.Click

        Try
            Form1.Print("Clearing Maptiles")
            My.Computer.FileSystem.DeleteDirectory(Form1.GOpensimBinPath & "bin\Maptiles\00000000-0000-0000-0000-000000000000", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.CreateDirectory(Form1.GOpensimBinPath & "bin\Maptiles\00000000-0000-0000-0000-000000000000")
        Catch ex As Exception
            Form1.Log("Warn", ex.Message)
            Return
        End Try
        Form1.Print("Maptiles cleared. Set maps on and reboot to make new maps")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim webAddress As String = "http://" + Form1.MySetting.PublicIP + ":" + Form1.MySetting.HttpPort & "/wifi/map.html"
        Process.Start(webAddress)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles SmallMapButton.Click
        Dim webAddress As String = "http://" + Form1.MySetting.PublicIP & ":" & Form1.MySetting.ApachePort & "/Metromap/index.php"
        Process.Start(webAddress)
    End Sub

    Private Sub LargeMapButton_Click(sender As Object, e As EventArgs) Handles LargeMapButton.Click
        Dim webAddress As String = "http://" + Form1.MySetting.PublicIP & ":" & Form1.MySetting.ApachePort & "/Metromap/indexmax.php"
        Process.Start(webAddress)
    End Sub

    Private Sub MapXStart_TextChanged(sender As Object, e As EventArgs) Handles MapXStart.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        MapXStart.Text = digitsOnly.Replace(MapXStart.Text, "")
        Form1.MySetting.MapCenterX = MapXStart.Text
    End Sub

    Private Sub MapYStart_TextChanged(sender As Object, e As EventArgs) Handles MapYStart.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        MapYStart.Text = digitsOnly.Replace(MapYStart.Text, "")
        Form1.MySetting.MapCenterY = MapYStart.Text
    End Sub
End Class