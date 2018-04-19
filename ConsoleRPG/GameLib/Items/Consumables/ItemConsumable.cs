using GameLib.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items.Consumables
{
    [Serializable]
    public abstract class ItemConsumable : Item
    {
        public ItemConsumable(string name, bool stackable, int price = -1) : base(name, stackable, price)
        {
            canBeUsed = true;
        }

        public override bool OnUse(MobPlayer player)
        {
            OnConsume(player);
            return true;
        }

        public abstract void OnConsume(MobPlayer player);

        public override string GetUseString()
        {
            return GetConsumeString();
        }

        protected abstract string GetConsumeString();
    }
}
