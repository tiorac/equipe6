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


            Distribuir();

            VezJogador1 = true;
        }

        public ValorCarta ValorManilha
        {
            get
            {
                var valorVirada = (int)CartaVirada.Valor;
                valorVirada += 1;

                if (valorVirada > (int)ValorCarta.Tres)
                    return ValorCarta.Quatro;
                else
                    return (ValorCarta)valorVirada;
            }
        }

        public EstadoJogo Estado
        {
            get
            {
                if (Jogador1.PontuacaoGeral < 11
                    && Jogador2.PontuacaoGeral < 11)
                    return EstadoJogo.Normal;

                if (Jogador1.PontuacaoGeral >= 12
                    || Jogador2.PontuacaoGeral >= 12)
                    return EstadoJogo.Finalizado;

                if (Jogador1.PontuacaoGeral == 11
                    ^ Jogador2.PontuacaoGeral == 11)
                    return EstadoJogo.Mao11;

                return EstadoJogo.MaoDeFerro;
            }
        }

        public int PesoCarta(Carta carta)
        {
            if (carta.Valor == ValorManilha)
                return 10 + (int)carta.Naipe;

            return (int)carta.Valor;
        }

        public int JogadorVencedor()
        {
            if (Jogador1.CartaSelecionada == null
                || Jogador2.CartaSelecionada == null)
                return -1;

            var pesoCarta1 = PesoCarta(Jogador1.CartaSelecionada);
            var pesoCarta2 = PesoCarta(Jogador2.CartaSelecionada);

            if (pesoCarta1 > pesoCarta2)
                return 1;
            else if (pesoCarta2 > pesoCarta1)
                return 2;
            else
                return 0;
        }

        public void SelecionarCarta(Carta carta)
        {
            if (VezJogador1
                && !Jogador1.SelecionarCarta(carta))
                return;

            if ((!VezJogador1)
                && !Jogador2.SelecionarCarta(carta))
                return;

            if (Jogador1.CartaSelecionada != null && Jogador2.CartaSelecionada != null)
                Pontuar();
            else
                VezJogador1 = !VezJogador1;
        }

        public void Distribuir()
        {
            Baralho.GerarBaralho();

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
            var valorRodada = Estado switch
            {
                EstadoJogo.Mao11 => 3,
                EstadoJogo.Truco => 3,
                _ => 1,
            };

            if (JogadorVencedor() == 1)
            {
                Jogador1.PontuacaoRodada++;

                if (Jogador1.PontuacaoRodada >= 2 && Jogador2.PontuacaoRodada <= 2)
                {

                    Jogador1.PontuacaoGeral += valorRodada;

                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }

                VezJogador1 = true;
            }
            else if (JogadorVencedor() == 2)
            {
                Jogador2.PontuacaoRodada++;

                if (Jogador1.PontuacaoRodada <= 2 && Jogador2.PontuacaoRodada >= 2)
                {
                    Jogador2.PontuacaoGeral += valorRodada;

                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }
                VezJogador1 = false;
            }
            else if (JogadorVencedor() == 0)
            {
                Jogador1.PontuacaoRodada++;
                Jogador2.PontuacaoRodada++;

                if (Jogador1.PontuacaoRodada == 3 && Jogador2.PontuacaoRodada == 3)
                {
                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }
                else if (Jogador1.PontuacaoRodada > Jogador2.PontuacaoRodada)
                {
                    Jogador1.PontuacaoGeral += valorRodada;

                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }
                else if (Jogador1.PontuacaoRodada < Jogador2.PontuacaoRodada)
                {
                    Jogador2.PontuacaoGeral += valorRodada;

                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }
                VezJogador1 = !VezJogador1;
            }  

            Jogador1.CartaSelecionada = null;
            Jogador2.CartaSelecionada = null;
        }
    }
}
