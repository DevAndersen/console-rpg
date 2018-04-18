using GameLib.Items;
using GameLib.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    public class DisplayLootInventory : DisplayItemList<ItemStack>
    {
        private ItemStack[] loot;
        private Inventory looterInventory;

        public DisplayLootInventory(Display previousDisplay, Monster monster, ItemStack[] loot, Inventory looterInventory) : base(previousDisplay, loot, $"{monster.Name}'s Loot", true, DisplayItemListMode.ItemMode)
        {
            this.loot = loot;
            this.looterInventory = looterInventory;
        }

        protected override ItemStringData[] ProvideTextForItem(ItemStack item, int itemIndex)
        {
            return DisplayInventory.InventoryTextForItem(item, itemIndex);
        }

        protected override void RenderItemList()
        {
            prefabs.RenderMenuExit();
            prefabs.RenderMenuBar(new MenuBarItem[]
            {
                new MenuBarItem(ConsoleKey.Enter, "Take loot", ConsoleColor.Green),
                new MenuBarItem(ConsoleKey.E, "Take all loot", ConsoleColor.Green)
            });
            if (loot.Length == 0)
            {
                Write("Empty", null, null, ConsoleColor.DarkGray);
            }
        }

        protected override void RenderItemStringDecoration(ItemStack item, int index, bool selected, int y)
        {

        }

        protected override Display RunItemList(ConsoleKey read)
        {
            if (read == ConsoleKey.Escape)
            {
                return previousDisplay;
            }
            if (read == ConsoleKey.Enter)
            {
                if (loot[selectedIndex] != null)
                {
                    LootSlot(selectedIndex);
                }
            }
            if (read == ConsoleKey.E)
            {
                bool issuesLootingSlots = false;
                foreach (ItemStack itemstack in loot)
                {
                    if (itemstack != null)
                    {
                        bool lootedSlotSuccesfully = LootSlot(selectedIndex);
                        if (!lootedSlotSuccesfully)
                        {
                            issuesLootingSlots = true;
                        }
                    }
                }
                if (issuesLootingSlots)
                {
                    return this;
                }
                return previousDisplay;
            }
            return this;
        }

        private bool LootSlot(int slot)
        {
            bool couldLootItemstack = looterInventory.AddItemStack(loot[selectedIndex]);
            if (couldLootItemstack)
            {
                loot[selectedIndex] = null;
            }
            return couldLootItemstack;
        }
    }
}
