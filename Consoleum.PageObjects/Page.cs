using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace Consoleum.PageObjects
{
    public abstract class Page
    {
        protected IConsoleDriver Driver { get; private set; }
        public abstract bool IsOpen { get; }

        protected bool ExistsInOutput(string pattern)
        {
            return RetryCaptureMatch(pattern, out _).Success;
        }

        protected string FindInOutput(string pattern)
        {
            var match = RetryCaptureMatch(pattern, out var output);
            if (!match.Success)
            {
                throw new OutputNotFoundException($@"Pattern '{pattern}' not found in output:

{output}") { Output = output, Pattern = pattern };
            }

            return match.Value;
        }

        private Match RetryCaptureMatch(string pattern, out string output)
        {
            var match = CaptureMatch(pattern, out output);
            for (int i = 0; !match.Success && i < 4; i++)
            {
                Thread.Sleep(1000);
                match = CaptureMatch(pattern, out output);
            }

            return match;
        }

        private Match CaptureMatch(string pattern, out string output)
        {
            output = Driver.Output.Capture();
            return Regex.Match(output, pattern);
        }

        public static TPage StartWith<TPage>(IConsoleDriver driver)
            where TPage : Page, new()
        {
            return new TPage { Driver = driver };
        }

        protected TPage NavigateTo<TPage>()
            where TPage : Page, new()
        {
            var next = new TPage { Driver = Driver };
            if (!next.IsOpen)
            {
                var output = Driver.Output.Capture();
                throw new NavigationFailedException($@"Navigation from '{GetType().Name}' to '{typeof(TPage).Name}' failed.

Capture of current screen:
                
{output}") { Output = output };
            }

            return next;
        }
    }
}