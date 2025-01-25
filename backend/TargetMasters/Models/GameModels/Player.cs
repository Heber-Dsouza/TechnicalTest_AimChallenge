namespace TargetMasters.Models.GameModels
{
    public class Player
    {
        public Player(string playerName, string connectionId)
        {
            this.PlayerName = playerName;
            this.ConnectionId = connectionId;
        }

        public string PlayerName { get; set; } = "";
        public string? ConnectionId { get; set; }
        public PlayerStats PlayerStats { get; set; } = new PlayerStats();
        public bool IsReady { get; set; } = false;
        public bool IsWatching { get; set; } = false;
        public int? Place { get; set; }
        public int? StackPosition { get; set; }

    }

    public class PlayerStats
    {
        public int SecondsMs { get; set; } = 30000;
        public bool IsPlayerTurn { get; set; } = false;
        public RandomColors RandomColors { get; set; } = new RandomColors();

    }

    public class RandomColors
    {
        public int R { get; set; } = Random255();
        public int G { get; set; } = Random255();
        public int B { get; set; } = Random255();

        public static int Random255()
        {
            Random random = new Random();
            return random.Next(0, 256);
        }
    }
}
