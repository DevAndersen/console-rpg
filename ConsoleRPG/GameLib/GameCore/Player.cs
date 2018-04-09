using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.GameCore
{
    [Serializable]
    public class Player
    {
        public string Name { get; }

        public Inventory inventory;

        public Player(string name)
        {
            Name = name;
            inventory = new Inventory(Name, 30);
        }
    }
}
