using GameLib.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplayRoom : Display
    {
        private Room room;

        public DisplayRoom(Display previousDisplay) : base(previousDisplay)
        {

        }

        public override Display Run()
        {
            ConsoleKey read = ReadKey();
            if (read == ConsoleKey.E)
            {
                return new DisplayInventory(this, GameCore.Core.instance.game.player.inventory);
            }
            else if (read == ConsoleKey.Escape)
            {
                return new DisplayPauseMenu(this);
            }
            return this;
        }

        protected override void RenderDisplay()
        {
            prefabs.RenderBorderBottomBar();
            prefabs.RenderMenuBar(new MenuBarItem[]
            {
                new MenuBarItem("[WIP] ROOM DESCRIPTION"),
                new MenuBarItem("W, A, S, D to move", ConsoleColor.DarkCyan),
                new MenuBarItem(ConsoleKey.E, "Character menu", ConsoleColor.Green),
                new MenuBarItem(ConsoleKey.Escape, "Pause menu", ConsoleColor.Yellow)
            });
        }
    }
}
