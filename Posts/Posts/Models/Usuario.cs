using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts
{
    public class Usuario
    {
        public int CodUsuario { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public ArrayList Amigos { get; set; } = new ArrayList();
    }

    public class UsuarioManager
    {
        private static List<Usuario> usuario = new List<Usuario>();

        public void ArmazenarUsuario(int codUser, string nome, string foto)
        {
            Usuario novoUsuario = new Usuario()
            {
                CodUsuario = codUser,
                Nome = nome,
                Foto = foto
            };

            usuario.Add(novoUsuario);
        }

        public void AdicionarAmigo(int codUsuario, int codAmigo)
        {
            usuario[codUsuario].Amigos.Add(codAmigo);
        }

        public int BuscarCodAmigo(int codUsuario, int i)
        {
            return (int)usuario[codUsuario].Amigos[i];
        }

        public string BuscarNome(int codUsuario)
        {
            return usuario[codUsuario].Nome;
        }

        public string BuscarFoto(int codUsuario)
        {
            return usuario[codUsuario].Foto;
        }

        public int BuscarQuantidadeAmigos(int codUsuario)
        {
            return usuario[codUsuario].Amigos.Count;
        }

        public Boolean VerificarCodAmigo(int codUsuario, int codAmigo)
        {
            return usuario[codUsuario].Amigos.Contains(codAmigo);
        }
    }
}
