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
        private readonly ZLibraryContext context;

        public UserService(ZLibraryContext context)
        {
            this.context = context;
        }

        public async Task<IList<User>> FindAll()
        {
            return await context.Users.ToAsyncEnumerable().ToArray();
        }

        public async Task<User> FindById(long id) {
            return await context.Users.FindAsync(id);
        }

        public async Task<long> Create(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            await context.Entry(user).ReloadAsync();
            
            return user.Id;
        }
    }
}
