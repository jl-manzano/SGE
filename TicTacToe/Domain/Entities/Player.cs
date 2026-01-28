namespace Domain.Entities
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }

        public Player(string connectionId, string symbol, string name)
        {
            ConnectionId = connectionId;
            Symbol = symbol;
            Name = name;
        }

        public Player()
        {
            ConnectionId = "";
            Symbol = "";
            Name = "";
        }
    }
}