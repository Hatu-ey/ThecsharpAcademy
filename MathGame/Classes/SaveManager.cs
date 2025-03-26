using MathGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MathGame.Classes
{
    internal class SaveManager
    {
        private static List<PlayerData> GameHistory = [];

        public static void AddGameHistory(PlayerData data)
        {
            GameHistory.Add(data);
        }

        public static List<PlayerData> GetGameHistory()
        {
            return GameHistory;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (PlayerData data in GameHistory)
            {
                sb.Append(data.ToString());
            }
            return sb.ToString();
        }
    }
}
