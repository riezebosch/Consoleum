using System;
using Shouldly;
using Xunit;

namespace Consoleum.Tests
{
    public class CaptureOutputUsingCliboardTests
    {
         [Fact]
        public void CaptureContentFromConsoleTest()
        {
            var data = Guid.NewGuid().ToString();
            using (var driver = new ConsoleDriver("Consoleum.Tests.ConsoleApp.exe", data))
            {
                driver
                    .Start()
                    .Sleep(2000)
                    .Output
                    .Capture()
                    .ShouldContain(data);
            }
        }
        
        [Fact]
        public void ThrowExceptionOnCaptureFailure()
        {            
            var data = Guid.NewGuid().ToString();
            using (var driver = new ConsoleDriver("notepad"))
            {
                var ex = Should.Throw<CaptureOutputException>(() => driver.Start().Output.Capture());
                ex.Message.ShouldContain("nonce");
            }
        }
    }
}