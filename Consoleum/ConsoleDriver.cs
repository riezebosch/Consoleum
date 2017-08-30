using System;
using System.Diagnostics;
using System.Threading;

namespace Consoleum
{
    public sealed class ConsoleDriver : IConsoleDriver, IDisposable
    {
        private readonly Process proc;
        private bool started;

        public ConsoleDriver(string file, string arguments = null)
        {
            proc = new Process();
            proc.StartInfo.FileName = file;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = true;
        }
        
        public void Start()
        {
            started = proc.Start();
            Thread.Sleep(200);
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