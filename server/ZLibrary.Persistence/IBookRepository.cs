using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public interface IBookRepository
    {
        Task<IList<Book>> FindAll();
        Task<Book> FindById(long id);
        Task Delete (long id);
        Task<long> Create(Book user);
        Task<IList<Book>> FindByTitle(string title);
        Task<IList<Book>> FindByIsbn(string isbn);
        Task<IList<Book>> FindByAuthor(string author);
        Task<IList<Book>> FindByPublisher(string publisher);

    }
}