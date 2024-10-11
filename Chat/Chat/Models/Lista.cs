using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Lista
    {
        private Node inicio, fim;

        public Lista()
        {
            inicio = fim = null;
        }

        public void AdicionarMensagem(int remetente, int destinatario, string conteudo, string horario)
        {
            if (inicio != null)
            {
                fim.setProx(new Node(remetente, destinatario, conteudo, horario));
                fim = fim.getProx();
            }
            else
            {
                inicio = fim = new Node(remetente, destinatario, conteudo, horario);
            }
        }

        public bool VerificarMensagemRemetente(int i, int remetente, int destinatario)
        {
            Node aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    if (aux.getRemetente() == remetente && aux.getDestinatario() == destinatario)
                    {
                        return true;
                    }
                }
                aux = aux.getProx();
                contador++;
            }
            return false;
        }

        public bool VerificarMensagemDestinatario(int i, int remetente, int destinatario)
        {
            Node aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    if (aux.getRemetente() == destinatario && aux.getDestinatario() == remetente)
                    {
                        return true;
                    }
                }
                aux = aux.getProx();
                contador++;
            }
            return false;
        }

        public string BuscarConteudo(int i)
        {
            Node aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    return aux.getConteudo();
                }
                aux = aux.getProx();
                contador++;
            }
            return null;
        }

        public string BuscarHorario(int i)
        {
            Node aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    return aux.getHorario();
                }
                aux = aux.getProx();
                contador++;
            }
            return null;
        }

        public int BuscarQuantidade()
        {
            Node aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                aux = aux.getProx();
                contador++;
            }
            return contador;
        }
    }
}
