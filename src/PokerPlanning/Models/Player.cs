namespace PokerPlanning.Models
{
    public record Player
    {

        public string UserName { get; set; } = string.Empty;

        public bool CardSelected
        {
            get => !string.IsNullOrEmpty(card);
        }

        private string? card;
        public string? Card
        {
            get => card;
            set
            {
                card = value;
                PlayerChanged?.Invoke(this, new());
            }
        }



        public EventHandler? PlayerChanged { get; set; }
    }
}
