using GameLib.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Items
{
    [Serializable]
    public class Inventory
    {
        private ItemStack[] items;
        public string Name { get; }
        public int Size { get { return items.Length; } }

        public Inventory(string name, int size)
        {
            Name = name;
            if(size <= 0)
            {
                Logger.Log($"Attempted to instantiate inventory of size {size}.", LoggingLevel.Error);
                size = 1;
            }
            items = new ItemStack[size];
        }

        public IReadOnlyCollection<ItemStack> GetItems()
        {
            return Array.AsReadOnly(items);
        }

        public ItemStack GetSlot(int slot)
        {
            return items[slot];
        }

        public bool AddItemStack(ItemStack itemstack)
        {
            if(itemstack.Item.Stackable)
            {
                int amount = itemstack.Amount;
                long countFreeSpaceInSimilarStacks = 0;
                foreach (ItemStack stack in items.Where(x => x != null && x.Item == itemstack.Item))
                {
                    countFreeSpaceInSimilarStacks += int.MaxValue - stack.Amount;
                }

                bool hasSlotsThatCanBeFilled = countFreeSpaceInSimilarStacks > 0 && itemstack.Amount < countFreeSpaceInSimilarStacks;
                bool hasEmptySlots = items.Count(x => x == null) != 0;

                if(!hasSlotsThatCanBeFilled && !hasEmptySlots)
                {
                    return false; //Not enough space for itemstack, return false
                }

                if (hasSlotsThatCanBeFilled)
                {
                    for (int slot = 0; slot < Size; slot++)
                    {
                        if (items[slot].Item == itemstack.Item)
                        {
                            int freeSpace = int.MaxValue - items[slot].Amount;
                            if(freeSpace >= amount)
                            {
                                items[slot].Amount += amount;
                                return true; //Slot has enough space for all items, return true
                            }
                            else if(freeSpace > 0)
                            {
                                items[slot].Amount += freeSpace;
                                amount -= freeSpace;
                            }
                        }
                    }

                    if (amount == 0)
                    {
                        return true; //No items left, return true
                    }
                    Logger.Log($"Attempted to add {itemstack.Amount} {itemstack.Item} to an inventory, leftover items occured despite count saying otherwise.", LoggingLevel.Error);
                    return false;

                }
                //Redundant?
                else
                {
                    for (int slot = 0; slot < Size; slot++)
                    {
                        if (items[slot] == null)
                        {
                            items[slot] = itemstack;
                            return true; //Slot found, return true
                        }
                    }
                    return false; //No slots found, return false
                }
            }
            else
            {
                for (int slot = 0; slot < Size; slot++)
                {
                    if(items[slot] == null)
                    {
                        items[slot] = itemstack;
                        return true; //Slot found, return true
                    }
                }
                return false; //No slots found, return false
            }
        }

        public void EmptySlot(int slot)
        {
            items[slot] = null;
        }

        public bool RemoveItems(Item item, int amount)
        {
            long countMatchingItems = 0;
            foreach (ItemStack itemstack in items.Where(x => x != null && x.Item == item))
            {
                countMatchingItems += itemstack.Amount;
            }

            if (countMatchingItems < amount)
            {
                return false;
            }

            for (int slot = 0; slot < Size; slot++)
            {
                if (items[slot].Item == item)
                {
                    ItemStack stack = items[slot];
                    if(amount < stack.Amount)
                    {
                        stack.Amount -= amount;
                        return true;
                    }
                    else if(amount == stack.Amount)
                    {
                        items[slot] = null;
                        return true;
                    }
                    else
                    {
                        amount -= stack.Amount;
                        items[slot] = null;
                    }
                }
            }
            return true;
        }

        public void RemoveAllOfItem(Item item)
        {
            for (int slot = 0; slot < Size; slot++)
            {
                if (items[slot].Item == item)
                {
                    items[slot] = null;
                }
            }
        }

        public void RemoveAllItems()
        {
            Array.Clear(items, 0, Size);
        }

        public void SwapSlots(int slot1, int slot2)
        {
            ItemStack tmp = items[slot1];
            items[slot1] = items[slot2];
            items[slot2] = tmp;
        }
    }
}
