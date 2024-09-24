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

    //Adicionar armazenamento de data e horário
    public partial class MainWindow : Window
    {
        private PostManager postManager = new PostManager();
        private UsuarioManager usuarioManager = new UsuarioManager();

        int codUsuario;
        string enderecoMidia;
        string exibicaoPost;
        public MainWindow()
        {
            InitializeComponent();

            //Atribuições para teste
            usuarioManager.ArmazenarUsuario(0, "Johnny", "");
            usuarioManager.ArmazenarUsuario(1, "Gojo", "");
            usuarioManager.ArmazenarUsuario(2, "Maria", "");
            usuarioManager.ArmazenarUsuario(3, "Luigi", "");

            codUsuario = 0;
            enderecoMidia = "";
            postManager.ArmazenarPost(0, "Pudim", "Fiz um pudim muito bom!", "");
            postManager.ArmazenarPost(0, "Elefante", "Olha esse elefante gigante", "");
            postManager.ArmazenarPost(1, "Como correr", "Leve uma perna para frente e em seguida a perna oposta. Repita o processo.", "");
            postManager.ArmazenarPost(1, "Joinha", "Deixa o Like!", "");
            postManager.AdicionarLike(0, 2);
            postManager.AdicionarLike(0, 3);
            postManager.AdicionarLike(0, 4);
            postManager.AdicionarLike(1, 4);

            exibicaoPost = "proprio";

            atualizarPaginaPostProprio();

            botaoPostProprio.Background = Brushes.Gray;
        }

        //Coloca todos os posts do próprio usuário na página
        public void atualizarPaginaPostProprio()
        {
            gridPosts.Children.Clear();

            for (int i = postManager.BuscarQuantidade() - 1; i >= 0; i--)
            {
                if (postManager.VerificarPostProprio(i, codUsuario))
                {
                    publicarPost(i);
                }
            }
        }

        //Coloca todos os posts de todos na página
        public void atualizarPaginaPostGeral()
        {
            gridPosts.Children.Clear();

            for (int i = postManager.BuscarQuantidade() - 1; i >= 0; i--)
            {
                publicarPost(i);
            }
        }

        //Insere o post na página
        public void publicarPost(int i)
        {
            //Colunas para os botões Curtir, Comentar e Recomendar
            Grid gridBotoes = new Grid();
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão curtir
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão comentar
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão recomendar

            //Colunar para a quantidade de likes, comentários e recomendações
            Grid gridQtdLCR = new Grid();
            gridQtdLCR.ColumnDefinitions.Add(new ColumnDefinition());//Quantidade de Likes
            gridQtdLCR.ColumnDefinitions.Add(new ColumnDefinition());//Quantidade de Comentários
            gridQtdLCR.ColumnDefinitions.Add(new ColumnDefinition());//Quantidade de Recomendações

            //Cria o balão
            Grid gridUmPost = new Grid();
            gridUmPost.RowDefinitions.Add(new RowDefinition());//Autor do post
            gridUmPost.RowDefinitions.Add(new RowDefinition());//Título
            gridUmPost.RowDefinitions.Add(new RowDefinition());//Texto
            gridUmPost.RowDefinitions.Add(new RowDefinition());//Mídia
            gridUmPost.RowDefinitions.Add(new RowDefinition());//Quantidade de Likes, comentários e recomendações
            gridUmPost.RowDefinitions.Add(new RowDefinition());//Botão Curtir, Comentar e Recomendar

            //Cria uma nova row no gridMensagens
            gridPosts.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            //Cria o texto do nome do Autor
            TextBlock newAutor = new TextBlock()
            {
                Text = usuarioManager.BuscarNome(postManager.BuscarRemetente(i)),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            //Cria o texto do título
            TextBlock newTitulo = new TextBlock()
            {
                Text = postManager.BuscarTitulo(i),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            //Cria o texto do conteúdo
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

            //Cria a foto
            Image newMidia = new Image()
            {
                Source = minhaBitmapImage,
                MaxHeight = 150,
                MaxWidth = 150
            };

            //Cria o texto de quantidade de likes
            TextBlock newLikes = new TextBlock() //Número de likes
            {
                Text = postManager.buscarQuantidadeLike(i).ToString(),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            //Cria o botão Curti
            Button botaoCurtir = new Button()
            {
                Content = "Curtir",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 0),
                Background = new SolidColorBrush(Colors.White)
            };

            //Função de curtir
            botaoCurtir.Click += (sender, e) => BotaoCurtir_Click(sender, e, i, botaoCurtir, newLikes);

            //Cria o botão comentar
            Button botaoComentar = new Button()
            {
                Content = "Comentar",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 0),
                Background = new SolidColorBrush(Colors.White)
            };

            //Cria o botão recomendar
            Button botaoRecomendar = new Button()
            {
                Content = "Recomendar",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 0),
                Background = new SolidColorBrush(Colors.White)
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0, 10, 0, 0),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Child = gridUmPost
            };

            //Adiciona a borda no gridMensagens
            gridPosts.Children.Add(border);

            //Adiciona o texto no balão
            Grid.SetRow(newAutor, 0);
            Grid.SetRow(newTitulo, 1);
            Grid.SetRow(newTexto, 2);
            Grid.SetRow(newMidia, 3);
            Grid.SetRow(gridQtdLCR, 4);
            Grid.SetRow(gridBotoes, 5);
            gridUmPost.Children.Add(newAutor);
            gridUmPost.Children.Add(newTitulo);
            gridUmPost.Children.Add(newTexto);
            gridUmPost.Children.Add(newMidia);
            gridUmPost.Children.Add(gridQtdLCR);
            gridUmPost.Children.Add(gridBotoes);

            //Adiciona a quantidade de likes, comentários e recomendações na gridQtdLCR
            Grid.SetColumn(newLikes, 0);
            gridQtdLCR.Children.Add(newLikes);

            //Adiciona os botões na gridBotoes
            Grid.SetColumn(botaoCurtir, 0);
            Grid.SetColumn(botaoComentar, 1);
            Grid.SetColumn(botaoRecomendar, 2);
            gridBotoes.Children.Add(botaoCurtir);
            gridBotoes.Children.Add(botaoComentar);
            gridBotoes.Children.Add(botaoRecomendar);

            //Adiciona o balão na tela
            Grid.SetRow(border, gridPosts.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);

            //Altera a cor do botão do like
            alterarCorBotaoLike(i, botaoCurtir);
        }

        //Função do botão Curtir
        private void BotaoCurtir_Click(object sender, EventArgs e, int i, Button button, TextBlock textBlock)
        {
            if (!postManager.verificarUsuarioLike(i, codUsuario))
            {
                postManager.AdicionarLike(i, codUsuario);
                button.Background = new SolidColorBrush(Colors.PowderBlue);
                textBlock.Text = postManager.buscarQuantidadeLike(i).ToString();
            }
            else
            {
                postManager.RemoverLike(i, codUsuario);
                button.Background = new SolidColorBrush(Colors.White);
                textBlock.Text = postManager.buscarQuantidadeLike(i).ToString();
            }
        }

        private void alterarCorBotaoLike(int i, Button button)
        {
            if (postManager.verificarUsuarioLike(i, codUsuario))
            {
                button.Background = new SolidColorBrush(Colors.PowderBlue);
            }
            else
            {
                button.Background = new SolidColorBrush(Colors.White);
            }
        }

        //Apagar a label "Título" quando digitar
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

        //Apagar a label "Texto" quando digitar
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

        //Botão para postar o post
        private void botaoPostar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(campoTexto.Text) && enderecoMidia == "")
            {
                MessageBox.Show("Escreva algum texto ou selecione uma imagem.");
            }
            else
            {
                postManager.ArmazenarPost(codUsuario, campoTitulo.Text, campoTexto.Text, enderecoMidia);

                campoTitulo.Clear();
                campoTexto.Clear();
                enderecoMidia = "";

                removerPrevia(); //Remove a prévia da foto após postar
                botaoImagem.Content = "Selecionar imagem";
            }

            if (exibicaoPost == "proprio")
            {
                atualizarPaginaPostProprio();
            }
            else if (exibicaoPost == "geral")
            {
                atualizarPaginaPostGeral();
            }
        }

        //Permite selecionar uma foto para a postagem
        private void botaoImagem_Click(object sender, RoutedEventArgs e)
        {
            if (enderecoMidia == "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

                if (openFileDialog.ShowDialog() == true)
                {
                    enderecoMidia = openFileDialog.FileName;
                    previaFoto();
                    botaoImagem.Content = "Remover imagem";
                }
            }
            else
            {
                enderecoMidia = "";
                removerPrevia();
                botaoImagem.Content = "Selecionar imagem";
            }
        }

        //Mostra uma prévia da foto selecionada
        private void previaFoto()
        {
            BitmapImage minhaBitmapImage = new BitmapImage();
            minhaBitmapImage.BeginInit();
            minhaBitmapImage.UriSource = new Uri(enderecoMidia, UriKind.RelativeOrAbsolute);
            minhaBitmapImage.EndInit();

            Image newMidia = new Image()
            {
                Source = minhaBitmapImage,
                MaxHeight = 150,
                MaxWidth = 150,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Grid.SetRow(newMidia, 2);
            gridFormPost.Children.Add(newMidia);
        }

        //Remove a prévia da foto
        private void removerPrevia()
        {
            var elementsInRow = gridFormPost.Children.Cast<UIElement>().Where(n => Grid.GetRow(n) == 2).ToList();
            foreach (var element in elementsInRow)
            {
                gridFormPost.Children.Remove(element);
            }
        }

        //Mostrar postagens próprias
        private void botaoPostProprio_Click(object sender, RoutedEventArgs e)
        {
            atualizarPaginaPostProprio();
            exibicaoPost = "proprio";

            botaoPostProprio.Background = Brushes.Gray;
            botaoPostGeral.Background = Brushes.White;
        }

        //Mostrar postagens de todos
        private void botaoPostGeral_Click(object sender, RoutedEventArgs e)
        {
            atualizarPaginaPostGeral();
            exibicaoPost = "geral";

            botaoPostProprio.Background = Brushes.White;
            botaoPostGeral.Background = Brushes.Gray;
        }
    }
}
