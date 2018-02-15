using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public class DisplaySplash : Display
    {
        int i;

        public override Display Run()
        {
            i++;
            return null;
        }

        protected override void RenderDisplay()
        {
            DrawResource("test", 0, 0, ConsoleColor.DarkCyan);
            DrawResource("test2", 2, 0, ConsoleColor.Gray);
            Write("heya", 146, 39);
            WriteCentered("heyaa", 10);
        }
    }
}
