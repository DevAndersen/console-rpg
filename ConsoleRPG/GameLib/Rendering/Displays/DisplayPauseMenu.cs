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
    public class DisplayPauseMenu : DisplayListMenu
    {
        private ListOption optionResumeGame = new ListOption("Resume game");
        private ListOption optionLog = new ListOption("[Debug] Log");
        private ListOption optionSaveGame = new ListOption("Save game");
        private ListOption optionLoadGame = new ListOption("Load game");
        private ListOption optionExit = new ListOption("Exit");

        public DisplayPauseMenu(Display previousDisplay) : base(previousDisplay)
        {
            options.Add(optionResumeGame);
            if (Core.instance.game.DebugMode)
            {
                options.Add(optionLog);
            }
            options.Add(optionSaveGame);
            options.Add(optionLoadGame);
            options.Add(optionExit);
        }

        protected override Display OnItemSelect(ListOption id)
        {
            if (id == optionResumeGame)
            {
                return previousDisplay;
            }
            else if (id == optionLog)
            {
                return new DisplayDebugLog(this);
            }
            else if (id == optionSaveGame)
            {
                Core.instance.SaveGame();
                return this;
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

        public override Display HandleAlternativeInput(ConsoleKey read)
        {
            if (read == ConsoleKey.X)
            {
                return previousDisplay;
            }
            else if (read == ConsoleKey.F1)
            {
                Core.instance.game.DebugMode = !Core.instance.game.DebugMode;
                return new DisplayPauseMenu(previousDisplay);
            }
            else
            {
                return this;
            }
        }

        protected override void RenderDisplay()
        {
            prefabs.RenderMenuBorder("Game paused");
            prefabs.RenderMenuExit();

            if (Core.instance.game.DebugMode)
            {
                string debugModeString = "Debug mode";
                Write(debugModeString, 2, 1, ConsoleColor.Red);
                DrawResource("menuBorderVerticalLine", debugModeString.Length + 3, 0);
            }

            for (int i = 0; i < options.Count; i++)
            {
                ListOption option = options[i];
                string text = option.Text;
                if (selectedOptionId == i)
                {
                    text = $">>> {option.Text} <<<";
                }
                int yPos = (Height / 2) - (options.Count) + (i * 2);
                Write(text, null, yPos);
            }
        }
    }
}
