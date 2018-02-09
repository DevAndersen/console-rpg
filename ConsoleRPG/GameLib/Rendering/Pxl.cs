using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering
{
    public class Pxl
    {
        public char? Char { get; set; }
        public ConsoleColor? ForegroundColor { get; set; }
        public ConsoleColor? BackgroundColor { get; set; }
        public int WaitMs { get; set; }

        public Pxl(char? character = null, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, int waitMs = 0)
        {
            Char = character;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            WaitMs = waitMs;
        }
    }
}
