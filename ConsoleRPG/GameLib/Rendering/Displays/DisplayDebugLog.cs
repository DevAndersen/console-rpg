using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public class DisplayDebugLog : Display
    {
        public DisplayDebugLog(Display previousDisplay) : base(previousDisplay)
        {

        }

        public override Display Run()
        {
            ConsoleKey read = ReadKey();
            
            if (read == ConsoleKey.X)
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

                ConsoleColor color = ConsoleColor.Gray;

                if(level == LoggingLevel.Debug)
                {
                    color = ConsoleColor.Green;
                }
                else if (level == LoggingLevel.Critical)
                {
                    color = ConsoleColor.Red;
                }
                else if (level == LoggingLevel.Warning)
                {
                    color = ConsoleColor.Yellow;
                }
                else if (level >= LoggingLevel.Warning)
                {
                    color = ConsoleColor.DarkRed;
                }

                Write($"[{timestamp.ToString("hh:mm:ss")}] <{level}>\t{message}", 1, 3 + i, color);
            }

            if (logEntries.Length == 0)
            {
                Write("Log empty.", 1, 3, ConsoleColor.DarkGray);
            }
        }
    }
}
