using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
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

    /*
    Para fazer:
    Função excluir postagem
    Adicionar botão para remover a foto prévia
    Posts Amigos é para mostrar só dos amigos ou o próprio também?
    Criar Label "Comente algo" no campo comentário
    Limitar quantidade de caractéres do campo título
    Limitar linhas dos campos texto e comentário
    Deixar as rows vazias caso não tenha conteúdo (usar if(isNull))
    */
    public partial class MainWindow : Window
    {
        private PostManager postManager = new PostManager();
        private UsuarioManager usuarioManager = new UsuarioManager();

        int codUsuario;
        string enderecoMidia;
        string exibicaoPost;
        string projectPath; //Caminho até o projeto para poder adicionar fotos e ícones em outros computadores

        //Cores
        SolidColorBrush corFundo;
        SolidColorBrush corPrincipal;
        SolidColorBrush corSecundaria;
        SolidColorBrush corPlano;
        public MainWindow()
        {
            InitializeComponent();

            //Atribuições para teste
            codUsuario = 0;
            enderecoMidia = "";
            exibicaoPost = "proprio";

            projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName; //Pega o caminho do projeto
            
            usuarioManager.ArmazenarUsuario(0, "Johnny Mukai", projectPath + "\\Imagens\\Pukki.jpg");
            usuarioManager.ArmazenarUsuario(1, "Satoru Gojo", projectPath + "\\Imagens\\Gojo.jpg");
            usuarioManager.ArmazenarUsuario(2, "Jennifer Lawrence", projectPath + "\\Imagens\\Jennifer.jpeg");
            usuarioManager.ArmazenarUsuario(3, "Luigi", projectPath + "\\Imagens\\Luigi.png");

            usuarioManager.AdicionarAmigo(codUsuario, 2);

            RichTextBox tempRichTextBox = new RichTextBox();

            tempRichTextBox.AppendText("Fiz um pudim muito bom!");
            TextRange tempTextRange = new TextRange(tempRichTextBox.Document.ContentStart, tempRichTextBox.Document.ContentEnd);
            string tempTextoFormatado;
            using (MemoryStream stream = new MemoryStream())
            {
                tempTextRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    tempTextoFormatado = reader.ReadToEnd();
                }
            }
            postManager.ArmazenarPost(0, "El Pudinho", tempTextoFormatado, "", "22/09/2024 10:10");
            tempRichTextBox.Document.Blocks.Clear();

            tempRichTextBox.AppendText("Alguém sabe onde eu coloquei minha carteira?");
            tempTextRange = new TextRange(tempRichTextBox.Document.ContentStart, tempRichTextBox.Document.ContentEnd);
            using (MemoryStream stream = new MemoryStream())
            {
                tempTextRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    tempTextoFormatado = reader.ReadToEnd();
                }
            }
            postManager.ArmazenarPost(1, "", tempTextoFormatado, "", "22/09/2024 10:54");
            tempRichTextBox.Document.Blocks.Clear();

            tempRichTextBox.AppendText("Olha esse elefante gigante");
            tempTextRange = new TextRange(tempRichTextBox.Document.ContentStart, tempRichTextBox.Document.ContentEnd);
            using (MemoryStream stream = new MemoryStream())
            {
                tempTextRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    tempTextoFormatado = reader.ReadToEnd();
                }
            }
            postManager.ArmazenarPost(0, "", tempTextoFormatado, "", "22/09/2024 11:13");
            tempRichTextBox.Document.Blocks.Clear();

            tempRichTextBox.AppendText("Deixa o Like!");
            tempTextRange = new TextRange(tempRichTextBox.Document.ContentStart, tempRichTextBox.Document.ContentEnd);
            using (MemoryStream stream = new MemoryStream())
            {
                tempTextRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    tempTextoFormatado = reader.ReadToEnd();
                }
            }
            postManager.ArmazenarPost(1, "", tempTextoFormatado, "", "23/09/2024 15:33");
            tempRichTextBox.Document.Blocks.Clear();

            tempRichTextBox.AppendText("Que Mario?");
            tempTextRange = new TextRange(tempRichTextBox.Document.ContentStart, tempRichTextBox.Document.ContentEnd);
            using (MemoryStream stream = new MemoryStream())
            {
                tempTextRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    tempTextoFormatado = reader.ReadToEnd();
                }
            }
            postManager.ArmazenarPost(3, "", tempTextoFormatado, "", "23/09/2024 19:27");
            tempRichTextBox.Document.Blocks.Clear();

            tempRichTextBox.AppendText("Estou com fome");
            tempTextRange = new TextRange(tempRichTextBox.Document.ContentStart, tempRichTextBox.Document.ContentEnd);
            using (MemoryStream stream = new MemoryStream())
            {
                tempTextRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    tempTextoFormatado = reader.ReadToEnd();
                }
            }
            postManager.ArmazenarPost(2, "", tempTextoFormatado, "", "24/09/2024 01:11");

            postManager.AdicionarLike(0, 2);
            postManager.AdicionarLike(0, 3);
            postManager.AdicionarLike(0, 4);
            postManager.AdicionarLike(1, 4);

            //Instância das cores
            corFundo = new SolidColorBrush(Color.FromRgb(240, 240, 250));
            corPrincipal = new SolidColorBrush(Color.FromRgb(55, 55, 110));
            corSecundaria = new SolidColorBrush(Color.FromRgb(75, 75, 130));
            corPlano = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            atualizarPaginaPostProprio();
            exibirFotoPerfil();

            botaoPostProprio.Background = corPlano;
            botaoPostProprio.Foreground = corPrincipal;
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
        public void atualizarPaginaPostAmigos()
        {
            gridPosts.Children.Clear();

            for (int i = postManager.BuscarQuantidade() - 1; i >= 0; i--)
            {
                if (usuarioManager.VerificarCodAmigo(codUsuario, postManager.BuscarRemetente(i)))
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
            //Cria o grid do corpo do post
            Grid gridPostCorpo = new Grid();
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Autor do post
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Título
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Texto
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Mídia
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Quantidade de Likes, comentários e recomendações
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Botão Curtir e Recomendar

            //Cria uma nova row no gridMensagens
            gridPosts.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            //Cria a grid do autor
            Grid gridAutor = new Grid();
            gridAutor.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridAutor.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridAutor.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            gridAutor.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            //Grid para os botões Curtir, Comentar e Recomendar
            Grid gridBotoes = new Grid();
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão curtir
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão comentar
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão recomendar
            //Borda do gridBotoes
            Border borderBotoes = new Border()
            {
                BorderBrush = corPrincipal,
                BorderThickness = new Thickness(0, 1, 0, 0)
            };
            borderBotoes.Child = gridBotoes;

            //Botão curtir 
            Grid gridCurtir = new Grid();
            gridCurtir.ColumnDefinitions.Add(new ColumnDefinition());//Ícone curtir
            gridCurtir.ColumnDefinitions.Add(new ColumnDefinition());//Quantidade de curtidas
            //Borda do botão curtir
            Border borderCurtir = new Border()
            {
                Background = corPlano,
                CornerRadius = new CornerRadius(0, 0, 0, 5)
            };
            borderCurtir.Child = gridCurtir;
            //Ícone curtir
            Image newIconeCurtir = new Image()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 20,
                Width = 20,
                Margin = new Thickness(10),
                Source = new BitmapImage(new Uri(projectPath + "\\Icones\\LikeAzul.png", UriKind.RelativeOrAbsolute))
            };
            //Quantidade de curtidas
            TextBlock newQuantidadeCurtida = new TextBlock()
            {
                Text = postManager.buscarQuantidadeLike(i).ToString(),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };
            borderCurtir.MouseLeftButtonUp += (sender, e) => gridCurtir_Click(sender, e, i, borderCurtir, newIconeCurtir, newQuantidadeCurtida);
            borderCurtir.MouseEnter += (sender, e) => gridCurtir_MouseEnter(sender, e, i, borderCurtir);
            borderCurtir.MouseLeave += (sender, e) => gridCurtir_MouseLeave(sender, e, i, borderCurtir);
            Grid.SetColumn(newIconeCurtir, 0);
            Grid.SetColumn(newQuantidadeCurtida, 1);
            gridCurtir.Children.Add(newIconeCurtir);
            gridCurtir.Children.Add(newQuantidadeCurtida);

            //Botão comentar
            Grid gridComentar = new Grid();
            gridComentar.ColumnDefinitions.Add(new ColumnDefinition());//Ícone comentar
            gridComentar.ColumnDefinitions.Add(new ColumnDefinition());//Quantidade de comentário
            //Borda do botão comentar
            Border borderComentar = new Border()
            {
                Background = corPlano
            };
            borderComentar.Child = gridComentar;
            //Ícone comentar
            Image newIconeComentar = new Image()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 20,
                Width = 20,
                Margin = new Thickness(10),
                Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Comentario.png", UriKind.RelativeOrAbsolute))
            };
            //Quantidade de comentário
            TextBlock newQuantidadeComentario = new TextBlock()
            {
                Text = postManager.buscarQuantidadeComentario(i).ToString(),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };
            borderComentar.MouseLeftButtonUp += (sender, e) => borderComentar_Click(sender, e, i, gridPostCorpo, borderBotoes, borderCurtir);
            borderComentar.MouseEnter += (sender, e) => borderComentar_MouseEnter(sender, e, i, borderComentar);
            borderComentar.MouseLeave += (sender, e) => borderComentar_MouseLeave(sender, e, i, borderComentar);
            Grid.SetColumn(newIconeComentar, 0);
            Grid.SetColumn(newQuantidadeComentario, 1);
            gridComentar.Children.Add(newIconeComentar);
            gridComentar.Children.Add(newQuantidadeComentario);

            //Botão recomendar
            Grid gridRecomendar = new Grid();
            //Borda do botão recomendar
            Border borderRecomendar = new Border()
            {
                Background = corPlano,
                CornerRadius = new CornerRadius(0, 0, 5, 0)
            };
            borderRecomendar.Child = gridRecomendar;
            //Texto recomendar
            TextBlock newRecomendar = new TextBlock()
            {
                Text = "Recomendar",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 14
            };
            borderRecomendar.MouseEnter += (sender, e) => borderRecomendar_MouseEnter(sender, e, i, borderRecomendar);
            borderRecomendar.MouseLeave += (sender, e) => borderRecomendar_MouseLeave(sender, e, i, borderRecomendar);
            gridRecomendar.Children.Add(newRecomendar);

            //Cria a foto do autor
            Ellipse newAutorFoto = new Ellipse()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 45,
                Width = 45,
                Stroke = corPrincipal,
                Margin = new Thickness(10),
                Fill = new ImageBrush(new BitmapImage(new Uri(usuarioManager.BuscarFoto(postManager.BuscarRemetente(i)), UriKind.Relative)))
            };

            //Cria o nome do autor
            TextBlock newAutorNome = new TextBlock()
            {
                Text = usuarioManager.BuscarNome(postManager.BuscarRemetente(i)),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5, 15, 0, 0)
            };

            //Cria a data e horário
            TextBlock newDataHora = new TextBlock()
            {
                Text = postManager.BuscarData(i),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(5, -10, 0, 0)
            };

            //Cria o titulo
            TextBlock newTitulo = new TextBlock()
            {
                Text = postManager.BuscarTitulo(i),
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 16,
                Foreground = corPrincipal,
                FontWeight= FontWeights.Bold,
                Margin = new Thickness(0, 5, 0, 5)
            };

            //Cria o texto
            RichTextBox newTexto = new RichTextBox()
            {
                FontSize = 14,
                Margin = new Thickness(15, 5, 15, 5),
                IsReadOnly = true,
                Style = (Style)FindResource("RichTextBoxArredondado")
            };
            newTexto.Document.Blocks.Clear();
            //Carrega o texto formatado do Xaml armazenado
            string textoFormatado = postManager.BuscarTexto(i);
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(textoFormatado)))
            {
                try
                {
                    TextRange range = new TextRange(newTexto.Document.ContentStart, newTexto.Document.ContentEnd);
                    range.Load(stream, DataFormats.Xaml);
                } catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar o texto formatado: " + ex.Message);
                }
            }

            //Cria a foto do post
            Image newMidia = new Image()
            {
                Source = new BitmapImage(new Uri(postManager.BuscarMidia(i), UriKind.RelativeOrAbsolute)),
                MaxHeight = 150,
                MaxWidth = 150,
                Margin = new Thickness(0, 10, 0, 10)
            };

            //Cria a borda arredondada
            Border border = new Border()
            {
                Background = corPlano,
                Margin = new Thickness(0, 10, 0, 0),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                BorderBrush = corPrincipal,
                BorderThickness = new Thickness(1),
                Child = gridPostCorpo
            };

            //Adiciona a borda no gridMensagens
            gridPosts.Children.Add(border);

            //Adiciona a foto e nome na gridAutor
            Grid.SetRow(newAutorFoto, 0);
            Grid.SetRowSpan(newAutorFoto, 2);
            Grid.SetColumn(newAutorFoto, 0);
            Grid.SetRow(newAutorNome, 0);
            Grid.SetColumn(newAutorNome, 1);
            Grid.SetRow(newDataHora, 1);
            Grid.SetColumn(newDataHora, 1);
            gridAutor.Children.Add(newAutorFoto);
            gridAutor.Children.Add(newAutorNome);
            gridAutor.Children.Add(newDataHora);

            //Adiciona os botões na gridBotoes
            Grid.SetColumn(borderCurtir, 0);
            Grid.SetColumn(borderComentar, 1);
            Grid.SetColumn(borderRecomendar, 2);
            gridBotoes.Children.Add(borderCurtir);
            gridBotoes.Children.Add(borderComentar);
            gridBotoes.Children.Add(borderRecomendar);

            //Adiciona tudo no gridPostCorpo
            Grid.SetRow(gridAutor, 0);
            Grid.SetRow(newTitulo, 1);
            Grid.SetRow(newTexto, 2);
            Grid.SetRow(newMidia, 3);
            Grid.SetRow(borderBotoes, 4);
            gridPostCorpo.Children.Add(gridAutor);
            gridPostCorpo.Children.Add(newTitulo);
            gridPostCorpo.Children.Add(newTexto);
            gridPostCorpo.Children.Add(newMidia);
            gridPostCorpo.Children.Add(borderBotoes);

            //Adiciona o post na tela
            Grid.SetRow(border, gridPosts.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);

            //Altera a cor do botão do like
            alterarCorBotaoLike(i, newIconeCurtir);
        }

        //Função do botão Curtir
        private void gridCurtir_Click(object sender, EventArgs e, int i, Border border, Image iconeCurtir, TextBlock quantidadeCurtida)
        {
            if (!postManager.verificarUsuarioLike(i, codUsuario))
            {
                postManager.AdicionarLike(i, codUsuario);
                iconeCurtir.Source = new BitmapImage(new Uri(projectPath + "\\Icones\\LikeAzulPreenchido.png", UriKind.RelativeOrAbsolute));
                quantidadeCurtida.Text = postManager.buscarQuantidadeLike(i).ToString();
            }
            else
            {
                postManager.RemoverLike(i, codUsuario);
                iconeCurtir.Source = new BitmapImage(new Uri(projectPath + "\\Icones\\LikeAzul.png", UriKind.RelativeOrAbsolute));
                quantidadeCurtida.Text = postManager.buscarQuantidadeLike(i).ToString();
            }
        }

        //Altera a cor quando clica no like
        private void alterarCorBotaoLike(int i, Image iconeCurtir)
        {
            if (postManager.verificarUsuarioLike(i, codUsuario))
            {
                iconeCurtir.Source = new BitmapImage(new Uri(projectPath + "\\Icones\\LikeAzulPreenchido.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                iconeCurtir.Source = new BitmapImage(new Uri(projectPath + "\\Icones\\LikeAzul.png", UriKind.RelativeOrAbsolute));
            }
        }

        //Altera cor quando passa o mouse no like
        private void gridCurtir_MouseEnter(object sender, EventArgs e, int i, Border border)
        {
            border.Background = corFundo;
        }

        //Altera a cor quando tira o mouse do like
        private void gridCurtir_MouseLeave(object sender, EventArgs e, int i, Border border)
        {
            border.Background = corPlano;
        }

        //Função do botão comentário. Abre o campo para comentar e mostra outros comentários.
        private void borderComentar_Click(Object sender, EventArgs e, int i, Grid gridPostCorpo, Border borderBotoes, Border borderCurtir)
        {
            if (gridPostCorpo.Children.Count < 6)
            {
                gridPostCorpo.RowDefinitions.Add(new RowDefinition());
                Grid gridFormComentario = new Grid();
                gridFormComentario.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                gridFormComentario.ColumnDefinitions.Add(new ColumnDefinition());
                gridFormComentario.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

                Ellipse newAutorFoto = new Ellipse()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 45,
                    Width = 45,
                    Stroke = Brushes.DarkGray,
                    Margin = new Thickness(10),
                    Fill = new ImageBrush(new BitmapImage(new Uri(usuarioManager.BuscarFoto(codUsuario))))
                };

                TextBox newCampoComentario = new TextBox()
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    FontSize = 14,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10, 10, 10, 10),
                    Padding = new Thickness(5, 5, 30, 5),
                    Style = (Style)Application.Current.Resources["TextBoxArredondado"]
                };

                Image newBotaoEnviarComentario = new Image()
                {
                    Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Enviar2.png", UriKind.RelativeOrAbsolute)),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Width = 25,
                    Height = 25,
                    Margin = new Thickness(0, 0, 15, 0)
                };

                Grid.SetColumn(newAutorFoto, 0);
                Grid.SetColumn(newCampoComentario, 1);
                Grid.SetColumn(newBotaoEnviarComentario, 1);
                gridFormComentario.Children.Add(newAutorFoto);
                gridFormComentario.Children.Add(newCampoComentario);
                gridFormComentario.Children.Add(newBotaoEnviarComentario);

                Grid.SetRow(gridFormComentario, 5);
                gridPostCorpo.Children.Add(gridFormComentario);

                newCampoComentario.Focus();

                //Alterações
                borderBotoes.BorderThickness = new Thickness(0, 1, 0, 1);
                borderCurtir.CornerRadius = new CornerRadius(0);
            }
            else
            {
                gridPostCorpo.Children.RemoveAt(gridPostCorpo.Children.Count - 1);

                //Desalterações
                borderBotoes.BorderThickness = new Thickness(0, 1, 0, 0);
                borderCurtir.CornerRadius = new CornerRadius(0, 0, 0, 5);
            }
        }

        //Altera cor quando passa o mouse no Comentar
        private void borderComentar_MouseEnter(object sender, MouseEventArgs e, int i, Border borderComentar)
        {
            borderComentar.Background = corFundo;
        }

        //Altera cor quando tira o mouse do Comentar
        private void borderComentar_MouseLeave(object sender, MouseEventArgs e, int i, Border borderComentar)
        {
            borderComentar.Background = corPlano;
        }

        //Altera cor quando passa o mouse no Recomendar
        private void borderRecomendar_MouseEnter(object sender, MouseEventArgs e, int i, Border borderRecomendar)
        {
            borderRecomendar.Background = corFundo;
        }

        //Altera cor quando tira o mouse do Recomendar
        private void borderRecomendar_MouseLeave(object sender, MouseEventArgs e, int i, Border borderRecomendar)
        {
            borderRecomendar.Background= corPlano;
        }

        //Apagar a label "Texto" quando digitar
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
            TextRange conteudo = new TextRange(campoTexto.Document.ContentStart, campoTexto.Document.ContentEnd); // Captura o texto no RichTextBox
            string texto = conteudo.Text.Trim();
            if (String.IsNullOrEmpty(texto))
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
            TextRange textRange = new TextRange(campoTexto.Document.ContentStart, campoTexto.Document.ContentEnd);
            string textoFormatado;
            using (MemoryStream stream = new MemoryStream())
            {
                textRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    textoFormatado = reader.ReadToEnd();
                }
            }
            bool isRichTextBoxEmpty = string.IsNullOrWhiteSpace(textRange.Text.Trim()) || textoFormatado.Trim().Equals("<FlowDocument xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" />");

            if (isRichTextBoxEmpty && string.IsNullOrEmpty(enderecoMidia))
            {
                MessageBox.Show("Escreva algum texto ou selecione uma imagem.");
            }
            else
            {
                postManager.ArmazenarPost(codUsuario, campoTitulo.Text, textoFormatado, enderecoMidia, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                campoTitulo.Clear();
                campoTexto.Document.Blocks.Clear();
                enderecoMidia = "";
                removerPrevia(); //Remove a prévia da foto após postar
            }

            if (exibicaoPost == "proprio")
            {
                atualizarPaginaPostProprio();
            }
            else if (exibicaoPost == "amigos")
            {
                atualizarPaginaPostAmigos();
            }
            else
            {
                atualizarPaginaPostGeral();
            }
        }

        //Permite selecionar uma foto para a postagem
        private void botaoAdicionarFoto_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (enderecoMidia == "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

                if (openFileDialog.ShowDialog() == true)
                {
                    enderecoMidia = openFileDialog.FileName;
                    previaFoto();
                }
            }
            else
            {
                enderecoMidia = "";
                removerPrevia();
            }
        }

        //Mostra uma prévia da foto selecionada
        private void previaFoto()
        {
            Image newMidia = new Image()
            {
                Source = new BitmapImage(new Uri(enderecoMidia, UriKind.RelativeOrAbsolute)),
                MaxHeight = 150,
                MaxWidth = 150,
                Margin = new Thickness(0, 10, 0, 10)
            };

            Grid.SetRow(newMidia, 2);
            Grid.SetColumn(newMidia, 0);
            Grid.SetColumnSpan(newMidia, 4);
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
            alterarCorBotaoPost(botaoPostProprio);
            exibicaoPost = "proprio";
        }

        //Mostrar postagens dos amigos
        private void botaoPostAmigos_Click(object sender, RoutedEventArgs e)
        {
            atualizarPaginaPostAmigos();
            alterarCorBotaoPost(botaoPostAmigos);
            exibicaoPost = "amigos";
        }

        //Mostrar postagens de todos
        private void botaoPostGeral_Click(object sender, RoutedEventArgs e)
        {
            atualizarPaginaPostGeral();
            alterarCorBotaoPost(botaoPostGeral);
            exibicaoPost = "geral";
        }

        private void alterarCorBotaoPost(Button botaoPost)
        {
            botaoPostProprio.Background = corPrincipal;
            botaoPostAmigos.Background = corPrincipal;
            botaoPostGeral.Background = corPrincipal;
            botaoPostProprio.Foreground = corFundo;
            botaoPostAmigos.Foreground = corFundo;
            botaoPostGeral.Foreground = corFundo;

            botaoPost.Background = corPlano;
            botaoPost.Foreground = corPrincipal;
        }

        //Exibir a foto de perfil no formulário de post
        private void exibirFotoPerfil()
        {
            postFormFoto.Fill = new ImageBrush(new BitmapImage(new Uri(usuarioManager.BuscarFoto(codUsuario))));
        }

        private void botaoAdicionarFoto_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoAdicionarFoto.Background = corFundo;
        }

        private void botaoAdicionarFoto_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoAdicionarFoto.Background = corPlano;
        }

        private void botaoPostar_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoPostar.Background = corSecundaria;
        }

        private void botaoPostar_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoPostar.Background = corPrincipal;
        }

        private void botaoPostProprio_MouseEnter(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "proprio")
            {
                botaoPostProprio.Background = corSecundaria;
            }
        }

        private void botaoPostProprio_MouseLeave(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "proprio")
            {
                botaoPostProprio.Background = corPrincipal;
            }
        }

        private void botaoPostAmigos_MouseEnter(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "amigos")
            {
                botaoPostAmigos.Background = corSecundaria;
            }
        }

        private void botaoPostAmigos_MouseLeave(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "amigos")
            {
                botaoPostAmigos.Background = corPrincipal;
            }
        }

        private void botaoPostGeral_MouseEnter(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "geral")
            {
                botaoPostGeral.Background = corSecundaria;
            }
        }

        private void botaoPostGeral_MouseLeave(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "geral")
            {
                botaoPostGeral.Background = corPrincipal;
            }
        }

        private void botaoNegrito_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoTexto.Selection;
            if (!selectedText.IsEmpty)
            {
                if (selectedText.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
                {
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                }
                else
                {
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
            }
            campoTexto.Focus();
        }

        private void botaoItalico_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoTexto.Selection;
            if (!selectedText.IsEmpty)
            {
                if (selectedText.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
                {
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                }
                else
                {
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                }
            }
            campoTexto.Focus();
        }

        private void botaoSublinhado_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoTexto.Selection;
            if (!selectedText.IsEmpty)
            {
                TextDecorationCollection textDecorations = (TextDecorationCollection)selectedText.GetPropertyValue(Inline.TextDecorationsProperty);
                if (textDecorations == TextDecorations.Underline)
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
            }
            campoTexto.Focus();
        }
    }
}
