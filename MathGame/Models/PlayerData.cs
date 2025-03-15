using MathGame.GameModes;

namespace MathGame.Models
{
    internal class PlayerData
    {
        public required string PlayerName { get; set; }
        public required GameMode GameMode { get; set; }
        public DateTime Date { get; set; }
    }

}
