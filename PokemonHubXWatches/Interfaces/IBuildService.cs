using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IBuildService
    {
        Task<IEnumerable<Build>> GetAllBuildsAsync();
        Task<Build> GetBuildByIdAsync(int id);
        Task<Build> CreateBuildAsync(Build build);
        Task<Build> UpdateBuildAsync(int id, Build build);
        Task<bool> DeleteBuildAsync(int id);
        Task<IEnumerable<Build>> GetBuildsByUserIdAsync(int userId);
        Task<IEnumerable<Build>> GetBuildsByPokemonIdAsync(int pokemonId);
        Task<Build> AddHeldItemToBuildAsync(int buildId, int heldItemId);
        Task<bool> RemoveHeldItemFromBuildAsync(int buildId, int heldItemId);
        Task<bool> ValidateBuildExistsAsync(int id);
    }
}