using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ValidateUserExistsAsync(int id);
        Task<bool> IsUserAdminAsync(int userId);
    }
}