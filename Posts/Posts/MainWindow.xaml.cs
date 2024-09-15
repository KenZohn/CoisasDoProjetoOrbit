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
using Microsoft.Win32;

namespace Posts
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>

    //Ajustar posição dos botões de like e recomendar
    //Mensagem de campo não preenchido
    //Ajustar o tamanho da imagem para caber no post
    //Adicionar barra de rolagem
    //Alterar campo de exibição da mídia
    public partial class MainWindow : Window
    {
        private PostManager postManager = new PostManager();

        int usuario;
        public MainWindow()
        {
            InitializeComponent();

            //Atribuições para teste
            usuario = 1;
            postManager.ArmazenarPost(1, "Pudim", "Fiz um pudim muito bom!", "FotoPudim");
            postManager.ArmazenarPost(1, "Elefante", "Olha esse elefante gigante", "FotoElefante");

            atualizarPaginaPost();
        }

        //Coloca todos os posts do usuário na página
        public void atualizarPaginaPost()
        {
            gridPosts.Children.Clear();

            for (int i = postManager.BuscarQuantidade() - 1; i >= 0; i--)
            {
                if (postManager.VerificarPostProprio(i, usuario))
                {
                    publicarPost(i);
                }
            }
        }

        //Insere o post na página
        public void publicarPost(int i)
        {
            //Cria o balão
            Grid gridTexto = new Grid();
            gridTexto.RowDefinitions.Add(new RowDefinition());//Título
            gridTexto.RowDefinitions.Add(new RowDefinition());//Texto
            gridTexto.RowDefinitions.Add(new RowDefinition());//Mídia
            gridTexto.RowDefinitions.Add(new RowDefinition());//Botão de Like e Recomendar

            //Cria uma nova row no gridMensagens
            gridPosts.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            //gridPosts.RowDefinitions.Insert(0, new RowDefinition() { Height = GridLength.Auto });

            //Cria o texto que vai no balão
            TextBlock newTitulo = new TextBlock()
            {
                Text = postManager.BuscarTitulo(i),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            TextBlock newTexto = new TextBlock()
            {
                Text = postManager.BuscarTexto(i),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            BitmapImage minhaBitmapImage = new BitmapImage();
            minhaBitmapImage.BeginInit();
            minhaBitmapImage.UriSource = new Uri(postManager.BuscarMidia(i), UriKind.RelativeOrAbsolute);
            minhaBitmapImage.EndInit();

            Image newMidia = new Image()
            {
                Source = minhaBitmapImage
            };

            Button botaoLike = new Button()
            {
                Content = "Like",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5)
            };

            Button botaoComentario = new Button()
            {
                Content = "Comentario",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(50, 0, 0, 0)
            };

            Button botaoRecomendar = new Button()
            {
                Content = "Recomendar",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(150, 0, 0, 0)
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = new SolidColorBrush(Colors.White),
                Margin = new Thickness(5, 5, 5, 5),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Child = gridTexto
            };

            //Adiciona a borda no gridMensagens
            gridPosts.Children.Add(border);

            //Adiciona o texto e o horário no balão
            Grid.SetRow(newTitulo, 0);
            Grid.SetRow(newTexto, 1);
            Grid.SetRow(newMidia, 2);
            Grid.SetRow(botaoLike, 3);
            Grid.SetRow(botaoComentario, 3);
            Grid.SetRow(botaoRecomendar, 3);
            gridTexto.Children.Add(newTitulo);
            gridTexto.Children.Add(newTexto);
            gridTexto.Children.Add(newMidia);
            gridTexto.Children.Add(botaoLike);
            gridTexto.Children.Add(botaoComentario);
            gridTexto.Children.Add(botaoRecomendar);

            //Adiciona o balão na tela
            Grid.SetRow(border, gridPosts.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);
        }

        private void campoTitulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(campoTitulo.Text))
            {
                labelTitulo.Visibility = Visibility.Visible;
            }
            else
            {
                labelTitulo.Visibility = Visibility.Hidden;
            }
        }

        private void campoTexto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(campoTexto.Text))
            {
                labelTexto.Visibility = Visibility.Visible;
            }
            else
            {
                labelTexto.Visibility = Visibility.Hidden;
            }
        }

        private void botaoPostar_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(campoTexto.Text))
            {
                postManager.ArmazenarPost(1, campoTitulo.Text, campoTexto.Text, campoMidia.Text);

                campoTitulo.Clear();
                campoTexto.Clear();
                campoMidia.Clear();
            }

            atualizarPaginaPost();
        }

        private void botaoImagem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.InitialDirectory = "c://";
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            //openFileDialog.FilterIndex = 1;
            //openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                campoMidia.Text = openFileDialog.FileName;
            }

        }
    }
}
