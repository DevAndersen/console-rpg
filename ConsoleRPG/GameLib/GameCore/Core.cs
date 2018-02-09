using GameLib.Rendering;
using GameLib.Rendering.Displays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.GameCore
{
    public class Core
    {
        public static Core instance;

        public delegate void DisplayUpdate(Pxl[,] grid);
        event DisplayUpdate OnDisplayUpdate;

        public IGameHandler gameHandler;

        public bool DebugMode { get; set; }

        private Display currentDisplay;
        private Display NextDisplay;

        public static void StartGame(IGameHandler gameHandler)
        {
            instance = new Core(gameHandler);
            instance.RunGame();
        }

        private Core(IGameHandler gameHandler)
        {
            instance = this;
            this.gameHandler = gameHandler;

            OnDisplayUpdate += this.gameHandler.OnDisplayUpdate;
        }

        private void RunGame()
        {
            currentDisplay = new DisplaySplash();
            while(currentDisplay != null)
            {
                OnDisplayUpdate.Invoke(currentDisplay.Render());
                NextDisplay = currentDisplay.Run();
                currentDisplay = NextDisplay;
            }
            Logger.GetLog().ToList().ForEach(x => Console.WriteLine($"[{x.timestamp.ToString("hh:mm:ss")}] <{x.level}>\t{x.message}"));
        }

        public ConsoleKeyInfo ReadKey()
        {
            return gameHandler.ReadKey();
        }

        public string ReadLine()
        {
            return gameHandler.ReadLine();
        }

        public void WaitMs(int ms)
        {
            gameHandler.WaitMs(ms);
        }
    }
}
