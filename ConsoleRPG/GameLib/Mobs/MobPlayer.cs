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
        public override string Name => Player.Name;
        public override char Character => 'X';
        public override ConsoleColor Color => ConsoleColor.White;
        public Player Player => Core.instance.game.player;

        public new int Health
        {
            get => Player.Health;
            set => Player.Health = value;
        }

        public MobPlayer(int x, int y, int health) : base(x, y, health)
        {

        }

        protected override int CalculateDamage()
        {
            return Player.Weapon == null ? 1 : (Player.Weapon.Type.BaseDamage * Player.Weapon.Material.Multiplier);
        }

        protected override int CalculateAccuracy()
        {
            return Player.Weapon == null ? 1 : (Player.Weapon.Type.BaseAccuracy * Player.Weapon.Material.Multiplier);
        }

        protected override int CalculateArmor()
        {
            int head = Player.Head == null ? 1 : (Player.Head.Type.BaseArmor * Player.Head.Material.Multiplier);
            int torso = Player.Torso == null ? 1 : (Player.Torso.Type.BaseArmor * Player.Torso.Material.Multiplier);
            int legs = Player.Legs == null ? 1 : (Player.Legs.Type.BaseArmor * Player.Legs.Material.Multiplier);
            int hands = Player.Hands == null ? 1 : (Player.Hands.Type.BaseArmor * Player.Hands.Material.Multiplier);
            int feet = Player.Feet == null ? 1 : (Player.Feet.Type.BaseArmor * Player.Feet.Material.Multiplier);

            return head + torso + legs + hands + feet;
        }
    }
}
