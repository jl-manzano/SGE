using Domain.Entities;

namespace TicTacToe.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio del juego
    /// </summary>
    public interface IGameRepository
    {
        Game GetGame();
        void Reset();
    }
}