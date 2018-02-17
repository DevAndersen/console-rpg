using System;
using System.Collections.Generic;
using GameLib.GameCore;
using GameLib.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTest
{
    [TestClass]
    public class InventoryTesting
    {
        static Item testItemStackable = new Item("", true);
        static Item testItemStackable2 = new Item("", true);
        static Item testItemNonStackable = new Item("", false);

        static int inventorySize = 5;
        static int itemsPerStack = 1000;

        [TestMethod]
        public void Inventory_Success_AddStackable()
        {
            Inventory inv = new Inventory("", inventorySize);

            for (int i = 0; i < inventorySize; i++)
            {
                bool addedSuccesfully = inv.AddItemStack(new ItemStack(testItemStackable, itemsPerStack));
                Assert.AreEqual(true, addedSuccesfully);
            }

            for (int i = 0; i < inventorySize; i++)
            {
                if(i == 0)
                {
                    Assert.AreEqual(inventorySize * itemsPerStack, inv.GetSlot(i).Amount);
                }
                else
                {
                    Assert.AreEqual(null, inv.GetSlot(i));
                }
            }
        }

        [TestMethod]
        public void Inventory_Success_AddNonStackable()
        {
            Inventory inv = new Inventory("", inventorySize);

            for (int i = 0; i < inventorySize; i++)
            {
                bool addedSuccesfully = inv.AddItemStack(new ItemStack(testItemNonStackable));
                Assert.AreEqual(true, addedSuccesfully);
            }

            for (int i = 0; i < inventorySize; i++)
            {
                Assert.AreNotEqual(null, inv.GetSlot(i));
            }
        }

        [TestMethod]
        public void Inventory_Fail_AddStackable_DifferentItem()
        {
            Inventory inv = new Inventory("", inventorySize);

            bool addedStackableSuccesfully = inv.AddItemStack(new ItemStack(testItemStackable));
            Assert.AreEqual(true, addedStackableSuccesfully);

            for (int i = 1; i < inventorySize; i++)
            {
                bool addedNonStackableSuccesfully = inv.AddItemStack(new ItemStack(testItemNonStackable));
                Assert.AreEqual(true, addedNonStackableSuccesfully);
            }

            bool failedToAdd = inv.AddItemStack(new ItemStack(testItemStackable2));
            Assert.AreEqual(false, failedToAdd);
        }

        [TestMethod]
        public void Inventory_Fail_AddStackable_TooManyItems()
        {
            Inventory inv = new Inventory("", inventorySize);

            for (int i = 0; i < inventorySize; i++)
            {
                bool addedSuccesfully = inv.AddItemStack(new ItemStack(testItemStackable, int.MaxValue));
                Assert.AreEqual(true, addedSuccesfully);
            }

            bool failedToAdd = inv.AddItemStack(new ItemStack(testItemStackable));
            Assert.AreEqual(false, failedToAdd);
        }

        [TestMethod]
        public void Inventory_Fail_AddNonstackable_TooManyItems()
        {
            Inventory inv = new Inventory("", inventorySize);

            for (int i = 0; i < inventorySize; i++)
            {
                bool addedSuccesfully = inv.AddItemStack(new ItemStack(testItemNonStackable));
                Assert.AreEqual(true, addedSuccesfully);
            }

            bool addOneUnstackableItemTooMany = inv.AddItemStack(new ItemStack(testItemNonStackable));
            Assert.AreEqual(false, addOneUnstackableItemTooMany);
        }

        [TestMethod]
        public void Inventory_Success_Swap()
        {
            Inventory inv = new Inventory("", inventorySize);

            Assert.AreEqual(null, inv.GetSlot(0));
            Assert.AreEqual(null, inv.GetSlot(1));
            Assert.AreEqual(null, inv.GetSlot(2));
            Assert.AreEqual(null, inv.GetSlot(3));
            Assert.AreEqual(null, inv.GetSlot(4));

            ItemStack stack = new ItemStack(testItemNonStackable);
            inv.AddItemStack(stack);

            Assert.AreNotEqual(null, inv.GetSlot(0));
            Assert.AreEqual(null, inv.GetSlot(1));
            Assert.AreEqual(null, inv.GetSlot(2));
            Assert.AreEqual(null, inv.GetSlot(3));
            Assert.AreEqual(null, inv.GetSlot(4));

            inv.SwapSlots(0, 3);
            Assert.AreEqual(stack, inv.GetSlot(3));

            Assert.AreEqual(null, inv.GetSlot(0));
            Assert.AreEqual(null, inv.GetSlot(1));
            Assert.AreNotEqual(null, inv.GetSlot(3));
            Assert.AreEqual(null, inv.GetSlot(2));
            Assert.AreEqual(null, inv.GetSlot(4));

        }
    }
}
