using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Persistence;

namespace ZLibrary.Core
{
    public class UserService : IUserService
    {
        private IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public async Task<IList<User>> FindAll()
        {
            return await UserRepository.FindAll();
        }

        public async Task<User> FindById(long id)
        {
            return await UserRepository.FindById(id);
        }

        public async Task<long> Create(User user)
        {
            return await UserRepository.Create(user);
        }

    }
}