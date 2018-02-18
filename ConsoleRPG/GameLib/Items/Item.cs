using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    [Serializable]
    public class Item
    {
        public virtual string Name { get; }
        public bool Stackable { get; set; }

        public Item(string name, bool stackable)
        {
            Name = name;
            Stackable = stackable;
        }
    }
}
