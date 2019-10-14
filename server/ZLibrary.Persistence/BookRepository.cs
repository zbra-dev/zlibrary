using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Impress;
using ZLibrary.Utils;

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

            return EnrichAuthors(books);
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

                var bookIdsSet = new HashSet<long>();
                bookIdsSet.Add(book.Id);

                var loanedCopies = GetLoanedCopies(bookIdsSet);

                book.NumberOfLoanedCopies = loanedCopies.MaybeGet(book.Id).Or(0);
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

                var bookIdsSet = new HashSet<long>();
                bookIdsSet.Add(book.Id);

                var loanedCopies = GetLoanedCopies(bookIdsSet);

                book.NumberOfLoanedCopies = loanedCopies.MaybeGet(book.Id).Or(0);
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

        public async Task<IList<Book>> FindByFilter(BookFilter filter)
        {
            IQueryable<Book> query = context.Books
                .Include(book => book.Publisher)
                .Include(book => book.Authors);

            if (!string.IsNullOrWhiteSpace(filter.FreeSearch))
            {
                var text = filter.FreeSearch;
                query = query.Where(b => b.Title.Contains(text)
                    || b.Synopsis.Contains(text)
                    || b.IsbnCode.Contains(text)
                    || b.Publisher.Name.Contains(text)
                    || b.Authors.Any(a => a.Author.Name.Contains(text))
                );
            }

            if (!filter.AllowNoCopies)
            {
                query = query.Where(b => b.NumberOfCopies > 0);
            }

            query = query.OrderBy(b => b.Title);

            var books = await query.ToListAsync();

            return EnrichAuthors(books);
        }

        private List<Book> EnrichAuthors(List<Book> books)
        {
            var bookIds = books.Select(b => b.Id).ToSet();

            var authorsMapping = context.BookAuthors.Where(ba => bookIds.Contains(ba.BookId))
                    .Include(bookAuthor => bookAuthor.Author)
                    .Select(bookAuthor => bookAuthor.Author)
                    .Distinct()
                    .ToDictionary(author => author.Id);

            var loanedCopies = GetLoanedCopies(bookIds);

            foreach (var book in books)
            {
                foreach (var bookAuthor in book.Authors)
                {
                    bookAuthor.Author = authorsMapping.MaybeGet(bookAuthor.AuthorId).OrNull();
                }

                book.NumberOfLoanedCopies = loanedCopies.MaybeGet(book.Id).Or(0);
            }

            return books;
        }

        internal IDictionary<long, int> GetLoanedCopies(ISet<long> bookIds)
        {
            var loans = context.Loans
                .Where(l => l.Reservation.Reason.Status == ReservationStatus.Approved)
                .Where(l => bookIds.Contains(l.Reservation.BookId))
                .GroupBy(l => l.Reservation.BookId)
                .ToDictionary(it => it.Key, it => it.Count());

            return loans;
        }

        public async Task<bool> HasBookWithIsbn(Isbn isbn)
        {
            return await context.Books
                         .AnyAsync(b => b.IsbnCode == isbn.ToString());
        }
    }
}