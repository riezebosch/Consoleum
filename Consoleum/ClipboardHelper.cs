using System;
using System.Threading;
using PInvoke;
using WindowsInput;
using WindowsInput.Native;

namespace Consoleum
{
    internal static class ClipboardHelper
    {
        public static string ReadContentFromClipboard()
        {
            string result;
            if (!User32.OpenClipboard(IntPtr.Zero))
            {
                throw new Win32Exception();
            }

            unsafe
            {
                var data = User32.GetClipboardData(13); // CF_UNICODETEXT: https://msdn.microsoft.com/nl-nl/library/windows/desktop/ff729168(v=vs.85).aspx
                if (data == null)
                {
                    throw new Win32Exception();
                }

                var text = Kernel32.GlobalLock(data);
                if (text == null)
                {
                    throw new Win32Exception();
                }

                result = new string((char*)text);
                if (!Kernel32.GlobalUnlock(data))
                {
                    throw new Win32Exception();
                }
            }

            if (!User32.CloseClipboard())
            {
                throw new Win32Exception();
            }

            return result;
        }

        public static void CopyConsoleOutputToClipboard(IKeyboardSimulator keyboard)
        {
            keyboard
                .KeyDown(VirtualKeyCode.CONTROL)
                .KeyPress(VirtualKeyCode.VK_A)
                .KeyUp(VirtualKeyCode.CONTROL)
                .KeyPress(VirtualKeyCode.RETURN)
                .Sleep(200);
        }
    }
}
