using WindowsInput;

namespace Consoleum
{
    public interface IConsoleDriver
    {
         IConsoleDriver Start();
         IKeyboardSimulator Keyboard { get; }
         ICaptureOutput Output { get; }
    }
}