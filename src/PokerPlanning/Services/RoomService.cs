using PokerPlanning.Models;
using System.Collections.Concurrent;

namespace PokerPlanning.Services
{
    public class RoomService
    {
        public RoomService()
        {
            Rooms = new ConcurrentDictionary<Guid, Room>();
        }

        public ConcurrentDictionary<Guid, Room> Rooms { get; set; }

        public Guid Create()
        {
            var room = new Room();
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
}
