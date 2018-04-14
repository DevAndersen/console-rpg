using GameLib.GameCore;
using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplayMainMenu : DisplayListMenu
    {
        private ListOption optionNewGame = new ListOption("New game");
        private ListOption optionLoadGame = new ListOption("Load game");
        private ListOption optionExit = new ListOption("Exit");

        public DisplayMainMenu(Display previousDisplay) : base(previousDisplay)
        {
            options.Add(optionNewGame);
            options.Add(optionLoadGame);
            options.Add(optionExit);
        }

        protected override Display OnItemSelect(ListOption id)
        {
            if (id == optionNewGame)
            {
                return new DisplayTextInput(this, new DisplayRoom(this), SetPlayerName, "Enter a name", 32);
            }
            else if (id == optionLoadGame)
            {
                Core.instance.LoadGame();
                return Core.instance.game.currentDisplay;
            }
            else if (id == optionExit)
            {
                return null;
            }
            else
            {
                return this;
            }
        }

        private void SetPlayerName(string playerName)
        {
            Core.instance.game.player = new Player(playerName);
        }

        public override Display HandleAlternativeInput(ConsoleKey read)
        {
            return this;
        }

        protected override void RenderDisplay()
        {
            DrawResource("simpleBorder", 0, 0);
            (int resourceX, int resourceY, int resourceWidth, int resourceHeight) = DrawResource("logo", null, 2);

            int textOffset = resourceY + resourceHeight + 5;

            for (int i = 0; i < options.Count; i++)
            {
                ListOption option = options[i];
                string text = option.Text;
                if (selectedOptionId == i)
                {
                    text = $">>> {option.Text} <<<";
                }
                Write(text, null, textOffset + i * 2);
            }
        }
    }
}
