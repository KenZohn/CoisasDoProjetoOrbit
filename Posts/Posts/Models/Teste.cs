using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;

namespace Posts.Models
{
    public class Teste
    {
        UsuarioManager usuarioManager = new UsuarioManager();
        PostManager postManager = new PostManager();
        string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        public void AdicionarUsuario()
        {
            usuarioManager.ArmazenarUsuario(0, "Johnny Mukai", projectPath + "\\Imagens\\Pukki.jpg");
            usuarioManager.ArmazenarUsuario(1, "Satoru Gojo", projectPath + "\\Imagens\\Gojo.jpg");
            usuarioManager.ArmazenarUsuario(2, "Jennifer Lawrence", projectPath + "\\Imagens\\Jennifer.jpeg");
            usuarioManager.ArmazenarUsuario(3, "Luigi", projectPath + "\\Imagens\\Luigi.png");
        }

        public void AdicionarAmigo(int codUsuario)
        {
            usuarioManager.AdicionarAmigo(codUsuario, 2);
        }

        public void AdicionarPost()
        {
            postManager.ArmazenarPost(0, "El Pudinho", FormatarTextoPost("Fiz um pudim muito bom!"), "", "22/09/2024 10:10");
            postManager.ArmazenarPost(1, "", FormatarTextoPost("Alguém sabe onde eu coloquei minha carteira?"), "", "22/09/2024 10:54");
            postManager.ArmazenarPost(0, "", FormatarTextoPost("Olha esse elefante gigante"), "", "22/09/2024 11:13");
            postManager.ArmazenarPost(1, "", FormatarTextoPost("Deixa o Like!"), "", "23/09/2024 15:33");
            postManager.ArmazenarPost(3, "", FormatarTextoPost("Que Mario?"), "", "23/09/2024 19:27");
            postManager.ArmazenarPost(2, "", FormatarTextoPost("Estou com fome"), "", "24/09/2024 01:11");
        }

        public void AdicionarLike()
        {
            postManager.AdicionarLike(0, 2);
            postManager.AdicionarLike(0, 3);
            postManager.AdicionarLike(0, 4);
            postManager.AdicionarLike(1, 4);
        }

        public void AdicionarComentario()
        {
            postManager.AdicionarComentario(2, 1, "Comentário legal!");
            postManager.AdicionarComentario(2, 2, "Comentário legal 2!");
            postManager.AdicionarComentario(5, 0, "UHUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
        }

        public string FormatarTextoPost(string texto)
        {
            RichTextBox RichTextBox = new RichTextBox()
            {
                FontSize = 14
            };

            RichTextBox.AppendText(texto);
            TextRange tempTextRange = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
            string TextoFormatado;
            using (MemoryStream stream = new MemoryStream())
            {
                tempTextRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    TextoFormatado = reader.ReadToEnd();
                }
            }
            return TextoFormatado;
        }
    }
}
