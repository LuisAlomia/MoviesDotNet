using Domain.Entites;
using Domain.Repositories;
using Infrastructure.Persistences.EntityFramework.ContextDB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.EntityFramework.Implementations
{
    public class UserRepoImpl : IUserRepository
    {
        private readonly ContextDatabase ContextDB;

        public UserRepoImpl(ContextDatabase ContextDB)
        {
            this.ContextDB = ContextDB;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            IEnumerable<User> listUsers = await ContextDB.Users.ToListAsync();

            return listUsers;
        }

        public async Task<IEnumerable<User>> GetAllUserByName(string name)
        {
            IEnumerable<User> listUsers = await ContextDB.Users
                                            .Where(user => user.Name.ToLower().Contains(name.ToLower()))
                                            .ToListAsync();

            return listUsers;
        }

        public async Task<User> GetUser(int id)
        {
            User user = await ContextDB.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user is null) return null;

            return user;
        }

        public async Task<User> GetUserEmail(string email)
        {
            User user = await ContextDB.Users.FirstOrDefaultAsync(user => user.Email == email);

            if (user is null) return null;

            return user;
        }
        public async Task<bool> CreateUser(User user)
        {
            await ContextDB.Users.AddAsync(user);

            await ContextDB.SaveChangesAsync();

            return true;
        }

        public Task<bool> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
