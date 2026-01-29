using TicTacToeServer.Entities;

namespace TicTacToeServer.Rooms
{
    public interface IRoomRepository
    {
        Room CreateRoom(string roomName);
        Room? GetRoom(string roomId);
        List<Room> GetAllRooms();
        void DeleteRoom(string roomId);
        bool RoomExists(string roomId);
        void CleanEmptyRooms();
    }
}