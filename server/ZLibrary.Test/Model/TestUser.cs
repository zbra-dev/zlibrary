using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestUser
    {
        [TestMethod]
        public void TestUserProperties()
        {
            var user = new User();
            user.Id = 3;
            user.Name = "Test";
            user.Email = "test@test.com";
            user.isAdministrator = true;

            Assert.IsNotNull(user);
            Assert.AreEqual(3, user.Id);
            Assert.AreEqual("Test", user.Name);
            Assert.AreEqual("test@test.com", user.Email);
            Assert.IsTrue(user.isAdministrator);
        }

        [TestMethod]
        public void TestUserWithOutSetProperties()
        {
            var user = new User();

            Assert.IsNotNull(user);
            Assert.AreEqual(0, user.Id);
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Email);
            Assert.IsFalse(user.isAdministrator);
        }
    }
}