using GameLib.Effects;
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

        public override void OnConsume(MobPlayer player)
        {
            player.AddEffect(effect);
        }

        protected override string GetConsumeString()
        {
            return "Drink"; 
        }
    }
}
