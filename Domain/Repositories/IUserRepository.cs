using Domain.Entites;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUser(int id);
        Task<User> GetUserEmail(string email);
        Task<IEnumerable<User>> GetAllUserByName(string name);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(int id, User user);
        Task<bool> DeleteUser(int id);
    }
}
