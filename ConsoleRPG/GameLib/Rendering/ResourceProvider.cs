using System;
using System.Collections.Generic;
using System.Linq;
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
            dictionary["dummyResource"] = new char[,] { { '1', '2', '3' }, { '4', '5', '6' } };
        }
    }
}
