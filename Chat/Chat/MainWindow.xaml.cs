using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chat.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Chat
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>

    public partial class MainWindow : Window
    {
        private PerfilManager perfilManager = new PerfilManager();
        private Lista lista = new Lista();

        int codUsuario;
        int codUsuarioAmigo;
        string projectPath;
        string data;

        Border borderCartao;
        TextBlock textNome;

        //Cores
        SolidColorBrush corFundo;
        SolidColorBrush corPrincipal;
        SolidColorBrush corSecundaria;
        SolidColorBrush corPlano;
        SolidColorBrush corLinha;

        public MainWindow()
        {
            InitializeComponent();

            projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

            //Atribuições de teste
            codUsuario = 0;
            codUsuarioAmigo = 0;

            perfilManager.AdicionarPerfil("Johnny Bravo", projectPath + "\\Imagens\\Gojo.png");
            perfilManager.AdicionarPerfil("Gojo Satorino", projectPath + "\\Imagens\\Gojo.png");
            perfilManager.AdicionarPerfil("Roger Joker", projectPath + "\\Imagens\\Roger.png");
            perfilManager.AdicionarPerfil("Maria Maria", projectPath + "\\Imagens\\Roger.png");
            perfilManager.AdicionarPerfil("Luiza Maria", projectPath + "\\Imagens\\Johnny.png");

            perfilManager.AdicionarAmigo(0, 1);
            perfilManager.AdicionarAmigo(0, 2);
            perfilManager.AdicionarAmigo(0, 3);
            perfilManager.AdicionarAmigo(0, 4);

            lista.AdicionarMensagem(0, 1, "Oi", "01/10/2024", "20:22");
            lista.AdicionarMensagem(1, 0, "Vamos comer?", "02/10/2024", "20:25");
            lista.AdicionarMensagem(0, 1, "Vamos", "02/10/2024", "20:30");
            lista.AdicionarMensagem(2, 0, "Elefante", "03/10/2024", "06:45");
            lista.AdicionarMensagem(0, 1, "Marmota", "04/10/2024", "10:45");

            //Instância das cores
            corFundo = new SolidColorBrush(Color.FromRgb(240, 240, 250));
            corPrincipal = new SolidColorBrush(Color.FromRgb(55, 55, 110));
            corSecundaria = new SolidColorBrush(Color.FromRgb(75, 75, 130));
            corPlano = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            corLinha = new SolidColorBrush(Color.FromRgb(200, 200, 200));

            campoMensagem.IsEnabled = false;
            repetirLista(codUsuario);
        }

        public void buscarHistorico()
        {
            data = "0";
            for (int i = 0; i < lista.BuscarQuantidade(); i++)
            {
                if (lista.VerificarMensagemRemetente(i, codUsuario, codUsuarioAmigo))
                {
                    atualizarData(i);
                    atualizarChatRemetente(i);
                }
                else if (lista.VerificarMensagemDestinatario(i, codUsuario, codUsuarioAmigo))
                {
                    atualizarData(i);
                    atualizarChatDestinatario(i);
                }
            }
        }

        //Adiciona as mensagens do usuario atual
        public void atualizarChatRemetente(int i)
        {
            //Cria o balão
            Grid gridTexto = new Grid();
            gridTexto.ColumnDefinitions.Add(new ColumnDefinition());
            gridTexto.ColumnDefinitions.Add(new ColumnDefinition());

            //Cria uma nova row no gridMensagens
            gridMensagens.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            //Cria o texto que vai no balão
            TextBlock newTextBlock = new TextBlock()
            {
                Text = lista.BuscarConteudo(i),
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(10),
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = corFundo,
                Margin = new Thickness(-80, 0, 10, 10),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Child = gridTexto
            };

            //Adiciona a borda no gridMensagens
            gridMensagens.Children.Add(border);

            //Pega o horário atual
            TextBlock textHorario = new TextBlock()
            {
                Text = lista.BuscarHorario(i),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Bottom,
                FontSize = 8,
                Opacity = 0.5
            };

            //Adiciona o texto e o horário no balão
            Grid.SetColumn(newTextBlock, 0);
            Grid.SetColumn(textHorario, 1);
            gridTexto.Children.Add(newTextBlock);
            gridTexto.Children.Add(textHorario);

            //Adiciona o balão na tela
            Grid.SetRow(border, gridMensagens.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);
        }

        //Adiciona as mensagens da pessoa com quem conversa
        public void atualizarChatDestinatario(int i)
        {
            //Cria o balão
            Grid gridTexto = new Grid();
            gridTexto.ColumnDefinitions.Add(new ColumnDefinition());
            gridTexto.ColumnDefinitions.Add(new ColumnDefinition());

            //Cria uma nova row no gridMensagens
            gridMensagens.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            //Cria o texto que vai no balão
            TextBlock newTextBlock = new TextBlock()
            {
                Text = lista.BuscarConteudo(i),
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(10),
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = new SolidColorBrush(Colors.Bisque),
                Margin = new Thickness(10, 0, -80, 10),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Child = gridTexto
            };

            //Adiciona a borda no gridMensagens
            gridMensagens.Children.Add(border);

            //Pega o horário atual
            TextBlock textHorario = new TextBlock()
            {
                Text = lista.BuscarHorario(i),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Bottom,
                FontSize = 8,
                Opacity = 0.5
            };

            //Adiciona o texto e o horário no balão
            Grid.SetColumn(newTextBlock, 0);
            Grid.SetColumn(textHorario, 1);
            gridTexto.Children.Add(newTextBlock);
            gridTexto.Children.Add(textHorario);

            //Adiciona o balão na tela
            Grid.SetRow(border, gridMensagens.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);
        }

        private void exibirNovaData(int i, string data)
        {
            Grid gridData = new Grid();

            gridMensagens.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            TextBlock newTextBlock = new TextBlock()
            {
                Text = data,
                FontSize = 12,
                Margin = new Thickness(5),
            };

            gridData.Children.Add(newTextBlock);

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = new SolidColorBrush(Colors.LightGray),
                Margin = new Thickness(10, 0, 0, 10),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Child = gridData
            };

            gridMensagens.Children.Add(border);

            Grid.SetRow(border, gridMensagens.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);
        }

        private void atualizarData(int i)
        {
            if (data != lista.BuscarData(i))
            {
                data = lista.BuscarData(i);
                exibirNovaData(i, data);
            }
        }

        //Funçao do botão enviar
        private void botaoEnviar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            enviarMensagem();
        }

        //Enviar a mensagem pressionando "Enter"
        private void campoMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                enviarMensagem();
            }
        }

        private void enviarMensagem()
        {
            if (!string.IsNullOrEmpty(campoMensagem.Text)) //Só envia se o campo de mensagem não estiver vazio.
            {
                lista.AdicionarMensagem(codUsuario, codUsuarioAmigo, campoMensagem.Text, DateTime.Now.ToShortDateString() ,DateTime.Now.ToShortTimeString());

                atualizarData(lista.BuscarQuantidade() - 1);

                atualizarChatRemetente(lista.BuscarQuantidade() - 1);

                campoMensagem.Clear();
            }
            campoMensagem.Focus();
        }

        //Descer a barra de rolagem quando enviar uma mensagem
        private void scrollChat_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0)
            {
                scrollChat.ScrollToEnd();
            }
        }

        //Listar amigos
        public void listarUsuario(int codPerfil)
        {
            Frame frame = new Frame()
            {
                Height = 65
            };
            frame.Navigate(new PageCartaoChat(codPerfil, this));
            panelAmigos.Children.Add(frame);
        }

        public void repetirLista(int codUser)
        {
            for (int i = 0; i < perfilManager.BuscarQuantidadeAmigos(codUser); i++)
            {
                listarUsuario(perfilManager.BuscarCodAmigo(codUsuario, i));
            }
        }

        public void exibirUsuarioSelecionado(int codPerfil)
        {
            frameAmigoSelecionado.Navigate(new PageCartaoChatSelecionado(codPerfil));
        }

        public void enviarCodPerfil(int codPerfil, Border _borderCartao, TextBlock _textNome)
        {
            codUsuarioAmigo = codPerfil;
            gridMensagens.Children.Clear();
            buscarHistorico();
            exibirUsuarioSelecionado(codPerfil);
            if (borderCartao != null)
            {
                borderCartao.Background = new SolidColorBrush(Colors.White);
                textNome.Foreground = new SolidColorBrush(Colors.Black);
            }
            borderCartao = _borderCartao;
            textNome = _textNome;
        }

        private void campoMensagem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(campoMensagem.Text))
            {
                labelMensagem.Visibility = Visibility.Visible;
            }
            else
            {
                labelMensagem.Visibility = Visibility.Hidden;
            }
        }
    }
}
