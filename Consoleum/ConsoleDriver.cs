using System;
using System.Diagnostics;
using System.Threading;
using WindowsInput;

namespace Consoleum
{
    public sealed class ConsoleDriver : IConsoleDriver, IDisposable
    {
        private readonly Process proc;
        private bool started;
        
        public IKeyboardSimulator Keyboard { get; }

        public ICaptureOutput Output { get; } 

        public ConsoleDriver(string file, string arguments = null)
        {
            proc = new Process();
            proc.StartInfo.FileName = file;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = true;

            Keyboard  = new InputSimulator().Keyboard;
            Output = new CaptureOutputUsingCliboard(Keyboard);
        }
        
        public IConsoleDriver Start()
        {
            started = proc.Start();
            Thread.Sleep(200);

            return this;
        }

        public void Dispose()
        {
            if (started && !proc.HasExited)
            {
                proc.Kill();
                Thread.Sleep(200);
            }

            proc.Dispose();
        }
    }
}