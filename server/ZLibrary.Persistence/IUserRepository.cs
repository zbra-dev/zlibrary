using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public interface IUserRepository
    {
        Task<IList<User>> FindAll();
        Task<User> FindById(long id);
        Task<long> Create(User user);

    }

}