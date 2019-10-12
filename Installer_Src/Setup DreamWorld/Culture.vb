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

Imports System.Globalization

Public Class Culture
    Private _Language As String

    Public Sub New()

        Dim culture As CultureInfo = CultureInfo.CurrentCulture
        Language = culture.EnglishName

        If Debugger.IsAttached Then
            Dim c As String = My.Application.UICulture.Name
            My.Application.ChangeUICulture("fr-FR")

            'My.Application.ChangeUICulture(c)
        End If

    End Sub

    Public Property Language As String
        Get
            Return _Language
        End Get
        Set(value As String)
            _Language = value
        End Set
    End Property

    Public Function Translate(input As String) As String

        ' reserved for MSFT translator

    End Function

End Class