using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Equipe6.Truco
{
    public class Baralho
    {
        public List<Carta> Cartas = new List<Carta>();

        public void GerarBaralho()
        {
            List<Carta> cartas = new List<Carta>();
            
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var carta = new Carta
                    {
                        naipe = (Naipes)j,
                        valor = (ValorCarta)i
                    };

                    cartas.Add(carta);
                }
            }

            Random r = new Random();
            Cartas = cartas.OrderBy(x => r.Next()).ToList();
        }

        public Carta GetTopCard()
        {
            Carta c = Cartas[0];
            Cartas.RemoveAt(0);

            return c;
        }
    }
}
