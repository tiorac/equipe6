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

        public void teste()
        {
            Jogador1 = new Player();
            Jogador2 = new Player();
            Baralho = new Baralho();

            Baralho.GerarBaralho();
            Distribuir(true);

            if (Jogador1.PontuacaoGeral < 12 && Jogador2.PontuacaoGeral < 12)
            {
                Jogador1.PontuacaoRodada = 0;
                Jogador2.PontuacaoRodada = 0;

                if (Jogador1.PontuacaoRodada < 2 && Jogador2.PontuacaoRodada < 2)
                {
                    Rodada();
                }

                if (Jogador1.PontuacaoRodada == 2)
                {
                    Jogador1.PontuacaoGeral++;
                }
                else if(Jogador2.PontuacaoRodada == 2)
                {
                    Jogador2.PontuacaoGeral++;
                }

                Distribuir(false);
            }

        }



        public void Distribuir(bool virar)
        {
            if(virar)
                CartaVirada = Baralho.GetTopCard();

            Jogador1.Carta1 = Baralho.GetTopCard();
            Jogador1.Carta2 = Baralho.GetTopCard();
            Jogador1.Carta3 = Baralho.GetTopCard();

            Jogador2.Carta1 = Baralho.GetTopCard();
            Jogador2.Carta2 = Baralho.GetTopCard();
            Jogador2.Carta3 = Baralho.GetTopCard();
        }

        public void Rodada()
        {
            if (Jogador1.CartaSelecionada.Valor > Jogador2.CartaSelecionada.Valor)
            {
                Jogador1.PontuacaoRodada++;
            }
            else
            {
                Jogador2.PontuacaoRodada++;
            }
        }
    }
}
