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
            return await context.Books
            .Include(book => book.Authors)
            .Include(book => book.Publisher)
            .Include(book => book.Isbn)
            // TODO - Load cover image
            .ToListAsync();
        }

        public async Task<Book> FindById(long id)
        {
            return await context.Books
            .Where(b => b.Id.Equals(id))
            .Include(book => book.Authors)
            .Include(book => book.Publisher)
            .Include(book => book.Isbn)
            .FirstAsync();

        }

        public async Task<long> Create(Book book)
        {

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            await context.Entry(book).ReloadAsync();

            return book.Id;
        }

    }

}