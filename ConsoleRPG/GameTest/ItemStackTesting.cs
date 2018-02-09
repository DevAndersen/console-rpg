using System;
using System.Diagnostics;
using GameLib.GameCore;
using GameLib.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameTest
{
    [TestClass]
    public class ItemStackTesting
    {
        [TestMethod]
        public void ItemStack_Success_Stackable()
        {
            Item testItemStackable = new Item("", true);
            ItemStack itemstackSingle = new ItemStack(testItemStackable, 1);
            ItemStack itemstackMulti = new ItemStack(testItemStackable, 5);

            Assert.AreEqual(true, itemstackSingle.Item == testItemStackable);
            Assert.AreEqual(true, itemstackSingle.Amount == 1);

            Assert.AreEqual(true, itemstackMulti.Item == testItemStackable);
            Assert.AreEqual(true, itemstackMulti.Amount == 5);
        }

        [TestMethod]
        public void ItemStack_Success_NonStackable()
        {
            Item testItemNonStackable = new Item("", true);
            ItemStack itemstackDefault = new ItemStack(testItemNonStackable);
            ItemStack itemstackSpecified = new ItemStack(testItemNonStackable, 1);

            Assert.AreEqual(true, itemstackDefault.Item == testItemNonStackable);
            Assert.AreEqual(true, itemstackDefault.Amount == 1);

            Assert.AreEqual(true, itemstackSpecified.Item == testItemNonStackable);
            Assert.AreEqual(true, itemstackSpecified.Amount == 1);
        }

        [TestMethod]
        public void ItemStack_Fail_Stackable_BadAmounts()
        {
            Item testItemStackable = new Item("", true);
            ItemStack itemstackZero = new ItemStack(testItemStackable, 0);
            ItemStack itemstackNegative = new ItemStack(testItemStackable, -1);

            Assert.AreEqual(true, itemstackZero.Item == Item.Items.fallback);
            Assert.AreEqual(1, itemstackZero.Amount);

            Assert.AreEqual(true, itemstackNegative.Item == Item.Items.fallback);
            Assert.AreEqual(1, itemstackNegative.Amount);
        }

        [TestMethod]
        public void ItemStack_Fail_NonStackable_BadAmounts()
        {
            Item testItemNonStackable = new Item("", true);
            ItemStack itemstackZero = new ItemStack(testItemNonStackable, 0);
            ItemStack itemstackNegative = new ItemStack(testItemNonStackable, -1);

            Assert.AreEqual(true, itemstackZero.Item == Item.Items.fallback);
            Assert.AreEqual(1, itemstackZero.Amount);

            Assert.AreEqual(true, itemstackNegative.Item == Item.Items.fallback);
            Assert.AreEqual(1, itemstackNegative.Amount);
        }
    }
}
