namespace TicTacToeServer.Entities
{
    /// <summary>
    /// Representa un jugador en una partida de Tic Tac Toe.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Identificador único de la conexión SignalR del jugador.
        /// </summary>
        public string ConnectionId { get; set; } = "";

        /// <summary>
        /// Símbolo asignado al jugador ("X" o "O").
        /// </summary>
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Nombre visible del jugador.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Constructor por defecto. Inicializa un jugador vacío.
        /// </summary>
        public Player()
        {
            ConnectionId = "";
            Symbol = "";
            Name = "Jugador";
        }

        /// <summary>
        /// Constructor con parámetros para crear un jugador completo.
        /// </summary>
        /// <param name="connectionId">ID de conexión SignalR.</param>
        /// <param name="symbol">Símbolo del jugador ("X" o "O").</param>
        /// <param name="name">Nombre del jugador.</param>
        public Player(string connectionId, string symbol, string name)
        {
            ConnectionId = connectionId;
            Symbol = symbol;
            Name = name;
        }

        /// <summary>
        /// Valida si el jugador tiene los datos mínimos requeridos.
        /// </summary>
        /// <returns>True si tiene ConnectionId y Symbol válidos.</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ConnectionId) &&
                   !string.IsNullOrWhiteSpace(Symbol);
        }
    }
}