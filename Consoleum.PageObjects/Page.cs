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
            return RetryMultipleTimes(() => Regex.IsMatch(Driver.Output.Capture(), pattern), r => r);
        }

        protected string FindInOutput(string pattern)
        {
            var result = RetryMultipleTimes(() =>
            {
                var output = Driver.Output.Capture();
                var match = Regex.Match(output, pattern);
                return new { match, output };

            }, r => r.match.Success);

            if (result.match.Success)
            {
                return result.match.Value;
            }

            throw new OutputNotFoundException($@"Pattern '{pattern}' not found in output:

{result.output}") { Output = result.output, Pattern = pattern };
        }


        /// <summary>
        /// Retry <paramref name="tryMe"/> multiple times
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tryMe">try this</param>
        /// <param name="isSuccess">is the result a success and stop?</param>
        /// <returns>the last result of <paramref name="tryMe"/></returns>
        private static T RetryMultipleTimes<T>(Func<T> tryMe, Func<T, bool> isSuccess)
        {
            T result = default(T);
            for (int i = 0; i < 4; i++)
            {
                result = tryMe();
                if (isSuccess(result))
                {
                    return result;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            return result;
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