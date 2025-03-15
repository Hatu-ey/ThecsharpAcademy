using System.Text.Json;

namespace MathGame
{
    abstract class GameMode
    {
        int Score { get; set; } = 0;
        int timeElapsed { get; set; }
        string? playerName { get; set; }
        public enum Difficulity { Easy, Medium, Hard };

        public GameMode(Difficulity level = Difficulity.Easy) { }

        public void Start() { }

        public void OnGameOver() { }

        public void SaveScore()
        {
            string jsonString = JsonSerializer.Serialize(this);
        }

    }
}
