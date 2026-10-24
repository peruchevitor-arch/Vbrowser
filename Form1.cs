
using Microsoft.Web.WebView2.WinForms;

namespace VBrowser;

public partial class Form1 : Form
{
    private WebView2 navegador = new WebView2();

    public Form1()
    {
        InitializeComponent();

        // Janela nativa do Windows
        FormBorderStyle = FormBorderStyle.Sizable;
        MinimumSize = new Size(800, 500);
        Text = "VBrowser";
        StartPosition = FormStartPosition.CenterScreen;

        // Barra de ferramentas
        var barraFerramentas = new FlowLayoutPanel();
        barraFerramentas.Dock = DockStyle.Top;
        barraFerramentas.Height = 40;
        barraFerramentas.BackColor = Color.FromArgb(35, 35, 35);
        barraFerramentas.Padding = new Padding(5);
        barraFerramentas.WrapContents = false;

        // Barra de endereço
        var barraEndereco = new TextBox();
        barraEndereco.Width = 500;
        barraEndereco.PlaceholderText = "Digite um endereço...";

        barraEndereco.MouseClick += (sender, e) =>
        {
            barraEndereco.SelectAll();
        };

        barraEndereco.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                string endereco = barraEndereco.Text;

                if (!endereco.StartsWith("http://") &&
                    !endereco.StartsWith("https://"))
                {
                    if (endereco.Contains("."))
                    {
                        endereco = "https://" + endereco;
                    }
                    else
                    {
                        endereco = "https://www.google.com/search?q=" + endereco;
                    }
                }

                navegador.Source = new Uri(endereco);
                e.SuppressKeyPress = true;
            }
        };

        // Botão voltar
        var backButton = new Button();
        backButton.Text = "<";
        backButton.Width = 40;

        backButton.Click += (sender, e) =>
        {
            if (navegador.CanGoBack)
            {
                navegador.GoBack();
            }
        };

        // Botão avançar
        var frontButton = new Button();
        frontButton.Text = ">";
        frontButton.Width = 40;

        frontButton.Click += (sender, e) =>
        {
            if (navegador.CanGoForward)
            {
                navegador.GoForward();
            }
        };

        // Botão atualizar
        var resetPag = new Button();
        resetPag.Text = "↻";
        resetPag.Width = 40;

        resetPag.Click += (sender, e) =>
        {
            navegador.Reload();
        };

        // Adicionar ferramentas
        barraFerramentas.Controls.Add(backButton);
        barraFerramentas.Controls.Add(frontButton);
        barraFerramentas.Controls.Add(resetPag);
        barraFerramentas.Controls.Add(barraEndereco);

        // Navegador
        navegador.Dock = DockStyle.Fill;

        navegador.SourceChanged += (sender, e) =>
        {
            if (navegador.Source != null)
            {
                barraEndereco.Text = navegador.Source.ToString();
            }
        };

        // Montagem da interface
        Controls.Add(navegador);
        Controls.Add(barraFerramentas);

        barraFerramentas.BringToFront();

        // Página inicial
        navegador.Source = new Uri("https://www.google.com");
    }
}
