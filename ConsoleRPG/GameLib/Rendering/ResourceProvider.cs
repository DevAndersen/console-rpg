using GameLib.GameCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering
{
    public static class ResourceProvider
    {
        private static Dictionary<string, char[,]> dictionary = new Dictionary<string, char[,]>();

        static ResourceProvider()
        {
            LoadResources();
        }

        public static char[,] GetResource(string key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : null;
        }

        private static void LoadResources()
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            ResourceManager rm = new ResourceManager($"{assemblyName}.Resources", Assembly.GetExecutingAssembly());
            ResourceSet resourceSet = rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                string key = entry.Key.ToString();
                byte[] bytes = (byte[])entry.Value;
                dictionary[key] = TranslateResource(bytes);
            }
        }

        private static char[,] TranslateResource(byte[] bytes)
        {
            string[] lines = Encoding.Default.GetString(bytes).Split('\n');
            char[,] chars = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                char[] charArray = line.ToCharArray();
                for (int j = 0; j < charArray.Length; j++)
                {
                    chars[i, j] = charArray[j];
                }
            }
            return chars;
        }
    }
}
