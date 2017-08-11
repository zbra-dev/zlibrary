using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestAuthor
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewAuthorWithNullConstructorShouldThrowException()
        {
            var author = new Author(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewAuthorWithEmptyConstructorShouldThrowException()
        {
            var author = new Author("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewAuthorWithWhiteSpaceConstructorShouldThrowException()
        {
            var author = new Author("   ");
        }

        [TestMethod]
        public void TestGetAuthorProperties()
        {
            var author = new Author("Test");
            author.Id = 3;

            Assert.IsNotNull(author);
            Assert.AreEqual("Test", author.Name);
            Assert.AreEqual(3, author.Id);
        }
    }
}