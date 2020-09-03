using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Equipe6.Truco
{
    public class Mesa
    {
        public Player Jogador1;
        public Player Jogador2;
        public Baralho Baralho;
        public Carta CartaVirada = new Carta();
        public int VencedorRodada1 = 0;

        public bool VezJogador1 { get; set; }

        public void Iniciar()
        {
            Jogador1 = new Player
            {
                Controlado = new HumanoControlador()
            };

            Jogador2 = new Player
            {
                //Controlado = new RandonControlador();
                Controlado = new BasicoControlador()
            };

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

        public int JogadorVencedor()
        {
            if (Jogador1.CartaSelecionada == null
                || Jogador2.CartaSelecionada == null)
                return -1;

            var pesoCarta1 = Jogador1.CartaSelecionada.PesoCarta(ValorManilha);
            var pesoCarta2 = Jogador2.CartaSelecionada.PesoCarta(ValorManilha);

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

            CartaVirada = Baralho.ObterCartaTopo();

            Jogador1.Carta1 = Baralho.ObterCartaTopo();
            Jogador1.Carta2 = Baralho.ObterCartaTopo();
            Jogador1.Carta3 = Baralho.ObterCartaTopo();

            Jogador2.Carta1 = Baralho.ObterCartaTopo();
            Jogador2.Carta2 = Baralho.ObterCartaTopo();
            Jogador2.Carta3 = Baralho.ObterCartaTopo();
        }

        public void Pontuar()
        {
            var valorRodada = Estado switch
            {
                EstadoJogo.Mao11 => 3,
                EstadoJogo.Truco => 3,
                _ => 1,
            };

            var jVencedor = JogadorVencedor();
            var rodada = Jogador1.CartasEmOrdem(ValorManilha).Count;

            if (jVencedor == 1)
            {
                Jogador1.PontuacaoRodada++;

                if (rodada == 2)
                    VencedorRodada1 = 1;
                else if (Jogador1.PontuacaoRodada > 0 && Jogador1.PontuacaoRodada > Jogador2.PontuacaoRodada)
                {
                    Jogador1.PontuacaoGeral += valorRodada;

                    VencedorRodada1 = 0;
                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }

                VezJogador1 = true;
            }
            else if (jVencedor == 2)
            {
                Jogador2.PontuacaoRodada++;

                if (rodada == 2)
                    VencedorRodada1 = 2;
                else if (Jogador2.PontuacaoRodada > 0 && Jogador2.PontuacaoRodada > Jogador1.PontuacaoRodada)
                {
                    Jogador2.PontuacaoGeral += valorRodada;

                    VencedorRodada1 = 0;
                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }

                VezJogador1 = false;
            }
            else if (jVencedor == 0)
            {               
                if (VencedorRodada1 == 1)
                {
                    Jogador1.PontuacaoGeral += valorRodada;

                    VencedorRodada1 = 0;
                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }
                else if(VencedorRodada1 == 2)
                {
                    Jogador2.PontuacaoGeral += valorRodada;

                    VencedorRodada1 = 0;
                    Jogador1.PontuacaoRodada = 0;
                    Jogador2.PontuacaoRodada = 0;
                    Distribuir();
                }
                else
                {
                    if (rodada == 0)
                    {
                        VencedorRodada1 = 0;
                        Jogador1.PontuacaoRodada = 0;
                        Jogador2.PontuacaoRodada = 0;
                        Distribuir();
                    }
                    else
                    {
                        Jogador1.PontuacaoRodada++;
                        Jogador2.PontuacaoRodada++;
                    }
                }
            }

            Jogador1.CartaSelecionada = null;
            Jogador2.CartaSelecionada = null;
        }

        public Tuple<Player, Player> OrdemJogadoresAtuais()
        {
            if (VezJogador1)
                return new Tuple<Player, Player>(Jogador1, Jogador2);
            else
                return new Tuple<Player, Player>(Jogador2, Jogador1);
        }

        public EstadoRodada ObterEstadoRodada()
        {
            return new EstadoRodada
            {
                CartaJogadaPlayer1 = Jogador1.CartaSelecionada,
                CartaJogadaPlayer2 = Jogador2.CartaSelecionada,
                PontosPlayer1 = Jogador1.PontuacaoGeral,
                PontosPlayer2 = Jogador2.PontuacaoGeral,
                RodadaPlayer1 = Jogador1.PontuacaoRodada,
                RodadaPlayer2 = Jogador2.PontuacaoRodada,
                ValorManilha = ValorManilha
            };
        }
    }
}
