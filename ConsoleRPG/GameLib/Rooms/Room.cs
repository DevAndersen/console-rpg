using GameLib.Rendering.Displays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rooms
{
    public class Room
    {
        public int Seed { get; }
        public int Width { get; }
        public int Height { get; }

        private Tile[,] tiles;

        public Room(int seed)
        {
            Seed = seed;
            Width = Display.Width - 2;
            Height = Display.Height - 4;
            tiles = new Tile[Width, Height];
            InitRoom();
        }

        private void InitRoom()
        {

        }
    }
}
