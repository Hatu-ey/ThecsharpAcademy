using MathGame.Classes;
using MathGame.Models;

namespace MathGame.GameModes
{
    public enum Difficulty { Easy, Medium, Hard };
    abstract class GameMode
    {
        public int PlayerHealth { get; protected set; }
        public int Score { get; protected set; }
        public TimeSpan TimeElapsed { get; protected set; }
        public Difficulty SelectedDifficulty { get; protected set; }
        public int Round { get; protected set; }
        public int BiggestStreak { get; protected set; }
        protected int CurrentStreak { get; set; }

        protected Random Rand = new();
        protected GameTimer GameTimer { get; private set; }

        public GameMode(Difficulty level = Difficulty.Easy)
        {
            SelectedDifficulty = level;
            GameTimer = new GameTimer();
        }
        public abstract void Start();
        protected void OnGameOver()
        {

            //TODO: Implement Saving Score
            // SaveScore();
            bool askAgain = true;
            do
            {
                Console.Clear();
                Console.WriteLine($"GameOver! Final Score: {Score} Biggest Streak: {BiggestStreak} Time: {TimeElapsed}");
                Console.WriteLine("Do you wish to restart the game? y/n?");
                string? userAnswer = Console.ReadLine();

                if (userAnswer == null) continue;

                userAnswer = userAnswer?.Trim().ToLower();

                if (userAnswer == "y")
                {
                    askAgain = false;
                    Restart();
                }
                else if (userAnswer == "n")
                {
                    askAgain = false;
                    Console.WriteLine("Thanks For playing! Press any Key to continue");
                    Console.ReadKey();
                    Menu MainMenu = new();
                    MainMenu.DisplayMenu();
                }
            }
            while (askAgain);
        }

        public virtual void Restart()
        {
            Score = 0;
            TimeElapsed = TimeSpan.Zero;
            Round = 0;
            BiggestStreak = 0;
            CurrentStreak = 0;
            Start();
        }
        protected void SaveScore() { }
        protected abstract void EasyGame();
        protected abstract void MediumGame();
        protected abstract void HardGame();

        protected void DisplayScore()
        {
            (int prevLeft, int prevTop) = Console.GetCursorPosition();
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.Write($"Current Health:{PlayerHealth} Streak:{CurrentStreak} Score:{Score} Round:{Round}");
            Console.SetCursorPosition(prevLeft, prevTop);
        }

        /// <summary>
        /// Displays message to user below their input, clears both line where user types and next one (we assume user creates nl after their input)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="consoleForegroundColor"></param>
        /// <exception cref="InvalidOperationException"></exception>
        protected void DisplayAnswer(string message, ConsoleColor consoleForegroundColor = ConsoleColor.Black)
        {
            int currentLine = Console.CursorTop;
            if (currentLine == 0)
            {
                throw new InvalidOperationException("Cannot write above the console window.");
            }

            //clear line above, where user types answer
            Console.SetCursorPosition(0, currentLine - 1);
            Console.Write(new string(' ', Console.WindowWidth));

            Console.SetCursorPosition(0, currentLine);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, currentLine);
            Console.ForegroundColor = consoleForegroundColor;
            Console.Write(message);
            Console.ResetColor();
            Console.SetCursorPosition(0, currentLine - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="correctAnswer"></param>
        /// <param name="question"></param>
        /// <returns>true if user answers correctly, false otherwise</returns>
        protected bool AskQuestion(double correctAnswer, string question)
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine(question);

            if (double.TryParse(Console.ReadLine(), out double answer) && answer == correctAnswer)
            {
                DisplayAnswer($"Correct! The answer is {correctAnswer}", ConsoleColor.Green);
                CurrentStreak++;
                return true;
            }
            else
            {
                DisplayAnswer($"Incorrect! The correct answer is {correctAnswer}", ConsoleColor.Red);
                CurrentStreak = 0;
                return false;
            }
        }
    }
}

