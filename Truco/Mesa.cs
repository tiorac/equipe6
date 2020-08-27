using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6.Truco
{
    public class Mesa
    {
        public Player Jogador1;
        public Player Jogador2;
        public Baralho Baralho;
        public Carta CartaVirada = new Carta();
        public bool VezJogador1 { get; set; }

        public void Iniciar()
        {
            Jogador1 = new Player();
            Jogador2 = new Player();
            Baralho = new Baralho();

            Baralho.GerarBaralho();
            Distribuir(true);

            VezJogador1 = true;
        }

        public void SelecionarCarta(Carta carta)
        {
            if (VezJogador1)
                Jogador1.SelecionarCarta(carta);

            if (!VezJogador1)
                Jogador2.SelecionarCarta(carta);

            if (Jogador1.CartaSelecionada != null && Jogador2.CartaSelecionada != null)
                Pontuar();
            else
                VezJogador1 = !VezJogador1;
        }

        public void Distribuir(bool virar)
        {
            if (virar)
                CartaVirada = Baralho.GetTopCard();

            Jogador1.Carta1 = Baralho.GetTopCard();
            Jogador1.Carta2 = Baralho.GetTopCard();
            Jogador1.Carta3 = Baralho.GetTopCard();

            Jogador2.Carta1 = Baralho.GetTopCard();
            Jogador2.Carta2 = Baralho.GetTopCard();
            Jogador2.Carta3 = Baralho.GetTopCard();
        }

        public void Pontuar()
        {
            if (Jogador1.CartaSelecionada.Valor > Jogador2.CartaSelecionada.Valor)
            {
                Jogador1.PontuacaoRodada++;
            }
            else
            {
                Jogador2.PontuacaoRodada++;
            }

            if(Jogador1.PontuacaoRodada == 2)
            {
                Jogador1.PontuacaoGeral++;
                Jogador1.PontuacaoRodada = 0;
                Jogador2.PontuacaoRodada = 0;
                Jogador1.CartaSelecionada = null;
                Jogador2.CartaSelecionada = null;
                Distribuir(false);
                VezJogador1 = true;
            }
            else if (Jogador1.PontuacaoRodada == 2)
            {
                Jogador1.PontuacaoGeral++;
                Jogador1.PontuacaoRodada = 0;
                Jogador2.PontuacaoRodada = 0;
                Jogador1.CartaSelecionada = null;
                Jogador2.CartaSelecionada = null;
                Distribuir(false);
                VezJogador1 = false;
            }
        }
    }
}
