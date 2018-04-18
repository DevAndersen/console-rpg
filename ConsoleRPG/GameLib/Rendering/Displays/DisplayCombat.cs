using GameLib.GameCore;
using GameLib.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplayCombat : Display
    {
        public MobPlayer Player { get; }
        public MobMonster Enemy { get; }

        private int? playerDamage = null;
        private int? enemyDamage = null;

        public DisplayCombat(Display previousDisplay, MobPlayer player, MobMonster enemy) : base(previousDisplay)
        {
            Player = player;
            Enemy = enemy;
        }

        public override Display Run()
        {
            if (!Enemy.Alive)
            {
                return OnPlayerVictorious();
            }

            ConsoleKey read = ReadKey();
            if (read == ConsoleKey.A)
            {
                playerDamage = Enemy.Damage(Player);
                RenderDisplay();
                return AfterPlayerAction();
            }
            else if (read == ConsoleKey.E)
            {
                return new DisplayInventory(this, Core.instance.game.player.inventory);
            }
            return this;
        }

        private Display AfterPlayerAction()
        {
            if (!Enemy.Alive)
            {
                return OnPlayerVictorious();
            }

            enemyDamage = Player.Damage(Enemy);

            if (!Player.Alive)
            {
                return new DisplayGameOver(null);
            }
            return this;
        }

        private Display OnPlayerVictorious()
        {
            return new DisplayLootInventory(previousDisplay, Enemy.Monster, Enemy.Monster.GenerateDrops(), Player.Player.inventory);
        }

        protected override void RenderDisplay()
        {
            prefabs.RenderMenuBorder(Enemy.Name);
            DrawResource("menuBorderHorizontalLine", 0, Height - 3);
            prefabs.RenderMenuBar(new MenuBarItem[]
            {
                new MenuBarItem(ConsoleKey.A, "Attack", ConsoleColor.Green),
                new MenuBarItem(ConsoleKey.E, "Character menu", ConsoleColor.Yellow),
            });

            prefabs.RenderHealthBar(Enemy, 100, null, 4);
            prefabs.RenderHealthBar(Player, 100, null, Height - 5);

            prefabs.RenderHealthText(Enemy, null, 6);
            prefabs.RenderHealthText(Player, null, Height - 7);

            if (playerDamage.HasValue)
            {
                Write(playerDamage.Value.ToString(), null, 7, ConsoleColor.Red);
            }

            if (enemyDamage.HasValue)
            {
                Write(enemyDamage.Value.ToString(), null, Height - 8, ConsoleColor.Red);
            }
        }
    }
}
