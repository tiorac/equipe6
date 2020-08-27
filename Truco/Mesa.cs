using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6.Truco
{
    public class Mesa
    {
        Player Jogador1;
        Player Jogador2;
        Baralho Baralho;
        Carta CartaVirada = new Carta();

        public void teste()
        {
            Jogador1 = new Player();
            Jogador2 = new Player();
            Baralho = new Baralho();

            Baralho.GerarBaralho();
            Distribuir();
            
        }

        public void Distribuir()
        {
            CartaVirada = Baralho.GetTopCard();

            Jogador1.Carta1 = Baralho.GetTopCard();
            Jogador1.Carta2 = Baralho.GetTopCard();
            Jogador1.Carta3 = Baralho.GetTopCard();

            Jogador2.Carta1 = Baralho.GetTopCard();
            Jogador2.Carta2 = Baralho.GetTopCard();
            Jogador2.Carta3 = Baralho.GetTopCard();
        }


    }
}
