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

Imports System.Text.RegularExpressions

Public Class FormMaps

#Region "Private Fields"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

#End Region

#Region "Public Properties"

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

#End Region

#Region "Private Methods"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ViewMap.Click

        Form1.Print(My.Resources.Clearing_Map_tiles_word)
        Dim f As String = Form1.PropOpensimBinPath & "bin\Maptiles\00000000-0000-0000-0000-000000000000"
        Try
            FileStuff.DeleteDirectory(f, FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.CreateDirectory(f)
#Disable Warning CA1031
        Catch ex As Exception
#Enable Warning CA1031
        End Try
        Form1.Print(My.Resources.Maps_Erased)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles SmallMapButton.Click
        Dim webAddress As String = "http://" & Settings.PublicIP & ":" & CStr(Settings.ApachePort) & "/Metromap/index.php"
        Try
            Process.Start(webAddress)
#Disable Warning CA1031
        Catch ex As Exception
#Enable Warning CA1031
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim X As Integer = Settings.MapCenterX
        Dim Y As Integer = Settings.MapCenterY

        Dim webAddress As String = "http://" & CStr(Settings.PublicIP) & ":" & CStr(Settings.HttpPort) & "/wifi/map.html?X=" & CStr(X) & "&Y=" & CStr(Y)

        Try
            Process.Start(webAddress)
#Disable Warning CA1031
        Catch ex As Exception
#Enable Warning CA1031
        End Try
    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click

        Form1.Help("Maps")

    End Sub

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        Form1.PropViewedSettings = True
        Settings.SaveSettings()

    End Sub

    Private Sub LargeMapButton_Click(sender As Object, e As EventArgs) Handles LargeMapButton.Click
        Dim webAddress As String = "http://" + Settings.PublicIP & ":" & Settings.ApachePort & "/Metromap/indexmax.php"
        Try
            Process.Start(webAddress)
#Disable Warning CA1031
        Catch ex As Exception
#Enable Warning CA1031
        End Try
    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load
        If Settings.MapType = "None" Then
            MapNone.Checked = True
            MapPicture.Image = My.Resources.blankbox
        ElseIf Settings.MapType = "Simple" Then
            MapSimple.Checked = True
            MapPicture.Image = My.Resources.Simple
        ElseIf Settings.MapType = "Good" Then
            MapGood.Checked = True
            MapPicture.Image = My.Resources.Good
        ElseIf Settings.MapType = "Better" Then
            MapBetter.Checked = True
            MapPicture.Image = My.Resources.Better
        ElseIf Settings.MapType = "Best" Then
            MapBest.Checked = True
            MapPicture.Image = My.Resources.Best
        End If

        If Form1.PropOpensimIsRunning Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If

        If Settings.ApacheEnable Then
            MapYStart.Text = CStr(Settings.MapCenterY)
            MapXStart.Text = CStr(Settings.MapCenterX)
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

        RenderMaxH.Text = CStr(Settings.RenderMaxHeight)
        RenderMinH.Text = CStr(Settings.RenderMinHeight)

        Form1.HelpOnce("Maps")
        SetScreen()

    End Sub

    Private Sub MapBest_CheckedChanged(sender As Object, e As EventArgs) Handles MapBest.CheckedChanged

        Settings.MapType = "Best"
        MapPicture.Image = My.Resources.Best

    End Sub

    Private Sub MapBetter_CheckedChanged(sender As Object, e As EventArgs) Handles MapBetter.CheckedChanged

        Settings.MapType = "Better"
        MapPicture.Image = My.Resources.Better

    End Sub

    Private Sub MapGood_CheckedChanged(sender As Object, e As EventArgs) Handles MapGood.CheckedChanged

        Settings.MapType = "Good"

        MapPicture.Image = My.Resources.Good

    End Sub

    Private Sub MapHelp_Click(sender As Object, e As EventArgs) Handles MapHelp.Click

        Form1.Help("Maps")

    End Sub

    Private Sub MapNone_CheckedChanged(sender As Object, e As EventArgs) Handles MapNone.CheckedChanged

        Settings.MapType = "None"
        MapPicture.Image = My.Resources.blankbox

    End Sub

    Private Sub MapSimple_CheckedChanged(sender As Object, e As EventArgs) Handles MapSimple.CheckedChanged

        Settings.MapType = "Simple"
        MapPicture.Image = My.Resources.Simple

    End Sub

    Private Sub MapXStart_TextChanged(sender As Object, e As EventArgs) Handles MapXStart.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        MapXStart.Text = digitsOnly.Replace(MapXStart.Text, "")
        Settings.MapCenterX = CInt("0" & MapXStart.Text)

    End Sub

    Private Sub MapYStart_TextChanged(sender As Object, e As EventArgs) Handles MapYStart.TextChanged

        Dim digitsOnly As Regex = New Regex("[^\d]")
        MapYStart.Text = digitsOnly.Replace(MapYStart.Text, "")
        Settings.MapCenterY = CInt("0" & MapYStart.Text)

    End Sub

    Private Sub RenderMaxH_TextChanged(sender As Object, e As EventArgs) Handles RenderMaxH.TextChanged

        Dim digitsOnly As Regex = New Regex("[^-\d]")
        RenderMaxH.Text = digitsOnly.Replace(RenderMaxH.Text, "")
        If CInt("0" & RenderMaxH.Text) <= 4096 And CInt("0" & RenderMaxH.Text) > 100 Then
            Settings.RenderMaxHeight = CInt("0" & RenderMaxH.Text)

        End If

    End Sub

    Private Sub RenderMinH_TextChanged(sender As Object, e As EventArgs) Handles RenderMinH.TextChanged
        Dim digitsOnly As Regex = New Regex("[^-\d]")
        RenderMinH.Text = digitsOnly.Replace(RenderMinH.Text, "")
        If CInt("0" & RenderMaxH.Text) >= -100 Then
            Settings.RenderMinHeight = CInt("0" & RenderMinH.Text)
        End If

    End Sub

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

#End Region

End Class
