using GameLib.GameCore;
using GameLib.Items;
using GameLib.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Rendering.Displays
{
    [Serializable]
    public class DisplayInventory : DisplayItemList<ItemStack>
    {
        private Inventory inventory;
        private InventoryMode inventoryMode;
        private int swapSlot;
        private MobPlayer player;

        public  DisplayInventory(Display previousDisplay, Inventory inventory, MobPlayer player) : base(previousDisplay, inventory.GetItems().ToArray(), $"{inventory.Name}'s Inventory", true, DisplayItemListMode.ItemMode)
        {
            this.inventory = inventory;
            this.player = player;
            inventoryMode = InventoryMode.Default;
        }

        public static ItemStringData[] InventoryTextForItem(ItemStack item, int itemIndex)
        {
            return new ItemStringData[]
            {
                new ItemStringData("Item", 30, item?.Item.Name, $"- Empty (slot {itemIndex + 1}) -"),
                new ItemStringData("Amount", 10, item?.Amount.ToString()),
                new ItemStringData("Price", 11, item == null ? "" : (item.Item.Tradable ? item?.Item.Price.ToString() : "Untradeable"))
            };
        }

        protected override ItemStringData[] ProvideTextForItem(ItemStack item, int itemIndex)
        {
            return InventoryTextForItem(item, itemIndex);
        }

        protected override void RenderItemList()
        {
            if (inventoryMode == InventoryMode.Default)
            {
                prefabs.RenderMenuExit();

                List<MenuBarItem> menuBarItems = new List<MenuBarItem>();
                if (inventory.GetSlot(selectedIndex)?.Item.CanBeUsed == true)
                {
                    menuBarItems.Add(new MenuBarItem(ConsoleKey.E, inventory.GetSlot(selectedIndex).Item.GetUseString(), ConsoleColor.Green));
                }
                menuBarItems.Add(new MenuBarItem(ConsoleKey.S, "Swap slots", ConsoleColor.Yellow));
                menuBarItems.Add(new MenuBarItem(ConsoleKey.D, "Destroy item", ConsoleColor.Red));

                prefabs.RenderMenuBar(menuBarItems.ToArray());
            }
            else if (inventoryMode == InventoryMode.Swap)
            {
                prefabs.RenderMenuBar(new MenuBarItem[]
                {
                    new MenuBarItem($"Swapping {inventory.GetSlot(selectedIndex)?.Item.Name} (slot {selectedIndex + 1})"),
                    new MenuBarItem(ConsoleKey.Enter, "Swap slots", ConsoleColor.Green),
                    new MenuBarItem(ConsoleKey.Escape, "Cancel", ConsoleColor.Yellow),
                });
            }
            else if (inventoryMode == InventoryMode.Destroy)
            {
                prefabs.RenderMenuBar(new MenuBarItem[]
                {
                    new MenuBarItem($"Destroy {inventory.GetSlot(selectedIndex)?.Item.Name} (slot {selectedIndex + 1})?"),
                    new MenuBarItem(ConsoleKey.Enter, "Destroy item", ConsoleColor.Red),
                    new MenuBarItem(ConsoleKey.Escape, "Cancel", ConsoleColor.Yellow),
                });
            }
        }

        protected override void RenderItemStringDecoration(ItemStack item, int index, bool selected, int y)
        {
            if (inventoryMode == InventoryMode.Swap)
            {
                if (index == swapSlot)
                {
                    Write(">", 1, y, ConsoleColor.Green);
                }
            }
        }

        protected override Display RunItemList(ConsoleKey read)
        {
            if (read == ConsoleKey.Escape)
            {
                if (inventoryMode != InventoryMode.Default)
                {
                    SetInventoryMode(InventoryMode.Default);
                    return this;
                }
                return previousDisplay;
            }
            else if (inventoryMode == InventoryMode.Default)
            {
                if (read == ConsoleKey.S)
                {
                    swapSlot = selectedIndex;
                    SetInventoryMode(InventoryMode.Swap);
                    return this;
                }
                else if (read == ConsoleKey.D && inventory.GetSlot(selectedIndex) != null)
                {
                    SetInventoryMode(InventoryMode.Destroy);
                    return this;
                }
                else if (read == ConsoleKey.E && inventory.GetSlot(selectedIndex)?.Item.CanBeUsed == true)
                {
                    bool consume = inventory.GetSlot(selectedIndex).Item.OnUse(player);
                    if (consume)
                    {
                        inventory.ReduceSlot(selectedIndex, 1);
                    }
                }
            }
            else if (inventoryMode == InventoryMode.Swap)
            {
                if (read == ConsoleKey.Enter && swapSlot != selectedIndex && !(inventory.GetSlot(swapSlot) == null && inventory.GetSlot(selectedIndex) == null))
                {
                    inventory.SwapSlots(swapSlot, selectedIndex);
                    SetInventoryMode(InventoryMode.Default);
                }
            }
            else if (inventoryMode == InventoryMode.Destroy)
            {
                if (read == ConsoleKey.Enter)
                {
                    inventory.ClearSlot(selectedIndex);
                    SetInventoryMode(InventoryMode.Default);
                }
            }
            return this;
        }

        private void SetInventoryMode(InventoryMode inventoryMode)
        {
            if (inventoryMode == InventoryMode.Default)
            {
                AllowScrolling = true;
            }
            else if (inventoryMode == InventoryMode.Swap)
            {
                AllowScrolling = true;
            }
            else if (inventoryMode == InventoryMode.Destroy)
            {
                AllowScrolling = false;
            }
            this.inventoryMode = inventoryMode;
        }

        protected override ItemStack[] GetListItems()
        {
            return inventory.GetItems().ToArray();
        }

        protected enum InventoryMode
        {
            Default,
            Swap,
            Destroy
        }
    }
}
