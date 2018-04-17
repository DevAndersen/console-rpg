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
        public static string GameName { get { return "ConsoleRPG"; } }

        public static Core instance;

        public delegate void DisplayUpdate(Pxl[,] grid);
        event DisplayUpdate OnDisplayUpdate;

        public IGameHandler gameHandler;

        public Game game;

        public static void StartGame(IGameHandler gameHandler)
        {
            instance = new Core(gameHandler);
            instance.RunGame();
        }

        private Core(IGameHandler gameHandler)
        {
            instance = this;
            game = new Game();
            this.gameHandler = gameHandler;

            OnDisplayUpdate += this.gameHandler.OnDisplayUpdate;
        }

        private void RunGame()
        {
            game.currentDisplay = new DisplaySplash(null);
            while (game.currentDisplay != null)
            {
                OnDisplayUpdate.Invoke(game.currentDisplay.Render());
                game.NextDisplay = game.currentDisplay.Run();
                game.currentDisplay = game.NextDisplay;
            }
        }

        public void ForceDisplayUpdate()
        {
            OnDisplayUpdate.Invoke(game.currentDisplay.Render());
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

        public void SaveGame()
        {
            Serializer.SaveGame(game);
        }

        public void LoadGame()
        {
            game = Serializer.LoadGame();
        }
    }
}
