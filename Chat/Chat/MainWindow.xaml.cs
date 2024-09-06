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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList remetente = new ArrayList(); //Código do usuário que enviou
        ArrayList destinatario = new ArrayList(); //Código do usuário que recebeu
        ArrayList conteudo = new ArrayList(); //Conteúdo da mensagem

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
            remetente.Add(2);
            destinatario.Add(1);
            conteudo.Add("Vamos comer?");
            remetente.Add(1);
            destinatario.Add(2);
            conteudo.Add("Vamos");
            remetente.Add(3);
            destinatario.Add(1);
            conteudo.Add("Elefante");

            for (int i = 0; i < remetente.Count; i++)
            {
                if ((int)remetente[i] == usuario && (int)destinatario[i] == usuario2)
                {
                    atualizarChat(i);
                }
                else if ((int)destinatario[i] == usuario && (int)remetente[i] == usuario2)
                {
                    atualizarChat2(1);
                }
            }
        }

        public void atualizarChat(int i)
        {
            //Cria o balão
            Grid gridTexto = new Grid();

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
                Margin = new Thickness(0, 5, 10, 5),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Child = gridTexto
            };

            gridMensagens.Children.Add(border);

            //Adiciona o texto no balão
            gridTexto.Children.Add(newTextBlock);

            //Adiciona o balão na tela
            Grid.SetRow(border, gridMensagens.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);
        }

        public void atualizarChat2(int i)
        {
            //Cria o balão
            Grid gridTexto = new Grid();

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
                Margin = new Thickness(10, 5, 0, 5),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Child = gridTexto
            };

            gridMensagens.Children.Add(border);

            //Adiciona o texto no balão
            gridTexto.Children.Add(newTextBlock);

            //Adiciona o balão na tela
            Grid.SetRow(border, gridMensagens.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);
        }

        private void botaoEnviar_Click(object sender, RoutedEventArgs e)
        {
            //Cria o balão
            Grid gridTexto = new Grid();

            gridMensagens.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            //Cria o texto que vai no balão
            TextBlock newTextBlock = new TextBlock()
            {
                Text = campoMensagem.Text,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0, 5, 10, 5),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Child = gridTexto
            };

            gridMensagens.Children.Add(border);

            //Adiciona o texto no balão
            gridTexto.Children.Add(newTextBlock);

            //Adiciona o balão na tela
            Grid.SetRow(border, gridMensagens.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);

            //Inverter
            /*int rowCount = gridMensagens.RowDefinitions.Count;
            foreach (UIElement element in gridMensagens.Children)
            {
                int currentRow = Grid.GetRow(element);
                Grid.SetRow(element, rowCount - 1 - currentRow);
            }*/

            campoMensagem.Clear();
            campoMensagem.Focus();

            //Adicionar barra de rolagem
            //Inverter posição de aparição dos balões
            //Fazer animação do balão aparecendo
            //Não deixar enviar mensagem em branco
            //Função de apagar a mensagem
        }

        private void campoMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                botaoEnviar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
