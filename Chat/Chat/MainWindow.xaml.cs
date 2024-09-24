using System;
using System.Collections;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

namespace Chat
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>

    //Função de apagar a mensagem
    //Colocar a data da mensagem
    //Desativar o botão enviar quando o campo de mensagem estiver vazia
    //Aumentar tamanho do campo quando passar pra linha de baixo
    //Ajustar a largura máxima do balão
    //Colocar foto e nome com quem fala
    //Personalizar a barra de rolagem (cor e arredondamento)
    //Mudar a cor do campo onde digita
    //Mudar a aparência do botão de enviar
    //Criar uma lista de amigos
    //Permitir selecionar o amigo com quem quer conversar

    public partial class MainWindow : Window
    {
        private ChatMensagensManager chatMensagensManager = new ChatMensagensManager();
        private PerfilManager perfilManager = new PerfilManager();

        int codUsuario;
        int codUsuarioAmigo;

        public MainWindow()
        {
            InitializeComponent();

            //Atribuições de teste
            codUsuario = 0; //Código do usuário logado
            codUsuarioAmigo = 2;
            chatMensagensManager.ArmazenarMensagem(0, 1, "Oi", "20:22");
            chatMensagensManager.ArmazenarMensagem(1, 0, "Vamos comer?", "20:25");
            chatMensagensManager.ArmazenarMensagem(0, 1, "Vamos", "20:30");
            chatMensagensManager.ArmazenarMensagem(2, 0, "Elefante", "06:45");

            perfilManager.ArmazenarPerfil(0, "Johnny", "C:\\Users\\ZettZ\\OneDrive\\Documentos\\Fatec\\Projeto ED\\Johnny\\Chat\\Chat\\Imagens\\Gojo.png");
            perfilManager.ArmazenarPerfil(1, "Gojo", "C:\\Users\\ZettZ\\OneDrive\\Documentos\\Fatec\\Projeto ED\\Johnny\\Chat\\Chat\\Imagens\\Gojo.png");
            perfilManager.ArmazenarPerfil(2, "Roger", "C:\\Users\\ZettZ\\OneDrive\\Documentos\\Fatec\\Projeto ED\\Johnny\\Chat\\Chat\\Imagens\\Roger.png");

            perfilManager.AdicionarAmigo(0, 1);
            perfilManager.AdicionarAmigo(0, 2);

            buscarHistorico();

            for (int i = 0; i < perfilManager.BuscarQuantidadeAmigos(codUsuario); i++)
            {
                codUsuarioAmigo = perfilManager.BuscarCodAmigo(codUsuario, i);
                listarAmigos(codUsuarioAmigo);
            }
        }

        public void buscarHistorico()
        {
            for (int i = 0; i < chatMensagensManager.BuscarQuantidade(); i++)
            {
                if (chatMensagensManager.VerificarMensagemRemetente(i, codUsuario, codUsuarioAmigo))
                {
                    atualizarChatRemetente(i);
                }
                else if (chatMensagensManager.VerificarMensagemDestinatario(i, codUsuario, codUsuarioAmigo))
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
                Text = chatMensagensManager.BuscarConteudo(i),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = new SolidColorBrush(Colors.White),
                Margin = new Thickness(-80, 5, 10, 5),
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
                Text = chatMensagensManager.BuscarHorario(i),
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
                Text = chatMensagensManager.BuscarConteudo(i),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = new SolidColorBrush(Colors.Bisque),
                Margin = new Thickness(10, 5, -80, 5),
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
                Text = chatMensagensManager.BuscarHorario(i),
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

        //Botão enviar
        private void botaoEnviar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(campoMensagem.Text)) //Só envia se o campo de mensagem não estiver vazio.
            {
                chatMensagensManager.ArmazenarMensagem(codUsuario, codUsuarioAmigo, campoMensagem.Text, DateTime.Now.ToShortTimeString());

                atualizarChatRemetente(chatMensagensManager.BuscarQuantidade() - 1);

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

        private void botaoAmigo3_Click(object sender, RoutedEventArgs e)
        {
            codUsuarioAmigo = 3;

            gridMensagens.Children.Clear();

            buscarHistorico();
        }

        private void botaoAmigo2_Click(object sender, RoutedEventArgs e)
        {
            codUsuarioAmigo = 2;

            gridMensagens.Children.Clear();

            buscarHistorico();
        }


        //Teste do amigo enviando a mensagem
        private void botaoEnviar2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(campoMensagem2.Text)) //Só envia se o campo de mensagem não estiver vazio.
            {
                chatMensagensManager.ArmazenarMensagem(codUsuarioAmigo, codUsuario, campoMensagem2.Text, DateTime.Now.ToShortTimeString());

                atualizarChatDestinatario(chatMensagensManager.BuscarQuantidade() - 1);

                campoMensagem2.Clear();
            }
            campoMensagem2.Focus();
        }

        //Listar amigos
        private void listarAmigos(int codAmigo)
        {
            //Grid para listar o amigo
            Grid gridUsuarioAmigo = new Grid();
            gridUsuarioAmigo.ColumnDefinitions.Add(new ColumnDefinition());
            gridUsuarioAmigo.ColumnDefinitions.Add(new ColumnDefinition());

            //Cria uma nova row
            gridAmigos.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            //Cria o texto para o nome do amigo
            TextBlock newAmigoNome = new TextBlock()
            {
                Text = perfilManager.BuscarNome(codAmigo),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = new SolidColorBrush(Colors.White),
                Margin = new Thickness(5, 10, 5, 0),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Child = gridUsuarioAmigo
            };

            ImageBrush imageBrush = new ImageBrush();
            BitmapImage bitmapImage = new BitmapImage(new Uri(perfilManager.BuscarFoto(codAmigo), UriKind.Relative));
            imageBrush.ImageSource = bitmapImage;

            Ellipse newAmigoFoto = new Ellipse()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 60,
                Width = 60,
                Fill = imageBrush
            };

            //Adiciona a borda no gridMensagens
            gridAmigos.Children.Add(border);

            //Adiciona o texto e o horário no balão
            Grid.SetColumn(newAmigoFoto, 0);
            Grid.SetColumn(newAmigoNome, 1);
            gridUsuarioAmigo.Children.Add(newAmigoFoto);
            gridUsuarioAmigo.Children.Add(newAmigoNome);

            //Adiciona o balão na tela
            Grid.SetRow(border, gridAmigos.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);
        }
    }
}
