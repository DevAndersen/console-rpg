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

        public override Display Run()
        {
            if(initial)
            {
                initial = false;
                return this;
            }
            return null;
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
