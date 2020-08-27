﻿using System;
using System.Collections.Generic;
using System.Text;
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
            MestaAtual = new Mesa();
            MestaAtual.Iniciar();

            InitializeComponent();
            DesenharJogo();
        }

        public void DesenharJogo()
        {
            cartaJog1Carta1.CartaAtual = MestaAtual.Jogador1.Carta1;
            cartaJog1Carta2.CartaAtual = MestaAtual.Jogador1.Carta2;
            cartaJog1Carta3.CartaAtual = MestaAtual.Jogador1.Carta3;

            cartaVirada.CartaAtual = MestaAtual.CartaVirada;

            cartaJog2Carta1.CartaAtual = MestaAtual.Jogador2.Carta1;
            cartaJog2Carta2.CartaAtual = MestaAtual.Jogador2.Carta2;
            cartaJog2Carta3.CartaAtual = MestaAtual.Jogador2.Carta3;

            cartaJogador1.CartaAtual = MestaAtual.Jogador1.CartaSelecionada;
            cartaJogador2.CartaAtual = MestaAtual.Jogador2.CartaSelecionada;

            textPontosJogador1.Text = MestaAtual.Jogador1.PontuacaoGeral.ToString();
            textPontosJogador2.Text = MestaAtual.Jogador2.PontuacaoGeral.ToString();

            textPontosRodada1.Text = MestaAtual.Jogador1.PontuacaoRodada.ToString();
            textPontosRodada2.Text = MestaAtual.Jogador2.PontuacaoRodada.ToString();
        }

        public Mesa MestaAtual
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
            MestaAtual.SelecionarCarta(carta);
            DesenharJogo();
        }
    }
}
