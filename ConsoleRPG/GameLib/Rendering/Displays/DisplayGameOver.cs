using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplayGameOver : Display
    {
        public DisplayGameOver(Display previousDisplay) : base(previousDisplay)
        {

        }

        public override Display Run()
        {
            ConsoleKey read = ReadKey();
            if (read == ConsoleKey.Enter)
            {
                return new DisplayMainMenu(null);
            }
            return this;
        }

        protected override void RenderDisplay()
        {
            DrawResource("simpleBorder", 0, 0);
            Write("Game Over", null, null, ConsoleColor.Red);
            Write("Press Enter to continue", null, Height - 2);
        }
    }
}
