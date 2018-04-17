using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    [Serializable]
    public class WeaponMaterial
    {
        public struct Materials
        {
            public static readonly WeaponMaterial wood = new WeaponMaterial("Wooden", 1);
            public static readonly WeaponMaterial iron = new WeaponMaterial("Iron", 2);
        }

        public string DefaultText { get; }
        public int Multiplier { get; }

        public WeaponMaterial(string defaultText, int multiplier)
        {
            DefaultText = defaultText;
            Multiplier = multiplier;
        }
    }
}
