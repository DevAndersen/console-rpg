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
            string decodedString = GetEncodingAndOffset(bytes).encoding.GetString(bytes.Skip(GetEncodingAndOffset(bytes).bytes).ToArray());
            string[] lines = decodedString.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            char[,] chars = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Replace("\r\n", "");
                char[] charArray = line.ToCharArray();
                for (int j = 0; j < charArray.Length; j++)
                {
                    chars[i, j] = charArray[j];
                }
            }
            return chars;
        }

        private static (Encoding encoding, byte bytes) GetEncodingAndOffset(byte[] bytes)
        {
            if (bytes.Length >= 3 && AreBytesEqual(bytes.Take(3).ToArray(), new byte[] { 0xEF, 0xBB, 0xBF }))
                return (Encoding.UTF8, 3);
            if (bytes.Length >= 3 && AreBytesEqual(bytes.Take(3).ToArray(), new byte[] { 0xE2, 0x94, 0x8C }))
                return (Encoding.UTF8, 0);
            return (Encoding.Default, 0);
        }

        private static bool AreBytesEqual(byte[] b1, byte[] b2)
        {
            for (int i = 0; i < b2.Length; i++)
            {
                if (b1[i] != b2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
