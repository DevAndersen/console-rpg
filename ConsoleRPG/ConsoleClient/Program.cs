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
        static void Main(string[] args)
        {
            new Program().StartGame();
            Console.WriteLine("GAME OVER");
            Console.ReadLine();
        }

        public void StartGame()
        {
            Console.WindowWidth = Display.Width;
            Console.WindowHeight = Display.Height + 1;
            
            Core.StartGame(this);
        }

        private void Render(Pxl[,] grid)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    Pxl pxl = grid[x, y];

                    if(pxl == null)
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

                        if(pxl.Char == null)
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write(pxl.Char);
                        }

                        if(pxl.WaitMs > 0)
                        {
                            WaitMs(pxl.WaitMs);
                        }
                    }
                }
            }
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
