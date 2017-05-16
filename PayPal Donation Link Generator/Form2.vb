Imports System.IO

Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True ' Mantiene el Formulario Visto
        Dim rute As String = Application.StartupPath + "\" + "api.gl"
        If File.Exists(rute) Then
            txtAPIKEY.Text = File.ReadAllText(rute)
        Else
            File.Create(rute)
            MessageBox.Show("Se a creado el archivo: " + rute, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If My.Computer.FileSystem.FileExists("api.gl") Then
            My.Computer.FileSystem.DeleteFile("api.gl")
            My.Computer.FileSystem.WriteAllText("api.gl", txtAPIKEY.Text & vbCrLf, True)
        Else
            My.Computer.FileSystem.WriteAllText("api.gl", txtAPIKEY.Text & vbCrLf, True)
        End If
    End Sub
End Class