using Xunit;
using Shouldly;
using System;

namespace Consoleum.Tests
{
    public partial class ClipboardHelperTests
    {
        [Fact]
        public void CopyToClipboard()
        {
            var data = Guid.NewGuid().ToString();
            
            ClipboardHelper.SetData(data);

            ClipboardHelper.GetData().ShouldBe(data);
        }
    }
}
