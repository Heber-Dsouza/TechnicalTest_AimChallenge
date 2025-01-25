namespace TargetMasters.Models.GameModels
{
    public class MainGame
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public bool HasStarted { get; set; }
        public List<Target> TargetsQueue { get; set; } = new List<Target>();
        public int CurrentGameDifficultLevel { get; set; } = 0;
        public int CurrentGameTurn { get; set; } = 0;
        public int CurrentGameRound { get; set; } = 0;

        // prop que vai auxiliar no registro do CurrentGameRound
        public string? LastPlayerConnectionIdTracker { get; set; }
    }
}
