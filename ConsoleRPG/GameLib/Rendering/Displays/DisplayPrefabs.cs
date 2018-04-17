using GameLib.Mobs;
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
                string text = ConsoleKey.Escape.ToString();
                display.Write(text, Width - text.Length - 2, 1, ConsoleColor.Yellow);
                display.DrawResource("menuBorderVerticalLine", Width - text.Length - 4, 0);
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

            public void RenderInputField(int length, string text, int x = -1, int y = -1)
            {
                if (x == -1)
                {
                    x = Width / 2 - length / 2;
                }
                if (y == -1)
                {
                    y = Height / 2 - 1;
                }

                for (int i = 0; i < length; i++)
                {
                    display.DrawResource("inputFieldMiddle", x + i, y);
                }
                display.Write(text, x, y + 1);
                if (text.Length < length)
                {
                    display.Write("_", x + text.Length, y + 1);
                }
                display.DrawResource("inputFieldLeft", x - 1, y);
                display.DrawResource("inputFieldRight", x + length, y);
            }

            public void ClearDisplay()
            {
                StringBuilder sbClearline = new StringBuilder().Append(' ', Width);
                for (int i = 0; i < Height; i++)
                {
                    display.Write(sbClearline.ToString(), 0, i, ConsoleColor.Gray, ConsoleColor.Black);
                }
            }

            public void RenderHealthBar(MobAttackable mob, int length, int? x, int? y)
            {
                int alive = (int)(((double)mob.Health / mob.MaxHealth) * length);

                for (int i = 0; i < length; i++)
                {
                    display.Write(" ", ((Width / 2) - (length / 2)) + i, y, i < alive ? ConsoleColor.Black : ConsoleColor.White, i < alive ? ConsoleColor.Green : ConsoleColor.Red);
                }
            }

            public void RenderHealthText(MobAttackable mob, int? x, int? y)
            {
                string healthString = $"{mob.Health}/{mob.MaxHealth}";
                display.Write(healthString, x, y);
            }
        }
    }
}
