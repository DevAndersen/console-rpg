using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class MenuBarItem
    {
        public ConsoleKey? Key { get; }
        public string Text { get; }
        public ConsoleColor Color { get; }

        public MenuBarItem(ConsoleKey key, string text, ConsoleColor color)
        {
            Key = key;
            Text = text;
            Color = color;
        }

        public MenuBarItem(string text, ConsoleColor color = ConsoleColor.DarkGray)
        {
            Text = text;
            Color = color;
        }
    }
}
