using PokerPlanning.Models;
using System.Collections.Concurrent;

namespace PokerPlanning.Services;

public sealed class RoomService : IRoomService
{
    private readonly ILogger<RoomService> _logger;
    public RoomService(ILogger<RoomService> logger)
    {
        Rooms = new ConcurrentDictionary<Guid, Room>();
        _logger = logger;
    }

    public ConcurrentDictionary<Guid, Room> Rooms { get; set; }

    /// <summary>
    /// Clean the rooms
    /// </summary>
    /// <param name="olderThan"></param>
    /// <returns>Number of rooms was closed</returns>
    public Task<int> CleanRooms(DateTime olderThan)
    {
        var closedRooms = 0;
        var dirtyRooms = Rooms.Where(x => x.Value.UpdateAt < olderThan);
        foreach (var dirtyRoom in dirtyRooms)
            if (Rooms.TryRemove(dirtyRoom.Key, out _))
                closedRooms++;

        return Task.FromResult(closedRooms);
    }

    public Guid Create(string[] cardValues)
    {
        var room = new Room()
        {
            CardValues = cardValues
        };
        Rooms.TryAdd(room.Id, room);

        _logger.LogInformation($"Room {room.Id} created.");

        return room.Id;
    }

    public Room? GetRoom(Guid id)
    {
        if (!Rooms.Any(x => x.Key == id))
            return null;

        return Rooms[id];
    }
}
