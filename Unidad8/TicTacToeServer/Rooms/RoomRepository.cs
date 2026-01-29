using TicTacToeServer.Entities;

namespace TicTacToeServer.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        private readonly Dictionary<string, Room> _rooms;
        private int _roomCounter;

        public RoomRepository()
        {
            _rooms = new Dictionary<string, Room>();
            _roomCounter = 0;
        }

        public Room CreateRoom(string roomName)
        {
            _roomCounter = _roomCounter + 1;
            string roomId = $"room_{_roomCounter}";
            Room room = new Room(roomId, roomName);
            _rooms[roomId] = room;
            Console.WriteLine($"✅ Sala creada: {roomId} - {roomName}");
            return room;
        }

        public Room? GetRoom(string roomId)
        {
            return _rooms.ContainsKey(roomId) ? _rooms[roomId] : null;
        }

        public List<Room> GetAllRooms()
        {
            return new List<Room>(_rooms.Values);
        }

        public void DeleteRoom(string roomId)
        {
            if (_rooms.ContainsKey(roomId))
            {
                _rooms.Remove(roomId);
                Console.WriteLine($"🗑️ Sala eliminada: {roomId}");
            }
        }

        public bool RoomExists(string roomId)
        {
            return _rooms.ContainsKey(roomId);
        }

        public void CleanEmptyRooms()
        {
            var emptyRooms = _rooms.Where(kvp => kvp.Value.IsEmpty())
                                   .Select(kvp => kvp.Key)
                                   .ToList();
            foreach (var roomId in emptyRooms)
            {
                DeleteRoom(roomId);
            }
            if (emptyRooms.Count > 0)
                Console.WriteLine($"🧹 Limpiadas {emptyRooms.Count} salas vacías");
        }
    }
}