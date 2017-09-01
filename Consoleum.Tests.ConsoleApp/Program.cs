using System;

namespace Consoleum.Tests.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
            
            Console.ReadKey();
        }
    }
}
