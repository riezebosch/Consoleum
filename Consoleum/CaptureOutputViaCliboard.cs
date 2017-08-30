using WindowsInput;

namespace Consoleum
{
    internal class CaptureOutputUsingCliboard : ICaptureOutput
    {
        private IKeyboardSimulator keyboard;

        public CaptureOutputUsingCliboard(IKeyboardSimulator keyboard)
        {
            this.keyboard = keyboard;
        }

        public string Capture()
        {
            ClipboardHelper.CopyConsoleOutputToClipboard(keyboard);
            return ClipboardHelper.ReadContentFromClipboard();
        }
    }
}