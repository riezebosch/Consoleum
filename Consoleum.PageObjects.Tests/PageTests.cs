using System;
using Consoleum.PageObjects.Tests.PageObjects;
using Xunit;
using Shouldly;

namespace Consoleum.PageObjects.Tests
{
    public class PageTests : IDisposable
    {
        private ConsoleDriver driver;

        public PageTests()
        {
            driver = new ConsoleDriver("Consoleum.PageObjects.Tests.App.exe");
            driver.Start();
        }

        public void Dispose()
        {
            driver.Dispose();
        }

        [Fact]
        public void StartWithOpensPage()
        {
            var page = Page.StartWith<Main>(driver);
            page.IsOpen.ShouldBeTrue();
        }

        [Fact]
        public void StartWithSecondIsOpenFalse()
        {
            var page = Page.StartWith<Second>(driver);
            page.IsOpen.ShouldBeFalse();
        }

        [Fact]
        public void NavigateFromStartPageToSecond()
        {
            Page
                .StartWith<Main>(driver)
                .Next()
                .IsOpen.ShouldBeTrue();
        }

        [Fact]
        public void NavigateValidatesNewPage()
        {
            var ex = Should.Throw<NavigationFailedException>(() => Page.StartWith<Main>(driver)
                .NotNext());

            ex.Message.ShouldContain("Main");
            ex.Message.ShouldContain("Second");
            ex.Message.ShouldContain("Hello World!");
            ex.Output.ShouldContain("Hello World!");
        }

        [Fact]
        public void FindValueInOutput()
        {
            Page
                .StartWith<Main>(driver)
                .EnterUnknownCommand()
                .WrongCommand.ShouldBe("X");
        }

        [Fact]
        public void FindValueInOutputFailsDetails()
        {
            var ex = Should.Throw<OutputNotFoundException>(() => Page
                .StartWith<Main>(driver)
                .WrongCommand.ToString());

            ex.Message.ShouldContain("Hello World!");
        }

        [Fact]
        public void ExistsInOutputDoesSomeRetriesBeforeFailure()
        {
            Page
                .StartWith<Main>(driver)
                .TrySlow()
                .IsOpen.ShouldBeTrue();  
        }

                [Fact]
        public void FindInOutputDoesSomeRetriesBeforeFailure()
        {
            Page
                .StartWith<Main>(driver)
                .TrySlow()
                .Label.ShouldBe(8);
        }

        [Fact]
        public void PageCanStartOnDriverObject()
        {
            driver.StartWith<Main>();
        }
    }
}
