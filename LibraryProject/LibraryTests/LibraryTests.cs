using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLib;
using BookLib.Enums;
using BookLib.Models;

namespace LibraryTests
{
    [TestClass]
    public class LibraryTests
    {
        readonly ItemCollection lib = new ItemCollection();
        readonly AbstractItem a = new Book("Harry Potter", "J.K Rolling", new DateTime(1999, 1, 11), 1, 700, BookCategory.Fantasy);
        [TestMethod]
        public void CheckAdd()
        {
            lib.Add(a, 50, 1);
            Assert.IsTrue(lib.Count > 0);
        }
        [TestMethod]
        public void CheckRemove() => Assert.IsTrue(lib.Remove(lib["Harry Potter"]));
        [TestMethod]
        public void CheckBorrow() => Assert.IsTrue(lib.Borrow(lib["Harry Potter"]));
        [TestMethod]
        public void CheckBuy() => Assert.IsTrue(lib.Buy(lib["Harry Potter"]));
        [TestMethod]
        public void CheckAmount()
        {
            int a = lib.AmountOf(lib["Harry Potter"]);
            lib.Borrow(lib["Harry Potter"]);
            int b = lib.AmountOf(lib["Harry Potter"]);
            Assert.IsTrue(a == b);
        }
        [TestMethod]
        public void CheckPrice() => Assert.AreEqual(lib.PriceOf(lib["Harry Potter"]), 48.5);
        [TestMethod]
        public void CheckFilter() => Assert.IsTrue(lib.FilterJournalsByCategory(JournalCategory.Academic).Count > 0);
        [TestMethod]
        public void CheckShowAll() => Assert.IsTrue(lib.ShowAll().Count > 0);
    }
}
