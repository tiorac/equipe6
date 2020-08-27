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
        int ValorRodada = 1;
        private bool Mao11 = false;

        public void Iniciar()
        {
            Jogador1 = new Player();
            Jogador2 = new Player();
            Baralho = new Baralho();

            Baralho.GerarBaralho();
            Distribuir(true);
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

                if (Jogador1.PontuacaoRodada >= 2 && Jogador2.PontuacaoRodada <= 2)
                {
                    if (Mao11)
                    {
                        Jogador1.PontuacaoGeral += ValorRodada;
                    }
                    else
                    {
                        Jogador1.PontuacaoGeral++;
                    }

                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir(false);
                }
            }
            else if (Jogador1.CartaSelecionada.Valor < Jogador2.CartaSelecionada.Valor)
            {
                Jogador2.PontuacaoRodada++;

                if (Jogador1.PontuacaoRodada <= 2 && Jogador2.PontuacaoRodada >= 2)
                {
                    if (Mao11)
                    {
                        Jogador2.PontuacaoGeral += ValorRodada;
                    }
                    else
                    {
                        Jogador2.PontuacaoGeral++;
                    }

                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir(false);
                }
            }
            else if (Jogador1.CartaSelecionada.Valor == Jogador2.CartaSelecionada.Valor)
            {
                Jogador1.PontuacaoRodada++;
                Jogador2.PontuacaoRodada++;

                if (Jogador1.PontuacaoRodada == 3 && Jogador2.PontuacaoRodada == 3)
                {
                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir(false);
                }
            }

            if (Jogador1.PontuacaoGeral == 11 || Jogador2.PontuacaoGeral == 11)
            {
                Mao11 = true;
                ValorRodada = 3;
            }
        }

        public void Truco(int valor)
        {
            ValorRodada = valor;
        }
    }
}
