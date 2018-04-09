using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Mobs;

namespace GameLib.Items.Consumables.Foods
{
    [Serializable]
    public class ItemEdible : ItemConsumableBase
    {
        public int HealAmount { get; }
        private EdibleType edibleType;

        public ItemEdible(string name, bool stackable, int healAmount, EdibleType edibleType, int price = -1) : base(name, stackable, price)
        {
            HealAmount = healAmount;
            this.edibleType = edibleType;
        }

        public override void OnConsume(MobAttackable consumingMob)
        {
            consumingMob.Heal(HealAmount);
        }

        protected override string ProvideConsumeString()
        {
            return edibleType == EdibleType.Food ? "Eat" : "Drink";
        }
    }
}
