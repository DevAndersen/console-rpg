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
            string[] lines = Encoding.UTF8.GetString(bytes).Split(new string[] { "\r\n" }, StringSplitOptions.None);
            bool ignoreFirstByte = GetFileEncoding(bytes) != Encoding.Default;
            char[,] chars = new char[lines.Length, (lines[0].Length - (ignoreFirstByte ? 1 : 0))];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Replace("\r\n", "");
                char[] charArray = line.ToCharArray();
                for (int j = (i == 0 && ignoreFirstByte ? 1 : 0); j < charArray.Length; j++)
                {
                    chars[i, (i == 0 && ignoreFirstByte ? j - 1 : j)] = charArray[j];
                }
            }
            return chars;
        }

        /// <summary>
        /// Returns the Encoding for a byte array. Source: https://stackoverflow.com/q/4520184.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Encoding GetFileEncoding(byte[] bytes)
        {
            byte[] buffer = new byte[5];
            buffer = bytes.Take(5).ToArray();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                return Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                return Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                return Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                return Encoding.UTF7;
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                return Encoding.GetEncoding(1201);
            else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                return Encoding.GetEncoding(1200);
            return Encoding.Default;
        }
    }
}
