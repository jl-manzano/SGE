namespace TicTacToeServer.Entities
{
    /// <summary>
    /// Jugador conectado a una sala
    /// Solo información básica para comunicación
    /// </summary>
    public class Player
    {
        public string ConnectionId { get; set; } = "";
        public string Symbol { get; set; } = "";  // "X" o "O"
        public string Name { get; set; } = "";

        public Player()
        {
            ConnectionId = "";
            Symbol = "";
            Name = "Jugador";
        }

        public Player(string connectionId, string symbol, string name)
        {
            ConnectionId = connectionId;
            Symbol = symbol;
            Name = name;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ConnectionId) &&
                   !string.IsNullOrWhiteSpace(Symbol);
        }
    }
}