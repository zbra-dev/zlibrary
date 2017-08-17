using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZLibrary.Model;
using ZLibrary.Test.Utils;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestBook
    {
        [TestMethod]
        public void TestBookProperties()
        {
            var book = new Book();
            var publisher = new Publisher("Publisher Test");
            var author1 = new Author("Test Author");
            var author2 = new Author("Author Test");
            var authors = new List<Author> { author1, author2 };
            var isbn = Isbn.FromValue("9788574591865");
            var synopsis = "Synopsis";
            var coverImage = new CoverImage();
            book.Title = "Test";
            book.Id = 3;
            book.Publisher = publisher;
            book.Authors = authors;
            book.Isbn = isbn;
            book.Synopsis = synopsis;
            book.PublicationYear = 2002;
            book.CoverImage = coverImage;

            Assert.IsNotNull(book);
            Assert.AreEqual("Test", book.Title);
            Assert.AreEqual(3, book.Id);
            Assert.AreEqual(2002, book.PublicationYear);
            Assert.AreEqual(publisher, book.Publisher);
            Assert.AreEqual(isbn, book.Isbn);
            Assert.AreEqual(synopsis, book.Synopsis);
            Assert.AreEqual(coverImage, book.CoverImage);
            authors.AssertListEquals(book.Authors);
        }
    }
}