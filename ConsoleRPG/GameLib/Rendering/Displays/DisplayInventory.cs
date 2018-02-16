using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public class DisplayInventory : Display
    {
        Inventory inventory;
        int slot = 0;

        public DisplayInventory(Display previousDisplay, Inventory inventory) : base(previousDisplay)
        {
            this.inventory = inventory;
        }

        public override Display Run()
        {
            switch(ReadKey())
            {
                case ConsoleKey.X:
                    return previousDisplay;
                case ConsoleKey.UpArrow:
                    if(slot > 0)
                    {
                        slot--;
                    }
                    return this;
                case ConsoleKey.DownArrow:
                    slot++;
                    return this;
                default:
                    return this;
            }
        }

        protected override void RenderDisplay()
        {
            prefabs.RenderMenuBorder("Inventory");
            prefabs.RenderMenuExit();
            Write("Selected", 1, 3 + slot);
        }
    }
}
