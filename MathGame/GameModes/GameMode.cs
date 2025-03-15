using MathGame.Models;
using System.Security.AccessControl;
using System.Text.Json;

namespace MathGame.GameModes
{
    abstract class GameMode
    {
        public int Score { get; protected set; }
        public int TimeElapsed { get; protected set; }
        public enum Difficulty { Easy, Medium, Hard };

        public GameMode(Difficulty level = Difficulty.Easy) { }

        public virtual void Start() { }

        public virtual void OnGameOver() { }

        public void SaveScore()
        {
            PlayerData gameScore = new()
            {
                Date = DateTime.Now,
                GameMode = this,
                PlayerName = "Andrzej"
            };
            string jsonString = JsonSerializer.Serialize(gameScore);
        }

        protected virtual void EasyGame() { }

        protected virtual void MediumGame() { }

        protected virtual void HardGame() { }
    }
}

