using WindowsInput;

namespace Consoleum
{
    public interface IConsoleDriver
    {
         void Start();
         IKeyboardSimulator Keyboard { get; }
         ICaptureOutput Output { get; }
    }
}