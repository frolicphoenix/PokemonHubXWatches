using PokemonHubXWatches.Models;

namespace PokemonHubXWatches.Interfaces
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> GetAllPokemonAsync();
        Task<Pokemon> GetPokemonByIdAsync(int id);
        Task<Pokemon> CreatePokemonAsync(Pokemon pokemon);
        Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemon);
        Task<bool> DeletePokemonAsync(int id);
        Task<IEnumerable<Pokemon>> GetTopPokemonAsync(int count);
        Task<IEnumerable<HeldItem>> GetHeldItemsForPokemonAsync(int pokemonId);
        Task<bool> ValidatePokemonExistsAsync(int id);
    }
}