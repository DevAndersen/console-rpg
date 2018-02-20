using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplaySplash : Display
    {
        public DisplaySplash(Display previousDisplay) : base(previousDisplay)
        {

        }

        public override Display Run()
        {
            return new DisplayMainMenu(this);
        }

        protected override void RenderDisplay()
        {
            prefabs.ClearDisplay();
        }
    }
}
