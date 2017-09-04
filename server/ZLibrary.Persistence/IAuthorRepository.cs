using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public interface IAuthorRepository
    {
        Task<IList<Author>> FindAll();
        Task<Author> FindById(long id);
        Task<IList<Author>> FindByName(string name);
        Task Delete(long id);
        Task<long> Create(Author author);
    }
}