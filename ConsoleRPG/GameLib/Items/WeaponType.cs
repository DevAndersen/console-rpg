using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    [Serializable]
    public class WeaponType
    {
        public struct Types
        {
            public static readonly WeaponType dagger = new WeaponType("dagger", 2, 5);
            public static readonly WeaponType sword = new WeaponType("sword", 4, 3);
        }

        public string DefaultText { get; }
        public int BaseDamage { get; }
        public int BaseAccuracy { get; }

        public WeaponType(string defaultText, int baseDamage, int baseAccuracy)
        {
            DefaultText = defaultText;
            BaseDamage = baseDamage;
            BaseAccuracy = baseAccuracy;
        }
    }
}
