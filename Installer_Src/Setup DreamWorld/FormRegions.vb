Option Explicit On

Imports System.Text.RegularExpressions
Imports Outworldz

Public Class FormRegions

    Dim PropRegionClass As RegionMaker = RegionMaker.Instance()

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

    Public Property PropRegionClass1 As RegionMaker
        Get
            Return PropRegionClass
        End Get
        Set(value As RegionMaker)
            PropRegionClass = value
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

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        LoadWelcomeBox()
        LoadRegionBox()

        X.Text = Form1.PropMySetting.HomeVectorX
        Y.Text = Form1.PropMySetting.HomeVectorY
        Z.Text = Form1.PropMySetting.HomeVectorZ

        Form1.HelpOnce("Regions")
        SetScreen()

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        Form1.PropMySetting.SaveSettings()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WelcomeBox1.SelectedIndexChanged

        Dim value As String = TryCast(WelcomeBox1.SelectedItem, String)
        Form1.PropMySetting.WelcomeRegion = value

        Debug.Print("Selected " + value)

    End Sub

    Private Sub RegionButton1_Click(sender As Object, e As EventArgs) Handles RegionButton.Click

        Dim X As Integer = 300
        Dim Y As Integer = 200
        Dim counter As Integer = 0

        For Each Z As Integer In PropRegionClass1.RegionNumbers
            Try
                Dim RegionName = PropRegionClass1.RegionName(Z)
                Dim RegionForm As New FormRegion

                RegionForm.Init(RegionName)
                RegionForm.Activate()
                RegionForm.Visible = True
                Application.DoEvents()
            Catch ex As Exception
                Form1.ErrorLog("Error:" + ex.Message)
            End Try
            counter = counter + 1
            Y += 100
            X += 100

        Next

    End Sub

    Private Sub AddRegion_Click(sender As Object, e As EventArgs) Handles AddRegion.Click

        Dim X As Integer = 300
        Dim Y As Integer = 200

        PropRegionClass1.CreateRegion("")

        Dim RegionForm As New FormRegion
        RegionForm.Init("")
        RegionForm.Activate()
        RegionForm.Visible = True

    End Sub

    Private Sub LoadWelcomeBox()

        ' Default welcome region load
        WelcomeBox1.Items.Clear()

        For Each X As Integer In PropRegionClass1.RegionNumbers
            'If PropRegionClass.RegionEnabled(X) Then
            WelcomeBox1.Items.Add(PropRegionClass1.RegionName(X))
            'End If
        Next

        Dim s = WelcomeBox1.FindString(Form1.PropMySetting.WelcomeRegion)
        If s > -1 Then
            WelcomeBox1.SelectedIndex = s
        Else
            MsgBox("Choose your Welcome region ", vbInformation, "Choose")
            Dim chosen = Form1.ChooseRegion(False)
            Dim Index = WelcomeBox1.FindString(chosen)
            WelcomeBox1.SelectedIndex = Index
        End If

    End Sub

    Private Sub LoadRegionBox()
        ' All region load
        RegionBox.Items.Clear()

        For Each X As Integer In PropRegionClass1.RegionNumbers
            RegionBox.Items.Add(PropRegionClass1.RegionName(X))
        Next

    End Sub

    Private Sub RegionBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RegionBox.SelectedIndexChanged

        Dim value As String = TryCast(RegionBox.SelectedItem, String)
        Dim RegionForm As New FormRegion
        RegionForm.Init(value)
        RegionForm.Activate()
        RegionForm.Visible = True
        RegionForm.Select()

    End Sub

    Private Sub RegionHelp_Click(sender As Object, e As EventArgs) Handles RegionHelp.Click
        Form1.Help("Regions")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles X.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        X.Text = digitsOnly.Replace(X.Text, "")
        Form1.PropMySetting.HomeVectorX = X.Text
    End Sub

    Private Sub Y_TextChanged(sender As Object, e As EventArgs) Handles Y.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        Y.Text = digitsOnly.Replace(Y.Text, "")
        Form1.PropMySetting.HomeVectorY = Y.Text
    End Sub

    Private Sub Z_TextChanged(sender As Object, e As EventArgs) Handles Z.TextChanged
        Dim digitsOnly As Regex = New Regex("[^\d]")
        Z.Text = digitsOnly.Replace(Z.Text, "")
        Form1.PropMySetting.HomeVectorZ = Z.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles NormalizeButton1.Click
        Dim result As MsgBoxResult = MsgBox("This moves all regions so the chosen region is at 1000,1000 to fit the map. Proceed?", vbYesNo)
        If result = vbYes Then

            Dim chosen = Form1.ChooseRegion(False) ' all regions, running or not

            ' Check for illegal stuff
            Dim RegionNum = PropRegionClass1.FindRegionByName(chosen)
            Dim X = PropRegionClass1.CoordX(RegionNum)
            Dim Y = PropRegionClass1.CoordY(RegionNum)
            Dim Err As Boolean = False
            Dim Failed As String = ""
            Dim DeltaX = 1000 - X
            Dim DeltaY = 1000 - Y
            For Each RegionNumber In PropRegionClass1.RegionNumbers
                If (PropRegionClass1.CoordX(RegionNumber) + DeltaX) <= 0 Then
                    Err = True
                    Failed = PropRegionClass1.RegionName(RegionNumber)
                End If
                If (PropRegionClass1.CoordY(RegionNumber) + DeltaY) <= 0 Then
                    Err = True
                    Failed = PropRegionClass1.RegionName(RegionNumber)
                End If
            Next

            If (Err) Then
                MsgBox("Cannot normalize map as a Region will be less than 0 ")
                Return
            End If

            For Each RegionNumber In PropRegionClass1.RegionNumbers
                PropRegionClass1.CoordX(RegionNumber) = PropRegionClass1.CoordX(RegionNumber) + DeltaX
                PropRegionClass1.CoordY(RegionNumber) = PropRegionClass1.CoordY(RegionNumber) + DeltaY
                PropRegionClass1.WriteRegionObject(PropRegionClass1.RegionName(RegionNumber))
            Next

        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Dim MysqlConn As New MysqlInterface(Form1.PropRobustConnStr())
        MysqlConn.DeregisterRegions()

    End Sub

    Private Sub DatabaseSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseSetupToolStripMenuItem.Click
        Form1.Help("Regions")
    End Sub
End Class