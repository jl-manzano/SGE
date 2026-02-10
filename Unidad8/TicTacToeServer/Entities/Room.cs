namespace TicTacToeServer.Entities
{
    /// <summary>
    /// Representa una sala de juego multijugador para Tic Tac Toe.
    /// Puede contener hasta 2 jugadores (X y O).
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Identificador único de la sala.
        /// </summary>
        public string RoomId { get; set; } = "";

        /// <summary>
        /// Nombre visible de la sala.
        /// </summary>
        public string RoomName { get; set; } = "";

        /// <summary>
        /// Jugador con el símbolo X (primer jugador en unirse).
        /// </summary>
        public Player? PlayerX { get; set; }

        /// <summary>
        /// Jugador con el símbolo O (segundo jugador en unirse).
        /// </summary>
        public Player? PlayerO { get; set; }

        /// <summary>
        /// Fecha y hora de creación de la sala en UTC.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Constructor por defecto. Crea una sala vacía.
        /// </summary>
        public Room()
        {
            RoomId = "";
            RoomName = "";
            PlayerX = null;
            PlayerO = null;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Constructor con parámetros para crear una sala con ID y nombre.
        /// </summary>
        /// <param name="roomId">Identificador único de la sala.</param>
        /// <param name="roomName">Nombre visible de la sala.</param>
        public Room(string roomId, string roomName)
        {
            RoomId = roomId;
            RoomName = roomName;
            PlayerX = null;
            PlayerO = null;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Obtiene el número de jugadores actualmente en la sala.
        /// </summary>
        /// <returns>Número de jugadores (0, 1 o 2).</returns>
        public int GetPlayerCount()
        {
            int count = 0;
            if (PlayerX != null) count++;
            if (PlayerO != null) count++;
            return count;
        }

        /// <summary>
        /// Verifica si la sala está llena (2 jugadores).
        /// </summary>
        /// <returns>True si la sala tiene 2 jugadores.</returns>
        public bool IsFull()
        {
            return GetPlayerCount() >= 2;
        }

        /// <summary>
        /// Verifica si la sala está vacía (sin jugadores).
        /// </summary>
        /// <returns>True si la sala no tiene jugadores.</returns>
        public bool IsEmpty()
        {
            return GetPlayerCount() == 0;
        }
    }
}