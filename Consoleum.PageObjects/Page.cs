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
            var match = RepetitiveTryToGetMatch(pattern, out _);

            return match?.Success ?? false;
        }

        protected string FindInOutput(string pattern)
        {
            var match = RepetitiveTryToGetMatch(pattern, out var output);

            if (match != null && match.Success)
            {
                return match.Value;
            }


            throw new OutputNotFoundException($@"Pattern '{pattern}' not found in output:

{output}") { Output = output, Pattern = pattern };
        }

        private Match RepetitiveTryToGetMatch(string pattern, out string output)
        {
            output = null;
            for (int i = 0; i < 4; i++)
            {
                output = Driver.Output.Capture();
                var match = Regex.Match(output, pattern);

                if (match.Success)
                {
                    return match;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            return null;
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