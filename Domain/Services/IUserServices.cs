using Domain.Entites;

namespace Domain.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<IEnumerable<User>> GetAllUserByName(string name);
        Task<User> GetUser(int id);
        Task<bool> UpdateUser(int id, User user);
        Task<bool> DeleteUser(int id);
    }
}
