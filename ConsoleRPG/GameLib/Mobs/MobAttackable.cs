using GameLib.Effects;
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
        private static Random rand = new Random();

        public int Health { get; set; }
        public int MaxHealth { get; }
        public bool Alive => Health > 0;

        public MobAttackable(int x, int y, int health) : base(x, y)
        {
            MaxHealth = health;
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

        public int Damage(MobAttackable enemy)
        {
            int damage = enemy.CalculateDamage();
            int accuracy = enemy.CalculateAccuracy();
            int armor = CalculateArmor();

            damage = damage < 0 ? 0 : damage;
            accuracy = accuracy < 0 ? 0 : accuracy;
            armor = armor < 0 ? 0 : armor;

            int effectiveAccuracy = accuracy > (armor * 2) ? (armor * 2) : accuracy;
            int armorPenetration = armor <= 0 ? 1 : (effectiveAccuracy / (armor * 2));
            int damagePenetration = damage * armorPenetration;

            damagePenetration = damagePenetration < 0 ? 0 : damagePenetration;

            //int damageMin = damagePenetration

            int actualDamage = /*VALUE*/1;
            DamageDirectly(actualDamage);
            return actualDamage;
        }

        protected abstract int CalculateDamage();

        protected abstract int CalculateAccuracy();

        protected abstract int CalculateArmor();

        private void UpdateHealth()
        {
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
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
