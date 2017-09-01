using System;
using Shouldly;
using WindowsInput;
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
                driver.Start();
                driver
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
                driver.Start();
                
                var ex = Should.Throw<CaptureOutputException>(() => driver.Output.Capture());
                ex.Message.ShouldContain("nonce");
            }
        }
    }
}