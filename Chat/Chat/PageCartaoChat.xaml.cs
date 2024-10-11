using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Chat
{
    /// <summary>
    /// Interação lógica para PageCartaoChat.xam
    /// </summary>
    public partial class PageCartaoChat : Page
    {
        PerfilManager perfil = new PerfilManager();
        MainWindow mainWindow;
        int codPerfil;
        public PageCartaoChat(int _codPerfil, MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            codPerfil = _codPerfil;
            buscarUsuario(codPerfil);
        }

        private void buscarUsuario(int codPerfil)
        {
            foto.Fill = new ImageBrush(new BitmapImage(new Uri(perfil.BuscarFoto(codPerfil))));
            textNome.Text = perfil.BuscarNome(codPerfil);
        }

        private void gridCartao_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mainWindow.enviarCodPerfil(codPerfil);
        }
    }
}
