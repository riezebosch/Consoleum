using System;
using System.Diagnostics;
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
            proc.WaitForExit();
        }
    }
}
