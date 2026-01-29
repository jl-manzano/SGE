namespace TicTacToeServer.Entities
{
    public class Room
    {
        public string RoomId { get; set; } = "";
        public string RoomName { get; set; } = "";
        public Player? PlayerX { get; set; }
        public Player? PlayerO { get; set; }
        public DateTime CreatedAt { get; set; }

        public Room()
        {
            RoomId = "";
            RoomName = "";
            PlayerX = null;
            PlayerO = null;
            CreatedAt = DateTime.UtcNow;
        }

        public Room(string roomId, string roomName)
        {
            RoomId = roomId;
            RoomName = roomName;
            PlayerX = null;
            PlayerO = null;
            CreatedAt = DateTime.UtcNow;
        }

        public int GetPlayerCount()
        {
            int count = 0;
            if (PlayerX != null) count++;
            if (PlayerO != null) count++;
            return count;
        }

        public bool IsFull()
        {
            return GetPlayerCount() >= 2;
        }

        public bool IsEmpty()
        {
            return GetPlayerCount() == 0;
        }
    }
}