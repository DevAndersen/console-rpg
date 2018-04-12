using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplayTextInput : Display
    {
        public delegate void InputHandler(string input);

        private Display nextDisplay;
        private InputHandler inputHandler;
        private string description;
        private int maxLength;

        private string input;

        public DisplayTextInput(Display previousDisplay, Display nextDisplay, InputHandler inputHandler, string description, int maxLength = 64) : base(previousDisplay)
        {
            this.nextDisplay = nextDisplay;
            this.inputHandler = inputHandler;
            this.description = description;
            this.maxLength = maxLength;

            input = string.Empty;
        }

        public override Display Run()
        {
            ConsoleKeyInfo readInfo = ReadKeyInfo();
            if (readInfo.Key == ConsoleKey.Escape)
            {
                return previousDisplay;
            }
            else if (readInfo.Key == ConsoleKey.Enter && input.Length > 0)
            {
                inputHandler(input);
                return nextDisplay;
            }
            else if (readInfo.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);
            }
            else if ((char.IsLetter(readInfo.KeyChar) || char.IsDigit(readInfo.KeyChar) || char.IsSeparator(readInfo.KeyChar) || char.IsSymbol(readInfo.KeyChar) || char.IsPunctuation(readInfo.KeyChar)) && input.Length < maxLength)
            {
                input += readInfo.KeyChar;
            }
            return this;
        }

        protected override void RenderDisplay()
        {
            prefabs.RenderMenuBorder(description);
            prefabs.RenderMenuExit();
            prefabs.RenderInputField(maxLength, input);
        }
    }
}
