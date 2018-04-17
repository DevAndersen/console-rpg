using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Mobs
{
    [Serializable]
    public class MobPlayer : MobAttackable
    {
        public override string Name => player.Name;
        public override char Character => 'X';
        public override ConsoleColor Color => ConsoleColor.White;

        public new int Health
        {
            get => player.Health;
            set => player.Health = value;
        }

        private Player player = Core.instance.game.player;

        public MobPlayer(int x, int y, int health) : base(x, y, health)
        {

        }

        protected override int CalculateDamage()
        {
            return player.Weapon == null ? 1 : (player.Weapon.Type.BaseDamage * player.Weapon.Material.Multiplier);
        }

        protected override int CalculateAccuracy()
        {
            return player.Weapon == null ? 1 : (player.Weapon.Type.BaseAccuracy * player.Weapon.Material.Multiplier);
        }

        protected override int CalculateArmor()
        {
            int head = player.Head == null ? 1 : (player.Head.Type.BaseArmor * player.Head.Material.Multiplier);
            int torso = player.Torso == null ? 1 : (player.Torso.Type.BaseArmor * player.Torso.Material.Multiplier);
            int legs = player.Legs == null ? 1 : (player.Legs.Type.BaseArmor * player.Legs.Material.Multiplier);
            int hands = player.Hands == null ? 1 : (player.Hands.Type.BaseArmor * player.Hands.Material.Multiplier);
            int feet = player.Feet == null ? 1 : (player.Feet.Type.BaseArmor * player.Feet.Material.Multiplier);

            return head + torso + legs + hands + feet;
        }
    }
}
