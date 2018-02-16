using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public class DisplaySplash : Display
    {
        bool initial = true;

        public DisplaySplash(Display previousDisplay) : base(previousDisplay)
        {

        }

        public override Display Run()
        {
            if(initial)
            {
                initial = false;
                return this;
            }
            switch(ReadKey())
            {
                case ConsoleKey.Enter:
                    return new DisplayInventory(this, null);
                case ConsoleKey.X:
                    return null;
                default:
                    return this;
            }
        }

        protected override void RenderDisplay()
        {
            if(initial)
            {
                prefabs.ClearDisplay();
            }
            else
            {
                prefabs.RenderMenuBorder("This is my text");
                Write("This is a demo string", 10, 10, ConsoleColor.Red, ConsoleColor.Green);
            }
        }
    }
}
