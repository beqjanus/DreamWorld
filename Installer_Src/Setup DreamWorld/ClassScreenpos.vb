#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO
Imports IniParser
Imports IniParser.Model

Public Class ClassScreenpos
    Implements IDisposable

#Region "Private Fields"

    Private ReadOnly parser As IniParser.FileIniDataParser
    Dim gName As String

#End Region

#Region "Public Constructors"

    Public Sub New(name As String)

        If String.IsNullOrEmpty(name) Then Return

        GName1 = name ' save gName for this form

        parser = New FileIniDataParser()
        parser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""
        parser.Parser.Configuration.CommentString = ";" ' Opensim uses semicolons
        XYINI = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\XYSettings.ini")

        If File.Exists(XYINI) Then
            LoadXYIni()
        Else
            Dim Retry = 100
            While Retry > 0
                Dim contents = "[Data]" + vbCrLf
                Try
                    Using outputFile As New StreamWriter(XYINI, False)
                        outputFile.WriteLine(contents)
                        Retry = 0
                    End Using
                Catch ex As Exception
                    BreakPoint.Show(ex)
                    Sleep(100)
                    Retry -= 1
                End Try
            End While

            LoadXYIni()

        End If

    End Sub

#End Region

#Region "Public Methods"

#Disable Warning CA1822 ' Mark members as static

    Public Function ColumnWidth(name As String, Optional size As Integer = 0) As Integer
#Enable Warning CA1822 ' Mark members as static

        If name Is Nothing Then Return size

        If XYData Is Nothing Then
            Return size
        End If

        Dim w As Integer = CType(XYData("Data".ToString(Globalization.CultureInfo.CurrentCulture))(name & "_width"), Integer)
        If w = 0 Then
            Return size
        End If
        Diagnostics.Debug.Print(name & "_width" & w.ToString(Globalization.CultureInfo.CurrentCulture))
        Return w

    End Function

    Public Function Exists() As Boolean

        If XYData Is Nothing Then
            Return True
        End If

        Dim Value = CType(XYData("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_Initted"), Integer)
        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_Initted", "1")
        SaveFormSettings()
        If Value = 0 Then Return False
        Return True

    End Function

    Public Function GetHW() As List(Of Integer)

        If XYData Is Nothing Then
            Dim array = New List(Of Integer) From {
                100,
                100
            }
            Return array
        End If
        Dim ValueHOld = CType(XYData("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_H"), Integer)
        Dim ValueWOld = CType(XYData("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_W"), Integer)

        Dim r As New List(Of Integer) From {
            ValueHOld,
            ValueWOld
        }
        Debug.Print("H<" + ValueHOld.ToString(Globalization.CultureInfo.CurrentCulture))
        Debug.Print("W<" + ValueWOld.ToString(Globalization.CultureInfo.CurrentCulture))
        Return r

    End Function

    Public Function GetXY() As List(Of Integer)

        If XYData Is Nothing Then
            Dim array = New List(Of Integer) From {100, 100}
            Return array
        End If
        Try
            Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
            Dim ValueXOld = CType(XYData("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_X"), Integer)
            Dim ValueYOld = CType(XYData("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_Y"), Integer)
            If ValueXOld <= 0 Then
                ValueXOld = 100
            End If

            If ValueYOld <= 0 Then
                ValueYOld = 100
            End If

            SaveXY(ValueXOld, ValueYOld)

            Dim r As New List(Of Integer) From {
                ValueXOld,
                ValueYOld
            }

            Debug.Print("X<" + ValueXOld.ToString(Globalization.CultureInfo.CurrentCulture))
            Debug.Print("Y<" + ValueYOld.ToString(Globalization.CultureInfo.CurrentCulture))
            Return r
        Catch ex As Exception
            Logger("Resize", ex.Message, "Error")
            BreakPoint.Show(ex)
        End Try
        Return New List(Of Integer) From {100, 100}

    End Function

    Public Sub LoadXYIni()

        Dim waiting As Integer = 50 ' 5 sec
        While waiting > 0
            Try
                XYData = ReadIniFile(XYINI)
                waiting = 0
            Catch ex As Exception
                waiting -= 1
                Sleep(100)
            End Try
        End While

    End Sub

#Disable Warning CA1822 ' Mark members as static

    Public Sub PutSize(name As String, size As Integer)
#Enable Warning CA1822 ' Mark members as static

        If name Is Nothing Then Return
        name = name.Replace("\n", "")
        name = name.Replace("\r", "")
        name = name.Replace(" ", "_")
        If name Is Nothing Then Return
        If XYData Is Nothing Then
            Return
        End If

        ' Debug.Print("Saving " & name & "=" & size.ToString(Globalization.CultureInfo.InvariantCulture))

        Dim s As String = name & "_width"
        XYData("Data")(s.ToString(Globalization.CultureInfo.CurrentCulture)) = size.ToString(Globalization.CultureInfo.InvariantCulture)

    End Sub

    Public Function ReadIniFile(myIni As String) As IniData
        Dim waiting As Integer = 50 ' 5 sec
        While waiting > 0
            Try
                Dim Data As IniData = parser.ReadFile(myIni, System.Text.Encoding.UTF8)
                Return Data
            Catch ex As Exception
                waiting -= 1
                Sleep(100)
            End Try
        End While

        Return Nothing

    End Function

    Public Sub SaveFormSettings()

        Try
            parser.WriteFile(XYINI, XYData, System.Text.Encoding.UTF8)
            Return
        Catch ex As Exception
            ErrorLog("Error:" + ex.Message)
        End Try

    End Sub

    Public Sub SaveHW(valueH As Integer, valueW As Integer)
        If XYData Is Nothing Then
            Return
        End If
        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_H", valueH.ToString(Globalization.CultureInfo.CurrentCulture))
        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_W", valueW.ToString(Globalization.CultureInfo.CurrentCulture))
        SaveFormSettings()
        Debug.Print("H>" + valueH.ToString(Globalization.CultureInfo.InvariantCulture))
        Debug.Print("W>" + valueW.ToString(Globalization.CultureInfo.InvariantCulture))

    End Sub

    Public Sub SaveXY(valueX As Integer, valueY As Integer)

        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_X", valueX.ToString(Globalization.CultureInfo.CurrentCulture))
        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_Y", valueY.ToString(Globalization.CultureInfo.CurrentCulture))
        SaveFormSettings()
        Debug.Print("X>" + valueX.ToString(Globalization.CultureInfo.CurrentCulture))
        Debug.Print("Y>" + valueY.ToString(Globalization.CultureInfo.CurrentCulture))

    End Sub

#End Region

#Region "Private Methods"

    Private Sub SetXYIni(section As String, key As String, value As String)

        If XYData Is Nothing Then
            Return
        End If

        section = section.ToString(Globalization.CultureInfo.CurrentCulture)
        key = key.ToString(Globalization.CultureInfo.CurrentCulture)
        ' sets values into any INI file
        Try
            XYData(section)(key) = value ' replace it
        Catch ex As Exception
            BreakPoint.Show(ex)
            ErrorLog(ex.Message)
        End Try

    End Sub

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' To detect redundant calls

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
        End If
        disposedValue = True
    End Sub

    '
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

#End Region

#Region "Properties"

    Public Property GName1 As String
        Get
            Return gName
        End Get
        Set(value As String)
            gName = value
        End Set
    End Property

#End Region

End Class
