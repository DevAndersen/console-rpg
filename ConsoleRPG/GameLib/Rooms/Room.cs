using GameLib.GameCore;
using GameLib.Mobs;
using GameLib.Rendering.Displays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rooms
{
    [Serializable]
    public class Room
    {
        public int Seed { get; }
        public int Width { get; }
        public int Height { get; }
        
        private Tile[,] tiles;
        public List<Mob> mobs;

        public Room(int seed)
        {
            Seed = seed;
            Width = Display.Width - 2;
            Height = Display.Height - 4;
            tiles = new Tile[Width, Height];

            mobs = new List<Mob>();
            InitRoom();
            PopulateRoom();
        }

        private void InitRoom()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                    {
                        tiles[x, y] = TilesList.wall;
                    }
                    else
                    {
                        tiles[x, y] = TilesList.floor;
                    }
                }
            }
        }

        private void PopulateRoom()
        {
            MobPlayer player = new MobPlayer(2, 2, 20);
            mobs.Add(player);
        }

        private void Tick()
        {

        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[x, y] = tile;
        }

        public void AddMob(Mob mob)
        {
            mobs.Add(mob);
        }

        public Mob GetMobForPos(int x, int y)
        {
            foreach (Mob mob in mobs)
            {
                if (x == mob.X && y == mob.Y)
                {
                    return mob;
                }
            }
            return null;
        }

        public void RemoveMob(Mob mob)
        {
            mobs.Remove(mob);
        }

        public MobPlayer GetPlayer()
        {
            foreach (Mob mob in mobs)
            {
                if (mob is MobPlayer)
                {
                    return mob as MobPlayer;
                }
            }
            Logger.Log("Could not find player in room.", LoggingLevel.Error);
            return null;
        }

        public bool IsTileEmpty(int x, int y)
        {
            foreach (Mob mob in mobs)
            {
                if (mob.X == x && mob.Y == y)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanMobMoveTo(Mob mob, int x, int y)
        {
            return IsTileEmpty(x, y) && !tiles[x, y].Solid;
        }

        public bool MoveMob(Mob mob, int x, int y)
        {
            if (CanMobMoveTo(mob, x, y))
            {
                mob.Move(x, y);
                return true;
            }
            return false;
        }

        public bool MoveMobRelative(Mob mob, int x, int y)
        {
            return MoveMob(mob, mob.X + x, mob.Y + y);
        }
    }
}
