using Domain.Entities;
using TicTacToe.Domain.Interfaces;

namespace TicTacToe.Domain.Services
{
    /// <summary>
    /// Repositorio del juego
    /// CORREGIDO: Ya NO es static, ahora es inyectable
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private Game _game;

        public GameRepository()
        {
            _game = new Game();
        }

        public Game GetGame()
        {
            return _game;
        }

        public void Reset()
        {
            _game = new Game();
        }
    }
}