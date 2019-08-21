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
        Task<IList<Book>> FindByTitleOrSynopsis(string text);
        Task<IList<Book>> FindByIsbn(string isbn);
        Task<bool> HasBookWithIsbn(string isbn);
        Task<IList<Book>> FindByAuthor(string author);
        Task<IList<Book>> FindByPublisher(string publisher);
        Task Delete (long id);
        Task<Book> Save(Book book);
    }
}