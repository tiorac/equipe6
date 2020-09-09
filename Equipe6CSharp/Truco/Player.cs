using System.Collections.Generic;
using System.Linq;

namespace Equipe6.Truco
{
    public class Player
    {
        public Carta Carta1 { get; set; }
        public Carta Carta2 { get; set; }
        public Carta Carta3 { get; set; }

        public Carta CartaSelecionada { get; set; }

        public IControlador Controlado { get; set; }

        public int PontuacaoRodada;
        public int PontuacaoGeral;

        public bool SelecionarCarta(Carta carta)
        {
            if (Carta1 == carta)
            {
                CartaSelecionada = carta;
                Carta1 = null;
                return true;
            }

            if (Carta2 == carta)
            {
                CartaSelecionada = carta;
                Carta2 = null;
                return true;
            }

            if (Carta3 == carta)
            {
                CartaSelecionada = carta;
                Carta3 = null;
                return true;
            }

            return false;
        }

        public List<Carta> CartasEmOrdem(ValorCarta valorManilha)
        {
            var lista = new List<Carta>();

            if (Carta1 != null)
                lista.Add(Carta1);

            if (Carta2 != null)
                lista.Add(Carta2);

            if (Carta3 != null)
                lista.Add(Carta3);

            return lista.OrderBy(a => a.PesoCarta(valorManilha)).ToList();
        }
    }
}
