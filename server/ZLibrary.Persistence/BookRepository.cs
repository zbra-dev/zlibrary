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

            book.Authors = context.BookAuthors.Where(ba => ba.BookId == book.Id).ToList();

            return book;
        }

        public async Task Delete(long id)
        {
            context.Books.Remove(context.Books.FirstOrDefault(i => i.Id == id));
            await context.SaveChangesAsync();
        }
        
        public async Task<long> Create(Book book)
        {
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            await context.Entry(book).ReloadAsync();

            return book.Id;
        }

        public async Task<IList<Book>> FindByTitle(string title)
        {
            var books = await context.Books
                .Include(book => book.Publisher)
                .Include(book => book.Isbn)
                .Where(b => b.Title.Contains(title))
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