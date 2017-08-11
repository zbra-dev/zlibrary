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
            var book = new Book();
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(book, externalReference);
            var user = new User();
            var reservation = new Reservation(bookCopy, user);

            Assert.IsNotNull(reservation);
            Assert.AreEqual(bookCopy, reservation.BookCopy);
            Assert.AreEqual(user, reservation.User);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateReservationWithBookCopyNullShouldThrowException()
        {
            var user = new User();
            var reservation = new Reservation(null, user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateReservationWithUserNullShouldThrowException()
        {
            var book = new Book();
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(book, externalReference);
            var reservation = new Reservation(bookCopy, null);
        }

        [TestMethod]
        public void TestReservationProperties()
        {
            var book = new Book();
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(book, externalReference);
            var user = new User();
            var reservation = new Reservation(bookCopy, user);

            Assert.IsNotNull(reservation);
            reservation.Reason.Description = "Test";
            Assert.IsNotNull(reservation.Reason);
            Assert.AreEqual(bookCopy, reservation.BookCopy);
            Assert.AreEqual(user, reservation.User);
            Assert.AreEqual(ReservationStatus.Requested, reservation.Reason.Status);
            Assert.AreEqual("Test", reservation.Reason.Description);
            Assert.IsNotNull(reservation.StartDate);
            Assert.AreEqual(DateTime.Now.Date, reservation.StartDate.Value);
        }

        [TestMethod]
        public void TestReservationChangeStatusApprovedPropertie()
        {
            var book = new Book();
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(book, externalReference);
            var user = new User();
            var reservation = new Reservation(bookCopy, user);

            Assert.IsNotNull(reservation);
            reservation.Reason.Status = ReservationStatus.Approved;
            reservation.Reason.Description = "Test";
            Assert.AreEqual(bookCopy, reservation.BookCopy);
            Assert.AreEqual(user, reservation.User);
            Assert.AreEqual(ReservationStatus.Approved, reservation.Reason.Status);
            Assert.AreEqual("Test", reservation.Reason.Description);
            Assert.IsNotNull(reservation.StartDate);
            Assert.AreEqual(DateTime.Now.Date, reservation.StartDate.Value);
        }


        [TestMethod]
        public void TestReservationChangeStatusWaitingPropertie()
        {
            var book = new Book();
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(book, externalReference);
            var user = new User();
            var reservation = new Reservation(bookCopy, user);

            Assert.IsNotNull(reservation);
            reservation.Reason.Status = ReservationStatus.Waiting;
            reservation.Reason.Description = "Test";
            Assert.AreEqual(bookCopy, reservation.BookCopy);
            Assert.AreEqual(user, reservation.User);
            Assert.AreEqual(ReservationStatus.Waiting, reservation.Reason.Status);
            Assert.AreEqual("Test", reservation.Reason.Description);
            Assert.IsNotNull(reservation.StartDate);
            Assert.AreEqual(DateTime.Now.Date, reservation.StartDate.Value);
        }

        [TestMethod]
        public void TestReservationChangeStatusRejectedPropertie()
        {
            var book = new Book();
            var externalReference = new BookExternalReference("Test");
            var bookCopy = new BookCopy(book, externalReference);
            var user = new User();
            var reservation = new Reservation(bookCopy, user);

            Assert.IsNotNull(reservation);
            reservation.Reason.Status = ReservationStatus.Rejected;
            reservation.Reason.Description = "Test";
            Assert.AreEqual(bookCopy, reservation.BookCopy);
            Assert.AreEqual(user, reservation.User);
            Assert.AreEqual(ReservationStatus.Rejected, reservation.Reason.Status);
            Assert.AreEqual("Test", reservation.Reason.Description);
            Assert.IsNotNull(reservation.StartDate);
            Assert.AreEqual(DateTime.Now.Date, reservation.StartDate.Value);
        }
    }
}