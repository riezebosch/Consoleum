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
            using (var driver = new ConsoleDriver("Consoleum.Tests.ConsoleApp.exe"))
            {
                driver.Start();
                
                ClipboardHelper.CopyConsoleOutputToClipboard(driver.Keyboard);
                ClipboardHelper.ReadContentFromClipboard().ShouldBe("Hello World!\r\n");
            }
        }
    }
}
