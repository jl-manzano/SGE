namespace TicTacToeServer.Entities
{
    /// <summary>
    /// Sala de juego
    /// Solo mantiene información de los jugadores conectados
    /// NO mantiene estado del juego (tablero, ganador, etc.)
    /// </summary>
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

        /// <summary>
        /// Cuenta jugadores en la sala
        /// </summary>
        public int GetPlayerCount()
        {
            int count = 0;
            if (PlayerX != null) count++;
            if (PlayerO != null) count++;
            return count;
        }

        /// <summary>
        /// Verifica si la sala está llena (2 jugadores)
        /// </summary>
        public bool IsFull()
        {
            return GetPlayerCount() >= 2;
        }

        /// <summary>
        /// Verifica si la sala está vacía
        /// </summary>
        public bool IsEmpty()
        {
            return GetPlayerCount() == 0;
        }
    }
}