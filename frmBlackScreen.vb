Public Class frmBlackScreen
    Public STicker As Long
    Private Sub frmBlackScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        STicker = bsTimeout
        Label1.Text = bsChar
        Me.Left = 0
        Me.Top = 0
        Me.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height
        Label1.Left = (Me.Width - Label1.Width) / 2
        Label1.Top = (Me.Height - Label1.Height) / 2
        Label2.Top = Label1.Top - Label1.Height - 50
        Label2.Left = (Me.Width - Label2.Width) / 2
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        STicker -= 1
        If STicker = 0 Then
            Timer1.Enabled = False
            Me.Close()
        End If
        Label2.Text = "让我们来数一数 -> " & STicker
        Label2.Left = (Me.Width - Label2.Width) / 2
    End Sub
End Class