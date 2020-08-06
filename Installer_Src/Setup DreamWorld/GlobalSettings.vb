Module GlobalSettings

    Private _mySetting As New MySettings

    Public Property Settings As MySettings
        Get
            Return _mySetting
        End Get
        Set(value As MySettings)
            _mySetting = value
        End Set
    End Property

    Public Function RobustName() As String

        Return "Robust " & Settings.PublicIP

    End Function


End Module
