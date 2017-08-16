using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestBookExternalReference
    {
        [TestMethod]
        public void TestCreateNewBookExternalReferenceChekingValueProperty()
        {
            var bookExternalReference = new BookExternalReference();
            Assert.IsNotNull(bookExternalReference);
            Assert.AreEqual(string.Empty, bookExternalReference.Value);
        }

        public void TestSetAndGetBookExternalReferenceProperties()
        {
            var bookExternalReference = new BookExternalReference();

            Assert.IsNotNull(bookExternalReference);
            Assert.AreEqual(string.Empty, bookExternalReference.Value);
            bookExternalReference.Value = "Test";
            Assert.AreEqual("Test", bookExternalReference.Value);
        }

        public void TestChengeBookExternalReferenceValuePropertieToNullShouldReturnEmpityString()
        {
            var bookExternalReference = new BookExternalReference();

            Assert.IsNotNull(bookExternalReference);
            Assert.AreEqual(string.Empty, bookExternalReference.Value);
            bookExternalReference.Value = null;
            Assert.AreEqual(string.Empty, bookExternalReference.Value);
        }

        public void TestChengeBookExternalReferenceValuePropertieToWhitSpacesStringShouldReturnEmpityString()
        {
            var bookExternalReference = new BookExternalReference();

            Assert.IsNotNull(bookExternalReference);
            Assert.AreEqual(string.Empty, bookExternalReference.Value);
            bookExternalReference.Value = "     ";
            Assert.AreEqual(string.Empty, bookExternalReference.Value);
        }
    }
}