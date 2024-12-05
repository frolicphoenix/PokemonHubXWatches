using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class HeldItemService : IHeldItemService
    {
        private readonly ApplicationDbContext _context;

        public HeldItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeldItem>> GetAllHeldItemsAsync()
        {
            return await _context.HeldItems
                .Include(h => h.Builds)
                .ToListAsync();
        }

        public async Task<HeldItem> GetHeldItemByIdAsync(int id)
        {
            return await _context.HeldItems
                .Include(h => h.Builds)
                .FirstOrDefaultAsync(h => h.HeldItemId == id);
        }

        public async Task<HeldItem> CreateHeldItemAsync(HeldItem heldItem)
        {
            _context.HeldItems.Add(heldItem);
            await _context.SaveChangesAsync();
            return heldItem;
        }

        public async Task<HeldItem> UpdateHeldItemAsync(int id, HeldItem heldItem)
        {
            var existingItem = await GetHeldItemByIdAsync(id);
            if (existingItem == null) return null;

            existingItem.HeldItemName = heldItem.HeldItemName;
            existingItem.HeldItemHP = heldItem.HeldItemHP;
            existingItem.HeldItemAttack = heldItem.HeldItemAttack;
            existingItem.HeldItemDefense = heldItem.HeldItemDefense;
            existingItem.HeldItemSpAttack = heldItem.HeldItemSpAttack;
            existingItem.HeldItemSpDefense = heldItem.HeldItemSpDefense;
            existingItem.HeldItemCDR = heldItem.HeldItemCDR;
            existingItem.HeldItemImage = heldItem.HeldItemImage;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<bool> DeleteHeldItemAsync(int id)
        {
            var heldItem = await GetHeldItemByIdAsync(id);
            if (heldItem == null) return false;

            _context.HeldItems.Remove(heldItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Build>> GetBuildsForHeldItemAsync(int heldItemId)
        {
            return await _context.BuildHeldItems
                .Where(bhi => bhi.HeldItemId == heldItemId)
                .Select(bhi => bhi.Build)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> ValidateHeldItemExistsAsync(int id)
        {
            return await _context.HeldItems.AnyAsync(h => h.HeldItemId == id);
        }
    }
}