using System.Numerics;
using System.Text.Json.Serialization;

namespace TicTacToe.Models
{
    public class GameState
    {
        [JsonPropertyName("board")]
        public string[] Board { get; set; } // Array de 9 posiciones

        [JsonPropertyName("currentTurn")]
        public string CurrentTurn { get; set; } // "X" o "O"

        [JsonPropertyName("winner")]
        public string? Winner { get; set; } // null, "X", "O", o "draw"

        [JsonPropertyName("gameOver")]
        public bool GameOver { get; set; }

        [JsonPropertyName("playerX")]
        public Player? PlayerX { get; set; }

        [JsonPropertyName("playerO")]
        public Player? PlayerO { get; set; }

        [JsonPropertyName("waitingForPlayer")]
        public bool WaitingForPlayer { get; set; }

        public GameState()
        {
            Board = new string[9];
            for (int i = 0; i < 9; i++)
            {
                Board[i] = "";
            }
            CurrentTurn = "X";
            Winner = null;
            GameOver = false;
            PlayerX = null;
            PlayerO = null;
            WaitingForPlayer = true;
        }

        public bool IsBoardFull()
        {
            return Array.TrueForAll(Board, cell => !string.IsNullOrEmpty(cell));
        }

        public string? CheckWinner()
        {
            // Combinaciones ganadoras
            int[][] winningCombinations = new int[][]
            {
                new int[] { 0, 1, 2 }, // Fila 1
                new int[] { 3, 4, 5 }, // Fila 2
                new int[] { 6, 7, 8 }, // Fila 3
                new int[] { 0, 3, 6 }, // Columna 1
                new int[] { 1, 4, 7 }, // Columna 2
                new int[] { 2, 5, 8 }, // Columna 3
                new int[] { 0, 4, 8 }, // Diagonal 1
                new int[] { 2, 4, 6 }  // Diagonal 2
            };

            foreach (var combo in winningCombinations)
            {
                string first = Board[combo[0]];
                if (!string.IsNullOrEmpty(first) &&
                    first == Board[combo[1]] &&
                    first == Board[combo[2]])
                {
                    return first;
                }
            }

            // Verificar empate
            if (IsBoardFull())
            {
                return "draw";
            }

            return null;
        }

        public void SwitchTurn()
        {
            CurrentTurn = CurrentTurn == "X" ? "O" : "X";
        }
    }
}