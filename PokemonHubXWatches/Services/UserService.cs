using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Reservations)
                .Include(u => u.Builds)
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Reservations)
                .Include(u => u.Builds)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            // Validate unique email
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new InvalidOperationException("Email already exists.");

            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            var existingUser = await GetUserByIdAsync(id);
            if (existingUser == null) return null;

            // Validate unique email if changed
            if (user.Email != existingUser.Email &&
                await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new InvalidOperationException("Email already exists.");

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            existingUser.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Reservations)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ValidateUserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.UserId == id);
        }

        public async Task<bool> IsUserAdminAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            return user?.Role?.ToUpper() == "ADMIN";
        }
    }
} 