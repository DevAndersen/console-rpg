using GameLib.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rooms
{
    [Serializable]
    public class Tile
    {
        public ConsoleColor Color { get; }
        public bool Solid { get; }

        public Tile(ConsoleColor color, bool solid)
        {
            Color = color;
            Solid = solid;
        }
    }
}
