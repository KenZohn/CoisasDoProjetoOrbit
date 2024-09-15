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

        int usuario;
        int usuario2;

        public MainWindow()
        {
            InitializeComponent();

            //Atribuições de teste
            usuario = 1; //Código do usuário logado
            usuario2 = 2; //Código do usuário com quem está falando
            chatMensagensManager.ArmazenarMensagem(1, 2, "Oi", "20:22");
            chatMensagensManager.ArmazenarMensagem(2, 1, "Vamos comer?", "20:25");
            chatMensagensManager.ArmazenarMensagem(1, 2, "Vamos", "20:30");
            chatMensagensManager.ArmazenarMensagem(3, 1, "Elefante", "06:45");

            buscarHistorico();
        }

        public void buscarHistorico()
        {
            for (int i = 0; i < chatMensagensManager.BuscarQuantidade(); i++)
            {
                if (chatMensagensManager.BuscarMensagemRemetente(i, usuario, usuario2))
                {
                    atualizarChatRemetente(i);
                }
                else if (chatMensagensManager.BuscarMensagemDestinatario(i, usuario, usuario2))
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
                chatMensagensManager.ArmazenarMensagem(usuario, usuario2, campoMensagem.Text, DateTime.Now.ToShortTimeString());

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
            usuario2 = 3;

            gridMensagens.Children.Clear();

            buscarHistorico();
        }

        private void botaoAmigo2_Click(object sender, RoutedEventArgs e)
        {
            usuario2 = 2;

            gridMensagens.Children.Clear();

            buscarHistorico();
        }


        //Teste do amigo enviando a mensagem
        private void botaoEnviar2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(campoMensagem2.Text)) //Só envia se o campo de mensagem não estiver vazio.
            {
                chatMensagensManager.ArmazenarMensagem(usuario2, usuario, campoMensagem2.Text, DateTime.Now.ToShortTimeString());

                atualizarChatDestinatario(chatMensagensManager.BuscarQuantidade() - 1);

                campoMensagem2.Clear();
            }
            campoMensagem2.Focus();
        }
    }
}
