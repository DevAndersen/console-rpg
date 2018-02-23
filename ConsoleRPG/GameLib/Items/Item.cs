using GameLib.GameCore;
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
        public bool Tradable { get { return Price != untradablePrice; } }

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
        }
    }
}
