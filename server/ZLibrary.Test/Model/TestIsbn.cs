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
            Isbn.FromString("9788574591865");
            Isbn.FromString("0201485672");
            Isbn.FromString("0321186125");
            Isbn.FromString("1590593898");
            Isbn.FromString("9780321127426");
            Isbn.FromString("9789087532321");
            Isbn.FromString("0321127420");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithNullConstructorShouldThrowException()
        {
            Isbn.FromString(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithEmptyConstructorShouldThrowException()
        {
            Isbn.FromString("");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithWhiteSpaceConstructorShouldThrowException()
        {
            Isbn.FromString("   ");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithFormatValueShouldFormatException()
        {
            Isbn.FromString("Teste");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestCreateNewIsbnFromValueWithFormatValueShouldInvalidOperationException()
        {
            Isbn.FromString("9728574591865");
        }

        [TestMethod]
        public void TestGetIsbnCheckValueProperties()
        {
            var isbn = Isbn.FromString("9788574591865");
            isbn.Id = 3;

            Assert.IsNotNull(isbn);
            Assert.AreEqual("9788574591865", isbn.Value);
            Assert.AreEqual(3, isbn.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestInvalidIsbnTenCreation()
        {
            Isbn.FromString("1234567890");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestIsbnNineCreation()
        {
            Isbn.FromString("123456789");
        }

        [TestMethod]
        [ExpectedException(typeof(IsbnException))]
        public void TestIsbnElevenCreation()
        {
            Isbn.FromString("12345678999");
        }
    }
}