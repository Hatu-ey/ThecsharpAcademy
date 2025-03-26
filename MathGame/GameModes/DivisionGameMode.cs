using MathGame.Classes;
using System.Diagnostics;


namespace MathGame.GameModes
{
    internal class DivisionGameMode(Difficulty level = Difficulty.Easy) : GameMode(level)
    {
        private const int MediumGameTimeLimit = 601;
        private const int MediumGameBaseScoring = 10;

        private const int HardGameTimeLimit = 301;
        private const int HardGameBaseScoring = 20;

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
        /// - positive and integers numbers only
        /// </summary>
        protected override void EasyGame()
        {

            (int dividend, int divisor) numbers;
            int correctAnswer;
            do
            {
                numbers = GameHelper.GenerateDivisionNumbers(10);
                correctAnswer = numbers.dividend / numbers.divisor;

                if (AskQuestion(correctAnswer, $"What's {numbers.dividend} / {numbers.divisor}? "))
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
        /// - 10 minutes to answer as many questions as they can
        /// - 10 points + streak points
        /// - 3 lifes
        /// - positive numbers only
        /// </summary>
        protected override void MediumGame()
        {
            (int dividend, int divisor) numbers;
            int correctAnswer;

            GameTimer.Start(MediumGameTimeLimit);

            do
            {
                numbers = GameHelper.GenerateDivisionNumbers(100);
                correctAnswer = numbers.dividend / numbers.divisor;
                if (AskQuestion(correctAnswer, $"What's {numbers.dividend} / {numbers.divisor}? ") && !GameTimer.IsTimeUp())
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
        /// Rules:
        /// - 3 numbers within 0-100
        /// - 5 min per question
        /// - 20 points + streak as extra points per question
        /// - 1 life
        /// - gain extra life every 10 streak
        /// </summary>
        protected override void HardGame()
        {

            (int dividend, int divisor) numbers;
            int correctAnswer;

            do
            {
                numbers = GameHelper.GenerateDivisionNumbers(100);
                correctAnswer = numbers.dividend / numbers.divisor;

                GameTimer.Start(HardGameTimeLimit);

                if (AskQuestion(correctAnswer, $"What's {numbers.dividend} / {numbers.divisor}?"))
                {
                    Score += HardGameBaseScoring + CurrentStreak;
                    if(CurrentStreak % 10 == 0)
                    {
                        PlayerHealth++;
                    }
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
