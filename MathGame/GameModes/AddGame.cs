using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathGame.GameModes
{
    internal class AddGame : GameMode
    {
        public AddGame(Difficulty level) : base(level) {

            switch (level)
            {
                case Difficulty.Easy:
                    break;
                case Difficulty.Medium:
                    break;
                case Difficulty.Hard: 
                    break;
            }
        }

        public override void Start()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
        }
    }
}
