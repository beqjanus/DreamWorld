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

Public Class ClrCache

    Public Sub New()
    End Sub

    Public Shared Sub WipeScripts()

        If Not Form1.PropOpensimIsRunning() Then
            Dim folders() = Directory.GetFiles(Form1.PropOpensimBinPath & "bin\ScriptEngines\", "*", SearchOption.AllDirectories)
            Form1.Print("Clearing Script cache. This may take a long time!")
            Dim ctr As Integer = 0
            For Each script As String In folders
                Dim ext = Path.GetExtension(script)
                If ext.ToLower(Form1.Invarient) <> ".state" And ext.ToLower(Form1.Invarient) <> ".keep" Then
                    Try
                        My.Computer.FileSystem.DeleteFile(script)
                    Catch ex As Exception
                    End Try

                    ctr += 1
                    Form1.Print("Updated " & CStr(ctr) & " scripts")
                    Application.DoEvents()
                End If
            Next
        End If

    End Sub

    Public Shared Sub WipeBakes()

        Form1.Print("Clearing bake cache")
        Try
            My.Computer.FileSystem.DeleteDirectory(Form1.PropOpensimBinPath & "bin\bakes\", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Sub WipeAssets()

        Form1.Print("Clearing Asset cache. This may take a long time!")
        If Form1.PropOpensimIsRunning Then
            Form1.ConsoleCommand("Robust", "fcache clear")
            Return
        End If

        Try
            Dim folders() = Directory.GetDirectories(Form1.PropOpensimBinPath & "bin\Assetcache\", "*", SearchOption.AllDirectories)
            Dim ctr As Integer = 0
            For Each folder As String In folders
                My.Computer.FileSystem.DeleteDirectory(folder, FileIO.DeleteDirectoryOption.DeleteAllContents)
                ctr += 1
                If ctr Mod 100 = 0 Then Form1.Print("Deleted " & CStr(ctr))
                Application.DoEvents()
            Next
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Sub WipeImage()

        Try
            Form1.Print("Clearing Image cache.")
            Dim folders() = IO.Directory.GetDirectories(Form1.PropOpensimBinPath & "bin\j2kDecodeCache\")
            Dim ctr = 0
            For Each folder As String In folders
                My.Computer.FileSystem.DeleteDirectory(folder, FileIO.DeleteDirectoryOption.DeleteAllContents)
                ctr += 1
                If ctr Mod 100 = 0 Then Form1.Print("Deleted " & CStr(ctr))
                Application.DoEvents()
            Next
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Sub WipeMesh()

        Try
            Form1.Print("Clearing Mesh cache")
            Dim fCount As Integer = Directory.GetFiles(Form1.PropOpensimBinPath & "bin\MeshCache\", "*", SearchOption.AllDirectories).Length

            Dim folders() = Directory.GetFiles(Form1.PropOpensimBinPath & "bin\MeshCache\", "*", SearchOption.AllDirectories)
            Dim ctr As Integer = 0
            For Each folder As String In folders
                My.Computer.FileSystem.DeleteDirectory(Form1.PropOpensimBinPath & "bin\MeshCache\", FileIO.DeleteDirectoryOption.DeleteAllContents)
                ctr += 1
                Form1.Print(CStr(ctr) + " of " + CStr(fCount))
                Application.DoEvents()
            Next
        Catch ex As Exception
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class