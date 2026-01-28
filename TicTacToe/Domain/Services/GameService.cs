using Domain.Entities;

namespace TicTacToe.Domain.Services
{
    /// <summary>
    /// Servicio de dominio que contiene toda la lógica del juego
    /// Sin break, sin múltiples return, solo while
    /// </summary>
    public class GameService
    {
        public bool IsWaitingForPlayer(Game game)
        {
            bool playerXNull = game.PlayerX == null;
            bool playerONull = game.PlayerO == null;
            bool result = playerXNull || playerONull;
            return result;
        }

        public void AssignPlayer(Game game, string connectionId, string playerName)
        {
            string finalName = playerName;
            if (string.IsNullOrWhiteSpace(finalName))
            {
                finalName = "Jugador";
            }

            bool playerXIsNull = game.PlayerX == null;
            if (playerXIsNull)
            {
                game.PlayerX = new Player(connectionId, "X", $"{finalName} X");
                return;
            }

            bool playerOIsNull = game.PlayerO == null;
            if (playerOIsNull)
            {
                game.PlayerO = new Player(connectionId, "O", $"{finalName} O");
                return;
            }

            throw new InvalidOperationException("El juego ya tiene dos jugadores");
        }

        public bool HasPlayer(Game game, string connectionId)
        {
            bool isPlayerX = game.PlayerX?.ConnectionId == connectionId;
            bool isPlayerO = game.PlayerO?.ConnectionId == connectionId;
            bool result = isPlayerX || isPlayerO;
            return result;
        }

        public string GetPlayerSymbol(Game game, string connectionId)
        {
            bool isPlayerX = game.PlayerX?.ConnectionId == connectionId;
            if (isPlayerX)
            {
                return "X";
            }

            bool isPlayerO = game.PlayerO?.ConnectionId == connectionId;
            if (isPlayerO)
            {
                return "O";
            }

            throw new InvalidOperationException("El jugador no pertenece a este juego");
        }

        public void ValidateMove(Game game, string connectionId, int position)
        {
            if (game.GameOver)
            {
                throw new InvalidOperationException("El juego ya ha terminado");
            }

            bool waitingForPlayer = IsWaitingForPlayer(game);
            if (waitingForPlayer)
            {
                throw new InvalidOperationException("Esperando al segundo jugador");
            }

            string symbol = GetPlayerSymbol(game, connectionId);

            bool notPlayerTurn = symbol != game.CurrentTurn;
            if (notPlayerTurn)
            {
                throw new InvalidOperationException($"No es el turno del jugador {symbol}");
            }

            bool invalidPosition = position < 0 || position > 8;
            if (invalidPosition)
            {
                throw new ArgumentException($"Posición {position} inválida");
            }

            bool positionOccupied = !string.IsNullOrEmpty(game.Board[position]);
            if (positionOccupied)
            {
                throw new InvalidOperationException($"La posición {position} ya está ocupada");
            }
        }

        public void MakeMove(Game game, string connectionId, int position)
        {
            ValidateMove(game, connectionId, position);

            string symbol = GetPlayerSymbol(game, connectionId);
            game.Board[position] = symbol;

            CheckGameResult(game);

            bool gameNotOver = !game.GameOver;
            if (gameNotOver)
            {
                SwitchTurn(game);
            }
        }

        public void SwitchTurn(Game game)
        {
            bool isXTurn = game.CurrentTurn == "X";
            game.CurrentTurn = isXTurn ? "O" : "X";
        }

        public void CheckGameResult(Game game)
        {
            string? winner = CheckWinner(game);
            bool hasWinner = winner != null;
            if (hasWinner)
            {
                game.Winner = winner;
                game.GameOver = true;
                return;
            }

            bool boardFull = IsBoardFull(game);
            if (boardFull)
            {
                game.Winner = "draw";
                game.GameOver = true;
                return;
            }
        }

        public string? CheckWinner(Game game)
        {
            int[][] winningCombinations = new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            int i = 0;
            while (i < winningCombinations.Length)
            {
                int[] combo = winningCombinations[i];
                string first = game.Board[combo[0]];

                bool firstNotEmpty = !string.IsNullOrEmpty(first);
                bool secondMatches = first == game.Board[combo[1]];
                bool thirdMatches = first == game.Board[combo[2]];

                bool isWinningCombo = firstNotEmpty && secondMatches && thirdMatches;
                if (isWinningCombo)
                {
                    return first;
                }

                i = i + 1;
            }

            return null;
        }

        public bool IsBoardFull(Game game)
        {
            int i = 0;
            while (i < game.Board.Length)
            {
                bool isEmpty = string.IsNullOrEmpty(game.Board[i]);
                if (isEmpty)
                {
                    return false;
                }
                i = i + 1;
            }
            return true;
        }

        public void ResetGame(Game game)
        {
            int i = 0;
            while (i < 9)
            {
                game.Board[i] = "";
                i = i + 1;
            }
            game.CurrentTurn = "X";
            game.Winner = null;
            game.GameOver = false;
        }
    }
}