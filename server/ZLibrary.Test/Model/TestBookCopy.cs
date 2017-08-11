using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestBookCopy
    {
        [TestMethod]
        public void TestCreateBook()
        {
            var book = new Book();
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(book, externalReference);

            Assert.IsNotNull(bookCopy);
            Assert.AreEqual(book, bookCopy.Book);
            Assert.AreEqual(externalReference, bookCopy.BookExternalReference);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBookWithExternalReferenceParamenterNullShouldThrowException()
        {
            var book = new Book();
            var bookCopy = new BookCopy(book, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBookWithBookParamenterNullShouldThrowException()
        {
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(null, externalReference);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBookWithParamentersNullShouldThrowException()
        {
            var bookCopy = new BookCopy(null, null);
        }

        [TestMethod]
        public void TestBookCopyProperties()
        {
            var book = new Book();
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(book, externalReference);

            Assert.IsNotNull(bookCopy);
            Assert.AreEqual(book, bookCopy.Book);
            Assert.AreEqual(externalReference, bookCopy.BookExternalReference);
        }
    }
}