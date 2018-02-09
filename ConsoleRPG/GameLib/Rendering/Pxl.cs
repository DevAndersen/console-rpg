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

        public override bool Equals(object obj)
        {
            if (this == null && obj == null)
                return true;

            if (obj is Pxl pxl)
            {
                bool charEqual = Char == pxl.Char;
                bool foreEqual = ForegroundColor == pxl.ForegroundColor;
                bool backEqual = BackgroundColor == pxl.BackgroundColor;
                return charEqual && foreEqual && backEqual;
            }
            return false;
        }
    }
}
