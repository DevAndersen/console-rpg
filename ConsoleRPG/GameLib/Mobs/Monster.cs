using GameLib.GameCore;
using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Mobs
{
    public class Monster
    {
        private static Random rand = new Random();

        public string Name { get; }
        public int Health { get; }
        public int Damage { get; }
        public int Accuracy { get; }
        public int Armor { get; }

        private List<Drop> dropTable = new List<Drop>();

        public Monster(string name, int health, int damage, int accuracy, int armor, List<Drop> dropTable)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Accuracy = accuracy;
            Armor = armor;
            this.dropTable = dropTable;
        }

        public List<ItemStack> GenerateDrops()
        {
            List<ItemStack> drops = new List<ItemStack>();

            foreach (Drop drop in dropTable)
            {
                if (GenerateDropChance() <= drop.Chance)
                {
                    drops.Add(drop.GetItemStack());
                }
            }
            return drops;
        }

        private double GenerateDropChance()
        {
            return Core.instance.game.DebugMode ? 0 : rand.NextDouble();
        }
    }
}
