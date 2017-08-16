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
            var externalReference = new BookExternalReference();
            var bookCopy = new BookCopy(book);

            Assert.IsNotNull(bookCopy);
            Assert.AreEqual(book, bookCopy.Book);
            Assert.AreEqual(BookExternalReference.Empty, bookCopy.BookExternalReference);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBookWithBookParamenterNullShouldThrowException()
        {
            var bookCopy = new BookCopy(null);
        }

        [TestMethod]
        public void TestGetAndSetBookCopyProperties()
        {
            var book = new Book();
            var externalReference = new BookExternalReference(){Value = "Teste"};;
            var bookCopy = new BookCopy(book);
            bookCopy.BookExternalReference = externalReference;

            Assert.IsNotNull(bookCopy);
            Assert.AreEqual(book, bookCopy.Book);
            Assert.AreEqual(externalReference, bookCopy.BookExternalReference);
            bookCopy.BookExternalReference = BookExternalReference.Empty;
            Assert.AreEqual(BookExternalReference.Empty, bookCopy.BookExternalReference);
        }
    }
}