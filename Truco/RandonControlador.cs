using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Equipe6.Truco
{
    public class RandonControlador : IControlador
    {
        public RandonControlador()
        {
            Random = new Random();
        }

        public Random Random { get; set; }

        public Carta Jogar(EstadoRodada estado, Player jogador)
        {
            var listaCartas = new List<Carta>();

            if (jogador.Carta1 != null)
                listaCartas.Add(jogador.Carta1);

            if (jogador.Carta2 != null)
                listaCartas.Add(jogador.Carta2);

            if (jogador.Carta3 != null)
                listaCartas.Add(jogador.Carta3);

            listaCartas = listaCartas.OrderBy(x => Random.Next()).ToList();
            return listaCartas.FirstOrDefault();
        }

        public void VerJogada(EstadoRodada estado, Player jogador)
        {
            
        }
    }
}
