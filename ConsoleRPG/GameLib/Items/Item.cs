using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    public class Item
    {
        public struct Items
        {
            #region Internal items
            public static readonly Item fallback = new Item("Fallback item", true);
            #endregion

            public static readonly Item itemOne = new Item("Item one", true);
            public static readonly Item itemTwo = new Item("Item two", false);

            #region Weapons
            public static readonly ItemWeapon daggerWood = new ItemWeapon(WeaponType.Types.dagger, WeaponMaterial.Materials.wood);
            #endregion

            #region Armor
            public static readonly ItemArmor helmCloth = new ItemArmor(ArmorType.Types.head, ArmorMaterial.Materials.cloth);
            #endregion
        }

        public virtual string Name { get; }
        public bool Stackable { get; set; }

        public Item(string name, bool stackable)
        {
            Name = name;
            Stackable = stackable;
        }
    }
}
