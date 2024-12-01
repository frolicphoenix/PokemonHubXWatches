using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IBuildService
    {
        Task<IEnumerable<Build>> GetAllAsync();
        Task<Build?> GetByIdAsync(int id);
        Task AddAsync(Build build);
        Task UpdateAsync(Build build);
        Task DeleteAsync(int id);
    }
}
