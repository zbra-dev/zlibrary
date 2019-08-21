using System;
using System.Linq;
using System.Collections.Generic;

namespace ZLibrary.Model
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Publisher Publisher { get; set; }
        public List<BookAuthor> Authors { get; set; }
        public Isbn Isbn { get; set; }
        public string Synopsis { get; set; }
        public int PublicationYear { get; set; }
        public int NumberOfCopies { get; set; }
        public Guid CoverImageKey { get; set; }
        public DateTime Created { get; set; }

        public Book()
        {
            Created = DateTime.Now;
            Authors = new List<BookAuthor>();
        }

        public int AddCopy()
        {
            return ++NumberOfCopies;
        }

        public int RemoveCopy()
        {
            return --NumberOfCopies;
        }

        public bool CanApproveLoan(IList<Loan> loans)
        {
            return NumberOfCopies > loans.Count(l => !l.IsReturned);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Book;
            if (other == null) return false;
            if (!object.Equals(Isbn, other.Isbn)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            var hash = 3;
            hash += 17 * Isbn.GetHashCode();
            return hash;
        }
    }
}