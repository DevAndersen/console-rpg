﻿using GameLib.Effects;
using GameLib.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items.Consumables
{
    [Serializable]
    public class ItemPotion : ItemConsumable
    {
        private Effect effect;

        public ItemPotion(string name, bool stackable, Effect effect, int price = -1) : base(name, stackable, price)
        {
            this.effect = effect;
        }

        override public void OnConsume(MobAttackable consumingMob)
        {
            consumingMob.AddEffect(effect);
        }

        override protected string ProvideConsumeString()
        {
            return "Drink"; 
        }
    }
}
