using GameLib.GameCore;
using GameLib.Items;
using GameLib.Rendering;
using GameLib.Rendering.Displays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameConsole
{
    class Program : IGameHandler
    {
        private Pxl[,] grid;
        private StringBuilder sbClearline = new StringBuilder().Append(' ', Display.Width);
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            new Program().StartGame();
        }

        public void StartGame()
        {
            Console.WindowWidth = Display.Width;
            Console.WindowHeight = Display.Height + 2;
            grid = new Pxl[Display.Width, Display.Height];
            
            Core.StartGame(this);
        }

        private void Render(Pxl[,] newGrid)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < newGrid.GetLength(1); y++)
            {
                for (int x = 0; x < newGrid.GetLength(0); x++)
                {
                    Pxl pxl = newGrid[x, y];

                    if (Equals(pxl, grid[x, y]))
                    {
                        grid[x, y] = pxl;
                    }
                    else
                    {
                        grid[x, y] = pxl;

                        if(Console.ForegroundColor != ConsoleColor.Gray)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (Console.BackgroundColor != ConsoleColor.Black)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                        }

                        Console.SetCursorPosition(x, y);

                        if (pxl == null)
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            if (pxl.ForegroundColor.HasValue && pxl.ForegroundColor != Console.ForegroundColor)
                            {
                                Console.ForegroundColor = pxl.ForegroundColor.Value;
                            }

                            if (pxl.BackgroundColor.HasValue && pxl.BackgroundColor != Console.BackgroundColor)
                            {
                                Console.BackgroundColor = pxl.BackgroundColor.Value;
                            }

                            if (pxl.Char == null)
                            {
                                Console.Write(" ");
                            }
                            else
                            {
                                Console.Write(pxl.Char);
                            }

                            if (pxl.WaitMs > 0)
                            {
                                WaitMs(pxl.WaitMs);
                            }
                        }
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, Display.Height);
            Console.Write(sbClearline);
            Console.SetCursorPosition(0, Display.Height);
        }

        public void OnDisplayUpdate(Pxl[,] grid)
        {
            Render(grid);
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WaitMs(int ms)
        {
            Thread.Sleep(ms);
        }
    }
}
