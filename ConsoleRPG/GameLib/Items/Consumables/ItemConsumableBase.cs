using GameLib.Mob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items.Consumables
{
    [Serializable]
    public abstract class ItemConsumableBase : Item
    {
        public string ConsomeString { get { return ProvideConsumeString(); } }

        public ItemConsumableBase(string name, bool stackable, int price = -1) : base(name, stackable, price)
        {

        }

        public abstract void OnConsume(MobAttackable consumingMob);

        protected abstract string ProvideConsumeString();
    }
}
