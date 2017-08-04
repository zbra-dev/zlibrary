using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{

    public interface IBookRepository
    {
        Task<IList<Book>> FindAll();
        Task<Book> FindById(long id);
        Task<long> Create(Book user);

    }

}