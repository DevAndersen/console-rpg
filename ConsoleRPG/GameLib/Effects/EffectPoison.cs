using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib.Mob;

namespace GameLib.Effects
{
    [Serializable]
    public class EffectPoison : Effect
    {
        public EffectPoison(string name, EffectType effectType, int rounds, int strength) : base(name, effectType, rounds, strength)
        {
        }

        public override void ActivateEffect(MobAttackable mob)
        {
            mob.DamageDirectly(Strength);
        }
    }
}
