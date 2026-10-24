
using Microsoft.Web.WebView2.WinForms;

namespace VBrowser;

public partial class Form1 : Form
{
    private WebView2 navegador = new WebView2();

    public Form1()
    {
        InitializeComponent();

        FormBorderStyle = FormBorderStyle.None;

        // Barra de título
        var barraTitulo = new Panel();
        barraTitulo.Dock = DockStyle.Top;
        barraTitulo.Height = 35;
        barraTitulo.BackColor = Color.FromArgb(30, 30, 30);

        var closeButton = new Button();
        closeButton.Text = "X";
        closeButton.Dock = DockStyle.Right;
        closeButton.FlatStyle = FlatStyle.Flat;
        closeButton.ForeColor = Color.White;
        closeButton.Click += (sender, e) => this.Close();

        barraTitulo.Controls.Add(closeButton);

        // Barra de ferramentas
        var barraFerramentas = new FlowLayoutPanel();
        barraFerramentas.Dock = DockStyle.Top;
        barraFerramentas.Height = 40;

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
                if (!barraEndereco.Text.StartsWith("http://") &&
                    !barraEndereco.Text.StartsWith("https://"))
                {
                    if (barraEndereco.Text.Contains("."))
                    {
                        barraEndereco.Text = "https://" + barraEndereco.Text;
                    }
                    else
                    {
                        barraEndereco.Text =
                            "https://www.google.com/search?q=" + barraEndereco.Text;
                    }
                }

                navegador.Source = new Uri(barraEndereco.Text);
                e.SuppressKeyPress = true;
            }
        };

        // Botão voltar
        var backButton = new Button();
        backButton.Text = "<";
        backButton.Click += (sender, e) =>
        {
            if (navegador.CanGoBack)
                navegador.GoBack();
        };

        // Botão avançar
        var frontButton = new Button();
        frontButton.Text = ">";
        frontButton.Click += (sender, e) =>
        {
            if (navegador.CanGoForward)
                navegador.GoForward();
        };

        // Botão atualizar
        var resetPag = new Button();
        resetPag.Text = "↻";
        resetPag.Click += (sender, e) =>
        {
            navegador.Reload();
        };

        barraFerramentas.Controls.Add(backButton);
        barraFerramentas.Controls.Add(frontButton);
        barraFerramentas.Controls.Add(resetPag);
        barraFerramentas.Controls.Add(barraEndereco);

        // Navegador
        navegador.Dock = DockStyle.Fill;

        navegador.SourceChanged += (sender, e) =>
        {
            barraEndereco.Text = navegador.Source.ToString();
        };

        // Montagem da janela
        // A ordem de Dock é importante no WinForms.
        Controls.Add(navegador);
        Controls.Add(barraFerramentas);
        Controls.Add(barraTitulo);

        barraTitulo.BringToFront();
        barraFerramentas.BringToFront();

        navegador.Source = new Uri("https://www.google.com");
    }
}
