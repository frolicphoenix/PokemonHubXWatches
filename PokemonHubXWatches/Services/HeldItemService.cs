using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;
using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;

namespace PokemonHubXWatches.Services
{
    public class HeldItemService : IHeldItemService
    {
        private readonly ApplicationDbContext _context;

        public HeldItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeldItem>> GetAllAsync()
        {
            return await _context.HeldItems.ToListAsync();
        }

        public async Task<HeldItem?> GetByIdAsync(int id)
        {
            return await _context.HeldItems.FirstOrDefaultAsync(h => h.HeldItemId == id);
        }

        public async Task AddAsync(HeldItem heldItem)
        {
            _context.HeldItems.Add(heldItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HeldItem heldItem)
        {
            _context.HeldItems.Update(heldItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var heldItem = await _context.HeldItems.FindAsync(id);
            if (heldItem != null)
            {
                _context.HeldItems.Remove(heldItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
