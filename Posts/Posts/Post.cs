using System;
using System.Collections;
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
        public string Data { get; set; }
        public string Horario { get; set; }
        public ArrayList Like { get; set; } = new ArrayList();
        public Dictionary<int, string> Comentario { get; set; } = new Dictionary<int, string>();
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

        public int BuscarRemetente(int i)
        {
            return posts[i].Remetente;
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

        public void AdicionarLike(int i, int codUsuario)
        {
            posts[i].Like.Add(codUsuario);
        }

        public void RemoverLike(int i, int codUsuario)
        {
            posts[i].Like.Remove(codUsuario);
        }

        public int buscarQuantidadeLike(int i)
        {
            return posts[i].Like.Count;
        }

        public Boolean verificarUsuarioLike(int i, int codUsuario) //Verifica se o usuário logado já deu like ou não
        {
            if (posts[i].Like.Contains(codUsuario))
            {
                return true;
            }
            return false;
        }
    }
}
