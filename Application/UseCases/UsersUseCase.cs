using Domain.Entites;
using Domain.Repositories;
using Domain.Services;

namespace Application.UseCases
{
    public class UsersUseCase : IUserServices
    {
        private readonly IUserRepository userRepo;

        public UsersUseCase(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await userRepo.GetAllUser();
        }

        public async Task<User> GetUser(int id)
        {
            User user = await userRepo.GetUser(id);

            if (user is null) return null;

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUserByName(string name)
        {
            IEnumerable<User> user = await userRepo.GetAllUserByName(name);

            return user;
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
