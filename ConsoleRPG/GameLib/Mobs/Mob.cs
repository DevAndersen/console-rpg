using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Mobs
{
    [Serializable]
    public abstract class Mob
    {
        public abstract string Name { get; }
        public abstract char Character { get; }
        public abstract ConsoleColor Color { get; }

        public int X { get; set; }
        public int Y { get; set; }

        public Mob(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
