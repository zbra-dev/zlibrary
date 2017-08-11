using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestCoverImage
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateCoverImageWithNullShouldThorwException()
        {
            var coverImage = new CoverImage(null);
        }

        [TestMethod]
        public void TestCoverImageProperties()
        {
            var byteArray = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            var coverImage = new CoverImage(byteArray);
            coverImage.Id = 3;
            coverImage.Image = byteArray;
            Assert.IsNotNull(coverImage);
            Assert.AreEqual(byteArray, coverImage.Image);
            Assert.AreEqual(3, coverImage.Id);
        }
    }
}