using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IBookService
    {
        Task<IList<Book>> FindAll();
        Task<Book> FindById(long id);
        Task Delete(long id);
        Task Save(Book book);
        Task<IList<Book>> FindBy(BookSearchParameter bookSearchParameter);
    }
}