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

        public int PontuacaoRodada;
        public int PontuacaoGeral;
    }
}
