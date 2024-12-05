using Microsoft.EntityFrameworkCore;
using PokemonHubXWatches.Data;
using PokemonHubXWatches.Interfaces;
using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Services
{
    public class BuildService : IBuildService
    {
        private readonly ApplicationDbContext _context;

        public BuildService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Build>> GetAllBuildsAsync()
        {
            return await _context.Builds
                .Include(b => b.Pokemon)
                .Include(b => b.HeldItems)
                    .ThenInclude(bhi => bhi.HeldItem)
                .ToListAsync();
        }

        public async Task<Build> GetBuildByIdAsync(int id)
        {
            return await _context.Builds
                .Include(b => b.Pokemon)
                .Include(b => b.HeldItems)
                    .ThenInclude(bhi => bhi.HeldItem)
                .FirstOrDefaultAsync(b => b.BuildId == id);
        }

        public async Task<Build> CreateBuildAsync(Build build)
        {
            _context.Builds.Add(build);
            await _context.SaveChangesAsync();
            return build;
        }

        public async Task<Build> UpdateBuildAsync(int id, Build build)
        {
            var existingBuild = await GetBuildByIdAsync(id);
            if (existingBuild == null) return null;

            existingBuild.PokemonUpdatedHP = build.PokemonUpdatedHP;
            existingBuild.PokemonUpdatedAttack = build.PokemonUpdatedAttack;
            existingBuild.PokemonUpdatedDefense = build.PokemonUpdatedDefense;
            existingBuild.PokemonUpdatedSpAttack = build.PokemonUpdatedSpAttack;
            existingBuild.PokemonUpdatedSpDefense = build.PokemonUpdatedSpDefense;
            existingBuild.PokemonUpdatedCDR = build.PokemonUpdatedCDR;
            existingBuild.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingBuild;
        }

        public async Task<bool> DeleteBuildAsync(int id)
        {
            var build = await GetBuildByIdAsync(id);
            if (build == null) return false;

            _context.Builds.Remove(build);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Build>> GetBuildsByUserIdAsync(int userId)
        {
            return await _context.Builds
                .Include(b => b.Pokemon)
                .Include(b => b.HeldItems)
                    .ThenInclude(bhi => bhi.HeldItem)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Build>> GetBuildsByPokemonIdAsync(int pokemonId)
        {
            return await _context.Builds
                .Include(b => b.HeldItems)
                    .ThenInclude(bhi => bhi.HeldItem)
                .Where(b => b.PokemonId == pokemonId)
                .ToListAsync();
        }

        public async Task<Build> AddHeldItemToBuildAsync(int buildId, int heldItemId)
        {
            var build = await GetBuildByIdAsync(buildId);
            if (build == null) return null;

            if (build.HeldItems.Count >= 3)
                throw new InvalidOperationException("A build cannot have more than 3 held items.");

            if (build.HeldItems.Any(bhi => bhi.HeldItemId == heldItemId))
                throw new InvalidOperationException("This held item is already added to the build.");

            var buildHeldItem = new BuildHeldItem
            {
                BuildId = buildId,
                HeldItemId = heldItemId
            };

            build.HeldItems.Add(buildHeldItem);
            await _context.SaveChangesAsync();
            return build;
        }

        public async Task<bool> RemoveHeldItemFromBuildAsync(int buildId, int heldItemId)
        {
            var buildHeldItem = await _context.BuildHeldItems
                .FirstOrDefaultAsync(bhi => bhi.BuildId == buildId && bhi.HeldItemId == heldItemId);

            if (buildHeldItem == null) return false;

            _context.BuildHeldItems.Remove(buildHeldItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateBuildExistsAsync(int id)
        {
            return await _context.Builds.AnyAsync(b => b.BuildId == id);
        }
    }
}