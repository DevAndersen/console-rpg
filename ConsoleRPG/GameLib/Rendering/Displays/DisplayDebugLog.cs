using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplayDebugLog : Display
    {
        public DisplayDebugLog(Display previousDisplay) : base(previousDisplay)
        {

        }

        public override Display Run()
        {
            ConsoleKey read = ReadKey();
            if (read == ConsoleKey.Escape)
            {
                return previousDisplay;
            }
            else
            {
                return this;
            }
        }

        protected override void RenderDisplay()
        {
            prefabs.RenderMenuBorder("Log");
            prefabs.RenderMenuExit();

            (string message, LoggingLevel level, DateTime timestamp)[] logEntries = Logger.GetLog().ToArray();

            for (int i = 0; i < logEntries.Length; i++)
            {
                (string message, LoggingLevel level, DateTime timestamp) = logEntries[i];

                Write($"[{timestamp.ToString("hh:mm:ss")}] <{level}>\t{message}", 1, 3 + i, GetErrorColor(level));
            }

            if (logEntries.Length == 0)
            {
                Write("Log empty.", 1, 3, ConsoleColor.DarkGray);
            }
        }

        private ConsoleColor GetErrorColor(LoggingLevel level)
        {
            switch (level)
            {
                case LoggingLevel.Debug:
                    return ConsoleColor.Green;
                case LoggingLevel.Warning:
                    return ConsoleColor.Yellow;
                case LoggingLevel.Critical:
                    return ConsoleColor.Red;
                case LoggingLevel lvl when lvl >= LoggingLevel.Warning:
                    return ConsoleColor.DarkRed;
                default:
                    return ConsoleColor.Gray;
            }
        }
    }
}
