

Imports Outworldz

Public Class AdvancedForm

#Region "Declarations"

    Dim Tos As New TosForm
    Dim Maps As New FormMaps
    Dim Gloebits As New Gloebits
    Dim Icecast As New Icecast
    Dim Voice As New FormVoice
    Dim Bird As New BirdForm
    Dim Tide As New Tides

    Dim Backups As New FormBackups
    Dim FormRegions As New FormRegions
    Dim FormDiva As New FormDiva
    Dim FormPhysics As New FormPhysics
    Dim FormDatabase As New FormDatabase
    Dim FormRestart As New FormRestart
    Dim FormPorts As New FormPorts
    Dim FormPermissions As New FormPermissions
    Dim FormDNSName As New FormDNSName

    Dim FormPublicity As New FormPublicity

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
        ScreenPosition = New ScreenPos(Me.Name)
        AddHandler ResizeEnd, Handler
        Dim xy As List(Of Integer) = ScreenPosition.GetXY()
        Me.Left = xy.Item(0)
        Me.Top = xy.Item(1)
    End Sub

#End Region

    Private Sub Advanced_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()
        Me.Visible = True
        Form1.ViewedSettings = True

    End Sub

#Region "Help"

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs)

        Dim webAddress As String = Form1.GDomain + "/Outworldz_installer/technical.htm#Regions"
        Process.Start(webAddress)

    End Sub


#End Region

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Try
            Me.Visible = False
        Catch
        End Try

    End Sub


#Region "Clicks"

    Private Sub VoiceButton1_Click(sender As Object, e As EventArgs) Handles VoiceButton1.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Voice.Close()
        Voice = New FormVoice
        Voice.Activate()
        Voice.Visible = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles GloebitsButton.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Gloebits.Close()
        Gloebits = New Gloebits
        Gloebits.Activate()
        Gloebits.Visible = True

    End Sub


    Private Sub Shoutcast_Click(sender As Object, e As EventArgs) Handles Shoutcast.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Icecast.Close()
        Icecast = New Icecast With {
            .Visible = True
        }
        Icecast.Activate()

    End Sub

    Private Sub TOSButton_Click(sender As Object, e As EventArgs) Handles TOSButton.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Tos.Close()
        Tos = New TosForm
        Tos.Activate()
        Tos.Visible = True

    End Sub

    Private Sub Birds_Click(sender As Object, e As EventArgs) Handles Birds.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Bird.Close()
        Bird = New BirdForm
        Bird.Activate()
        Bird.Visible = True

    End Sub

    Private Sub TideButton_Click(sender As Object, e As EventArgs) Handles TideButton.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Tide.Close()
        Tide = New Tides
        Tide.Activate()
        Tide.Visible = True

    End Sub

    Private Sub MapsButton_Click(sender As Object, e As EventArgs) Handles MapsButton.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Maps.Close()
        Maps = New FormMaps
        Maps.Activate()
        Maps.Visible = True

    End Sub

    Private Sub BackupButton1_Click(sender As Object, e As EventArgs) Handles BackupButton1.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Backups.Close()
        Backups = New FormBackups
        Backups.Activate()
        Backups.Visible = True

    End Sub

    Private Sub RegionsButton1_Click(sender As Object, e As EventArgs) Handles RegionsButton1.Click

        FormRegions.Close()
        FormRegions = New FormRegions
        FormRegions.Activate()
        FormRegions.Visible = True

    End Sub
    Private Sub DivaButton1_Click(sender As Object, e As EventArgs) Handles DivaButton1.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormDiva.Close()
        FormDiva = New FormDiva
        FormDiva.Activate()
        FormDiva.Visible = True

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles PortsButton1.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormPorts.Close()
        FormPorts = New FormPorts
        FormPorts.Activate()
        FormPorts.Visible = True

    End Sub

    Private Sub PhysicsButton1_Click(sender As Object, e As EventArgs) Handles PhysicsButton1.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormPhysics.Close()
        FormPhysics = New FormPhysics
        FormPhysics.Activate()
        FormPhysics.Visible = True

    End Sub

    Private Sub DatabaseButton2_Click(sender As Object, e As EventArgs) Handles DatabaseButton2.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormDatabase.Close()
        FormDatabase = New FormDatabase
        FormDatabase.Activate()
        FormDatabase.Visible = True

    End Sub

    Private Sub DNSButton_Click(sender As Object, e As EventArgs) Handles DNSButton.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormDNSName.Close()
        FormDNSName = New FormDNSName
        FormDNSName.Activate()
        FormDNSName.Visible = True

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormRestart.Close()
        FormRestart = New FormRestart
        FormRestart.Activate()
        FormRestart.Visible = True

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormPermissions.Close()
        FormPermissions = New FormPermissions
        FormPermissions.Activate()
        FormPermissions.Visible = True

    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        FormPublicity.Close()
        FormPublicity = New FormPublicity
        FormPublicity.Activate()
        FormPublicity.Visible = True

    End Sub

    Private Sub CacheButton1_Click(sender As Object, e As EventArgs) Handles CacheButton1.Click
        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        Form1.FormCaches.Close()
        Form1.FormCaches = New FormCaches
        Form1.FormCaches.Activate()
        Form1.FormCaches.Visible = True

    End Sub




#End Region


End Class
