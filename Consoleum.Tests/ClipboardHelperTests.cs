using System.Diagnostics;
using WindowsInput;
using WindowsInput.Native;
using Xunit;
using Shouldly;
using System.Threading;
using System;

namespace Consoleum.Tests
{
    public partial class ClipboardHelperTests
    {
        [Fact]
        public void CaptureContentFromConsoleTest()
        {
            IConsoleDriver driver = new ConsoleDriver("Consoleum.Tests.ConsoleApp.exe");
            driver.Start();

            
            var result = driver.Output.Capture();

            result.ShouldBe("Hello World!\r\n");
            (driver as IDisposable)?.Dispose();
        }
    }
}
