using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZLibrary.Model;
using ZLibrary.Test.Utils;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestBag
    {
        [TestMethod]
        public void TestBagProperties()
        {
            var bag = new Bag();
            var book1 = new Book();
            var book2 = new Book();
            var books = new List<Book>() { book1, book2 };
            bag.Books = books;

            Assert.IsNotNull(bag);
            Assert.IsNotNull(bag.Books);
            Assert.AreEqual(bag.Books, books);

            books.AssertListEquals(bag.Books);
        }
    }
}