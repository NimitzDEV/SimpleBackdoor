Public Class frmAuth
    Public regIDs As Integer
    Dim loopPin As Integer
    Dim decLine As String
    Private Sub tbIDinput_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbIDinput.TextChanged
        If tbIDinput.Text = "" Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        FileOpen(1, Application.StartupPath & "\aid.txt", OpenMode.Output)
        Print(1, (Strings.Right((WebBrowser1.DocumentText), Len(WebBrowser1.DocumentText) - 1)))
        FileClose(1)
        If WebBrowser1.DocumentText.Contains("authorize_user") Then
            authID()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        WebBrowser1.Navigate("http://nimitzdev.free3v.net/authorize.html")

    End Sub

    Private Sub frmAuth_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Settings.authID <> "NULL" Then
            Form1.Show()
            Me.Close()
        End If
        loopPin = 0
    End Sub

    Private Sub authID()
        FileOpen(1, Application.StartupPath & "\aid.txt", OpenMode.Input)
        Do Until EOF(1)
            decLine = LineInput(1)
            loopPin += 1
            If loopPin = 2 Then
                regIDs = Int(Split(decLine, "=")(1))
            End If
        Loop
        FileClose(1)
        Debug.Print(regIDs)
    End Sub
End Class