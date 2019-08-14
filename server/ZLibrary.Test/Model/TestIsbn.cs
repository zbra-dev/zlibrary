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
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithNullConstructorShouldThrowException()
        {
            var isbn = Isbn.FromValue(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithEmptyConstructorShouldThrowException()
        {
             var isbn = Isbn.FromValue("");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithWhiteSpaceConstructorShouldThrowException()
        {
           var isbn = Isbn.FromValue("   ");
        }


        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithFormatValueShouldFormatException()
        {
           var isbn = Isbn.FromValue("Teste");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
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

        [TestMethod]
        public void TestValidIsbnTenCreation()
        {
            var isbn = Isbn.FromValue("0321127420");

            Assert.IsNotNull(isbn);
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestInvalidIsbnTenCreation()
        {
            var isbn = Isbn.FromValue("1234567890");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestIsbnNineCreation()
        {
            var isbn = Isbn.FromValue("123456789");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestIsbnElevenCreation()
        {
            var isbn = Isbn.FromValue("12345678999");
        }
    }
}