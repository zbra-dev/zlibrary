using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestBookExternalReference
    {
        [TestMethod]
        public void TestCreateBookExternalReference()
        {
            var bookExternalReference = new BookExternalReference("Test");
            Assert.IsNotNull(bookExternalReference);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewBookExternalReferenceWithNullConstructorShouldThrowException()
        {
            var bookExternalReference = new BookExternalReference(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewBookExternalReferenceWithEmptyConstructorShouldThrowException()
        {
            var bookExternalReference = new BookExternalReference("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewBookExternalReferenceWithWhiteSpaceConstructorShouldThrowException()
        {
            var bookExternalReference = new BookExternalReference("   ");
        }

        [TestMethod]
        public void TestGetIsbnProperties()
        {
            var bookExternalReference = new BookExternalReference("Test");

            Assert.IsNotNull(bookExternalReference);
            Assert.AreEqual("Test", bookExternalReference.Value);
        }
    }
}