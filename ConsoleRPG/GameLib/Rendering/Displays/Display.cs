using GameLib.GameCore;
using GameLib.Rendering.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public abstract class Display
    {
        public static int Width { get { return 150; } }
        public static int Height { get { return 40; } }

        protected Pxl[,] grid = new Pxl[Width, Height];

        protected Menu menu;

        public abstract Display Run();

        protected abstract void RenderDisplay();

        public Pxl[,] Render()
        {
            Array.Clear(grid, 0, grid.Length);

            RenderDisplay();

            if(menu != null)
            {
                Pxl[,] menuGrid = menu.Render();
            }

            return grid;
        }

        protected ConsoleKeyInfo ReadKeyInfo()
        {
            return Core.instance.ReadKey();
        }

        protected ConsoleKey ReadKey()
        {
            return ReadKeyInfo().Key;
        }

        protected int? ReadKeyAsDigit()
        {
            ConsoleKeyInfo key = ReadKeyInfo();
            if(char.IsDigit(key.KeyChar))
            {
                return (int)Char.GetNumericValue(key.KeyChar);
            }
            return null;
        }

        protected string ReadLine()
        {
            return Core.instance.ReadLine();
        }

        protected void Wait(int ms)
        {
            Core.instance.WaitMs(ms);
        }

        protected void DrawResource(string resourceKey, int posX, int posY, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, int waitMs = 0)
        {
            char[,] resource = ResourceProvider.GetResource(resourceKey);

            if (resource == null)
            {
                Logger.Log($"Could not find resource {resourceKey} for display {GetType()}.", LoggingLevel.Error);
                return;
            }

            int resourceWidth = resource.GetLength(1);
            int resourceHeight = resource.GetLength(0);

            if(posX < 0 || posY < 0 || posX + resourceWidth > Width || posY + resourceHeight > Height)
            {
                Logger.Log($"Tried to draw resource {resourceKey} outside display for {GetType()}.", LoggingLevel.Error);
                return;
            }

            for (int x = 0; x < resourceWidth; x++)
            {
                for (int y = 0; y < resourceHeight; y++)
                {
                    grid[posX + x, posY + y] = new Pxl(resource[y, x], foregroundColor, backgroundColor, waitMs);
                }
            }
        }
    }
}
