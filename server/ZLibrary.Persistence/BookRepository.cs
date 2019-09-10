using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace ZLibrary.Persistence
{
    public class BookRepository : IBookRepository
    {
        private readonly ZLibraryContext context;

        public BookRepository(ZLibraryContext context)
        {
            this.context = context;
        }

        public async Task<IList<Book>> FindAll()
        {
            var books = await context.Books
                .Include(book => book.Publisher)
                .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id)
                .Include(a => a.Author)
                .ToList();

                book.NumberOfLoanedCopies = GetNumberOfLoanedCopies(book);
            }

            return books;
        }

        private int GetNumberOfLoanedCopies(Book book)
        {
            return context.Loans
                    .Where(l => l.Reservation.Reason.Status == ReservationStatus.Approved
                                && l.Reservation.BookId == book.Id)
                    .Count();
        }

        public async Task<Book> FindById(long id)
        {
            var book = await context.Books
                .Include(b => b.Publisher)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (book != null)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id)
                .Include(a => a.Author)
                .ToList();

                book.NumberOfLoanedCopies = GetNumberOfLoanedCopies(book);
            }

            return book;
        }

        public async Task<Book> FindByCoverImageKey(Guid key)
        {
            var book = await context.Books
                .Include(b => b.Publisher)
                .SingleOrDefaultAsync(b => b.CoverImageKey == key);

            if (book != null)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id)
                .Include(a => a.Author)
                .ToList();

                book.NumberOfLoanedCopies = GetNumberOfLoanedCopies(book);
            }

            return book;
        }

        public async Task Delete(long id)
        {
            var book = context.Books.SingleOrDefault(i => i.Id == id);
            if (book == null)
            {
                return;
            }
            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }

        public async Task<Book> Save(Book book)
        {
            if (book.Publisher != null)
            {
                book.Publisher = context.Publishers.Where(p => p.Id == book.Publisher.Id).FirstOrDefault();
            }

            if (book.Id == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                var bookAuthors = book.Authors;
                book.Authors = new List<BookAuthor>();
                context.Books.Update(book);
                await context.SaveChangesAsync();
                book.Authors = bookAuthors;
                context.Books.Update(book);
            }
            await context.SaveChangesAsync();
            await context.Entry(book).ReloadAsync();
            return book;
        }

        public async Task<IList<Book>> FindByTitleOrSynopsis(string text)
        {
            var books = await context.Books
                .Include(book => book.Publisher)
                .Where(b => b.Title.Contains(text) || b.Synopsis.Contains(text))
                .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).Include(a => a.Author)
                                                                                    .ToList();

                book.NumberOfLoanedCopies = GetNumberOfLoanedCopies(book);
            }

            return books;
        }

        public async Task<IList<Book>> FindByIsbn(string isbn)
        {
            var books = await context.Books
                .Include(book => book.Publisher)
                .Where(b => b.IsbnCode.Contains(isbn))
                .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).Include(a => a.Author)
                                                                                    .ToList();

                book.NumberOfLoanedCopies = GetNumberOfLoanedCopies(book);
            }

            return books;
        }

        public async Task<bool> HasBookWithIsbn(Isbn isbn)
        {
            return await context.Books
                         .AnyAsync(b => b.IsbnCode == isbn.ToString());
        }

        public async Task<IList<Book>> FindByAuthor(string author)
        {
            var books = await context.Books
               .Include(book => book.Publisher)
               .Where(b => b.Authors.Any(a => a.Author.Name.Contains(author)))
               .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).Include(a => a.Author).ToList();
                book.NumberOfLoanedCopies = GetNumberOfLoanedCopies(book);
            }

            return books;
        }

        public async Task<IList<Book>> FindByPublisher(string publisher)
        {
            var books = await context.Books
                .Include(book => book.Publisher)
                .Where(b => b.Publisher.Name.Contains(publisher))
                .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).Include(a => a.Author).ToList();
                book.NumberOfLoanedCopies = GetNumberOfLoanedCopies(book);
            }

            return books;
        }
    }
}