namespace Equipe6.Truco
{
    public class EstadoRodada
    {
        public ValorCarta ValorManilha { get; set; }

        public Carta CartaJogadaPlayer1 { get; set; }

        public Carta CartaJogadaPlayer2 { get; set; }

        public int PontosPlayer1 { get; set; }

        public int PontosPlayer2 { get; set; }

        public int RodadaPlayer1 { get; set; }

        public int RodadaPlayer2 { get; set; }
    }
}