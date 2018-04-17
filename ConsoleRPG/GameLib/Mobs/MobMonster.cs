using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Mobs
{
    [Serializable]
    public class MobMonster : MobAttackable
    {
        public override string Name => Monster.Name;
        public override char Character => 'X';
        public override ConsoleColor Color => ConsoleColor.Red;
        public Monster Monster { get; }

        public MobMonster(Monster monster, int x, int y) : base(x, y, monster.Health)
        {
            Monster = monster;
        }

        protected override int CalculateDamage()
        {
            return Monster.Damage;
        }

        protected override int CalculateAccuracy()
        {
            return Monster.Accuracy;
        }

        protected override int CalculateArmor()
        {
            return Monster.Armor;
        }
    }
}
