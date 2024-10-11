using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class Perfil
    {
        public int CodUsuario { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public ArrayList Amigos { get; set; } = new ArrayList();
    }

    public class PerfilManager
    {
        private static List<Perfil> perfil = new List<Perfil>();

        public void ArmazenarPerfil(int codUser, string nome, string foto)
        {
            Perfil novoPerfil = new Perfil()
            {
                CodUsuario = codUser,
                Nome = nome,
                Foto = foto
            };

            perfil.Add(novoPerfil);
        }

        public void AdicionarAmigo(int codUsuario, int codAmigo)
        {
            perfil[codUsuario].Amigos.Add(codAmigo);
        }

        public int BuscarCodAmigo(int codUsuario, int i)
        {
            return (int)perfil[codUsuario].Amigos[i];
        }

        public string BuscarNome(int codUsuario)
        {
            return perfil[codUsuario].Nome;
        }

        public string BuscarFoto(int codUsuario)
        {
            return perfil[codUsuario].Foto;
        }

        public int BuscarQuantidadeAmigos(int codUsuario)
        {
            return perfil[codUsuario].Amigos.Count;
        }


    }
}
