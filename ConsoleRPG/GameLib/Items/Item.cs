using GameLib.GameCore;
using GameLib.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    [Serializable]
    public class Item
    {
        private const int untradablePrice = -1;

        public virtual string Name { get; }
        public bool Stackable { get; set; }
        public int Price { get; }
        public bool Tradable { get => Price != untradablePrice; }
        public bool CanBeUsed => canBeUsed;

        protected bool canBeUsed = true;

        public Item(string name, bool stackable, int price = untradablePrice)
        {
            Name = name;
            Stackable = stackable;
            if (price != untradablePrice && price < 1)
            {
                Logger.Log($"Item '{Name}' had unacceptable price {price}. Defaults to {untradablePrice}, untradable.", LoggingLevel.Error);
                price = untradablePrice;
            }
            Price = price;
            canBeUsed = false;
        }

        public virtual bool OnUse(MobPlayer player)
        {
            return false;
        }

        public virtual string GetUseString()
        {
            return "Use";
        }
    }
}
