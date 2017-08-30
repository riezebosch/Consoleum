namespace Consoleum.PageObjects
{
    public static class IConsoleDriverExtensions
    {
        public static TPage StartWith<TPage>(this IConsoleDriver driver)
            where TPage: Page, new()
        {
            return Page.StartWith<TPage>(driver);
        }
    }
}