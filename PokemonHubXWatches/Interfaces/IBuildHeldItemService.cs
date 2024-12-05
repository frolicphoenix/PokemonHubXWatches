using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IBuildHeldItemService
    {
        Task<IEnumerable<BuildHeldItem>> GetAllBuildHeldItemsAsync();
        Task<BuildHeldItem> GetBuildHeldItemByIdAsync(int id);
        Task<BuildHeldItem> CreateBuildHeldItemAsync(BuildHeldItem buildHeldItem);
        Task<bool> DeleteBuildHeldItemAsync(int id);
        Task<IEnumerable<BuildHeldItem>> GetBuildHeldItemsByBuildIdAsync(int buildId);
        Task<bool> ValidateBuildHeldItemExistsAsync(int id);
    }
}