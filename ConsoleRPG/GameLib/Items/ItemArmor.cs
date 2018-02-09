using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    public class ItemArmor : Item
    {
        public ArmorType Type { get; }
        public ArmorMaterial Material { get; }

        public override string Name
        {
            get
            {
                string materialString = Material.DefaultText;
                string typeString = Type.DefaultText;

                return $"{Material.DefaultText} {Type.DefaultText}";
            }
        }
        
        public ItemArmor(ArmorType type, ArmorMaterial material) : base(string.Empty, false)
        {
            Type = type;
            Material = material;
        }
    }
}
