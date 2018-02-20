using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public abstract partial class Display
    {
        protected Prefabs prefabs;

        [Serializable]
        protected class Prefabs
        {
            private Display display;

            public Prefabs(Display display)
            {
                this.display = display;
            }

            public void RenderMenuBorder(string text)
            {
                display.DrawResource("menuBorder", 0, 0, ConsoleColor.Gray);
                display.WriteCentered(text, 1);
            }

            public void RenderMenuExit()
            {
                display.Write("X", Width - 3, 1, ConsoleColor.Red);
                display.DrawResource("menuBorderVerticalLine", Width - 5, 0);
            }

            public void ClearDisplay()
            {
                StringBuilder sbClearline = new StringBuilder().Append(' ', Width);
                for (int i = 0; i < Height; i++)
                {
                    display.Write(sbClearline.ToString(), 0, i, ConsoleColor.Gray, ConsoleColor.Black);
                }
            }
        }
    }
}
