using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestDate
    {
        [TestMethod]
        public void TestCreateDate()
        {
            var date = new Date(DateTime.Now);

            Assert.IsNotNull(date);
        }

        [TestMethod]
        public void TestCreateDateProperties()
        {
            var dateNow = DateTime.Now;
            var date = new Date(dateNow);

            Assert.IsNotNull(date);
            Assert.AreEqual(dateNow.Date, date.Value);
        }
    }
}