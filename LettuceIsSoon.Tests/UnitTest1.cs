using System;
using System.Diagnostics;
using System.Threading;
using PInvoke;
using WindowsInput;
using WindowsInput.Native;
using Xunit;
using Shouldly;
using System.Text;

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
            Thread.Sleep(200);

            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_X);
            
            User32.OpenClipboard(IntPtr.Zero).ShouldBeTrue();
            unsafe 
            {
                var data = User32.GetClipboardData(13); // CF_UNICODETEXT: https://msdn.microsoft.com/nl-nl/library/windows/desktop/ff729168(v=vs.85).aspx
                var text = Kernel32.GlobalLock(data);

                var result = new string((char*)text);
                Kernel32.GlobalUnlock(data);
                User32.CloseClipboard().ShouldBeTrue();

                result.ShouldBe("Hello World!\r\n");
            }

            proc.WaitForExit();
        }
    }
}
