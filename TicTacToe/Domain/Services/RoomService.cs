using Domain.Entities;
using TicTacToe.Domain.Interfaces;

namespace TicTacToe.Domain.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly GameService _gameService;

        public RoomService(IRoomRepository roomRepository, GameService gameService)
        {
            _roomRepository = roomRepository;
            _gameService = gameService;
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

            _gameService.AssignPlayer(room.Game, connectionId, playerName);
            Console.WriteLine($"👤 Jugador {connectionId} se unió a sala {roomId}");
            return room;
        }

        public Room? LeaveRoom(string roomId, string connectionId)
        {
            var room = _roomRepository.GetRoom(roomId);
            if (room == null) return null;

            bool hasPlayer = _gameService.HasPlayer(room.Game, connectionId);
            if (!hasPlayer) return room;

            room.Game = new Game();
            Console.WriteLine($"👋 Jugador {connectionId} salió de sala {roomId}");

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