using System;
using Xunit;
using Shouldly;
using NSubstitute;
using WindowsInput;
using System.Threading.Tasks;

namespace Consoleum.PageObjects.Tests
{
    public class PageTests
    {
        private ICaptureOutput capture;
        private IConsoleDriver driver;

        public PageTests()
        {
            capture = Substitute.For<ICaptureOutput>();
            driver = Substitute.For<IConsoleDriver>();
            driver
                .Output
                .Returns(capture);

            driver
                .Keyboard
                .Returns(Substitute.For<IKeyboardSimulator>());
        }

        [Fact]
        public void StartWithOpensPage()
        {
            var page = Page.StartWith<PageWrapper>(driver);
            page
                .IsOpen
                .ShouldBeTrue();
        }

        [Fact]
        public void StartWithSecondIsOpenFalse()
        {
            var page = Page.StartWith<PageNotOpen>(driver);
            page
                .IsOpen
                .ShouldBeFalse();
        }

        [Fact]
        public void NavigateFromStartPageToSecond()
        {
            Page
                .StartWith<PageWrapper>(driver)
                .NavigateToDriver<PageWrapper>()
                .IsOpen.ShouldBeTrue();
        }

        [Fact]
        public void NavigateThrowsIfToIsNotOpen()
        {
            var data = CreateUniqueData();
            capture.Capture().Returns(data);

            var ex = Should.Throw<NavigationFailedException>(() =>
                Page
                    .StartWith<PageWrapper>(driver)
                    .NavigateToDriver<PageNotOpen>());

            ex.Output.ShouldContain(data);
        }

        [Fact]
        public void FindOnPageThrowsAfter4Retries()
        {
            var data = CreateUniqueData();

            Should.Throw<OutputNotFoundException>(() => driver.StartWith<PageWrapper>().FindInOutputDriver(data));
            capture.Received(5).Capture();
        }

        [Fact]
        public void ExistsOnPageFalseAfter4Retries()
        {
            var data = CreateUniqueData();

            driver.StartWith<PageWrapper>()
                .ExistsInOutputDriver(data)
                .ShouldBeFalse();

            capture.Received(5).Capture();
        }

        [Fact]
        public void FindOnPageSucceedsAfter4thTry()
        {
            var data = CreateUniqueData();
            capture
                .Capture()
                .Returns("first", "second", "third", data);

            driver
                .StartWith<PageWrapper>()
                .FindInOutputDriver(data)
                .ShouldBe(data);

            capture.Received(4).Capture();
        }

        [Fact]
        public void ExistsOnPageSucceedsAfter4thTry()
        {
            var data = CreateUniqueData();
            capture
                .Capture()
                .Returns("first", "second", "third", data);

            driver
                .StartWith<PageWrapper>()
                .ExistsInOutputDriver(data)
                .ShouldBeTrue();

            capture.Received(4).Capture();
        }

        [Fact]
        public void FindOnPage()
        {
            var data = CreateUniqueData();
            capture.Capture().Returns(data);

            driver
                .StartWith<PageWrapper>()
                .FindInOutputDriver(data)
                .ShouldBe(data);
        }

        [Fact]
        public void FindOnPageWaitsAtLeast4Seconds()
        {
            var delayed = string.Empty;
            capture.Capture().Returns(info => delayed);

            var data = CreateUniqueData();
            Task
                .Delay(TimeSpan.FromSeconds(4))
                .ContinueWith(t => delayed = data);

            driver
                .StartWith<PageWrapper>()
                .FindInOutputDriver(data);
        }

        private static string CreateUniqueData()
        {
            return Guid.NewGuid().ToString();
        }


        class PageWrapper : Page
        {
            public override bool IsOpen => true;

            public string FindInOutputDriver(string pattern)
            {
                return FindInOutput(pattern);
            }

            public T NavigateToDriver<T>()
                where T: Page, new()
            {
                return NavigateTo<T>();
            }

            public bool ExistsInOutputDriver(string pattern)
            {
                return base.ExistsInOutput(pattern);
            }
        }

        class PageNotOpen : Page
        {
            public override bool IsOpen => false;
        }
    }
}
