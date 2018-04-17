﻿using GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
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
            inventory.AddItemStack(new ItemStack(ItemsList.daggerWood, 1));
            inventory.AddItemStack(new ItemStack(ItemsList.itemOne, 4));
        }

        public override Display Run()
        {
            if (inventory == null)
            {
                return previousDisplay;
            }

            ConsoleKey read = ReadKey();
            if (read == ConsoleKey.Escape)
            {
                if (inventoryMode != InventoryMode.None)
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
            else if (read == ConsoleKey.S && (inventoryMode == InventoryMode.None || inventoryMode == InventoryMode.Swap))
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
            
            RenderInventory(inventory, 0);

            DrawResource("menuBorderHorizontalLine", 0, Height - 3);

            if (inventoryMode == InventoryMode.None)
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

        private void RenderInventory(Inventory specificInventory, int xOffset)
        {
            for (int slotIndex = 0 - offset; slotIndex < -offset + slotsToRender; slotIndex++)
            {
                string slotString = $"Empty [Slot {(slotIndex + 1)}]";
                ConsoleColor slotColor = ConsoleColor.DarkGray;
                if (specificInventory.GetSlot(slotIndex) != null)
                {
                    slotString = specificInventory.GetSlot(slotIndex).ToString();
                    slotColor = ConsoleColor.Gray;
                }
                int posX = 3 + xOffset;
                int posY = 3 + ((slotIndex + offset) * 2);
                Write(slotString, posX, posY, slotColor);
            }
        }

        private void RenderModeNone()
        {
            prefabs.RenderMenuExit();
            prefabs.RenderMenuBar(new MenuBarItem[]
            {
                new MenuBarItem(ConsoleKey.S, "Swap slots", ConsoleColor.Green),
                new MenuBarItem(ConsoleKey.D, "Destroy item", ConsoleColor.Red),
            });
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

            prefabs.RenderMenuBar(new MenuBarItem[]
            {
                new MenuBarItem(slotString),
                new MenuBarItem(ConsoleKey.Enter, "Swap slots", ConsoleColor.Green),
                new MenuBarItem(ConsoleKey.Escape, "Cancel", ConsoleColor.Red),
            });
        }

        private void RenderModeDestroy()
        {
            string slotString = "Empty";
            if (inventory.GetSlot(destroySlot) != null)
            {
                slotString = inventory.GetSlot(destroySlot).ToString();
            }
            slotString = $"Destroy {slotString}?";

            prefabs.RenderMenuBar(new MenuBarItem[]
            {
                new MenuBarItem(slotString),
                new MenuBarItem(ConsoleKey.Enter, "Yes", ConsoleColor.Green),
                new MenuBarItem(ConsoleKey.Escape, "No", ConsoleColor.Red),
            });
        }

        private enum InventoryMode
        {
            None,
            Swap,
            Destroy
        }
    }
}
