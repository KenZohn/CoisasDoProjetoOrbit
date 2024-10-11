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

    /*
    Função de apagar a mensagem
    Colocar a data da mensagem
    Desativar o botão enviar quando o campo de mensagem estiver vazia
    Aumentar tamanho do campo quando passar pra linha de baixo
    Ajustar a largura máxima do balão
    Colocar foto e nome com quem fala
    Personalizar a barra de rolagem (cor e arredondamento)
    Mudar a cor do campo onde digita
    Mudar a aparência do botão de enviar
    Criar uma lista para selecionar o amigo com quem quer conversar
    */

    public partial class MainWindow : Window
    {
        private PerfilManager perfilManager = new PerfilManager();
        private Lista lista = new Lista();

        int codUsuario;
        int codUsuarioAmigo;
        string projectPath;

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
            codUsuario = 0; //Código do usuário logado
            codUsuarioAmigo = 1;

            perfilManager.AdicionarPerfil("Johnny Bravo", projectPath + "\\Imagens\\Gojo.png");
            perfilManager.AdicionarPerfil("Gojo Satorino", projectPath + "\\Imagens\\Gojo.png");
            perfilManager.AdicionarPerfil("Roger Joker", projectPath + "\\Imagens\\Roger.png");
            perfilManager.AdicionarPerfil("Maria Maria", projectPath + "\\Imagens\\Roger.png");
            perfilManager.AdicionarPerfil("Luiza Maria", projectPath + "\\Imagens\\Johnny.png");

            perfilManager.AdicionarAmigo(0, 1);
            perfilManager.AdicionarAmigo(0, 2);
            perfilManager.AdicionarAmigo(0, 3);
            perfilManager.AdicionarAmigo(0, 4);

            lista.AdicionarMensagem(0, 1, "Oi", "20:22");
            lista.AdicionarMensagem(1, 0, "Vamos comer?", "20:25");
            lista.AdicionarMensagem(0, 1, "Vamos", "20:30");
            lista.AdicionarMensagem(2, 0, "Elefante", "06:45");

            //Instância das cores
            corFundo = new SolidColorBrush(Color.FromRgb(240, 240, 250));
            corPrincipal = new SolidColorBrush(Color.FromRgb(55, 55, 110));
            corSecundaria = new SolidColorBrush(Color.FromRgb(75, 75, 130));
            corPlano = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            corLinha = new SolidColorBrush(Color.FromRgb(200, 200, 200));

            buscarHistorico();

            repetirLista(codUsuario);
        }

        public void buscarHistorico()
        {
            for (int i = 0; i < lista.BuscarQuantidade(); i++)
            {
                if (lista.VerificarMensagemRemetente(i, codUsuario, codUsuarioAmigo))
                {
                    atualizarChatRemetente(i);
                }
                else if (lista.VerificarMensagemDestinatario(i, codUsuario, codUsuarioAmigo))
                {
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

        //Funçao do botão enviar
        private void botaoEnviar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(campoMensagem.Text)) //Só envia se o campo de mensagem não estiver vazio.
            {
                lista.AdicionarMensagem(codUsuario, codUsuarioAmigo, campoMensagem.Text, DateTime.Now.ToShortTimeString());

                atualizarChatRemetente(lista.BuscarQuantidade() - 1);

                campoMensagem.Clear();
            }
            campoMensagem.Focus();
        }

        //Enviar a mensagem pressionando "Enter"
        private void campoMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                botaoEnviar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        //Descer a barra de rolagem quando enviar uma mensagem
        private void scrollChat_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0)
            {
                scrollChat.ScrollToEnd();
            }
        }

        //Teste do amigo enviando a mensagem
        private void botaoEnviar2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(campoMensagem2.Text)) //Só envia se o campo de mensagem não estiver vazio.
            {
                lista.AdicionarMensagem(codUsuarioAmigo, codUsuario, campoMensagem2.Text, DateTime.Now.ToShortTimeString());

                atualizarChatDestinatario(lista.BuscarQuantidade() - 1);

                campoMensagem2.Clear();
            }
            campoMensagem2.Focus();
        }

        //Listar amigos
        public void listarUsuario(int codPerfil)
        {
            PageCartaoChat pageCartaoChat = new PageCartaoChat(codPerfil, this);
            Frame frame = new Frame()
            {
                Margin = new Thickness(10, 10, 10, 0)
            };
            frame.Navigate(pageCartaoChat);
            panelAmigos.Children.Add(frame);
        }

        public void repetirLista(int codUser)
        {
            for (int i = 0; i < perfilManager.BuscarQuantidadeAmigos(codUser); i++)
            {
                listarUsuario(perfilManager.BuscarCodAmigo(codUsuario, i));
            }
        }

        public void enviarCodPerfil(int codPerfil)
        {
            codUsuarioAmigo = codPerfil;
            gridMensagens.Children.Clear();
            buscarHistorico();
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
