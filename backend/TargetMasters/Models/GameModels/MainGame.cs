﻿namespace TargetMasters.Models.GameModels
{
    public class MainGame
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public bool HasStarted { get; set; }
        public List<Target> TargetsQueue { get; set; } = new List<Target>();
        public int CurrentGameDifficultLevel { get; set; } = 0;
        public int CurrentGameTurn { get; set; } = 0;
        public int CurrentGameRound { get; set; } = 0;
        public int NumberOfPlayers { get; set; } = 0;
        public bool RunWatch { get; set; } = false;

        public string? LastPlayerConnectionIdTracker { get; set; } // prop que vai auxiliar no registro do CurrentGameRound

        public void SetNextPlayerTurn()
        {
            var players = this.Players;
            if (players == null || players.Count == 0) return;

            var currentPlayerIndex = players.Where(x => x.IsReady && !x.IsWatching).ToList().FindIndex(p => p.PlayerStats.IsPlayerTurn);

            if (currentPlayerIndex == -1)
            {
                players.Where(x => x.IsReady && !x.IsWatching).ToList()[0].PlayerStats.IsPlayerTurn = true;
            }
            else
            {
                players[currentPlayerIndex].PlayerStats.IsPlayerTurn = false;

                var nextPlayerIndex = (currentPlayerIndex + 1) % players.Where(x => x.IsReady && !x.IsWatching).Count();
                players[nextPlayerIndex].PlayerStats.IsPlayerTurn = true;
            }
        }
    }
}
