using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6.Truco
{
    public static class CartaHelper
    {
        public static int PesoCarta(this Carta carta, ValorCarta valorManilha)
        {
            if (carta.Valor == valorManilha)
                return 10 + (int)carta.Naipe;

            return (int)carta.Valor;
        }
    }
}
