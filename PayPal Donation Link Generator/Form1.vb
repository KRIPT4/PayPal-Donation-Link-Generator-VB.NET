Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "PayPal Donation Link Generator v" + Application.ProductVersion
        Me.TopMost = True ' Mantiene el Formulario Visto
        ComboBox1.SelectedIndex = 0 ' Inicia ComboBox1 en el Item 0
        Dim rute As String = Application.StartupPath + "\" + "api.gl"
        If File.Exists(rute) Then
            Form2.txtAPIKEY.Text = File.ReadAllText(rute)
        Else
            File.Create(rute)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim URLLINK As String = "" ' Variable final de URL
        If Form2.txtAPIKEY.Text = "" Then
            MessageBox.Show("Debe completar el API-KEY!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If txtNumber.Text = "" Then ' Si TExtBox3 no tiene nada se genera el link sin Item Number.
                URLLINK = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=" + txtMail.Text + "&lc=AL&item_name=" + txtItem.Text + "&amount=" + txtPrice.Text + "&currency_code=" + ComboBox1.Text + "&no_note=1&no_shipping=1&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted"
                txtURL.Text = URLLINK.Replace(",", "%2e").Replace(".", "%2e") 'Mostramos el Link en el TextBox6
                ' Shorten URL:
                txtShort.Text = Shorten(txtURL.Text)
                My.Computer.Clipboard.SetText(txtShort.Text) ' Copiamos el Link generado al ClipBoard
            Else ' De lo Contrario: se Genera el Link con el Item Number.
                URLLINK = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=" + txtMail.Text + "&lc=AL&item_name=" + txtItem.Text + "&item_number=" + txtNumber.Text + "&amount=" + txtPrice.Text + "&currency_code=" + ComboBox1.Text + "&no_note=1&no_shipping=1&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted"
                txtURL.Text = URLLINK.Replace(",", "%2e").Replace(".", "%2e") 'Mostramos el Link en el TextBox6.
                ' Shorten URL:
                txtShort.Text = Shorten(txtURL.Text)
                My.Computer.Clipboard.SetText(txtShort.Text) ' Copiamos el Link generado al ClipBoard
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtMail.TextChanged
        TextBox5.Text = txtMail.Text ' Copiamos el Mismo Contenido entre TextBoxs.
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        txtMail.Text = TextBox5.Text ' Copiamos el Mismo Contenido entre TextBoxs.
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtURL.Text = "" Then ' Si el TextBox6 no tiene contenido, se muestra un error.
            MessageBox.Show("Donation Link not Generated!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Process.Start(txtURL.Text)
            'Me.MinimizeBox = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        ' TextBox Only Numbers
        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub DonateNowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DonateNowToolStripMenuItem.Click
        Process.Start("https://goo.gl/RGjq3O")
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Form2.Show()
    End Sub

    ' Google-URL-Shortener-VB.NET 
    ' More Info :  https://github.com/KRIPT4/Google-URL-Shortener-VB.NET/
    Public Shared Function Shorten(url As String) As String
        Dim rute As String = Application.StartupPath + "\" + "api.gl"
        Dim key As String = File.ReadAllText(rute)
        Dim lastKey As String = ""
        Dim post As String = (Convert.ToString("{""longUrl"": """) & url) + """}"
        Dim shortUrl As String = url
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(Convert.ToString("https://www.googleapis.com/urlshortener/v1/url?key=") & key), HttpWebRequest)

        Try
            request.ServicePoint.Expect100Continue = False
            request.Method = "POST"
            request.ContentLength = post.Length
            request.ContentType = "application/json"
            request.Headers.Add("Cache-Control", "no-cache")

            Using requestStream As Stream = request.GetRequestStream()
                Dim postBuffer As Byte() = Encoding.ASCII.GetBytes(post)
                requestStream.Write(postBuffer, 0, postBuffer.Length)
            End Using

            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                Using responseStream As Stream = response.GetResponseStream()
                    Using responseReader As New StreamReader(responseStream)
                        Dim json As String = responseReader.ReadToEnd()
                        shortUrl = Regex.Match(json, """id"": ?""(?<id>.+)""").Groups("id").Value
                    End Using
                End Using
            End Using

        Catch ex As Exception
            ' if Google's URL Shortner is down...
            System.Diagnostics.Debug.WriteLine(ex.Message)
            System.Diagnostics.Debug.WriteLine(ex.StackTrace)
        End Try
        lastKey = shortUrl
        Return shortUrl
    End Function
End Class