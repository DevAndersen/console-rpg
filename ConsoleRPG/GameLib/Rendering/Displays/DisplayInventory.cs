using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public class DisplayInventory : Display
    {
        Inventory inventory;
        int slot = 0;
        int offset = 0;
        int slotsToRender = (int)Math.Ceiling(((double)Height - 6) / 2);

        public DisplayInventory(Display previousDisplay, Inventory inventory) : base(previousDisplay)
        {
            this.inventory = inventory;
            inventory.AddItemStack(new ItemStack(ItemArmor.Items.daggerWood, 1));
            inventory.AddItemStack(new ItemStack(ItemArmor.Items.itemOne, 4));
        }

        public override Display Run()
        {
            if(inventory == null)
            {
                return previousDisplay;
            }

            ConsoleKey read = ReadKey();
            if(read == ConsoleKey.X)
            {
                return previousDisplay;
            }
            else if (read == ConsoleKey.UpArrow)
            {
                if (slot > 0)
                {
                    if (slot == -offset)
                    {
                        offset++;
                    }
                    slot--;
                }
                return this;
            }
            else if (read == ConsoleKey.DownArrow)
            {
                if (slot < inventory.Size - 1)
                {
                    slot++;
                    if (slot + offset == slotsToRender)
                    {
                        offset--;
                    }
                }
                return this;
            }
            else
            {
                return this;
            }
        }

        protected override void RenderDisplay()
        {
            prefabs.RenderMenuBorder(inventory.Name);
            prefabs.RenderMenuExit();
            for (int slotIndex = 0 - offset; slotIndex < -offset + slotsToRender; slotIndex++)
            {
                string slotString = $"Empty [Slot {(slotIndex + 1)}]";
                ConsoleColor slotColor = ConsoleColor.DarkGray;
                if(inventory.GetSlot(slotIndex) != null)
                {
                    slotString = inventory.GetSlot(slotIndex).ToString();
                    slotColor = ConsoleColor.Gray;
                }
                bool line = slotIndex % 2 == 1;
                int posX = 3;
                int posY = 3 + ((slotIndex + offset) * 2);
                Write(slotString, posX, posY, slotColor);
                DrawResource("menuBorderHorizontalLine", 0, Height - 3);
            }
            Write(">", 1, 3 + ((slot + offset) * 2));
        }
    }
}
