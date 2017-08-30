using Xunit;
using Shouldly;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Consoleum.Tests
{
    public class ConsoleDriverTests
    {
        [Fact]
        public void Test1()
        {
            var before = Process.GetProcessesByName("Consoleum.Tests.ConsoleApp");

            var driver = new ConsoleDriver("Consoleum.Tests.ConsoleApp.exe");
            driver.Start();

            var after = Process.GetProcessesByName("Consoleum.Tests.ConsoleApp");
            after.Length.ShouldBeGreaterThan(before.Length);

            driver.Dispose();
        }

        [Fact]
        public void DisposeKillsStartedProcess()
        {
            var driver = new ConsoleDriver("Consoleum.Tests.ConsoleApp.exe");
            driver.Start();

            var before = Process.GetProcessesByName("Consoleum.Tests.ConsoleApp");
            driver.Dispose();

            var after = Process.GetProcessesByName("Consoleum.Tests.ConsoleApp");
            after.Length.ShouldBeLessThan(before.Length);
        }

        [Fact]
        public void DisposeDoesNotKillsNotStartedProcess()
        {
            var driver = new ConsoleDriver("Consoleum.Tests.ConsoleApp.exe");
            driver.Dispose();
        }

        [Fact]
        public void DisposeDoesNotThrowWhenKillingClosedProcess()
        {
            var driver = new ConsoleDriver("cmd", "/C");
            driver.Start();
            driver.Dispose();
        }
    }
}