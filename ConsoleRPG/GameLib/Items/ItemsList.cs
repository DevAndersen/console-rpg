using GameLib.Effects;
using GameLib.Items.Consumables;
using GameLib.Items.Consumables.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    public static class ItemsList
    {
        #region Internal items
        public static readonly Item fallback = new Item("Fallback item", true);
        #endregion

        public static readonly Item itemOne = new Item("Item one", true);
        public static readonly Item itemTwo = new Item("Item two", false);

        #region Equipment
        
        #region Weapons
        public static readonly ItemWeapon daggerWood = new ItemWeapon(WeaponType.Types.dagger, WeaponMaterial.Materials.wood);
        #endregion

        #region Armor
        public static readonly ItemArmor helmCloth = new ItemArmor(ArmorType.Types.head, ArmorMaterial.Materials.cloth);
        #endregion

        #endregion

        #region Consumables

        #region Food
        public static readonly ItemEdible bread = new ItemEdible("Bread", true, 2, EdibleType.Food, 5);
        #endregion

        #region Potions

        #endregion
        public static readonly ItemPotion potionRestorationBasic = new ItemPotion("Basic restoration potion", true, EffectsList.restorationBasic, 20);
        #endregion
    }
}
