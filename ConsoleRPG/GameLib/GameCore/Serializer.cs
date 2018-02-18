using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.GameCore
{
    public class Serializer
    {
        private static string AppDataFolder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Core.GameName + "/");
            }
        }

        public static void SaveGame(Game game)
        {
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(AppDataFolder + "save.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, game);
            }
        }

        public static Game LoadGame()
        {
            Game game;
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(AppDataFolder + "save.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                game = (Game)formatter.Deserialize(stream);
            }
            return game;
        }
    }
}
