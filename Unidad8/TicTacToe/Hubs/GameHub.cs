using Microsoft.AspNetCore.SignalR;
using TicTacToe.Models;

namespace TicTacToe.Hubs
{
    /// <summary>
    /// Hub de SignalR para el juego de Tres en Raya
    /// Versión mejorada con logging detallado
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
            Console.WriteLine($"╔══════════════════════════════════════════╗");
            Console.WriteLine($"║  ✅ CLIENTE CONECTADO                    ║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
            Console.WriteLine($"   Connection ID: {Context.ConnectionId}");

            lock (_lock)
            {
                // Asignar jugador según disponibilidad
                if (_gameState.PlayerX == null)
                {
                    _gameState.PlayerX = new Player(Context.ConnectionId, "X", "Jugador X");
                    Console.WriteLine($"   🎮 Rol asignado: JUGADOR X");
                }
                else if (_gameState.PlayerO == null)
                {
                    _gameState.PlayerO = new Player(Context.ConnectionId, "O", "Jugador O");
                    _gameState.WaitingForPlayer = false;
                    Console.WriteLine($"   🎮 Rol asignado: JUGADOR O");
                    Console.WriteLine($"   ✅ ¡Juego listo para comenzar!");
                }
                else
                {
                    Console.WriteLine($"   👀 Rol asignado: ESPECTADOR");
                }

                Console.WriteLine($"   Estado actual:");
                Console.WriteLine($"   - Player X: {(_gameState.PlayerX != null ? "Conectado" : "Vacío")}");
                Console.WriteLine($"   - Player O: {(_gameState.PlayerO != null ? "Conectado" : "Vacío")}");
                Console.WriteLine($"   - Esperando: {_gameState.WaitingForPlayer}");
            }

            // Notificar a TODOS los clientes del nuevo estado
            await Clients.All.SendAsync("GameStateUpdated", _gameState);
            Console.WriteLine($"   📤 Estado enviado a todos los clientes\n");
        }

        /// <summary>
        /// Evento: Cliente desconectado
        /// Si un jugador se desconecta, reinicia el juego
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine($"╔══════════════════════════════════════════╗");
            Console.WriteLine($"║  🔌 CLIENTE DESCONECTADO                 ║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
            Console.WriteLine($"   Connection ID: {Context.ConnectionId}");

            lock (_lock)
            {
                // Si un jugador se desconecta, reiniciar
                if (_gameState.PlayerX?.ConnectionId == Context.ConnectionId ||
                    _gameState.PlayerO?.ConnectionId == Context.ConnectionId)
                {
                    Console.WriteLine($"   🔄 Un jugador se desconectó - Reiniciando juego");
                    _gameState = new GameState();
                }
                else
                {
                    Console.WriteLine($"   👀 Un espectador se desconectó");
                }
            }

            await Clients.All.SendAsync("GameStateUpdated", _gameState);
            Console.WriteLine($"   📤 Estado actualizado enviado\n");
        }

        /// <summary>
        /// Acción: Realizar un movimiento en el tablero
        /// </summary>
        public async Task MakeMove(int position)
        {
            Console.WriteLine($"╔══════════════════════════════════════════╗");
            Console.WriteLine($"║  📥 MOVIMIENTO RECIBIDO                  ║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
            Console.WriteLine($"   Connection ID: {Context.ConnectionId}");
            Console.WriteLine($"   Posición: {position}");

            try
            {
                lock (_lock)
                {
                    // Validar que el juego no haya terminado
                    if (_gameState.GameOver)
                    {
                        Console.WriteLine($"   ❌ RECHAZADO: El juego ya terminó");
                        Console.WriteLine();
                        return;
                    }

                    // Validar que haya dos jugadores
                    if (_gameState.WaitingForPlayer)
                    {
                        Console.WriteLine($"   ❌ RECHAZADO: Esperando al segundo jugador");
                        Console.WriteLine();
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
                        Console.WriteLine($"   ❌ RECHAZADO: Jugador no válido (espectador)");
                        Console.WriteLine();
                        return;
                    }

                    Console.WriteLine($"   Jugador identificado: {playerSymbol}");

                    // Validar que sea su turno
                    if (playerSymbol != _gameState.CurrentTurn)
                    {
                        Console.WriteLine($"   ❌ RECHAZADO: No es el turno de {playerSymbol}");
                        Console.WriteLine($"   Turno actual: {_gameState.CurrentTurn}");
                        Console.WriteLine();
                        return;
                    }

                    // Validar posición
                    if (position < 0 || position > 8)
                    {
                        Console.WriteLine($"   ❌ RECHAZADO: Posición inválida ({position})");
                        Console.WriteLine();
                        return;
                    }

                    // Validar que la casilla esté vacía
                    if (!string.IsNullOrEmpty(_gameState.Board[position]))
                    {
                        Console.WriteLine($"   ❌ RECHAZADO: Casilla {position} ocupada por '{_gameState.Board[position]}'");
                        Console.WriteLine();
                        return;
                    }

                    // ✅ REALIZAR MOVIMIENTO
                    _gameState.Board[position] = playerSymbol;
                    Console.WriteLine($"   ✅ MOVIMIENTO ACEPTADO");
                    Console.WriteLine($"   Tablero actualizado: [{string.Join(", ", _gameState.Board.Select(c => string.IsNullOrEmpty(c) ? "_" : c))}]");

                    // Verificar ganador
                    string? winner = _gameState.CheckWinner();
                    if (winner != null)
                    {
                        _gameState.Winner = winner;
                        _gameState.GameOver = true;
                        if (winner == "draw")
                            Console.WriteLine($"   🤝 RESULTADO: ¡EMPATE!");
                        else
                            Console.WriteLine($"   🏆 RESULTADO: ¡Ganó {winner}!");
                    }
                    else
                    {
                        // Cambiar turno
                        _gameState.SwitchTurn();
                        Console.WriteLine($"   🔄 Turno cambiado a: {_gameState.CurrentTurn}");
                    }
                }

                // Notificar a TODOS los clientes
                await Clients.All.SendAsync("GameStateUpdated", _gameState);
                Console.WriteLine($"   📤 Estado enviado a todos los clientes\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ❌ ERROR: {ex.Message}");
                Console.WriteLine($"   Stack: {ex.StackTrace}\n");
            }
        }

        /// <summary>
        /// Acción: Reiniciar el juego
        /// </summary>
        public async Task ResetGame()
        {
            Console.WriteLine($"╔══════════════════════════════════════════╗");
            Console.WriteLine($"║  🔄 REINICIO DE JUEGO SOLICITADO         ║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
            Console.WriteLine($"   Connection ID: {Context.ConnectionId}");

            try
            {
                lock (_lock)
                {
                    // Guardar jugadores actuales
                    var playerX = _gameState.PlayerX;
                    var playerO = _gameState.PlayerO;
                    var waitingForPlayer = (playerX == null || playerO == null);

                    Console.WriteLine($"   Estado antes del reinicio:");
                    Console.WriteLine($"   - Player X: {(playerX != null ? playerX.ConnectionId : "null")}");
                    Console.WriteLine($"   - Player O: {(playerO != null ? playerO.ConnectionId : "null")}");
                    Console.WriteLine($"   - WaitingForPlayer: {_gameState.WaitingForPlayer}");

                    // Reiniciar manteniendo los jugadores
                    _gameState = new GameState
                    {
                        PlayerX = playerX,
                        PlayerO = playerO,
                        WaitingForPlayer = waitingForPlayer
                    };

                    Console.WriteLine($"   ✅ Juego reiniciado");
                    Console.WriteLine($"   Estado después del reinicio:");
                    Console.WriteLine($"   - Tablero: [{string.Join(", ", _gameState.Board.Select(c => string.IsNullOrEmpty(c) ? "_" : c))}]");
                    Console.WriteLine($"   - Turno actual: {_gameState.CurrentTurn}");
                    Console.WriteLine($"   - Game Over: {_gameState.GameOver}");
                    Console.WriteLine($"   - Winner: {_gameState.Winner ?? "null"}");
                    Console.WriteLine($"   - WaitingForPlayer: {_gameState.WaitingForPlayer}");
                }

                // Notificar a TODOS los clientes
                await Clients.All.SendAsync("GameStateUpdated", _gameState);
                Console.WriteLine($"   📤 Estado enviado a todos los clientes\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ❌ ERROR en ResetGame: {ex.Message}");
                Console.WriteLine($"   Stack: {ex.StackTrace}\n");
            }
        }

        /// <summary>
        /// Acción: Obtener el estado actual del juego
        /// </summary>
        public async Task GetGameState()
        {
            Console.WriteLine($"╔══════════════════════════════════════════╗");
            Console.WriteLine($"║  📥 SOLICITUD DE ESTADO                  ║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
            Console.WriteLine($"   Connection ID: {Context.ConnectionId}");
            Console.WriteLine($"   Tablero: [{string.Join(", ", _gameState.Board.Select(c => string.IsNullOrEmpty(c) ? "_" : c))}]");
            Console.WriteLine($"   Turno: {_gameState.CurrentTurn}");
            Console.WriteLine($"   Game Over: {_gameState.GameOver}");

            await Clients.Caller.SendAsync("GameStateUpdated", _gameState);
            Console.WriteLine($"   📤 Estado enviado al cliente\n");
        }
    }
}