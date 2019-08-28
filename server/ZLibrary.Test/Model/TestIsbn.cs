using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;
using ZLibrary.Web.Validators;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestIsbn
    {

        private readonly IsbnValidator isbnValidator = new IsbnValidator();

        [TestMethod]
        public void TestMultipleValidIsbns()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("9788574591865");
            Assert.IsTrue(!validationResult.HasError);
            validationResult = isbnValidator.Validate("0201485672");
            Assert.IsTrue(!validationResult.HasError);
            validationResult = isbnValidator.Validate("0321186125");
            Assert.IsTrue(!validationResult.HasError);
            validationResult = isbnValidator.Validate("1590593898");
            Assert.IsTrue(!validationResult.HasError);
            validationResult = isbnValidator.Validate("9780321127426");
            Assert.IsTrue(!validationResult.HasError);
            validationResult = isbnValidator.Validate("9789087532321");
            Assert.IsTrue(!validationResult.HasError);
            validationResult = isbnValidator.Validate("0321127420");
            Assert.IsTrue(!validationResult.HasError);
        }

        [TestMethod]
        public void TestCreateNewIsbnFromValueWithNullConstructor()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate(null);
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void TestCreateNewIsbnFromValueWithEmptyConstructor()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("");
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        
        public void TestCreateNewIsbnFromValueWithWhiteSpaceConstructor()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("   ");
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void TestCreateNewIsbnFromValueWithFormatValue()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("Teste");
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void TestCreateNewIsbnFromValueWithInvalidLength()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("9728574591865");
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void TestGetIsbnCheckValueProperties()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("9788574591865");
            Assert.IsTrue(!validationResult.HasError);
        }

        [TestMethod]
        public void TestInvalidIsbnTenCreation()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("1234567890");
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void TestIsbnNineCreation()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("123456789");
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void TestIsbnElevenCreation()
        {
            ValidationResult validationResult;
            validationResult = isbnValidator.Validate("12345678999");
            Assert.IsTrue(validationResult.HasError);
        }
    }
}