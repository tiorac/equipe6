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
            Cartas = new List<Carta>();
            
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    var carta = new Carta
                    {
                        naipe = (Naipes)j,
                        valor = (ValorCarta)i
                    };

                    Cartas.Add(carta);
                }
            }

            Embaralhar();
        }

        private void Embaralhar()
        {
            Random r = new Random();
            Cartas = Cartas.OrderBy(x => r.Next()).ToList();
        }

        public Carta GetTopCard()
        {
            Carta c = Cartas[0];
            Cartas.RemoveAt(0);

            return c;
        }
    }
}
