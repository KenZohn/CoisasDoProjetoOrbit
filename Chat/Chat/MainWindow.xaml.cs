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

    //Fazer animação do balão aparecendo
    //Função de apagar a mensagem
    //Colocar a data da mensagem
    //Desativar o botão enviar quando o campo de mensagem estiver vazia
    //Criar um dicionário para armazenar as informações
    //Aumentar tamanho do campo quando passar pra linha de baixo
    //Ajustar a largura máxima do balão
    //Colocar foto e nome com quem fala
    //Personalizar a barra de rolagem (cor e arredondamento)
    //Mudar a cor do campo onde digita
    //Mudar o botão de enviar

    public partial class MainWindow : Window
    {
        ArrayList remetente = new ArrayList(); //Código do usuário que enviou
        ArrayList destinatario = new ArrayList(); //Código do usuário que recebeu
        ArrayList conteudo = new ArrayList(); //Conteúdo da mensagem
        ArrayList horario = new ArrayList(); //Horário da mensagem
        ArrayList data = new ArrayList(); //Data da mensagem

        int usuario;
        int usuario2;

        public MainWindow()
        {
            InitializeComponent();

            //Atribuições de teste
            usuario = 1; //Código do usuário logado
            usuario2 = 2; //Código do usuário com quem está falando
            remetente.Add(1);
            destinatario.Add(2);
            conteudo.Add("Oi");
            horario.Add("20:22");
            remetente.Add(2);
            destinatario.Add(1);
            conteudo.Add("Vamos comer?");
            horario.Add("20:25");
            remetente.Add(1);
            destinatario.Add(2);
            conteudo.Add("Vamos");
            horario.Add("20:30");
            remetente.Add(3);
            destinatario.Add(1);
            conteudo.Add("Elefante");
            horario.Add("06:45");

            for (int i = 0; i < remetente.Count; i++)
            {
                if ((int)remetente[i] == usuario && (int)destinatario[i] == usuario2)
                {
                    atualizarChat(i);
                }
                else if ((int)destinatario[i] == usuario && (int)remetente[i] == usuario2)
                {
                    atualizarChat2(i);
                }
            }
        }

        //Adiciona as mensagens do usuario atual
        public void atualizarChat(int i)
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
                Text = (string)conteudo[i],
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
                Text = (string)horario[i],
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

            //Animação
            /*DoubleAnimation sizeAnimation = new DoubleAnimation();
            sizeAnimation.From = 0;
            sizeAnimation.To = border.ActualWidth;
            sizeAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.05));
            border.BeginAnimation(Border.WidthProperty, sizeAnimation);*/
        }

        //Adiciona as mensagens da pessoa com quem conversa
        public void atualizarChat2(int i)
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
                Text = (string)conteudo[i],
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
                Text = (string)horario[i],
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
                remetente.Add(usuario);
                destinatario.Add(usuario2);
                conteudo.Add(campoMensagem.Text);
                horario.Add(DateTime.Now.ToShortTimeString());

                atualizarChat(remetente.Count - 1);

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
                remetente.Add(usuario2);
                destinatario.Add(usuario);
                conteudo.Add(campoMensagem2.Text);
                horario.Add(DateTime.Now.ToShortTimeString());

                atualizarChat2(remetente.Count - 1);

                campoMensagem2.Clear();
            }
            campoMensagem2.Focus();
        }
    }
}
