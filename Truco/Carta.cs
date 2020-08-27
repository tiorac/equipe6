using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6.Truco
{
    public class Carta
    {
        public ValorCarta valor;
        public Naipes naipe;

        public ValorCarta Valor
        {
            get
            {
                return valor;
            }
        }

        public Naipes Naipe
        {
            get
            {
                return naipe;
            }
        }

        public string LabelValorCarta
        {
            get
            {
                switch (this.Valor)
                {
                    case ValorCarta.Quatro:
                        return "4";
                    case ValorCarta.Cinco:
                        return "5";
                    case ValorCarta.Seis:
                        return "6";
                    case ValorCarta.Sete:
                        return "7";
                    case ValorCarta.Dama:
                        return "Q";
                    case ValorCarta.Valete:
                        return "J";
                    case ValorCarta.Rei:
                        return "K";
                    case ValorCarta.As:
                        return "A";
                    case ValorCarta.Dois:
                        return "2";
                    case ValorCarta.Tres:
                        return "3";
                    default:
                        return "";
                }
            }
        }

        public string LabelNaipeCarta
        {
            get
            {
                switch (this.Naipe)
                {
                    case Naipes.Paus:
                        return "♣";
                    case Naipes.Copas:
                        return "♥";
                    case Naipes.Espada:
                        return "♠";
                    case Naipes.Ouros:
                        return "♦";
                    default:
                        return "";
                }

            }
        }

        public string Cor
        {
            get
            {
                switch (this.Naipe)
                {
                    case Naipes.Copas:
                    case Naipes.Ouros:
                        return "#FF0000";

                    case Naipes.Paus:
                    case Naipes.Espada:
                    default:
                        return "#000000";
                }
            }
        }
    }
}
