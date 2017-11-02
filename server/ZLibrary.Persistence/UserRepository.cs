using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ZLibraryContext context;

        public UserRepository(ZLibraryContext context)
        {
            this.context = context;
        }

        public async Task<IList<User>> FindAll()
        {
            return await context.Users.ToListAsync();
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

        public async Task<User> FindByEmail(string email) 
        {
           return await context.Users.SingleOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task Update(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            await context.Entry(user).ReloadAsync();
        }
    }
}