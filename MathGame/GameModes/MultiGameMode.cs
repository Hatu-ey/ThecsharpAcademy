using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame.GameModes
{
    internal class MultiGameMode : GameMode
    {
        private const int MediumGameTimeLimit = 181;
        private const int HardGameTimeLimit = 61;

        public MultiGameMode(Difficulty level = Difficulty.Hard) : base(level) { }
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

                correctAnswer = firstNumber - secondNumber;

                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"What's {firstNumber} * {secondNumber}? ");

                if (int.TryParse(Console.ReadLine(), out int answer) && answer == correctAnswer)
                {
                    DisplayAnswer($"Correct! {firstNumber} * {secondNumber} equals {correctAnswer}", ConsoleColor.Green);
                    Score++;
                    CurrentStreak++;
                }
                else
                {
                    DisplayAnswer($"Incorrect! {firstNumber} * {secondNumber} equals {correctAnswer}", ConsoleColor.Red);
                    PlayerHealth--;
                    CurrentStreak = 0;
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
        /// - 3 minutes to answer as many questions as they can
        /// - 2x Score + streak points
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

                if (firstNumber > secondNumber)
                {
                    (firstNumber, secondNumber) = (secondNumber, firstNumber);
                }

                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"What's {firstNumber} + {secondNumber}? ");
                correctAnswer = firstNumber + secondNumber;
                if (int.TryParse(Console.ReadLine(), out int answer) && answer == correctAnswer)
                {
                    DisplayAnswer($"Correct! {firstNumber} + {secondNumber} equals {correctAnswer}", ConsoleColor.Green);
                    CurrentStreak++;
                    Score += 2 + CurrentStreak;
                }
                else
                {
                    DisplayAnswer($"Incorrect! {firstNumber} + {secondNumber} equals {correctAnswer}", ConsoleColor.Red);
                    PlayerHealth--;
                    CurrentStreak = 0;
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
        /// - 2 numbers within 0-100
        /// - 60 sec per question
        /// - 3x Score + streak as extra points
        /// - 3 lifes
        /// - numbers with 2 point precision
        /// result may be negative
        /// </summary>
        protected override void HardGame()
        {

            double firstNumber, secondNumber, correctAnswer;
            do
            {
                firstNumber = Rand.NextDouble() * 100;
                secondNumber = Rand.NextDouble() * 100;
                correctAnswer = Math.Round(firstNumber + secondNumber, 2);
                correctAnswer = 0;


                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"What's {firstNumber:F2} - {secondNumber:F2}  ? ");
                GameTimer.Start(HardGameTimeLimit);

                if (double.TryParse(Console.ReadLine(), out double answer) && answer == correctAnswer && !GameTimer.IsTimeUp())
                {
                    GameTimer.Stop();
                    DisplayAnswer($"Correct! {firstNumber:F2} + {secondNumber:F2} equals {correctAnswer:F2}", ConsoleColor.Green);
                    CurrentStreak++;
                    Score += 4 + CurrentStreak;
                }
                else
                {
                    GameTimer.Stop();
                    DisplayAnswer($"Incorrect! {firstNumber:F2} + {secondNumber:F2} equals {correctAnswer:F2}", ConsoleColor.Red);
                    PlayerHealth--;
                    CurrentStreak = 0;
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
