using GameLib.Rendering.Displays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.GameCore
{
    [Serializable]
    public class Game
    {
        public bool DebugMode { get; set; } = true;

        public Display currentDisplay;
        public Display NextDisplay;
    }
}
