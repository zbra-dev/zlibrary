using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestReservationReason
    {
        [TestMethod]
        public void TestCreateReservationReasonProperties()
        {
            var reason = new ReservationReason();
            reason.Description = "Test";
            Assert.IsNotNull(reason);
            Assert.AreEqual(ReservationStatus.Requested, reason.Status);
            Assert.AreEqual("Test", reason.Description);
        }

        [TestMethod]
        public void TestCreateReservationReasonPropertiesStatusWaitingAndDescriptionNull()
        {
            var reason = new ReservationReason();
            reason.Status = ReservationStatus.Waiting;
            Assert.IsNotNull(reason);
            Assert.AreEqual(ReservationStatus.Waiting, reason.Status);
            Assert.IsNull(reason.Description);
        }

        [TestMethod]
        public void TestCreateReservationReasonPropertiesStatusRejectedAndDescriptionNull()
        {
            var reason = new ReservationReason();
            reason.Status = ReservationStatus.Rejected;
            Assert.IsNotNull(reason);
            Assert.AreEqual(ReservationStatus.Rejected, reason.Status);
            Assert.IsNull(reason.Description);
        }

        [TestMethod]
        public void TestCreateReservationReasonPropertiesStatusApprovedAndDescriptionNull()
        {
            var reason = new ReservationReason();
            reason.Status = ReservationStatus.Approved;
            Assert.IsNotNull(reason);
            Assert.AreEqual(ReservationStatus.Approved, reason.Status);
            Assert.IsNull(reason.Description);
        }
    }
}