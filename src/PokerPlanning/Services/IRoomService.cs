using PokerPlanning.Models;
using System.Collections.Concurrent;

namespace PokerPlanning.Services
{
    public interface IRoomService
    {
        ConcurrentDictionary<Guid, Room> Rooms { get; set; }

        Task<int> CleanRooms();
        Guid Create(string[] cardValues);
        Room? GetRoom(Guid id);
    }
}