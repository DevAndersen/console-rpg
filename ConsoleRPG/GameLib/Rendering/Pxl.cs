using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering
{
    [Serializable]
    public class Pxl
    {
        public char? Char { get; }
        public ConsoleColor? ForegroundColor { get; }
        public ConsoleColor? BackgroundColor { get; }
        public int WaitMs { get; }

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

        /// <summary>
        /// Auto generated.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = -893613736;
            hashCode = hashCode * -1521134295 + EqualityComparer<char?>.Default.GetHashCode(Char);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConsoleColor?>.Default.GetHashCode(ForegroundColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConsoleColor?>.Default.GetHashCode(BackgroundColor);
            hashCode = hashCode * -1521134295 + WaitMs.GetHashCode();
            return hashCode;
        }
    }
}
