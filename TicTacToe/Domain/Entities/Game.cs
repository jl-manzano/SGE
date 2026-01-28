namespace Domain.Entities
{
    public class Game
    {
        public string[] Board { get; set; }
        public Player? PlayerX { get; set; }
        public Player? PlayerO { get; set; }
        public string CurrentTurn { get; set; }
        public string? Winner { get; set; }
        public bool GameOver { get; set; }

        public Game()
        {
            Board = new string[9];
            int i = 0;
            while (i < 9)
            {
                Board[i] = "";
                i = i + 1;
            }
            CurrentTurn = "X";
            Winner = null;
            GameOver = false;
        }
    }
}