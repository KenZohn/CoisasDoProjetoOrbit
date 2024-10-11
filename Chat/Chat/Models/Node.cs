using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Node
    {
        //private int x;
        private Node prox;
        private int Remetente;
        private int Destinatario;
        private string Conteudo;
        private string Horario;
        public Node(int remetente, int destinatario, string conteudo, string horario)
        {
            Remetente = remetente;
            Destinatario = destinatario;
            Conteudo = conteudo;
            Horario = horario;
            prox = null;
        }

        public void setProx(Node px)
        {
            prox = px;
        }

        public Node getProx()
        {
            return prox;
        }

        public int getRemetente()
        {
            return Remetente;
        }

        public int getDestinatario()
        {
            return Destinatario;
        }

        public string getConteudo()
        {
            return Conteudo;
        }

        public string getHorario()
        {
            return Horario;
        }
    }
}
