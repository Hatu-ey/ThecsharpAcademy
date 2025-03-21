using MathGame.Models;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Text.Json;
using System.Timers;
using Timer = System.Timers.Timer;

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

        private readonly Timer QuestionTimer;
        private int TimeLeft;
        private bool HasAnswered;

        public GameMode(Difficulty level = Difficulty.Easy)
        {
            SelectedDifficulty = level;
            QuestionTimer = new Timer();
            QuestionTimer.Interval = 1000;
            QuestionTimer.Elapsed += DisplayTimer;
        }
        public abstract void Start();
        protected void OnGameOver() {

            //TODO: Implement Saving Score
            // SaveScore();
            bool askAgain = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Do you wish to restart the game? y/n?");
                string? userAnswer = Console.ReadLine();
                
                if (userAnswer == null) continue;
                
                userAnswer = userAnswer.Trim().ToLower();

                if (userAnswer == "y")
                {
                    askAgain = false;
                    Restart();
                } else if(userAnswer == "n")
                {
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

        //Display Timer on Right side of the console
        protected void DisplayTimer(object? sender, ElapsedEventArgs e)
        {
            Console.CursorVisible = false;
            (int prevLeft, int prevTop) = Console.GetCursorPosition();
            Console.SetCursorPosition(Console.BufferWidth - 15, 0);
            Console.Write(new string(' ', 15));
            Console.SetCursorPosition(Console.BufferWidth - 15, 0);
            TimeLeft--;
            Console.Write(!IsTimeUp() ? $"Time Left:{TimeLeft}" : "Time's Up!");
            Console.SetCursorPosition(prevLeft, prevTop);
            Console.CursorVisible = true;
            
            if (IsTimeUp())
            {
                StopTimer();
                Console.Write("Time's up! Press Any Key to continue");
            }

        }

        protected void StartTimer(int timeLeft)
        {
            TimeLeft = timeLeft;
            QuestionTimer.Start();
        }

        protected void StopTimer() { 
            QuestionTimer.Stop();
        }

        protected bool IsTimeUp()
        {
            return TimeLeft < 0;
        }


    }


}

