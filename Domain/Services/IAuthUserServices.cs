using Domain.DTOs.Request;
using Domain.Entites;

namespace Domain.Services
{
    public interface IAuthUserServices
    {
        public Task<User> Register(User user);
        public Task<User> Login(AuthUser user);
    }
}
