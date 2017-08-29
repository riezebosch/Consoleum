using System;
using System.Diagnostics;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;
using Xunit;
using static PInvoke.Kernel32;

namespace LettuceIsSoon.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var proc = new Process();
            proc.StartInfo.FileName = "LettuceIsSoon.Tests.ConsoleApp.exe";
            proc.StartInfo.UseShellExecute = true; // must always be false with .NET Core
            proc.Start();
            Thread.Sleep(1000);

            var simulator = new InputSimulator();
            simulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_A);
            simulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_X);
            
            proc.WaitForExit();
        }
    }
}
