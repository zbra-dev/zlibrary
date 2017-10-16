using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IUserService
    {
        Task<IList<User>> FindAll();
        Task<User> FindById(long id);
        Task<long> Create(User user);
        Task<User> FindByEmail(string email);
        Task Update(User user);
    }
}
