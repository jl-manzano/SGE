using Microsoft.AspNetCore.SignalR;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Services;
using Domain.Entities;

namespace TicTacToe.Presentation.Hubs
{
    /// <summary>
    /// Hub de SignalR - Capa de Presentación (UI)
    /// CORREGIDO: Ahora inyecta GameService en lugar de usar new
    /// </summary>
    public class GameHub : Hub
    {
        private readonly IUseCases _useCases;
        private readonly GameService _gameService;

        /// <summary>
        /// Constructor con INYECCIÓN DE DEPENDENCIAS
        /// CORREGIDO: Recibe GameService como parámetro
        /// </summary>
        public GameHub(IUseCases useCases, GameService gameService)
        {
            _useCases = useCases;
            _gameService = gameService;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine($"✅ Cliente conectado: {Context.ConnectionId}");

            try
            {
                Game game = _useCases.ConnectPlayer(
                    Context.ConnectionId,
                    "Jugador"
                );

                await Clients.All.SendAsync(
                    "GameStateUpdated",
                    CreateGameStateDto(game)
                );

                Console.WriteLine("📤 Estado enviado\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine($"🔌 Cliente desconectado: {Context.ConnectionId}");

            try
            {
                Game game = _useCases.DisconnectPlayer(Context.ConnectionId);

                await Clients.All.SendAsync(
                    "GameStateUpdated",
                    CreateGameStateDto(game)
                );

                Console.WriteLine("📤 Estado actualizado\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        public async Task MakeMove(int position)
        {
            Console.WriteLine($"📥 Movimiento en posición {position}");

            try
            {
                Game game = _useCases.MakeMove(
                    Context.ConnectionId,
                    position
                );

                await Clients.All.SendAsync(
                    "GameStateUpdated",
                    CreateGameStateDto(game)
                );

                Console.WriteLine("✅ Movimiento exitoso\n");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"❌ Rechazado: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Inválido: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
            catch (Exception)
            {
                await Clients.Caller.SendAsync(
                    "Error",
                    "Error al procesar movimiento"
                );
            }
        }

        public async Task ResetGame()
        {
            Console.WriteLine("🔄 Reinicio solicitado");

            try
            {
                Game game = _useCases.ResetGame();

                await Clients.All.SendAsync(
                    "GameStateUpdated",
                    CreateGameStateDto(game)
                );

                Console.WriteLine("✅ Juego reiniciado\n");
            }
            catch (Exception)
            {
                await Clients.Caller.SendAsync(
                    "Error",
                    "Error al reiniciar"
                );
            }
        }

        private object CreateGameStateDto(Game game)
        {
            bool waitingForPlayer = _gameService.IsWaitingForPlayer(game);

            object? playerXDto = null;
            if (game.PlayerX != null)
            {
                playerXDto = new
                {
                    connectionId = game.PlayerX.ConnectionId,
                    symbol = game.PlayerX.Symbol,
                    name = game.PlayerX.Name
                };
            }

            object? playerODto = null;
            if (game.PlayerO != null)
            {
                playerODto = new
                {
                    connectionId = game.PlayerO.ConnectionId,
                    symbol = game.PlayerO.Symbol,
                    name = game.PlayerO.Name
                };
            }

            return new
            {
                board = game.Board,
                currentTurn = game.CurrentTurn,
                winner = game.Winner,
                gameOver = game.GameOver,
                playerX = playerXDto,
                playerO = playerODto,
                waitingForPlayer = waitingForPlayer
            };
        }
    }
}