Module GlobalSettings

    Private _mySetting As New MySettings
    Private _regionClass As RegionMaker

    Public Property PropRegionClass As RegionMaker
        Get
            Return _regionClass
        End Get
        Set(value As RegionMaker)
            _regionClass = value
        End Set
    End Property

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
