using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Mobs
{
    [Serializable]
    public class Drop
    {
        private static Random rand = new Random();
        
        public double Chance { get; }

        private Item item;
        private int amountSpecific;
        private int amountMin;
        private int amountMax;

        bool dropSpecificAmount;

        private Drop(Item item, double chance)
        {
            this.item = item;
            Chance = chance;
        }

        public Drop(Item item, double chance, int amount = 1) : this(item, chance)
        {
            dropSpecificAmount = true;
            amountSpecific = amount;
        }

        public Drop(Item item, double chance, int amountMin, int amountMax)
        {
            dropSpecificAmount = false;
            this.amountMin = amountMin;
            this.amountMax = amountMax;
        }

        public ItemStack GetItemStack()
        {
            return new ItemStack(item, GetDropAmount());
        }

        private int GetDropAmount()
        {
            if (dropSpecificAmount)
            {
                return amountSpecific;
            }
            return rand.Next(amountMin, amountMax);
        }
    }
}
