#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

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
#Disable Warning IDE0044 ' Add readonly modifier

#Region "Private Fields"

    Dim Data As IniParser.Model.IniData
    Dim gName As String
    Dim myINI As String
#Enable Warning IDE0044 ' Add readonly modifier
    Dim parser As FileIniDataParser

#End Region

#Region "Public Constructors"

    Public Sub New(Name As String)

        gName = Name ' save gName for this form
        parser = New FileIniDataParser()
        parser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""
        parser.Parser.Configuration.CommentString = ";" ' Opensim uses semicolons
        myINI = Form1.PropMyFolder + "\OutworldzFiles\XYSettings.ini"

        If File.Exists(myINI) Then
            LoadXYIni()
        Else
            Dim contents = "[Data]" + vbCrLf
            Using outputFile As New StreamWriter(myINI, True)
                outputFile.WriteLine(contents)
            End Using
            LoadXYIni()
        End If

    End Sub

#End Region

#Region "Public Methods"

    Public Function Exists() As Boolean
        Dim Value = CType(Data("Data")(gName + "_Initted"), Integer)
        SetXYIni("Data", gName + "_Initted", "1")
        SaveFormSettings()
        If Value = 0 Then Return False
        Return True
    End Function

    Public Function GetHW() As List(Of Integer)

        Dim ValueHOld = CType(Data("Data")(gName + "_H"), Integer)
        Dim ValueWOld = CType(Data("Data")(gName + "_W"), Integer)

        Dim r As New List(Of Integer) From {
            ValueHOld,
            ValueWOld
        }
        Debug.Print("H<" + ValueHOld.ToString(Form1.Invarient))
        Debug.Print("W<" + ValueWOld.ToString(Form1.Invarient))
        Return r

    End Function

    Public Function GetXY() As List(Of Integer)

        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

        Dim ValueXOld = CType(Data("Data")(gName + "_X"), Integer)
        Dim ValueYOld = CType(Data("Data")(gName + "_Y"), Integer)
        If ValueXOld <= 0 Then
            ValueXOld = 100
        End If
        'If ValueXOld > screenWidth Then
        ' ValueXOld = screenWidth - 100
        'End If
        If ValueYOld <= 0 Then
            ValueYOld = 100
        End If
        'If ValueYOld > screenHeight Then
        'ValueYOld = screenHeight - 100
        'End If

        SaveXY(ValueXOld, ValueYOld)

        Dim r As New List(Of Integer) From {
            ValueXOld,
            ValueYOld
        }
        Debug.Print("X<" + ValueXOld.ToString(Form1.Invarient))
        Debug.Print("Y<" + ValueYOld.ToString(Form1.Invarient))
        Return r

    End Function

    Public Sub LoadXYIni()

        Try
            Form1.Log("Info", "Loading " + myINI)
            Data = parser.ReadFile(myINI, System.Text.Encoding.ASCII)
        Catch ex As Exception
            Form1.ErrorLog(ex.Message)
        End Try

    End Sub

    Public Sub SaveFormSettings()

        Try
            parser.WriteFile(myINI, Data, System.Text.Encoding.ASCII)
        Catch ex As Exception
            Form1.ErrorLog("Error:" + ex.Message)
        End Try

    End Sub

    Public Sub SaveHW(ValueH As Integer, ValueW As Integer)

        SetXYIni("Data", gName + "_H", ValueH.ToString(Form1.Invarient))
        SetXYIni("Data", gName + "_W", ValueW.ToString(Form1.Invarient))
        SaveFormSettings()
        Debug.Print("H>" + ValueH.ToString(Form1.Invarient))
        Debug.Print("W>" + ValueW.ToString(Form1.Invarient))

    End Sub

    Public Sub SaveXY(ValueX As Integer, ValueY As Integer)

        SetXYIni("Data", gName + "_X", ValueX.ToString(Form1.Invarient))
        SetXYIni("Data", gName + "_Y", ValueY.ToString(Form1.Invarient))
        SaveFormSettings()
        Debug.Print("X>" + ValueX.ToString(Form1.Invarient))
        Debug.Print("Y>" + ValueY.ToString(Form1.Invarient))

    End Sub

#End Region

#Region "Private Methods"

    Private Sub SetXYIni(section As String, key As String, value As String)

        ' sets values into any INI file
        Try
            Form1.Log("Info", "Writing section [" + section + "] " + key + "=" + value)
            Data(section)(key) = value ' replace it
        Catch ex As Exception
            Form1.ErrorLog(ex.Message)
        End Try

    End Sub

#End Region

End Class
