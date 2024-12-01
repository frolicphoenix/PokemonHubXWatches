using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;

namespace PokemonHubXWatches.Services
{
    public class WatchService : IWatchService
    {
        private readonly ApplicationDbContext _context;

        public WatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Watch>> GetAllAsync()
        {
            return await _context.Watches.Include(w => w.ThemedPokemon).ToListAsync();
        }

        public async Task<Watch?> GetByIdAsync(int id)
        {
            return await _context.Watches.Include(w => w.ThemedPokemon).FirstOrDefaultAsync(w => w.WatchID == id);
        }

        public async Task AddAsync(Watch watch)
        {
            _context.Watches.Add(watch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Watch watch)
        {
            _context.Watches.Update(watch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var watch = await _context.Watches.FindAsync(id);
            if (watch != null)
            {
                _context.Watches.Remove(watch);
                await _context.SaveChangesAsync();
            }
        }
    }
}
