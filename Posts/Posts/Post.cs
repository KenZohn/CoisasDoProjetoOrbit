using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts
{
    public class Post
    {
        public int Remetente { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Midia { get; set; }
    }

    public class PostManager
    {
        private static List<Post> posts = new List<Post>();

        public void ArmazenarPost(int remetente, string titulo, string texto, string midia)
        {
            Post novoPost = new Post()
            {
                Remetente = remetente,
                Titulo = titulo,
                Texto = texto,
                Midia = midia
            };

            posts.Add(novoPost);
        }

        public Boolean VerificarPostProprio(int i, int remetente)
        {
            if (posts[i].Remetente == remetente)
            {
                return true;
            }
            return false;
        }

        public string BuscarTitulo(int i)
        {
            return posts[i].Titulo;
        }

        public string BuscarTexto(int i)
        {
            return posts[i].Texto;
        }

        public string BuscarMidia(int i)
        {
            return posts[i].Midia;
        }

        public int BuscarQuantidade()
        {
            return posts.Count;
        }
    }
}
