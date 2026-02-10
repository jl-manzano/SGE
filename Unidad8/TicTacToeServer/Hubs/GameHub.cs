using Microsoft.AspNetCore.SignalR;
using TicTacToeServer.Entities;

namespace TicTacToeServer.Hubs
{
    /// <summary>
    /// Hub de SignalR para gestionar la comunicación en tiempo real del juego Tic Tac Toe.
    /// Maneja conexiones, salas y retransmisión de movimientos entre jugadores.
    /// </summary>
    public class GameHub : Hub
    {
        /// <summary>
        /// Diccionario de salas activas indexadas por RoomId.
        /// </summary>
        private static readonly Dictionary<string, Room> _rooms = new Dictionary<string, Room>();

        /// <summary>
        /// Mapeo de ConnectionId a RoomId para rastrear en qué sala está cada conexión.
        /// </summary>
        private static readonly Dictionary<string, string> _connectionRooms = new Dictionary<string, string>();

        /// <summary>
        /// Contador incremental para generar IDs únicos de sala.
        /// </summary>
        private static int _roomCounter = 0;

        /// <summary>
        /// Se ejecuta cuando un cliente se conecta al hub.
        /// Envía la lista de salas actualizada al nuevo cliente.
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await SendRoomListToAll();
        }

        /// <summary>
        /// Se ejecuta cuando un cliente se desconecta del hub.
        /// Notifica al oponente y limpia la sala si es necesario.
        /// </summary>
        /// <param name="exception">Excepción que causó la desconexión, si la hay.</param>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);

            try
            {
                if (_connectionRooms.ContainsKey(Context.ConnectionId))
                {
                    string roomId = _connectionRooms[Context.ConnectionId];
                    _connectionRooms.Remove(Context.ConnectionId);

                    await Clients.OthersInGroup(roomId).SendAsync("OpponentDisconnected");

                    LeaveRoomInternal(roomId, Context.ConnectionId);
                    await SendRoomListToAll();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en desconexión: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea una nueva sala de juego.
        /// </summary>
        /// <param name="roomName">Nombre de la sala. Si está vacío, se genera uno automático.</param>
        public async Task CreateRoom(string roomName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(roomName))
                    roomName = $"Sala {DateTime.Now:HH:mm}";

                _roomCounter++;
                string roomId = $"room_{_roomCounter}";

                var room = new Room(roomId, roomName);
                _rooms[roomId] = room;

                await SendRoomListToAll();
                await Clients.Caller.SendAsync("RoomCreated", new { roomId, roomName });
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        /// <summary>
        /// Une al cliente actual a una sala existente.
        /// Asigna símbolo X o O según disponibilidad.
        /// </summary>
        /// <param name="roomId">ID de la sala a unirse.</param>
        /// <param name="playerName">Nombre del jugador.</param>
        public async Task JoinRoom(string roomId, string playerName = "Jugador")
        {
            try
            {
                if (!_rooms.ContainsKey(roomId))
                    throw new InvalidOperationException("La sala no existe");

                var room = _rooms[roomId];

                if (room.IsFull())
                    throw new InvalidOperationException("La sala está llena");

                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

                string symbol = room.PlayerX == null ? "X" : "O";

                var player = new Player(Context.ConnectionId, symbol, $"{playerName} {symbol}");

                if (symbol == "X")
                    room.PlayerX = player;
                else
                    room.PlayerO = player;

                _connectionRooms[Context.ConnectionId] = roomId;

                if (room.PlayerX != null)
                {
                    await Clients.Group(roomId).SendAsync("PlayerJoined", new
                    {
                        connectionId = room.PlayerX.ConnectionId,
                        symbol = room.PlayerX.Symbol,
                        playerName = room.PlayerX.Name,
                        playerCount = room.GetPlayerCount()
                    });
                }

                if (room.PlayerO != null)
                {
                    await Clients.Group(roomId).SendAsync("PlayerJoined", new
                    {
                        connectionId = room.PlayerO.ConnectionId,
                        symbol = room.PlayerO.Symbol,
                        playerName = room.PlayerO.Name,
                        playerCount = room.GetPlayerCount()
                    });
                }

                await SendRoomListToAll();
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        /// <summary>
        /// Retransmite un movimiento a todos los jugadores de la sala.
        /// </summary>
        /// <param name="position">Posición del tablero (0-8).</param>
        public async Task BroadcastMove(int position)
        {
            try
            {
                if (!_connectionRooms.ContainsKey(Context.ConnectionId))
                    throw new InvalidOperationException("No estás en ninguna sala");

                string roomId = _connectionRooms[Context.ConnectionId];

                await Clients.Group(roomId).SendAsync("MoveBroadcasted", new
                {
                    connectionId = Context.ConnectionId,
                    position = position
                });
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        /// <summary>
        /// Retransmite la solicitud de reinicio del juego a todos los jugadores de la sala.
        /// </summary>
        public async Task BroadcastReset()
        {
            try
            {
                if (!_connectionRooms.ContainsKey(Context.ConnectionId))
                    throw new InvalidOperationException("No estás en ninguna sala");

                string roomId = _connectionRooms[Context.ConnectionId];

                await Clients.Group(roomId).SendAsync("ResetBroadcasted", new
                {
                    connectionId = Context.ConnectionId
                });
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        /// <summary>
        /// Permite al cliente abandonar la sala actual.
        /// Notifica al oponente y limpia la sala si queda vacía.
        /// </summary>
        public async Task LeaveRoom()
        {
            try
            {
                if (!_connectionRooms.ContainsKey(Context.ConnectionId))
                    return;

                string roomId = _connectionRooms[Context.ConnectionId];
                _connectionRooms.Remove(Context.ConnectionId);

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
                await Clients.OthersInGroup(roomId).SendAsync("OpponentLeft");

                LeaveRoomInternal(roomId, Context.ConnectionId);
                await SendRoomListToAll();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al salir: {ex.Message}");
            }
        }

        /// <summary>
        /// Envía la lista de salas disponibles al cliente que lo solicita.
        /// </summary>
        public async Task GetRoomList()
        {
            try
            {
                var roomList = GetRoomListInternal();
                await Clients.Caller.SendAsync("RoomListUpdated", roomList);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        /// <summary>
        /// Remueve un jugador de una sala internamente.
        /// Elimina la sala si queda vacía.
        /// </summary>
        /// <param name="roomId">ID de la sala.</param>
        /// <param name="connectionId">ID de conexión del jugador a remover.</param>
        private void LeaveRoomInternal(string roomId, string connectionId)
        {
            if (!_rooms.ContainsKey(roomId))
                return;

            var room = _rooms[roomId];

            if (room.PlayerX?.ConnectionId == connectionId)
                room.PlayerX = null;
            else if (room.PlayerO?.ConnectionId == connectionId)
                room.PlayerO = null;

            if (room.IsEmpty())
            {
                _rooms.Remove(roomId);
            }
        }

        /// <summary>
        /// Genera la lista de salas con su información para enviar a los clientes.
        /// </summary>
        /// <returns>Lista de objetos anónimos con datos de cada sala.</returns>
        private List<object> GetRoomListInternal()
        {
            return _rooms.Values.Select(room => new
            {
                roomId = room.RoomId,
                roomName = room.RoomName,
                playerCount = room.GetPlayerCount(),
                maxPlayers = 2,
                isFull = room.IsFull(),
                createdAt = room.CreatedAt
            }).ToList<object>();
        }

        /// <summary>
        /// Envía la lista actualizada de salas a todos los clientes conectados.
        /// </summary>
        private async Task SendRoomListToAll()
        {
            try
            {
                var roomList = GetRoomListInternal();
                await Clients.All.SendAsync("RoomListUpdated", roomList);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al enviar lista: {ex.Message}");
            }
        }
    }
}