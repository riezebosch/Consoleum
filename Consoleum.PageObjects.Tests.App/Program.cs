using System;
using System.Threading;

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

                    case ConsoleKey.D3:
                        Console.Clear();
                        System.Console.WriteLine("Waiting...");
                        Thread.Sleep(2000);
                        System.Console.WriteLine("Finally...");
                        Thread.Sleep(2000);
                        System.Console.WriteLine("Result=8");
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
