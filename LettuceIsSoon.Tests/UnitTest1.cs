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
            var proc = StartApplication();

            CopyConsoleOutputToClipboard();
            var result = ReadContentFromClipboard();

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
            proc.StartInfo.FileName = "LettuceIsSoon.Tests.ConsoleApp.exe";
            proc.StartInfo.UseShellExecute = true; // must always be false with .NET Core
            proc.Start();
            Thread.Sleep(1000);
            return proc;
        }

        private static string ReadContentFromClipboard()
        {
            string result;
            if (!User32.OpenClipboard(IntPtr.Zero))
            {
                throw new Win32Exception();
            }

            unsafe
            {
                var data = User32.GetClipboardData(13); // CF_UNICODETEXT: https://msdn.microsoft.com/nl-nl/library/windows/desktop/ff729168(v=vs.85).aspx
                if (data == null)
                {
                   throw new Win32Exception();
                }

                var text = Kernel32.GlobalLock(data);
                if (text == null)
                {
                    throw new Win32Exception();
                }

                result = new string((char*)text);
                if (!Kernel32.GlobalUnlock(data))
                {
                    throw new Win32Exception();
                }
            }

            User32.CloseClipboard().ShouldBeTrue();
            return result;
        }

        private static void CopyConsoleOutputToClipboard()
        {
            var simulator = new InputSimulator();
            simulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_A);
            simulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(200);
        }
    }
}
