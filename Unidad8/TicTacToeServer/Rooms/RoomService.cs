using TicTacToeServer.Entities;

namespace TicTacToeServer.Rooms
{

    /// <summary>
    /// Servicio simplificado: SOLO gestiona salas
    /// NO mantiene estado del juego, NO valida movimientos
    /// </summary>
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Room CreateRoom(string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
                roomName = $"Sala {DateTime.Now:HH:mm}";

            return _roomRepository.CreateRoom(roomName);
        }

        public Room JoinRoom(string roomId, string connectionId, string playerName)
        {
            var room = _roomRepository.GetRoom(roomId);
            if (room == null)
                throw new InvalidOperationException($"La sala {roomId} no existe");

            if (room.IsFull())
                throw new InvalidOperationException("La sala está llena");

            // Solo asignar el jugador, sin lógica de juego
            if (room.Game.PlayerX == null)
            {
                room.Game.PlayerX = new Player(connectionId, "X", $"{playerName} X");
            }
            else if (room.Game.PlayerO == null)
            {
                room.Game.PlayerO = new Player(connectionId, "O", $"{playerName} O");
            }

            Console.WriteLine($"👤 Jugador {connectionId} se unió a sala {roomId}");
            return room;
        }

        public Room? LeaveRoom(string roomId, string connectionId)
        {
            var room = _roomRepository.GetRoom(roomId);
            if (room == null) return null;

            // Remover jugador
            if (room.Game.PlayerX?.ConnectionId == connectionId)
            {
                room.Game.PlayerX = null;
            }
            else if (room.Game.PlayerO?.ConnectionId == connectionId)
            {
                room.Game.PlayerO = null;
            }

            Console.WriteLine($"👋 Jugador {connectionId} salió de sala {roomId}");

            // Si la sala queda vacía, eliminarla
            if (room.IsEmpty())
            {
                _roomRepository.DeleteRoom(roomId);
                return null;
            }

            return room;
        }

        public List<object> GetRoomList()
        {
            var rooms = _roomRepository.GetAllRooms();
            return rooms.Select(room => new
            {
                roomId = room.RoomId,
                roomName = room.RoomName,
                playerCount = room.GetPlayerCount(),
                maxPlayers = room.MaxPlayers,
                isFull = room.IsFull(),
                createdAt = room.CreatedAt
            }).ToList<object>();
        }

        public void CleanEmptyRooms()
        {
            _roomRepository.CleanEmptyRooms();
        }
    }
}
