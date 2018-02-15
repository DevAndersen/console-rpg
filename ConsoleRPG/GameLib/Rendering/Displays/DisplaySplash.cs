using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public class DisplaySplash : Display
    {
        static bool b = false;
        static int i;

        private char c = '#';

        public override Display Run()
        {
            b = !b;
            c = b ? 'X' : ' ';
            i++;
            ReadLine();
            return i < 5 ? new DisplaySplash() : null;
        }

        protected override void RenderDisplay()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    //grid[x, y] = new Pxl(c, (ConsoleColor)((x + y * Width) % 16), (ConsoleColor)((8 + x + y * Width) % 16));
                    //grid[x, y] = new Pxl(c);
                }
            }

            DrawResource("test", 0, 0, ConsoleColor.DarkCyan);
            DrawResource("test2", 2, 0, ConsoleColor.Gray);
        }
    }
}
