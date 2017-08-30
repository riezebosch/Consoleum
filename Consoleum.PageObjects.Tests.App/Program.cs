using System;

namespace Consoleum.PageObjects.Tests.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            ConsoleKey key;
            while ((key = Console.ReadKey().Key) != ConsoleKey.Q)
            {
                switch (key)
                {
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("You're on the second page.");
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine($"Unknown command '{key}'.");
                        break;
                }
            }
        }
    }
}
