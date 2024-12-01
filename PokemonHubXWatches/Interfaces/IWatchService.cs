using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IWatchService
    {
        Task<IEnumerable<Watch>> GetAllAsync();
        Task<Watch?> GetByIdAsync(int id);
        Task AddAsync(Watch watch);
        Task UpdateAsync(Watch watch);
        Task DeleteAsync(int id);
    }
}
