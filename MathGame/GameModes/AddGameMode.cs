using System.Diagnostics;


namespace MathGame.GameModes
{
    internal class AddGameMode : GameMode
    {
        private Random Rand = new();
        private readonly float HardGameTimeLimit = 60.0f;

        public AddGameMode(Difficulty level = Difficulty.Easy) : base(level) { }
        public override void Start()
        {
            Console.Clear();
            Console.WriteLine("Add Game Begins!");

            Stopwatch stopwatch = Stopwatch.StartNew();
            DisplayScore();
            stopwatch.Start();
            switch (SelectedDifficulty)
            {
                case Difficulty.Easy:
                    PlayerHealth = 5;
                    EasyGame();
                    break;
                case Difficulty.Medium:
                    PlayerHealth = 3;
                    break;
                case Difficulty.Hard:
                    PlayerHealth = 1;
                    break;
            }

            stopwatch.Stop();
            TimeElapsed = stopwatch.Elapsed;
            OnGameOver();
        }
        public override void Restart()
        {
            Score = 0;

        }


        /// <summary>
        /// Rules:
        /// - 2 numbers within 0-10
        /// - No time Limit per question
        /// - Normal Scoring
        /// - 5 lifes
        /// - positive numbers only
        /// </summary>
        protected override void EasyGame()
        {
            
            int firstNumber = 0;
            int secondNumber = 0;
            do
            {
                firstNumber = Rand.Next(11);
                secondNumber = Rand.Next(11);
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"What's {firstNumber} + {secondNumber}? ");

                if (int.TryParse(Console.ReadLine(), out int answer) && answer == firstNumber + secondNumber)
                {
                    DisplayAnswer($"Correct! {firstNumber} + {secondNumber} equals {firstNumber + secondNumber}", ConsoleColor.Green);
                    Score++;
                    CurrentStreak++;
                }
                else
                {
                    DisplayAnswer($"Incorrect! {firstNumber} + {secondNumber} equals {firstNumber + secondNumber}", ConsoleColor.Red);
                    PlayerHealth--;
                }

                if(CurrentStreak > BiggestStreak)
                    BiggestStreak = CurrentStreak;

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0);
        }

        /// <summary>
        /// Rules:
        /// - 3 numbers within 0-100
        /// - No time Limit per question
        /// - 2x Score
        /// - 3 lifes
        /// - positive numbers only
        /// </summary>
        protected override void MediumGame()
        {
            int firstNumber = 0;
            int secondNumber = 0;
            do
            {
                firstNumber = Rand.Next(101);
                secondNumber = Rand.Next(101);
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"What's {firstNumber} + {secondNumber}? ");

                if (int.TryParse(Console.ReadLine(), out int answer) && answer == firstNumber + secondNumber)
                {
                    DisplayAnswer($"Correct! {firstNumber} + {secondNumber} equals {firstNumber + secondNumber}", ConsoleColor.Green);
                    Score += 2;
                    CurrentStreak++;
                }
                else
                {
                    DisplayAnswer($"Incorrect! {firstNumber} + {secondNumber} equals {firstNumber + secondNumber}", ConsoleColor.Red);
                    PlayerHealth--;
                }

                if (CurrentStreak > BiggestStreak)
                    BiggestStreak = CurrentStreak;

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0);
        }
        protected override void HardGame()
        {
            throw new NotImplementedException();
        }

       
    }
}
