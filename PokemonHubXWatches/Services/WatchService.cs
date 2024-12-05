using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class WatchService : IWatchService
    {
        private readonly ApplicationDbContext _context;

        public WatchService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Watch>> GetAllWatchesAsync()
        {
            return await _context.Watches
                .Include(w => w.ThemedPokemon)
                .Include(w => w.Reservation)
                .ToListAsync();
        }

        public async Task<Watch?> GetWatchByIdAsync(int id)
        {
            return await _context.Watches
                .Include(w => w.ThemedPokemon)
                .Include(w => w.Reservation)
                .FirstOrDefaultAsync(w => w.WatchID == id);
        }

        public async Task<Watch> CreateWatchAsync(Watch watch)
        {
            if (watch == null) throw new ArgumentNullException(nameof(watch));

            _context.Watches.Add(watch);
            await _context.SaveChangesAsync();
            return watch;
        }

        public async Task<Watch?> UpdateWatchAsync(int id, Watch watch)
        {
            if (watch == null) throw new ArgumentNullException(nameof(watch));

            var existingWatch = await GetWatchByIdAsync(id);
            if (existingWatch == null) return null;

            existingWatch.Name = watch.Name;
            existingWatch.Description = watch.Description;
            existingWatch.Price = watch.Price;
            existingWatch.ImageUrl = watch.ImageUrl;
            existingWatch.IsAvailable = watch.IsAvailable;
            existingWatch.StockQuantity = watch.StockQuantity;
            existingWatch.PokemonID = watch.PokemonID;
            existingWatch.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return existingWatch;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ValidateWatchExistsAsync(id))
                    return null;
                throw;
            }
        }

        public async Task<bool> DeleteWatchAsync(int id)
        {
            var watch = await GetWatchByIdAsync(id);
            if (watch == null) return false;

            _context.Watches.Remove(watch);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Watch>> GetTopWatchesAsync(int count)
        {
            return await _context.Watches
                .Include(w => w.ThemedPokemon)
                .Where(w => w.IsAvailable)
                .OrderByDescending(w => w.Reservation != null)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Watch>> GetAvailableWatchesAsync()
        {
            return await _context.Watches
                .Include(w => w.ThemedPokemon)
                .Where(w => w.IsAvailable && w.StockQuantity > 0 && w.Reservation == null)
                .ToListAsync();
        }

        public async Task<bool> IsWatchAvailableAsync(int watchId)
        {
            var watch = await GetWatchByIdAsync(watchId);
            return watch != null && watch.IsAvailable && watch.StockQuantity > 0 && watch.Reservation == null;
        }

        public async Task<bool> ValidateWatchExistsAsync(int id)
        {
            return await _context.Watches.AnyAsync(w => w.WatchID == id);
        }
    }
}