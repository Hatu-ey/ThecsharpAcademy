using MathGame.GameModes;

namespace MathGame.Models
{
    internal class PlayerData
    {
        public static string? PlayerName { get; set; }
        public required GameMode GameMode { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"Name: {PlayerName} Stats: {GameMode.ToString()} Date: {Date}";
        }
    }
}
