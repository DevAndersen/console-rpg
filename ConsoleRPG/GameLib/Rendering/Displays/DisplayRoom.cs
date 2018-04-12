using GameLib.Mobs;
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
            room = new Room(1000);
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
            else if (read == ConsoleKey.W || read == ConsoleKey.A || read == ConsoleKey.S || read == ConsoleKey.D)
            {
                MobPlayer player = room.GetPlayer();
                int x = (read == ConsoleKey.A ? -1 : (read == ConsoleKey.D ? 1 : 0));
                int y = (read == ConsoleKey.W ? -1 : (read == ConsoleKey.S ? 1 : 0));
                bool result = room.MoveMobRelative(player, x, y);
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

            //(char? c, ConsoleColor foregroundColor, ConsoleColor backgroundColor)[,] roomGrid = new (char? c, ConsoleColor foregroundColor, ConsoleColor backgroundColor)[Width, Height];

            int offsetX = Width / 2 - room.Width / 2;
            int offsetY = Height / 2 - room.Height / 2 - 2;

            for (int x = 0; x < room.Width; x++)
            {
                for (int y = 0; y < room.Height; y++)
                {
                    Tile tile = room.GetTile(x, y);
                    Mob mob = room.GetMobForPos(x, y);

                    string character = mob == null ? " " : mob.Character.ToString();
                    ConsoleColor foregroundColor = mob == null ? ConsoleColor.White : mob.Color;
                    ConsoleColor backgroundColor = tile.Color;

                    Write(character, offsetX + x + 1, offsetY + y + 1, foregroundColor, backgroundColor);
                }
            }
        }
    }
}
