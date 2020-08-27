using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Equipe6.Truco
{
    /// <summary>
    /// Interação lógica para CartaBaralho.xam
    /// </summary>
    public partial class CartaBaralho : UserControl
    {
        public CartaBaralho(Carta carta)
        {
            this.DataContext = carta;
            InitializeComponent();
        }

        public Carta Carta
        {
            return (Carta)this.DataContext;
        }
    }
}
