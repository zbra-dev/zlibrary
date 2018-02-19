using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IAuthorService
    {
        Task<IList<Author>> FindAll();
        Author FindById(long id);
        Task<IList<Author>> FindByName(string name);
    }
}