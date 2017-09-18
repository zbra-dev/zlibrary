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
                .Include(book => book.Isbn)
                .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).ToList();
            }

            return books;
        }

        public async Task<Book> FindById(long id)
        {
            var book = await context.Books
                .Include(b => b.Publisher)
                .Include(b => b.Isbn)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (book != null)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).ToList();
            }


            return book;
        }

        public async Task Delete(long id)
        {
            context.Books.Remove(context.Books.SingleOrDefault(i => i.Id == id));
            await context.SaveChangesAsync();
        }

        public async Task Save(Book book)
        {
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
        }

        public async Task<IList<Book>> FindByTitleOrSynopsis(string text)
        {
            var books = await context.Books
                .Include(book => book.Publisher)
                .Include(book => book.Isbn)
                .Where(b => b.Title.Contains(text) || b.Synopsis.Contains(text))
                .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).ToList();
            }

            return books;
        }

        public async Task<IList<Book>> FindByIsbn(string isbn)
        {
            var books = await context.Books
                .Include(book => book.Publisher)
                .Include(book => book.Isbn)
                .Where(b => b.Isbn.Value.Contains(isbn))
                .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).ToList();
            }

            return books;
        }

        public async Task<IList<Book>> FindByAuthor(string author)
        {
            var books = await context.Books
               .Include(book => book.Publisher)
               .Include(book => book.Isbn)
               .Where(b => b.Authors.Any(a => a.Author.Name.Contains(author)))
               .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).ToList();
            }

            return books;
        }

        public async Task<IList<Book>> FindByPublisher(string publisher)
        {
            var books = await context.Books
                .Include(book => book.Publisher)
                .Include(book => book.Isbn)
                .Where(b => b.Publisher.Name.Contains(publisher))
                .ToListAsync();

            foreach (var book in books)
            {
                book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).ToList();
            }

            return books;
        }
    }
}