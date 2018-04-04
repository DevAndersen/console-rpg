using GameLib.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rooms
{
    public static class TilesList
    {
        public static readonly Tile wall = new Tile(ConsoleColor.DarkGray, true);
        public static readonly Tile floor = new Tile(ConsoleColor.Black, false);
    }
}
