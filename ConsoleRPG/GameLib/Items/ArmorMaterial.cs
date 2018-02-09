using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    public class ArmorMaterial
    {
        public struct Materials
        {
            public static readonly ArmorMaterial cloth = new ArmorMaterial("Cloth", 1);
            public static readonly ArmorMaterial iron = new ArmorMaterial("Iron", 1);
        }

        public string DefaultText { get; set; }
        public int ArmorMultiplier { get; }
        public int DamageMultiplier { get; }

        public ArmorMaterial(string defaultText, int armorMultiplier, int damageMultiplier = 1)
        {
            DefaultText = defaultText;
            ArmorMultiplier = armorMultiplier;
            DamageMultiplier = damageMultiplier;
        }
    }
}
