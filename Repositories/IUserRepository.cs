using OtomasyonApi.Models;

namespace OtomasyonApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
    }
}
