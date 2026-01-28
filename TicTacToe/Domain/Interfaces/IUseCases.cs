using Domain.Entities;

namespace TicTacToe.Domain.Interfaces
{
    public interface IUseCases
    {
        Game ConnectPlayer(string connectionId, string playerName);
        Game DisconnectPlayer(string connectionId);
        Game MakeMove(string connectionId, int position);
        Game ResetGame();
        (bool IsAllowed, string? Reason) ValidateGameTime();
    }
}