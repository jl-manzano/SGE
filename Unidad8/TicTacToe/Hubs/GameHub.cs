using Microsoft.AspNetCore.SignalR;
using TicTacToe.Models;
using System.Numerics;

namespace TicTacToe.Hubs
{
    /// <summary>
    /// Hub de SignalR para el juego de Tres en Raya
    /// El servidor NO tiene vista, solo gestiona la lógica del juego
    /// </summary>
    public class GameHub : Hub
    {
        // Estado del juego compartido (en producción usar Redis o similar)
        private static GameState _gameState = new GameState();
        private static readonly object _lock = new object();

        /// <summary>
        /// Evento: Cliente conectado
        /// Asigna al jugador como X u O según orden de llegada
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine($"✅ Cliente conectado: {Context.ConnectionId}");

            lock (_lock)
            {
                // Asignar jugador según disponibilidad
                if (_gameState.PlayerX == null)
                {
                    _gameState.PlayerX = new Player(Context.ConnectionId, "X", "Jugador X");
                    Console.WriteLine($"🎮 Jugador X asignado: {Context.ConnectionId}");
                }
                else if (_gameState.PlayerO == null)
                {
                    _gameState.PlayerO = new Player(Context.ConnectionId, "O", "Jugador O");
                    _gameState.WaitingForPlayer = false;
                    Console.WriteLine($"🎮 Jugador O asignado: {Context.ConnectionId}");
                }
                else
                {
                    Console.WriteLine($"👀 Espectador conectado: {Context.ConnectionId}");
                }
            }

            // Notificar a TODOS los clientes del nuevo estado
            await Clients.All.SendAsync("GameStateUpdated", _gameState);
        }

        /// <summary>
        /// Evento: Cliente desconectado
        /// Si un jugador se desconecta, reinicia el juego
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine($"🔌 Cliente desconectado: {Context.ConnectionId}");

            lock (_lock)
            {
                // Si un jugador se desconecta, reiniciar
                if (_gameState.PlayerX?.ConnectionId == Context.ConnectionId ||
                    _gameState.PlayerO?.ConnectionId == Context.ConnectionId)
                {
                    Console.WriteLine("🔄 Reiniciando juego por desconexión");
                    _gameState = new GameState();
                }
            }

            await Clients.All.SendAsync("GameStateUpdated", _gameState);
        }

        /// <summary>
        /// Acción: Realizar un movimiento en el tablero
        /// Valida el movimiento y notifica a todos los clientes
        /// </summary>
        public async Task MakeMove(int position)
        {
            try
            {
                Console.WriteLine($"📥 MakeMove - Position: {position}, ConnectionId: {Context.ConnectionId}");

                lock (_lock)
                {
                    // Validar que el juego no haya terminado
                    if (_gameState.GameOver)
                    {
                        Console.WriteLine("❌ El juego ya terminó");
                        return;
                    }

                    // Validar que haya dos jugadores
                    if (_gameState.WaitingForPlayer)
                    {
                        Console.WriteLine("❌ Esperando al segundo jugador");
                        return;
                    }

                    // Determinar quién está jugando
                    string playerSymbol = "";
                    if (_gameState.PlayerX?.ConnectionId == Context.ConnectionId)
                        playerSymbol = "X";
                    else if (_gameState.PlayerO?.ConnectionId == Context.ConnectionId)
                        playerSymbol = "O";
                    else
                    {
                        Console.WriteLine("❌ Jugador no válido");
                        return;
                    }

                    // Validar que sea su turno
                    if (playerSymbol != _gameState.CurrentTurn)
                    {
                        Console.WriteLine($"❌ No es el turno de {playerSymbol}");
                        return;
                    }

                    // Validar posición
                    if (position < 0 || position > 8)
                    {
                        Console.WriteLine($"❌ Posición inválida: {position}");
                        return;
                    }

                    // Validar que la casilla esté vacía
                    if (!string.IsNullOrEmpty(_gameState.Board[position]))
                    {
                        Console.WriteLine($"❌ Casilla {position} ocupada");
                        return;
                    }

                    // ✅ REALIZAR MOVIMIENTO
                    _gameState.Board[position] = playerSymbol;
                    Console.WriteLine($"✅ Movimiento: {playerSymbol} → posición {position}");

                    // Verificar ganador
                    string? winner = _gameState.CheckWinner();
                    if (winner != null)
                    {
                        _gameState.Winner = winner;
                        _gameState.GameOver = true;
                        Console.WriteLine($"🏆 Juego terminado. Resultado: {winner}");
                    }
                    else
                    {
                        // Cambiar turno
                        _gameState.SwitchTurn();
                        Console.WriteLine($"🔄 Turno cambiado a: {_gameState.CurrentTurn}");
                    }
                }

                // Notificar a TODOS los clientes
                await Clients.All.SendAsync("GameStateUpdated", _gameState);
                Console.WriteLine("📤 Estado enviado a todos los clientes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en MakeMove: {ex.Message}");
            }
        }

        /// <summary>
        /// Acción: Reiniciar el juego
        /// Mantiene los jugadores pero limpia el tablero
        /// </summary>
        public async Task ResetGame()
        {
            try
            {
                Console.WriteLine($"🔄 ResetGame solicitado por: {Context.ConnectionId}");

                lock (_lock)
                {
                    // Mantener jugadores, reiniciar tablero
                    var playerX = _gameState.PlayerX;
                    var playerO = _gameState.PlayerO;
                    var waitingForPlayer = _gameState.WaitingForPlayer;

                    _gameState = new GameState
                    {
                        PlayerX = playerX,
                        PlayerO = playerO,
                        WaitingForPlayer = waitingForPlayer
                    };

                    Console.WriteLine("✅ Juego reiniciado");
                }

                await Clients.All.SendAsync("GameStateUpdated", _gameState);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en ResetGame: {ex.Message}");
            }
        }

        /// <summary>
        /// Acción: Obtener el estado actual del juego
        /// Útil cuando un cliente se reconecta
        /// </summary>
        public async Task GetGameState()
        {
            await Clients.Caller.SendAsync("GameStateUpdated", _gameState);
        }
    }
}
