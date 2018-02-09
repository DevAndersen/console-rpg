using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    public class ArmorType
    {
        public struct Types
        {
            public static readonly ArmorType head = new ArmorType("helmet", 3);
            public static readonly ArmorType torso = new ArmorType("chest", 5);
            public static readonly ArmorType legs = new ArmorType("legs", 4);
            public static readonly ArmorType hands = new ArmorType("gloves", 2);
            public static readonly ArmorType feet = new ArmorType("boots", 1);
        }

        public string DefaultText { get; set; }
        public int BaseArmor { get; }

        public ArmorType(string defaultText, int baseArmor)
        {
            DefaultText = defaultText;
            BaseArmor = baseArmor;
        }
    }
}
