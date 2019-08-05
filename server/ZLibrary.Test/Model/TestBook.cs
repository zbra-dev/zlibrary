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
        //TODO: Test cover image key
        [TestMethod]
        public void TestBookProperties()
        {
            var book = new Book();
            var publisher = new Publisher("Publisher Test");
            var author1 = new Author("Test Author");
            author1.Id = 2;
            var a = new Author("Test")
            {
                Id = 1
            };
            var author2 = new Author("Author Test")
            {
                Id = 2
            };

            var bookAuthors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = book,
                        BookId = book.Id,
                        Author = author1,
                        AuthorId = author1.Id
                    },
                    new BookAuthor()
                    {
                        Book = book,
                        BookId = book.Id,
                        Author = author2,
                        AuthorId = author2.Id
                    },
                };
            var isbn = Isbn.FromValue("9788574591865");
            var synopsis = "Synopsis";
            var numberOfCopies = 2;
            book.Title = "Test";
            book.Id = 3;
            book.Publisher = publisher;
            book.Authors = bookAuthors;
            book.Isbn = isbn;
            book.Synopsis = synopsis;
            book.PublicationYear = 2002;
            book.NumberOfCopies = numberOfCopies;

            Assert.IsNotNull(book);
            Assert.AreEqual("Test", book.Title);
            Assert.AreEqual(3, book.Id);
            Assert.AreEqual(2002, book.PublicationYear);
            Assert.AreEqual(publisher, book.Publisher);
            Assert.AreEqual(isbn, book.Isbn);
            Assert.AreEqual(synopsis, book.Synopsis);
            Assert.AreEqual(numberOfCopies, book.NumberOfCopies);
            bookAuthors.AssertListEquals(book.Authors);
        }
    }
}