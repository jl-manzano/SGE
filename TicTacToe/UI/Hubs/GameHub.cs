using Microsoft.AspNetCore.SignalR;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Services;
using Domain.Entities;

namespace TicTacToe.Presentation.Hubs
{
    public class GameHub : Hub
    {
        private readonly IUseCases _useCases;
        private readonly GameService _gameService;
        private readonly IRoomRepository _roomRepository;
        private readonly RoomService _roomService;
        private static readonly Dictionary<string, string> _connectionRooms = new Dictionary<string, string>();

        public GameHub(IUseCases useCases, GameService gameService, IRoomRepository roomRepository, RoomService roomService)
        {
            _useCases = useCases;
            _gameService = gameService;
            _roomRepository = roomRepository;
            _roomService = roomService;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine($"✅ Cliente conectado: {Context.ConnectionId}");
            await SendRoomListToAll();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine($"🔌 Cliente desconectado: {Context.ConnectionId}");

            try
            {
                if (_connectionRooms.ContainsKey(Context.ConnectionId))
                {
                    string roomId = _connectionRooms[Context.ConnectionId];
                    _connectionRooms.Remove(Context.ConnectionId);

                    Room? room = _roomService.LeaveRoom(roomId, Context.ConnectionId);

                    if (room != null)
                    {
                        await Clients.Group(roomId).SendAsync("GameStateUpdated", CreateGameStateDto(room.Game));
                    }

                    await SendRoomListToAll();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en desconexión: {ex.Message}");
            }
        }

        public async Task CreateRoom(string roomName)
        {
            Console.WriteLine($"🏗️ Creando sala: {roomName}");
            try
            {
                Room room = _roomService.CreateRoom(roomName);
                Console.WriteLine($"✅ Sala creada: {room.RoomId} - {room.RoomName}");
                await SendRoomListToAll();
                await Clients.Caller.SendAsync("RoomCreated", new { roomId = room.RoomId, roomName = room.RoomName });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear sala: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task JoinRoom(string roomId, string playerName = "Jugador")
        {
            Console.WriteLine($"🚪 {Context.ConnectionId} intentando unirse a sala {roomId}");
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                Room room = _roomService.JoinRoom(roomId, Context.ConnectionId, playerName);
                _connectionRooms[Context.ConnectionId] = roomId;

                Console.WriteLine($"✅ {Context.ConnectionId} se unió a sala {roomId}");

                await Clients.Group(roomId).SendAsync("GameStateUpdated", CreateGameStateDto(room.Game));
                await SendRoomListToAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al unirse a sala: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        // ✅ NUEVO: Método para salir voluntariamente de una sala
        public async Task LeaveRoom()
        {
            Console.WriteLine($"🚪 {Context.ConnectionId} saliendo de la sala voluntariamente");

            try
            {
                if (!_connectionRooms.ContainsKey(Context.ConnectionId))
                {
                    Console.WriteLine($"⚠️ {Context.ConnectionId} no está en ninguna sala");
                    return;
                }

                string roomId = _connectionRooms[Context.ConnectionId];
                _connectionRooms.Remove(Context.ConnectionId);

                // Remover del grupo de SignalR
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

                // Remover del juego
                Room? room = _roomService.LeaveRoom(roomId, Context.ConnectionId);

                Console.WriteLine($"✅ {Context.ConnectionId} salió de sala {roomId}");

                // Si la sala aún existe, notificar a los demás jugadores
                if (room != null)
                {
                    await Clients.Group(roomId).SendAsync("GameStateUpdated", CreateGameStateDto(room.Game));
                    Console.WriteLine($"📤 Estado actualizado enviado a sala {roomId}");
                }
                else
                {
                    Console.WriteLine($"🗑️ Sala {roomId} eliminada (estaba vacía)");
                }

                // Actualizar lista de salas para todos
                await SendRoomListToAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al salir de sala: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task MakeMove(int position)
        {
            Console.WriteLine($"📥 Movimiento en posición {position}");
            try
            {
                if (!_connectionRooms.ContainsKey(Context.ConnectionId))
                    throw new InvalidOperationException("No estás en ninguna sala");

                string roomId = _connectionRooms[Context.ConnectionId];
                Room? room = _roomRepository.GetRoom(roomId);

                if (room == null)
                    throw new InvalidOperationException("La sala no existe");

                _gameService.MakeMove(room.Game, Context.ConnectionId, position);

                await Clients.Group(roomId).SendAsync("GameStateUpdated", CreateGameStateDto(room.Game));
                Console.WriteLine($"✅ Movimiento procesado en sala {roomId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task ResetGame()
        {
            Console.WriteLine($"🔄 Reinicio solicitado por {Context.ConnectionId}");
            try
            {
                if (!_connectionRooms.ContainsKey(Context.ConnectionId))
                    throw new InvalidOperationException("No estás en ninguna sala");

                string roomId = _connectionRooms[Context.ConnectionId];
                Room? room = _roomRepository.GetRoom(roomId);

                if (room == null)
                    throw new InvalidOperationException("La sala no existe");

                _gameService.ResetGame(room.Game);

                await Clients.Group(roomId).SendAsync("GameStateUpdated", CreateGameStateDto(room.Game));
                Console.WriteLine($"✅ Juego reiniciado en sala {roomId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al reiniciar: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task GetRoomList()
        {
            try
            {
                List<object> rooms = _roomService.GetRoomList();
                await Clients.Caller.SendAsync("RoomListUpdated", rooms);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener salas: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        private async Task SendRoomListToAll()
        {
            try
            {
                List<object> rooms = _roomService.GetRoomList();
                await Clients.All.SendAsync("RoomListUpdated", rooms);
                Console.WriteLine($"📤 Lista de salas enviada ({rooms.Count} salas)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al enviar lista: {ex.Message}");
            }
        }

        private object CreateGameStateDto(Game game)
        {
            bool waitingForPlayer = _gameService.IsWaitingForPlayer(game);

            object? playerXDto = game.PlayerX != null ? new
            {
                connectionId = game.PlayerX.ConnectionId,
                symbol = game.PlayerX.Symbol,
                name = game.PlayerX.Name
            } : null;

            object? playerODto = game.PlayerO != null ? new
            {
                connectionId = game.PlayerO.ConnectionId,
                symbol = game.PlayerO.Symbol,
                name = game.PlayerO.Name
            } : null;

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