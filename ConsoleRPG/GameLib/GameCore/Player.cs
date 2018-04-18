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
        public int Health { get; set; }

        public Inventory inventory;

        public ItemArmor Head { get; set; }
        public ItemArmor Torso { get; set; }
        public ItemArmor Legs { get; set; }
        public ItemArmor Hands { get; set; }
        public ItemArmor Feet { get; set; }

        public ItemWeapon Weapon { get; set; }

        public Player(string name)
        {
            Name = name;
            inventory = new Inventory(Name, 30);
            AddStartItems();
        }

        private void AddStartItems()
        {
            inventory.AddItemStack(new ItemStack(ItemsList.coin, 10));
            inventory.AddItemStack(new ItemStack(ItemsList.bread, 1));
        }
    }
}
