using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Equipe6.Truco
{
    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class JogoTruco : Window
    {
        public JogoTruco()
        {
            MesaAtual = new Mesa();
            MesaAtual.Iniciar();

            InitializeComponent();
            DesenharJogo();
        }

        public void DesenharJogo()
        {
            cartaJog1Carta1.CartaAtual = MesaAtual.Jogador1.Carta1;
            cartaJog1Carta2.CartaAtual = MesaAtual.Jogador1.Carta2;
            cartaJog1Carta3.CartaAtual = MesaAtual.Jogador1.Carta3;

            cartaVirada.CartaAtual = MesaAtual.CartaVirada;

            cartaJog2Carta1.CartaAtual = MesaAtual.Jogador2.Carta1;
            cartaJog2Carta2.CartaAtual = MesaAtual.Jogador2.Carta2;
            cartaJog2Carta3.CartaAtual = MesaAtual.Jogador2.Carta3;

            cartaJogador1.CartaAtual = MesaAtual.Jogador1.CartaSelecionada;
            cartaJogador2.CartaAtual = MesaAtual.Jogador2.CartaSelecionada;

            textPontosJogador1.Text = MesaAtual.Jogador1.PontuacaoGeral.ToString();
            textPontosJogador2.Text = MesaAtual.Jogador2.PontuacaoGeral.ToString();

            textPontosRodada1.Text = MesaAtual.Jogador1.PontuacaoRodada.ToString();
            textPontosRodada2.Text = MesaAtual.Jogador2.PontuacaoRodada.ToString();
            EventoJogo();
        }

        public Mesa MesaAtual
        {
            get
            {
                return (Mesa)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void SelecionaCarta_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var carta = ((CartaBaralho)sender).CartaAtual;
            MesaAtual.SelecionarCarta(carta);
            MesaAtual.Jogador2.Controlado.VerJogada(MesaAtual.ObterEstadoRodada(), MesaAtual.Jogador2);
            DesenharJogo();
        }

        public void EventoJogo()
        {
            //var ordemAtual = MesaAtual.OrdemJogadoresAtuais();
            if (!MesaAtual.VezJogador1)
            {
                var carta = MesaAtual.Jogador2.Controlado.Jogar(MesaAtual.ObterEstadoRodada(), MesaAtual.Jogador2);
                MesaAtual.SelecionarCarta(carta);
                DesenharJogo();
            }
        }
    }
}
