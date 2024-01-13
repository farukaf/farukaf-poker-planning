using PokerPlanning.Models;
using System.Collections.Concurrent;

namespace PokerPlanning.Services;

public sealed class RoomService : IRoomService
{
    public RoomService()
    {
        Rooms = new ConcurrentDictionary<Guid, Room>();
    }

    public ConcurrentDictionary<Guid, Room> Rooms { get; set; }

    public Task<int> CleanRooms(DateTime olderThan)
    {
        var count = 0;
        var dirtyRooms = Rooms.Where(x => x.Value.UpdateAt < olderThan);
        foreach (var dirtyRoom in dirtyRooms)
            if (Rooms.TryRemove(dirtyRoom.Key, out _))
                count++;

        return Task.FromResult(count);
    }

    public Guid Create(string[] cardValues)
    {
        var room = new Room()
        {
            CardValues = cardValues
        };
        Rooms.TryAdd(room.Id, room);

        return room.Id;
    }

    public Room? GetRoom(Guid id)
    {
        if (!Rooms.Any(x => x.Key == id))
            return null;

        return Rooms[id];
    }
}
