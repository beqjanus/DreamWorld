#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports IniParser
Imports IniParser.Model
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class LoadIni

    Private _encoding As System.Text.Encoding
    Private _filename As String
    Private _parser As FileIniDataParser
    Private _sep As String
    Private _SettingsData As IniParser.Model.IniData

    Public Sub New(File As String, arg As String, encoding As System.Text.Encoding)

        _filename = File
        If arg Is Nothing Then Return
        If arg.Contains("/Regions/") Then CheckINI()
        _sep = arg

        _encoding = encoding

        _parser = New FileIniDataParser()
        _parser.Parser.Configuration.SkipInvalidLines = True
        _parser.Parser.Configuration.AssigmentSpacer = ""
        _parser.Parser.Configuration.CommentString = _sep ' Opensim uses semicolons
        _SettingsData = ReadINIFile()

    End Sub

    Public Function GetIni(section As String, key As String, Value As String, Optional V As String = Nothing) As Object

        If _SettingsData Is Nothing Then
            ErrorLog($"No Settings for {section} {key}")
            Return Nothing
        End If

        Dim Variable = Stripqq(_SettingsData(section)(key))

        If Variable = Nothing Then Variable = Value
        If Variable Is Nothing Then Return Value

        Dim bool As Boolean
        If V = "Boolean" Then
            If Not Boolean.TryParse(Variable, bool) Then
                Return Variable
            End If
            Return bool
        ElseIf V = "String" Then
            Return Variable.Trim
        ElseIf V = "Double" Then
            Dim DBL As Double
            If Not Double.TryParse(Variable, DBL) Then
                Return Variable
            End If
            Return DBL
        ElseIf V = "Single" Then
            Dim SNG As Single
            If Not Single.TryParse(Variable, SNG) Then
                Return Variable
            End If
            Return SNG
        ElseIf V = "Integer" Then
            Dim I As Integer
            If Not Integer.TryParse(Variable, I) Then
                Return Variable
            End If
            Return I
        End If

        Return Variable

    End Function

    Public Sub SaveINI()

        Dim Retry As Integer = 10 ' 1 sec
        While Retry > 0
            Try
                _parser.WriteFile(_filename, _SettingsData, _encoding)
                Retry = 0
            Catch ex As Exception
                'ErrorLog("Error:" + ex.Message)
                Retry -= 1
                Thread.Sleep(100)
            End Try
        End While

    End Sub

    ''' <summary>Save to the ini the name value pair.</summary>
    ''' <param name="section"></param>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function SetIni(section As String, key As String, value As String) As Boolean

        ' sets values into any INI file Form1.Log(My.Resources.Info, "Writing section [" + section + "] " + key + "=" + value)
        Try
            _SettingsData(section)(key) = value
        Catch ex As Exception
            ErrorLog(ex.Message)
            Return True
        End Try
        Return False

    End Function

    ''' <summary>
    ''' Repair INI files with extra [sections]
    ''' </summary>
    ''' <param name="file">Path to region ini file</param>
    Private Sub CheckINI()
        Dim c As Integer
        Using Reader As New System.IO.StreamReader(_filename)
            Dim RepairedLine As String = ""
            While Not Reader.EndOfStream
                Dim line As String = Reader.ReadLine

                Dim pattern As Regex = New Regex("^\[.*?\]")
                Dim match As Match = pattern.Match(line)
                If match.Success Then
                    c += 1
                End If
                If c > 1 Then
                    FileStuff.DeleteFile(_filename)
                    Using Writer As New StreamWriter(_filename)
                        Writer.Write(RepairedLine)
                    End Using
                    Exit While
                End If
                'Debug.Print(line)
                RepairedLine += line & vbCrLf
            End While
        End Using

    End Sub

    Private Function ReadINIFile() As IniData

        Dim waiting As Integer = 10 ' 1 sec
        While waiting > 0
            Try
                Dim Data As IniData = _parser.ReadFile(_filename, _encoding)
                Return Data
            Catch ex As Exception
                waiting -= 1
                Sleep(100)
            End Try
        End While

        Return Nothing

    End Function

End Class
