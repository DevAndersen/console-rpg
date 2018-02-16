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

        public static void Serialize()
        {
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(AppDataFolder + "MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, 1);
            }
        }

        public static void Deserialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(AppDataFolder + "MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int obj = (int)formatter.Deserialize(stream);
            }
        }
    }
}
