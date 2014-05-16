Imports System.Runtime.InteropServices
Module mdlProgram
    '--------------for bs---
    Public bsChar As String
    Public bsTimeout As Long
    '--------------
    Public incrementalData As Double
    Public targetData As String
    Public cmdName As String
    Public cmdReference As String
    Public sdkVersion As Integer
    '----------Volume Control APIs & Values----------------
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function
    Const WM_APPCOMMAND As UInteger = &H319
    Const APPCOMMAND_VOLUME_UP As UInteger = &HA
    Const APPCOMMAND_VOLUME_DOWN As UInteger = &H9
    Const APPCOMMAND_VOLUME_MUTE As UInteger = &H8
    '---------------------------------------------------------------
    Private Declare Function ExitWindowsEx Lib "user32" (ByVal uFlags As Integer, ByVal dwReserved As Integer) As Integer
    Public Const EWX_FORCE As Short = 4 '强制执行标志，暂不使用
    Public Const EWX_LOGOFF As Short = 0 '注销标志
    Public Const EWX_REBOOT As Short = 2 '重启标志
    Public Const EWX_SHUTDOWN As Short = 1 '关机标志
    Public Function pwmComputer(ByVal actionVal As Short) As Integer
        Return ExitWindowsEx(actionVal, 0)
    End Function
    Public Sub updateCmd(ByVal cmdStr As String)
        If cmdStr = "" Then Exit Sub
        incrementalData = Split(Split(cmdStr, "#")(1), "@")(1)
        targetData = Split(Split(cmdStr, "#")(2), "@")(1)
        cmdName = Split(Split(cmdStr, "#")(3), "@")(1)
        cmdReference = Split(Split(cmdStr, "#")(4), "@")(1)
        sdkVersion = Split(Split(cmdStr, "#")(5), "@")(1)
        Debug.Print(incrementalData)
        Debug.Print(targetData)
        Debug.Print(cmdName)
        Debug.Print(cmdReference)
        Debug.Print(sdkVersion)
        If My.Settings.incremental < incrementalData Then
            My.Settings.incremental = incrementalData
            My.Settings.Save()
            executeCmd()
        Else
            Debug.Print("LOCAL:" & My.Settings.incremental & "/ SERVER:" & incrementalData)
        End If
    End Sub

    Public Sub executeCmd()
        On Error Resume Next
        Debug.Print(incrementalData)
        If targetData <> My.Settings.onevent And targetData <> "ALL" Then
            Exit Sub
        End If
        Select Case cmdName
            '-----------volume
            Case "volumedec"
                For tmp = 0 To cmdReference - 1
                    SendMessage(Form1.Handle, WM_APPCOMMAND, &H30292, APPCOMMAND_VOLUME_DOWN * &H10000)
                Next
            Case "volumeinc"
                For tmp = 0 To cmdReference - 1
                    SendMessage(Form1.Handle, WM_APPCOMMAND, &H30292, APPCOMMAND_VOLUME_UP * &H10000)
                Next
            Case "volumemute"
                SendMessage(Form1.Handle, WM_APPCOMMAND, &H200EB0, APPCOMMAND_VOLUME_MUTE * &H10000)
                '--------------screen
            Case "closedesktop"
                Shell("taskkill /f /im explorer.exe")
            Case "opendesktop"
                Shell("explorer.exe")
            Case "openshell"
                Process.Start(cmdReference)
            Case "blackscreen"
                bsChar = Split(cmdReference, "^")(0)
                bsTimeout = Int(Split(cmdReference, "^")(1))
                frmBlackScreen.Close()
                frmBlackScreen.Show()
                '---------------power
            Case "shutdown"
                pwmComputer(EWX_SHUTDOWN)
            Case "logoff"
                pwmComputer(EWX_LOGOFF)
            Case "restart"
                pwmComputer(EWX_REBOOT)
                '-------------pm
            Case "reset"
            Case "killprocess"
                Shell("taskkill /f /im " & cmdReference)
            Case "cleanstatus"
                '-----------voice
            Case "speak"
            Case "showonscreen"
                '----------update
            Case "update"
        End Select
        Debug.Print("]]]]")
    End Sub
End Module
