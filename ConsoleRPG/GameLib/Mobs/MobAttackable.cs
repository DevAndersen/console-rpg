using GameLib.Effects;
using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Mobs
{
    [Serializable]
    public abstract class MobAttackable : Mob
    {
        public int Health { get; set; }
        public bool Alive { get => Health > 0; }

        public ItemArmor Helm { get; set; }
        public ItemArmor Torso { get; set; }
        public ItemArmor Legs { get; set; }
        public ItemArmor Hands { get; set; }
        public ItemArmor Feet { get; set; }

        public ItemWeapon Weapon { get; set; }

        private int maxHealth;

        public MobAttackable(int x, int y, int health) : base(x, y)
        {
            maxHealth = health;
            Health = health;
        }

        private List<Effect> effects = new List<Effect>();

        public void Heal(int healAmount)
        {
            Health += healAmount;
            UpdateHealth();
        }

        public void DamageDirectly(int damageAmount)
        {
            Health -= damageAmount;
            UpdateHealth();
        }

        public void Damage(MobAttackable enemy)
        {
            throw new NotImplementedException();

            UpdateHealth();
        }

        private void UpdateHealth()
        {
            if (Health > maxHealth)
            {
                Health = maxHealth;
            }
            else if (Health < 1)
            {
                Health = 0;
            }
        }

        private void EffectTick()
        {
            List<Effect> effectsToRemove = new List<Effect>();

            foreach (Effect effect in effects)
            {
                effect.ActivateEffect(this);
                effect.Rounds--;

                if (effect.Rounds <= 0)
                {
                    effectsToRemove.Add(effect);
                }
            }

            foreach (Effect effectToRemove in effectsToRemove)
            {
                effects.Remove(effectToRemove);
            }
        }

        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
        }
    }
}
