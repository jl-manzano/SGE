using System.Text.Json.Serialization;

namespace TicTacToe.Models
{
    public class Player
    {
        [JsonPropertyName("connectionId")]
        public string ConnectionId { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } // "X" o "O"

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public Player()
        {
            ConnectionId = string.Empty;
            Symbol = string.Empty;
            Name = "Jugador";
        }

        public Player(string connectionId, string symbol, string name)
        {
            ConnectionId = connectionId;
            Symbol = symbol;
            Name = name;
        }
    }
}