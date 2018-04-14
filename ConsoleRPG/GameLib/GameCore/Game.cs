using GameLib.Rendering.Displays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.GameCore
{
    /// <summary>
    /// The game state. This should contain everything that defines the current state of the game.
    /// </summary>
    [Serializable]
    public class Game
    {
        public bool DebugMode { get; set; } = true;
        public Player player;
        public Display currentDisplay;
        public Display NextDisplay;

        public Game()
        {
            player = new Player("Defauto");
        }
    }
}
