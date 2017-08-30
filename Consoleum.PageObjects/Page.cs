using System;
using System.Text.RegularExpressions;

namespace Consoleum.PageObjects
{
    public abstract class Page
    {
        protected IConsoleDriver Driver { get; private set; }
        public abstract bool IsOpen { get; }

        protected bool ExistsInOutput(string pattern)
        {
            return Regex.IsMatch(Driver.Output.Capture(), pattern);
        }

        protected string FindInOutput(string pattern)
        {
            string output = Driver.Output.Capture();
            var match = Regex.Match(output, pattern);
            if (!match.Success)
            {
                throw new OutputNotFoundException($@"Pattern '{pattern}' not found in output:

{output}") { Output = output, Pattern = pattern };
            }

            return match.Value;
        }

        public static TPage StartWith<TPage>(IConsoleDriver driver)
            where TPage: Page, new()
        {
            return new TPage { Driver = driver };
        }

        protected TPage NavigateTo<TPage>()
            where TPage: Page, new()
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