namespace Equipe6.Truco
{
    public interface IControlador
    {
        Carta Jogar(EstadoRodada estado, Player jogador);

        void VerJogada(EstadoRodada estado, Player jogador);
    }
}
