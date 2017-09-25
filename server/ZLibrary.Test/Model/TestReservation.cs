using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestReservation
    {
        [TestMethod]
        public void TestCreateReservation()
        {
            var bookId = 3;
            var user = new User();
            var reservation = new Reservation(bookId, user);

            Assert.IsNotNull(reservation);
            Assert.AreEqual(bookId, reservation.BookId);
            Assert.AreEqual(user, reservation.User);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateReservationWithBookIdZeroShouldThrowException()
        {
            var user = new User();
            var reservation = new Reservation(0, user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateReservationWithUserNullShouldThrowException()
        {
            var bookId = 3;
            var reservation = new Reservation(bookId, null);
        }

        [TestMethod]
        public void TestReservationProperties()
        {
            var bookId = 3;
            var user = new User();
            var reservation = new Reservation(bookId, user);

            Assert.IsNotNull(reservation);
            reservation.Reason.Description = "Test";
            Assert.IsNotNull(reservation.Reason);
            Assert.AreEqual(bookId, reservation.BookId);
            Assert.AreEqual(user, reservation.User);
            Assert.AreEqual(ReservationStatus.Requested, reservation.Reason.Status);
            Assert.AreEqual("Test", reservation.Reason.Description);
            Assert.AreEqual(DateTime.Now.Date, reservation.StartDate.Date);
        }

        [TestMethod]
        public void TestReservationChangeStatusApprovedPropertie()
        {
            var bookId = 3;
            var user = new User();
            var reservation = new Reservation(bookId, user);

            Assert.IsNotNull(reservation);
            reservation.Reason.Status = ReservationStatus.Approved;
            reservation.Reason.Description = "Test";
            Assert.AreEqual(bookId, reservation.BookId);
            Assert.AreEqual(user, reservation.User);
            Assert.AreEqual(ReservationStatus.Approved, reservation.Reason.Status);
            Assert.AreEqual("Test", reservation.Reason.Description);
            Assert.IsNotNull(reservation.StartDate);
            Assert.AreEqual(DateTime.Now.Date, reservation.StartDate.Date);
        }


        [TestMethod]
        public void TestReservationChangeStatusWaitingPropertie()
        {
            var bookId = 3;
            var user = new User();
            var reservation = new Reservation(bookId, user);

            Assert.IsNotNull(reservation);
            reservation.Reason.Status = ReservationStatus.Waiting;
            reservation.Reason.Description = "Test";
            Assert.AreEqual(bookId, reservation.BookId);
            Assert.AreEqual(user, reservation.User);
            Assert.AreEqual(ReservationStatus.Waiting, reservation.Reason.Status);
            Assert.AreEqual("Test", reservation.Reason.Description);
            Assert.IsNotNull(reservation.StartDate);
            Assert.AreEqual(DateTime.Now.Date, reservation.StartDate.Date);
        }

        [TestMethod]
        public void TestReservationChangeStatusRejectedPropertie()
        {
            var bookId = 3;
            var user = new User();
            var reservation = new Reservation(bookId, user);

            Assert.IsNotNull(reservation);
            reservation.Reason.Status = ReservationStatus.Rejected;
            reservation.Reason.Description = "Test";
            Assert.AreEqual(bookId, reservation.BookId);
            Assert.AreEqual(user, reservation.User);
            Assert.AreEqual(ReservationStatus.Rejected, reservation.Reason.Status);
            Assert.AreEqual("Test", reservation.Reason.Description);
            Assert.IsNotNull(reservation.StartDate);
            Assert.AreEqual(DateTime.Now.Date, reservation.StartDate.Date);
        }
    }
}