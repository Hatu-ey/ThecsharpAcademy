using System.Diagnostics;

namespace MathGame.GameModes
{
    internal class MultiGameMode(Difficulty level = Difficulty.Easy) : GameMode(level)
    {
        private const int MediumGameTimeLimit = 181;
        private const int MediumGameBaseScoring = 5;

        private const int HardGameTimeLimit = 61;
        private const int HardGameBaseScoring = 10;

        public override void Start()
        {
            Console.Clear();
            DisplayScore();

            Stopwatch stopwatch = Stopwatch.StartNew();

            switch (SelectedDifficulty)
            {
                case Difficulty.Easy:
                    PlayerHealth = 10;
                    EasyGame();
                    break;
                case Difficulty.Medium:
                    PlayerHealth = 5;
                    MediumGame();
                    break;
                case Difficulty.Hard:
                    PlayerHealth = 3;
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
        /// - non negative result
        /// - No time Limit per question
        /// - Normal Scoring
        /// - 10 lifes
        /// </summary>
        protected override void EasyGame()
        {

            int firstNumber, secondNumber, correctAnswer;
            do
            {
                firstNumber = Rand.Next(11);
                secondNumber = Rand.Next(11);
                correctAnswer = firstNumber * secondNumber;

                if (AskQuestion(correctAnswer, $"What's {firstNumber} * {secondNumber}? "))
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
        /// Rules:
        /// - 2 numbers within 0-100 int
        /// - 3 minutes to answer as many questions as they can
        /// - 5 points + streak points
        /// - 5 lifes
        /// - non negative result
        /// </summary>
        protected override void MediumGame()
        {
            int firstNumber, secondNumber, correctAnswer;
            GameTimer.Start(MediumGameTimeLimit);

            do
            {
                firstNumber = Rand.Next(101);
                secondNumber = Rand.Next(101);
                correctAnswer = firstNumber * secondNumber;

                if(AskQuestion(correctAnswer, $"What's {firstNumber} * {secondNumber}?"))
                {
                    Score += MediumGameBaseScoring + CurrentStreak;

                }else
                {
                    PlayerHealth--;
                }
     

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0 && !GameTimer.IsTimeUp());

        }

        /// <summary>
        /// Rules:
        /// - 2 numbers within 0-100
        /// - 60 sec per question
        /// - 10 points + streak as extra points
        /// - 3 lifes
        /// - numbers with 2 point precision
        /// result may be negative
        /// </summary>
        protected override void HardGame()
        {

            double firstNumber, secondNumber, correctAnswer;
            do
            {
                firstNumber = Math.Round(Rand.NextDouble() * 100, 2);
                secondNumber = Math.Round(Rand.NextDouble() * 100, 2);
                correctAnswer = Math.Round(firstNumber * secondNumber, 2);

                GameTimer.Start(HardGameTimeLimit);

                if(AskQuestion(correctAnswer, $"What's {firstNumber:F2} * {secondNumber:F2}  ? "))
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
