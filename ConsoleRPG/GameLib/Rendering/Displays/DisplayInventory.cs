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

        InventoryMode inventoryMode = InventoryMode.None;
        int swapSlot;
        int destroySlot;

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
                if(inventoryMode != InventoryMode.None)
                {
                    inventoryMode = InventoryMode.None;
                    return this;
                }
                else
                {
                    return previousDisplay;
                }
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
            else if (read == ConsoleKey.S && inventoryMode == InventoryMode.None)
            {
                inventoryMode = InventoryMode.Swap;
                swapSlot = slot;
                return this;
            }
            else if (read == ConsoleKey.D && inventoryMode == InventoryMode.None && inventory.GetSlot(slot) != null)
            {
                inventoryMode = InventoryMode.Destroy;
                destroySlot = slot;
                return this;
            }
            else if (read == ConsoleKey.Enter)
            {
                if (inventoryMode == InventoryMode.Swap)
                {
                    inventory.SwapSlots(swapSlot, slot);
                    inventoryMode = InventoryMode.None;
                }
                else if (inventoryMode == InventoryMode.Destroy)
                {
                    inventory.ClearSlot(destroySlot);
                    inventoryMode = InventoryMode.None;
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

            if(inventoryMode == InventoryMode.None)
            {
                RenderModeNone();
            }
            else if (inventoryMode == InventoryMode.Swap)
            {
                RenderModeSwap();
            }
            else if (inventoryMode == InventoryMode.Destroy)
            {
                RenderModeDestroy();
            }

            Write(">", 1, 3 + ((slot + offset) * 2));
        }

        private void RenderModeNone()
        {
            prefabs.RenderMenuExit();

            Write("Swap slots", 2, Height - 2, ConsoleColor.DarkGreen);
            Write("S", 2, Height - 2, ConsoleColor.Green);
            DrawResource("menuBorderVerticalLine", 13, Height - 3);

            Write("Destroy item", 15, Height - 2, ConsoleColor.DarkRed);
            Write("D", 15, Height - 2, ConsoleColor.Red);
            DrawResource("menuBorderVerticalLine", 28, Height - 3);
        }

        private void RenderModeSwap()
        {
            if ((swapSlot + offset) < slotsToRender && (swapSlot + offset) >= 0)
            {
                Write(">", 1, 3 + ((swapSlot + offset) * 2), ConsoleColor.DarkGreen);
            }

            string slotString = "Empty";
            if (inventory.GetSlot(swapSlot) != null)
            {
                slotString = inventory.GetSlot(swapSlot).ToString();
            }
            slotString = $"Swapping {slotString} [Slot {swapSlot + 1}]";
            Write(slotString, 2, Height - 2, ConsoleColor.DarkGray);
            int slotStringOffset = slotString.Length + 3;

            DrawResource("menuBorderVerticalLine", slotStringOffset, Height - 3);

            Write("[Enter] Swap", slotStringOffset + 2, Height - 2, ConsoleColor.DarkGreen);
            Write("Enter", slotStringOffset + 3, Height - 2, ConsoleColor.Green);

            DrawResource("menuBorderVerticalLine", slotStringOffset + 15, Height - 3);

            Write("[X] Cancel", slotStringOffset + 17, Height - 2, ConsoleColor.DarkRed);
            Write("X", slotStringOffset + 18, Height - 2, ConsoleColor.Red);

            DrawResource("menuBorderVerticalLine", slotStringOffset + 28, Height - 3);
        }

        private void RenderModeDestroy()
        {
            string slotString = "Empty";
            if (inventory.GetSlot(destroySlot) != null)
            {
                slotString = inventory.GetSlot(destroySlot).ToString();
            }
            slotString = $"Destroy {slotString}?";
            Write(slotString, 2, Height - 2, ConsoleColor.DarkGray);
            int slotStringOffset = slotString.Length + 3;

            DrawResource("menuBorderVerticalLine", slotStringOffset, Height - 3);

            Write("[Enter] Yes", slotStringOffset + 2, Height - 2, ConsoleColor.DarkRed);
            Write("Enter", slotStringOffset + 3, Height - 2, ConsoleColor.Red);

            DrawResource("menuBorderVerticalLine", slotStringOffset + 14, Height - 3);

            Write("[X] No", slotStringOffset + 16, Height - 2, ConsoleColor.DarkGreen);
            Write("X", slotStringOffset + 17, Height - 2, ConsoleColor.Green);

            DrawResource("menuBorderVerticalLine", slotStringOffset + 23, Height - 3);
        }

        private enum InventoryMode
        {
            None,
            Swap,
            Destroy
        }
    }
}
