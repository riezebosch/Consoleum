using System;
using WindowsInput.Native;

namespace Consoleum.PageObjects.Tests.PageObjects
{
    public class Main : Page
    {
        public override bool IsOpen => ExistsInOutput(".*Hello World!.*");

        public string WrongCommand => FindInOutput("(?<=Unknown command ').");

        internal Second Next()
        {
            Driver
                .Keyboard
                .KeyPress(VirtualKeyCode.VK_2)
                .Sleep(200);

            return NavigateTo<Second>();
        }

        internal Main EnterUnknownCommand()
        {
            Driver
                .Keyboard
                .KeyPress(VirtualKeyCode.VK_X)
                .Sleep(200);

            return this;
        }

        internal Second NotNext()
        {
            return NavigateTo<Second>();
        }

        internal Slow TrySlow()
        {
            Driver
                .Keyboard
                .KeyPress(VirtualKeyCode.VK_3)
                .Sleep(200);

            return NavigateTo<Slow>();
        }
    }
}