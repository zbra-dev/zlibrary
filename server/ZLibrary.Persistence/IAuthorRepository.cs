using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public interface IAuthorRepository
    {
        Task<IList<Author>> FindAll();
        Author FindById(long id);
    }
}