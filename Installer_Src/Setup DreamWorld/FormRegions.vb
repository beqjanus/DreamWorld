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

Option Explicit On

Imports System.Text.RegularExpressions

Public Class FormRegions

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
        'Me.Text = "Form screen position = " & Me.Location.ToString
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

    Private Sub AddRegion_Click(sender As Object, e As EventArgs) Handles AddRegion.Click

        Form1.PropRegionClass.CreateRegion("")
#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
        RegionForm.Init("")
        RegionForm.Activate()
        RegionForm.Visible = True
        RegionForm.Select()
        RegionForm.BringToFront()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles NormalizeButton1.Click
        Dim result As MsgBoxResult = MsgBox(My.Resources.This_Moves, vbYesNo)
        If result = vbYes Then

            Dim chosen = Form1.ChooseRegion(False) ' all regions, running or not

            ' Check for illegal stuff
            Dim RegionUUID As String = Form1.PropRegionClass.FindRegionByName(chosen)
            Dim X = Form1.PropRegionClass.CoordX(RegionUUID)
            Dim Y = Form1.PropRegionClass.CoordY(RegionUUID)
            Dim Err As Boolean = False
            Dim Failed As String
            Dim DeltaX = 1000 - X
            Dim DeltaY = 1000 - Y
            For Each UUID As String In Form1.PropRegionClass.RegionUUIDs
                If (Form1.PropRegionClass.CoordX(UUID) + DeltaX) <= 0 Then
                    Err = True
                    Failed = Form1.PropRegionClass.RegionName(UUID)
                End If
                If (Form1.PropRegionClass.CoordY(RegionUUID) + DeltaY) <= 0 Then
                    Err = True
                    Failed = Form1.PropRegionClass.RegionName(UUID)
                End If
            Next

            If (Err) Then
                MsgBox(My.Resources.Cannot_Normalize)
                Return
            End If

            For Each UUID As String In Form1.PropRegionClass.RegionUUIDs
                Form1.PropRegionClass.CoordX(RegionUUID) = Form1.PropRegionClass.CoordX(UUID) + DeltaX
                Form1.PropRegionClass.CoordY(RegionUUID) = Form1.PropRegionClass.CoordY(UUID) + DeltaY
                Form1.PropRegionClass.WriteRegionObject(Form1.PropRegionClass.RegionName(UUID))
            Next

        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Form1.StartMySQL()
        MysqlInterface.DeregisterRegions()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles SmartStartEnabled.CheckedChanged
        Settings.SmartStart = SmartStartEnabled.Checked
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WelcomeBox1.SelectedIndexChanged

        Dim value As String = TryCast(WelcomeBox1.SelectedItem, String)
        Settings.WelcomeRegion = value

        Debug.Print("Selected " & value)

    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Regions")
    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        Settings.SaveSettings()

    End Sub

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        '!!!remove for production
        If Debugger.IsAttached = False Then
            SmartStartEnabled.Enabled = False
        End If

        LoadWelcomeBox()
        LoadRegionBox()

        X.Text = Settings.HomeVectorX
        Y.Text = Settings.HomeVectorY
        Z.Text = Settings.HomeVectorZ

        SmartStartEnabled.Checked = Settings.SmartStart

        Form1.HelpOnce("Regions")
        SetScreen()

    End Sub

    Private Sub LoadRegionBox()
        ' All region load
        RegionBox.Items.Clear()

        For Each RegionUUID As String In Form1.PropRegionClass.RegionUUIDs
            RegionBox.Items.Add(Form1.PropRegionClass.RegionName(RegionUUID))
        Next

    End Sub

    Private Sub LoadWelcomeBox()

        ' Default welcome region load
        WelcomeBox1.Items.Clear()

        For Each RegionUUID As String In Form1.PropRegionClass.RegionUUIDs
            WelcomeBox1.Items.Add(Form1.PropRegionClass.RegionName(RegionUUID))
        Next

        Dim s = WelcomeBox1.FindString(Settings.WelcomeRegion)
        If s > -1 Then
            WelcomeBox1.SelectedIndex = s
        Else
            MsgBox(My.Resources.Choose_Welcome, vbInformation, My.Resources.Choose_Region_word)
            Dim chosen = Form1.ChooseRegion(False)
            Dim Index = WelcomeBox1.FindString(chosen)
            WelcomeBox1.SelectedIndex = Index
        End If

    End Sub

    Private Sub RegionBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RegionBox.SelectedIndexChanged

        Dim value As String = TryCast(RegionBox.SelectedItem, String)
#Disable Warning CA2000 ' Dispose objects before losing scope
        Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
        RegionForm.Init(value)
        RegionForm.Activate()
        RegionForm.Visible = True
        RegionForm.Select()
        RegionForm.BringToFront()

    End Sub

    Private Sub RegionButton1_Click(sender As Object, e As EventArgs) Handles RegionButton.Click

        Dim X As Integer = 300
        Dim Y As Integer = 200
        Dim counter As Integer = 0

        For Each RegionUUID As String In Form1.PropRegionClass.RegionUUIDs

            Dim RegionName = Form1.PropRegionClass.RegionName(RegionUUID)
#Disable Warning CA2000 ' Dispose objects before losing scope
            Dim RegionForm As New FormRegion
#Enable Warning CA2000 ' Dispose objects before losing scope
            RegionForm.Init(RegionName)
            RegionForm.Activate()
            RegionForm.Visible = True
            RegionForm.Select()
            RegionForm.BringToFront()

            Application.DoEvents()
            counter += 1
            Y += 100
            X += 100

        Next

    End Sub

    Private Sub RegionHelp_Click(sender As Object, e As EventArgs) Handles RegionHelp.Click
        Form1.Help("Regions")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles X.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        X.Text = digitsOnly.Replace(X.Text, "")
        Settings.HomeVectorX = X.Text
    End Sub

    Private Sub Y_TextChanged(sender As Object, e As EventArgs) Handles Y.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        Y.Text = digitsOnly.Replace(Y.Text, "")
        Settings.HomeVectorY = Y.Text
    End Sub

    Private Sub Z_TextChanged(sender As Object, e As EventArgs) Handles Z.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        Z.Text = digitsOnly.Replace(Z.Text, "")
        Settings.HomeVectorZ = Z.Text
    End Sub

#End Region

End Class
