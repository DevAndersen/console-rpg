using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.GameCore
{
    public static class Logger
    {
        private static List<(string message, LoggingLevel level, DateTime timestamp)> log = new List<(string message, LoggingLevel level, DateTime timestamp)>();

        public static void Log(string message, LoggingLevel level)
        {
            if ((level != LoggingLevel.Debug) || (level == LoggingLevel.Debug && Core.instance != null && Core.instance.game != null && Core.instance.game.DebugMode))
            {
                log.Add((message, level, DateTime.Now));
            }
        }

        public static IReadOnlyCollection<(string message, LoggingLevel level, DateTime timestamp)> GetLog()
        {
            return log.AsReadOnly();
        }
    }

    public enum LoggingLevel
    {
        Info = 0,
        Warning = 1,
        Error = 2,
        Critical = 3,
        Debug = -1
    }
}
