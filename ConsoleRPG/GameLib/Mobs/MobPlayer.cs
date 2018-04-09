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
        public override char Character => 'X';
        public override ConsoleColor Color => ConsoleColor.White;

        private Player player = Core.instance.game.player;

        public MobPlayer(int x, int y, int health) : base(x, y, health)
        {

        }
    }
}
