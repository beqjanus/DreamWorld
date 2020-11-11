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

Imports System.IO
Imports IniParser

Public Class ScreenPos
    Implements IDisposable

#Region "Private Fields"

    ReadOnly parser As FileIniDataParser
    Dim Data As IniParser.Model.IniData
    Dim gName As String
    Dim myINI As String

#End Region

#Region "Public Constructors"

    Public Sub New(Name As String)

        If String.IsNullOrEmpty(Name) Then Return
        GName1 = Name ' save gName for this form

        parser = New FileIniDataParser()
        parser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""
        parser.Parser.Configuration.CommentString = ";" ' Opensim uses semicolons
        MyINI1 = IO.Path.Combine(Settings.CurrentDirectory, "OutworldzFiles\XYSettings.ini")

        If File.Exists(MyINI1) Then
            LoadXYIni()
        Else
            Dim contents = "[Data]" + vbCrLf
            Try
                Using outputFile As New StreamWriter(MyINI1, True)
                    outputFile.WriteLine(contents)
                End Using
            Catch ex As Exception
                BreakPoint.Show(ex.Message)
            End Try
            LoadXYIni()

        End If

    End Sub

#End Region

#Region "Public Methods"

    Public Function ColumnWidth(name As String, Optional size As Integer = 0) As Integer

        If name Is Nothing Then Return size

        If Data Is Nothing Then
            Return size
        End If

        Dim w As Integer = CType(Data("Data".ToString(Globalization.CultureInfo.CurrentCulture))(name & "_width"), Integer)
        If w = 0 Then
            Return size
        End If
        Diagnostics.Debug.Print(name & "_width" & w.ToString(Globalization.CultureInfo.CurrentCulture))
        Return w

    End Function

    Public Function Exists() As Boolean

        If Data Is Nothing Then
            Return True
        End If

        Dim Value = CType(Data("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_Initted"), Integer)
        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_Initted", "1")
        SaveFormSettings()
        If Value = 0 Then Return False
        Return True

    End Function

    Public Function GetHW() As List(Of Integer)

        If Data Is Nothing Then
            Dim array = New List(Of Integer) From {
                100,
                100
            }
            Return array
        End If
        Dim ValueHOld = CType(Data("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_H"), Integer)
        Dim ValueWOld = CType(Data("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_W"), Integer)

        Dim r As New List(Of Integer) From {
            ValueHOld,
            ValueWOld
        }
        Debug.Print("H<" + ValueHOld.ToString(Globalization.CultureInfo.CurrentCulture))
        Debug.Print("W<" + ValueWOld.ToString(Globalization.CultureInfo.CurrentCulture))
        Return r

    End Function

    Public Function GetXY() As List(Of Integer)

        If Data Is Nothing Then
            Dim array = New List(Of Integer) From {
                100,
                100
            }
            Return array
        End If
        Try
            Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
            Dim ValueXOld = CType(Data("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_X"), Integer)
            Dim ValueYOld = CType(Data("Data".ToString(Globalization.CultureInfo.CurrentCulture))(GName1 + "_Y"), Integer)
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
            Form1.Logger("Resize", ex.Message, "Error")
            BreakPoint.Show(ex.Message)
        End Try
        Return New List(Of Integer) From {100, 100}

    End Function

    Public Sub LoadXYIni()

        Try
            Data = parser.ReadFile(MyINI1, System.Text.Encoding.UTF8)
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Diagnostics.Debug.Print("Error:" & ex.Message)
        End Try

    End Sub

    Public Sub PutSize(name As String, size As Integer)

        If name Is Nothing Then Return
        name = name.Replace("\n", "")
        name = name.Replace("\r", "")
        name = name.Replace(" ", "_")
        If name Is Nothing Then Return
        If Data Is Nothing Then
            Return
        End If

        ' Debug.Print("Saving " & name & "=" & size.ToString(Globalization.CultureInfo.InvariantCulture))

        Dim s As String = name & "_width"
        Data("Data")(s.ToString(Globalization.CultureInfo.CurrentCulture)) = size.ToString(Globalization.CultureInfo.InvariantCulture)

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId:="Outworldz.Form1.ErrorLog(System.String)")>
    Public Sub SaveFormSettings()

        If Data Is Nothing Then Return
        Try
            parser.WriteFile(MyINI1, Data, System.Text.Encoding.UTF8)
        Catch ex As Exception

            BreakPoint.Show(ex.Message)
            Form1.ErrorLog("Error:" + ex.Message)
        End Try

    End Sub

    Public Sub SaveHW(ValueH As Integer, ValueW As Integer)
        If Data Is Nothing Then
            Return
        End If
        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_H", ValueH.ToString(Globalization.CultureInfo.CurrentCulture))
        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_W", ValueW.ToString(Globalization.CultureInfo.CurrentCulture))
        SaveFormSettings()
        Debug.Print("H>" + ValueH.ToString(Globalization.CultureInfo.InvariantCulture))
        Debug.Print("W>" + ValueW.ToString(Globalization.CultureInfo.InvariantCulture))

    End Sub

    Public Sub SaveXY(ValueX As Integer, ValueY As Integer)

        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_X", ValueX.ToString(Globalization.CultureInfo.CurrentCulture))
        SetXYIni("Data".ToString(Globalization.CultureInfo.InvariantCulture), GName1 + "_Y", ValueY.ToString(Globalization.CultureInfo.CurrentCulture))
        SaveFormSettings()
        Debug.Print("X>" + ValueX.ToString(Globalization.CultureInfo.CurrentCulture))
        Debug.Print("Y>" + ValueY.ToString(Globalization.CultureInfo.CurrentCulture))

    End Sub

#End Region

#Region "Private Methods"

    Private Sub SetXYIni(section As String, key As String, value As String)

        If Data Is Nothing Then
            Return
        End If

        section = section.ToString(Globalization.CultureInfo.CurrentCulture)
        key = key.ToString(Globalization.CultureInfo.CurrentCulture)
        ' sets values into any INI file
        Try
            Data(section)(key) = value ' replace it
        Catch ex As Exception
            BreakPoint.Show(ex.Message)
            Form1.ErrorLog(ex.Message)
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

    Public Property MyINI1 As String
        Get
            Return myINI
        End Get
        Set(value As String)
            myINI = value
        End Set
    End Property

#End Region

End Class
