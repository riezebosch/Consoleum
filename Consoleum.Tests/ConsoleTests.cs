using System.Diagnostics;
using WindowsInput;
using WindowsInput.Native;
using Xunit;
using Shouldly;
using System.Threading;
using System;

namespace Consoleum.Tests
{
    public partial class ConsoleTests
    {
        [Fact]
        public void Test1()
        {
            IConsoleDriver driver = new ConsoleDriver("Consoleum.Tests.ConsoleApp.exe");
            driver.Start();

            Console.CopyConsoleOutputToClipboard();
            var result = Console.ReadContentFromClipboard();

            result.ShouldBe("Hello World!\r\n");
            (driver as IDisposable)?.Dispose();
        }
    }
}
