using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplayDebugLog : DisplayItemList<(string message, LoggingLevel level, DateTime timestamp)>
    {
        public DisplayDebugLog(Display previousDisplay, (string message, LoggingLevel level, DateTime timestamp)[] items) : base(previousDisplay, items, "Debug log", true, DisplayItemListMode.ScrollMode)
        {

        }

        protected override ItemStringData[] ProvideTextForItem((string message, LoggingLevel level, DateTime timestamp) item, int itemIndex)
        {
            return new ItemStringData[]
            {
                new ItemStringData("Timestamp", 9, item.timestamp.ToString("HH:mm:ss")),
                new ItemStringData("Level", 8, item.level.ToString()),
                new ItemStringData("Message", 100, item.message),
            };
        }

        protected override void RenderItemList()
        {
            prefabs.RenderMenuExit();
        }

        protected override void RenderItemStringDecoration((string message, LoggingLevel level, DateTime timestamp) item, int index, bool selected, int y)
        {

        }

        protected override Display RunItemList(ConsoleKey read)
        {
            if (read == ConsoleKey.Escape)
            {
                return previousDisplay;
            }
            return this;
        }
    }
}
