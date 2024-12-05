using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IWatchService
    {
        Task<IEnumerable<Watch>> GetAllWatchesAsync();
        Task<Watch> GetWatchByIdAsync(int id);
        Task<Watch> CreateWatchAsync(Watch watch);
        Task<Watch> UpdateWatchAsync(int id, Watch watch);
        Task<bool> DeleteWatchAsync(int id);
        Task<IEnumerable<Watch>> GetTopWatchesAsync(int count);
        Task<IEnumerable<Watch>> GetAvailableWatchesAsync();
        Task<bool> IsWatchAvailableAsync(int watchId);
        Task<bool> ValidateWatchExistsAsync(int id);
    }
}