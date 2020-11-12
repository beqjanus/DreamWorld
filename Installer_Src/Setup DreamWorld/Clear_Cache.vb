Imports System.IO

Module Clear_Cache

#Region "Copyright"

    ' Copyright 2014 Fred Beckhusen for outworldz.com+ https://opensource.org/licenses/AGPL

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
    'DEALINGS IN THE SOFTWARE.

#End Region

    Public Class ClrCache

#Region "Public Constructors"

        Public Sub New()
        End Sub

#End Region

#Region "Public Methods"

        Public Shared Sub WipeAssets()

            FormSetup.Print(My.Resources.Clearing_Assets)
            Dim folders() = Nothing
            Dim Flotsam As String = Settings.CacheFolder
            If Flotsam.ToUpperInvariant = ".\ASSETCACHE" Then
                Flotsam = Settings.OpensimBinPath & "assetcache"
            End If
            If Directory.Exists(Flotsam) Then
                folders = Directory.GetDirectories(Flotsam, "*", SearchOption.TopDirectoryOnly)
            End If
            Dim ctr As Integer = 0
            If folders IsNot Nothing Then

                For Each folder As String In folders

                    FileStuff.DeleteDirectory(folder, FileIO.DeleteDirectoryOption.DeleteAllContents)

                    ctr += 1
                    If ctr Mod 100 = 0 Then FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " folders")
                    Application.DoEvents()
                Next
            End If
            FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " folders")

            Dim files() = Nothing
            Try
                files = IO.Directory.GetFiles(Flotsam)

            Catch ex As Exception

                BreakPoint.Show(ex.Message)
            End Try

            ctr = 0
            If files IsNot Nothing Then
                For Each file As String In files

                    FileStuff.DeleteFile(file)
                    ctr += 1
                    If ctr Mod 100 = 0 Then FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
                    Application.DoEvents()
                Next
                FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
            End If

        End Sub

        Public Shared Sub WipeBakes()

            FormSetup.Print(My.Resources.Clearing_Bake_Cache_word)
            Dim files() = Nothing
            If Directory.Exists(Settings.OpensimBinPath & "bakes\") Then
                Try
                    files = IO.Directory.GetFiles(Settings.OpensimBinPath & "j2kDecodeCache\")

                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
            End If

            If files IsNot Nothing Then
                Dim ctr As Integer = 0
                For Each file As String In files

                    FileStuff.DeleteFile(file)
                    ctr += 1
                    If ctr Mod 100 = 0 Then FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
                    Application.DoEvents()
                Next
                FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
            End If

        End Sub

        Public Shared Sub WipeImage()

            FormSetup.Print(My.Resources.Clearing_Image_Cache_word)
            Dim files() = Nothing
            If Directory.Exists(Settings.OpensimBinPath & "j2kDecodeCache\") Then
                Try
                    files = IO.Directory.GetFiles(Settings.OpensimBinPath & "j2kDecodeCache\")

                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
            End If

            If files IsNot Nothing Then
                Dim ctr As Integer = 0
                For Each file As String In files
                    FileStuff.DeleteFile(file)
                    ctr += 1
                    If ctr Mod 100 = 0 Then FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
                    Application.DoEvents()
                Next
                FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
            End If

        End Sub

        Public Shared Sub WipeMesh()

            FormSetup.Print(My.Resources.Clearing_Mesh_Cache_word)

            Dim files() = Nothing
            If Directory.Exists(Settings.OpensimBinPath & "MeshCache\") Then
                Try
                    files = IO.Directory.GetFiles(Settings.OpensimBinPath & "j2kDecodeCache\")

                Catch ex As Exception

                    BreakPoint.Show(ex.Message)
                End Try
            End If

            Dim ctr As Integer = 0
            If files IsNot Nothing Then
                For Each file As String In files
                    FileStuff.DeleteFile(file)
                    ctr += 1
                    If ctr Mod 100 = 0 Then FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
                    Application.DoEvents()
                Next
            End If
            FormSetup.Print(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
        End Sub

        Public Shared Sub WipeScripts()

            If Not FormSetup.PropOpensimIsRunning() Then
                Dim ctr As Integer = 0
                Dim folders() = Nothing
                If Directory.Exists(Settings.OpensimBinPath & "ScriptEngines\") Then
                    folders = Directory.GetFiles(Settings.OpensimBinPath & "ScriptEngines\", "*", SearchOption.AllDirectories)
                    If folders IsNot Nothing Then
                        FormSetup.Print(My.Resources.Clearing_Script)

                        For Each script As String In folders
                            Dim ext = Path.GetExtension(script)
                            If ext.ToUpper(Globalization.CultureInfo.InvariantCulture) <> ".STATE" And ext.ToUpper(Globalization.CultureInfo.InvariantCulture) <> ".KEEP" Then
                                FileStuff.DeleteFile(script)
                                ctr += 1
                                If ctr Mod 100 = 0 Then FormSetup.Print(My.Resources.Updated_word & " " & CStr(ctr) & " scripts")

                                Application.DoEvents()
                            End If
                        Next
                    End If
                End If
                FormSetup.Print(My.Resources.Updated_word & " " & CStr(ctr) & " scripts")
            End If

        End Sub

#End Region

    End Class

End Module
