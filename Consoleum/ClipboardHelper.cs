using System;
using System.Threading;

namespace Consoleum
{
    internal static class ClipboardHelper
    {
        public static string GetData()
        {
            string result = null;
            ExecuteOnSTAThread(() => result = System.Windows.Forms.Clipboard.GetText());

            return result;
        }

        public static void SetData(string data)
        {
            ExecuteOnSTAThread(() => System.Windows.Forms.Clipboard.SetText(data));
        }

        private static void ExecuteOnSTAThread(ThreadStart action)
        {
            var thread = new Thread(action);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}
