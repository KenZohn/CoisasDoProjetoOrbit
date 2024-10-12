using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class ChatMensagens
    {
        public int Remetente { get; set; }
        public int Destinatario { get; set; }
        public string Conteudo { get; set; }
        public string Horario { get; set; }
    }

    public class ChatMensagensManager
    {
        private static List<ChatMensagens> chatMensagens = new List<ChatMensagens>();

        public void AdicionarMensagem(int remetente, int destinatario, string conteudo, string horario)
        {
            ChatMensagens novaMensagem = new ChatMensagens()
            {
                Remetente = remetente,
                Destinatario = destinatario,
                Conteudo = conteudo,
                Horario = horario
            };

            chatMensagens.Add(novaMensagem);
        }

        public bool VerificarMensagemRemetente(int i, int usuario, int usuario2)
        {
            if (chatMensagens[i].Remetente == usuario && chatMensagens[i].Destinatario == usuario2)
            {
                return true;
            }
            return false;
        }

        public bool VerificarMensagemDestinatario(int i, int usuario, int usuario2)
        {
            if (chatMensagens[i].Remetente == usuario2 && chatMensagens[i].Destinatario == usuario)
            {
                return true;
            }
            return false;
        }

        public bool VerificarUltimaMensagem(int i, int usuario)
        {
            if (chatMensagens[i].Remetente == usuario || chatMensagens[i].Destinatario == usuario)
            {
                return true;
            }
            return false;
        }

        public string BuscarConteudo(int i)
        {
            return chatMensagens[i].Conteudo;
        }

        public string BuscarHorario(int i)
        {
            return chatMensagens[i].Horario;
        }

        public int BuscarQuantidade()
        {
            return chatMensagens.Count;
        }
    }
}
