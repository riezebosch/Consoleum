namespace Consoleum.PageObjects.Tests.PageObjects
{
    public class Slow : Page
    {
        public override bool IsOpen => ExistsInOutput("Finally...");

        public int Label => int.Parse(FindInOutput(@"(?<=Result=)\d+"));
    }
}