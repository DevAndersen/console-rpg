using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Effects
{
    public static class EffectsList
    {
        public static readonly Effect restorationBasic = new EffectRestoration("Restoration", EffectType.Positive, 3, 2);

        public static readonly Effect poisonBasic = new EffectPoison("Poison", EffectType.Negative, 3, 2);
    }
}
