using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6.Truco
{
    public class Player
    {
        public Carta Carta1 { get; set; }
        public Carta Carta2 { get; set; }
        public Carta Carta3 { get; set; }

        public Carta CartaSelecionada { get; set; }

        public int PontuacaoRodada;
        public int PontuacaoGeral;

        public void SelecionarCarta(Carta carta)
        {
            if (Carta1 == carta)
            {
                CartaSelecionada = carta;
                Carta1 = null;
            }

            if (Carta2 == carta)
            {
                CartaSelecionada = carta;
                Carta2 = null;
            }

            if (Carta3 == carta)
            {
                CartaSelecionada = carta;
                Carta3 = null;
            }
        }
    }
}
