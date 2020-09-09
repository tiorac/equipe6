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
                return Valor switch
                {
                    ValorCarta.Quatro => "4",
                    ValorCarta.Cinco => "5",
                    ValorCarta.Seis => "6",
                    ValorCarta.Sete => "7",
                    ValorCarta.Dama => "Q",
                    ValorCarta.Valete => "J",
                    ValorCarta.Rei => "K",
                    ValorCarta.As => "A",
                    ValorCarta.Dois => "2",
                    ValorCarta.Tres => "3",
                    _ => "",
                };
            }
        }

        public string LabelNaipeCarta
        {
            get
            {
                return Naipe switch
                {
                    Naipes.Paus => "♣",
                    Naipes.Copas => "♥",
                    Naipes.Espada => "♠",
                    Naipes.Ouros => "♦",
                    _ => "",
                };
            }
        }

        public string Cor
        {
            get
            {
                switch (Naipe)
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
