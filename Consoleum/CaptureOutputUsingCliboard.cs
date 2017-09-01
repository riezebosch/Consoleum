using System;
using System.Threading;
using PInvoke;
using WindowsInput;
using WindowsInput.Native;

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
            var nonce = Guid.NewGuid().ToString();
            ClipboardHelper.SetData(nonce);

            SimulateKeyStrokesToCaptureConsoleContent();
            var data = ClipboardHelper.GetData();

            if (data == nonce)
            {
                throw new CaptureOutputException(@"Capture output from console window failed.
The nonce was still present on the clipboard after simulating the select all and copy keystrokes.");
            }
            
            return data;
        }

        private void SimulateKeyStrokesToCaptureConsoleContent()
        {
            // InputSimulator did not work here simulating ALT+SPACE
            // The solution was -----v
            User32.keybd_event(0x12, 1, 0, IntPtr.Zero);
            Thread.Sleep(10);
            User32.keybd_event(0x20, 1, 0, IntPtr.Zero);
            Thread.Sleep(10);
            User32.keybd_event(0x20, 1, User32.KEYEVENTF.KEYEVENTF_KEYUP, IntPtr.Zero);
            Thread.Sleep(10);
            User32.keybd_event(0x12, 1, User32.KEYEVENTF.KEYEVENTF_KEYUP, IntPtr.Zero);
            Thread.Sleep(10);

            keyboard
                .KeyPress(VirtualKeyCode.VK_E, VirtualKeyCode.VK_S, VirtualKeyCode.RETURN)
                .Sleep(200);
        }
    }
}