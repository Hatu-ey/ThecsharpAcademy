using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MathGame.Classes
{
    internal class GameTimer
    {
        private readonly Timer _timer;
        public int TimeLeft { get; private set; }

        public GameTimer()
        {
            _timer = new Timer();
            _timer.Elapsed += DisplayTimer;
            _timer.Interval = 1000;
        }
        
        //Display Timer on Top-Right side of the console
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
                Stop();
                Console.Write("Time's up! Press Any Key to continue");
            }

        }

        /// <summary>
        /// Set seconds that player has to answer, add 1 sec before loading the question
        /// </summary>
        /// <param name="timeLeft"></param>
        public void Start(int timeLeft)
        {
            TimeLeft = timeLeft;
            DisplayTimer(this, new ElapsedEventArgs(DateTime.Now));
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public bool IsTimeUp()
        {
            return TimeLeft <= 0;
        }
    }
}
