using MathGame.GameModes;
using MathGame.Models;

namespace MathGame.Classes
{

    internal class Menu
    {
        private readonly Dictionary<ConsoleKey, Func<Difficulty, GameMode>> gameModes = new()
        {
            { ConsoleKey.D1, difficulty => new AddGameMode(difficulty) },
            { ConsoleKey.D2, difficulty => new SubGameMode(difficulty) },
            { ConsoleKey.D3, difficulty => new MultiGameMode(difficulty) },
            { ConsoleKey.D4, difficulty => new DivisionGameMode(difficulty) },
            { ConsoleKey.D5, difficulty => new RandomGameMode(difficulty) }
        };
        public void DisplayMenu()
        {
            Console.Clear();

            if(PlayerData.PlayerName == null)
            {
                Console.WriteLine("Hello! What's your name?");
                string? name = Console.ReadLine();
                bool invalidName = string.IsNullOrEmpty(name);
                PlayerData.PlayerName = invalidName ?  "Guest" : name?.Trim();

            } else
            {
                Console.WriteLine($"Hello! {PlayerData.PlayerName}\nPress Any Key to Continue...");
                Console.ReadKey();
            }
           
            Console.Clear();
            Console.WriteLine("Do you want to play a game? Select one below!");
            Console.WriteLine("1 - Addition Game\n2 - Substraction game\n3 - Multiplication game\n4 - Division game\n5 - Random game\nL -leaderboards\nQ - Quit Game");
            ConsoleKey selectedOption = Console.ReadKey().Key;

            if (gameModes.TryGetValue(selectedOption, out Func<Difficulty, GameMode>? gameModeFactory))
            {
                Console.Clear();
                Console.WriteLine("Would you like to play \n1 - Easy\n2 - Medium\n3 - Hard Game?");
                gameModeFactory(GetDifficulty()).Start();
            }
            else
            {
                switch (selectedOption)
                {
                    case ConsoleKey.L:
                        Console.WriteLine("Leaderboard");
                        DisplayLeaderBoard();
                        break;
                    case ConsoleKey.Q:
                        Console.WriteLine("Bye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Returning to menu...");
                        DisplayMenu();
                        break;
                }
            }

        }

        private Difficulty GetDifficulty()
        {
            ConsoleKey selectedDifficulty = Console.ReadKey().Key;
            return selectedDifficulty switch
            {
                ConsoleKey.D1 => Difficulty.Easy,
                ConsoleKey.D2 => Difficulty.Medium,
                ConsoleKey.D3 => Difficulty.Hard,
                _ => Difficulty.Easy
            };
        }

        private void DisplayLeaderBoard()
        {
            Console.Clear();
            Console.WriteLine("Current LeaderBoard! ");
            List<PlayerData> GameList = SaveManager.GetGameHistory();
            foreach (PlayerData playerData in GameList) {
                Console.WriteLine(playerData);
            }
            Console.WriteLine("Press Any Key to continue...");
            Console.ReadKey();
            DisplayMenu();
        }
    }

}
