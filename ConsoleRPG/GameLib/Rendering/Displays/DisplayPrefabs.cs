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

            public void RenderBorderBottomBar()
            {
                display.DrawResource("simpleBorderBottomBar", 0, 0, ConsoleColor.Gray);
            }

            public void RenderMenuBorder(string text)
            {
                display.DrawResource("menuBorder", 0, 0, ConsoleColor.Gray);
                display.Write(text, null, 1);
            }

            public void RenderMenuExit()
            {
                display.Write("X", Width - 3, 1, ConsoleColor.Red);
                display.DrawResource("menuBorderVerticalLine", Width - 5, 0);
            }

            public void RenderMenuBar(MenuBarItem[] menuBarItems, int x = 1, int y = -1)
            {
                int offset = x;

                if (y == -1)
                {
                    y = Height - 2;
                }

                for (int i = 0; i < menuBarItems.Length; i++)
                {
                    MenuBarItem item = menuBarItems[i];
                    bool hasKey = item.Key.HasValue;

                    if (hasKey)
                    {
                        display.Write("[", offset + 1, y, ConsoleColor.DarkGray);
                        display.Write("]", offset + 2 + item.Key.ToString().Length, y, ConsoleColor.DarkGray);
                        display.Write(item.Key.ToString(), offset + 2, y, item.Color);
                        offset += 3 + item.Key.ToString().Length;
                    }

                    display.Write(item.Text, offset + 1, y, item.Color);
                    display.DrawResource("menuBorderVerticalLine", offset + 2 + item.Text.Length, y - 1);
                    offset += 3 + item.Text.Length;
                }
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
