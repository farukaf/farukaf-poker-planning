using System.Collections.Concurrent;

namespace PokerPlanning.Models
{
    public class Room
    {
        public Room()
        {
            Players = new ConcurrentQueue<Player>();
            Id = Guid.NewGuid();
            CreateAt = DateTime.UtcNow;
        }
        public Guid Id { get; }
        public DateTime CreateAt { get; }
        public ConcurrentQueue<Player> Players { get; private set; }


        public EventHandler? RoomChanged { get; set; }

        public void EnterRoom(Player player)
        {
            if (Players.Any(x => x == player))
                return;
            Players.Enqueue(player);

            RoomChanged?.Invoke(this, new());
        }
    }
}
