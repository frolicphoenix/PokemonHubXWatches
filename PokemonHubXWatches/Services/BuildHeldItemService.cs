using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class BuildHeldItemService : IBuildHeldItemService
    {
        private readonly ApplicationDbContext _context;

        public BuildHeldItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BuildHeldItem>> GetAllBuildHeldItemsAsync()
        {
            return await _context.BuildHeldItems
                .Include(bhi => bhi.Build)
                .Include(bhi => bhi.HeldItem)
                .ToListAsync();
        }

        public async Task<BuildHeldItem> GetBuildHeldItemByIdAsync(int id)
        {
            return await _context.BuildHeldItems
                .Include(bhi => bhi.Build)
                .Include(bhi => bhi.HeldItem)
                .FirstOrDefaultAsync(bhi => bhi.Id == id);
        }

        public async Task<BuildHeldItem> CreateBuildHeldItemAsync(BuildHeldItem buildHeldItem)
        {
            // Validate that the build doesn't already have 3 items
            var existingItemsCount = await _context.BuildHeldItems
                .CountAsync(bhi => bhi.BuildId == buildHeldItem.BuildId);

            if (existingItemsCount >= 3)
                throw new InvalidOperationException("A build cannot have more than 3 held items.");

            // Validate that the item isn't already added to the build
            var isDuplicate = await _context.BuildHeldItems
                .AnyAsync(bhi => bhi.BuildId == buildHeldItem.BuildId &&
                                bhi.HeldItemId == buildHeldItem.HeldItemId);

            if (isDuplicate)
                throw new InvalidOperationException("This held item is already added to the build.");

            _context.BuildHeldItems.Add(buildHeldItem);
            await _context.SaveChangesAsync();
            return buildHeldItem;
        }

        public async Task<bool> DeleteBuildHeldItemAsync(int id)
        {
            var buildHeldItem = await GetBuildHeldItemByIdAsync(id);
            if (buildHeldItem == null) return false;

            _context.BuildHeldItems.Remove(buildHeldItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BuildHeldItem>> GetBuildHeldItemsByBuildIdAsync(int buildId)
        {
            return await _context.BuildHeldItems
                .Include(bhi => bhi.HeldItem)
                .Where(bhi => bhi.BuildId == buildId)
                .ToListAsync();
        }

        public async Task<bool> ValidateBuildHeldItemExistsAsync(int id)
        {
            return await _context.BuildHeldItems.AnyAsync(bhi => bhi.Id == id);
        }
    }
}