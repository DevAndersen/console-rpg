using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Mobs
{
    public static class MonsterList
    {
        public static readonly Monster zombie = new Monster("Zombie", 10, 3, 2, 1, new List<Drop>()
        {
            new Drop(ItemsList.bread, 1/2, 1, 2),
            new Drop(ItemsList.coin, 1/2, 1, 5)
        });

        public static readonly Monster skeleton = new Monster("Skeleton", 10, 1, 3, 2, new List<Drop>()
        {
            new Drop(ItemsList.bone, 1),
            new Drop(ItemsList.coin, 1/2, 1, 10)
        });
    }
}
