#Region "Copyright AGPL3.0"

' Copyright Outworldz, LLC.
' AGPL3.0  https://opensource.org/licenses/AGPL

#End Region

Imports System.IO

Module Clear_Cache

#Region "Public Methods"

    Public Sub WipeAssets()

        TextPrint(My.Resources.Clearing_Assets)
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
            Try
                For Each folder As String In folders
                    DeleteDirectory(folder, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    ctr += 1
                    If ctr Mod 100 = 0 Then TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " folders")
                    Application.DoEvents()
                Next
            Catch ex As Exception
            End Try
        End If
        TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " folders")

        Dim files() = Nothing
        Try
            files = IO.Directory.GetFiles(Flotsam)
        Catch ex As Exception
            BreakPoint.Dump(ex)
        End Try

        ctr = 0
        If files IsNot Nothing Then
            For Each file As String In files
                DeleteFile(file)
                ctr += 1
                If ctr Mod 100 = 0 Then TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
                Application.DoEvents()
            Next
            TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
        End If

    End Sub

    Public Sub WipeBakes()

        TextPrint(My.Resources.Clearing_Bake_Cache_word)
        Dim files() = Nothing
        If Directory.Exists(Settings.OpensimBinPath & "bakes\") Then
            Try
                files = IO.Directory.GetFiles(Settings.OpensimBinPath & "j2kDecodeCache\")
            Catch ex As Exception
                BreakPoint.Dump(ex)
            End Try
        End If

        If files IsNot Nothing Then
            Dim ctr As Integer = 0
            Try
                For Each file As String In files
                    DeleteFile(file)
                    ctr += 1
                    If ctr Mod 100 = 0 Then TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
                    Application.DoEvents()
                Next
            Catch ex As Exception
            End Try

            TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
        End If

    End Sub

    Public Sub WipeImage()

        TextPrint(My.Resources.Clearing_Image_Cache_word)
        Dim files() = Nothing
        If Directory.Exists(Settings.OpensimBinPath & "j2kDecodeCache\") Then
            Try
                files = IO.Directory.GetFiles(Settings.OpensimBinPath & "j2kDecodeCache\")
            Catch ex As Exception
                BreakPoint.Dump(ex)
            End Try
        End If

        If files IsNot Nothing Then
            Dim ctr As Integer = 0
            For Each file As String In files
                DeleteFile(file)
                ctr += 1
                If ctr Mod 100 = 0 Then TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
                Application.DoEvents()
            Next
            TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
        End If

    End Sub

    Public Sub WipeMesh()

        TextPrint(My.Resources.Clearing_Mesh_Cache_word)

        Dim files() = Nothing
        If Directory.Exists(Settings.OpensimBinPath & "MeshCache\") Then
            Try
                files = IO.Directory.GetFiles(Settings.OpensimBinPath & "j2kDecodeCache\")
            Catch ex As Exception
                BreakPoint.Dump(ex)
            End Try
        End If

        Dim ctr As Integer = 0
        If files IsNot Nothing Then
            Try
                For Each file As String In files
                    DeleteFile(file)
                    ctr += 1
                    If ctr Mod 100 = 0 Then TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
                    Application.DoEvents()
                Next
            Catch
            End Try
        End If
        TextPrint(My.Resources.Deleted_word & " " & CStr(ctr) & " files")
    End Sub

    Public Sub WipeScripts(now As Boolean)

        If Not PropOpensimIsRunning() Or now Then
            Dim ctr As Integer = 0
            Dim folders() = Nothing
            Dim src As String = Path.Combine(Settings.OpensimBinPath, "ScriptEngines")
            TextPrint(My.Resources.Clearing_Script)
            If Directory.Exists(src) Then
                Try
                    folders = IO.Directory.GetFiles(src, "*", SearchOption.AllDirectories)
                    If folders IsNot Nothing Then
                        For Each script As String In folders
                            Dim ext = Path.GetExtension(script)
                            If ext.ToUpper(Globalization.CultureInfo.InvariantCulture) <> ".STATE" And ext.ToUpper(Globalization.CultureInfo.InvariantCulture) <> ".KEEP" Then
                                DeleteFile(script)
                                ctr += 1
                                If ctr Mod 100 = 0 Then TextPrint(My.Resources.Updated_word & " " & CStr(ctr) & " scripts")
                                Application.DoEvents()
                            End If
                        Next
                    End If
                Catch ex As Exception
                End Try

            End If
            TextPrint(My.Resources.Updated_word & " " & CStr(ctr) & " scripts")
        End If

    End Sub

#End Region

End Module
