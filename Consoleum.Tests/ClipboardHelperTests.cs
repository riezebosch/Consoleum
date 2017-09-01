using System.Diagnostics;
using WindowsInput;
using WindowsInput.Native;
using Xunit;
using Shouldly;
using System.Threading;
using System;
using PInvoke;

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
