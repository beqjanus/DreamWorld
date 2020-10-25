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

Imports System.Net
Imports System.Text.RegularExpressions

Public Class FormDNSName

#Region "Private Fields"

    Dim initted As Boolean

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
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

#Region "Load"

    Private Sub DNS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetScreen()
        Me.Text = Global.Outworldz.My.Resources.Dynamic_DNS_word
        DNSNameBox.Text = Settings.DNSName
        UniqueId.Text = Settings.MachineID()
        EnableHypergrid.Checked = Settings.EnableHypergrid
        SuitcaseCheckbox.Checked = Settings.Suitcase
        NextNameButton.Enabled = True
        DNSAliasTextBox.Text = Settings.AltDnsName
        Form1.HelpOnce("DNS")
        initted = True
    End Sub

#End Region

#Region "Buttons"

    Private Sub EnableHypergrid_CheckedChanged(sender As Object, e As EventArgs) Handles EnableHypergrid.CheckedChanged

        If Not initted Then Return
        Settings.EnableHypergrid = EnableHypergrid.Checked

    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        Form1.Help("DNS")
    End Sub

    Private Sub NextNameButton_Click(sender As Object, e As EventArgs) Handles NextNameButton.Click

        NextNameButton.Text = Global.Outworldz.My.Resources.Busy_word
        DNSNameBox.Text = String.Empty
        Application.DoEvents()
        Dim newname = Form1.GetNewDnsName()
        NextNameButton.Text = Global.Outworldz.My.Resources.Next1
        If newname.Length = 0 Then
            MsgBox(My.Resources.Please_enter, vbInformation, Global.Outworldz.My.Resources.Info_word)
            NextNameButton.Enabled = False
        Else
            NextNameButton.Enabled = True
            DNSNameBox.Text = newname
        End If

    End Sub

    Private Sub SaveAll()

        NextNameButton.Text = Global.Outworldz.My.Resources.Saving_word
        Dim address As System.Net.IPAddress = Nothing

        If DNSNameBox.Text.Length = 0 Then
            Settings.PublicIP = IP()
            Settings.DNSName = ""
        Else
            Settings.DNSName = DNSNameBox.Text
            Form1.RegisterName(Settings.PublicIP, True)

            Try
                If IPAddress.TryParse(DNSNameBox.Text, address) Then
                    Settings.PublicIP = IP()
                Else
                    Dim IP = Form1.GetHostAddresses(DNSNameBox.Text)
                End If
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
        End If

        If DNSAliasTextBox.Text.Length > 0 Then

            Form1.RegisterName(DNSAliasTextBox.Text, True)
            Settings.AltDnsName = DNSAliasTextBox.Text

        End If

        Settings.SaveSettings()

        Form1.PropViewedSettings = True
        Me.Close()

    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton1.Click

        SaveAll()

    End Sub

    Private Sub SuitcaseCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles SuitcaseCheckbox.CheckedChanged

        If Not initted Then Return
        Settings.Suitcase() = SuitcaseCheckbox.Checked

        If Not SuitcaseCheckbox.Checked Then
            MsgBox(My.Resources.Disabling_Suitcase)
        End If

    End Sub

    Private Sub TestButton1_Click(sender As Object, e As EventArgs) Handles TestButton1.Click

        NextNameButton.Text = Global.Outworldz.My.Resources.Busy_word

        Dim address As System.Net.IPAddress = Nothing
        If DNSNameBox.Text.Length = 0 Then
            Settings.PublicIP = IP()
        Else
            Settings.PublicIP = DNSNameBox.Text
            Form1.RegisterName(Settings.PublicIP, True)    ' force it to register

            Try
                If IPAddress.TryParse(DNSNameBox.Text, address) Then
                    Dim IP = Form1.GetHostAddresses(DNSNameBox.Text)
                    If IP.Length = 0 Then
                        MsgBox(My.Resources.Cannot_resolve_word & " " & DNSNameBox.Text, vbInformation, Global.Outworldz.My.Resources.Error_word)
                    Else
                        MsgBox(DNSNameBox.Text + " " & Global.Outworldz.My.Resources.resolved & " " & IP, vbInformation, Global.Outworldz.My.Resources.Info_word)
                    End If
                Else
                    Dim IP = Form1.GetHostAddresses(DNSNameBox.Text)
                    If IP.Length = 0 Then
                        MsgBox(My.Resources.Cannot_resolve_word & " " & DNSNameBox.Text, vbInformation, Global.Outworldz.My.Resources.Error_word)
                    Else
                        MsgBox(DNSNameBox.Text + " " & Global.Outworldz.My.Resources.resolved & " " & IP, vbInformation, Global.Outworldz.My.Resources.Info_word)
                    End If
                End If
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try

            If DNSAliasTextBox.Text.Length Then
                Try
                    If IPAddress.TryParse(DNSAliasTextBox.Text, address) Then
                        MsgBox(DNSAliasTextBox.Text + " " & Global.Outworldz.My.Resources.resolved & " " & DNSAliasTextBox.Text, vbInformation, Global.Outworldz.My.Resources.Info_word)
                    Else
                        Dim IP = Form1.GetHostAddresses(DNSAliasTextBox.Text)
                        If IP.Length = 0 Then
                            MsgBox(My.Resources.Cannot_resolve_word & " " & DNSAliasTextBox.Text, vbInformation, Global.Outworldz.My.Resources.Error_word)
                        Else
                            MsgBox(DNSAliasTextBox.Text + " " & Global.Outworldz.My.Resources.resolved & " " & IP, vbInformation, Global.Outworldz.My.Resources.Info_word)
                        End If
                    End If
                Catch ex As Exception
                    BreakPoint.Show(ex.Message)
                End Try

            End If
        End If

        NextNameButton.Text = Global.Outworldz.My.Resources.Next1

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")>
    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles DNSNameBox.TextChanged

        If DNSNameBox.Text.Length > 0 Then

            DNSNameBox.Text = DNSNameBox.Text.Replace("http://", "")
            DNSNameBox.Text = DNSNameBox.Text.Replace("https://", "")

            DNSNameBox.Text = Regex.Replace(DNSNameBox.Text, ":\d+", "") ' no :8002 on end.

            Dim rgx As New Regex("[^a-zA-Z0-9\.\-]")
            DNSNameBox.Text = rgx.Replace(DNSNameBox.Text, "")
#Disable Warning CA1308 ' Normalize strings to uppercase
            DNSNameBox.Text = DNSNameBox.Text.ToLower(Globalization.CultureInfo.InvariantCulture)
#Enable Warning CA1308 ' Normalize strings to uppercase

        End If

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")>
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles DNSAliasTextBox.TextChanged

        If DNSAliasTextBox.Text.Length > 0 Then
            DNSAliasTextBox.Text = DNSAliasTextBox.Text.Replace("http://", "")
            DNSAliasTextBox.Text = DNSAliasTextBox.Text.Replace("https://", "")
            DNSAliasTextBox.Text = Regex.Replace(DNSAliasTextBox.Text, ":\d+", "") ' no :8002 on end.
            Dim rgx As New Regex("[^a-zA-Z0-9\.\-,]")
            DNSAliasTextBox.Text = rgx.Replace(DNSAliasTextBox.Text, "")
#Disable Warning CA1308 ' Normalize strings to uppercase
            DNSAliasTextBox.Text = DNSAliasTextBox.Text.ToLower(Globalization.CultureInfo.InvariantCulture)
#Enable Warning CA1308 ' Normalize strings to uppercase

        End If

    End Sub

    Private Sub UniqueId_TextChanged_1(sender As Object, e As EventArgs) Handles UniqueId.TextChanged

        If Not initted Then Return
        Settings.MachineID() = UniqueId.Text

    End Sub

#End Region

End Class
