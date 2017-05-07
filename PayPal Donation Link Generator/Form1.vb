Imports System
Imports System.Deployment.Application

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "PayPal Donation Link Generator v" + Application.ProductVersion
        Me.TopMost = True ' Mantiene el Formulario Visto
        ComboBox1.SelectedIndex = 0 ' Inicia ComboBox1 en el Item 0
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim URLLINK As String = "" ' Variable final de URL
        If TextBox3.Text = "" Then ' Si TExtBox3 no tiene nada se genera el link sin Item Number.
            URLLINK = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=" + TextBox1.Text + "&lc=AL&item_name=" + TextBox2.Text + "&amount=" + TextBox4.Text + "&currency_code=" + ComboBox1.Text + "&no_note=1&no_shipping=1&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted"
            TextBox6.Text = URLLINK.Replace(",", "%2e").Replace(".", "%2e") 'Mostramos el Link en el TextBox6
            My.Computer.Clipboard.SetText(TextBox6.Text) ' Copiamos el Link generado al ClipBoard
        Else ' De lo Contrario: se Genera el Link con el Item Number.
            URLLINK = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=" + TextBox1.Text + "&lc=AL&item_name=" + TextBox2.Text + "&item_number=" + TextBox3.Text + "&amount=" + TextBox4.Text + "&currency_code=" + ComboBox1.Text + "&no_note=1&no_shipping=1&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted"
            TextBox6.Text = URLLINK.Replace(",", "%2e").Replace(".", "%2e") 'Mostramos el Link en el TextBox6.
            My.Computer.Clipboard.SetText(TextBox6.Text) ' Copiamos el Link generado al ClipBoard
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox5.Text = TextBox1.Text ' Copiamos el Mismo Contenido entre TextBoxs.
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        TextBox1.Text = TextBox5.Text ' Copiamos el Mismo Contenido entre TextBoxs.
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox6.Text = "" Then ' Si el TextBox6 no tiene contenido, se muestra un error.
            MessageBox.Show("Donation Link not Generated!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Process.Start(TextBox6.Text)
            'Me.MinimizeBox = True
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        ' TextBox Only Numbers
        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub DONATEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DONATEToolStripMenuItem.Click
        Process.Start("https://goo.gl/RGjq3O")
    End Sub
End Class