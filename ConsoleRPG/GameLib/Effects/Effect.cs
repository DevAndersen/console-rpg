using GameLib.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Effects
{
    [Serializable]
    public abstract class Effect
    {
        public string Name { get; }
        public EffectType EffectType { get; }
        public int Rounds { get; set; }
        public int Strength { get; set; }
        public bool HasLimitedRounds
        {
            get
            {
                return Rounds != -1;
            }
        }

        public Effect(string name, EffectType effectType, int rounds, int strength)
        {
            Name = name;
            effectType = EffectType;
            Rounds = rounds;
            Strength = strength;
        }

        public abstract void ActivateEffect(MobAttackable mob);
    }
}
