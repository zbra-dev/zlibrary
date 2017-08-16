using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZLibrary.Model;

namespace ZLibrary.Test.Model
{
    [TestClass]
    public class TestLoan
    {
        [TestMethod]
        public void TestCreateLoan()
        {
            var book = new Book();
            var bookCopy = new BookCopy(book);
            var user = new User();
            var reservation = new Reservation(bookCopy, user);
            var loan = new Loan(reservation);

            Assert.IsNotNull(loan);
            Assert.AreEqual(reservation, loan.Reservation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateLoanWithNullParameterShouldThrowException()
        {
            var loan = new Loan(null);
        }

        [TestMethod]
        public void TestLoanProperties()
        {
            var book = new Book();
            var bookCopy = new BookCopy(book);
            var user = new User();
            var reservation = new Reservation(bookCopy, user);
            var loan = new Loan(reservation);
            var expirationDate = DateTime.Now.Add(new TimeSpan(90, 0, 0, 0));
            Assert.IsNotNull(loan);
            Assert.AreEqual(reservation, loan.Reservation);
            Assert.AreEqual(LoanStatus.Borrowed, loan.Status);
            Assert.IsNotNull(loan.ExpirationDate);
            Assert.AreEqual(expirationDate.Date, loan.ExpirationDate.Value);
        }

        [TestMethod]
        public void TestLoanChangeStatusPropertie()
        {
            var book = new Book();
            var bookCopy = new BookCopy(book);
            var user = new User();
            var reservation = new Reservation(bookCopy, user);
            var loan = new Loan(reservation);
            var expirationDate = DateTime.Now.Add(new TimeSpan(90, 0, 0, 0));
            loan.Status = LoanStatus.Expired;
            Assert.IsNotNull(loan);
            Assert.AreEqual(reservation, loan.Reservation);
            Assert.AreEqual(LoanStatus.Expired, loan.Status);
            Assert.IsNotNull(loan.ExpirationDate);
            Assert.AreEqual(expirationDate.Date, loan.ExpirationDate.Value);
        }
    }
}