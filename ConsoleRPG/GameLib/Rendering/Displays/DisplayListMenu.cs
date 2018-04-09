using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public abstract class DisplayListMenu : Display
    {
        protected List<ListOption> options = new List<ListOption>();
        protected int selectedOptionId = 0;

        public DisplayListMenu(Display previousDisplay) : base(previousDisplay)
        {

        }

        public override Display Run()
        {
            ConsoleKey read = ReadKey();

            if (read == ConsoleKey.UpArrow)
            {
                if (selectedOptionId == 0)
                {
                    selectedOptionId = options.Count - 1;
                }
                else
                {
                    selectedOptionId--;
                }
                return this;
            }
            else if (read == ConsoleKey.DownArrow)
            {
                if (selectedOptionId == options.Count - 1)
                {
                    selectedOptionId = 0;
                }
                else
                {
                    selectedOptionId++;
                }
                return this;
            }
            else if (read == ConsoleKey.Enter)
            {
                return OnItemSelect(options[selectedOptionId]);
            }
            else
            {
                return HandleAlternativeInput(read);
            }
        }

        public abstract Display HandleAlternativeInput(ConsoleKey read);

        protected abstract Display OnItemSelect(ListOption id);

        protected abstract override void RenderDisplay();

        [Serializable]
        protected class ListOption
        {
            public string Text { get; set; }

            public ListOption(string text)
            {
                Text = text;
            }
        }
    }
}
