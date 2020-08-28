using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Equipe6.Truco
{
    public class BasicoControlador : IControlador
    {
        public Carta Jogar(EstadoRodada estado, Player jogador)
        {
            var rodada = jogador.CartasEmOrdem(estado.ValorManilha).Count();

            switch (rodada)
            {
                case 3:
                    return EstrategiaRodada1(estado, jogador);

                case 2:
                    return EstrategiaRodada2(estado, jogador);

                case 1:
                default:
                    return EstrategiaRodada3(estado, jogador);
            }
        }

        public void VerJogada(EstadoRodada estado, Player jogador)
        {
            //TODO: Guardar Histórico ou limpar ele...
        }

        private Carta EstrategiaRodada1(EstadoRodada estado, Player jogador)
        {
            var cartasOrdem = jogador.CartasEmOrdem(estado.ValorManilha);

            if (estado.CartaJogadaPlayer1 == null)
            {    
                return cartasOrdem[1];
            }
            else
            {
                if (cartasOrdem[1].PesoCarta(estado.ValorManilha) > 10)
                    return cartasOrdem.First();

                if (cartasOrdem.Last().PesoCarta(estado.ValorManilha) > estado.CartaJogadaPlayer1.PesoCarta(estado.ValorManilha))
                {
                    if (cartasOrdem[1].PesoCarta(estado.ValorManilha) > estado.CartaJogadaPlayer1.PesoCarta(estado.ValorManilha))
                    {
                        if (cartasOrdem.First().PesoCarta(estado.ValorManilha) > estado.CartaJogadaPlayer1.PesoCarta(estado.ValorManilha))
                        {
                            return cartasOrdem.First();
                        }

                        return cartasOrdem[1];
                    }

                    return cartasOrdem.Last();
                }

                if (cartasOrdem.Last().PesoCarta(estado.ValorManilha) == estado.CartaJogadaPlayer1.PesoCarta(estado.ValorManilha))
                    return cartasOrdem.Last();

                return cartasOrdem.First();
            }
        }

        private Carta EstrategiaRodada2(EstadoRodada estado, Player jogador)
        {
            var cartasOrdem = jogador.CartasEmOrdem(estado.ValorManilha);

            if (estado.CartaJogadaPlayer1 == null)
            {
                if (estado.RodadaPlayer2 > estado.RodadaPlayer1)
                {
                    if (cartasOrdem.Last().PesoCarta(estado.ValorManilha) >= 8)
                    {
                        //TODO: Iria esconder...
                        return cartasOrdem.First();
                    }
                    else
                    {
                        return cartasOrdem.Last();
                    }
                }
                else
                    return cartasOrdem.Last();
            }
            else
            {
                if (estado.RodadaPlayer2 > estado.RodadaPlayer1)
                {
                    if (cartasOrdem.Last().PesoCarta(estado.ValorManilha) >= estado.CartaJogadaPlayer1.PesoCarta(estado.ValorManilha))
                    {
                        return cartasOrdem.Last();
                    }
                    else
                    {
                        return cartasOrdem.First();
                    }
                }
                else
                {
                    if (cartasOrdem.Last().PesoCarta(estado.ValorManilha) > estado.CartaJogadaPlayer1.PesoCarta(estado.ValorManilha))
                    {
                        if (cartasOrdem.First().PesoCarta(estado.ValorManilha) > estado.CartaJogadaPlayer1.PesoCarta(estado.ValorManilha))
                        {
                            return cartasOrdem.First();
                        }

                        return cartasOrdem.Last();
                    }
                    else
                    {
                        return cartasOrdem.First();
                    }
                }
            }
        }

        private Carta EstrategiaRodada3(EstadoRodada estado, Player jogador)
        {
            return jogador.CartasEmOrdem(estado.ValorManilha).FirstOrDefault();
        }
    }
}
