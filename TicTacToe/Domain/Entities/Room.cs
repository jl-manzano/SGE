namespace Domain.Entities
{
    public class Room
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public Game Game { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MaxPlayers { get; set; }

        public Room(string roomId, string roomName)
        {
            RoomId = roomId;
            RoomName = roomName;
            Game = new Game();
            CreatedAt = DateTime.UtcNow;
            MaxPlayers = 2;
        }

        public int GetPlayerCount()
        {
            int count = 0;
            if (Game.PlayerX != null) count = count + 1;
            if (Game.PlayerO != null) count = count + 1;
            return count;
        }

        public bool IsFull()
        {
            return GetPlayerCount() >= MaxPlayers;
        }

        public bool IsEmpty()
        {
            return GetPlayerCount() == 0;
        }
    }
}