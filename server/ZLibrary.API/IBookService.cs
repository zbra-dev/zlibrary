using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IBookService
    {
        Task<IList<Book>> FindAll();
        Task<Book> FindById(long id);
        Task<long> Create(Book book);
    }
    
}