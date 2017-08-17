using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestIsbn
    {
      
        [TestMethod]
        public void TestCreateIsbnFromValue()
        {
            var isbn = Isbn.FromValue("9788574591865");
            Assert.IsNotNull(isbn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewIsbnFromValueWithNullConstructorShouldThrowException()
        {
            var isbn = Isbn.FromValue(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewIsbnFromValueWithEmptyConstructorShouldThrowException()
        {
             var isbn = Isbn.FromValue("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewIsbnFromValueWithWhiteSpaceConstructorShouldThrowException()
        {
           var isbn = Isbn.FromValue("   ");
        }


        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestCreateNewIsbnFromValueWithFormatValueShouldFormatException()
        {
           var isbn = Isbn.FromValue("Teste");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestCreateNewIsbnFromValueWithFormatValueShouldInvalidOperationException()
        {
           var isbn = Isbn.FromValue("9728574591865");
        }

       
        [TestMethod]
        public void TestGetIsbnCheckValueProperties()
        {
            var isbn = Isbn.FromValue("9788574591865");
            isbn.Id = 3;

            Assert.IsNotNull(isbn);
            Assert.AreEqual("9788574591865", isbn.Value);
            Assert.AreEqual(3, isbn.Id);
        }
    }
}