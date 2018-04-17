using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    [Serializable]
    public class ArmorMaterial
    {
        public struct Materials
        {
            public static readonly ArmorMaterial cloth = new ArmorMaterial("Cloth", 1);
            public static readonly ArmorMaterial iron = new ArmorMaterial("Iron", 1);
        }

        public string DefaultText { get; }
        public int Multiplier { get; }

        public ArmorMaterial(string defaultText, int multiplier)
        {
            DefaultText = defaultText;
            Multiplier = multiplier;
        }
    }
}
