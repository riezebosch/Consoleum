using System.Diagnostics;
using WindowsInput;
using WindowsInput.Native;
using Xunit;
using Shouldly;
using System.Threading;

namespace Consoleum.Tests
{
    public partial class ConsoleTests
    {
        [Fact]
        public void Test1()
        {
            var proc = StartApplication();

            Console.CopyConsoleOutputToClipboard();
            var result = Console.ReadContentFromClipboard();

            result.ShouldBe("Hello World!\r\n");
            CloseApplication(proc);
        }

        private static void CloseApplication(Process proc)
        {
            var simulator = new InputSimulator();
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_X);
            
            proc.WaitForExit();
        }

        private static Process StartApplication()
        {
            var proc = new Process();
            proc.StartInfo.FileName = "Consoleum.Tests.ConsoleApp.exe";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();

            Thread.Sleep(100);
 
            return proc;
        }
    }
}
