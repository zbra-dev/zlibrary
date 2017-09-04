using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IAuthorService
    {
        Task<IList<Author>> FindAll();
        Task<Author> FindById(long id);
        Task Delete(long id);
        Task<long> Create(Author author);
        Task<IList<Author>> FindByName(string keyword);
    }
}