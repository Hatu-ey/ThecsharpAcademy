using MathGame.GameModes;

namespace MathGame.Models
{
    [Serializable]
    internal class PlayerData
    {
        public string? PlayerName { get; set; }
        public required GameMode GameMode { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }

}
