using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IHeldItemService
    {
        Task<IEnumerable<HeldItem>> GetAllAsync();
        Task<HeldItem?> GetByIdAsync(int id);
        Task AddAsync(HeldItem heldItem);
        Task UpdateAsync(HeldItem heldItem);
        Task DeleteAsync(int id);
    }
}
