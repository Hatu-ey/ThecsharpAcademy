using System.Diagnostics;


namespace MathGame.GameModes
{
    internal class AddGameMode : GameMode
    {
        private Random Rand = new();
        private readonly int MediumGameTimeLimit = 601;
        private readonly int HardGameTimeLimit = 60;

        public AddGameMode(Difficulty level = Difficulty.Hard) : base(level) { }
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
                    MediumGame();
                    break;
                case Difficulty.Hard:
                    PlayerHealth = 1;
                    HardGame();
                    break;
            }

            stopwatch.Stop();
            TimeElapsed = stopwatch.Elapsed;
            OnGameOver();
            Console.ReadKey();
        }
        public override void Restart()
        {
            Console.Write("Add Game Begins");
            base.Restart();
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

            int firstNumber, secondNumber, correctAnswer;
            do
            {
                firstNumber = Rand.Next(11);
                secondNumber = Rand.Next(11);
                correctAnswer = firstNumber + secondNumber;
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"What's {firstNumber} + {secondNumber}? ");

                if (int.TryParse(Console.ReadLine(), out int answer) && answer == correctAnswer)
                {
                    DisplayAnswer($"Correct! {firstNumber} + {secondNumber} equals {correctAnswer}", ConsoleColor.Green);
                    Score++;
                    CurrentStreak++;
                }
                else
                {
                    DisplayAnswer($"Incorrect! {firstNumber} + {secondNumber} equals {correctAnswer}", ConsoleColor.Red);
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
        /// - 2 numbers within 0-100
        /// - 5 minutes to answer as many questions as they can
        /// - 2x Score + streak points
        /// - 3 lifes
        /// - positive numbers only
        /// </summary>
        protected override void MediumGame()
        {
            int firstNumber, secondNumber, correctAnswer;
            StartTimer(MediumGameTimeLimit);

            do
            {
                firstNumber = Rand.Next(101);
                secondNumber = Rand.Next(101);
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
                }

                if (CurrentStreak > BiggestStreak)
                    BiggestStreak = CurrentStreak;

                Round++;
                DisplayScore();
            }
            while (PlayerHealth > 0 && !IsTimeUp());

        }

        /// <summary>
        /// Rules:
        /// - 3 numbers within 0-100
        /// - 60 sec per question
        /// - 3x Score + streak as extra points
        /// - 1 life
        /// - positive numbers only
        /// </summary>
        protected override void HardGame()
        {

            int firstNumber, secondNumber, thirdNumber, correctAnswer;
            do
            {
                firstNumber = Rand.Next(101);
                secondNumber = Rand.Next(101);
                thirdNumber = Rand.Next(101);
                Console.SetCursorPosition(0, 1);
                Console.WriteLine($"What's {firstNumber} + {secondNumber} + {thirdNumber}? ");
                correctAnswer = firstNumber + secondNumber + thirdNumber;

                StartTimer(HardGameTimeLimit);

                if (int.TryParse(Console.ReadLine(), out int answer) && answer == correctAnswer && !IsTimeUp())
                {
                    StopTimer();
                    DisplayAnswer($"Correct! {firstNumber} + {secondNumber} + {thirdNumber} equals {correctAnswer}", ConsoleColor.Green);
                    CurrentStreak++;
                    Score += 4 + CurrentStreak;
                }
                else
                {
                    StopTimer();
                    DisplayAnswer($"Incorrect! {firstNumber} + {secondNumber} + {thirdNumber} equals {correctAnswer}", ConsoleColor.Red);
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
