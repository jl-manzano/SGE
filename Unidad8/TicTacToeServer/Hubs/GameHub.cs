using Microsoft.AspNetCore.SignalR;
using TicTacToeServer.Entities;

namespace TicTacToeServer.Hubs
{
    public class GameHub : Hub
    {
        private static readonly Dictionary<string, Room> _rooms = new Dictionary<string, Room>();
        private static readonly Dictionary<string, string> _connectionRooms = new Dictionary<string, string>();
        private static int _roomCounter = 0;

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

                    await Clients.OthersInGroup(roomId).SendAsync("OpponentDisconnected");

                    LeaveRoomInternal(roomId, Context.ConnectionId);
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
                if (string.IsNullOrWhiteSpace(roomName))
                    roomName = $"Sala {DateTime.Now:HH:mm}";

                _roomCounter++;
                string roomId = $"room_{_roomCounter}";

                var room = new Room(roomId, roomName);
                _rooms[roomId] = room;

                Console.WriteLine($"✅ Sala creada: {roomId} - {roomName}");
                await SendRoomListToAll();
                await Clients.Caller.SendAsync("RoomCreated", new { roomId, roomName });
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

                Console.WriteLine($"✅ {Context.ConnectionId} se unió como {symbol}");

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
                Console.WriteLine($"❌ Error al unirse: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task BroadcastMove(int position)
        {
            try
            {
                if (!_connectionRooms.ContainsKey(Context.ConnectionId))
                    throw new InvalidOperationException("No estás en ninguna sala");

                string roomId = _connectionRooms[Context.ConnectionId];

                Console.WriteLine($"📤 Retransmitiendo movimiento: posición {position} en sala {roomId}");

                await Clients.Group(roomId).SendAsync("MoveBroadcasted", new
                {
                    connectionId = Context.ConnectionId,
                    position = position
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task BroadcastReset()
        {
            try
            {
                if (!_connectionRooms.ContainsKey(Context.ConnectionId))
                    throw new InvalidOperationException("No estás en ninguna sala");

                string roomId = _connectionRooms[Context.ConnectionId];

                Console.WriteLine($"🔄 Retransmitiendo reinicio en sala {roomId}");

                await Clients.Group(roomId).SendAsync("ResetBroadcasted", new
                {
                    connectionId = Context.ConnectionId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

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

                Console.WriteLine($"👋 {Context.ConnectionId} salió de sala {roomId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al salir: {ex.Message}");
            }
        }

        public async Task GetRoomList()
        {
            try
            {
                var roomList = GetRoomListInternal();
                await Clients.Caller.SendAsync("RoomListUpdated", roomList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener salas: {ex.Message}");
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        private void LeaveRoomInternal(string roomId, string connectionId)
        {
            if (!_rooms.ContainsKey(roomId))
                return;

            var room = _rooms[roomId];

            if (room.PlayerX?.ConnectionId == connectionId)
                room.PlayerX = null;
            else if (room.PlayerO?.ConnectionId == connectionId)
                room.PlayerO = null;

            Console.WriteLine($"👋 Jugador {connectionId} salió de sala {roomId}");

            if (room.IsEmpty())
            {
                _rooms.Remove(roomId);
                Console.WriteLine($"🗑️ Sala eliminada: {roomId}");
            }
        }

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

        private async Task SendRoomListToAll()
        {
            try
            {
                var roomList = GetRoomListInternal();
                await Clients.All.SendAsync("RoomListUpdated", roomList);
                Console.WriteLine($"📤 Lista de salas enviada ({roomList.Count} salas)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al enviar lista: {ex.Message}");
            }
        }
    }
}