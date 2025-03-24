using System.Diagnostics;


namespace MathGame.GameModes
{
    internal class AddGameMode : GameMode
    {
        private const int MediumGameTimeLimit = 601;
        private const int MediumGameBaseScoring = 2;

        private const int HardGameTimeLimit = 61;
        private const int HardGameBaseScoring = 5;

        public AddGameMode(Difficulty level = Difficulty.Easy) : base(level) { }
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

        /// <summary>
        /// Rules:
        /// - 2 numbers within 0-10 int
        /// - No time Limit per question
        /// - Normal Scoring
        /// - 5 lifes
        /// - positive numbers only
        /// </summary>
        protected override void EasyGame()
        {

            int firstNumber, secondNumber, correctAnswer;
            do
            {
                firstNumber = Rand.Next(11);
                secondNumber = Rand.Next(11);
                correctAnswer = firstNumber + secondNumber;

                if (AskQuestion(correctAnswer, $"What's {firstNumber} + {secondNumber}? "))
                {
                    Score++;
                }
                else
                {
                    PlayerHealth--;
                }

                if (CurrentStreak > BiggestStreak)
                    BiggestStreak = CurrentStreak;

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0);
        }

        /// <summary>
        /// Rules:
        /// - 2 numbers within 0-100 int
        /// - 5 minutes to answer as many questions as they can
        /// - 2 points + streak points
        /// - 3 lifes
        /// - positive numbers only
        /// </summary>
        protected override void MediumGame()
        {
            int firstNumber, secondNumber, correctAnswer;
            GameTimer.Start(MediumGameTimeLimit);

            do
            {
                firstNumber = Rand.Next(101);
                secondNumber = Rand.Next(101);
                correctAnswer = firstNumber + secondNumber;
                if (AskQuestion(correctAnswer, $"What's {firstNumber} + {secondNumber}? "))
                {
                    Score += MediumGameBaseScoring + CurrentStreak;
                }
                else
                {
                    PlayerHealth--;
                }

                if (CurrentStreak > BiggestStreak)
                    BiggestStreak = CurrentStreak;

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0 && !GameTimer.IsTimeUp());

        }

        /// <summary>
        /// Rules:
        /// - 3 numbers within 0-100
        /// - 60 sec per question
        /// - 5 points + streak as extra points per question
        /// - 1 life
        /// - positive numbers only 2 point precision
        /// </summary>
        protected override void HardGame()
        {

            double firstNumber, secondNumber, thirdNumber, correctAnswer;
            do
            {
                firstNumber = Rand.NextDouble() * 100;
                secondNumber = Rand.NextDouble() * 100;
                thirdNumber = Rand.NextDouble() * 100;
                correctAnswer = Math.Round(firstNumber + secondNumber + thirdNumber, 2);

                GameTimer.Start(HardGameTimeLimit);

                if (AskQuestion(correctAnswer, $"What's {firstNumber:F2} + {secondNumber:F2} + {thirdNumber:F2} ans {correctAnswer}?"))
                {
                    Score += HardGameBaseScoring + CurrentStreak;
                }
                else
                {
                    PlayerHealth--;
                }

                if (CurrentStreak > BiggestStreak)
                    BiggestStreak = CurrentStreak;

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0);
        }
    }

}
