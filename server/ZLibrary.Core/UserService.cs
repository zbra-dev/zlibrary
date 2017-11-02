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
        private readonly IUserRepository UserRepository;

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

        public async Task<User> FindByEmail(string email)
        {
            return await UserRepository.FindByEmail(email);
        }

        public async Task Update(User user)
        {
            await UserRepository.Update(user);
        }
    }
}