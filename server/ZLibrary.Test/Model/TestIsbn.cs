using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestIsbn
    {
        [TestMethod]
        public void TestCreateIsbn()
        {
            var isbn = new Isbn("Test");
            Assert.IsNotNull(isbn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewIsbnWithNullConstructorShouldThrowException()
        {
            var isbn = new Isbn(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewIsbnWithEmptyConstructorShouldThrowException()
        {
            var isbn = new Isbn("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewIsbnWithWhiteSpaceConstructorShouldThrowException()
        {
            var isbn = new Isbn("   ");
        }

        [TestMethod]
        public void TestGetIsbnProperties()
        {
            var isbn = new Isbn("Test");
            isbn.Id = 3;

            Assert.IsNotNull(isbn);
            Assert.AreEqual("Test", isbn.Value);
            Assert.AreEqual(3, isbn.Id);
        }
    }
}