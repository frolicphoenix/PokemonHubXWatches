using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IHeldItemService
    {
        Task<IEnumerable<HeldItem>> GetAllHeldItemsAsync();
        Task<HeldItem> GetHeldItemByIdAsync(int id);
        Task<HeldItem> CreateHeldItemAsync(HeldItem heldItem);
        Task<HeldItem> UpdateHeldItemAsync(int id, HeldItem heldItem);
        Task<bool> DeleteHeldItemAsync(int id);
        Task<IEnumerable<Build>> GetBuildsForHeldItemAsync(int heldItemId);
        Task<bool> ValidateHeldItemExistsAsync(int id);
    }
}