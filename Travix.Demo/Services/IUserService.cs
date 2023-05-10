using Travix.DB;

namespace Travix.Demo.Services
{
    public interface IUserService
    {
        Task<User> GetAsync(long id);
        Task<long> CreateAsync(User User);
        Task DeleteAsync(long id);
        Task UpdatAsync(long id, User user);
        Task<List<User>> GetAllAsync();
    }
}
