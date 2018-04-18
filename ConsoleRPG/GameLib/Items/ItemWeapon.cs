using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    [Serializable]
    public class ItemWeapon : Item
    {
        public WeaponType Type { get; }
        public WeaponMaterial Material { get; }

        public override string Name
        {
            get
            {
                string materialString = Material.DefaultText;
                string typeString = Type.DefaultText;

                return $"{Material.DefaultText} {Type.DefaultText}";
            }
        }

        public ItemWeapon(WeaponType type, WeaponMaterial material, int price = -1) : base(string.Empty, false, price)
        {
            Type = type;
            Material = material;
        }
    }
}
