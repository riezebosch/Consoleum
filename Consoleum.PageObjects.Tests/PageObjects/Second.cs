namespace Consoleum.PageObjects.Tests.PageObjects
{
    internal class Second : Page
    {
        public override bool IsOpen => ExistsInOutput(".*second page.*");
    }
}