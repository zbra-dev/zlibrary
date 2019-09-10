using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IBookFacade
    {
        Task<IList<Book>> FindAll();
        Task<Book> FindById(long id);
        Task<Book> FindByCoverImageKey(Guid key);
        Task<IList<Book>> FindBy(BookFilter bookSearchParameter);
        Task Delete(long id);
        Task<Book> Save(Book book, string imagePath);
    }
}