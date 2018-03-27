using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Mob
{
    [Serializable]
    public class MobPlayer : MobAttackable
    {
        private Player player;

        public MobPlayer(Player player, int health) : base(health)
        {
            player = Core.instance.game.player;
        }
    }
}
