using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public interface IBookRepository
    {
        Task<IList<Book>> FindAll();
        Task<Book> FindById(long id);
        Task<Book> FindByCoverImageKey(Guid key);
        Task<IList<Book>> FindByFilter(BookFilter filter);
        Task<bool> HasBookWithIsbn(Isbn isbn);
        Task Delete (long id);
        Task<Book> Save(Book book);
    }
}