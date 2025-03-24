using MathGame.GameModes;

namespace MathGame.Classes
{
    internal class Menu
    {
        private bool displayMenu = true;
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Hello! Do you want to play a game? Select one below!");
            Console.WriteLine("1 - Addition Game\n2 - Substraction game\n3 - Multiplication game\n4 - Division game\nQ - Quit Game");
            ConsoleKey selectedOption = Console.ReadKey().Key;

            switch (selectedOption)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Add Game");
                    AddGameMode addGame = new();
                    addGame.Start();
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Substraction Game");
                    SubGameMode subGame = new();
                    subGame.Start();
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Multiplication Game");
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Division Game");
                    break;
                case ConsoleKey.D5:
                    Console.WriteLine("Leader Board");
                    break;
                case ConsoleKey.D6:
                    Console.WriteLine("Quit");
                    displayMenu = false;
                    break;
                default:
                    Console.WriteLine("No menu Option Selected");
                    break;
            }

        }
    }
}
