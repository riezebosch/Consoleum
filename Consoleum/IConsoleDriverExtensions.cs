using System;
using System.Threading;

namespace Consoleum
{
    public static class IConsoleDriverExtensions
    {
        public static IConsoleDriver Sleep(this IConsoleDriver driver, int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return driver;
        }
    }
}