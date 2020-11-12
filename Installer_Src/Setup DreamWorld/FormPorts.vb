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

Public Class FormPorts

#Region "Private Fields"

    Dim initted As Boolean

#End Region

#Region "FormPos"

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
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Private Methods"

    Private Sub IsClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed

        FormSetup.PropViewedSettings = True
        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        Translate.Run(Name)
        SetScreen()

        FirstRegionPort.Text = CStr(Settings.FirstRegionPort())
        MaxP.Text = Global.Outworldz.My.Resources.Highest_Used_word & " " & FormSetup.PropMaxPortUsed.ToString(Globalization.CultureInfo.InvariantCulture)

        FirstXMLRegionPort.Text = Settings.FirstXMLRegionPort()

        MaxX.Text = Global.Outworldz.My.Resources.Highest_Used_word & " " & FormSetup.PropMaxXMLPortUsed.ToString(Globalization.CultureInfo.InvariantCulture)

        uPnPEnabled.Checked = Settings.UPnPEnabled

        'ports
        DiagnosticPort.Text = CStr(Settings.DiagnosticPort)
        PrivatePort.Text = CStr(Settings.PrivatePort)
        HTTPPort.Text = CStr(Settings.HttpPort)

        ' only used for region servers
        ExternalHostName.Text = Settings.OverrideName

        If Settings.ServerType <> "Robust" Then
            OverrideNameLabel.Visible = True
            ExternalHostName.Visible = True
        Else
            ExternalHostName.Visible = False
            OverrideNameLabel.Visible = False
            ExternalHostName.Text = ""
        End If

        HelpOnce("Ports")
        initted = True

    End Sub

    Private Sub UPnPEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles uPnPEnabled.CheckedChanged

        If Not initted Then Return
        Settings.UPnPEnabled = uPnPEnabled.Checked
        Settings.SaveSettings()

    End Sub

#End Region

#Region "Ports"

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        HelpManual("Ports")
    End Sub

    Private Sub DiagnosticPort_TextChanged(sender As Object, e As EventArgs) Handles DiagnosticPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        DiagnosticPort.Text = digitsOnly.Replace(DiagnosticPort.Text, "")

        Settings.DiagnosticPort = CInt("0" & DiagnosticPort.Text)
        Settings.SaveSettings()
        FormSetup.CheckDefaultPorts()

    End Sub

    Private Sub ExternalHostName_TextChanged(sender As Object, e As EventArgs) Handles ExternalHostName.TextChanged

        If Not initted Then Return

        Settings.OverrideName = ExternalHostName.Text

    End Sub

    Private Sub FirstRegionPort_TextChanged_1(sender As Object, e As EventArgs) Handles FirstRegionPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        FirstRegionPort.Text = digitsOnly.Replace(FirstRegionPort.Text, "")
        Settings.FirstRegionPort() = CInt("0" & FirstRegionPort.Text)
        Settings.SaveSettings()

        RegionMaker.UpdateAllRegionPorts()
        FirstRegionPort.Text = CStr(Settings.FirstRegionPort())
        MaxP.Text = Global.Outworldz.My.Resources.Highest_Used_word & " " & FormSetup.PropMaxPortUsed.ToString(Globalization.CultureInfo.InvariantCulture)

        FirstXMLRegionPort.Text = Settings.FirstXMLRegionPort()
        MaxX.Text = Global.Outworldz.My.Resources.Highest_Used_word & " " & FormSetup.PropMaxXMLPortUsed.ToString(Globalization.CultureInfo.InvariantCulture)

    End Sub

    Private Sub HTTPPort_TextChanged(sender As Object, e As EventArgs) Handles HTTPPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        HTTPPort.Text = digitsOnly.Replace(HTTPPort.Text, "")
        Settings.HttpPort = CInt("0" & HTTPPort.Text)
        Settings.SaveSettings()
        FormSetup.CheckDefaultPorts()

    End Sub

    Private Sub PrivatePort_TextChanged(sender As Object, e As EventArgs) Handles PrivatePort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        PrivatePort.Text = digitsOnly.Replace(PrivatePort.Text, "")
        Settings.PrivatePort = CInt("0" & PrivatePort.Text)
        Settings.SaveSettings()
        FormSetup.CheckDefaultPorts()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles FirstXMLRegionPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d ]")
        FirstXMLRegionPort.Text = digitsOnly.Replace(FirstXMLRegionPort.Text, "")
        Settings.FirstXMLRegionPort() = FirstXMLRegionPort.Text
        Settings.SaveSettings()

        RegionMaker.UpdateAllRegionPorts()
        FirstRegionPort.Text = CStr(Settings.FirstRegionPort())
        MaxP.Text = Global.Outworldz.My.Resources.Highest_Used_word & " " & FormSetup.PropMaxPortUsed.ToString(Globalization.CultureInfo.InvariantCulture)

        FirstXMLRegionPort.Text = Settings.FirstXMLRegionPort()
        MaxX.Text = Global.Outworldz.My.Resources.Highest_Used_word & " " + FormSetup.PropMaxXMLPortUsed.ToString(Globalization.CultureInfo.InvariantCulture)

    End Sub

    Private Sub Upnp_Click(sender As Object, e As EventArgs) Handles Upnp.Click

        HelpManual("Ports")

    End Sub

#End Region

End Class
