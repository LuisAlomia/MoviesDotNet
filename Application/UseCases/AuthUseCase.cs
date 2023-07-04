using Domain.DTOs.Request;
using Domain.Entites;
using Domain.Repositories;
using Domain.Services;

namespace Application.UseCases
{
    public class AuthUseCase : IAuthUserServices
    {
        private readonly IUserRepository userRepo;

        public AuthUseCase(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<User> Login(AuthUser creadentials)
        {
            User user = await userRepo.GetUserEmail(creadentials.Email);

            if (user is null || user.Password != creadentials.Password) return null;

            return user;
        }

        public async Task<User> Register(User user)
        {
            User userExist = await userRepo.GetUserEmail(user.Email);

            if (userExist is not null) return null;

            user.Role = "client";

            await userRepo.CreateUser(user);

            return user;
        }
    }
}
