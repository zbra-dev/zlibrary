using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestPublisher
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewPublisherWithNullConstructorShouldThrowException()
        {
            var publisher = new Publisher(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewPublisherWithEmptyConstructorShouldThrowException()
        {
            var publisher = new Publisher("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateNewPublisherWithWhiteSpaceConstructorShouldThrowException()
        {
            var publisher = new Publisher("   ");
        }

        [TestMethod]
        public void TestGetPublisherProperties()
        {
            var publisher = new Publisher("Test");
            publisher.Id = 3;

            Assert.IsNotNull(publisher);
            Assert.AreEqual("Test", publisher.Name);
            Assert.AreEqual(3, publisher.Id);
        }
    }
}