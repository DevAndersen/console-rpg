using GameLib.GameCore;
using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public class DisplayMainMenu : Display
    {
        List<(string text, Display display)> options = new List<(string text, Display)>();
        int selectedId = 0;

        public DisplayMainMenu(Display previousDisplay) : base(previousDisplay)
        {
            options.Add(("New game", new DisplayInventory(this, new Inventory("Inventory", 30))));
            options.Add(("Load game", this));
            options.Add(("Exit", null));
        }

        public override Display Run()
        {
            ConsoleKey read = ReadKey();
            
            if(read == ConsoleKey.UpArrow)
            {
                if(selectedId == 0)
                {
                    selectedId = options.Count - 1;
                }
                else
                {
                    selectedId--;
                }
                return this;
            }
            else if(read == ConsoleKey.DownArrow)
            {
                if (selectedId == options.Count - 1)
                {
                    selectedId = 0;
                }
                else
                {
                    selectedId++;
                }
                return this;
            }
            else if(read == ConsoleKey.Enter)
            {
                if (options[selectedId].display == this)
                {
                    Core.instance.LoadGame();
                }
                return options[selectedId].display;
            }
            else
            {
                return this;
            }
        }

        protected override void RenderDisplay()
        {
            DrawResource("simpleBorder", 0, 0);
            (int resourceX, int resourceY, int resourceWidth, int resourceHeight) logo = DrawResource("logo", null, 2);

            var textOffset = logo.resourceY + logo.resourceHeight + 5;

            for (int i = 0; i < options.Count; i++)
            {
                (string text, Display display) = options[i];

                if (selectedId == i)
                {
                    text = $">>> {text} <<<";
                }

                WriteCentered(text, textOffset + i * 2);
            }
        }
    }
}
