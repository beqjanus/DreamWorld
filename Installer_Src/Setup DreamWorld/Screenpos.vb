
Imports System.IO
Imports IniParser

Public Class ScreenPos
#Disable Warning IDE0044 ' Add readonly modifier
    Dim myINI As String
    Dim gName As String
#Enable Warning IDE0044 ' Add readonly modifier
    Dim parser As FileIniDataParser
    Dim Data As IniParser.Model.IniData


    Public Sub New(Name As String)

        gName = Name ' save gName for this form
        parser = New FileIniDataParser()
        parser.Parser.Configuration.SkipInvalidLines = True
        parser.Parser.Configuration.AssigmentSpacer = ""
        parser.Parser.Configuration.CommentString = ";" ' Opensim uses semicolons
        myINI = Form1.MyFolder + "\OutworldzFiles\XYSettings.ini"

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

    Public Sub SaveXY(ValueX As Integer, ValueY As Integer)

        SetXYIni("Data", gName + "_X", ValueX.ToString)
        SetXYIni("Data", gName + "_Y", ValueY.ToString)
        SaveFormSettings()
        Debug.Print("X>" + ValueX.ToString)
        Debug.Print("Y>" + ValueY.ToString)

    End Sub
    Public Sub SaveHW(ValueH As Integer, ValueW As Integer)

        SetXYIni("Data", gName + "_H", ValueH.ToString)
        SetXYIni("Data", gName + "_W", ValueW.ToString)
        SaveFormSettings()
        Debug.Print("H>" + ValueH.ToString)
        Debug.Print("W>" + ValueW.ToString)

    End Sub

    Public Function Exists() As Boolean
        Dim Value = CType(Data("Data")(gName + "_Initted"), Integer)
        SetXYIni("Data", gName + "_Initted", "1")
        SaveFormSettings()
        If Value = 0 Then Return False
        Return True
    End Function
    Public Function GetXY() As List(Of Integer)

        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

        Dim ValueXOld = CType(Data("Data")(gName + "_X"), Integer)
        Dim ValueYOld = CType(Data("Data")(gName + "_Y"), Integer)
        If ValueXOld <= 0 Then
            ValueXOld = 100
        End If
        If ValueXOld > screenWidth Then
            ValueXOld = screenWidth - 100
        End If
        If ValueYOld <= 0 Then
            ValueYOld = 100
        End If
        If ValueYOld > screenHeight Then
            ValueYOld = screenHeight - 100
        End If

        SaveXY(ValueXOld, ValueYOld)

        Dim r As New List(Of Integer) From {
            ValueXOld,
            ValueYOld
        }
        Debug.Print("X<" + ValueXOld.ToString)
        Debug.Print("Y<" + ValueYOld.ToString)
        Return r

    End Function
    Public Function GetHW() As List(Of Integer)

        Dim ValueHOld = CType(Data("Data")(gName + "_H"), Integer)
        Dim ValueWOld = CType(Data("Data")(gName + "_W"), Integer)

        Dim r As New List(Of Integer) From {
            ValueHOld,
            ValueWOld
        }
        Debug.Print("H<" + ValueHOld.ToString)
        Debug.Print("W<" + ValueWOld.ToString)
        Return r

    End Function
    Private Sub SetXYIni(section As String, key As String, value As String)

        ' sets values into any INI file
        Try
            Form1.Log("Info","Writing section [" + section + "] " + key + "=" + value)
            Data(section)(key) = value ' replace it 
        Catch ex As Exception
            Form1.ErrorLog(ex.Message)
        End Try

    End Sub

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
End Class
