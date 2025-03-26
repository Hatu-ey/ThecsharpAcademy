using MathGame.Classes;
using System;
using System.Diagnostics;

namespace MathGame.GameModes
{
    internal class RandomGameMode(Difficulty level = Difficulty.Easy) : GameMode(level)
    {
        private enum GameType { Add, Subtract, Multiply, Divide }

        private readonly Dictionary<GameType, Func<int, int, int>> GameTypeOperations = new()
        {
            {GameType.Add, (int a, int b) => a + b},
            {GameType.Subtract, (int a, int b) => a - b},
            {GameType.Multiply, (int a, int b) => a * b},
            {GameType.Divide, (int a, int b) => b != 0 ? a / b : throw new DivideByZeroException("You can't Divide by 0!")}
        };

        private const int MediumGameTimeLimit = 300;
        private const int MediumGameBaseScoring = 2;

        private const int HardGameTimeLimit = 120;
        private const int HardGameBaseScoring = 2;

        public override void Start()
        {
            Console.Clear();
            DisplayScore();
            Stopwatch stopwatch = Stopwatch.StartNew();

            switch (SelectedDifficulty)
            {
                case Difficulty.Easy:
                    PlayerHealth = 5;
                    EasyGame();
                    break;
                case Difficulty.Medium:
                    PlayerHealth = 3;
                    MediumGame();
                    break;
                case Difficulty.Hard:
                    PlayerHealth = 1;
                    HardGame();
                    break;
            }

            GameTimer.Stop();
            stopwatch.Stop();
            TimeElapsed = stopwatch.Elapsed;
            OnGameOver();
        }

        private GameType GetRandomGameType() => Enum.GetValues<GameType>()[Rand.Next(4)];

        private string GetOperationSymbol(GameType gameType)
        {
            return gameType switch
            {
                GameType.Add => "+",
                GameType.Subtract => "-",
                GameType.Multiply => "*",
                GameType.Divide => "/",
                _ => throw new ArgumentException("Invalid game type")
            };
        }

        /// <summary>
        /// rules:
        /// - 2 numbers within 0-10
        /// - All operations allowed
        /// - No time limit
        /// - 5 lives
        /// </summary>
        protected override void EasyGame()
        {
            do
            {
                GameType currentGameType = GetRandomGameType();

                int firstNumber = Rand.Next(11);
                int secondNumber = currentGameType == GameType.Divide? GameHelper.GetDivisor(firstNumber) : Rand.Next(11);

                var operation = GameTypeOperations[currentGameType];
                int correctAnswer = operation(firstNumber, secondNumber);

                string operationSymbol = GetOperationSymbol(currentGameType);
                string question = $"What's {firstNumber} {operationSymbol} {secondNumber}? ";

                if (AskQuestion(correctAnswer, question))
                {
                    Score++;
                }
                else
                {
                    PlayerHealth--;
                }

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0);
        }

        /// <summary>
        /// Medium Game Mode:
        /// - Numbers up to 50
        /// - Time limit of 5 minutes
        /// - More complex scoring
        /// - 3 lives
        /// </summary>
        protected override void MediumGame()
        {
            GameTimer.Start(MediumGameTimeLimit);

            do
            {
                GameType currentGameType = GetRandomGameType();

                int firstNumber = Rand.Next(101);
                int secondNumber = currentGameType == GameType.Divide ? GameHelper.GetDivisor(firstNumber) : Rand.Next(101);

                var operation = GameTypeOperations[currentGameType];
                int correctAnswer = operation(firstNumber, secondNumber);

                string operationSymbol = GetOperationSymbol(currentGameType);
                string question = $"What's {firstNumber} {operationSymbol} {secondNumber}? ";

                if (AskQuestion(correctAnswer, question) && !GameTimer.IsTimeUp())
                {
                    Score += MediumGameBaseScoring + CurrentStreak;
                }
                else
                {
                    PlayerHealth--;
                }

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0 && !GameTimer.IsTimeUp());
        }

        /// <summary>
        /// Hard Game Mode:
        /// - Numbers up to 100
        /// - Very short time limit
        /// - Highest scoring potential
        /// - Only 1 life
        /// </summary>
        protected override void HardGame()
        {
            do
            {
                GameTimer.Start(HardGameTimeLimit);

                GameType currentGameType = GetRandomGameType();

                int firstNumber = Rand.Next(101);
                int secondNumber = currentGameType == GameType.Divide ? GameHelper.GetDivisor(firstNumber) : Rand.Next(101);

                var operation = GameTypeOperations[currentGameType];
                int correctAnswer = operation(firstNumber, secondNumber);

                string operationSymbol = GetOperationSymbol(currentGameType);
                string question = $"What's {firstNumber} {operationSymbol} {secondNumber}? ";

                if (AskQuestion(correctAnswer, question))
                {
                    Score += HardGameBaseScoring + CurrentStreak;
                }
                else
                {
                    PlayerHealth--;
                }

                GameTimer.Stop();
                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0);
        }


    }
}