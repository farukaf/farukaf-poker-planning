using PokerPlanning.Extensions;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace PokerPlanning.Models
{
    public class Room
    {
        public Room()
        {
            Players = new ConcurrentQueue<Player>();
            Id = Guid.NewGuid();
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
            CardValues = new string[] { "0", "1", "2", "3", "5", "8", "13", "20", "40", "100", "?" };
        }

        public string[] CardValues { get; set; }

        public Guid Id { get; }
        public DateTime CreateAt { get; }
        //Disclaimer the set of this prop is public when should be internal
        //TODO: Create a lib layer so only the tests can access this internal set
        public DateTime UpdateAt { get; set; }
        public ConcurrentQueue<Player> Players { get; private set; }


        public EventHandler? RoomChanged { get; set; }

        public bool CardsAreRevealed { get; set; } = false;

        public Player EnterRoom(Player player)
        {
            UpdateAt = DateTime.UtcNow;
            var _player = Players.FirstOrDefault(x => x.UserName == player.UserName);
            if (_player is not null)
                return _player;

            Players.Enqueue(player);

            player.PlayerChanged += (sender, args) => RoomChanged?.Invoke(sender, new());

            RoomChanged?.Invoke(this, new());
            return player;
        }

        public bool CanShowCards => Players.Any(x => x.CardSelected);

        public void ShowCards()
        {
            CardsAreRevealed = true;
            RoomChanged?.Invoke(this, new());
            UpdateAt = DateTime.UtcNow;
        }

        public void NewGame()
        {
            CardsAreRevealed = false;
            foreach (var player in Players)
            {
                player.Card = string.Empty;
            }
            RoomChanged?.Invoke(this, new());
            UpdateAt = DateTime.UtcNow;
        }

        public string GetAverage()
        {
            var values = Players.Select(x => x.Card?.ToDigitsOnly())?
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => Convert.ToInt32(x));

            if (!(values?.Any() ?? false))
                return string.Empty;

            return values.Average(Convert.ToInt32).ToString("#0.##");
        }
    }
}
