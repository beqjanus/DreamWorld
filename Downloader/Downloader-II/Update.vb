Imports System.Net
Imports System.IO

' AGPL 3.0 Copyright 2019 Fred Beckhusen
' Redistribution and use in binary and source form is permitted provided
' that ALL the licenses in the text files are followed and included in all copies

Public Class Update

#Region "Private Fields"

    Dim MyFolder = Nothing

#End Region

    ' Holds the current folder that we are running in

#Region "Private Methods"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        Me.Text = "DreamGrid Downloader"
        MyFolder = My.Application.Info.DirectoryPath
        Label1.Text = ""
        If Debugger.IsAttached = True Then
            ' for debugging when compiling

            MyFolder = MyFolder.Replace("\Downloader\Downloader-II\bin\Debug", "")
            MyFolder = MyFolder.Replace("\Downloader\Downloader-II\bin\Release", "")
            ' for testing, as the compiler buries itself in ../../../debug

        End If
        ChDir(MyFolder)

        Dim Version = ""
        Dim Filename As String = ""
        Dim args() As String = System.Environment.GetCommandLineArgs()
        If args.Length = 2 Then
            Filename = args(1)
        End If
        If Filename.StartsWith("DreamGrid-V") Then
            Try
                Label1.Text = "Downloading Update..."
                Application.DoEvents()
                Dim client As WebClient
                client = New WebClient()
                client.Credentials = New NetworkCredential("", "")
                Dim FName As String = "https://www.outworldz.com/Outworldz_Installer/Grid/DreamGridSetup.exe"
                client.DownloadFile(FName, "DreamGridSetup.exe")

                FName = "https://www.outworldz.com/Outworldz_Installer/Grid/DreamGrid.zip"
                client.DownloadFile(FName, Filename)
            Catch ex As Exception
                Environment.Exit(1)
            End Try

            Label1.Text = "Download Complete"

            Environment.Exit(0)
        Else
            MsgBox("Syntax: Downloader DreamGrid-Vn.n.zip")
        End If

        Environment.Exit(1)

    End Sub

#End Region

End Class
