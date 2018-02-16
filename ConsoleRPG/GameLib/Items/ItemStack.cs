using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    [Serializable]
    public class ItemStack
    {
        public Item Item { get; set; }
        public int Amount { get; set; }

        public ItemStack(Item item, int amount = 1)
        {
            if ((item.Stackable && amount > 0) || (!item.Stackable && amount == 1))
            {
                Item = item;
                Amount = amount;
            }
            else
            {
                Item = Item.Items.fallback;
                Amount = 1;
                Logger.Log($"Failed at instantiating itemstack {item}x{amount}.", LoggingLevel.Critical);
            }
        }

        public override string ToString()
        {
            if(Item.Stackable)
            {
                return $"{Amount} x {Item.Name}";
            }
            return $"{Item.Name}";
        }
    }
}
