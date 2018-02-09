using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    public class WeaponType
    {
        public struct Types
        {
            public static readonly WeaponType dagger = new WeaponType("dagger", 2);
            public static readonly WeaponType sword = new WeaponType("sword", 4);
        }

        public string DefaultText { get; set; }
        public int BaseDamage { get; }

        public WeaponType(string defaultText, int baseDamage)
        {
            DefaultText = defaultText;
            BaseDamage = baseDamage;
        }
    }
}
