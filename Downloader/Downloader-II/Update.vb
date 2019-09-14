Imports System.Net
Imports System.IO

' AGPL 3.0 Copyright 2019 Fred Beckhusen
' Redistribution and use in binary and source form is permitted provided
' that ALL the licenses in the text files are followed and included in all copies

Public Class Update

    Dim MyFolder = Nothing   ' Holds the current folder that we are running in

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        Me.Text = "DreamGrid Downloader"
        MyFolder = My.Application.Info.DirectoryPath

        If Debugger.IsAttached = True Then
            ' for debugging when compiling

            MyFolder = MyFolder.Replace("\Downloader\Downloader-II\bin\Debug", "")
            MyFolder = MyFolder.Replace("\Downloader\Downloader-II\bin\Release", "")
            ' for testing, as the compiler buries itself in ../../../debug
            Label1.Text = ""
        End If

        Dim FName As String = "https://www.outworldz.com/Outworldz_Installer/Grid/DreamGrid.zip"

        Try
            Label1.Text = "Downloading Update..."
            Application.DoEvents()
            Dim client As WebClient
            client = New WebClient()
            client.Credentials = New NetworkCredential("", "")
            client.DownloadFile(FName, "DreamGrid.Zip")
        Catch ex As Exception
            Environment.Exit(1)
        End Try

        Label1.Text = "Download Complete"

        Environment.Exit(0)

    End Sub

End Class