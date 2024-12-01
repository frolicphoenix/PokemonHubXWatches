using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;

namespace PokemonHubXWatches.Services
{
    public class BuildService : IBuildService
    {
        private readonly ApplicationDbContext _context;

        public BuildService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Build>> GetAllAsync()
        {
            return await _context.Builds.Include(b => b.Pokemon).Include(b => b.HeldItems).ToListAsync();
        }

        public async Task<Build?> GetByIdAsync(int id)
        {
            return await _context.Builds
                .Include(b => b.Pokemon)
                .Include(b => b.HeldItems)
                .FirstOrDefaultAsync(b => b.BuildId == id);
        }

        public async Task AddAsync(Build build)
        {
            _context.Builds.Add(build);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Build build)
        {
            _context.Builds.Update(build);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var build = await _context.Builds.FindAsync(id);
            if (build != null)
            {
                _context.Builds.Remove(build);
                await _context.SaveChangesAsync();
            }
        }
    }
}
