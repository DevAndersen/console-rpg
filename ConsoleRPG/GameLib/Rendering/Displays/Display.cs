using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public abstract partial class Display
    {
        public static int Width { get { return 151; } }
        public static int Height { get { return 41; } }

        protected Pxl[,] grid = new Pxl[Width, Height];
        protected Display previousDisplay;

        public abstract Display Run();

        protected abstract void RenderDisplay();

        protected Display(Display previousDisplay)
        {
            this.previousDisplay = previousDisplay;
            prefabs = new Prefabs(this);
        }

        public Pxl[,] Render()
        {
            Array.Clear(grid, 0, grid.Length);

            RenderDisplay();

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

        protected void Write(string text, int posX, int posY, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, int waitMs = 0)
        {
            if (posX < 0 || posY < 0 || posX + text.Length > Width || posY >= Height)
            {
                Logger.Log($"Tried to write string '{text}' outside display for {GetType()}.", LoggingLevel.Error);
                return;
            }
            for (int i = 0; i < text.Length; i++)
            {
                grid[posX + i, posY] = new Pxl(text[i], foregroundColor, backgroundColor, waitMs);
            }
        }

        protected void WriteCentered(string text, int posY, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, int waitMs = 0)
        {
            int x = Width / 2 - text.Length / 2;
            Write(text, x, posY, foregroundColor, backgroundColor, waitMs);
        }

        /// <summary>
        /// Draws the specified resource, according to the specified parameters. In the resource, '~' will not be drawn, allowing for "transparency".
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="waitMs"></param>
        protected (int resourceX, int resourceY, int resourceWidth, int resourceHeight) DrawResource(string resourceKey, int? posX, int? posY, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null, int waitMs = 0)
        {
            char[,] resource = ResourceProvider.GetResource(resourceKey);

            if (resource == null)
            {
                Logger.Log($"Could not find resource '{resourceKey}' for display {GetType()}.", LoggingLevel.Error);
                return (posX ?? 0, posY ?? 0, 0, 0);
            }

            int resourceWidth = resource.GetLength(1);
            int resourceHeight = resource.GetLength(0);
            int realPosX = posX ?? (Width / 2) - (resourceWidth / 2);
            int realPosY = posY ?? (Height / 2) - (resourceHeight / 2);

            if(realPosX < 0 || realPosY < 0 || realPosX + resourceWidth > Width || realPosY + resourceHeight > Height)
            {
                Logger.Log($"Tried to draw resource '{resourceKey}' outside display for {GetType()}.", LoggingLevel.Error);
                return (posX ?? 0, posY ?? 0, 0, 0);
            }

            for (int x = 0; x < resourceWidth; x++)
            {
                for (int y = 0; y < resourceHeight; y++)
                {
                    if(resource[y, x] != '~')
                    {
                        grid[realPosX + x, realPosY + y] = new Pxl(resource[y, x], foregroundColor, backgroundColor, waitMs);
                    }
                }
            }
            return (realPosX, realPosY, resourceWidth, resourceHeight);
        }
    }
}
