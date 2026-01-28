using Domain.Entities;
using Microsoft.Extensions.Configuration;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Services;

namespace TicTacToe.Domain.UseCases
{
    public class UseCases : IUseCases
    {
        private readonly IConfiguration _configuration;
        private readonly IGameRepository _repository;
        private readonly GameService _gameService;

        /// <summary>
        /// Constructor con INYECCIÓN DE DEPENDENCIAS
        /// CORREGIDO: Ahora recibe todas las dependencias
        /// </summary>
        public UseCases(
            IConfiguration configuration,
            IGameRepository repository,
            GameService gameService)
        {
            _configuration = configuration;
            _repository = repository;
            _gameService = gameService;
        }

        public Game ConnectPlayer(string connectionId, string playerName)
        {
            var validation = ValidateGameTime();
            if (!validation.IsAllowed)
            {
                throw new InvalidOperationException(validation.Reason);
            }

            Game game = _repository.GetGame();
            _gameService.AssignPlayer(game, connectionId, playerName);
            return game;
        }

        public Game DisconnectPlayer(string connectionId)
        {
            Game game = _repository.GetGame();

            if (_gameService.HasPlayer(game, connectionId))
            {
                _repository.Reset();
                game = _repository.GetGame();
            }

            return game;
        }

        public Game MakeMove(string connectionId, int position)
        {
            var validation = ValidateGameTime();
            if (!validation.IsAllowed)
            {
                throw new InvalidOperationException(validation.Reason);
            }

            Game game = _repository.GetGame();
            _gameService.MakeMove(game, connectionId, position);
            return game;
        }

        public Game ResetGame()
        {
            Game game = _repository.GetGame();
            _gameService.ResetGame(game);
            return game;
        }

        public (bool IsAllowed, string? Reason) ValidateGameTime()
        {
            // Por ahora siempre permite jugar
            // Puedes implementar la lógica de restricción horaria aquí
            return (true, null);
        }
    }
}