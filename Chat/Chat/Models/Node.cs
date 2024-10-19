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
        private Node Prox;
        private int Remetente;
        private int Destinatario;
        private string Conteudo;
        private string Data;
        private string Horario;
        public Node(int remetente, int destinatario, string conteudo, string data, string horario)
        {
            Remetente = remetente;
            Destinatario = destinatario;
            Conteudo = conteudo;
            Data = data;
            Horario = horario;
            Prox = null;
        }

        public void setProx(Node prox)
        {
            Prox = prox;
        }

        public Node getProx()
        {
            return Prox;
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

        public string getData()
        {
            return Data;
        }

        public string getHorario()
        {
            return Horario;
        }
    }
}
